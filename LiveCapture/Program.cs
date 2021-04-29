using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace LiveCapture
{
    static class Program
    {        
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                //Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault( false );
                Application.Run( new QATool() );
            }
            catch(Exception ex)
            {
                MessageBox.Show( ex.Message + "\n" + ex.GetType().ToString() + ex.StackTrace, "Main" );
            }
        }
    }
}