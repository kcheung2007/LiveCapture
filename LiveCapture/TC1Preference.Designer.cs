namespace LiveCapture
{
    partial class TC1Preference
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TC1Preference));
            this.btnTest = new System.Windows.Forms.Button();
            this.cboPort = new System.Windows.Forms.ComboBox();
            this.cboSMTP = new System.Windows.Forms.ComboBox();
            this.lblPort = new System.Windows.Forms.Label();
            this.lblSMTP = new System.Windows.Forms.Label();
            this.rdoSMTP = new System.Windows.Forms.RadioButton();
            this.rdoNotesClient = new System.Windows.Forms.RadioButton();
            this.gbxSMTP = new System.Windows.Forms.GroupBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.ttpPreference = new System.Windows.Forms.ToolTip(this.components);
            this.btnCancel = new System.Windows.Forms.Button();
            this.gbxSMTP.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(286, 9);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(48, 21);
            this.btnTest.TabIndex = 40;
            this.btnTest.Text = "Test";
            // 
            // cboPort
            // 
            this.cboPort.ItemHeight = 13;
            this.cboPort.Location = new System.Drawing.Point(228, 9);
            this.cboPort.Name = "cboPort";
            this.cboPort.Size = new System.Drawing.Size(52, 21);
            this.cboPort.Sorted = true;
            this.cboPort.TabIndex = 39;
            this.cboPort.Text = "25";
            // 
            // cboSMTP
            // 
            this.cboSMTP.ItemHeight = 13;
            this.cboSMTP.Items.AddRange(new object[] {
            ""});
            this.cboSMTP.Location = new System.Drawing.Point(48, 9);
            this.cboSMTP.Name = "cboSMTP";
            this.cboSMTP.Size = new System.Drawing.Size(144, 21);
            this.cboSMTP.Sorted = true;
            this.cboSMTP.TabIndex = 38;
            this.cboSMTP.Text = "10.1.89.201";
            // 
            // lblPort
            // 
            this.lblPort.Location = new System.Drawing.Point(194, 13);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(28, 16);
            this.lblPort.TabIndex = 37;
            this.lblPort.Text = "Port";
            // 
            // lblSMTP
            // 
            this.lblSMTP.Location = new System.Drawing.Point(6, 13);
            this.lblSMTP.Name = "lblSMTP";
            this.lblSMTP.Size = new System.Drawing.Size(37, 16);
            this.lblSMTP.TabIndex = 36;
            this.lblSMTP.Text = "SMTP";
            // 
            // rdoSMTP
            // 
            this.rdoSMTP.AutoSize = true;
            this.rdoSMTP.Location = new System.Drawing.Point(11, 3);
            this.rdoSMTP.Name = "rdoSMTP";
            this.rdoSMTP.Size = new System.Drawing.Size(55, 17);
            this.rdoSMTP.TabIndex = 41;
            this.rdoSMTP.TabStop = true;
            this.rdoSMTP.Text = "SMTP";
            this.ttpPreference.SetToolTip(this.rdoSMTP, "Use SMTP protocol");
            this.rdoSMTP.UseVisualStyleBackColor = true;
            this.rdoSMTP.Click += new System.EventHandler(this.rdoSMTP_Click);
            // 
            // rdoNotesClient
            // 
            this.rdoNotesClient.AutoSize = true;
            this.rdoNotesClient.Location = new System.Drawing.Point(74, 3);
            this.rdoNotesClient.Name = "rdoNotesClient";
            this.rdoNotesClient.Size = new System.Drawing.Size(82, 17);
            this.rdoNotesClient.TabIndex = 42;
            this.rdoNotesClient.TabStop = true;
            this.rdoNotesClient.Text = "Notes Client";
            this.ttpPreference.SetToolTip(this.rdoNotesClient, "Use Notes Client COM object");
            this.rdoNotesClient.UseVisualStyleBackColor = true;
            this.rdoNotesClient.Click += new System.EventHandler(this.rdoNotesClient_Click);
            // 
            // gbxSMTP
            // 
            this.gbxSMTP.Controls.Add(this.cboSMTP);
            this.gbxSMTP.Controls.Add(this.lblSMTP);
            this.gbxSMTP.Controls.Add(this.lblPort);
            this.gbxSMTP.Controls.Add(this.btnTest);
            this.gbxSMTP.Controls.Add(this.cboPort);
            this.gbxSMTP.Location = new System.Drawing.Point(1, 18);
            this.gbxSMTP.Name = "gbxSMTP";
            this.gbxSMTP.Size = new System.Drawing.Size(342, 36);
            this.gbxSMTP.TabIndex = 43;
            this.gbxSMTP.TabStop = false;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(123, 76);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(47, 22);
            this.btnOK.TabIndex = 44;
            this.btnOK.Text = "OK";
            this.ttpPreference.SetToolTip(this.btnOK, "Save the Setting");
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(174, 76);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(47, 22);
            this.btnCancel.TabIndex = 45;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // MailPreference
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(344, 105);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.gbxSMTP);
            this.Controls.Add(this.rdoNotesClient);
            this.Controls.Add(this.rdoSMTP);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MailPreference";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Mail Preference";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.MailPreference_Load);
            this.gbxSMTP.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.ComboBox cboPort;
        private System.Windows.Forms.ComboBox cboSMTP;
        private System.Windows.Forms.Label lblPort;
        private System.Windows.Forms.Label lblSMTP;
        private System.Windows.Forms.RadioButton rdoSMTP;
        private System.Windows.Forms.RadioButton rdoNotesClient;
        private System.Windows.Forms.GroupBox gbxSMTP;
        private System.Windows.Forms.ToolTip ttpPreference;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
    }
}