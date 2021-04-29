using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LiveCapture
{
    public partial class TC1Preference : Form
    {
        private int sendMethod; // defaul - SMTP; 1 : notes client

        //private static WsClient.Properties.Settings appSetting = new WsClient.Properties.Settings();

        public TC1Preference()
        {
            InitializeComponent();
        }

        private void MailPreference_Load(object sender, EventArgs e)
        {
            cboSMTP.Text = LiveCapture.Properties.Settings.Default.SMTP_IP;
            cboPort.Text = LiveCapture.Properties.Settings.Default.Port_number;
            sendMethod = LiveCapture.Properties.Settings.Default.SendMethod;
            switch (sendMethod)
            {
                case 0 : //defaul - SMTP protocol
                    rdoSMTP.Checked = true;
                    InitControls(true);
                    rdoNotesClient.Checked = false;
                    break;
                case 1 : //notes client COM
                    rdoSMTP.Checked = false;
                    InitControls(false);
                    rdoNotesClient.Checked = true;
                    break;
            }//end of switch
        }//end of MailPreference_Load

        private void InitControls( bool val )
        {
            cboSMTP.Enabled = val;
            cboPort.Enabled = val;
            btnTest.Enabled = val;
            lblSMTP.Enabled = val;
            lblPort.Enabled = val;
        }//end of InitControls

        private void btnOK_Click(object sender, EventArgs e)
        {
            LiveCapture.Properties.Settings.Default.SendMethod = sendMethod;
            LiveCapture.Properties.Settings.Default.SMTP_IP = cboSMTP.Text;
            LiveCapture.Properties.Settings.Default.Port_number = cboPort.Text;

            LiveCapture.Properties.Settings.Default.Save();


        }//end of btnOK_Click

        private void rdoSMTP_Click(object sender, EventArgs e)
        {
            if (rdoSMTP.Checked)
            {
                sendMethod = 0;
                InitControls(true);
            }
        }

        private void rdoNotesClient_Click(object sender, EventArgs e)
        {
            if (rdoNotesClient.Checked)
            {
                sendMethod = 1;
                InitControls(false);
            }
        }//end of InitControls

    }
}