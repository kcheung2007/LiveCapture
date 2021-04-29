using System;
using System.Diagnostics;
using System.IO;


namespace LiveCapture
{
	/// <summary>
	/// Summary description for AttachObj.
	/// </summary>
	public class AttachObj
	{
		private int m_IdxAttach; 
		private int m_NumFile;
		//private string m_inFolder;

		private DirectoryInfo m_di;
		private FileInfo[] m_lstFiles;

		public AttachObj( string inFolder )
		{
			//m_inFolder = inFolder;			
			m_di = new DirectoryInfo(inFolder); // attachment folder
			m_lstFiles = m_di.GetFiles();
			m_NumFile = m_lstFiles.Length;
		}//end of constructor

		public int IdxAttach
		{
			get
			{
				return m_IdxAttach;
			}
			set
			{
				m_IdxAttach = value;
			}
		}// end of idxAttach

		public int NumFile
		{
			get
			{
				return( m_NumFile );
			}
		}// end of numFile

		public string AttachFullName
		{
			get
			{
				return( m_lstFiles[m_IdxAttach].FullName );
			}
		}//end of attchFullFame

        //public FileInfo[] GetListFiles
        //{
        //    get
        //    {
        //        return( m_lstFiles );
        //    }
        //}

        public FileInfo[] GetListFiles()
        {
            return (m_lstFiles);
        }//end of GetListFiles

        /// <summary>
        /// Return a list of FileInfo object if the file name start with the input pattern.
        /// </summary>
        /// <param name="strPatten">Input search pattern</param>
        /// <returns>Array of FileInfo</returns>
        public FileInfo[] GetFilesStartWith(string strPatten)
        {
            int numFile = 0;
            try
            {
                for(int i = 0; i < m_lstFiles.Length; i++)
                {
                    if(m_lstFiles[i].ToString().StartsWith( strPatten, StringComparison.Ordinal ))
                    {
                        numFile++;
                    }
                }
            }//end of try
            catch(StackOverflowException sofEx)
            {
                // is it right catch?
                Debug.WriteLine( sofEx.Message );
            }
            //catch(Exception ex)
            //{
            //    Debug.WriteLine( ex.Message );
            //}

            // create the array
            FileInfo [] fileList = new FileInfo[numFile];

            try
            {
                int j = 0;
                for (int i = 0; i < m_NumFile; i++)
                {
                    if (m_lstFiles[i].ToString().StartsWith(strPatten, StringComparison.Ordinal))
                    {
                        fileList[j] = m_lstFiles[i];
                        j++;
                    }
                }
            }//end of try
            catch(StackOverflowException sofEx)
            {
                // is it right catch?
                Debug.WriteLine( sofEx.Message );
            }
            //catch (Exception ex)
            //{
            //    Debug.WriteLine(ex.Message);
            //}
            return (fileList);
        }

	}
}
