using System;
using System.Diagnostics;
using System.IO;


namespace LiveCapture
{
	/// <summary>
    /// Summary description for FilesListObj.
    /// Pass in a directory name and store each file name into this obj
	/// </summary>
	public class FilesListObj
	{
		private int m_idxFile; // file pointer - point to current file
		private int m_NumFile; // total number of files in particular folder
		private string m_inFolder = "";

		private DirectoryInfo m_di;
		private FileInfo[] m_lstFiles; // list of file name (no path info)

        /// <summary>
        /// Constructor - must pass in folder name
        /// Get Direcotry info, list of file name, and number of file etc.
        /// </summary>
        /// <param name="inFolder"></param>        
        public FilesListObj(string InFolder)
		{
            Debug.WriteLine("FilesListObj.cs - constructor:FilesListObj");
			m_inFolder = InFolder;			
			m_di = new DirectoryInfo(InFolder); // attachment folder
            if (m_di.Exists)
            {
                m_lstFiles = m_di.GetFiles();
                m_NumFile = m_lstFiles.Length;
            }
		}//end of constructor

        ~FilesListObj()
        {
            Debug.WriteLine("FilesListObj.cs - destructor:FilesListObj");
            m_lstFiles = null;
        }

		public int idxFile
		{
			get
			{
				return m_idxFile;
			}
			set
			{
				m_idxFile = value;
			}
		}// end of idxAttach

		public int numFile
		{
			get
			{
				return( m_NumFile );
			}
		}// end of numFile

		public string fullFileName
		{
			get
			{
				return( m_lstFiles[m_idxFile].FullName );
			}
		}//end of attchFullFame

        public long currentFileSize
        {
            get
            {
                return (m_lstFiles[m_idxFile].Length);
            }
        }//end of currentFileSize


        public FileInfo[] getListFiles
        {
            get
            {
                return( m_lstFiles );
            }
        }
	}//end of class - FilesListObj
}//end of namespace - LiveCapture