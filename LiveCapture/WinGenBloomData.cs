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
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Windows.Forms;
using Microsoft.Xml.XMLGen;

namespace LiveCapture
{
    /// <summary>
    /// WinGenBloomData assumption:
    /// 1. Only three type of Bloomberg Documents: Blog.xml, IB.xml and Msg.xml
    /// </summary>
    public partial class WinGenBloomData : Form
    {
        XmlSchemaSet m_schemas = new XmlSchemaSet();
        XmlQualifiedName m_qname = XmlQualifiedName.Empty;
        int m_maxThreshold = 0;
        int m_listLength = 0;
        string m_inFullPathXsd; // input full path xsd file name
        string m_outFullPathXml; // output full path xml template file

        private Thread m_thdGenData;
        private int m_docType = 3; // default Msg.xml
        private int m_msgCount = 0; // default 1
        private int m_numMsg = 0; // default 1
        private string inXmlFullPathFN; // input xml full path file name template
        private string outFullPathFN; // output data full path file name
//        private string m_innerXml;
        private StringBuilder m_innerXml;

        // update the version here for more selection
        private string[] BlogVer = new string[] { "0.1", "0.2", "1.0" };
        private string[] IbVer   = new string[] { "1.0", "1.1", "1.2" };
        private string[] MsgVer  = new string[] { "1.1", "1.2", "1.4", "1.5" };

        private CommObj commObj = new CommObj();

        private delegate void DelegateJobDoneNotification(int count); // all thread done - indicate Thread index
        private DelegateJobDoneNotification m_delegateJobDoneNotification;

        private delegate void DelegateUpdate_txtDisplay(int numMail);
        private DelegateUpdate_txtDisplay m_delegateUpdateDisplay;

        public WinGenBloomData()
        {
            InitializeComponent();
            m_delegateJobDoneNotification = new DelegateJobDoneNotification( JobDoneHandler );
            m_delegateUpdateDisplay = new DelegateUpdate_txtDisplay( Update_txtDisplay );
        }

        public void JobDoneHandler(int count)
        {
            Debug.WriteLine( "WinGenBloomData.cs - +++++++ JobDoneHandler ++++++++" );

            EnableControls( true );

            string msg = "Thread " + m_thdGenData.Name + "Done\r\n"
                + "Total Message Generate: " + count;

            txtDisplay.Text = msg;
            commObj.LogToFile( msg );
        }//end of JobDoneHandler

        private void Update_txtDisplay(int mailCount)
        {
            txtDisplay.Text = "Message Count " + mailCount;
        }//end of Update_txtDisplay

        /// <summary>
        /// Predefined index: 0 - Blog.xml; 1 - IB.xml; 2 = Msg.xml
        /// </summary>
        /// <param name="idxDoc"></param>
        /// <returns></returns>
        private string getXmlDocNode(int idxDoc)
        {
            string rv = "";
            switch(idxDoc)
            {
                case 0 : // Blog.xml
                    rv = "//WebLog";
                    break;
                case 1: // Ib.xml
                    rv = "//Conversation";
                    break;
                case 2: //Msg.xml
                    rv = "//Message";
                    break;
            }//end of switch

            return (rv);
        }//end of getXmlDocNode

        private void BuildTree(XmlNode node, int i)
        {
            if(node != null)
                Format( node, i );

            if(node.HasChildNodes)
            {
                node = node.FirstChild;
                while(node != null)
                {
                    BuildTree( node, i );
                    if(node.NextSibling == null)
                    {                        
                        // m_innerXml += "</" + node.ParentNode.Name + ">";
                        string str = "</" + node.ParentNode.Name + ">";
                        Debug.WriteLine( str );
                        m_innerXml = m_innerXml.Append( str );
                    }
                    node = node.NextSibling;
                }//end of while
            }//end of if
        }//end of BuildTree

        // Format the output
        private void Format(XmlNode node, int i)
        {
            if(!node.HasChildNodes)
            {   // Creating value here
                Debug.WriteLine("+++++++++++++++++++++++++++++");
                Debug.WriteLine( node.ParentNode.Name + "_" + i.ToString() ); // data value
                Debug.WriteLine( node.ParentNode.NodeType.ToString() ); // data value
                Debug.WriteLine("+++++++++++++++++++++++++++++");
                //innerXml += node.ParentNode.Name + "_" + i.ToString();
                //m_innerXml += GenBloomCustomValue( node.ParentNode.Name, i );

                m_innerXml = m_innerXml.Append( GenBloomCustomValue( node.ParentNode.Name, i ) );
            }
            else
            {
                string nodeName = "<" + node.Name;
                //m_innerXml += nodeName;
                m_innerXml = m_innerXml.Append( nodeName );
                Debug.Write( nodeName );

                if(XmlNodeType.Element == node.NodeType)
                {
                    XmlNamedNodeMap map = node.Attributes;// attribute section
                    foreach(XmlNode attrnode in map)
                    {
                        Debug.Write( attrnode.Name + "=\"" + attrnode.Value + i.ToString() + "\"" );
                        string tmpStr = " " + attrnode.Name + "=\"" + attrnode.Value + i.ToString() + "\"";
                        //m_innerXml += tmpStr;
                        m_innerXml = m_innerXml.Append( tmpStr );
                    }
                    //m_innerXml += ">";
                    m_innerXml = m_innerXml.Append( ">" );
                }// end of if - element node
            }//end of else
        }//end of Format

        /// <summary>
        /// Make sure user input ok before generate the data.
        /// Only validate the input xml template and output xml data text box.
        /// </summary>
        /// <returns>true - OK; false - fail</returns>
        private bool ValidateUserInput()
        {
            bool rv = true; // assume everything is OK
            //if(txtXmlFile.Text == "")
            if( String.IsNullOrEmpty(txtXmlFile.Text) )
            {
                rv = false;
                txtXmlFile.BackColor = Color.YellowGreen;
            }
            else
                if(!File.Exists( txtXmlFile.Text ))
                {
                    rv = false;
                    txtXmlFile.BackColor = Color.YellowGreen;
                    txtXmlFile.Text = "FILE DOES NOT EXISTS";
                }
                else
                    //if(txtOutputXml.Text == "")
                    if( String.IsNullOrEmpty(txtOutputXml.Text) )
                    {
                        rv = false;
                        txtOutputXml.BackColor = Color.YellowGreen;
                    }

            return (rv);
        }//end of ValidateUserInput

        /// <summary>
        /// After validation - everything is good
        /// Initial the XML generation variables.
        /// </summary>
        private void InitUserData()
        {
            //m_innerXml = ""; // clean up
            m_innerXml = null;
            m_innerXml = new StringBuilder( 1000 ); // new obj
            m_msgCount = 0; // reset
            m_numMsg = (int)numMsg.Value;
            // everything good and save the full path with file name
            inXmlFullPathFN = txtXmlFile.Text;
            txtXmlFile.BackColor = TextBox.DefaultBackColor;

            outFullPathFN = txtOutputXml.Text;
            txtOutputXml.BackColor = TextBox.DefaultBackColor;
        }//end of InitUserData

        /// <summary>
        /// Generate special custom value for Bloomberg Document. 
        /// Check the template tag and custom the value.
        /// </summary>
        /// <param name="nodeName"></param>
        /// <param name="counter"></param>
        /// <returns></returns>
        private string GenBloomCustomValue(string nodeName, int counter)
        {
            string strValue = "";
            switch(nodeName)
            {                           
                case "AccountNumber":
                case "BloombergUUID":
                case "ConversationID":
                case "FirmNumber":
                case "LogID":
                case "MsgID":
                case "UUID":
                    strValue = counter.ToString( "0000000000" );
                    break;
                case "BloombergEmailAddress":
                    strValue = "Bloom." + counter.ToString() + "@bloombergQA.zantaz.com";
                    break;
                case "CorporateEmailAddress":
                case "EmailAddress":
                    strValue = "Corp_" + counter.ToString() + "@QA.zantaz.com";
                    break;
                case "CreateTime":
                case "DateTime":
                case "MsgTime":
                case "StartTime":
                    strValue = System.DateTime.Now.ToUniversalTime().ToString();
                    break;
                case "CreateTimeUTC":
                case "DateTimeUTC":
                case "MsgTimeUTC":
                case "StartTimeUTC":                
                    strValue = System.DateTime.Now.Ticks.ToString();
                    break;
                case "EntryID":
                    strValue = counter.ToString();
                    break;
                case "Language":
                case "MsgLang":
                    strValue = "English";
                    break;
                case "Recipient":
                case "Invitee":
                case "User":
                    // Do nothing ... by pass the value generation
                    break;
                default:
                    strValue = nodeName + "_" + counter.ToString();
                    break;
            }//end of switch

            return (strValue);
        }//end of GenBlogXml_0_1_Value

        /// <summary>
        /// Simply enable or disable control prevent user action
        /// </summary>
        /// <param name="flag"></param>
        private void EnableControls(bool flag)
        {
            btnGenerate.Enabled = flag;
            cboDocType.Enabled = flag;
            cboTypeVer.Enabled = flag;
            numMsg.Enabled = flag;
            txtXmlFile.Enabled = flag;
            txtOutputXml.Enabled = flag;

            btnCreate.Enabled = flag;
            btnDone.Enabled = flag;
            btnHelp.Enabled = flag;
            txtInXsd.Enabled = flag;
            txtOutputXml.Enabled = flag;

        }//end of EnableControls

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            try
            {                
                if (!ValidateUserInput())
                    return;
                InitUserData();
                EnableControls( false ); // disable control

                m_thdGenData = new Thread( new ThreadStart( this.Thd_GenBloomData ) );
                m_thdGenData.Name = "Thd_GenBloomData";
                m_thdGenData.Start();

                commObj.LogToFile( "Thread.log", "++ Thd_GenBloomData Start ++" );
            }
            catch(Exception ex)
            {
                Debug.WriteLine( ex.Message );
                commObj.LogToFile( "Data Generation thread Erro : " + ex.Message.ToString() );
            }
        }//end of btnGenerate_Click

        private void Thd_GenBloomData()
        {
            Debug.WriteLine( "WinGenBloomData.cs - Thd_GenBloomData()" );
            try
            {
                XmlTextReader reader = new XmlTextReader( txtXmlFile.Text );
                XmlDocument doc = new XmlDocument();
                doc.Load( reader );
                reader.Close();
                XmlNode currNode = null;

                //XmlNodeList nodelist = doc.SelectNodes( "//WebLog" );
                XmlNodeList nodelist = doc.SelectNodes( getXmlDocNode( m_docType ) );
                XmlDocumentFragment docFrag = doc.CreateDocumentFragment();

                for(int i = 0; i < m_numMsg; i++)
                {
                    foreach(XmlNode xmlNode in nodelist)
                    {
                        BuildTree( xmlNode, i );
                    }
                    docFrag.InnerXml = m_innerXml.ToString();
                    // docFrag.InnerXml = m_innerXml;
                    //m_msgCount++; // may not be here
                    lock(this)
                    {
                        //IAsyncResult rm = BeginInvoke( m_delegateUpdateDisplay, new object[] { m_msgCount++ } );
                        BeginInvoke( m_delegateUpdateDisplay, new object[] { m_msgCount++ } );
                    }
                    currNode = doc.DocumentElement;
                    currNode.InsertAfter( docFrag, currNode.LastChild );

                    // save the output to a file
                    // put this outside the loop will increase the performance
                    // But will lost the conversion if abort
                    //doc.Save( outFullPathFN );

                    Debug.WriteLine( m_innerXml );
                    //m_innerXml = "";
                    m_innerXml = null; // clean up
                    m_innerXml = new StringBuilder( 1000 );
                }//end of for

                // save the output to a file
                // put this outside the loop will increase the performance
                // But will lost the conversion if abort
                doc.Save( outFullPathFN );

                // clean up here?
                currNode = null;
                nodelist = null;
                doc = null;
                docFrag.RemoveAll();

            }//end of try
            catch(Exception ex)
            {
                MessageBox.Show( ex.Message + "\n" + ex.GetType().ToString() + ex.StackTrace, "Thd_GenBloomData" );
                //KillGenDataThread();
            }//end of catch
            finally
            {
                lock(this)
                {
                    BeginInvoke( m_delegateJobDoneNotification, new object[] { m_msgCount } );
                }

            }//end of finally
            
        }//end of Thd_GenBloomData

        private void KillGenDataThread()
        {
            Trace.WriteLine( "WinGenBloomData.cs - KillGenDataThread()" );
            try
            {
                commObj.LogToFile( "Thread.log", "Kill KillGenDataThread Start" );
                m_thdGenData.Abort(); // abort
                m_thdGenData.Join();  // require for ensure the thread kill
            }//end of try 
            catch(ThreadAbortException thdEx)
            {
                Trace.WriteLine( thdEx.Message );
                commObj.LogToFile( "Aborting the Count thread : " + thdEx.Message.ToString() );
            }//end of catch				

        }//end of KillGenDataThread

        private void btnAbort_Click(object sender, EventArgs e)
        {
            Trace.WriteLine( "WinGenBloomData.cs - btnAbort_Click" );
            try
            {
                if(m_thdGenData != null && m_thdGenData.IsAlive)
                    KillGenDataThread();
            }//end of try
            catch(Exception ex)
            {
                Debug.WriteLine( "WinGenBloomData.cs - btnAbort_Click " + ex.Message + "\n" + ex.StackTrace );
                commObj.LogToFile( "WinGenBloomData.cs - btnAbort_Click " + ex.Message + "\n" + ex.StackTrace );
                MessageBox.Show( ex.Message + "\n" + ex.StackTrace, "Abort Exception" );
            }//end of catch               
        }//end of btnAbort_Click

        /// <summary>
        /// Set MSGXML as the default msg type in the combo box. Then initial the version in the combo box.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WinGenBloomData_Load(object sender, EventArgs e)
        {
            cboDocType.SelectedIndex = 2; // MSGXML
            InitTypeVersionCombo( cboDocType.SelectedIndex );
            
        }//end of WinGenBloomData_Load

        private void lnkSampleXml_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {                
                OpenFileDialog ofDlg = new OpenFileDialog();
                ofDlg.ShowReadOnly = true;
                ofDlg.RestoreDirectory = true;
                if(ofDlg.ShowDialog() == DialogResult.OK)
                {
                    txtXmlFile.Text = ofDlg.FileName;
                }//end of if
            }
            catch(IOException ex)
            {
                Debug.WriteLine( ex.Message + "\n" + ex.GetType().ToString() + ex.StackTrace );
            }
        }//end of lnkSampleXml_LinkClicked

        private void lnkOutputXml_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                string fn = cboDocType.Text + "_" + cboTypeVer.Text + ".xml";
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "xml files (*.xml)|*.xml|All files (*.*)|*.*";
                saveFileDialog.FilterIndex = 1;
                saveFileDialog.RestoreDirectory = true;
                saveFileDialog.FileName = fn;

                if(saveFileDialog.ShowDialog() == DialogResult.OK)
                    txtOutputXml.Text = saveFileDialog.FileName;
                else
                    txtOutputXml.Text = ""; // cancel by user
            }
            catch(IOException ex)
            {
                Debug.WriteLine( ex.Message + "\n" + ex.GetType().ToString() + ex.StackTrace );
            }
        }//end of lnkOutputXml_LinkClicked

        /// <summary>
        /// Init the version number of doc type.
        /// </summary>
        /// <param name="index">index of the Doc Type combobox</param>
        private void InitTypeVersionCombo(int index)
        {
            cboTypeVer.Items.Clear(); // clear the combo box
            switch(index)
            {
                case 0: //Blog
                    foreach(string s in BlogVer)
                        cboTypeVer.Items.Add( s );
                    break;
                case 1: // IB
                    foreach(string s in IbVer)
                        cboTypeVer.Items.Add( s );
                    break;
                case 2: // MsgXml
                    foreach(string s in MsgVer)
                        cboTypeVer.Items.Add( s );
                    break;
            }//end of switch

            cboTypeVer.SelectedIndex = 0; // default
        }//end of InitTypeVersionCombo

        private void cboDocType_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_docType = cboDocType.SelectedIndex;
            InitTypeVersionCombo( m_docType );
            txtXmlFile.Text = "";
            txtOutputXml.Text = "";
        }

        private void cboTypeVer_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtXmlFile.Text = "";
            txtOutputXml.Text = "";
        }

        /// <summary>
        /// Validate the user input for creating xml template from xsd file
        /// Check the input xsd text box and output template text box
        /// </summary>
        /// <returns>true - OK; false - Fail</returns>
        private bool ValidateXsdUserInput()        
        {
            bool rv = true; //assume everything ok
            txtDisplay.Text = ""; // reset

            //if(txtInXsd.Text == "")
            if( String.IsNullOrEmpty(txtInXsd.Text) )
            {
                rv = false;
                txtInXsd.BackColor = Color.YellowGreen;
            }
            else
                if(!File.Exists( txtInXsd.Text ))
                {
                    rv = false;
                    txtInXsd.BackColor = Color.YellowGreen;
                    txtDisplay.Text = "FILE DOES NOT EXISTS";
                }
                else
                    if(Path.GetExtension( txtInXsd.Text ) != ".xsd")
                    {
                        rv = false;
                        txtInXsd.BackColor = Color.YellowGreen;
                        txtDisplay.Text = "File does not appear to be xsd file";
                    }
                    else
                        //if(txtOutXmlTemplate.Text == "")
                        if( String.IsNullOrEmpty(txtOutXmlTemplate.Text) )
                        {
                            rv = false;
                            txtOutXmlTemplate.BackColor = Color.YellowGreen;
                            txtDisplay.Text = "Please specify output xml template file name";
                        }
            return (rv);
        }//end of ValidateXsdUserInput

        /// <summary>
        /// Create a xml template from a xsd file. 
        /// Then the template xml will be used for data generation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                m_schemas = new XmlSchemaSet(); // reset
                m_qname = XmlQualifiedName.Empty;

                if(!ValidateXsdUserInput())
                    return;

                // Initial user input
                m_inFullPathXsd = txtInXsd.Text;
                m_outFullPathXml = txtOutXmlTemplate.Text;

                m_schemas.Add( null, m_inFullPathXsd );
                XmlTextWriter textWriter = new XmlTextWriter( m_outFullPathXml, null );
                textWriter.Formatting = Formatting.Indented;
                XmlSampleGenerator genr = new XmlSampleGenerator( m_schemas, m_qname );
                if(0 < m_maxThreshold)
                    genr.MaxThreshold = m_maxThreshold;
                if(0 < m_listLength)
                    genr.ListLength = m_listLength;

                genr.WriteXml( textWriter );

                txtDisplay.Text = "XML Template generation done!!\n\r" + m_outFullPathXml;
            }//end of try
            catch(Exception ex)
            {
                Debug.WriteLine( ex.Message + "\n" + ex.GetType().ToString() + ex.StackTrace );
            }
        }//end of btnCreate_Click

        private void lnkInXsd_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                OpenFileDialog ofDlg = new OpenFileDialog();
                ofDlg.ShowReadOnly = true;
                ofDlg.RestoreDirectory = true;
                if(ofDlg.ShowDialog() == DialogResult.OK)
                {
                    txtInXsd.Text = ofDlg.FileName;
                }//end of if
            }
            catch(IOException ex)
            {
                Debug.WriteLine( ex.Message + "\n" + ex.GetType().ToString() + ex.StackTrace );
            }
        }//end of lnkInXsd_LinkClicked

        private void lnkOutXml_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                string fn = cboDocType.Text + "_" + cboTypeVer.Text + ".xml";
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "xml files (*.xml)|*.xml|All files (*.*)|*.*";
                saveFileDialog.FilterIndex = 1;
                saveFileDialog.RestoreDirectory = true;
                saveFileDialog.FileName = fn;

                if(saveFileDialog.ShowDialog() == DialogResult.OK)
                    txtOutXmlTemplate.Text = saveFileDialog.FileName;
                else
                    txtOutputXml.Text = ""; // cancel by user
            }
            catch(IOException ex)
            {
                Debug.WriteLine( ex.Message + "\n" + ex.GetType().ToString() + ex.StackTrace );
            }
        }//end of lnkOutXml_LinkClicked

        private void btnHelp_Click(object sender, EventArgs e)
        {
            MessageBox.Show( "Under construction", "Information" );
        }
    }
}