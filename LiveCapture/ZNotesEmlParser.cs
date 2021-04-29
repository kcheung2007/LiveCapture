using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace LiveCapture
{
    class ZNotesEmlParser
    {
        //private string fileName = ""; 
        private FileInfo fileInfo;
        private string fileString;

        /// <summary>
        /// Constructor:
        /// </summary>
        /// <param name="emlFileName">Full path eml file name</param>
        public ZNotesEmlParser(string emlFileName)
        {
            //fileName = emlFileName;
            fileInfo = new FileInfo(emlFileName);
        }

        public Dictionary<string, string> ParseEmlFile( string[] strPatten )
        {
            ReadEmlFile();

            Dictionary<string, string> keyValSet = new Dictionary<string, string>();
            string strTmp = "";

            try
            {
                foreach (string str in strPatten)
                {
                    if (-1 != str.IndexOf("X-Z"))
                    {
                        strTmp = getXZantazValue(str, fileString);
                        keyValSet.Add(str, strTmp);
                    }
                    else
                        if (-1 != str.IndexOf("ZA"))
                        {
                            strTmp = getXNotesItem(str, fileString);
                            keyValSet.Add(str, strTmp);
                        }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.GetType(), "ParseEmlFile");
            }
            //string tmpString = getXNotesItem(strPatten[6], fileString);
            //string tmpString1 = getXZantazValue("X-ZANTAZ-RECIPIENT-NAMES1", fileString);
            //string tmpString2 = getXNotesItem("ZArchiveOriginalRecipients", fileString);

            return (keyValSet);
        }//end of ParseEmlFile

        /// <summary>
        /// Read the eml file into a string object for processing.
        /// Assumption: If the file less than 256KB, read the entire file into the string object.
        /// Otherwise, read the first 5000 lines of the file. This assume the header info
        /// is less than 5000 lines.
        /// </summary>
        /// <returns>bool: true - OK; false - Fail</returns>
        private bool ReadEmlFile()
        {
            bool rv = true; //assume everything OK

            try
            {               
                if (fileInfo.Length < 262144) //256 * 1024
                {
                    //read entire file into string object
                    fileString = File.ReadAllText(fileInfo.FullName);
                }
                else
                {
                    // read first 5000 lines - assume header info less than 5000 lines
                    int numLine = 0;
                    string line = "";
                    StringBuilder sb = new StringBuilder();                    
                    StreamReader sr = new StreamReader(fileInfo.FullName);
                    while ((numLine < 5000) && ((line = sr.ReadLine()) != null))
                    {
                        sb.Append(line); // "/r/n" was trimmed
                        numLine++;
                    }
                    sr.Close();
                    fileString = sb.ToString();
                }//end of else                
            }
            catch (Exception ex)
            {
                rv = false;
                MessageBox.Show(ex.Message + "\n" + ex.GetType().ToString());
            }//end of catch
            finally
            {
               
            }
            return (rv);
        }//end of ReadEmlFile

        /// <summary>
        /// Look for "name=" value, then move backward for "X-Notes-Item" value.
        /// Return the whole block as string.        
        /// </summary>
        /// <param name="name">Search patten "name=" from Notes header</param>
        /// <param name="file">Input file as a long string</param>
        /// <returns>OK: X-Notes-Items value; Fail: ""</returns>
        private string getXNotesItem(string name, string file)
        {
            int p1 = 0;
            int p2 = 0;
            string patten1 = "name=" + name;
            string patten2 = "X-Notes-Item: ";
            string retString = "";

            try
            {
                if (-1 == (p1 = file.IndexOf(patten1))) //not found
                    return ("");
                if( -1 == (p2 = file.LastIndexOf(patten2, p1)))
                    return ("");

                retString = file.Substring((p2 + patten2.Length), (p1 - p2 - patten2.Length));
            }
            catch (Exception ex)
            {
                retString = "";
                MessageBox.Show(ex.Message + "\n" + ex.GetType().ToString() + ex.StackTrace, "getXNotesItem");                
            }

            return (retString);
        }//end of getXNotesItem

        /// <summary>
        /// Look for "X-ZANTAZ-[name]", then next "X-" value.
        /// Return the whole block as string
        /// </summary>
        /// <param name="name">Search patten of X-ZANTAZ- value</param>
        /// <param name="file">Input file as a long string</param>
        /// <returns>OK: X-ZANTAZ value; Fail: ""</returns>
        private string getXZantazValue(string name, string file)
        {
            int p1 = 0;
            int p2 = 0;
            string patten1 = name + ": ";
            string patten2 = "X-";
            string retString = "";

            try
            {
                if( -1 == (p1 = file.IndexOf(patten1)))//Not found
                    return ("");
                if( -1 == (p2 = file.IndexOf(patten2, p1 + patten1.Length)))
                    return("");

                retString = file.Substring((p1 + patten1.Length), (p2 - p1 - patten1.Length));
            }
            catch (Exception ex)
            {
                retString = "";
                MessageBox.Show(ex.Message + "\n" + ex.GetType().ToString() + ex.StackTrace, "getXZantazValue");                
            }

            return (retString);
        }
    }
}
