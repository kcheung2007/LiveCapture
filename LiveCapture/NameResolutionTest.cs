using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;


namespace LiveCapture
{
    public partial class NameResolutionTest
    {
        private string emlFolder = "";
        private DataSet ds;
        private FileInfo[] lstFiles;
        private Point[] lstFailCell;
        private CommObj commObj = new CommObj();

        #region Class properties section
        private int _totalProcess;
        private int _totalEML;
        private int _failTC;
        private int _passTC;
        
        public int totalProcess
        {
            get { return _totalProcess; }
            //set { _totalProcess = value; }
        }

        public int totalEML
        {
            get { return _totalEML; }
            //set { _totalEML = value; }
        }

        public int failTC
        {
            get { return _failTC; }
            //set { _failTC = value; }
        }

        public int passTC
        {
            get { return _passTC; }
            //set { _passTC = value; }
        }
        #endregion


        /// <summary>
        /// Constructor: Get the data set and folder name that store EMLs, and report the total number of emls
        /// </summary>
        /// <param name="csvDataSet"></param>
        /// <param name="folderName"></param>
        public NameResolutionTest( DataSet csvDataSet, string folderName)
        {
            Debug.WriteLine("NameResolutionTest() - constructor");
            try
            {
                ds = csvDataSet;
                emlFolder = folderName;

                // Get a list of eml files from exchange M drive
                AttachObj attObj = new AttachObj( emlFolder );
                lstFiles = attObj.GetFilesStartWith( "TC" );
                _totalEML = lstFiles.Length;
            }
            catch(Exception ex)
            {
                MessageBox.Show( ex.Message + "\n" + ex.GetType().ToString(), "NameResolution Constructor" );
            }
        }//end of constructor

        /// <summary>
        /// 1. Get the header value from the data set.
        /// 2. For each eml file, get the value according to the header name.
        /// 3. Get the subject value based on the eml file name.
        /// 4. Find the corresponding TC from top to bottom.
        /// 5. Find it, do the column lookup and compare the data set value with the file value.
        /// </summary>
        /// <returns>A list of fail cell. If OK, return null.</returns>
        public Point[] ProcessTest()
        {
            #region kentsave
            //int x = ds.Tables[0].Columns.Count;
            //string str = ds.Tables[0].Rows[0].ToString();
            //string str1 = ds.Tables[0].Columns[3].ToString();
            //string str2 = ds.Tables[0].Columns[3].ColumnName;
            //string str3 = ds.Tables[0].Rows[8]["BCC"].ToString();
            #endregion

            ArrayList xlist = new ArrayList();
            ArrayList ylist = new ArrayList();

            try
            {
                // Get Column header
                string[] headers = new string[ds.Tables[0].Columns.Count];
                for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                {
                    headers[i] = ds.Tables[0].Columns[i].ColumnName;
                }

                // Parse eml file value based on the header key from data set
                // Compare the eml file value to the data set
                // 1. number of eml file == to the TC in data set - OK
                // 2. eml files < TC in data set - Record undo test case
                // 3. TC in data set < eml files - Error warning to user
                foreach(FileInfo fileInfo in lstFiles)
                {
                    ZNotesEmlParser zParser = new ZNotesEmlParser( fileInfo.FullName );
                    Dictionary<string, string> keyValSet = zParser.ParseEmlFile( headers );

                    bool bFailTC = false; // default no fail (pass)
                    // Subject, To, CC, BCC, Header 1, Header 2, ... Hearder N
                    //    0     1   2   3    4
                    string subject = fileInfo.Name.Substring( 0, fileInfo.Name.Length - 4 ); // trim eml

                    // Find the right TC - top down TC lookup [TO DO: hash search is better]
                    for(int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        string tmp = ds.Tables[0].Rows[i][0].ToString();
                        if(tmp == subject) // find it -> process comparision
                        {
                            _totalProcess++;
                            // column look up
                            for(int j = 4; j < ds.Tables[0].Columns.Count; j++)
                            {
                                // trim " due to csv file problem
                                string v1 = commObj.TrimString( ds.Tables[0].Rows[i][j].ToString(), "\"\t\r\n " );
                                string v2 = commObj.TrimString( keyValSet[headers[j]], "\"\t\r\n " );

                                if(v1 != v2)
                                {
                                    xlist.Add( j );
                                    ylist.Add( i );
                                    Debug.WriteLine( "Test Case: " + ds.Tables[0].Rows[i][0].ToString() );
                                    Debug.WriteLine( "   Column: " + headers[j] );
                                    bFailTC = true;
                                }
                                else
                                {

                                }
                            }//end of for - column lookup
                            if(bFailTC)
                                _failTC++;
                            else
                                _passTC++;
                        }// end of if - process comparision
                    }//end of for - TC lookup
                }//end of foreach

                lstFailCell = new Point[xlist.Count];                
                for(int i = 0; i < xlist.Count; i++)
                {
                    lstFailCell[i].X = (int)xlist[i];
                    lstFailCell[i].Y = (int)ylist[i];
                }
            }//end of try
            catch(Exception ex)
            {
                MessageBox.Show( ex.Message + "\n" + ex.GetType().ToString(), "ProcessTest" );
            }         
           
            return (lstFailCell);
        }

        

    }
}
