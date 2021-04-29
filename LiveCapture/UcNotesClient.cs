using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
using System.Xml;
using System.Windows.Forms;

namespace LiveCapture
{
    public partial class UcNotesClient : UserControl
    {
        private int _delay;
        private int _loop = 1;
        private int _sentCount; // number of mail sent out
        private int _failCount; // number of fail count
        private bool _ckGuid = false; // default don't include guid
        private bool _ckMultiAttach = false; // for UI mailing
        private bool _ckAttach = false; // for the file mailing
        private string _addrFile;
        private string _attachFolder; // for file
        private string _attachFile; //for UI
        private string _password; // Notes password
        private string _memo; // notes client profile
        private string _to;
        private string _cc;
        private string _bcc;
        private string _txtSubject; // default subject - variable reuse for UI update
        private string _richBox; // default richard text box - variable reuse for UI update
        private string _server; // server name or ip for sending mail without profile setup
        private string _senderNsf; // sender db for storing the sent mail
        private string _targetNsf; // target nsf for clean up
        private string m_xmlFileName = "InitNotesClient.xml";
        private Thread ncMailThread;
        private Thread m_thdCleanUp;
        

        private CommObj commObj = new CommObj();
        private AttachObj attachObj = null;

        private delegate void DelegateUpdate_txtSubject(string szSubject);
        private DelegateUpdate_txtSubject m_delegateShowSubject;

        private delegate void DelegateUpdate_richBox(string szMsg);
        private DelegateUpdate_richBox m_delegateDisplayMsg;

        private delegate void DelegateJobDoneNotification(int thdId); // all thread done - indicate Thread index
        private DelegateJobDoneNotification m_delegateJobDoneNotification;

        //public event UpdateStatusEventHandler statusChanged;
        public event EventHandler<StatusEventArgs> statusChanged;

        public UcNotesClient()
        {
            InitializeComponent();
            m_delegateJobDoneNotification = new DelegateJobDoneNotification( JobDoneHandler );
            m_delegateShowSubject = new DelegateUpdate_txtSubject(Update_txtSubject);
            m_delegateDisplayMsg = new DelegateUpdate_richBox(Update_richBox);
        }

        #region Initial combo box control Code
        /// <summary>
        /// Save the list of combo box items by calling other method: WriteComboBoxEntries()
        /// The Write Order is important...
        /// </summary>
        ///         
        private void SaveComboBoxItem()
        {
            Debug.WriteLine( "UcNotesClient.cs - SaveComboBoxItem" );
            XmlTextWriter tw = null;
            try
            {
                string cboPath = Application.StartupPath + "\\" + m_xmlFileName;
                if(!File.Exists( cboPath ))
                {
                    using(StreamWriter sw = File.CreateText( cboPath ))
                    {
                    }//end of using - for auto close etc... yes... empty
                }

                // Save the combox
                tw = new XmlTextWriter( Application.StartupPath + "\\" + m_xmlFileName, System.Text.Encoding.UTF8 );

                Debug.WriteLine( "\t ComboBox Item file" + Application.StartupPath + "\\" + m_xmlFileName );

                tw.WriteStartDocument();
                tw.WriteStartElement( "InitData" );

                //The order is important - match with InitMailSender.xml and switch case in load combo box
                WriteComboBoxEntries( cboMemo, "cboMemo", cboMemo.Text, tw ); //nodeList.Item(0)
                WriteComboBoxEntries( cboTo, "cboTo", cboTo.Text, tw ); //nodeList.Item(1)
                WriteComboBoxEntries( cboCC, "cboCC", cboCC.Text, tw ); //nodeList.Item(2)
                WriteComboBoxEntries( cboBCC, "cboBCC", cboBCC.Text, tw ); //nodeList.Item(3)
                WriteComboBoxEntries( cboServer, "cboServer", cboServer.Text, tw ); //nodeList.Item(4)
                WriteComboBoxEntries( cboSenderDB, "cboSenderDB", cboSenderDB.Text, tw ); //nodeList.Item(5)
                
                // Text Box but not combo box
                // WriteTextBoxEntries(txtInputFile, "txtInputFile", txtInputFile.Text, tw);
                // WriteComboBoxEntries(txtMailFolder, "txtFolder", txtMailFolder.Text, tw);

                tw.WriteEndElement();
            }//end of try
            catch(SystemException ex)
            {
                string msg = ex.Message + "\n" + ex.GetType().ToString() + ex.StackTrace;
                Debug.WriteLine( msg, "SaveComboBoxItem()" );
            }//end of catch
            finally
            {
                if(tw != null)
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
            Debug.WriteLine( "UcNotesClient.cs - WriteComboBoxEntries" );
            int maxEntriesToStore = 10;

            tw.WriteStartElement( "combobox" );
            tw.WriteStartAttribute( "name", string.Empty );
            tw.WriteString( cboBoxName );
            tw.WriteEndAttribute();

            // Write the item from the text box first.
            if(txtBoxText.Length != 0)
            {
                tw.WriteStartElement( "entry" );
                tw.WriteString( txtBoxText );
                tw.WriteEndElement();
                maxEntriesToStore -= 1;
            }//end of if

            // Write the rest of the entries (up to 10).
            for(int i = 0; i < cboBox.Items.Count && i < maxEntriesToStore; ++i)
            {
                if(txtBoxText != cboBox.Items[i].ToString())
                {
                    tw.WriteStartElement( "entry" );
                    tw.WriteString( cboBox.Items[i].ToString() );
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
        private void LoadComboBoxes()
        {
            Debug.WriteLine( "UcNotesClient.cs - LoadComboBoxes" );
            try
            {
                cboMemo.Items.Clear();
                cboTo.Items.Clear();
                cboCC.Items.Clear();
                cboBCC.Items.Clear();
                cboServer.Items.Clear();
                cboSenderDB.Items.Clear();
                XmlDocument xdoc = new XmlDocument();
                string cboPath = Application.StartupPath + "\\" + m_xmlFileName;
                if(!File.Exists( cboPath ))
                {
                    //File.CreateText(cboPath);
                    SaveComboBoxItem();
                    return;
                }//end of if - full path file doesn't exist

                xdoc.Load( cboPath );
                XmlElement root = xdoc.DocumentElement;
                XmlNodeList nodeList = root.ChildNodes;

                int numComboBox = nodeList.Count; // count text box also
                int x;
                for(int i = 0; i < numComboBox; i++) // Order is important here
                {
                    switch(nodeList.Item( i ).Attributes.GetNamedItem( "name" ).InnerText)
                    {
                        case "cboMemo":
                            for(x = 0; x < nodeList.Item( 0 ).ChildNodes.Count; ++x)
                            {
                                cboMemo.Items.Add( nodeList.Item( 0 ).ChildNodes.Item( x ).InnerText );
                            }
                            break;
                        case "cboTo":
                            for(x = 0; x < nodeList.Item( 1 ).ChildNodes.Count; ++x)
                            {
                                cboTo.Items.Add( nodeList.Item( 1 ).ChildNodes.Item( x ).InnerText );
                            }
                            break;
                        case "cboCC":
                            for(x = 0; x < nodeList.Item( 2 ).ChildNodes.Count; ++x)
                            {
                                cboCC.Items.Add( nodeList.Item( 2 ).ChildNodes.Item( x ).InnerText );
                            }
                            break;
                        case "cboBCC":
                            for(x = 0; x < nodeList.Item( 3 ).ChildNodes.Count; ++x)
                            {
                                cboBCC.Items.Add( nodeList.Item( 3 ).ChildNodes.Item( x ).InnerText );
                            }
                            break;
                        case "cboServer":
                            for(x = 0; x < nodeList.Item( 4 ).ChildNodes.Count; ++x)
                            {
                                cboServer.Items.Add( nodeList.Item( 4 ).ChildNodes.Item( x ).InnerText );
                            }
                            break;
                        case "cboSenderDB":
                            for(x = 0; x < nodeList.Item( 5 ).ChildNodes.Count; ++x)
                            {
                                cboSenderDB.Items.Add( nodeList.Item( 5 ).ChildNodes.Item( x ).InnerText );
                            }
                            break;

                    }//end of switch
                }//end of for

                if(0 < cboMemo.Items.Count)
                    cboMemo.Text = cboMemo.Items[0].ToString();
                if(0 < cboTo.Items.Count)
                    cboTo.Text = cboTo.Items[0].ToString();
                if(0 < cboCC.Items.Count)
                    cboCC.Text = cboCC.Items[0].ToString();
                if(0 < cboBCC.Items.Count)
                    cboBCC.Text = cboBCC.Items[0].ToString();
                if(0 < cboServer.Items.Count)
                    cboServer.Text = cboServer.Items[0].ToString();
                if(0 < cboSenderDB.Items.Count)
                    cboSenderDB.Text = cboSenderDB.Items[0].ToString();
            }//end of try
            catch(Exception ex)
            {
                string msg = ex.Message + "\n" + ex.GetType().ToString() + ex.StackTrace;
                Debug.WriteLine( msg, "Exception" );
                MessageBox.Show( msg, "LoadComboBoxes()" );
            }//end of catch
        }//end of LoadComboBoxes
        #endregion

        #region Handle Key Down and Press
        private void cboMemo_KeyDown(object sender, KeyEventArgs e)
        {
            switch(e.KeyValue)
            {
                case 13: // enter key down
                    SaveComboBoxItem();
                    break;
            }//end of switch
        }

        private void cboTo_KeyDown(object sender, KeyEventArgs e)
        {
            switch(e.KeyValue)
            {
                case 13: // enter key down
                    SaveComboBoxItem();
                    break;
            }//end of switch
        }

        private void cboCC_KeyDown(object sender, KeyEventArgs e)
        {
            switch(e.KeyValue)
            {
                case 13: // enter key down
                    SaveComboBoxItem();
                    break;
            }//end of switch
        }

        private void cboBCC_KeyDown(object sender, KeyEventArgs e)
        {
            switch(e.KeyValue)
            {
                case 13: // enter key down
                    SaveComboBoxItem();
                    break;
            }//end of switch
        }
        
        private void cboSenderDB_KeyDown(object sender, KeyEventArgs e)
        {
            switch(e.KeyValue)
            {
                case 13: // enter key down
                    SaveComboBoxItem();
                    break;
            }//end of switch
        }

        private void cboServer_KeyDown(object sender, KeyEventArgs e)
        {
            switch(e.KeyValue)
            {
                case 13: // enter key down
                    SaveComboBoxItem();
                    break;
            }//end of switch
        }

        private void cboTo_KeyPress(object sender, KeyPressEventArgs e)
        {
            AutoCompleteCombo( sender, e );
        }

        private void cboCC_KeyPress(object sender, KeyPressEventArgs e)
        {
            AutoCompleteCombo( sender, e );
        }

        private void cboBCC_KeyPress(object sender, KeyPressEventArgs e)
        {
            AutoCompleteCombo( sender, e );
        }
        private void cboMemo_KeyPress(object sender, KeyPressEventArgs e)
        {
             AutoCompleteCombo( sender, e );
        }

        private void cboServer_KeyPress(object sender, KeyPressEventArgs e)
        {
            AutoCompleteCombo( sender, e );
        }

        private void cboSenderDB_KeyPress(object sender, KeyPressEventArgs e)
        {
            AutoCompleteCombo( sender, e );
        }


        /// <summary>
        /// Handle the auto completion for user input in combo box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AutoCompleteCombo(object sender, KeyPressEventArgs e)
        {
            ComboBox cb = (ComboBox)sender;//this.ComboBox1
            if(Char.IsControl( e.KeyChar ))
                return;

            String ToFind = cb.Text.Substring( 0, cb.SelectionStart ) + e.KeyChar;
            int index = cb.FindStringExact( ToFind );
            if(index == -1)
                index = cb.FindString( ToFind );

            if(index == -1)
                return;

            cb.SelectedIndex = index;
            cb.SelectionStart = ToFind.Length;
            cb.SelectionLength = cb.Text.Length - cb.SelectionStart;
            e.Handled = true;

        }//end of AutoCompleteCombo
        #endregion


        private void Update_txtSubject(string szSubject)
        {
            Debug.WriteLine(szSubject);
            txtSubject.Text = szSubject;
            txtSubject.Refresh();
        }//end of Update_txtSubject

        private void Update_richBox(string szMsg)
        {
            richBox.Text = szMsg;
        }//end of Update_richBox
        // "StatusEventArgs" - argument in EventArgs class
        protected virtual void OnUpdateStatusBar(StatusEventArgs eArgs)
        {
            statusChanged(this, eArgs);
        }//end of OnUpdateStatusBar

        private void btnSend_Click(object sender, EventArgs e)
        {
            SetInitData();
            ncMailThread = new Thread(new ThreadStart(this.Thd_SendNotesClientMail));
            ncMailThread.Name = "NotesClientMailThread";
            ncMailThread.Start();

            commObj.LogToFile("Thread.log", "++ NotesClientMailThread Start ++");
        }//end of btnSend_Click

        /// <summary>
        /// Send mail by usint notes client in threading manner
        /// </summary>
        private void Thd_SendNotesClientMail()
        {
            Trace.WriteLine("NotesClient.cs - Thd_SendNotesClientMail()");

            if (rdoUI.Checked) // selected - info from UI
            {
                LnUiSendMail();
            }//end of if - select info from UI
            else
                if (rdoFile.Checked)
                {
                    LnFileSendMail();
                }//end of if - select info from file
        }// end of Thd_SendOutlookMail

        private void EnableServerNSenderDB(bool val)
        {
            lblServer.Enabled = val;
            lblSenderNsf.Enabled = val;
            cboServer.Enabled = val;
            cboSenderDB.Enabled = val;
        }
        private void EnableFileUI(bool val)
        {
            lnkFile.Enabled = val;
            txtFile.Enabled = val;
            chkAttach.Enabled = val;
            lnkFolder.Enabled = val;
            txtFolder.Enabled = val;
        }//end of EnableFileUI
        private void EnableAttachment(bool val)
        {
            lnkFolder.Enabled = val;
            txtFolder.Enabled = val;
        }
        private void EnableMailUI(bool val)
        {
            lnkTo.Enabled = val;
            lnkCC.Enabled = val;
            lnkBCC.Enabled = val;
            lnkAttach.Enabled = val;
            cboMemo.Enabled = val;
            cboTo.Enabled = val;
            cboCC.Enabled = val;
            cboBCC.Enabled = val;
            txtAttach.Enabled = val;
            txtPassword.Enabled = val;
            chkMultiAttach.Enabled = val;
        }//end of EnableMailUI

        private void EnableCleanUp(bool val)
        {
            btnCleanUp.Enabled = val;            
            cboInNsfFile.Enabled = val;
            lblPassword.Enabled = val;
            txtPwd.Enabled = val;
            lblClnUpServer.Enabled = val;
            cboClnUpServer.Enabled = val;

        }//end of EnableCleanUp

        private void rdoFile_Click(object sender, EventArgs e)
        {
            rdoFile.Checked = true;
            rdoUI.Checked = false;
            rdoCleanUp.Checked = false;

            EnableFileUI( true );                
            EnableMailUI( false );
            EnableCleanUp( false );
        }//end of rdoFile_Click

        private void rdoUI_Click(object sender, EventArgs e)
        {
            rdoFile.Checked = false;
            rdoUI.Checked = true;
            rdoCleanUp.Checked = false;

            EnableFileUI( false );
            EnableMailUI( true );
            EnableCleanUp( false );
        }//end of rdoUI_Click

        private void rdoCleanUp_Click(object sender, EventArgs e)
        {
            rdoFile.Checked = false;
            rdoUI.Checked = false;
            rdoCleanUp.Checked = true;

            EnableFileUI( false );
            EnableMailUI( false );
            EnableCleanUp( true );
        }//end of rdoCleanUp_Click

        /// <summary>
        /// Set the initial data from UI. 
        /// </summary>
        private void SetInitData()
        {
            if (rdoFile.Checked)
            {
                _addrFile = txtFile.Text;
                _attachFolder = txtFolder.Text;
                _ckAttach = chkAttach.Checked;
            }
            else
                if (rdoUI.Checked)
                {
                    //_password = txtPassword.Text;
                    _memo = cboMemo.Text;
                    _to = cboTo.Text;
                    _cc = cboCC.Text;
                    _bcc = cboBCC.Text;
                    _ckMultiAttach = chkMultiAttach.Checked;
                    _attachFile = txtAttach.Text;
                }

            _password = txtPassword.Text; // reuse the password from UI ^_^
            _txtSubject = txtSubject.Text;
            _richBox = richBox.Text = ""; // per Sailaja request, remove the info
            _delay = (int)nudDelay.Value;
            _loop = (int)nudLoop.Value;
            _ckGuid = chkGUID.Checked;
            _server = cboServer.Text;
            _senderNsf = cboSenderDB.Text;

            _sentCount = 0; //reset
            _failCount = 0;
        }//end of void SetInitData

        /// <summary>
        /// Send notes client mail based on user input from UI. Send one mail at a time.
        /// Only create one session and then loop through it.
        /// Initialize: this method can be used on a computer with a Notes client or Domino server 
        /// and bases the session on the current user ID. If a password is specified, it must match 
        /// the user ID password. If a password is not specified, the user is prompted for a password 
        /// as required and as the software permits. If the software does not support prompting 
        /// (for example, VBScript under ASP/IIS), you must supply the password or the user ID must not have one. 
        /// InitializeUsingNotesUserName: this method can be used only on a computer with a Domino server. 
        /// If a name is specified, the InitializeUsingNotesUserName method looks it up in the local Domino 
        /// Directory and permits access to the local server depending on the "Server Access" and 
        /// "COM Restrictions" settings. The password must match the Internet password associated with the name. 
        /// If no name is specified, access is granted if the server permits Anonymous access.
        /// </summary>
        private void LnUiSendMail()
        {
            Debug.WriteLine("UcMailClient.cs - LnUiSendMail");
            Domino.NotesSession domSession = null;
            Domino.NotesDatabase notesDB;            
            Domino.NotesDocument notesDoc;
            Domino.NotesDbDirectory domDBDir;
            // Rework on performance
            //Domino.NotesItem docForm;
            //Domino.NotesItem docSubject;
            //Domino.NotesItem docCopyTo;  // CC
            //Domino.NotesItem docBlindCC; // BCC
            //Domino.NotesItem docPrincipal; // Principal field
            Domino.NotesRichTextItem docRTFBody;

            try
            {
                domSession = new Domino.NotesSession();

                // only with computer with Domino Server installed
                // domSession.InitializeUsingNotesUserName("Oxygen Admin/acem", "password0");
                // domSession.InitializeUsingNotesUserName( "UserN n N0011/acem", "password0" );

                // used on a computer with a Notes client/Domino server 
                // and bases on the session on the current user ID - admin.id
                // domSession.Initialize("");
                domSession.Initialize(_password);
            }//end of try
            catch (Exception exSession)
            {               
                string msg = exSession.Message + "\n" + exSession.GetType().ToString() + "\n" + exSession.StackTrace;
                MessageBox.Show( "Fail in creating/initializing Notes Session.\n" + msg, "LnFileSendMail" );
                Debug.WriteLine( msg + "\r\nSession Fail in LnFileSendMail: releaseing session COM Object" );
                if( domSession != null )
                    System.Runtime.InteropServices.Marshal.ReleaseComObject( domSession );

            }//end of catch - notes session exception

            string strGUID = "";
            string strTmpSubj = "";
            try
            {
                Debug.WriteLine( "Session User Name: " + domSession.UserName );
                if(chkUseProfile.Checked) // use local profile
                {
                    domDBDir = domSession.GetDbDirectory( "" );
                    notesDB = domDBDir.OpenMailDatabase();
                    if(notesDB == null)
                    {
                        MessageBox.Show( "Open mail db fail" );
                        return;
                    }
                }
                else
                {
                    //Create sender database to save the sent mail.
                    //oNotesDatabase = oNotesSession.GetDatabase( "oxygen/team", "mail\\aadminis.nsf", false );
                    notesDB = domSession.GetDatabase( _server, "mail\\" + _senderNsf, false );
                    Debug.WriteLine( "\tServer: " + _server + " - " + _senderNsf );
                }
                //If the database is not already open then open it.
                if(!notesDB.IsOpen)
                {
                    Debug.WriteLine( "Open notes DB" );
                    notesDB.Open();
                }
                
                for (int j = 0; j < _loop; j++)
                {
                    if (chkGUID.Checked)
                    {
                        strGUID = System.Guid.NewGuid().ToString();
                        strTmpSubj = _txtSubject + (j+1) + " " + strGUID;
                        commObj.LogGuid("lnGUID.LOG", strGUID);
                    }//end of if - GUID
                    else
                        strTmpSubj = _txtSubject + (j+1); // always same

                    _richBox = "\nUser name: " + domSession.UserName
                             + "\nServer name: " + domSession.ServerName; // server name is null                

                    string strTo = _to;
                    string strCC = _cc;
                    string strBCC = _bcc;

                    string[] toArray = strTo.Split(new char[] { ',', ';' });
                    string[] ccArray = strCC.Split(new char[] { ',', ';' });
                    string[] bccArray = strBCC.Split(new char[] { ',', ';' });
                    string inSubject = strTmpSubj;
                    string bodyText = "";
                    string notesItems = _memo; 
                    
                    Object recipients = toArray;
                    Object carboncopy = ccArray;
                    Object blindcopy = bccArray;

                    notesDoc = notesDB.CreateDocument();
                    if(notesDoc == null)
                        MessageBox.Show( "notes Doc == null " );

                    // Rework on the performance
                    //docForm = notesDoc.ReplaceItemValue("Form", notesItems);
                    //docSubject = notesDoc.ReplaceItemValue("Subject", inSubject);
                    //docCopyTo = notesDoc.ReplaceItemValue("CopyTo", carboncopy);
                    //docBlindCC = notesDoc.ReplaceItemValue("BlindCopyTo", blindcopy);
                    //docPrincipal = notesDoc.ReplaceItemValue( "Principal", domSession.UserName );

                    // Rework on the performance
                    notesDoc.ReplaceItemValue( "Form", notesItems );
                    notesDoc.ReplaceItemValue( "Subject", inSubject );
                    notesDoc.ReplaceItemValue( "CopyTo", carboncopy );
                    notesDoc.ReplaceItemValue( "BlindCopyTo", blindcopy );
                    notesDoc.ReplaceItemValue( "Principal", domSession.UserName );

                    docRTFBody = notesDoc.CreateRichTextItem("Body");

                    bodyText = _richBox
                        + "\n To: " + _to
                        + "\n CC: " + _cc
                        + "\n BCC: " + _bcc
                        + "\n Notes_Subject: " + inSubject
                        + "\n " + DateTime.Now + "\n";

                    Trace.WriteLine("Before docRTFBody.AppendText " + docRTFBody.Values.ToString());
                    docRTFBody.AppendText(bodyText);
                    Trace.WriteLine("docRTFBody.AppendText( bodyText )");

                    string fn = _attachFile;
                    //if (fn != "")
                    if( !String.IsNullOrEmpty(fn) )
                    {
                        bodyText += fn;
                        char[] delim = new char[] { ';' };
                        foreach (string str in fn.Split(delim))
                        {
                            docRTFBody.EmbedObject(Domino.EMBED_TYPE.EMBED_ATTACHMENT, "", str, "");
                        }//end of foreach
                    }//end of if - adding attachment                    

                    // update UI info:
                    BeginInvoke( m_delegateShowSubject, new object[] { inSubject } );

                    notesDoc.Send(false, ref recipients); // send notes mail
                    _sentCount++;
                    
#if(DEBUG)
                    commObj.LogToFile("Notes Document sent");
#endif
                }//end of for
            }//end of try
            catch (Exception ex)
            {
                string msg = ex.Message + "\n" + ex.GetType().ToString();
                MessageBox.Show(msg, "Handle UI Info");
                _failCount++;
            }//end of catch - exception
            finally
            {
                Debug.WriteLine("NotesClient.cs - finally - releaseing session COM Object");
                if( domSession != null)
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(domSession);
            }//end of finally

            string info = "Total mail sent = " + _sentCount + "\r\n"
                       + "Total fail count = " + _failCount;

            //IAsyncResult ra = BeginInvoke(m_delegateDisplayMsg, new object[] { info });
            BeginInvoke( m_delegateDisplayMsg, new object[] { info } );
        }//end of LnUiSendMail

        /// <summary>
        /// Read from a file for all senders, receivers
        /// Text file format: To, CC, BCC, From, password.
        /// Each field separated by comma, mail addresses in each field separated by semi-colon;
        /// Two Loops:
        /// 1) Inner loop: sending mail based on the number of 'TO' users in the text file within a session.
        /// 2) Outer loop: repeat the whole mailing list in the file. Each loop will create a new session.
        /// Therefore, total number of sent mail == inner loop x outer loop.
        /// </summary>
        
        private void LnFileSendMail()
        {
            Trace.WriteLine("UcMailClient.cs - HandleFileSendMail()");

            // int counter = 0; // mail sent count
            string tmpSubj = _txtSubject; // save the user input subject
            string tmpBody = _richBox;	  // save the rich Box info

            string strTo = "";
            string strCC = "";
            string strBCC = "";

            string[] toArray;
            string[] ccArray;
            string[] bccArray;
            string inSubject = "";
            //string bodyText = "";
            string notesItems = "";

            Domino.NotesSession domSession = null;
            Domino.NotesDatabase notesDB;
            Domino.NotesDocument notesDoc;
            Domino.NotesDbDirectory domDBDir;
            //Rework on performance
            //Domino.NotesItem docForm;
            //Domino.NotesItem docSubject;
            //Domino.NotesItem docCopyTo;  // CC
            //Domino.NotesItem docBlindCC; // BCC
            //Domino.NotesItem docPrincipal;
            Domino.NotesRichTextItem docRTFBody;

            Object recipients;
            Object carboncopy;
            Object blindcopy;

            StreamReader sr = null;

            //StreamReader sr = new StreamReader( txtFile.Text ); // address book - put here for exception catch
            if (chkAttach.Checked) // point to attachment folder ONLY
                attachObj = new AttachObj(_attachFolder);

            for (int j = 0; j < _loop; j++)
            {
                string strGUID = "";
                string strLine = "";

                Debug.WriteLine("\t inside for loop: Open Domino Session");
                //domSession = new Domino.NotesSession();
                try
                {
                    domSession = new Domino.NotesSession();
                    // only with computer with Domino Server installed
                    // domSession.InitializeUsingNotesUserName("atest0", "password0");

                    // used on a computer with a Notes client/Domino server 
                    // and bases on the session on the current user ID - admin.id
                    // without initial password, a box will pop up for input password.
                    domSession.Initialize("");

                    // TO DO: Need to modify, should NOT reuse the UI password field.
                    // domSession.Initialize(_password);
                }//end of try
                catch (Exception exSession)
                {                    
                    MessageBox.Show( "Fail in creating Notes Session. User name: " + domSession.UserName, "LnFileSendMail" );

                    string msg = exSession.Message + "\n" + exSession.GetType().ToString() + "\n" + exSession.StackTrace;
                    Debug.WriteLine(msg + "\r\nSession Fail in LnFileSendMail: releaseing session COM Object");
                    MessageBox.Show( msg, "Notes Session Info" );
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(domSession);
                }//end of catch - notes session exception

                try
                {
                    if(chkUseProfile.Checked) // use local profile
                    {
                        domDBDir = domSession.GetDbDirectory( "" );
                        notesDB = domDBDir.OpenMailDatabase();
                        if(notesDB == null)
                        {
                            MessageBox.Show( "Open mail db fail" );
                            return;
                        }
                    }
                    else
                    {
                        //Create sender database to save the sent mail.
                        //oNotesDatabase = oNotesSession.GetDatabase( "oxygen/team", "mail\\aadminis.nsf", false );
                        notesDB = domSession.GetDatabase( _server, "mail\\" + _senderNsf, false );
                        Debug.WriteLine( "\tServer: " + _server + " - " + _senderNsf );
                    }
                    //If the database is not already open then open it.
                    if(!notesDB.IsOpen)
                    {
                        Debug.WriteLine( "Open notes DB" );
                        notesDB.Open();
                    }

                    sr = new StreamReader(_addrFile); // address book - put here for exception catch

                    Debug.WriteLine("\t Inside for loop - create stream reader");
                    Debug.WriteLine("\t StreamReader = " + sr.ToString());

                    while ((strLine = sr.ReadLine()) != null) // file name from txtFrom field
                    {                        
                        Debug.WriteLine("\t - LnFileSendMail - inside while loop : " + j.ToString());
                        if (strLine[0] != '#') // skip all comment - first character
                        {
                            _richBox = "\r\n- Read line : " + strLine;
                            if (chkGUID.Checked)
                            {
                                strGUID = System.Guid.NewGuid().ToString();
                                _txtSubject = tmpSubj + " " + strGUID;
                                // commObj.LogGuid( "GUID.LOG", strGUID );
                            }//end of if - GUID			

                            // string[0] - subject
                            // string[1] - To
                            // string[2] - CC
                            // string[3] - BCC
                            // string[n] - 
                            string[] splitStr = new string[5]; // match with the colum of input text file						
                            splitStr = strLine.Split(new Char[] { ',' }); // each field separated by comma

                            for (int k = 0; k < splitStr.Length; k++) // trim leading and ending space
                            {
                                Debug.WriteLine("k = " + k.ToString() + " splitStr = " + splitStr[k].ToString());
                                splitStr[k] = splitStr[k].Trim(' ');
                            }//end of for

                            Debug.WriteLine("after for loop - triming leading and ending space");
                            if ( 5 <= splitStr.Length ) // at least 5 cloumns, may be more...
                            {
                                Debug.WriteLine("split string == 5. Inside if");
                                strTo = splitStr[1];
                                strCC = splitStr[2];
                                strBCC = splitStr[3];

                                toArray = strTo.Split(new char[] { ';' });
                                ccArray = strCC.Split(new char[] { ';' });
                                bccArray = strBCC.Split(new char[] { ';' });
                                //inSubject = (_sentCount + 1) + " " + _txtSubject; // +1 for user request
                                inSubject = _txtSubject+(_sentCount+1); // +1 for user request
                                //bodyText = _richBox;
                                notesItems = "memo"; // hard code

                                recipients = toArray;
                                carboncopy = ccArray;
                                blindcopy = bccArray;

                                //Debug.WriteLine("Will open domino DB ");
                                //notesDB = domDBDir.OpenMailDatabase();
                                //Debug.WriteLine("After open domino DB " + notesDB.ToString());

                                notesDoc = notesDB.CreateDocument();

                                // rework on performance
                                //docForm = notesDoc.ReplaceItemValue("Form", notesItems);
                                //docSubject = notesDoc.ReplaceItemValue("Subject", inSubject);
                                //docCopyTo = notesDoc.ReplaceItemValue("CopyTo", carboncopy);
                                //docBlindCC = notesDoc.ReplaceItemValue("BlindCopyTo", blindcopy);
                                //docPrincipal = notesDoc.ReplaceItemValue( "Principal", domSession.UserName );

                                // rework on performance
                                notesDoc.ReplaceItemValue( "Form", notesItems );
                                notesDoc.ReplaceItemValue( "Subject", inSubject );
                                notesDoc.ReplaceItemValue( "CopyTo", carboncopy );
                                notesDoc.ReplaceItemValue( "BlindCopyTo", blindcopy );
                                notesDoc.ReplaceItemValue( "Principal", domSession.UserName );
                                
                                docRTFBody = notesDoc.CreateRichTextItem("Body");

                                _richBox = tmpBody
                                    + "\n To: " + strTo
                                    + "\n CC: " + strCC
                                    + "\n BCC: " + strBCC
                                    + "\n Notes_Subject: " + inSubject
                                    + "\n " + DateTime.Now + "\n";

                                //- fix bug... _to, _cc, _bcc is empty
                                    //+ "\n To: " + _to 
                                    //+ "\n CC: " + _cc
                                    //+ "\n BCC: " + _bcc
                                    //+ "\n Notes_Subject: " + _txtSubject
                                    //+ "\n " + DateTime.Now + "\n";
                                docRTFBody.AppendText(_richBox);

                                if (chkAttach.Checked)
                                {
                                    if (attachObj.IdxAttach == attachObj.NumFile)
                                        attachObj.IdxAttach = 0; //reset

                                    Debug.WriteLine(attachObj.IdxAttach, "\t - idxAttach");
                                    Debug.WriteLine(attachObj.AttachFullName, "\t - filename");
                                    docRTFBody.EmbedObject(Domino.EMBED_TYPE.EMBED_ATTACHMENT, "", attachObj.AttachFullName, "");

                                    _richBox += "\r\nAttach file index = " + attachObj.IdxAttach
                                             + "\r\nAttach file name = " + attachObj.AttachFullName;

                                    attachObj.IdxAttach++; // point to next file
                                }//end of if                                

                                // update UI info:
                                IAsyncResult rs = BeginInvoke( m_delegateShowSubject, new object[] { inSubject } );

                                Debug.WriteLine("Just before sending mail");
                                notesDoc.Send(false, ref recipients); // send notes mail
                                _sentCount++;
                                Debug.WriteLine("Just after sending mail");
                              
                                _richBox = ""; // reset
                                _txtSubject = tmpSubj;
                                if (chkGUID.Checked)
                                {	// log the guid after dumpToOutbox
                                    commObj.LogGuid("GUID.LOG", strGUID);
                                }//end of if - GUID
                            }//end of if - correct file 
                        }//end of if - skip comment                        
                        Thread.Sleep(_delay * 1000); // delay per message
                    }//end of while
                }//end of try
                catch (Exception ex)
                {
                    Trace.WriteLine(ex.Message.ToString(), "\t Exception");
                    commObj.LogGuid("lnGUID.LOG", "Exception occur " + strGUID.ToString() + "\r\n" + ex.Message.ToString());
                    _failCount++;
                }//end of catch - exception
                finally
                {
                    if (sr != null)
                    {
                        Trace.WriteLine("Finally - close the Stream Reader");
                        sr.Close();
                    }//end of if

                    string msg = "Total mail sent = " + _sentCount + "\r\n"
                               + "Total fail count = " + _failCount + "\r\n"
                               + "Please check the lnGUID.LOG for detail";

                    IAsyncResult rs = BeginInvoke(m_delegateDisplayMsg, new object[] { msg });
                }//end of finally - clean up everything
            }//end of for

            //            richBox.Text = "Total Mail send: " + counter.ToString();

            commObj.LogToFile("WcMailClient.cs +++ End of LnFileSendMail() +++");
        }//end of LnFileSendMail

        private void UcNotesClient_Load(object sender, EventArgs e)
        {
            LoadComboBoxes(); // cannot do in constructor
        }

        /// <summary>
        /// 1) For GUI send mail, browse and select particular file.
        /// 2) Can attach multiple file base on the "multi" check box. 
        /// 3) Use semicolon for seperate multi files.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lnkAttach_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string[] fileNames;

            OpenFileDialog ofDlg = new OpenFileDialog();
            if(chkMultiAttach.Checked)
            {
                ofDlg.Multiselect = true;
                if(ofDlg.ShowDialog() == DialogResult.OK)
                {
                    fileNames = ofDlg.FileNames;
                    foreach(string str in fileNames)
                    {
                        txtAttach.Text += ";" + str;
                    }//end of foreach

                    //check the first char
                    string tmpStr = txtAttach.Text.ToString();
                    if(tmpStr[0] == ';')
                        txtAttach.Text = txtAttach.Text.Remove( 0, 1 );
                }//end of if		
            }//end of if
            else
            {
                if(ofDlg.ShowDialog() == DialogResult.OK)
                {
                    txtAttach.Text = ofDlg.FileName;
                }//end of if		
            }//end of else        
        }//end of lnkAttach_LinkClicked

        /// <summary>
        /// Kill the send mail thread when program exit
        /// </summary>
        public void KillNotesClientMailThread()
        {
            Trace.WriteLine("NotesClient.cs - KillNotesClientMailThread()");
            try
            {
                commObj.LogToFile("Thread.log", "Kill Notes Client Mail Start");
                ncMailThread.Abort(); // abort
                ncMailThread.Join();  // require for ensure the thread kill
            }//end of try 
            catch (ThreadAbortException thdEx)
            {
                Trace.WriteLine(thdEx.Message);
                commObj.LogToFile("Aborting the Notes Client Mail thread : " + thdEx.Message.ToString());
            }//end of catch
        }//end of KillNotesClientMailThread

        private void btnAbort_Click(object sender, EventArgs e)
        {
            Trace.WriteLine("NotesClient.cs - btnAbort_Click");
            try
            {
                if (ncMailThread != null && ncMailThread.IsAlive)
                    KillNotesClientMailThread();
            }//end of try
            catch (Exception ex)
            {
                Debug.WriteLine("NotesClient.cs - btnAbort_Click " + ex.Message + "\n" + ex.StackTrace);
                commObj.LogToFile("NotesClient.cs - btnAbort_Click " + ex.Message + "\n" + ex.StackTrace);
                MessageBox.Show(ex.Message + "\n" + ex.StackTrace, "Abort Exception");
            }//end of catch
        }//end of btnAbort_Click

        private void lnkFile_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenFileDialog ofDlg = new OpenFileDialog();
            ofDlg.ShowReadOnly = true;
            if(ofDlg.ShowDialog() == DialogResult.OK)
            {
                //if(ofDlg.FileName != "")
                if( !String.IsNullOrEmpty(ofDlg.FileName) )
                {
                    txtFile.Text = ofDlg.FileName;
                }//end of if - open file name
            }// end of if - open file dialog
        }//end of lnkFile_LinkClicked

        private void lnkFolder_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FolderBrowserDialog fbDlg = new FolderBrowserDialog();

            fbDlg.RootFolder = Environment.SpecialFolder.MyComputer; // set the default root folder
            if(txtFolder.Text != null)
                fbDlg.SelectedPath = txtFolder.Text;  // set the default folder

            if(fbDlg.ShowDialog() == DialogResult.OK)
            {
                txtFolder.Text = fbDlg.SelectedPath;
            }
        }//end of lnkFolder_LinkClicked

        private void chkUseProfile_Click(object sender, EventArgs e)
        {
            if(chkUseProfile.Checked)
            {
                EnableServerNSenderDB( false ); // disable server and sender DB controls
            }
            else
            {
                EnableServerNSenderDB( true );
            }
        }//end of chkUseProfile_Click

        private void chkAttach_Click(object sender, EventArgs e)
        {
            if(chkAttach.Checked)
                EnableAttachment( true );
            else
                EnableAttachment( false );

        }//end of chkAttach_Click

        #region Clean up section

        private void InitCleanUpData()
        {
            _password = txtPwd.Text;
            _targetNsf = cboInNsfFile.Text;
            _server = cboClnUpServer.Text;
        }//end of InitCleanUpData()

        /// <summary>
        /// Check user input. Everything good -> return true
        /// </summary>
        /// <returns></returns>
        private bool ValidateUserInput()
        {
            return (true);
        }//end of ValidateUserInput
        private void btnCleanUp_Click(object sender, EventArgs e)
        {
            try
            {
                if(!ValidateUserInput())
                    return;

                btnCleanUp.Enabled = false; // only one thread for clean up

                InitCleanUpData();
                m_thdCleanUp = new Thread( new ThreadStart( this.Thd_CleanUpNSF ) );
                m_thdCleanUp.Name = "Thd_CleanUpNSF";
                m_thdCleanUp.Start();

                commObj.LogToFile( "++ Thd_CleanUpNSF Start ++" );
            }
            catch(Exception ex)
            {
                Debug.WriteLine( ex.Message );
                commObj.LogToFile( "Data Generation thread Error : " + ex.Message.ToString() );
            }
        }//end of btnCleanUp_Click

        private void Thd_CleanUpNSF()
        {           
            Debug.WriteLine("UcMailClient.cs - Thd_CleanUpNSF");
            Domino.NotesSession domSession = null;
            Domino.NotesDatabase notesDB;            

            try
            {
                domSession = new Domino.NotesSession();

                // only with computer with Domino Server installed
                // domSession.InitializeUsingNotesUserName("Oxygen Admin/acem", "password0");

                // used on a computer with a Notes client/Domino server 
                // and bases on the session on the current user ID - admin.id
                // domSession.Initialize("");
                domSession.Initialize(_password);
            }//end of try
            catch (Exception exSession)
            {               
                string msg = exSession.Message + "\n" + exSession.GetType().ToString() + "\n" + exSession.StackTrace;
                MessageBox.Show( "Fail in creating/initializing Notes Session.\n" + msg, "Thd_CleanUpNSF" );
                Debug.WriteLine( msg + "\r\nSession Fail in Thd_CleanUpNSF: releaseing session COM Object" );
                if( domSession != null )
                    System.Runtime.InteropServices.Marshal.ReleaseComObject( domSession );

            }//end of catch - notes session exception

            try
            {
                Debug.WriteLine( "Session User Name: " + domSession.UserName );
                notesDB = domSession.GetDatabase( _server, "mail\\" + _targetNsf, false );
                Debug.WriteLine( "\tServer: " + _server + " - " + _targetNsf );

                //If the database is not already open then open it.
                if(!notesDB.IsOpen)
                {
                    Debug.WriteLine( "Open notes DB" );
                    notesDB.Open();
                }
                Domino.NotesDocumentCollection ndc = notesDB.AllDocuments;
                IAsyncResult rs = BeginInvoke( m_delegateDisplayMsg, new object[] { ndc.Count.ToString() } );
                for(int i = 0; i < ndc.Count; i++)
                {
                    Domino.NotesDocument nd = ndc.GetNthDocument( i );
                    nd.RemovePermanently( true );
                    string msg = "Total Messages: " + ndc.Count.ToString() + "\n" + "Delete - " + i.ToString();
                    rs = BeginInvoke( m_delegateDisplayMsg, new object[] { msg } );
                    //ndc.GetNthDocument(i).Remove(true);
                }
                // ndc.RemoveAll( true );
                rs = BeginInvoke( m_delegateDisplayMsg, new object[] { "Delete Done!" } );
            }//end of try
            catch(Exception ex)
            {
                MessageBox.Show( ex.Message + "\n" + ex.GetType().ToString() );
            }//end of catch
            finally
            {
                lock(this)
                {
                    BeginInvoke( m_delegateJobDoneNotification, new object[] { m_thdCleanUp.ManagedThreadId } );
                }
            }
        }//end of Thd_CleanUpNSF

        /// <summary>
        /// Kill the send mail thread when program exit
        /// </summary>
        public void KillNotesCleanUpThread()
        {
            Trace.WriteLine( "NotesClient.cs - KillNotesCleanUpThread()" );
            try
            {
                commObj.LogToFile( "Thread.log", "Kill Notes Clean Up Thread Start" );
                m_thdCleanUp.Abort(); // abort
                m_thdCleanUp.Join();  // require for ensure the thread kill
            }//end of try 
            catch(ThreadAbortException thdEx)
            {
                Trace.WriteLine( thdEx.Message );
                commObj.LogToFile( "Aborting the Notes Client Mail thread : " + thdEx.Message.ToString() );
            }//end of catch
        }//end of KillNotesCleanUpThread

        private void btnClnUpAbort_Click(object sender, EventArgs e)
        {
            Trace.WriteLine( "NotesClient.cs - btnAbort_Click" );
            try
            {
                if(m_thdCleanUp != null && m_thdCleanUp.IsAlive)
                    KillNotesCleanUpThread();
            }//end of try
            catch(Exception ex)
            {
                Debug.WriteLine( "NotesClient.cs - btnAbort_Click " + ex.Message + "\n" + ex.StackTrace );
                commObj.LogToFile( "NotesClient.cs - btnAbort_Click " + ex.Message + "\n" + ex.StackTrace );
                MessageBox.Show( ex.Message + "\n" + ex.StackTrace, "Abort Exception" );
            }//end of catch
        }//end of btnClnUpAbort_Click

        public void JobDoneHandler(int thdId)
        {
            Debug.WriteLine( "NotesClient.cs - +++++++ JobDoneHandler ++++++++" );
            if( rdoCleanUp.Checked )
                btnCleanUp.Enabled = true;            
        }//end of JobDoneHandler
        #endregion

        

    }
}
