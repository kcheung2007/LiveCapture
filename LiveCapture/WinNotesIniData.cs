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
    public partial class WinNotesIniData : Form
    {
        private CommObj commObj = new CommObj();
        private string m_iniOutFolder = "";
        private string m_iniFileName = "";
        private string m_shortFileName = ""; // input template file - notes.ini
        private string m_inTruthTable = "";
        private string m_workingFolder = "";

        public WinNotesIniData()
        {
            InitializeComponent();
        }

        private void initVariable()
        {
            m_iniFileName = txtInTemplate.Text;
            int filenameLen = txtInTruthTable.Text.Trim().Length;
            int lastIndex = txtInTruthTable.Text.Trim().LastIndexOf( "\\" );
            m_workingFolder = txtInTemplate.Text.Trim().Substring( 0, lastIndex ); // save the working folder
            m_shortFileName = txtInTruthTable.Text.Trim().Substring( lastIndex, filenameLen - lastIndex );
            m_shortFileName = m_shortFileName.Remove( 0, 1 ).Trim(); // cannot move the point by inc lastIndex ie: trim black slash

            m_iniOutFolder = txtOutFolder.Text;
            m_inTruthTable = txtInTruthTable.Text;
        }//end of initVariable

        private void btnDoIt_Click(object sender, EventArgs e)
        {
            try
            {
                Debug.WriteLine( "WinNotesIniData.cs - btnDoIt_Click" );
                initVariable();

                //read entire file into string object
                string iniFileStr = File.ReadAllText( m_iniFileName );
                //StringBuilder strBuilder = new StringBuilder( iniFileStr );

                DataSet ds = commObj.connectCSV( m_shortFileName, m_workingFolder );
                // Get Column header
                string[] headers = new string[ds.Tables[0].Columns.Count];
                for(int i = 0; i < ds.Tables[0].Columns.Count; i++)
                {
                    headers[i] = ds.Tables[0].Columns[i].ColumnName;
                }

                for(int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    string fn = m_iniOutFolder + "\\" + ds.Tables[0].Rows[i][0] + "_notes.ini";
                    Debug.WriteLine( "\t" + fn );
                    StreamWriter sw = new StreamWriter( fn ); // open file for writing
                    sw.WriteLine( iniFileStr ); // write the original notes.ini
                    for(int j = 1; j < headers.Length; j++)
                    {
                        Debug.WriteLine( "\t\t" + headers[j] + " = " + ds.Tables[0].Rows[i][j] );
                        sw.WriteLine( headers[j] + " = " + ds.Tables[0].Rows[i][j] );                        
                    }//end of for
                    sw.Close();
                }//end of for
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

        private void lnkInTemplate_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDlg.Filter = "ini file|*.ini|All files|*.*";
            if(openFileDlg.ShowDialog() == DialogResult.OK)
            {
                txtInTemplate.Text = openFileDlg.FileName;
            }
        }//end of lnkInTemplate_LinkClicked

        private void lnkOutCSVFile_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDlg.Filter = "csv file|*.csv|All files|*.*";
            if(openFileDlg.ShowDialog() == DialogResult.OK)
            {
                txtInTruthTable.Text = openFileDlg.FileName;
            }
        }//end of lnkOutCSVFile_LinkClicked

        private void lnkOutFolder_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if(folderBrowserDlg.ShowDialog() == DialogResult.OK)
            {
                txtOutFolder.Text = folderBrowserDlg.SelectedPath;
            }//end of if
        }
    }
}