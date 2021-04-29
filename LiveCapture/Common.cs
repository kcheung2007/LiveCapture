using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.Odbc;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Net.Mail;
using System.Net.Sockets;
using System.Text;
using System.Web;
using System.Windows.Forms;

namespace LiveCapture
{
	/// <summary>
	/// Summary description for Common.
	/// No and CANNOT have local variable. If object exist only for implementation purpose.
	/// NO data should store here. ONLY method for accessing common function.
	/// </summary>
	public class CommObj
	{
        private string _logPath = Application.StartupPath + "\\Logs";

		public CommObj()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		/// <summary>
		/// Read addresses from a text file
		/// 1) Get file name from open file dialog
		/// 2) Read the file line by line
		/// 3) Append ; & space to form one line
		/// 4) Put the result back to input parameter
		/// </summary>
		public void LoadAddrFromFile(ref string inStr)
		{
			// Show the Open File Dialog.
			// If the user clicked OK in the dialog and a text file was selected, open it.
			// Display an OpenFileDialog and show the read-only files...
			OpenFileDialog ofDlg = new OpenFileDialog();
			ofDlg.ShowReadOnly = true;

			if( ofDlg.ShowDialog() == DialogResult.OK )
			{
				//if( ofDlg.FileName != "" )
                if( !String.IsNullOrEmpty( ofDlg.FileName ) )
				{
					inStr = ""; // clear the default text
					StreamReader sr = new StreamReader( ofDlg.FileName );
					String str;
					while( (str = sr.ReadLine()) != null )
					{
						inStr = inStr + str + "; ";
					}//end of while
					sr.Close();
				}//end of if - open file name
			}// end of if - open file dialog				
		}//end of LoadAddrFromFile

		/// <summary>
		/// Open a file and add a line of text into a text file.
		/// If file doesn't exist, create it. Will log the time stamp
		/// </summary>
		/// <param name="fileName">full file name - No path</param>
		/// <param name="strText">text to add into file</param>
//		public void LogToFile( string fileName, string strText, bool bdebug )
        public void LogToFile( string fileName, string strText )
		{
//            if( !bdebug )
//                return;
			/*** save for reference
			 * string logFile = DateTime.Now.ToShortDateString().Replace(@"/",@"-").Replace(@"\",@"-" + ".log";
			 * save for reference */

            try
            {
                string logFileName = _logPath + "\\" + fileName;
                if (!File.Exists(logFileName))
                {
                    // Try to create the directory. No checking.. just create it
                    //DirectoryInfo di = Directory.CreateDirectory(_logPath);
                    Directory.CreateDirectory( _logPath );
                }//end of if - full path file doesn't exist

                lock (this)
                {
                    // if full path file name exist, append text
                    using (StreamWriter sw = File.AppendText(logFileName))
                    {
                        Log(strText, sw);
                        sw.Close(); // Close the writer and underlying file.
                    }
                }//end of lock
            }//end of try
            catch (IOException ex)
            {
                string msg = ex.Message + "\n" + ex.GetType().ToString() + ex.StackTrace;
                MessageBox.Show(msg, "Generic Exception");
            }//end of catch                      		    
	    }// end of LogToFile

		/// <summary>
		/// Overload function. Will log the time stamp
		/// </summary>
		/// <param name="strText">text to add into file</param>
//		public void LogToFile( string strText, bool bdebug )
        public void LogToFile( string strText )
		{
//            if( !bdebug )
//                return;
            // short name only
            LogToFile("LiveCapt.log", strText);
		}// end of LogToFile
		
		protected void Log( String logMessage, TextWriter w )
		{
			w.Write("Log: "); // if want blank line, add /r/n -> w.Write("/r/nLog Entry : ");
			w.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(), DateTime.Now.ToShortDateString());
			w.WriteLine("{0}", logMessage);
			w.WriteLine ("-------------------------------");
			// Update the underlying file.
			w.Flush(); 
		}// end of Log

		/// <summary>
		/// Special function for logging GUID ONLY!!!
		/// </summary>
		/// <param name="fileName"></param>
		/// <param name="strGUID"></param>
		public void LogGuid( string fileName, string strGUID )
		{
            string logFileName = _logPath + "\\" + fileName;
            lock (this)
            {
                StreamWriter sw = File.AppendText(logFileName);
                sw.WriteLine("{0}", strGUID);
                sw.Close();
            }
		}//end of LogGuid

		/// <summary>
		/// Write to a file line by line. No time stamp
		/// </summary>
		/// <param name="fn">file name</param>
		/// <param name="line">text line</param>
		public void WriteLineByLine(string fn, string line)
		{
            string logFileName = _logPath + "\\" + fn;
            if (!File.Exists(logFileName))
            {
                // Try to create the directory. No checking.. just create it
                DirectoryInfo di = Directory.CreateDirectory(_logPath);
            }//end of if - full path file doesn't exist

            lock (this)
            {
                StreamWriter sw = File.AppendText(fn);
                sw.WriteLine("{0}", line);
                sw.Close();
            }
		}// end of WriteLineByLine

		/// <summary>
		/// Test the smtp server connection
		/// </summary>
		/// <param name="smtpHost">SMTP server name or IP address</param>
		/// <param name="smtpPort">SMTP server listening port number - default 25</param>
		/// <returns>true - connection OK</returns>
		/// <returns>false - connection FAIL</returns>
		public bool TestSMTPConnection(string smtpHost, string smtpPort)
		{
			bool rv = false; // no connection
			System.Net.Sockets.TcpClient tcpClient = new TcpClient();
			try
			{	
				// Test the connection - if smtp server down, exit.				
				tcpClient.Connect( smtpHost, int.Parse(smtpPort) );
				
				rv = true; // connection OK, no exception							
			}//end of try
			catch( SocketException ex )
			{
				rv = false; // connection FAIL
				MessageBox.Show(ex.Message.ToString(), "SMTP Connection Test");
			}//end of catch - SocketException
			finally
			{				
				if( tcpClient != null )
				{
					Debug.WriteLine("Finally - close TCP Client connection");
					tcpClient.Close();
				}// end of if
			}//end of finally
			return( rv );
		}//end of TestSMTPConnection

		/// <summary>
		/// Test the sql server DB connection
		/// 1) Open connection. is there any exception?
		/// 2) Close the connection if and only if no exception
		/// </summary>
		/// <param name="strConnetion">fully constructed connection string</param>
		/// <returns>true - connection OK</returns>
		/// <returns>false - connection FAIL</returns>
		public bool TestSQLConnection(string strConnect)
		{
			bool rv = false; // no connection
			try
			{
				SqlConnection myConnection = new SqlConnection(strConnect);
				myConnection.Open();
				MessageBox.Show("ServerVersion: " + myConnection.ServerVersion
					+ "\nState: " + myConnection.State.ToString()
					+ "\nData Base " + myConnection.Database.ToString()
					+ "\nData Source " + myConnection.DataSource.ToString()
					+ "\n Connection OK", "DB Connection Test");
				myConnection.Close();
		
				rv = true; // every OK
			}//end of try
			catch( SqlException sqlEx )
			{
				string errorMessages = "";
				for (int i=0; i < sqlEx.Errors.Count; i++)
				{
					errorMessages += "Index #" + i + "\n" +
						"Message: " + sqlEx.Errors[i].Message + "\n" +
						"LineNumber: " + sqlEx.Errors[i].LineNumber + "\n" +
						"Source: " + sqlEx.Errors[i].Source + "\n" +
						"Procedure: " + sqlEx.Errors[i].Procedure + "\n";
				}
				MessageBox.Show( errorMessages, "SQL Error" );
				
				rv = false; // something wrong
			}//end of catch 0 SQLException

			return( rv );
		}// emd pf TestSQLConnection

		/// <summary>
		/// Read from file line by line and Load the item into ComboBox.
		/// </summary>
		/// <param name="cbx">Pass by reference - combobox control</param>
		/// <param name="fn">File Name that contains combo box item</param>
		public void LoadComboBoxItem( ComboBox cbx, string fn )
		{
			String str;
			StreamReader sr = new StreamReader( fn );
			while( (str = sr.ReadLine()) != null )
			{
				cbx.Items.Add( str );
			}//end of while
			sr.Close();
		}//end of LoadComboBoxItem

        /// <summary>
        /// Get the number of line from a file.
        /// </summary>
        /// <param name="fn">INPUT: file name</param>
        /// <returns>FAIL: -1; OK: number of line</returns>
        public int GetNumLineOfFile(string fn)
        {
            int count = 0;
            if(!File.Exists( fn ))
                return -1;

            try
            {
                string line = "";
                StreamReader sr = new StreamReader( fn );
                while((line = sr.ReadLine()) != null)
                    count++;
            }
            catch(IOException ex)
            {
                count = -1;
                Debug.WriteLine( ex.Message + "\n" + ex.GetType().ToString() + ex.StackTrace );
            }

            return count;
        }//end of GetNumLineOfFile

        /// <summary>
        /// Get the number of line from a file, but ignore the line start with delimiter. 
        /// </summary>
        /// <param name="fn">File Name</param>
        /// <param name="ignor">Ignor the line that start with this character</param>
        /// <returns></returns>
        public int GetNumLineOfFile(string fn, char delimiter)
        {
            int count = 0;
            if(!File.Exists( fn ))
                return -1;

            if(delimiter == 0)
                return (GetNumLineOfFile( fn ));

            try
            {
                string line = "";
                StreamReader sr = new StreamReader( fn );
                while((line = sr.ReadLine()) != null)
                {
                    if(line[0] != delimiter)
                        count++;
                    Debug.WriteLine( "Line Num: " + count );
                }
            }
            catch(IOException ex)
            {
                count = -1;
                Debug.WriteLine( ex.Message + "\n" + ex.GetType().ToString() + ex.StackTrace );
            }

            return count;
        }//end of GetNumLineOfFile

		/// <summary>
		/// Initial the ComboBoxItem during start up.
		/// Every call will loop through the whole ini file QATool.ini for finding 
		/// the specific section.
		/// 1) Ensure the QATool.ini exit.
		/// 2) Find the section.
		/// 3) Add item for that particular section until '[' find.
		/// 4) Ignor line start with # sign
        /// eg: commObj.InitComboBoxItem(cboTo, "[To Address]");
		/// </summary>
		/// <param name="cbx"></param>
		/// <param name="iniSection"></param>
		/// <param name="iniFileName"></param>
        public void InitComboBoxItem(ComboBox cbx, string iniSection, string iniFileName)
        {
            string iniFile = iniFileName;
            string line = "";
            bool   done = false;
            try
            {
                cbx.Items.Clear();
                StreamReader sr = new StreamReader( iniFile );
                while( (line = sr.ReadLine()) != null && !done )
                {
                    line = line.Trim(' ');
                    //if( (line != "") && (line[0] != '#') )
                    if((!String.IsNullOrEmpty(line)) && (line[0] != '#'))
                    {
                        if( line.IndexOf(iniSection) != -1 )
                        {
                            while( (line = sr.ReadLine()) != null ) // read next line
                            {
                                line = line.Trim(' ');
                                //if( (line != "") && line[0] != '[' ) // DONE if empty line or  [ 
                                if((!String.IsNullOrEmpty(line)) && line[0] != '[') // DONE if empty line or  [ 
                                    cbx.Items.Add( line );
                                else
                                {
                                    done = true;
                                    break; // inner while
                                }
                            }//end of while
                        }//end of if
                    }//end of if - ignor #						
                }//end of while 
                sr.Close();
            }//end of try 
            catch( IOException ioEx )
            {
                Debug.WriteLine( "\t" + ioEx.Message );
            }//end of catch IOException

        }//end of InitComboBoxItem

        /// <summary>
        /// Initial the ComboBoxItem during start up.
        /// Every call will loop through the whole ini file QATool.ini for finding 
        /// the specific section.
        /// 1) Ensure the QATool.ini exit.
        /// 2) Find the section.
        /// 3) Add item for that particular section until '[' find.
        /// 4) Ignor line start with # sign
        /// </summary>
        /// <param name="cbx"></param>
        /// <param name="iniSection"></param>
        public void InitComboBoxItem(ComboBox cbx, string iniSection)
        {
            Trace.WriteLine("Common.cs - InitComboBoxItem( ComboBox, string )");
            InitComboBoxItem( cbx, iniSection, getFullPathIni() );
        }//end of InitComboBoxItem

		/// <summary>
		/// Get the full path file name of QATool.ini...
		/// 1) Check the current path first.
		/// 2) Not exit, pop up a dialog for location.
		/// </summary>
		/// <returns></returns>
		public string getFullPathIni()
		{
			Trace.WriteLine("Common.cs - getFullPathIni");
            string uri = Application.StartupPath + "\\LiveCapt.ini"; // uri = Full Path + file name + ext

			if( !File.Exists(uri) )
			{
				OpenFileDialog ofDlg = new OpenFileDialog();
				ofDlg.ShowReadOnly = true;
//				ofDlg.RestoreDirectory = true;

				if( ofDlg.ShowDialog() == DialogResult.OK )
				{
					//if( ofDlg.FileName != "" )
                    if( !String.IsNullOrEmpty(ofDlg.FileName) )
					{
						uri = ofDlg.FileName;
					}//end of if - open file name
				}// end of if - open file dialog								
			}// end of if - File !exist
			
			return( uri );;
		}// end of getFullPathIni

        /// <summary>
        /// Mainly check does file exist, and get file info
        /// </summary>
        /// <param name="fn"></param>
        /// <returns>OK - string with file info. Fail - error message</returns>
        public static string CheckFileInfo( string fn )
        {
            Trace.WriteLine("Common.cs - CheckFileInfo: " + fn);

            string str = null;
            //if( fn != "" )
            if( !String.IsNullOrEmpty(fn))
            {
                try
                {
                    FileInfo fInfo  = new FileInfo( fn );

                    str = "File Name = " + fInfo.FullName + "\n"
                        + "Creation Time = " + fInfo.CreationTime + "\n"
                        + "Last Access Time = " + fInfo.LastAccessTime + "\n"
                        + "Last Write Time = " + fInfo.LastWriteTime + "\n"
                        + "Size = " + fInfo.Length;
                }// end of try
                catch( IOException ex )
                {
                    Trace.WriteLine( ex.Message.ToString() );
                    str = ex.Message.ToString();                    
                }// end of catch
            }
            else
            {
                str = "No File Name specified!!";
            }// end of else
            return( str );
        }// end of CheckFileInfo

        /// <summary>
        /// Read a file into string object. There is a max chars for this implementation.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns>OK - string object; ERROR - ""</returns>
        public string readFileToString(string fileName)
        {
            Debug.WriteLine("Common.cs - readFileToString");

            string fileInString = "";
            StringBuilder strBuilder = new StringBuilder(); // again for testing purpose only
            try
            {
                // Create an instance of StreamReader to read from a file.
                // The using statement also closes the StreamReader.
                using (StreamReader sr = new StreamReader(fileName))
                {
                    String line;
                    // Read and display lines from the file until the end of 
                    // the file is reached.
                    while ((line = sr.ReadLine()) != null)
                    {
                        strBuilder = strBuilder.Append(line);
                    }
                }// end of using

                fileInString = strBuilder.ToString();
            }// end of try
            catch (IOException ex)
            {
                string msg = ex.Message + "\n" + ex.GetType().ToString() + ex.StackTrace;
                MessageBox.Show(msg, "Exception");
            }// end of catch

            return (fileInString);
        }//end of readFileToString

        /// <summary>
        /// Full Qaulified Path for the log file without tail "\\". Default is the application start up path.
        /// </summary>
        public string LogFullPath
        {
            get
            {
                return( _logPath );
            }
            set
            {
                _logPath = value;
            }
        }

        /// <summary>
        /// Check does directory exist, if not create it. Then set the directory.
        /// </summary>
        /// <param name="path"></param>
        public void SetCurrentDirectory(string path)
        {
            try
            {   // Determine whether the directory exists.
                if( !Directory.Exists(path) )
                {
                    //DirectoryInfo di = Directory.CreateDirectory(path);
                    Directory.CreateDirectory( path );
                }
            }
            catch (IOException ex)
            {
                Trace.WriteLine(ex.Message.ToString());
            }

            Directory.SetCurrentDirectory(path); // set the current directory
        }//end of VerifyDirectory

        /// <summary>
        /// fullFileName = Full Path + File Name + ext.
        /// myProcessTitle is used for killing the notepad app..
        /// </summary>
        /// <param name="fullFileName"></param>
        /// <param name="myProcessTitle"></param>
        public string ViewLogFromNotepad(string fullFileName, string myProcessTitle)
        {
            string logFullPathFileName = fullFileName;
            string rvStr = myProcessTitle;

            try
            {
                Process myProcess = new Process();

                int iHandle = Win32Api.FindWindowEx(0, 0, null, myProcessTitle);
                if (iHandle != 0) // log file opened - bug for != and return
                {
                    Win32Api.SetForegroundWindow(iHandle);
                    return( rvStr );
                }//end of if

                // Get the path that stores user documents.
                myProcess.StartInfo.FileName = logFullPathFileName;
                myProcess.Start();

                // Allow the process to finish starting. But what if loop forever?
                while (!myProcess.WaitForInputIdle()) ;
                rvStr = myProcess.MainWindowTitle;
            }//end of try
            catch (Win32Exception win32Ex)
            {
                Trace.WriteLine(win32Ex.Message + "\n" + win32Ex.GetType().ToString() + win32Ex.StackTrace);
            }//end of catch - win32 exception

            return (rvStr);
        }//end of ViewLogFromNotepad

        // C# to convert a string to a byte array.
        public byte[] StrToByteArray(string str)
        {
            System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
            return encoding.GetBytes(str);
        }//end of StrToByteArray

        /// <summary>
        /// Passing the full file name with path and then extract the short file name + ext.
        /// eg) D://abc//xyz//123//filename.txt --> filename.txt
        /// </summary>
        /// <param name="fullFileName"></param>
        /// <returns></returns>
        public string GetShortFileName(string fullFileName)
        {
            FileInfo fInfo = new FileInfo(fullFileName);
            return (fInfo.Name);
        }//end of GetShortFileName

        /// <summary>
        /// Trim special characters within a string.
        /// </summary>
        /// <param name="inString">INPUT: Original String</param>
        /// <param name="junk">INPUT: string of char that want to trim out</param>
        /// <returns></returns>
        public string TrimString(string inString, string junk)
        {
            string[] ret = inString.Split(junk.ToCharArray());
            return string.Concat(ret);
        }//end of TrimString

        // The following three functions were published in CodeProject website
        // http://www.codeproject.com/cs/miscctrl/DBGridCurrentRow.asp published by Chad Z. Hower 
        // http://www.codeproject.com/useritems/FromDataGrid2DataSet.asp publish by Blexrude

        /// <summary>
        /// Get ONE selected row from the data grid and return the data row.
        /// Example of Usage: DataRow xRow = commObj.CurrentRow(MyDataGrid);
        /// </summary>
        /// <param name="aGrid"></param>
        /// <returns></returns>
        public DataRow GetCurrentRow(DataGrid aGrid)
        {
            CurrencyManager xCM = (CurrencyManager)aGrid.BindingContext[aGrid.DataSource, aGrid.DataMember];
            DataRowView xDRV = (DataRowView)xCM.Current;
            return xDRV.Row;
        }//end of GetCurrentRow

        /// <summary>        
        /// Returns an ArrayList containing the indexes of selected rows in a DataGrid..
        /// </summary>
        /// <param name="aGrid"></param>
        /// <returns></returns>
        public ArrayList GetSelectedRowsArray(DataGrid aGrid)
        {
            ArrayList al = new ArrayList();
            CurrencyManager cm = (CurrencyManager)aGrid.BindingContext[aGrid.DataSource, aGrid.DataMember];
            DataView dv = (DataView)cm.List;
            for(int i = 0; i < dv.Count; ++i)
            {
                if(aGrid.IsSelected( i ))
                    al.Add( i );
            }
            return al; 
        }//end of GetSelectedRowsArray

        /// <summary>
        /// Returns a DataSet containing information for selected rows of a DataGrid
        /// </summary>
        /// <param name="aGrid"></param>
        /// <returns></returns>
        public DataSet GetSelectedRowsDataSet(DataGrid aGrid)
        {
            //DataSet and DataTable objects used in creating the new DataSet. 
            DataSet ds = new DataSet();
            DataTable table = new DataTable();
            try
            {
                //Use the CurrencyManager to create a data view so that you can 
                //use this to create the table schema. 
                CurrencyManager cm = (CurrencyManager)aGrid.BindingContext[aGrid.DataSource, aGrid.DataMember];
                DataView dv = (DataView)cm.List;
                //Add columns from dataview to table 
                foreach(DataColumn col in dv.Table.Columns)
                {
                    table.Columns.Add( col.ColumnName );
                }

                //Using the GetSelectedRowsArray function loop through each of the selected 
                //row indexes setting the CurrentRowIndex property of your grid. Then import 
                //the currrent row using the GetCurrentDataRow function. 
                foreach(int i in GetSelectedRowsArray( aGrid ))
                {
                    aGrid.CurrentRowIndex = i;
                    table.ImportRow( this.GetCurrentRow( aGrid ) );
                }
                //Add your new table to the DataSet and return it. 
                ds.Tables.Add( table );
                return ds;
            }//end of try
            catch(System.Exception se)
            {
                //Error has occured 
                throw se;
            } // end of catch
        }//end of GetSelectedRowsDataSet

        /// <summary>
        /// Get the absolute path and export file name. Default file name is the table name .csv
        /// eg: table name is mytable, file name is mytable.csv
        /// </summary>
        /// <param name="fn">input default file name</param>
        /// <returns>string: full path filename OR ""</returns>
        public string GetSaveAbsPathFileName(string fn)
        {
            string filename = fn;
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "csv files (*.csv)|*.csv|txt files (*.txt)|*.txt|All files (*.*)|*.*";
                saveFileDialog.FilterIndex = 1;
                saveFileDialog.RestoreDirectory = true;
                saveFileDialog.FileName = fn;

                if(saveFileDialog.ShowDialog() == DialogResult.OK)
                    filename = saveFileDialog.FileName;
                else
                    filename = ""; // cancel by user
            }
            catch(IOException ex)
            {
                Debug.WriteLine( ex.Message + "\n" + ex.GetType().ToString() + ex.StackTrace, "GetSaveAbsPahtFileName" );
            }

            return (filename);
        }// end of GetSaveAbsPathFileName

        /// <summary>
        /// Importing Data From CSV File. 
        /// You can get connected to driver either by using DSN or connection string
        /// NOTE: 
        /// filetable - refer to the input csv short file name and represent table name in data set.
        /// TC1 - refer to the name of the sheet in csv file. It is good for multiple sheet (tables) in data set.
        /// </summary>
        /// <param name="filetable">Input SHORT csv file name: Will parse for table name and schema location</param>
        /// <param name="workingFolder">Forgot what it is. Just pass the working folder path here</param>
        /// <returns></returns>
        public DataSet connectCSV(string filetable, string workingFolder)
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
                conn = new System.Data.Odbc.OdbcConnection( strConnString.Trim() );

                // SAVE - For creating a connection using DSN, use following line
                // conn	=	new System.Data.Odbc.OdbcConnection(DSN="MyDSN");

                conn.Open(); //Open the connection                 
                sql_select = "select * from [" + filetable + "]"; //Fetch records from CSV - filetable is the file name
                obj_oledb_da = new System.Data.Odbc.OdbcDataAdapter( sql_select, conn );
                obj_oledb_da.Fill( ds, "TC1" ); //Fill dataset with the records from CSV file; TC1 is the name of the sheet

                #region kentsave
                //int x = ds.Tables[0].Columns.Count;
                //string str = ds.Tables[0].Columns[3].ToString();
                //string str2 = ds.Tables[0].Columns[3].ColumnName;
                //string str3 = ds.Tables[0].Rows[9]["BCC"].ToString();
                
                //Set the datagrid properties
                //dataGridView.DataSource = ds;
                //dataGridView.DataMember = "TC1"; // name of the sheet    
                
                //for(int i = 0; i < lstFailCell.Length; i++)
                //{
                //    DataGridViewCell cell = dataGridView[lstFailCell[i].X, lstFailCell[i].Y];
                //    cell.Style.BackColor = Color.YellowGreen; ;
                //}
                #endregion

                //Close Connection to CSV file
                conn.Close();
            }
            catch(OdbcException e)
            {
                string errMsg = "";

                for(int i = 0; i < e.Errors.Count; i++)
                {
                    errMsg += "Index #" + i + "\n" +
                              "Message: " + e.Errors[i].Message + "\n" +
                              "NativeError: " + e.Errors[i].NativeError.ToString() + "\n" +
                              "Source: " + e.Errors[i].Source + "\n" +
                              "SQL: " + e.Errors[i].SQLState + "\n";
                }

                MessageBox.Show( errMsg );
            }//end of catch - OdbcException
            //catch(Exception ex2) //Error
            //{
            //    MessageBox.Show( ex2.Message );
            //}
            return ds;
        }//end of ConnectCSV

    }// end of class - CommObj
}