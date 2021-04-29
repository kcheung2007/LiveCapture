using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LiveCapture
{
    public partial class QATool : Form
    {
        private UcCalc        pgCalc;
        private UcMailSender  pgMailSender;
        private UcNotesClient pgNotesClient;
        private UcTester      pgTester;
        private UcExplorer    pgExplorer;
        private UcMailCounter pgMailCounter;
        private UcSshCmd      pgSshCmd;

        private int defaultWidth;
        private int defaultHeight;

        public QATool()
        {
            InitializeComponent();

            try
            {
                #region Left Top Tab Control
                pgCalc = new UcCalc();
                pgCalc.Dock = DockStyle.Fill;
                this.tabPgNotePad.Controls.Add(pgCalc);
                //pgCalc.statusChanged += new UpdateStatusEventHandler(pgCalc_statusChanged);
                pgCalc.statusChanged += new EventHandler<StatusEventArgs>( pgCalc_statusChanged );

                pgMailSender = new UcMailSender();
                pgMailSender.Dock = DockStyle.Fill;
                this.tabPgSMTP.Controls.Add(pgMailSender);
                pgMailSender.statusChanged += new EventHandler<StatusEventArgs>( pgMailSender_statusChanged );

                pgNotesClient = new UcNotesClient();
                pgNotesClient.Dock = DockStyle.Fill;
                this.tabPgNotesClient.Controls.Add(pgNotesClient);
                pgNotesClient.statusChanged += new EventHandler<StatusEventArgs>( pgNotesClient_statusChanged );

                pgTester = new UcTester();
                pgTester.Dock = DockStyle.Fill;
                this.tabPgTester.Controls.Add(pgTester);
                pgTester.statusChanged += new EventHandler<StatusEventArgs>( pgTester_statusChanged );

                pgMailCounter = new UcMailCounter();
                pgMailCounter.Dock = DockStyle.Fill;
                this.tabPgCounter.Controls.Add( pgMailCounter );
                pgMailCounter.statusChanged += new EventHandler<StatusEventArgs>( pgMailCounter_statusChanged );

                pgSshCmd = new UcSshCmd();
                pgSshCmd.Dock = DockStyle.Fill;
                this.tabPgCmd.Controls.Add( pgSshCmd );
                pgSshCmd.statusChanged += new EventHandler<StatusEventArgs>( pgSshCmd_statusChanged );
                pgSshCmd.displayMsgChanged += new EventHandler<DisplayMsgEventArgs>( pgSshCmd_displayMsgChanged );
                
                #endregion
                
                #region Bottom Tab Control
                pgExplorer = new UcExplorer();
                pgExplorer.Dock = DockStyle.Fill;
                this.tabPgExplorer.Controls.Add(pgExplorer);
                pgExplorer.statusChanged += new EventHandler<StatusEventArgs>( pgExplorer_statusChanged );
                
                #endregion

                defaultWidth = this.Width;
                defaultHeight = this.Height;
            }
            catch (Exception ex)
            {
                string msg = ex.Message + "\n" + ex.GetType().ToString() + ex.StackTrace;
                MessageBox.Show(msg, "QATool.cs - Error");
            }
        }//end of Constructor - QATool
       

        private void QATool_Load(object sender, EventArgs e)
        {
            try
            {
                Debug.WriteLine("QATool.cs - MainForm_Load");
                this.Text = Application.ExecutablePath;
                //                this.Activate();
            }//end of try
            catch (Exception ex)
            {
                string msg = ex.Message + "\n" + ex.GetType().ToString() + ex.StackTrace;
                MessageBox.Show(msg, "QATool - Load");
            }
        }//end of QATool_Load

        void pgSshCmd_displayMsgChanged(object sender, DisplayMsgEventArgs e)
        {
            //pgCalc.Controls[0].Text = "This is test";
            pgCalc.Controls[0].Text = e.strMsg; // controls[0] is the notepad control
            //throw new NotImplementedException();
        }

        #region Status Change EVENT from user control
        private void pgCalc_statusChanged(object sender, StatusEventArgs e)
        {
            Debug.WriteLine("QATool.cs - pgSimpleTest_statusChanged");
            toolStripStatusLabel1.Text = e.strMsg;
        }
        
        private void pgMailSender_statusChanged(object sender, StatusEventArgs e)
        {
            Debug.WriteLine("QATool.cs - pgMailSender_statusChanged");
            toolStripStatusLabel1.Text = e.strMsg;
        }

        private void pgNotesClient_statusChanged(object sender, StatusEventArgs e)
        {
            Debug.WriteLine("QATool.cs - pgNotesClient_statusChanged");
            toolStripStatusLabel1.Text = e.strMsg;
        }

        private void pgTester_statusChanged(object sender, StatusEventArgs e)
        {
            Debug.WriteLine("QATool.cs - pgTtester_statusChanged");
            toolStripStatusLabel1.Text = e.strMsg;
        }

        private void pgExplorer_statusChanged(object sender, StatusEventArgs e)
        {
            Debug.WriteLine("QATool.cs - pgExplorer_statusChanged");
            toolStripStatusLabel1.Text = e.strMsg;
        }

        private void pgMailCounter_statusChanged(object sender, StatusEventArgs e)
        {
            Debug.WriteLine( "QATool.cs - pgMailCounter_statusChanged" );
            toolStripStatusLabel1.Text = e.strMsg;
        }

        private void pgSshCmd_statusChanged(object sender, StatusEventArgs e)
        {
            Debug.WriteLine( "QATool.cs - pgSshCmd_statusChanged" );
            toolStripStatusLabel1.Text = e.strMsg;
        }
        #endregion



        public void UpdateStatusBar(string msg)
        {
            toolStripStatusLabel1.Text = msg;         
        }//end of UpdateStatusBar

        private void tabCtrlLeft_SelectedIndexChanged(object sender, EventArgs e)
        {
            Debug.WriteLine("QATool.cs - tabCtrlLeft_SelectedIndexChanged");
            UpdateStatusBar(tabCtrlLeft.SelectedTab.Text);
        }//end of pgMailSender_statusChanged

        private void tabCtrlRight_SelectedIndexChanged(object sender, EventArgs e)
        {
            Debug.WriteLine("QATool.cs - tabCtrlRight_SelectedIndexChanged");
            UpdateStatusBar(tabCtrlRight.SelectedTab.Text);
        }//end of tabCtrlRight_SelectedIndexChanged

        private void tabCtrlBottom_SelectedIndexChanged(object sender, EventArgs e)
        {
            Debug.WriteLine("QATool.cs - tabCtrlBottom_SelectedIndexChanged");
            UpdateStatusBar(tabCtrlBottom.SelectedTab.Text);
        }//end of tabCtrlBottom_SelectedIndexChanged

        private void tabCtrlLeft_Enter(object sender, EventArgs e)
        {
            Debug.WriteLine("QATool.cs - tabCtrlLeft_Enter");
        }

        private void QATool_ResizeEnd(object sender, EventArgs e)
        {
            Debug.WriteLine( "MainForm.cs - MainForm_ResizeEnd" );
            this.Width = this.Width < defaultWidth ? defaultWidth : this.Width;

            if(this.Height < defaultHeight)
                this.Height = defaultHeight;
        }//end of QATool_ResizeEnd

        /// <summary>
        /// Generate Bloomberg XML Data based on bloomberg xsd definition file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBloomData_Click(object sender, EventArgs e)
        {
            WinGenBloomData genBData = new WinGenBloomData();
            genBData.ShowDialog();
        }//end of btnBloomData_Click

        /// <summary>
        /// Extract the header/value from zArchived eml, and construct the test case 
        /// spread sheet for regression test.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnZAData_Click(object sender, EventArgs e)
        {
            WinZAData genZAData = new WinZAData();
            //DialogResult rv = genZAData.ShowDialog();
            genZAData.ShowDialog();
        }//end of btnZAData_Click

        private void btnNotesData_Click(object sender, EventArgs e)
        {
            WinNotesIniData genIniData = new WinNotesIniData();
            genIniData.ShowDialog();
        }//end of btnNotesData_Click

        private void btnDXLData_Click(object sender, EventArgs e)
        {
            WinGenDXLData genDXLData = new WinGenDXLData();
            genDXLData.ShowDialog();
        }
    }//end of QATool - class

    // Custom Event - send notification to main form to update the status bar
    public class StatusEventArgs : EventArgs
    {
        private string _msg = "";
        public StatusEventArgs(string msg)
        {
            _msg = msg;
        }

        public string strMsg
        {
            get
            {
                return _msg;
            }
            set
            {
                _msg = value;
            }
        }//end of msg
    }//end of class - StatusEventArgs 

    /// <summary>
    /// Custom event - Display message in Notepad
    /// </summary>
    public class DisplayMsgEventArgs : EventArgs
    {
        private string _msg;
        public DisplayMsgEventArgs(string msg)
        {
            _msg = msg;
        }

        public string strMsg
        {
            get
            {
                return _msg;
            }
            set
            {
                _msg = value;
            }
        }//end of msg
    }//end of class - DisplayMsgEventArgs 
}