using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace LiveCapture
{
    public partial class WinZAData : Form
    {
        private FileInfo[] m_lstEmlFiles;
        private CommObj commObj = new CommObj();
        private string m_workingFolder = "";
        private string m_shortFileName = "";

        public WinZAData()
        {
            InitializeComponent();
        }

        private void lnkEmlFolder_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if(folderBrowserDlg.ShowDialog() == DialogResult.OK)
            {
                txtEmlFolder.Text = folderBrowserDlg.SelectedPath;
            }//end of if
        }//end of lnkEmlFolder_LinkClicked

        private void lnkInCSVFile_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if(openFileDlg.ShowDialog() == DialogResult.OK)
            {
                txtInFile.Text = openFileDlg.FileName;
                txtOutFile.Text = txtInFile.Text.Insert( (txtInFile.Text.Length - 4), "OUT" );
            }
        }//end of lnkInCSVFile_LinkClicked

        private void lnkOutCSVFile_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if(saveFileDlg.ShowDialog() == DialogResult.OK)
            {
                txtOutFile.Text = saveFileDlg.FileName;
            }
        }//end of lnkOutCSVFile_LinkClicked

        private void btnDoIt_Click(object sender, EventArgs e)
        {
            try
            {
                initVariable();

                DataSet ds = commObj.connectCSV( m_shortFileName, m_workingFolder );

                // Get Column header
                string[] headers = new string[ds.Tables[0].Columns.Count];
                for(int i = 0; i < ds.Tables[0].Columns.Count; i++)
                {
                    headers[i] = ds.Tables[0].Columns[i].ColumnName;
                }

                // Parse eml file value based on the header key from data set
                // Compare the eml file value to the data set
                // 1. number of eml file == to the TC in data set - OK
                // 2. eml files < TC in data set - Record undo test case
                // 3. TC in data set < eml files - Error warning to user
                foreach(FileInfo fileInfo in m_lstEmlFiles)
                {
                    ZNotesEmlParser zParser = new ZNotesEmlParser( fileInfo.FullName );
                    Dictionary<string, string> keyValSet = zParser.ParseEmlFile( headers );

                    // Subject, To, CC, BCC, Header 1, Header 2, ... Hearder N
                    //    0     1   2   3    4
                    string subject = fileInfo.Name.Substring( 0, fileInfo.Name.Length - 4 ); // trim eml

                    // Find the right TC - top down TC lookup [TO DO: hash search is better]
                    for(int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        string tmp = ds.Tables[0].Rows[i][0].ToString();
                        if(tmp == subject) // find it -> process comparision
                        {
                            // column look up
                            for(int j = 4; j < ds.Tables[0].Columns.Count; j++)
                            {
                                try
                                {
                                    // some reason " will not work. ie: trim the double when create ds.
                                    ds.Tables[0].Rows[i][j] = commObj.TrimString( keyValSet[headers[j]], "\t\r\n\"" );
                                }
                                catch(KeyNotFoundException knfex)
                                {
                                    MessageBox.Show( knfex.Message + ": " + headers[j] + "\n" + ds.Tables[0].Rows[i][0], "Key Not Found" );
                                    string msg = knfex.Message + "\n" + ds.Tables[0].Rows[i][0] + ": Key is " + headers[j];
                                    commObj.LogToFile( msg );
                                }//end of catch
                            }//end of for - column lookup
                        }// end of if - process comparision
                    }//end of for - TC lookup
                }//end of foreach

                saveCSVFile( ds, txtInFile.Text ); // no " in the csv file
            }//end of try
            catch(Exception ex)
            {
                string msg = ex.Message + "\n" + ex.GetType().ToString() + ex.StackTrace;
                MessageBox.Show( msg, "Do It Exception" );
            }//end of catch
            finally
            {
                btnDone.Enabled = true;
            }
        }//end of btnDoIt_Click

        private void initVariable()
        {
            int filenameLen = txtInFile.Text.Trim().Length;
            int lastIndex = txtInFile.Text.Trim().LastIndexOf( "\\" );
            m_workingFolder = txtInFile.Text.Trim().Substring( 0, lastIndex ); // save the working folder
            m_shortFileName = txtInFile.Text.Trim().Substring( lastIndex, filenameLen - lastIndex );
            m_shortFileName = m_shortFileName.Remove( 0, 1 ).Trim(); // cannot move the point by inc lastIndex ie: trim black slash

            // Get a list of eml files
            AttachObj attObj = new AttachObj( txtEmlFolder.Text );
            m_lstEmlFiles = attObj.GetFilesStartWith( "TC" );
        }//end of initVariable

        /// <summary>
        /// Save the DataSet info into a file. Assume the file is writable. Otherwise, throw exception.
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="fn"></param>
        private void saveCSVFile( DataSet ds, string fn )
        {
            try
            {
                StreamWriter sw = new StreamWriter( fn );
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
                MessageBox.Show( ex.Message, "Save CSV File" );
            }//end of catch
        }
        
    }
}