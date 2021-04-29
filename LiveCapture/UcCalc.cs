using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace LiveCapture
{
    public partial class UcCalc : UserControl
    {
        //public event UpdateStatusEventHandler statusChanged;
        public event EventHandler<StatusEventArgs> statusChanged;
        
        public UcCalc()
        {
            InitializeComponent();            
        }

        // "StatusEventArgs" - argument in EventArgs class
        protected virtual void OnUpdateStatusBar(StatusEventArgs eArgs)
        {
            statusChanged(this, eArgs);
        }//end of virtual

        private void UcCalc_Load(object sender, EventArgs e)
        {

        }
    }//end of class UcCalc


}
