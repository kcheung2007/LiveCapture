using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Net.Mail;
using System.Net.Mime;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Xml;
using System.Windows.Forms;

namespace LiveCapture
{
    public partial class UcMailSender : UserControl
    {
        private CommObj commObj = new CommObj();
        private AttachObj attachObj; // for attachment
        private String msgCaption = "Mail Sender Page";
        private Thread[] m_thdList;
        private static int m_thdCount;
        private DateTime m_procStartTime;
        private DateTime m_procEndTime;
        private int m_initialThread;
        private int m_delay;
        private int m_sentMail;        

        private string m_MailFrom = "";
        private string m_RcptTo = ""; // share with both smtp and ms api
        private string m_To = "";
        private string m_CC = "";
        private string m_BCC = "";
        private string m_Subject = "";
        private string m_smtpServer = "";
        private string m_portNumber = "";
        private string m_txtInputFile = ""; // use by either smtp case or normal case
        private string m_ZANTAZ_Header = "";
        private string m_ZANTAZ_Value = "";
        
        //public event UpdateStatusEventHandler statusChanged;
        public event EventHandler<StatusEventArgs> statusChanged;

        private delegate void DelegateJobDoneNotification(int thdId); // all thread done - indicate Thread index
        private DelegateJobDoneNotification m_delegateJobDoneNotification;

        private delegate void DelegateUpdate_txtNumMail(int numMail);
        private DelegateUpdate_txtNumMail m_delegateNumMailCtrl;

        private delegate void DelegateUpdate_txtSubject(string szSubject);
        private DelegateUpdate_txtSubject m_delegateShowSubject;        

        public UcMailSender()
        {
            InitializeComponent();

            m_delegateJobDoneNotification = new DelegateJobDoneNotification(JobDoneHandler);
            m_delegateNumMailCtrl = new DelegateUpdate_txtNumMail(Update_txtNumMail);
            m_delegateShowSubject = new DelegateUpdate_txtSubject(Update_txtSubject);

            // save for reference - how to use the ini file
            //commObj.InitComboBoxItem(cboTo, "[To Address]");
        }

        private void Update_txtSubject(string szSubject)
        {
            txtSubject.Text = szSubject;
        }//end of Update_txtSubject

        private void Update_txtNumMail(int numMail)
        {
            txtNumMail.Text = numMail.ToString();
        }//end of Update_txtNumMail

        // "StatusEventArgs" - argument in EventArgs class
        protected virtual void OnUpdateStatusBar(StatusEventArgs eArgs)
        {
            statusChanged(this, eArgs);
        }//end of virtual

        #region Initial combo box control Code
        /// <summary>
        /// Save the list of combo box items by calling other method: WriteComboBoxEntries()
        /// The Write Order is important...
        /// </summary>
        /// 
        string m_xmlFileName = "InitMailSender.xml";
        private void SaveComboBoxItem()
        {
            Debug.WriteLine("UcMailSender.cs - SaveComboBoxItem");
            XmlTextWriter tw = null;
            try
            {
                string cboPath = Application.StartupPath + "\\" + m_xmlFileName;
                if (!File.Exists(cboPath))
                {
                    using (StreamWriter sw = File.CreateText(cboPath))
                    {
                    }//end of using - for auto close etc... yes... empty
                }

                // Save the combox
                tw = new XmlTextWriter(Application.StartupPath + "\\" + m_xmlFileName, System.Text.Encoding.UTF8);

                Debug.WriteLine("\t ComboBox Item file" + Application.StartupPath + "\\" + m_xmlFileName);

                tw.WriteStartDocument();
                tw.WriteStartElement("InitData");

                //The order is important - match with InitMailSender.xml and switch case in load combo box
                WriteComboBoxEntries(cboFileFrom, "cboFileFrom", cboFileFrom.Text, tw); //nodeList.Item(0)
                WriteComboBoxEntries(cboTo, "cboTo", cboTo.Text, tw); //nodeList.Item(1)
                WriteComboBoxEntries(cboCC, "cboCC", cboCC.Text, tw); //nodeList.Item(2)
                WriteComboBoxEntries(cboBCC, "cboBCC", cboBCC.Text, tw); //nodeList.Item(3)
                WriteComboBoxEntries(cboRcptTo, "cboRcptTo", cboRcptTo.Text, tw); //nodeList.Item(4)
                WriteComboBoxEntries(cboMailFrom, "cboMailFrom", cboMailFrom.Text, tw); //nodeList.Item(5)

                WriteComboBoxEntries(cboHeader, "cboHeader", cboHeader.Text, tw); //nodeList.Item(6)
                WriteComboBoxEntries(cboHeaderVal, "cboHeaderVal", cboHeaderVal.Text, tw); //nodeList.Item(7)

                WriteComboBoxEntries(cboSMTP, "cboSMTP", cboSMTP.Text, tw); //nodeList.Item(8)
                WriteComboBoxEntries(cboPort, "cboPort", cboPort.Text, tw); //nodeList.Item(9)

                // Text Box but not combo box
                // WriteTextBoxEntries(txtInputFile, "txtInputFile", txtInputFile.Text, tw);
                // WriteComboBoxEntries(txtMailFolder, "txtFolder", txtMailFolder.Text, tw);

                tw.WriteEndElement();
            }//end of try
            catch (Exception ex)
            {
                string msg = ex.Message + "\n" + ex.GetType().ToString() + ex.StackTrace;
                Debug.WriteLine(msg, "SaveComboBoxItem()");
            }//end of catch
            finally
            {
                if (tw != null)
                {
                    tw.Flush();
                    tw.Close();
                }
            }

            LoadComboBoxes();
        }//end of SaveComboBoxItem

        /// <summary>
        /// Write a list of combox box entries into an xml file
        /// </summary>
        /// <param name="cboBox">ComboBox control</param>
        /// <param name="cboBoxName">Name of the control in XML</param>
        /// <param name="cboBoxText">The input text in combo box</param>
        /// <param name="tw">XmlTextWriter</param>
        private void WriteComboBoxEntries(ComboBox cboBox, string cboBoxName, string txtBoxText, XmlTextWriter tw)
        {
            Debug.WriteLine("UcMailSender.cs - WriteComboBoxEntries");
            int maxEntriesToStore = 10;

            tw.WriteStartElement("combobox");
            tw.WriteStartAttribute("name", string.Empty);
            tw.WriteString(cboBoxName);
            tw.WriteEndAttribute();

            // Write the item from the text box first.
            if (txtBoxText.Length != 0)
            {
                tw.WriteStartElement("entry");
                tw.WriteString(txtBoxText);
                tw.WriteEndElement();
                maxEntriesToStore -= 1;
            }//end of if

            // Write the rest of the entries (up to 10).
            for (int i = 0; i < cboBox.Items.Count && i < maxEntriesToStore; ++i)
            {
                if (txtBoxText != cboBox.Items[i].ToString())
                {
                    tw.WriteStartElement("entry");
                    tw.WriteString(cboBox.Items[i].ToString());
                    tw.WriteEndElement();
                }
            }//end of for
            tw.WriteEndElement();
        }//end of WriteComboBoxEntries

        //private void WriteTextBoxEntries(TextBox txtBox, string txtBoxName, string txtBoxText, XmlTextWriter tw)
        //{
        //    Debug.WriteLine("UcMailSender.cs - WriteTextBoxEntries");
        //    int maxEntriesToStore = 1;

        //    tw.WriteStartElement("textbox");
        //    tw.WriteStartAttribute("name", string.Empty);
        //    tw.WriteString(txtBoxName);
        //    tw.WriteEndAttribute();

        //    // Write the item from the text box first.
        //    if (txtBoxText.Length != 0)
        //    {
        //        tw.WriteStartElement("entry");
        //        tw.WriteString(txtBoxText);
        //        tw.WriteEndElement();
        //        maxEntriesToStore -= 1;
        //    }//end of if

        //     Write the rest of the entries (up to 10).
        //    for (int i = 0; i < cboBox.Items.Count && i < maxEntriesToStore; ++i)
        //    {
        //        if (txtBoxText != cboBox.Items[i].ToString())
        //        {
        //            tw.WriteStartElement("entry");
        //            tw.WriteString(cboBox.Items[i].ToString());
        //            tw.WriteEndElement();
        //        }
        //    }//end of for
        //    tw.WriteEndElement();
        //}//end of WriteTextBoxEntries
        /// <summary>
        /// Load the text value into combo boxes. (OK... hardcode)
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage( "Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity" )]
        private void LoadComboBoxes()
        {
            Debug.WriteLine("UcMailSender.cs - LoadComboBoxes");
            try
            {
                cboFileFrom.Items.Clear();
                cboTo.Items.Clear();
                cboCC.Items.Clear();
                cboBCC.Items.Clear();
                cboHeader.Items.Clear();
                cboHeaderVal.Items.Clear();
                cboMailFrom.Items.Clear();
                cboRcptTo.Items.Clear();
                cboSMTP.Items.Clear();
                cboPort.Items.Clear();

                XmlDocument xdoc = new XmlDocument();
                string cboPath = Application.StartupPath + "\\" + m_xmlFileName;
                if (!File.Exists(cboPath))
                {
                    //File.CreateText(cboPath);
                    SaveComboBoxItem();
                    return;
                }//end of if - full path file doesn't exist

                xdoc.Load(cboPath);
                XmlElement root = xdoc.DocumentElement;
                XmlNodeList nodeList = root.ChildNodes;

                int numComboBox = nodeList.Count; // count text box also
                int x;
                for (int i = 0; i < numComboBox; i++) // Order is important here
                {
                    switch (nodeList.Item(i).Attributes.GetNamedItem("name").InnerText)
                    {
                        case "cboFileFrom":
                            for (x = 0; x < nodeList.Item(0).ChildNodes.Count; ++x)
                            {
                                cboFileFrom.Items.Add(nodeList.Item(0).ChildNodes.Item(x).InnerText);
                            }
                            break;
                        case "cboTo":
                            for (x = 0; x < nodeList.Item(1).ChildNodes.Count; ++x)
                            {
                                cboTo.Items.Add(nodeList.Item(1).ChildNodes.Item(x).InnerText);
                            }
                            break;
                        case "cboCC":
                            for (x = 0; x < nodeList.Item(2).ChildNodes.Count; ++x)
                            {
                                cboCC.Items.Add(nodeList.Item(2).ChildNodes.Item(x).InnerText);
                            }
                            break;
                        case "cboBCC":
                            for (x = 0; x < nodeList.Item(3).ChildNodes.Count; ++x)
                            {
                                cboBCC.Items.Add(nodeList.Item(3).ChildNodes.Item(x).InnerText);
                            }
                            break;
                        case "cboRcptTo":
                            for (x = 0; x < nodeList.Item(4).ChildNodes.Count; ++x)
                            {
                                cboRcptTo.Items.Add(nodeList.Item(4).ChildNodes.Item(x).InnerText);
                            }
                            break;
                        case "cboMailFrom":
                            for (x = 0; x < nodeList.Item(5).ChildNodes.Count; ++x)
                            {
                                cboMailFrom.Items.Add(nodeList.Item(5).ChildNodes.Item(x).InnerText);
                            }
                            break;
                        case "cboHeader":
                            for (x = 0; x < nodeList.Item(6).ChildNodes.Count; ++x)
                            {
                                cboHeader.Items.Add(nodeList.Item(6).ChildNodes.Item(x).InnerText);
                            }
                            break;
                        case "cboHeaderVal":
                            for (x = 0; x < nodeList.Item(7).ChildNodes.Count; ++x)
                            {
                                cboHeaderVal.Items.Add(nodeList.Item(7).ChildNodes.Item(x).InnerText);
                            }
                            break;
                        case "cboSMTP":
                            for (x = 0; x < nodeList.Item(8).ChildNodes.Count; ++x)
                            {
                                cboSMTP.Items.Add(nodeList.Item(8).ChildNodes.Item(x).InnerText);
                            }
                            break;
                        case "cboPort":
                            for (x = 0; x < nodeList.Item(9).ChildNodes.Count; ++x)
                            {
                                cboPort.Items.Add(nodeList.Item(9).ChildNodes.Item(x).InnerText);
                            }
                            break;
                    }//end of switch
                }//end of for

                if(0 < cboFileFrom.Items.Count)
                    cboFileFrom.Text = cboFileFrom.Items[0].ToString();
                if (0 < cboTo.Items.Count)
                    cboTo.Text = cboTo.Items[0].ToString();
                if (0 < cboCC.Items.Count)
                    cboCC.Text = cboCC.Items[0].ToString();
                if (0 < cboBCC.Items.Count)
                    cboBCC.Text = cboBCC.Items[0].ToString();
                if (0 < cboRcptTo.Items.Count) 
                    cboRcptTo.Text = cboRcptTo.Items[0].ToString();
                if (0 < cboMailFrom.Items.Count) 
                    cboMailFrom.Text = cboMailFrom.Items[0].ToString();
                if (0 < cboHeader.Items.Count)
                    cboHeader.Text = cboHeader.Items[0].ToString();
                if (0 < cboHeaderVal.Items.Count)
                    cboHeaderVal.Text = cboHeaderVal.Items[0].ToString();
                if (0 < cboSMTP.Items.Count)
                    cboSMTP.Text = cboSMTP.Items[0].ToString();
                if (0 < cboPort.Items.Count)
                    cboPort.Text = cboPort.Items[0].ToString();

            }//end of try
            catch (Exception ex)
            {
                string msg = ex.Message + "\n" + ex.GetType().ToString() + ex.StackTrace;
                Debug.WriteLine(msg, "Exception");
                MessageBox.Show(msg, "LoadComboBoxes()");
            }//end of catch
        }//end of //end of LoadComboBoxes
        #endregion

        #region Handle Key Down and Press
        private void cboFileFrom_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyValue)
            {
                case 13: // enter key down
                    SaveComboBoxItem();
                    break;
            }//end of switch

        }

        private void cboTo_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyValue)
            {
                case 13: // enter key down
                    SaveComboBoxItem();
                    break;
            }//end of switch

        }

        private void cboCC_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyValue)
            {
                case 13: // enter key down
                    SaveComboBoxItem();
                    break;
            }//end of switch

        }

        private void cboBCC_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyValue)
            {
                case 13: // enter key down
                    SaveComboBoxItem();
                    break;
            }//end of switch

        }

        private void cboHeader_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyValue)
            {
                case 13: // enter key down
                    SaveComboBoxItem();
                    break;
            }//end of switch

        }

        private void cboHeaderVal_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyValue)
            {
                case 13: // enter key down
                    SaveComboBoxItem();
                    break;
            }//end of switch

        }

        private void cboMailFrom_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyValue)
            {
                case 13: // enter key down
                    SaveComboBoxItem();
                    break;
            }//end of switch

        }

        private void cboRcptTo_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyValue)
            {
                case 13: // enter key down
                    SaveComboBoxItem();
                    break;
            }//end of switch

        }

        private void cboSMTP_KeyDown(object sender, KeyEventArgs e)
        {
           switch (e.KeyValue)
            {
                case 13: // enter key down
                    SaveComboBoxItem();
                    break;
            }//end of switch
        }

        private void cboPort_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyValue)
            {
                case 13: // enter key down
                    SaveComboBoxItem();
                    break;
            }//end of switch
        }

        private void cboFileFrom_KeyPress(object sender, KeyPressEventArgs e)
        {
            AutoCompleteCombo(sender, e);
        }

        private void cboTo_KeyPress(object sender, KeyPressEventArgs e)
        {
            AutoCompleteCombo(sender, e);
        }

        private void cboCC_KeyPress(object sender, KeyPressEventArgs e)
        {
            AutoCompleteCombo(sender, e);
        }

        private void cboBCC_KeyPress(object sender, KeyPressEventArgs e)
        {
            AutoCompleteCombo(sender, e);
        }

        private void cboMailFrom_KeyPress(object sender, KeyPressEventArgs e)
        {
            AutoCompleteCombo(sender, e);
        }

        private void cboRcptTo_KeyPress(object sender, KeyPressEventArgs e)
        {
            AutoCompleteCombo(sender, e);
        }

        private void cboHeader_KeyPress(object sender, KeyPressEventArgs e)
        {
            AutoCompleteCombo(sender, e);
        }

        private void cboHeaderVal_KeyPress(object sender, KeyPressEventArgs e)
        {
            AutoCompleteCombo(sender, e);
        }

        private void cboPort_KeyPress(object sender, KeyPressEventArgs e)
        {
            AutoCompleteCombo(sender, e);
        }

        private void cboSMTP_KeyPress(object sender, KeyPressEventArgs e)
        {
            AutoCompleteCombo(sender, e);
        }

        /// <summary>
        /// Handle the auto completion for user input in combo box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AutoCompleteCombo(object sender, KeyPressEventArgs e)
        {
            ComboBox cb = (ComboBox)sender;//this.ComboBox1
            if (Char.IsControl(e.KeyChar))
                return;

            String ToFind = cb.Text.Substring(0, cb.SelectionStart) + e.KeyChar;
            int index = cb.FindStringExact(ToFind);
            if (index == -1)
                index = cb.FindString(ToFind);

            if (index == -1)
                return;

            cb.SelectedIndex = index;
            cb.SelectionStart = ToFind.Length;
            cb.SelectionLength = cb.Text.Length - cb.SelectionStart;
            e.Handled = true;

        }//end of AutoCompleteCombo
        #endregion

        private void btnTest_Click(object sender, EventArgs e)
        {
            Trace.WriteLine("UcMailSender.cs - lnkTest_LinkClicked");
            this.Cursor = Cursors.WaitCursor;
            richBox.Text = commObj.TestSMTPConnection(cboSMTP.Text, cboPort.Text) ? "Connection OK" : "Connection FAIL";
            this.Cursor = Cursors.Default;
        }//end of btnTest_Click

        private void EnableFileCaseCtrl(bool value)
        {
            lnkFile.Enabled = value;
            txtMailAddrFile.Enabled = value;
        }//end of EnableFileCase

        private void EnableNormalCaseCtrl(bool value)
        {
            //rdoNormal.Checked = value;
            lnkSender.Enabled = value;
            lnkTo.Enabled = value;
            lnkCC.Enabled = value;
            lnkBCC.Enabled = value;
            cboFileFrom.Enabled = value;
            cboTo.Enabled = value;
            cboCC.Enabled = value;
            cboBCC.Enabled = value;
            lblHeader.Enabled = value;
            cboHeader.Enabled = value;
            lblValue.Enabled = value;
            cboHeaderVal.Enabled = value;
        }//end of EnableNormalCaseCtrl

        private void EnableSmtpCaseCtrl(bool value)
        {
            //rdoSMTP.Checked = value;
            lblRcptTo.Enabled = value;
            lblMailFrom.Enabled = value;
            cboRcptTo.Enabled = value;
            cboMailFrom.Enabled = value;
            txtInputFile.Enabled = value;
            txtMailFolder.Enabled = value;
            rdoInputFile.Enabled = value;
            rdoMsgFolder.Enabled = value;
            
            if (value == true)
            {
                if (rdoInputFile.Checked)
                {
                    txtInputFile.Enabled = true;
                    txtMailFolder.Enabled = false;
                    btnBroFolder.Enabled = false;
                    btnBroMailFile.Enabled = true;                    
                }
                else
                {
                    txtInputFile.Enabled = false;
                    txtMailFolder.Enabled = true;
                    btnBroFolder.Enabled = true;
                    btnBroMailFile.Enabled = false;                    
                }
            }//end of value == true
            else
            {   // value == false
                txtInputFile.Enabled = false;
                txtMailFolder.Enabled = false;
                btnBroFolder.Enabled = false;
                btnBroMailFile.Enabled = false;                
            }
        }//end of EnableSmtpCaseCtrl

        private void EnableOtherCtrl(bool value)
        {
            chkGUID.Enabled = value;
            chkAttach.Enabled = value;
            lblSubject.Enabled = value;
            lnkFolder.Enabled = value;
            txtSubject.Enabled = value;
            txtFolder.Enabled = value;
        }//end of EnableOtherCtrl

        /// <summary>
        /// Handle enable or disable controls
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rdoFileCase_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("UcMailSender.cs - rdoFileCase_Click");
            rdoNormal.Checked = false;
            rdoSMTP.Checked = false;
            rdoFileCase.Checked = true;

            // enable normal group control
            EnableNormalCaseCtrl(false);
            EnableSmtpCaseCtrl(false);
            EnableFileCaseCtrl(true);
            EnableOtherCtrl(true);

            OnUpdateStatusBar(new StatusEventArgs("Dynamic send simple mail by using MS mail API."));
        }//end of rdoFileCase_Click

        /// <summary>
        /// Handle enable or disable of the controls
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rdoNormal_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("UcMailSender.cs - rdoNormal_Click" );
            rdoNormal.Checked = true;
            rdoSMTP.Checked = false;
            rdoFileCase.Checked = false;

            // enable normal group control
            EnableNormalCaseCtrl(true);
            EnableSmtpCaseCtrl(false);
            EnableFileCaseCtrl(false);
            EnableOtherCtrl(true);

            OnUpdateStatusBar(new StatusEventArgs("Dynamic send simple mail by using MS mail API."));
        }//end of rdoNormal_Click

        /// <summary>
        /// Handle enable or disable of the controls
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rdoSMTP_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("UcMailSender.cs - rdoSMTP_Click");
            rdoNormal.Checked = false;
            rdoSMTP.Checked = true;
            rdoFileCase.Checked = false;

            EnableNormalCaseCtrl(false);
            EnableSmtpCaseCtrl(true);
            EnableFileCaseCtrl(false);
            EnableOtherCtrl(true);

            OnUpdateStatusBar(new StatusEventArgs("Stream a eml file from one address to another address"));
        }//end of rdoSMTP_Click

        private void chkAttach_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAttach.Checked)
            {
                lnkFolder.Enabled = true;
                txtFolder.Enabled = true;
            }
            else
            {
                lnkFolder.Enabled = false;
                txtFolder.Enabled = false;
            }//end of else - disable
        }// end of chkAttach_CheckedChanged

        private void btnSend_Click(object sender, EventArgs e)
        {
            Trace.WriteLine("UcMailSender.cs - btnSend_Click");

            if (chkAttach.Checked)
            {
                attachObj = new AttachObj(txtFolder.Text);
            }// end of if - attachment check

            if (rdoNormal.Checked)
            {
                doMsApiMailing();
            }//end of if - do the normal mailing
            else                    
                if (rdoSMTP.Checked)
                {
                    doSocketMailing();
                }// end of if - stream file into socket
                else
                    if (rdoFileCase.Checked)
                    {
                        doFileCaseMailing();
                    }
        }//end of btnSend_Click

        private void doFileCaseMailing()
        {
            if (!ValidateFileCaseInput())
                return;

            SetFileCaseData(); // avoiding cross-thread operation - timer start inside this function

            EnableNormalCaseCtrl(false);
            EnableSmtpCaseCtrl(false);
            EnableFileCaseCtrl(false);
            EnableOtherCtrl(false);
            btnSend.Enabled = false; // exclusive between Send button and abort button
            //btnAbort.Enabled = true;

            m_thdList = new Thread[m_initialThread];
            for (int i = 0; i < m_initialThread; i++)
            {
                m_thdList[i] = new Thread(new ThreadStart(this.Thd_SendFileMail));
                m_thdList[i].Name = "Thd_SendFileMail_" + i.ToString();
                m_thdList[i].Start();
#if(DEBUG)
                commObj.LogToFile("Thread.log", "Start Thread: " + m_thdList[i].Name);
#endif
            }
        }//end of doFileCaseMailing

        private void doMsApiMailing()
        {
            if (!ValidateMsApiInput())
                return;

            SetMsApiData(); // avoiding cross-thread operation - timer start inside this function

            EnableNormalCaseCtrl(false);
            EnableSmtpCaseCtrl(false);
            EnableFileCaseCtrl(false);
            EnableOtherCtrl(false);
            btnSend.Enabled = false; // exclusive between Send button and abort button
            //btnAbort.Enabled = true;

            m_thdList = new Thread[m_initialThread];
            for (int i = 0; i < m_initialThread; i++)
            {
                m_thdList[i] = new Thread(new ThreadStart(this.Thd_SendGuidMail));
                m_thdList[i].Name = "Thd_SendGuidMail_" + i.ToString();
                m_thdList[i].Start();
#if(DEBUG)
                commObj.LogToFile("Thread.log", "Start Thread: " + m_thdList[i].Name);
#endif
            }//end of for
        }//end of doMsApiMailing

        private void SetFileCaseData()
        {
            m_initialThread = (int)nudThread.Value;
            m_delay = (int)nudDelay.Value;

            m_Subject = txtSubject.Text;
            m_smtpServer = cboSMTP.Text;
            m_portNumber = cboPort.Text;

            // reset value
            m_thdCount = 0;
            m_sentMail = 0;
            m_thdList = null;
            m_procStartTime = DateTime.Now;
            txtStartTime.Text = m_procStartTime.ToString();
            txtEndTime.Text = "";
            txtNumMail.Text = "";
            txtMailPerSec.Text = "";
            txtMailsSize.Text = "";
            txtAveSize.Text = "";
            txtDuration.Text = "";
        }//end of SetFileCaseData

        private void SetMsApiData()
        {
            m_initialThread = (int)nudThread.Value;
            m_delay = (int)nudDelay.Value;

//            m_MailFrom = cboMailFrom.Text;
            m_txtInputFile = cboFileFrom.Text; // file that contains sender addresses
            m_RcptTo = cboTo.Text;
            m_CC = cboCC.Text;
            m_BCC = cboBCC.Text;
            m_Subject = txtSubject.Text;
            m_smtpServer = cboSMTP.Text;
            m_portNumber = cboPort.Text;
            m_ZANTAZ_Header = cboHeader.Text;
            m_ZANTAZ_Value = cboHeaderVal.Text;

            // reset value
            m_thdCount = 0;
            m_sentMail = 0;
            m_thdList = null;
            m_procStartTime = DateTime.Now;
            txtStartTime.Text = m_procStartTime.ToString();
            txtEndTime.Text = "";
            txtNumMail.Text = "";
            txtMailPerSec.Text = "";
            txtMailsSize.Text = "";
            txtAveSize.Text = "";
            txtDuration.Text = "";
        }//end of SetApiData

        private void SetSmtpData()
        {
            m_initialThread = (int)nudThread.Value;
            m_delay = (int)nudDelay.Value;

            m_MailFrom = cboMailFrom.Text;
            m_RcptTo = cboRcptTo.Text;
            m_smtpServer = cboSMTP.Text;
            m_portNumber = cboPort.Text;

            if (rdoInputFile.Checked)
                m_txtInputFile = txtInputFile.Text;

            // reset value
            m_thdCount = 0;
            m_sentMail = 0;
            m_thdList = null;
            m_procStartTime = DateTime.Now;
            txtStartTime.Text = m_procStartTime.ToString();
            txtEndTime.Text = "";
            txtNumMail.Text = "";
            txtMailPerSec.Text = "";
            txtMailsSize.Text = "";
            txtAveSize.Text = "";
            txtDuration.Text = "";
        }//end of SetSmtpData

        private bool ValidateFileCaseInput()
        {
            bool rv = true; // assume everything is ok
            //if (txtMailAddrFile.Text == "")
            if( String.IsNullOrEmpty(txtMailAddrFile.Text) )
            {
                txtMailAddrFile.Focus();
                txtMailAddrFile.BackColor = Color.Yellow;
                rv = false;
            }
            return (rv);
        }//end of ValidateFileCaseInput

        private bool ValidateMsApiInput()
        {
            bool rv = true; // assume everything is ok
            //if (cboFileFrom.Text == "")
            if( String.IsNullOrEmpty(cboFileFrom.Text) )
            {
                cboFileFrom.Focus();
                cboFileFrom.BackColor = Color.Yellow;
                rv = false;
            }
            else
                //if((cboTo.Text == "") && (cboCC.Text == "") && (cboBCC.Text == "")) // MS API Must have To field
                if((String.IsNullOrEmpty(cboTo.Text)) && (String.IsNullOrEmpty(cboCC.Text)) && (String.IsNullOrEmpty(cboBCC.Text))) // MS API Must have To field
                {
                    cboTo.Focus();
                    cboTo.BackColor = Color.Yellow;
                    rv = false;
                }                
            return (rv);
        }//end of ValidateMsApiInput

        /// <summary>
        /// Validate user input
        /// </summary>
        /// <returns>bool: OK - true; Fail - false</returns>
        private bool ValidateSmtpInput()
        {
            bool rv = true; // assume everything is ok
            //if (cboMailFrom.Text == "")
            if(String.IsNullOrEmpty(cboMailFrom.Text))
            {
                cboMailFrom.Focus();
                cboMailFrom.BackColor = Color.Yellow;
                rv = false;
            }//end of check Mail From
            else
                //if (cboRcptTo.Text == "")
                if(String.IsNullOrEmpty(cboRcptTo.Text))
                {
                    cboRcptTo.Focus();
                    cboRcptTo.BackColor = Color.Yellow;
                    rv = false;
                }
                else
                {
                    if (rdoInputFile.Checked)
                    {
                        //if (txtInputFile.Text == "")
                        if( String.IsNullOrEmpty(txtInputFile.Text))
                        {
                            txtInputFile.Focus();
                            txtInputFile.BackColor = Color.Yellow;
                            rv = false;
                        }
                    }
                    else
                        if (rdoMsgFolder.Checked)
                        {
                            //if (txtMailFolder.Text == "")
                            if( String.IsNullOrEmpty(txtMailFolder.Text) )
                            {
                                txtMailFolder.Focus();
                                txtMailFolder.BackColor = Color.Yellow;
                                rv = false;
                            }
                        }//end of rdoMsgFolder
                }//end of else
            return (rv);
        }//end of ValidateSmtpInput

        private void doSocketMailing()
        {
            if (!ValidateSmtpInput())
                return;

            SetSmtpData(); // avoiding cross-thread operation - timer start inside this function
            
            EnableNormalCaseCtrl(false);
            EnableSmtpCaseCtrl(false);
            EnableFileCaseCtrl(false);
            EnableOtherCtrl(false);
            btnSend.Enabled = false; // exclusive between Send button and abort button
            //btnAbort.Enabled = true;

            m_thdList = new Thread[m_initialThread];
            for (int i = 0; i < m_initialThread; i++)
            {
                m_thdList[i] = new Thread(new ThreadStart(this.Thd_SendSmtpMail));
                m_thdList[i].Name = "Thd_SendSmtpMail_" + i.ToString();
                m_thdList[i].Start();
#if(DEBUG)
                commObj.LogToFile("Thread.log", "Start Thread: " + m_thdList[i].Name);
#endif
            }//end of for
        }//end of doSocketMailing

        private void lnkSender_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Trace.WriteLine("UcMailSender.cs - lnkFrom_LinkClicked");

            OpenFileDialog ofDlg = new OpenFileDialog();
            ofDlg.ShowReadOnly = true;
            ofDlg.RestoreDirectory = true;
            if (ofDlg.ShowDialog() == DialogResult.OK)
            {
                cboFileFrom.Text = ofDlg.FileName;
            }//end of if
        }//end of lnkSender_LinkClicked

        private void lnkFolder_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Trace.WriteLine("UcMailSender.cs - lnkFolder_LinkClicked");
            FolderBrowserDialog fbDlg = new FolderBrowserDialog();

            fbDlg.RootFolder = Environment.SpecialFolder.MyComputer; // set the default root folder
            if (txtFolder.Text != null)
                fbDlg.SelectedPath = txtFolder.Text;  // set the default folder

            if (fbDlg.ShowDialog() == DialogResult.OK)
            {
                txtFolder.Text = fbDlg.SelectedPath;
            }
        }//end of lnkFolder_LinkClicked

        public void Thd_SendFileMail()
        {
            Debug.WriteLine("UcMailSender.cs - Thd_SendFileMail");
            string strGUID = "";
            StreamReader sr = null;
            DateTime startTime = DateTime.Now;
            try
            {
                lock (this)
                {
                    m_thdCount++; // increment thread count
                    Debug.WriteLine("\t Start send FILE mail thread Count: " + m_thdCount);
                    OnUpdateStatusBar(new StatusEventArgs("Start send FILE mail : Thread Count = " + m_thdCount.ToString()));
                }
                for (int i = 0; i < nudCycle.Value; i++)
                {
                    Debug.WriteLine("\t In Cycle " + i.ToString());                    
                    string strLine; // read from file

                    try
                    {
                        sr = new StreamReader(txtMailAddrFile.Text); // address book - put here for exception catch
                        while ((strLine = sr.ReadLine()) != null) // file name from txtFrom field
                        {
                            //counter++;
                            Debug.WriteLine("\t - AutoSendMail - inside while loop : " + m_sentMail.ToString());

                            strGUID = System.Guid.NewGuid().ToString();
                            if (strLine[0] != '#') // skip all comment
                            {
                                //richBox.Text += "\r\nRead line : " + strLine;
                                //if (chkGUID.Checked)
                                //{
                                //    strGUID = System.Guid.NewGuid().ToString();
                                //    txtSubject.Text = savSubj + counter.ToString() + " " + strGUID;
                                //    commObj.LogGuid("GUID.LOG", strGUID);
                                //}//end of if - GUID			

                                // parse each line from the file, which defines a specific field:
                                // Total 4 fields: FROM, TO, CC, BCC and separated by commer.
                                // Fill can be null, and store in an array.
                                string[] splitStr = new string[4];
                                splitStr = strLine.Split(new Char[] { ',' });
                                for (int k = 0; k < splitStr.Length; k++) // trim leading and ending space
                                    splitStr[k] = splitStr[k].Trim(' ');

                                //m_MailFrom = splitStr[0];
                                //m_RcptTo = splitStr[1];
                                //m_CC = splitStr[2];
                                //m_BCC = splitStr[3];

                                // Create mail address
                                MailMessage mailMsg = new MailMessage();
                                //if (splitStr[0] != "") // add from
                                if( !String.IsNullOrEmpty(splitStr[0]) ) // add from
                                {
                                    MailAddress from = new MailAddress(splitStr[0]);
                                    mailMsg.From = from;
                                }
                                //if (splitStr[1] != "") // add to
                                if( !String.IsNullOrEmpty(splitStr[1]) ) // add to
                                {
                                    splitStr[1] = splitStr[1].Replace(';', ',');
                                    MailAddress to = new MailAddress(splitStr[1]);
                                    mailMsg.To.Add(to);
                                }

                                //if (splitStr[2] != "") // add CC
                                if( !String.IsNullOrEmpty(splitStr[2]) ) // add CC
                                {
                                    splitStr[2] = splitStr[2].Replace(';', ',');
                                    MailAddress cc = new MailAddress(splitStr[2]);
                                    mailMsg.CC.Add(cc);
                                }
                                //if (splitStr[3] != "") // add BCC
                                if( !String.IsNullOrEmpty(splitStr[3]) ) // add BCC
                                {
                                    splitStr[3] = splitStr[3].Replace(';', ',');
                                    MailAddress bcc = new MailAddress(splitStr[3]);
                                    mailMsg.Bcc.Add(bcc);
                                }

                                if (chkGUID.Checked)
                                {
                                    mailMsg.Subject = m_sentMail.ToString()
                                                    + " " + m_Subject + " : "
                                                    + strGUID;

                                    commObj.LogGuid("GUID.LOG", strGUID);
                                }
                                else
                                {
                                    mailMsg.Subject = m_Subject;
                                }
                                mailMsg.Body = "Sent Mail: " + m_sentMail.ToString()
                                         + "\nFrom: " + splitStr[0]		// start from here change
                                         + "\n TO:  " + splitStr[1]
                                         + "\n CC:  " + splitStr[2]
                                         + "\n BCC: " + splitStr[3]
                                         + "\n Body_Subject: " + mailMsg.Subject
                                         + "\n" + DateTime.Now;


                                if (chkAttach.Checked)
                                {
                                    if (attachObj.IdxAttach == attachObj.NumFile)
                                        attachObj.IdxAttach = 0; //reset

                                    Debug.WriteLine(attachObj.IdxAttach, "\t - idxAttach");
                                    Debug.WriteLine(attachObj.AttachFullName, "\t - filename");
                                    // Create  the file attachment for this e-mail message.
                                    Attachment attachData = new Attachment(attachObj.AttachFullName, MediaTypeNames.Application.Octet);
                                    // Add time stamp information for the file.
                                    ContentDisposition disposition = attachData.ContentDisposition;
                                    disposition.CreationDate = System.IO.File.GetCreationTime(attachObj.AttachFullName);
                                    disposition.ModificationDate = System.IO.File.GetLastWriteTime(attachObj.AttachFullName);
                                    disposition.ReadDate = System.IO.File.GetLastAccessTime(attachObj.AttachFullName);
                                    // Add the file attachment to this e-mail message.
                                    mailMsg.Attachments.Add(attachData);

                                    mailMsg.Body += "\r\nAttach file index = " + attachObj.IdxAttach
                                                  + "\r\nAttach file name = " + attachObj.AttachFullName;

                                    attachObj.IdxAttach++; // point to next file
                                }//end of if

                                Debug.WriteLine("Send mail la");
                                SmtpClient smtpClient = new SmtpClient(m_smtpServer);
                                smtpClient.Port = int.Parse(m_portNumber);

                                smtpClient.Send(mailMsg);

                                lock (this)
                                {
                                    // m_sentMail++; // inc counter
                                    //IAsyncResult rm = BeginInvoke(m_delegateNumMailCtrl, new object[] { m_sentMail++ });
                                    //IAsyncResult rs = BeginInvoke(m_delegateShowSubject, new object[] { mailMsg.Subject });
                                    BeginInvoke( m_delegateNumMailCtrl, new object[] { m_sentMail++ } );
                                    BeginInvoke( m_delegateShowSubject, new object[] { mailMsg.Subject } );
                                }

                                Thread.Sleep( (int)nudDelay.Value * 1000 ); // delay per message
                            }//end of if - skip all commtn                            
                        }//end of while
                    }//end of try
                    catch (SMTPException ex)
                    {
                        Trace.WriteLine("\tSMTPClientSender() Exception: " + ex.SmtpMessage.ToString());
                        commObj.LogToFile("Exception occur" + ex.SmtpMessage.ToString());
                        // MessageBox.Show(ex.SmtpMessage.ToString(), msgCaption);
                    }//end of catch
                    catch (Exception gex)
                    {
                        Debug.WriteLine("\t Generic Exception: " + gex.ToString());
                        commObj.LogToFile("Exception occur" + gex.ToString());
                    }//end of catch generic exception
                    finally
                    {
                        if (sr != null)
                        {
                            Trace.WriteLine("Finally - close the Stream Reader");
                            commObj.LogToFile("Finally - close the Stream Reader");
                            sr.Close();
                        }//end of if

                        lock (this)
                        {
                            if (--m_thdCount == 0)
                            {
                                BeginInvoke(m_delegateJobDoneNotification, new object[] { m_thdCount });
                                commObj.LogToFile("Job Done Notification - FILE mail thread : " + m_thdCount);
                                OnUpdateStatusBar(new StatusEventArgs("Thread Count = " + m_thdCount.ToString()));
                            }                            
                        }

                        OnUpdateStatusBar(new StatusEventArgs("Thread Count = " + m_thdCount.ToString()));
                    }//end of finally
                }//end of for
            }//end of outer try
            catch (Exception finalEx)
            {
                Debug.WriteLine("\t Generic File Exception: " + finalEx.ToString());
                MessageBox.Show(finalEx.Message, msgCaption);
            }
        }//end of Thd_SendFileMail

        public void Thd_SendGuidMail()
        {
            Trace.WriteLine("UcMailSender.cs - Thd_SendGuidMail");
            string strGUID = "";
            StreamReader sr = null;
            DateTime startTime = DateTime.Now;

            try
            {
                lock (this)
                {
                    m_thdCount++; // increment thread count
                    Debug.WriteLine("\t Start send GUI mail thread Count: " + m_thdCount);
                }
                for (int i = 0; i < nudCycle.Value; i++)
                {
                    Debug.WriteLine("\t In Cycle " + i.ToString());                    
                    string strFrom;

                    // save for reference
                    // IPHostEntry hostInfo = Dns.GetHostByName("localhost");
                    // strHostName = hostInfo.HostName;
                    
                    sr = new StreamReader(m_txtInputFile); // address book - put here for exception catch
                    while ((strFrom = sr.ReadLine()) != null) // file name from txtFrom field
                    {
                        //  counter++;
                        Debug.WriteLine("\t - HandleSendMail - inside while loop : " + m_sentMail.ToString());

                        // save for reference
                        // cboHeaderVal.Text = strHostName + ";" + numSent.ToString("00000000000000000000");
                        strGUID = System.Guid.NewGuid().ToString();                        
		
                        // Create mail address
                        MailMessage mailMsg = new MailMessage();
                        //if (strFrom != "") // add from
                        if( !String.IsNullOrEmpty(strFrom) ) // add from
                        {
                            //MailAddress from = new MailAddress(strFrom, "dummy FromName");
                            MailAddress from = new MailAddress( strFrom );
                            mailMsg.From = from;
                        }
                        //if (m_RcptTo != "") // add to
                        if( !String.IsNullOrEmpty(m_RcptTo) ) // add to
                        {
                            //MailAddress to = new MailAddress(m_RcptTo, "RcptTo Name");
                            MailAddress to = new MailAddress( m_RcptTo );
                            mailMsg.To.Add(to);
                        }

                        //if (m_CC != "") // add CC
                        if( !String.IsNullOrEmpty(m_CC) ) // add CC
                        {
                            //MailAddress cc = new MailAddress(m_CC, "CC Name");
                            MailAddress cc = new MailAddress( m_CC );
                            mailMsg.CC.Add(cc);
                        }
                        //if (m_BCC != "") // add BCC
                        if( !String.IsNullOrEmpty(m_BCC) ) // add BCC
                        {
                            //MailAddress bcc = new MailAddress(m_BCC, "BCC display Name");
                            MailAddress bcc = new MailAddress( m_BCC );
                            mailMsg.Bcc.Add(bcc);
                        }

                        //MailAddress from = new MailAddress(strFrom, "dummy from name");
                        //MailAddress to = new MailAddress(m_RcptTo, "RcptTo Name"); // MS API must have Rcpt To

                        //MailMessage mailMsg = new MailMessage(from,to);

                        //if (m_CC != "") // add CC
                        //{
                        //    MailAddress cc = new MailAddress(m_CC, "CC Name");
                        //    mailMsg.CC.Add(cc);
                        //}
                        //if( m_BCC != "") // add BCC
                        //{
                        //    MailAddress bcc = new MailAddress(m_BCC, "BCC display Name");
                        //    mailMsg.Bcc.Add(bcc);
                        //}

                        if (chkGUID.Checked)
                        {
                            mailMsg.Subject = m_sentMail.ToString()
                                            + " " + m_Subject + " : "
                                            + strGUID;

                            commObj.LogGuid("GUID.LOG", strGUID);
                        }
                        else
                        {
                            mailMsg.Subject = m_Subject;
                        }                       

                        mailMsg.Body = "Sent Mail: " + m_sentMail.ToString()
                                 + "\nFrom: " + strFrom		// start from here change
                                 + "\n TO:  " + m_RcptTo
                                 + "\n CC:  " + m_CC
                                 + "\n BCC: " + m_BCC
                                 + "\n Body_Subject: " + mailMsg.Subject
                                 + "\n" + DateTime.Now;

                        // save for reference - this one is needed for creating new doc type
                        // mailMsg.Headers.Add("X-ZANTAZDOCCLASS", "CONFIRMATIONS");
                        //if( m_ZANTAZ_Header != "")
                        if( !String.IsNullOrEmpty(m_ZANTAZ_Header) )
                            mailMsg.Headers.Add(m_ZANTAZ_Header, m_ZANTAZ_Value);

                        // Create attachment
                        if (chkAttach.Checked)
                        {
                            if (attachObj.IdxAttach == attachObj.NumFile)
                                attachObj.IdxAttach = 0; //reset

                            Debug.WriteLine(attachObj.IdxAttach, "\t - idxAttach");
                            Debug.WriteLine(attachObj.AttachFullName, "\t - filename");

                            // Create  the file attachment for this e-mail message.
                            Attachment attachData = new Attachment(attachObj.AttachFullName, MediaTypeNames.Application.Octet);
                            // Add time stamp information for the file.
                            ContentDisposition disposition = attachData.ContentDisposition;
                            disposition.CreationDate = System.IO.File.GetCreationTime(attachObj.AttachFullName);
                            disposition.ModificationDate = System.IO.File.GetLastWriteTime(attachObj.AttachFullName);
                            disposition.ReadDate = System.IO.File.GetLastAccessTime(attachObj.AttachFullName);
                            // Add the file attachment to this e-mail message.
                            mailMsg.Attachments.Add(attachData);

                            mailMsg.Body += "\r\nAttach file index = " + attachObj.IdxAttach
                                          + "\r\nAttach file name = " + attachObj.AttachFullName;

                            attachObj.IdxAttach++; // point to next file
                        }//end of if


                        try
                        {
                            Debug.WriteLine("Send mail la");
                            SmtpClient smtpClient = new SmtpClient(m_smtpServer);
                            smtpClient.Port = int.Parse(m_portNumber);

                            smtpClient.Send(mailMsg);

                            lock (this)
                            {
                                // m_sentMail++; // inc counter
                                IAsyncResult rm = BeginInvoke(m_delegateNumMailCtrl, new object[] { m_sentMail++ });
                                IAsyncResult rs = BeginInvoke(m_delegateShowSubject, new object[] { mailMsg.Subject });
                            }

                            Thread.Sleep( (int)nudDelay.Value * 1000 ); // delay per message
                        }//end of try
                        catch (System.Net.WebException webEx)
                        {
                            Debug.WriteLine(webEx.Message.ToString());
                            commObj.LogToFile("Exception occur" + strGUID.ToString());
                            //	MessageBox.Show(ex.Message.ToString(), msgCaption);
                        }// end of catch
                    }//end of while
                                                               
                }//end of for				
            }//end of outer try
            catch (SMTPException ex)
            {
                Trace.WriteLine("\tSMTPClientSender() Exception: " + ex.SmtpMessage.ToString());
                MessageBox.Show(ex.SmtpMessage.ToString(), "SMTPException");
            }//end of catch
            catch (Exception gex)
            {
                Trace.WriteLine("\t Generic Exception: " + gex.ToString());
                //MessageBox.Show(gex.Message, msgCaption);
                MessageBox.Show("Thread Abort!", msgCaption);
            }//end of catch generic exception
            finally
            {
                if (sr != null)
                {
                    Trace.WriteLine("Finally - close the Stream Reader");
                    commObj.LogToFile("Finally - close the Stream Reader");
                    sr.Close();
                }//end of if

                lock (this)
                {
                    if (--m_thdCount == 0)
                    {
                        BeginInvoke(m_delegateJobDoneNotification, new object[] { m_thdCount });
                        commObj.LogToFile("Job Done Notification - Send GUI Mail thread : " + m_thdCount);
                        OnUpdateStatusBar(new StatusEventArgs("Thread Count = " + m_thdCount.ToString()));
                    }
                } 
            }//end of finally            
        }//end of Thd_SendGuidMail

        /// <summary>
        /// Send mail by streaming the message into SMTP socket.
        /// Option 1: Stream a eml file.
        /// Option 2: Specify a folder that contain a list of eml files and send them all.
        /// </summary>
        public void Thd_SendSmtpMail()
        {
            Trace.WriteLine("UcMailSender.cs - Thd_SendSmtpMail");
            bool bSent = true;
            DateTime startTime = DateTime.Now;

            try
            {
                lock (this)
                {
                    m_thdCount++; // increment thread count
                    Debug.WriteLine("\t Start send smtp mail thread Count: " + m_thdCount);
                }
                for (int i = 0; i < nudCycle.Value; i++)
                {
                    Debug.WriteLine("\t In Cycle " + i.ToString());
                    if (rdoMsgFolder.Checked)
                    {
                        Debug.WriteLine("\t Send mail in Folder");
                        AttachObj mailFiles = new AttachObj(txtMailFolder.Text); // TO DO : Better design Memory intensive
                        for (mailFiles.IdxAttach = 0; mailFiles.IdxAttach < mailFiles.NumFile; mailFiles.IdxAttach++)
                        {
                            Debug.WriteLine("\t mailFile index = " + mailFiles.IdxAttach.ToString());
                            //richBox.Text = mailFile.attchFullName;
                            //richBox.Text = mailFile.idxAttach.ToString();
                            SMTPSender smtpSender = new SMTPSender();
                            smtpSender.mailFrom = m_MailFrom;
                            smtpSender.mailRcptTo = m_RcptTo;
                            smtpSender.smtpServer = m_smtpServer;
                            smtpSender.smtpPortNum = m_portNumber;

                            bSent = smtpSender.SmtpSend(mailFiles.AttachFullName);
                            if (bSent)
                            {
                                lock (this)
                                {
                                    //m_sentMail++;
                                    IAsyncResult r = BeginInvoke(m_delegateNumMailCtrl, new object[] { ++m_sentMail });
                                }
                            }
                        }//end of for - loop through the file in folder
                    }//end of if - message folder
                    else
                    {   // test case file
                        ////richBox.Text = txtInputFile.Text;
                        //SMTPSender smtpSender = new SMTPSender();
                        //smtpSender.mailFrom = m_MailFrom;
                        //smtpSender.mailRcptTo = m_RcptTo;

                        //smtpSender.smtpServer = m_smtpServer;
                        //smtpSender.smtpPortNum = m_portNumber;

                        string strLine; // read one line from file
                        StreamReader sr = null;
                        try
                        {
                            sr = new StreamReader(m_txtInputFile); // from test case spreadsheet
                            while((strLine = sr.ReadLine()) != null) 
                            {
                                if(strLine[0] != '#') // skip all comment
                                {
                                    // parse each line from the file, which defines a specific field:
                                    // Total 5 fields: Subject, TO, CC, BCC, <other> and separated by commer.
                                    // Fill can be null, and store in an array.
                                    string[] splitStr = new string[10];
                                    splitStr = strLine.Split( new Char[] { ',' } );
                                    for(int k = 0; k < splitStr.Length; k++) // trim leading and ending space
                                        splitStr[k] = splitStr[k].Trim( ' ' );

                                    SMTPSender smtpSender = new SMTPSender();
                                    smtpSender.mailFrom = m_MailFrom;
                                    smtpSender.mailRcptTo = m_RcptTo;
                                    smtpSender.smtpServer = m_smtpServer;
                                    smtpSender.smtpPortNum = m_portNumber;

                                    smtpSender.mailSubject = splitStr[0];
                                    smtpSender.mailTo = m_To = splitStr[1].Replace(';', ',');
                                    smtpSender.mailCC = m_CC = splitStr[2].Replace( ';', ',' );
                                    smtpSender.mailBCC = m_BCC = splitStr[3].Replace( ';', ',' );

                                    smtpSender.mailBody = "\r\nFrom = " + m_MailFrom + "<br>"
                                                        + "\r\nRcpt To = " + m_RcptTo + "<br>"
                                                        + "\r\nTo = " + m_To + "<br>"
                                                        + "\r\nCC = " + m_CC + "<br>"
                                                        + "\r\nBCC = " + m_BCC + "<br>"
                                                        + "\r\n" + DateTime.Now;                                                        

                                    //bSent = smtpSender.SmtpSend( m_inclCC_BCC );
                                    bSent = smtpSender.SmtpSend( false ); // enhancement need, therefore, force to false.
                                    if(bSent)
                                    {
                                        lock(this)
                                        {
                                            //m_sentMail++;
                                            IAsyncResult r = BeginInvoke( m_delegateNumMailCtrl, new object[] { ++m_sentMail } );
                                        }
                                    }//end of if - bSent
                                }//end of if - not comment
                            }//end of while
                            sr.Close();
                        }//end of try
                        catch(Exception inEx)
                        {
                            string msg = inEx.Message + "\n" + inEx.GetType().ToString() + inEx.StackTrace;
                            MessageBox.Show( msg, "SMTP Mailing" );
                        }//end of catch - inEx

                        // Change.. no longer read from file.
                        // smtpSender.SmtpSend(m_txtInputFile);
                    }//end of else                    
                }//end of for
            }//end of outer try
            catch (SMTPException ex)
            {
                Trace.WriteLine("\tSMTPClientSender() Exception: " + ex.SmtpMessage.ToString());
                MessageBox.Show(ex.SmtpMessage.ToString(), "SMTP Exception");
            }//end of catch
            catch (Exception gex)
            {
                Trace.WriteLine("\t Generic Exception: " + gex.ToString());
                //MessageBox.Show(gex.Message, msgCaption);
                MessageBox.Show("Thread Abort!", msgCaption);
            }//end of catch generic exception
            finally
            {
                lock (this)
                {
                    //IAsyncResult r = BeginInvoke(m_delegateJobDoneNotification, new object[] { --m_thdCount });
                    if (--m_thdCount == 0)
                    {
                        BeginInvoke(m_delegateJobDoneNotification, new object[] { m_thdCount });
                        commObj.LogToFile("Job Done Notification - SMTP mail thread : " + m_thdCount);
                        OnUpdateStatusBar(new StatusEventArgs("Thread Count = " + m_thdCount.ToString()));
                    }
                } 
            }//end of finally           
        }//end of Thd_SendSmtpMail

        private void rdoInputFile_Click(object sender, EventArgs e)
        {
            Trace.WriteLine("UcMailSender.cs - rdoInputFile_Click");
            txtInputFile.Enabled = true;
            txtMailFolder.Enabled = false;

            btnBroMailFile.Enabled = true;
            btnBroFolder.Enabled = false;
            
            rdoMsgFolder.Checked = false;
            //if (File.Exists(txtInputFile.Text))
            //    return;

        }//end of rdoInputFile_Click

        private void rdoMsgFolder_Click(object sender, EventArgs e)
        {
            Trace.WriteLine("UcMailSender.cs - rdoMsgFolder_Click");
            txtInputFile.Enabled = false;
            txtMailFolder.Enabled = true;
            btnBroMailFile.Enabled = false;
            btnBroFolder.Enabled = true;
            
            rdoInputFile.Checked = false; // uncheck the other
        }//end of rdoMsgFolder_Click

        private void UcMailSender_Load(object sender, EventArgs e)
        {
            //SplashScreen.SetStatus("Loading Mail Sender");
            LoadComboBoxes(); // cannot do in constructor
        }//end of UcMailSender_Load

        private void btnAbort_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("UcMailSender.cs - btnAbort_Click");
            try
            {
                OnUpdateStatusBar( new StatusEventArgs( "Mail Sender Panel: Abort! Thread Count = " + m_thdCount.ToString() ) );

                for(int i = 0; i < m_initialThread; i++)
                {
                    if(m_thdList[i] != null && m_thdList[i].IsAlive)
                        KillMailSenderThread( ref m_thdList[i] );
                }//end of for

                // reset mouse cursor and enable control
                //IAsyncResult r = BeginInvoke( m_delegateJobDoneNotification, new object[] { -99 } );
                BeginInvoke( m_delegateJobDoneNotification, new object[] { -99 } );
            }//end of try
            catch(ThreadAbortException thdAbortEx)
            {
                commObj.LogToFile( "UcmailSender.cs - btnAbort_Click " + thdAbortEx.Message );
                MessageBox.Show( thdAbortEx.Message + "\n" + thdAbortEx.GetType().ToString() + thdAbortEx.StackTrace, "Abort" );
            }
            catch(Exception ex)
            {
                Debug.WriteLine( ex.Message + "\n" + ex.GetType().ToString() + ex.StackTrace );
                throw;
            }//end of catch
        }//end of btnAbort_Click

        private void KillMailSenderThread(ref Thread oneThd)
        {
            Debug.WriteLine("KillMailSenderThread");
            try
            {
                oneThd.Abort(); // abort
                oneThd.Join(); // require for ensure the thread kill

            }//end of try
            catch (ThreadAbortException thdEx)
            {
                Debug.WriteLine(thdEx.Message);
                commObj.LogToFile("Thread.log", "\t Exception ocurr in KillMailSenderThread:" + oneThd.Name);                
            }//end of catch - ThreadAbortException thdEx
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                MessageBox.Show(ex.Message + "\n" + ex.GetType().ToString() + ex.StackTrace);
            }//end of catch
        }//end of KillMailSenderThread

        public void JobDoneHandler(int thdId)
        {
            Debug.WriteLine("UcMailSender.cs - +++++++ JobDoneHandler ++++++++");

            m_procEndTime = DateTime.Now;
            txtEndTime.Text = m_procEndTime.ToString();
            txtNumMail.Text = m_sentMail.ToString();

            TimeSpan timeSpan = m_procEndTime - m_procStartTime;
            txtDuration.Text = timeSpan.TotalSeconds.ToString();
            
            double mailPerSec = m_sentMail / timeSpan.TotalSeconds;
            txtMailPerSec.Text = mailPerSec.ToString();

            if (rdoSMTP.Checked)
            {
                EnableNormalCaseCtrl(false);
                EnableSmtpCaseCtrl(true);
                EnableFileCaseCtrl(false);
            }
            else
                if (rdoNormal.Checked)
                {
                    EnableNormalCaseCtrl(true);
                    EnableSmtpCaseCtrl(false);
                    EnableFileCaseCtrl(false);
                }
                else
                    if (rdoFileCase.Checked)
                    {
                        EnableNormalCaseCtrl(false);
                        EnableSmtpCaseCtrl(false);
                        EnableFileCaseCtrl(true);
                    }

            EnableOtherCtrl(true);
            btnSend.Enabled = true;
            //btnAbort.Enabled = false;


            string msg = "Thread " + thdId.ToString() + " - Mail Send Duration: " + txtDuration.Text + "\r\n"
                + "Total Sent Files: " + txtNumMail.Text + "\r\n"
                + "Mails per second: " + txtMailPerSec.Text;

            commObj.LogToFile(msg);
        }//end of JobDoneHandler

        private void btnBroMailFile_Click(object sender, EventArgs e)
        {
            string inFile = "";
            if( File.Exists(txtInputFile.Text) )
                inFile = txtInputFile.Text;

            OpenFileDialog ofDlg = new OpenFileDialog();
            ofDlg.ShowReadOnly = true;
            //ofDlg.CheckFileExists = File.Exists(txtInputFile.Text);
            ofDlg.RestoreDirectory = true;
            if (ofDlg.ShowDialog() == DialogResult.OK)
            {
                txtInputFile.Text = ofDlg.FileName;
            }//end of if
            else
            {
                txtInputFile.Text = inFile;
            }
        }//end of btnBroMailFile_Click

        private void btnBroFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbDlg = new FolderBrowserDialog();
            fbDlg.RootFolder = Environment.SpecialFolder.MyComputer; // set the default root folder
            //if (txtMailFolder.Text != "")
            if( !String.IsNullOrEmpty(txtMailFolder.Text) )
            {
                if (Directory.Exists(txtMailFolder.Text))
                    fbDlg.SelectedPath = txtMailFolder.Text;  // set the default folder
            }

            if (fbDlg.ShowDialog() == DialogResult.OK)
            {
                txtMailFolder.Text = fbDlg.SelectedPath;
            }

        }// emd pf btnBroFolder_Click

        private void lnkFile_Click(object sender, EventArgs e)
        {
            string inFile = "";
            if (File.Exists(txtMailAddrFile.Text))
                inFile = txtMailAddrFile.Text;

            OpenFileDialog ofDlg = new OpenFileDialog();
            ofDlg.ShowReadOnly = true;
            //ofDlg.CheckFileExists = File.Exists(txtInputFile.Text);
            ofDlg.RestoreDirectory = true;
            if (ofDlg.ShowDialog() == DialogResult.OK)
            {
                txtMailAddrFile.Text = ofDlg.FileName;
            }//end of if
            else
            {
                txtMailAddrFile.Text = inFile;
            }
        }

        // For dynamic tool tip
        #region Mouse Event - For Dynamic tool tip (Override default setting)
        private void cboFileFrom_MouseEnter(object sender, EventArgs e)
        {
            ttpBatchMail.SetToolTip(cboFileFrom, "File contains FROM address.\r\nHit Enter to save the current value.\r\n" + cboFileFrom.Text);
        }

        private void cboTo_MouseEnter(object sender, EventArgs e)
        {
            ttpBatchMail.SetToolTip(cboTo, "Hit Enter to save the current value.\r\n" + cboTo.Text);
        }

        private void cboCC_MouseEnter(object sender, EventArgs e)
        {
            ttpBatchMail.SetToolTip(cboCC, "Hit Enter to save the current value.\r\n" + cboCC.Text);
        }

        private void cboBCC_MouseEnter(object sender, EventArgs e)
        {
            ttpBatchMail.SetToolTip(cboBCC, "Hit Enter to save the current value.\r\n" + cboBCC.Text);
        }

        private void cboHeader_MouseEnter(object sender, EventArgs e)
        {
            ttpBatchMail.SetToolTip(cboHeader, "ZANTAZ Header. \r\nHit Enter to save the current value.\r\n" + cboHeader.Text);
        }

        private void cboHeaderVal_MouseEnter(object sender, EventArgs e)
        {
            ttpBatchMail.SetToolTip(cboHeaderVal, "ZANTAZ Value. \r\nHit Enter to save the current value.\r\n" + cboHeaderVal.Text);
        }

        private void cboMailFrom_MouseEnter(object sender, EventArgs e)
        {
            ttpBatchMail.SetToolTip(cboMailFrom, "Hit Enter to save the current value.\r\n" + cboMailFrom.Text);
        }

        private void cboRcptTo_MouseEnter(object sender, EventArgs e)
        {
            ttpBatchMail.SetToolTip(cboRcptTo, "Hit Enter to save the current value.\r\n" + cboRcptTo.Text);
        }

        private void txtInputFile_MouseEnter(object sender, EventArgs e)
        {
            ttpBatchMail.SetToolTip( txtInputFile, "Create mail from spread sheet and send.\r\n" + txtInputFile.Text );
        }

        private void txtMailFolder_MouseEnter(object sender, EventArgs e)
        {
            ttpBatchMail.SetToolTip( txtMailFolder, "Folder contains EML files.\r\n" + txtMailFolder.Text );
        }

        private void txtStartTime_MouseEnter(object sender, EventArgs e)
        {
            ttpBatchMail.SetToolTip(txtStartTime, txtStartTime.Text);
        }

        private void txtEndTime_MouseEnter(object sender, EventArgs e)
        {
            ttpBatchMail.SetToolTip(txtEndTime, txtEndTime.Text);
        }

        private void txtDuration_MouseEnter(object sender, EventArgs e)
        {
            ttpBatchMail.SetToolTip(txtDuration, txtDuration.Text);
        }

        private void txtNumMail_MouseEnter(object sender, EventArgs e)
        {
            ttpBatchMail.SetToolTip(txtNumMail, txtNumMail.Text);
        }

        private void txtMailPerSec_MouseEnter(object sender, EventArgs e)
        {
            ttpBatchMail.SetToolTip(txtMailPerSec, txtMailPerSec.Text);
        }

        private void txtMailsSize_MouseEnter(object sender, EventArgs e)
        {
            ttpBatchMail.SetToolTip(txtMailsSize, txtMailsSize.Text);
        }

        private void txtAveSize_MouseEnter(object sender, EventArgs e)
        {
            ttpBatchMail.SetToolTip(txtAveSize, txtAveSize.Text);
        }

        private void txtSubject_MouseEnter(object sender, EventArgs e)
        {
            ttpBatchMail.SetToolTip(txtSubject, txtSubject.Text);
        }

        private void txtFolder_MouseEnter(object sender, EventArgs e)
        {
            ttpBatchMail.SetToolTip(txtFolder, "Folder contains attachments.\r\n" + txtFolder.Text);
        }
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                GetSubDirectoryNodes(txtMailFolder.Text, true);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("The process failed: ", ex.ToString());
            }
        }

        /// <summary>
        /// Get all the subdirectories from the parentNode, and add to directory tree.
        /// INPUT: string fullName - full pathname of parentNode directory
        /// INPUT: bool getFileName - indicate get the file name or not
        /// </summary>
        public void GetSubDirectoryNodes(string fullName, bool getFileName)
        {
            DirectoryInfo dir = new DirectoryInfo(fullName);
            DirectoryInfo[] dirSubs = dir.GetDirectories();

            // add a child node for each subdirectory
            foreach (DirectoryInfo dirSub in dirSubs)
            {
                // do not show hidden file
                if ((dirSub.Attributes & FileAttributes.Hidden) != 0)
                    continue;

                //Debug.WriteLine("Dir Name: " + dirSub.FullName);
                // Call GetDubDirectoryNodes recursively
                GetSubDirectoryNodes(dirSub.FullName, getFileName);
            }//end of foreach

            if (getFileName)
            {
                // get any files for this node
                FileInfo[] files = dir.GetFiles();

                // after placing the nodes, place the files in that subdirectory
                foreach (FileInfo file in files)
                {
                    Debug.WriteLine("File Full Name: " + dir.FullName + "\\" + file.Name);
                    
                    SMTPSender smtpSender = new SMTPSender();
                    smtpSender.mailFrom = cboMailFrom.Text;
                    smtpSender.mailRcptTo = cboRcptTo.Text;

                    smtpSender.smtpServer = cboSMTP.Text;
                    smtpSender.smtpPortNum = cboPort.Text;

                    smtpSender.SmtpSend(dir.FullName + "\\" + file.Name);
                    // TreeNode fileNode = new TreeNode(file.Name);
                    // parentNode.Nodes.Add(fileNode);
                }//end of foreach
            }//end of if - getFileName
        }

    }
}
