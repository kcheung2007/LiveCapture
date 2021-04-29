using System;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace LiveCapture
{
	/// <summary>
	/// Handle sending outlook mail only.
	/// Contain its own logging mechanism - this moment, the file name is hard code
	/// </summary>
	public class olMailObj
	{
        //private Microsoft.Office.Tools.Outlook.Application oApp;
        private Outlook._Application oApp;
        private Outlook._NameSpace	oNameSpace;

		private string _To   = "";
		private string _From = "";
		private string _CC   = "";
		private string _BCC  = "";
		private string _Subj = "";
		private string _Body = "";
		private string _filename = ""; // attachment filename
		private string _profile  = ""; // MA mail profile
		private string _password = ""; // outlook profile password

		private int    _delay = 0; 

//		private QATool.CommObj commObj = new CommObj();

		public olMailObj()
		{
			// Return a reference to the MAPI layer
			// 1) Create Outlook session - reference to Outlook Interop assembly
			// 2) Bound to MAPI namespace to get access to all Outlook folder
			try
			{
                olWriteLine("LiveCapt.log", "olMailObj.cs - create new outlook");
				oApp = new Outlook.ApplicationClass();
                //oApp = new Microsoft.Office.Tools.Outlook.Application();
                olWriteLine("LiveCapt.log", "olMailObj.cs - Get Namespace");
				oNameSpace = oApp.GetNamespace("MAPI");
			}
			catch( Exception ex )
			{
				MessageBox.Show( ex.ToString(), "Error" );
                olWriteLine("LiveCapt.log", "olMailObj.cs - " + ex.ToString());
                olWriteLine("LiveCapt.log", "\tST - " + ex.StackTrace.ToString());
			}//end of catch - ex
		}//end of constructor - olMailObj

		~olMailObj()
		{
            olWriteLine("LiveCapt.log", "olMailObj.cs - destructor of ~olMailObj");
			oNameSpace.Logoff(); // both sucessful or fail

            if( oApp != null )
            {
                olWriteLine("LiveCapt.log", "\tLogoff and then Quit oApp");
                oApp.Quit();
            }
		}//end of destructure
		public string strTo
		{
			set
			{
				_To = value;
			}
		}// end of property - strTo

		public string strFrom
		{
			set
			{
				_From = value;
			}
		}// end of property - strFrom

		public string strCC
		{
			set
			{
				_CC = value;
			}
		}// end of property - strCC

		public string strBCC
		{
			set
			{
				_BCC = value;
			}
		}// end of property - strBCC

		public string strSubject
		{
			set
			{
				_Subj = value;
			}
		}// end of property - strSubject

		public string strBody
		{
			set
			{
				_Body = value;
			}
			get
			{
				return( _Body );
			}
		}// end of property - strBody

		public string strAttachName
		{
			set
			{
				_filename = value;
			}
		}// end of property - strAttachName

		public string strProfile
		{
			set
			{
				_profile = value;
			}
		}// end of property - strProfile

		public string strPassword
		{
			set
			{
				_password = value;
			}
		}// end of property - strPassword

		public int nDelay
		{
			set
			{
				_delay = value;
			}
		}//end of property - nDelay

        /// <summary>
        /// Sent the mail
        /// </summary>
        /// <returns>true - send ok, false - fail</returns>
		public bool dumpToOutbox()
		{
			// Logon to Outlook user (account)
			// Profile: Set to null if using the currently logged on user, 
			//			or set to an empty string ("") for the default Outlook Profile. 
			// Password: Set to null if  using the currently logged on user, 
			//			 or set to an empty string ("") for the default Outlook Profile.
			// ShowDialog: Set to True to display the Outlook Profile dialog box.
			// NewSession: Set to True to start a new session. Set to False to use the current session. 
			bool rv = true; // OK
			try
			{
				oNameSpace.Logon( _profile, _password, false, true );

// save for reference - adding the pst into outlook
//				oNameSpace.Logon( "duser51", "password0", false, true );
//				oNameSpace.AddStore( @"D:\\R1_00017.pst" );

                
                int count = oNameSpace.Folders.Count;
                Outlook.MAPIFolder olMapiFolder = oNameSpace.Folders.GetLast();
                int mailCount = olMapiFolder.Items.Count;
                                
				// creates a new outlook Mail Item object
				Outlook._MailItem oMailItem = (Outlook._MailItem)oApp.CreateItem(Outlook.OlItemType.olMailItem);      

				oMailItem.To      = _To;
				oMailItem.CC      = _CC;
				oMailItem.BCC     = _BCC;
				oMailItem.Subject = _Subj;
				oMailItem.Body    = _Body;
                
				// oMailItem.Attachments.Add( "c:\\bible\\map1.pdf", 1, 1, "displayname");
				// public abstract new Outlook.Attachment 
				//	Add( System.Object Source, 
				//		 System.Object Type,		[don't know what it is - 1 == byValue]
				//		 System.Object Position,	[lenght = 4, position is 3 (0-3)]
				//		 System.Object DisplayName )
				//if( _filename != "")
                if( !String.IsNullOrEmpty(_filename) )
				{
					char[] delim = new char[]{';'};
					int i = _Body.Length; // append to the body 
					foreach( string str in _filename.Split(delim) )
					{
						oMailItem.Attachments.Add(str, 1, ++i, str); // ++i for next position
					}//end of foreach
				}//end of if - attachment
			         
//				uncomment this to also save this in your draft
//				oMailItem.Save();
                
				//adds it to the outbox
				oMailItem.Send();
//				Thread.Sleep(1000); // 1 sec				
			}//end of try 
			catch( Exception ex )
			{
				ex.Message.ToString();
				MessageBox.Show( ex.Message.ToString(), "Error - dumpToOutBox" );
                olWriteLine("LiveCapt.log", "ST: " + ex.StackTrace.ToString());
				rv = false; // fail 
//				commObj.LogToFile( "ST: " + ex.StackTrace.ToString() );
			}//end of catch - Exception

			oNameSpace.Logoff(); // both sucessful or fail
			return( rv );
		}//end of dumpToOutbox

		/// <summary>
		/// Outlook Mail Object's own logging system.
		/// Hard Code the file name - olMailObj.log
		/// </summary>
		/// <param name="fn">file name - hard code this moment</param>
		/// <param name="flag">true - turn on debug, false - turn off debug</param>
		public void olWriteLine(string fn, string line)
		{
			StreamWriter sw = File.AppendText( fn );
			sw.WriteLine("{0}",line );
			sw.Close();		
		}//end of dumpToOutbox()

        public void nsLogoff()
        {
            olWriteLine("LiveCapt.log", "olMailObj.cs - nsLogoff");
            oNameSpace.Logoff(); // both sucessful or fail
            oApp.Quit();
        }
	}
}
