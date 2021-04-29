using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Xml;
//using OutlookApp = Microsoft.Office.Interop.Outlook;

namespace LiveCapture
{
    public partial class UcTester : UserControl
    {
        //public event UpdateStatusEventHandler statusChanged;
        public event EventHandler<StatusEventArgs> statusChanged;
        
        private string workingFolder = "";
        private string shortFileName = "";
        private DataSet csvDataSet;
        private Point[] lstFailCell;
        private CommObj commObj = new CommObj();
        private int numTC;

        private delegate void DelegateUpdate_contextMenu();
        private DelegateUpdate_contextMenu m_delegateShowContextMenu;

        public UcTester()
        {            
            InitializeComponent();
            m_delegateShowContextMenu = new DelegateUpdate_contextMenu( Update_contextMenu );
        }

        // "StatusEventArgs" - argument in EventArgs class
        protected virtual void OnUpdateStatusBar(StatusEventArgs eArgs)
        {
            statusChanged(this, eArgs);
        }//end of virtual

        private void UcTester_Load(object sender, EventArgs e)
        {
            LoadComboBoxes(); // cannot do in constructor
            cboDelimiter.SelectedIndex = 0;
        }//end of UcTester_Load

        private void Update_contextMenu()
        {
            testerContextMnuStrip.Items.Clear();
            testerContextMnuStrip.Items.Add( "Export Table", null, new System.EventHandler( this.MnuExportTable ) );
        }//end of Update_contextMenu

        private void tabCtrlTester_SelectedIndexChanged(object sender, EventArgs e)
        {
            Debug.WriteLine("tabCtrlTester_SelectedIndexChanged");
            OnUpdateStatusBar(new StatusEventArgs("Tester:" + tabCtrlTester.SelectedTab.Text));
        }//end of tabCtrlTester_SelectedIndexChanged

        /// <summary>
        /// Event handler for exporting whole table to CSV file
        /// This is DataGridView to CSV file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void MnuExportTable(System.Object sender, System.EventArgs e)
        {
            Debug.WriteLine( "UcTest.cs - MnuExportTable" );

            // Create the CSV file to which grid data will be exported.
            string exportFileName = commObj.GetSaveAbsPathFileName( Application.StartupPath.ToString() + "abc.csv" );
            //if(exportFileName == "")
            if( String.IsNullOrEmpty(exportFileName) )
                return;

            try
            {
                DataSet ds = (DataSet)dataGridView.DataSource;

                StreamWriter sw = new StreamWriter( exportFileName );
                // First we will write the headers.
                int colCount = ds.Tables[0].Columns.Count;
                for(int i = 0; i < colCount; i++)
                {
                    sw.Write( ds.Tables[0].Columns[i] );
                    if(i < colCount - 1)
                        sw.Write( "," );
                }//end of for
                sw.Write( sw.NewLine );

                // OK... Now write all the rows
                foreach(DataRow dataRow in ds.Tables[0].Rows)
                {
                    for(int i = 0; i < colCount; i++)
                    {
                        if(!Convert.IsDBNull( dataRow[i] ))
                        {
                            sw.Write( dataRow[i].ToString() );
                        }
                        if(i < colCount - 1)
                            sw.Write( "," );
                    }//end of for
                    sw.Write( sw.NewLine );
                }//end of foreach

                sw.Close();
            }//end of try
            catch(Exception ex)
            {
                Debug.WriteLine( ex.Message + "\n" + ex.GetType().ToString() + ex.StackTrace );
            }//end of catch
        }//end of MnuExportTable

        #region Initial combo box control Code
        /// <summary>
        /// Save the list of combo box items by calling other method: WriteComboBoxEntries()
        /// The Write Order is important...
        /// </summary>
        /// 
        string m_xmlFileName = "InitTester.xml";
        private void SaveComboBoxItem()
        {
            Debug.WriteLine("UcTester.cs - SaveComboBoxItem");
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
                WriteComboBoxEntries( cboInFile, "cboInFile", cboInFile.Text, tw ); //nodeList.Item(0)
                WriteComboBoxEntries( cboMDrive, "cboMDrive", cboMDrive.Text, tw ); //nodeList.Item(1)

                tw.WriteEndElement();
            }//end of try
            catch(IOException ioEx)
            {
                string msg = ioEx.Message + "\n" + ioEx.GetType().ToString() + ioEx.StackTrace;
                Debug.WriteLine( msg, "SaveComboBoxItem()" );
            }
            //catch(FileNotFoundException fnfEx)
            //{
            //    string msg = fnfEx.Message + "\n" + fnfEx.GetType().ToString() + fnfEx.StackTrace;
            //    Debug.WriteLine( msg, "SaveComboBoxItem()" );
            //}
            //catch(Exception ex)
            //{
            //    string msg = ex.Message + "\n" + ex.GetType().ToString() + ex.StackTrace;
            //    Debug.WriteLine( msg, "SaveComboBoxItem()" );
            //}//end of catch
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
            Debug.WriteLine("UcTester.cs - WriteComboBoxEntries");
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

        /// <summary>
        /// Load the text value into combo boxes. (OK... hardcode)
        /// </summary>
        private void LoadComboBoxes()
        {
            Debug.WriteLine( "UcTester.cs - LoadComboBoxes" );
            try
            {
                cboInFile.Items.Clear();
                cboMDrive.Items.Clear();

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
                        case "cboInFile":
                            for (x = 0; x < nodeList.Item(0).ChildNodes.Count; ++x)
                            {
                                cboInFile.Items.Add(nodeList.Item(0).ChildNodes.Item(x).InnerText);
                            }
                            break;
                        case "cboMDrive":
                            for (x = 0; x < nodeList.Item(1).ChildNodes.Count; ++x)
                            {
                                cboMDrive.Items.Add(nodeList.Item(1).ChildNodes.Item(x).InnerText);
                            }
                            break;
                    }//end of switch
                }//end of for

                if (0 < cboInFile.Items.Count)
                    cboInFile.Text = cboInFile.Items[0].ToString();
                if (0 < cboMDrive.Items.Count)
                    cboMDrive.Text = cboMDrive.Items[0].ToString();
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
        private void cboInFile_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyValue)
            {
                case 13: // enter key down
                    SaveComboBoxItem();
                    break;
            }//end of switch
        }

        private void cboMDrive_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyValue)
            {
                case 13: // enter key down
                    SaveComboBoxItem();
                    break;
            }//end of switch
        }

        private void cboInFile_KeyPress(object sender, KeyPressEventArgs e)
        {
            AutoCompleteCombo(sender, e);
        }

        private void cboMDrive_KeyPress(object sender, KeyPressEventArgs e)
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

        #region TC1 TAB LOGIC CODE
        private void btnTest_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("Test Button Clikc");

            try
            {
                if( !userInputValidation())
                    return;
                initVariable();
                importCSVData( shortFileName );

                NameResolutionTest nrt = new NameResolutionTest( csvDataSet, cboMDrive.Text );
                lstFailCell = nrt.ProcessTest(); // return a list of fail cell or null if everything OK.

                if(lstFailCell != null) // some TC fail
                {
                    for(int i = 0; i < lstFailCell.Length; i++)
                    {
                        DataGridViewCell cell = dataGridView[lstFailCell[i].X, lstFailCell[i].Y];
                        cell.Style.BackColor = Color.Yellow;

                        if(this.dataGridView.Rows[lstFailCell[i].Y].ErrorText != "Fail")
                        {
                            this.dataGridView.Rows[lstFailCell[i].Y].ErrorText = "Fail";
                        }
                        //this.dataGridView.Rows[lstFailCell[i].Y].HeaderCell.ErrorText = "ERROR";                                                
                    }
                    dataGridView.AutoResizeRowHeadersWidth( DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders );
                    // DISABLE - dataGridView.AutoResizeColumns();
                }//end of if - fail occurred.

                txtTotalEml.Text = nrt.totalEML.ToString();
                txtFail.Text = nrt.failTC.ToString();
                txtTotalTC.Text = numTC.ToString(); // count it in UcTester.cs
                txtPass.Text = nrt.passTC.ToString();

                if(nrt.totalEML != numTC)
                {
                    txtTotalEml.BackColor = Color.Yellow;
                    lblTotalEml.BackColor = Color.Yellow;
                }

                // enable the context menu
                BeginInvoke( m_delegateShowContextMenu, new object[] { } );
            }//end of try
            catch(Exception ex)
            {
                MessageBox.Show( ex.Message + "\n" + ex.GetType().ToString(), "Test" );
            }
            
        }//end of btnTest_Click       

        private void lnkInFile_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string inFile = "";
            if (File.Exists(cboInFile.Text))
                inFile = cboInFile.Text;

            OpenFileDialog ofDlg = new OpenFileDialog();
            ofDlg.ShowReadOnly = true;
            //ofDlg.CheckFileExists = File.Exists(txtInputFile.Text);
            ofDlg.RestoreDirectory = true;
            if (ofDlg.ShowDialog() == DialogResult.OK)
            {
                cboInFile.Text = ofDlg.FileName;
                SaveComboBoxItem();
            }//end of if
            else
            {
                cboInFile.Text = inFile;
            }
        }//end of lnkInFile_LinkClicked

        private void lnkMDrive_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FolderBrowserDialog fbDlg = new FolderBrowserDialog();
            fbDlg.RootFolder = Environment.SpecialFolder.MyComputer; // set the default root folder
            //if (cboMDrive.Text != "")
            if( !String.IsNullOrEmpty(cboMDrive.Text) )
            {
                if (Directory.Exists(cboMDrive.Text))
                    fbDlg.SelectedPath = cboMDrive.Text;  // set the default folder
            }

            if (fbDlg.ShowDialog() == DialogResult.OK)
            {
                cboMDrive.Text = fbDlg.SelectedPath;
                SaveComboBoxItem();
            }
        }//end of lnkMDrive_LinkClicked

        
        /// <summary>
        /// The .csv file format specifies, how the file is delimited, either by using 
        /// comma,tab or custom delimited
        /// </summary>
        /// <returns></returns>
        private string getDelimiterFormat()
        {
            string rz = "CSVDelimited"; //default value
            try
            {
                switch (cboDelimiter.SelectedIndex)
                {
                    case 0:
                        rz = "CSVDelimited";
                        break;
                    case 1:
                        rz = "TabDelimited";
                        break;
                    default:
                        rz = "Delimited(" + txtDelimiter.Text.Trim() + ")";
                        break;
                }//end of switch                
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            { 
            }
            return (rz);
        }//end of getDelimiterFormat

        private void cboDelimiter_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                if (cboDelimiter.SelectedIndex == 2)
                {
                    txtDelimiter.Enabled = true;
                    txtDelimiter.BackColor = Color.FromName("Info");
                }
                else
                {
                    txtDelimiter.Enabled = false;
                    txtDelimiter.BackColor = Color.FromName("InactiveBorder");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            { }
        }//end of cboDelimiter_SelectionChangeCommitted

        /// <summary>
        /// Import the CSV into Data View
        /// </summary>
        /// <param name="fileName">Full path file name of the import CSV</param>
        private void importCSVData( string fileName )
        {
            try
            {                    
                writeSchema();
                csvDataSet = connectCSV(fileName);                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            { }
        }//end of importCSVData

        /// <summary>
        /// User Input Validation. Assume everything OK.
        /// </summary>
        /// <returns>true - OK; false = error</returns>
        private bool userInputValidation()
        {
            bool rv = true; //assume everything ok
            try
            {
                if(!File.Exists( cboInFile.Text ))
                {
                    cboInFile.BackColor = Color.Yellow;
                    rv = false;
                }//end of if - In File Check
                else
                    if(!Directory.Exists( cboMDrive.Text ))
                    {
                        cboMDrive.BackColor = Color.Yellow;
                        rv = false;
                    }//end of Directory check
                else
                    if (cboDelimiter.SelectedIndex == 2)
                    {
                        //if( txtDelimiter.Text == "")
                        if( String.IsNullOrEmpty(txtDelimiter.Text) )
                        {
                            //txtDelimiter.Focused = true;
                            txtDelimiter.BackColor = Color.Yellow;
                            rv = false;                        
                        }                    
                    }//end of if Delimiter check

            }//end of try
            catch(IOException ioEx)
            {
                string msg = ioEx.Message + "\n" + ioEx.GetType().ToString() + ioEx.StackTrace;
                Debug.WriteLine( msg, "userInputValidation()" );
            }
            //catch(Exception ex)
            //{
            //    Debug.WriteLine( ex.Message + "\n" + ex.GetType().ToString() + ex.StackTrace );
            //    rv = false;
            //}//end of catch

            return (rv);
        }//end of userInputValidation

        /// <summary>
        /// Get the number of test case from csv file, ignor '#'.
        /// </summary>
        /// <param name="fn">Full Path File Name</param>
        /// <returns></returns>
        private int getNumTC(string fn)
        {
            int count = 0;
            //int tmp = 0;
            string strLine = "";
            StreamReader sr = new StreamReader( fn );
            while((strLine = sr.ReadLine()) != null)
            {
                if(strLine.Contains( "TC" ))
                    count++;

                // delete it later
                //if( (strLine[0] == 'T') && (strLine[1] == 'C') ) // skip all comment
                //{
                //    count++;
                //}
            }
            sr.Close();

            return (count);
        }//end of getNumTC

        private void initVariable()
        {
            int filenameLen = cboInFile.Text.Trim().Length;
            int lastIndex = cboInFile.Text.Trim().LastIndexOf("\\");
            workingFolder = cboInFile.Text.Trim().Substring(0, lastIndex); // save the working folder
            shortFileName = cboInFile.Text.Trim().Substring(lastIndex, filenameLen - lastIndex);
            shortFileName = shortFileName.Remove(0, 1).Trim(); // cannot move the point by inc lastIndex ie: trim black slash

            numTC = getNumTC( cboInFile.Text );
        }//end of initVariable

        /// <summary>
        /// Importing Data From CSV File. 
        /// You can get connected to driver either by using DSN or connection string
        /// NOTE: 
        /// filetable - refer to the input csv short file name and represent table name in data set.
        /// TC1 - refer to the name of the sheet in csv file. It is good for multiple sheet (tables) in data set.
        /// </summary>
        /// <param name="filename">Input short csv file name: Will parse for table name and schema location</param>
        /// <returns></returns>
        public DataSet connectCSV(string filetable)
        {
            DataSet ds = new DataSet();
            System.Data.Odbc.OdbcDataAdapter obj_oledb_da;
                        
            try
            {
                // Create a connection string as below, if you want to use DSN less connection. The DBQ attribute sets the path of directory which contains CSV files
                string strConnString = "Driver={Microsoft Text Driver (*.txt; *.csv)};Dbq=" + workingFolder.Trim() + ";Extensions=asc,csv,tab,txt;Persist Security Info=False";
                string sql_select;
                System.Data.Odbc.OdbcConnection conn;

                //Create connection to CSV file
                conn = new System.Data.Odbc.OdbcConnection(strConnString.Trim());                

                // SAVE - For creating a connection using DSN, use following line
                // conn	=	new System.Data.Odbc.OdbcConnection(DSN="MyDSN");

                conn.Open(); //Open the connection                 
                sql_select = "select * from [" + filetable + "]"; //Fetch records from CSV - filetable is the file name
                obj_oledb_da = new System.Data.Odbc.OdbcDataAdapter(sql_select, conn);
                obj_oledb_da.Fill(ds, "TC1"); //Fill dataset with the records from CSV file; TC1 is the name of the sheet

                // kentsave
                //int x = ds.Tables[0].Columns.Count;
                //string str = ds.Tables[0].Columns[3].ToString();
                //string str2 = ds.Tables[0].Columns[3].ColumnName;
                //string str3 = ds.Tables[0].Rows[9]["BCC"].ToString();


                //Set the datagrid properties
                dataGridView.DataSource = ds;
                dataGridView.DataMember = "TC1"; // name of the sheet    


                //for(int i = 0; i < lstFailCell.Length; i++)
                //{
                //    DataGridViewCell cell = dataGridView[lstFailCell[i].X, lstFailCell[i].Y];
                //    cell.Style.BackColor = Color.YellowGreen; ;
                //}
                

                //Close Connection to CSV file
                conn.Close();
            }
            catch (Exception ex2) //Error
            {
                MessageBox.Show(ex2.Message);
            }
            return ds;
        }//end of ConnectCSV

        
        /// <summary>
        /// Schema.ini File (Text File Driver)
        /// When the Text driver is used, the format of the text file is determined by using a
        /// schema information file. The schema information file, which is always named Schema.ini
        /// and always kept in the same directory as the text data source, provides the IISAM 
        /// with information about the general format of the file, the column name and data type
        /// information, and a number of other data characteristics
        /// </summary>
        private void writeSchema()
        {
            try
            {
                FileStream fsOutput = new FileStream(workingFolder + "\\schema.ini", FileMode.Create, FileAccess.Write);
                StreamWriter srOutput = new StreamWriter(fsOutput);
                string s1, s2, s3, s4, s5;
                s1 = "[" + shortFileName + "]";
                s2 = "ColNameHeader=true";
                s3 = "Format=" + getDelimiterFormat();
                s4 = "MaxScanRows=100";
                s5 = "CharacterSet=OEM";
                //srOutput.WriteLine(s1.ToString() + '\n' + s2.ToString() + '\n' + s3.ToString() + '\n' + s4.ToString() + '\n' + s5.ToString());
                srOutput.WriteLine( s1.ToString() + "\r\n" + s2.ToString() + "\r\n" + s3.ToString() + "\r\n" + s4.ToString() + "\r\n" + s5.ToString() );
                srOutput.Close();
                fsOutput.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            { }
        }//end of writeSchema

        private void btnProperty_Click(object sender, EventArgs e)
        {
            TC1Preference mp = new TC1Preference();
            //DialogResult result = mp.ShowDialog();
            mp.ShowDialog();
        }
        #endregion

        private void dataGridView_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            //MessageBox.Show( "Row post paint" );
        }


    }
}

/*********** save for reference
private OutlookApp.ApplicationClass oApp;
private OutlookApp._NameSpace oNameSpace;
private string msgFileName = "";
private string msgFullFileName = "";
private string msgPath = "C:\\tmp\\";
 * * ********/
/******************* save for reference
private void btnSaveEml_Click(object sender, EventArgs e)
{
    try
    {
        oApp = new OutlookApp.ApplicationClass();
        oNameSpace = oApp.GetNamespace("MAPI");
        oNameSpace.Logon(cboProfile.Text, txtPassword.Text, false, true);

        OutlookApp.MAPIFolder inBox = oNameSpace.GetDefaultFolder(OutlookApp.OlDefaultFolders.olFolderInbox);

        int n = 0;
        foreach (Object msgObject in inBox.Items)
        {
            if (msgObject is Outlook.MailItem) // check is the object type compatible
            {
                OutlookApp.MailItem mailItem = (OutlookApp.MailItem)msgObject; // yes. Cast it                
                msgFileName = "n" + n.ToString() + ".eml";
                msgFullFileName = msgPath + msgFileName;
                mailItem.SaveAs(msgFullFileName, OutlookApp.OlSaveAsType.olTXT);
                n++;
            }
        }//end of foreach
    }//end of try
    catch (Exception ex)
    {
        Debug.WriteLine("btnSaveEml_Click: \n" + "ex.Message" + "\n" + ex.GetType().ToString());  
    }//end of generic Exception
    finally
    {
        try
        {
            oNameSpace.Logoff(); // both sucessful or fail
            if (oApp != null)
            {
                Debug.WriteLine("\t Quit Outlook");
                oApp.Quit();
                Process[] localByName = Process.GetProcessesByName("outlook");
                for (int n = 0; n < localByName.Length; n++)
                    localByName[n].Kill();
            }
        }//end of try - logoff outlook
        catch (Exception ex2)
        {
            string msg = ex2.Message.ToString() + "\n"
                + ex2.GetType().ToString() + ex2.StackTrace.ToString();
            MessageBox.Show("Check log: " + ex2.Message.ToString(), "Outlook Logoff");
        }//end of catch - mainly for outlook logoff and quit
    }//end of finally
}
* *************/


