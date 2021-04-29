namespace LiveCapture
{
    partial class UcNotesClient
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
            if (ncMailThread != null && ncMailThread.IsAlive)
            {
                this.KillNotesClientMailThread();
                commObj.LogToFile("Thread.log", "++ KillNotesClientMailThread Killed ++");
            }

            if(m_thdCleanUp != null && m_thdCleanUp.IsAlive)
            {
                this.KillNotesCleanUpThread();
                commObj.LogToFile( "Thread.log", "++ KillNotesCleanUpThread Killed ++" );
            }

            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnAbort = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdoFile = new System.Windows.Forms.RadioButton();
            this.lnkFile = new System.Windows.Forms.LinkLabel();
            this.txtFile = new System.Windows.Forms.TextBox();
            this.chkAttach = new System.Windows.Forms.CheckBox();
            this.txtFolder = new System.Windows.Forms.TextBox();
            this.lnkFolder = new System.Windows.Forms.LinkLabel();
            this.cboCC = new System.Windows.Forms.ComboBox();
            this.chkGUID = new System.Windows.Forms.CheckBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.nudLoop = new System.Windows.Forms.NumericUpDown();
            this.richBox = new System.Windows.Forms.RichTextBox();
            this.lblLoop = new System.Windows.Forms.Label();
            this.lblSubject = new System.Windows.Forms.Label();
            this.txtSubject = new System.Windows.Forms.TextBox();
            this.lnkBCC = new System.Windows.Forms.LinkLabel();
            this.lnkCC = new System.Windows.Forms.LinkLabel();
            this.lblDelay = new System.Windows.Forms.Label();
            this.cboTo = new System.Windows.Forms.ComboBox();
            this.lnkTo = new System.Windows.Forms.LinkLabel();
            this.nudDelay = new System.Windows.Forms.NumericUpDown();
            this.cboBCC = new System.Windows.Forms.ComboBox();
            this.ttipOLPage = new System.Windows.Forms.ToolTip( this.components );
            this.rdoUI = new System.Windows.Forms.RadioButton();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtAttach = new System.Windows.Forms.TextBox();
            this.chkMultiAttach = new System.Windows.Forms.CheckBox();
            this.lnkAttach = new System.Windows.Forms.LinkLabel();
            this.cboMemo = new System.Windows.Forms.ComboBox();
            this.chkUseProfile = new System.Windows.Forms.CheckBox();
            this.lblServer = new System.Windows.Forms.Label();
            this.lblSenderNsf = new System.Windows.Forms.Label();
            this.cboServer = new System.Windows.Forms.ComboBox();
            this.cboSenderDB = new System.Windows.Forms.ComboBox();
            this.txtPwd = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.cboClnUpServer = new System.Windows.Forms.ComboBox();
            this.lblClnUpServer = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblMemo = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnClnUpAbort = new System.Windows.Forms.Button();
            this.cboInNsfFile = new System.Windows.Forms.ComboBox();
            this.btnCleanUp = new System.Windows.Forms.Button();
            this.rdoCleanUp = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudLoop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDelay)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAbort
            // 
            this.btnAbort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAbort.Location = new System.Drawing.Point( 591, 133 );
            this.btnAbort.Name = "btnAbort";
            this.btnAbort.Size = new System.Drawing.Size( 64, 21 );
            this.btnAbort.TabIndex = 117;
            this.btnAbort.Text = "Abort";
            this.btnAbort.Click += new System.EventHandler( this.btnAbort_Click );
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add( this.rdoFile );
            this.groupBox1.Controls.Add( this.lnkFile );
            this.groupBox1.Controls.Add( this.txtFile );
            this.groupBox1.Controls.Add( this.chkAttach );
            this.groupBox1.Controls.Add( this.txtFolder );
            this.groupBox1.Controls.Add( this.lnkFolder );
            this.groupBox1.Location = new System.Drawing.Point( 351, 1 );
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size( 308, 58 );
            this.groupBox1.TabIndex = 111;
            this.groupBox1.TabStop = false;
            // 
            // rdoFile
            // 
            this.rdoFile.AutoCheck = false;
            this.rdoFile.Location = new System.Drawing.Point( 5, 14 );
            this.rdoFile.Name = "rdoFile";
            this.rdoFile.Size = new System.Drawing.Size( 16, 16 );
            this.rdoFile.TabIndex = 97;
            this.ttipOLPage.SetToolTip( this.rdoFile, "Automate from address file" );
            this.rdoFile.Click += new System.EventHandler( this.rdoFile_Click );
            // 
            // lnkFile
            // 
            this.lnkFile.Enabled = false;
            this.lnkFile.Font = new System.Drawing.Font( "Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)) );
            this.lnkFile.Location = new System.Drawing.Point( 22, 14 );
            this.lnkFile.Name = "lnkFile";
            this.lnkFile.Size = new System.Drawing.Size( 28, 16 );
            this.lnkFile.TabIndex = 51;
            this.lnkFile.TabStop = true;
            this.lnkFile.Text = "File :";
            this.lnkFile.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ttipOLPage.SetToolTip( this.lnkFile, "Browse the address file" );
            this.lnkFile.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler( this.lnkFile_LinkClicked );
            // 
            // txtFile
            // 
            this.txtFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFile.Enabled = false;
            this.txtFile.Location = new System.Drawing.Point( 52, 11 );
            this.txtFile.Name = "txtFile";
            this.txtFile.Size = new System.Drawing.Size( 249, 20 );
            this.txtFile.TabIndex = 0;
            this.txtFile.Text = "load address from file";
            // 
            // chkAttach
            // 
            this.chkAttach.Enabled = false;
            this.chkAttach.Location = new System.Drawing.Point( 5, 36 );
            this.chkAttach.Name = "chkAttach";
            this.chkAttach.Size = new System.Drawing.Size( 57, 16 );
            this.chkAttach.TabIndex = 99;
            this.chkAttach.Text = "Attach";
            this.ttipOLPage.SetToolTip( this.chkAttach, "Include Attachment" );
            this.chkAttach.Click += new System.EventHandler( this.chkAttach_Click );
            // 
            // txtFolder
            // 
            this.txtFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFolder.Enabled = false;
            this.txtFolder.Location = new System.Drawing.Point( 102, 34 );
            this.txtFolder.Name = "txtFolder";
            this.txtFolder.Size = new System.Drawing.Size( 198, 20 );
            this.txtFolder.TabIndex = 1;
            this.ttipOLPage.SetToolTip( this.txtFolder, "Attachment folder ONLY" );
            // 
            // lnkFolder
            // 
            this.lnkFolder.Enabled = false;
            this.lnkFolder.Font = new System.Drawing.Font( "Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)) );
            this.lnkFolder.Location = new System.Drawing.Point( 58, 35 );
            this.lnkFolder.Name = "lnkFolder";
            this.lnkFolder.Size = new System.Drawing.Size( 42, 16 );
            this.lnkFolder.TabIndex = 61;
            this.lnkFolder.TabStop = true;
            this.lnkFolder.Text = "Folder";
            this.lnkFolder.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ttipOLPage.SetToolTip( this.lnkFolder, "Locate the attachement folder" );
            this.lnkFolder.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler( this.lnkFolder_LinkClicked );
            // 
            // cboCC
            // 
            this.cboCC.Location = new System.Drawing.Point( 66, 57 );
            this.cboCC.Name = "cboCC";
            this.cboCC.Size = new System.Drawing.Size( 265, 21 );
            this.cboCC.TabIndex = 3;
            this.ttipOLPage.SetToolTip( this.cboCC, "Hit Enter to save current value" );
            this.cboCC.KeyPress += new System.Windows.Forms.KeyPressEventHandler( this.cboCC_KeyPress );
            this.cboCC.KeyDown += new System.Windows.Forms.KeyEventHandler( this.cboCC_KeyDown );
            // 
            // chkGUID
            // 
            this.chkGUID.Location = new System.Drawing.Point( 356, 91 );
            this.chkGUID.Name = "chkGUID";
            this.chkGUID.Size = new System.Drawing.Size( 92, 17 );
            this.chkGUID.TabIndex = 108;
            this.chkGUID.Text = "Include GUID";
            this.ttipOLPage.SetToolTip( this.chkGUID, "include GUID" );
            // 
            // btnSend
            // 
            this.btnSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSend.Location = new System.Drawing.Point( 591, 111 );
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size( 64, 21 );
            this.btnSend.TabIndex = 5;
            this.btnSend.Text = "Send";
            this.ttipOLPage.SetToolTip( this.btnSend, "Send mail via native mail client" );
            this.btnSend.Click += new System.EventHandler( this.btnSend_Click );
            // 
            // nudLoop
            // 
            this.nudLoop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nudLoop.Location = new System.Drawing.Point( 591, 88 );
            this.nudLoop.Maximum = new decimal( new int[] {
            999999,
            0,
            0,
            0} );
            this.nudLoop.Minimum = new decimal( new int[] {
            1,
            0,
            0,
            0} );
            this.nudLoop.Name = "nudLoop";
            this.nudLoop.Size = new System.Drawing.Size( 64, 20 );
            this.nudLoop.TabIndex = 2;
            this.nudLoop.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ttipOLPage.SetToolTip( this.nudLoop, "0..999999" );
            this.nudLoop.Value = new decimal( new int[] {
            1,
            0,
            0,
            0} );
            // 
            // richBox
            // 
            this.richBox.Location = new System.Drawing.Point( 352, 192 );
            this.richBox.Name = "richBox";
            this.richBox.Size = new System.Drawing.Size( 303, 42 );
            this.richBox.TabIndex = 109;
            this.richBox.Text = "richBox";
            // 
            // lblLoop
            // 
            this.lblLoop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblLoop.Font = new System.Drawing.Font( "Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)) );
            this.lblLoop.Location = new System.Drawing.Point( 559, 92 );
            this.lblLoop.Name = "lblLoop";
            this.lblLoop.Size = new System.Drawing.Size( 32, 16 );
            this.lblLoop.TabIndex = 105;
            this.lblLoop.Text = "Loop";
            // 
            // lblSubject
            // 
            this.lblSubject.Font = new System.Drawing.Font( "Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)) );
            this.lblSubject.Location = new System.Drawing.Point( 347, 64 );
            this.lblSubject.Name = "lblSubject";
            this.lblSubject.Size = new System.Drawing.Size( 56, 16 );
            this.lblSubject.TabIndex = 104;
            this.lblSubject.Text = "Subject";
            this.lblSubject.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtSubject
            // 
            this.txtSubject.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSubject.Location = new System.Drawing.Point( 403, 63 );
            this.txtSubject.Name = "txtSubject";
            this.txtSubject.Size = new System.Drawing.Size( 251, 20 );
            this.txtSubject.TabIndex = 0;
            this.txtSubject.Text = "TC";
            // 
            // lnkBCC
            // 
            this.lnkBCC.Font = new System.Drawing.Font( "Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)) );
            this.lnkBCC.Location = new System.Drawing.Point( 29, 83 );
            this.lnkBCC.Name = "lnkBCC";
            this.lnkBCC.Size = new System.Drawing.Size( 36, 20 );
            this.lnkBCC.TabIndex = 102;
            this.lnkBCC.TabStop = true;
            this.lnkBCC.Text = "BCC";
            this.lnkBCC.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lnkCC
            // 
            this.lnkCC.Font = new System.Drawing.Font( "Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)) );
            this.lnkCC.Location = new System.Drawing.Point( 37, 59 );
            this.lnkCC.Name = "lnkCC";
            this.lnkCC.Size = new System.Drawing.Size( 28, 16 );
            this.lnkCC.TabIndex = 101;
            this.lnkCC.TabStop = true;
            this.lnkCC.Text = "CC";
            this.lnkCC.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDelay
            // 
            this.lblDelay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDelay.Font = new System.Drawing.Font( "Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)) );
            this.lblDelay.Location = new System.Drawing.Point( 466, 92 );
            this.lblDelay.Name = "lblDelay";
            this.lblDelay.Size = new System.Drawing.Size( 36, 16 );
            this.lblDelay.TabIndex = 113;
            this.lblDelay.Text = "Delay";
            this.ttipOLPage.SetToolTip( this.lblDelay, "in Sec (0..5)" );
            // 
            // cboTo
            // 
            this.cboTo.Items.AddRange( new object[] {
            ""} );
            this.cboTo.Location = new System.Drawing.Point( 66, 33 );
            this.cboTo.Name = "cboTo";
            this.cboTo.Size = new System.Drawing.Size( 265, 21 );
            this.cboTo.TabIndex = 2;
            this.cboTo.Text = "administrator@team.zantaz.com";
            this.ttipOLPage.SetToolTip( this.cboTo, "Hit Enter to save current value" );
            this.cboTo.KeyPress += new System.Windows.Forms.KeyPressEventHandler( this.cboTo_KeyPress );
            this.cboTo.KeyDown += new System.Windows.Forms.KeyEventHandler( this.cboTo_KeyDown );
            // 
            // lnkTo
            // 
            this.lnkTo.Font = new System.Drawing.Font( "Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)) );
            this.lnkTo.Location = new System.Drawing.Point( 37, 35 );
            this.lnkTo.Name = "lnkTo";
            this.lnkTo.Size = new System.Drawing.Size( 28, 16 );
            this.lnkTo.TabIndex = 110;
            this.lnkTo.TabStop = true;
            this.lnkTo.Text = "To";
            this.lnkTo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // nudDelay
            // 
            this.nudDelay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nudDelay.Location = new System.Drawing.Point( 502, 88 );
            this.nudDelay.Maximum = new decimal( new int[] {
            1000,
            0,
            0,
            0} );
            this.nudDelay.Name = "nudDelay";
            this.nudDelay.Size = new System.Drawing.Size( 54, 20 );
            this.nudDelay.TabIndex = 1;
            this.nudDelay.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ttipOLPage.SetToolTip( this.nudDelay, "Per sec Per message" );
            // 
            // cboBCC
            // 
            this.cboBCC.Location = new System.Drawing.Point( 66, 81 );
            this.cboBCC.Name = "cboBCC";
            this.cboBCC.Size = new System.Drawing.Size( 265, 21 );
            this.cboBCC.TabIndex = 4;
            this.ttipOLPage.SetToolTip( this.cboBCC, "Hit Enter to save current value" );
            this.cboBCC.KeyPress += new System.Windows.Forms.KeyPressEventHandler( this.cboBCC_KeyPress );
            this.cboBCC.KeyDown += new System.Windows.Forms.KeyEventHandler( this.cboBCC_KeyDown );
            // 
            // rdoUI
            // 
            this.rdoUI.AutoCheck = false;
            this.rdoUI.Checked = true;
            this.rdoUI.Location = new System.Drawing.Point( 12, 13 );
            this.rdoUI.Name = "rdoUI";
            this.rdoUI.Size = new System.Drawing.Size( 16, 16 );
            this.rdoUI.TabIndex = 0;
            this.rdoUI.TabStop = true;
            this.ttipOLPage.SetToolTip( this.rdoUI, "Send individual mail" );
            this.rdoUI.Click += new System.EventHandler( this.rdoUI_Click );
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point( 201, 11 );
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '+';
            this.txtPassword.Size = new System.Drawing.Size( 130, 20 );
            this.txtPassword.TabIndex = 1;
            this.txtPassword.Text = "password0";
            this.ttipOLPage.SetToolTip( this.txtPassword, "password" );
            // 
            // txtAttach
            // 
            this.txtAttach.Location = new System.Drawing.Point( 66, 105 );
            this.txtAttach.Name = "txtAttach";
            this.txtAttach.Size = new System.Drawing.Size( 245, 20 );
            this.txtAttach.TabIndex = 5;
            this.ttipOLPage.SetToolTip( this.txtAttach, "Attachment - file name" );
            // 
            // chkMultiAttach
            // 
            this.chkMultiAttach.CheckAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.chkMultiAttach.Location = new System.Drawing.Point( 315, 107 );
            this.chkMultiAttach.Name = "chkMultiAttach";
            this.chkMultiAttach.Size = new System.Drawing.Size( 16, 16 );
            this.chkMultiAttach.TabIndex = 75;
            this.chkMultiAttach.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.ttipOLPage.SetToolTip( this.chkMultiAttach, "Include multiple Attachment" );
            // 
            // lnkAttach
            // 
            this.lnkAttach.Font = new System.Drawing.Font( "Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)) );
            this.lnkAttach.Location = new System.Drawing.Point( 2, 107 );
            this.lnkAttach.Name = "lnkAttach";
            this.lnkAttach.Size = new System.Drawing.Size( 61, 16 );
            this.lnkAttach.TabIndex = 75;
            this.lnkAttach.TabStop = true;
            this.lnkAttach.Text = "Attachment";
            this.lnkAttach.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ttipOLPage.SetToolTip( this.lnkAttach, "Browse attachment file" );
            this.lnkAttach.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler( this.lnkAttach_LinkClicked );
            // 
            // cboMemo
            // 
            this.cboMemo.Location = new System.Drawing.Point( 66, 10 );
            this.cboMemo.Name = "cboMemo";
            this.cboMemo.Size = new System.Drawing.Size( 129, 21 );
            this.cboMemo.TabIndex = 0;
            this.cboMemo.Text = "memo";
            this.ttipOLPage.SetToolTip( this.cboMemo, "nsf name" );
            this.cboMemo.KeyPress += new System.Windows.Forms.KeyPressEventHandler( this.cboMemo_KeyPress );
            this.cboMemo.KeyDown += new System.Windows.Forms.KeyEventHandler( this.cboMemo_KeyDown );
            // 
            // chkUseProfile
            // 
            this.chkUseProfile.Checked = true;
            this.chkUseProfile.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkUseProfile.Location = new System.Drawing.Point( 356, 158 );
            this.chkUseProfile.Name = "chkUseProfile";
            this.chkUseProfile.Size = new System.Drawing.Size( 157, 18 );
            this.chkUseProfile.TabIndex = 119;
            this.chkUseProfile.Text = "Send mail using local profile";
            this.ttipOLPage.SetToolTip( this.chkUseProfile, "Notes Client must be installed on this local machine" );
            this.chkUseProfile.UseVisualStyleBackColor = true;
            this.chkUseProfile.Click += new System.EventHandler( this.chkUseProfile_Click );
            // 
            // lblServer
            // 
            this.lblServer.Enabled = false;
            this.lblServer.Location = new System.Drawing.Point( 348, 115 );
            this.lblServer.Name = "lblServer";
            this.lblServer.Size = new System.Drawing.Size( 39, 15 );
            this.lblServer.TabIndex = 120;
            this.lblServer.Text = "Server";
            this.ttipOLPage.SetToolTip( this.lblServer, "Server IP or Name that holds the sender mailbox" );
            // 
            // lblSenderNsf
            // 
            this.lblSenderNsf.Enabled = false;
            this.lblSenderNsf.Location = new System.Drawing.Point( 346, 136 );
            this.lblSenderNsf.Name = "lblSenderNsf";
            this.lblSenderNsf.Size = new System.Drawing.Size( 41, 15 );
            this.lblSenderNsf.TabIndex = 121;
            this.lblSenderNsf.Text = "Sender";
            this.lblSenderNsf.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ttipOLPage.SetToolTip( this.lblSenderNsf, "Sender NSF" );
            // 
            // cboServer
            // 
            this.cboServer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cboServer.Enabled = false;
            this.cboServer.Location = new System.Drawing.Point( 389, 111 );
            this.cboServer.Name = "cboServer";
            this.cboServer.Size = new System.Drawing.Size( 198, 21 );
            this.cboServer.TabIndex = 3;
            this.ttipOLPage.SetToolTip( this.cboServer, "Server IP or Name that holds the sender mailbox\r\nHit Enter to save current value" );
            this.cboServer.KeyPress += new System.Windows.Forms.KeyPressEventHandler( this.cboServer_KeyPress );
            this.cboServer.KeyDown += new System.Windows.Forms.KeyEventHandler( this.cboServer_KeyDown );
            // 
            // cboSenderDB
            // 
            this.cboSenderDB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cboSenderDB.Enabled = false;
            this.cboSenderDB.Location = new System.Drawing.Point( 389, 134 );
            this.cboSenderDB.Name = "cboSenderDB";
            this.cboSenderDB.Size = new System.Drawing.Size( 198, 21 );
            this.cboSenderDB.TabIndex = 4;
            this.ttipOLPage.SetToolTip( this.cboSenderDB, "Sender NSF\r\nHit Enter to save current value" );
            this.cboSenderDB.KeyPress += new System.Windows.Forms.KeyPressEventHandler( this.cboSenderDB_KeyPress );
            this.cboSenderDB.KeyDown += new System.Windows.Forms.KeyEventHandler( this.cboSenderDB_KeyDown );
            // 
            // txtPwd
            // 
            this.txtPwd.Enabled = false;
            this.txtPwd.Location = new System.Drawing.Point( 254, 10 );
            this.txtPwd.Name = "txtPwd";
            this.txtPwd.PasswordChar = '+';
            this.txtPwd.Size = new System.Drawing.Size( 80, 20 );
            this.txtPwd.TabIndex = 12;
            this.txtPwd.Text = "password0";
            this.ttipOLPage.SetToolTip( this.txtPwd, "Password for local profile that able to access remote nsf" );
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Enabled = false;
            this.lblPassword.Location = new System.Drawing.Point( 198, 14 );
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size( 53, 13 );
            this.lblPassword.TabIndex = 11;
            this.lblPassword.Text = "Password";
            this.ttipOLPage.SetToolTip( this.lblPassword, "Password for local profile that able to access remote nsf" );
            // 
            // cboClnUpServer
            // 
            this.cboClnUpServer.Enabled = false;
            this.cboClnUpServer.Location = new System.Drawing.Point( 81, 33 );
            this.cboClnUpServer.Name = "cboClnUpServer";
            this.cboClnUpServer.Size = new System.Drawing.Size( 114, 21 );
            this.cboClnUpServer.TabIndex = 121;
            this.ttipOLPage.SetToolTip( this.cboClnUpServer, "Server IP or Name that holds the sender mailbox\r\nHit Enter to save current value" );
            // 
            // lblClnUpServer
            // 
            this.lblClnUpServer.Enabled = false;
            this.lblClnUpServer.Location = new System.Drawing.Point( 40, 37 );
            this.lblClnUpServer.Name = "lblClnUpServer";
            this.lblClnUpServer.Size = new System.Drawing.Size( 39, 15 );
            this.lblClnUpServer.TabIndex = 122;
            this.lblClnUpServer.Text = "Server";
            this.ttipOLPage.SetToolTip( this.lblClnUpServer, "Server IP or Name that holds the sender mailbox" );
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add( this.cboMemo );
            this.groupBox2.Controls.Add( this.rdoUI );
            this.groupBox2.Controls.Add( this.txtPassword );
            this.groupBox2.Controls.Add( this.lblMemo );
            this.groupBox2.Controls.Add( this.cboCC );
            this.groupBox2.Controls.Add( this.txtAttach );
            this.groupBox2.Controls.Add( this.chkMultiAttach );
            this.groupBox2.Controls.Add( this.lnkAttach );
            this.groupBox2.Controls.Add( this.cboTo );
            this.groupBox2.Controls.Add( this.cboBCC );
            this.groupBox2.Controls.Add( this.lnkTo );
            this.groupBox2.Controls.Add( this.lnkCC );
            this.groupBox2.Controls.Add( this.lnkBCC );
            this.groupBox2.Location = new System.Drawing.Point( 4, 1 );
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size( 337, 129 );
            this.groupBox2.TabIndex = 118;
            this.groupBox2.TabStop = false;
            // 
            // lblMemo
            // 
            this.lblMemo.AutoSize = true;
            this.lblMemo.Location = new System.Drawing.Point( 29, 13 );
            this.lblMemo.Name = "lblMemo";
            this.lblMemo.Size = new System.Drawing.Size( 36, 13 );
            this.lblMemo.TabIndex = 120;
            this.lblMemo.Text = "Memo";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add( this.btnClnUpAbort );
            this.groupBox3.Controls.Add( this.cboClnUpServer );
            this.groupBox3.Controls.Add( this.lblClnUpServer );
            this.groupBox3.Controls.Add( this.txtPwd );
            this.groupBox3.Controls.Add( this.lblPassword );
            this.groupBox3.Controls.Add( this.cboInNsfFile );
            this.groupBox3.Controls.Add( this.btnCleanUp );
            this.groupBox3.Controls.Add( this.rdoCleanUp );
            this.groupBox3.Location = new System.Drawing.Point( 4, 133 );
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size( 337, 60 );
            this.groupBox3.TabIndex = 124;
            this.groupBox3.TabStop = false;
            // 
            // btnClnUpAbort
            // 
            this.btnClnUpAbort.Location = new System.Drawing.Point( 269, 33 );
            this.btnClnUpAbort.Name = "btnClnUpAbort";
            this.btnClnUpAbort.Size = new System.Drawing.Size( 64, 21 );
            this.btnClnUpAbort.TabIndex = 123;
            this.btnClnUpAbort.Text = "Abort";
            this.btnClnUpAbort.Click += new System.EventHandler( this.btnClnUpAbort_Click );
            // 
            // cboInNsfFile
            // 
            this.cboInNsfFile.Enabled = false;
            this.cboInNsfFile.FormattingEnabled = true;
            this.cboInNsfFile.Location = new System.Drawing.Point( 81, 10 );
            this.cboInNsfFile.Name = "cboInNsfFile";
            this.cboInNsfFile.Size = new System.Drawing.Size( 114, 21 );
            this.cboInNsfFile.TabIndex = 10;
            // 
            // btnCleanUp
            // 
            this.btnCleanUp.Enabled = false;
            this.btnCleanUp.Location = new System.Drawing.Point( 201, 32 );
            this.btnCleanUp.Name = "btnCleanUp";
            this.btnCleanUp.Size = new System.Drawing.Size( 62, 21 );
            this.btnCleanUp.TabIndex = 1;
            this.btnCleanUp.Text = "Clean Up";
            this.btnCleanUp.UseVisualStyleBackColor = true;
            this.btnCleanUp.Click += new System.EventHandler( this.btnCleanUp_Click );
            // 
            // rdoCleanUp
            // 
            this.rdoCleanUp.AutoCheck = false;
            this.rdoCleanUp.AutoSize = true;
            this.rdoCleanUp.Location = new System.Drawing.Point( 5, 12 );
            this.rdoCleanUp.Name = "rdoCleanUp";
            this.rdoCleanUp.Size = new System.Drawing.Size( 76, 17 );
            this.rdoCleanUp.TabIndex = 0;
            this.rdoCleanUp.TabStop = true;
            this.rdoCleanUp.Text = "Clean NSF";
            this.rdoCleanUp.UseVisualStyleBackColor = true;
            this.rdoCleanUp.Click += new System.EventHandler( this.rdoCleanUp_Click );
            // 
            // UcNotesClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add( this.groupBox3 );
            this.Controls.Add( this.cboSenderDB );
            this.Controls.Add( this.cboServer );
            this.Controls.Add( this.lblSenderNsf );
            this.Controls.Add( this.lblServer );
            this.Controls.Add( this.chkUseProfile );
            this.Controls.Add( this.groupBox2 );
            this.Controls.Add( this.btnAbort );
            this.Controls.Add( this.groupBox1 );
            this.Controls.Add( this.chkGUID );
            this.Controls.Add( this.btnSend );
            this.Controls.Add( this.nudLoop );
            this.Controls.Add( this.richBox );
            this.Controls.Add( this.lblLoop );
            this.Controls.Add( this.lblSubject );
            this.Controls.Add( this.txtSubject );
            this.Controls.Add( this.lblDelay );
            this.Controls.Add( this.nudDelay );
            this.Name = "UcNotesClient";
            this.Size = new System.Drawing.Size( 662, 383 );
            this.Load += new System.EventHandler( this.UcNotesClient_Load );
            this.groupBox1.ResumeLayout( false );
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudLoop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDelay)).EndInit();
            this.groupBox2.ResumeLayout( false );
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout( false );
            this.groupBox3.PerformLayout();
            this.ResumeLayout( false );
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolTip ttipOLPage;
        private System.Windows.Forms.Button btnAbort;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rdoFile;
        private System.Windows.Forms.LinkLabel lnkFile;
        private System.Windows.Forms.TextBox txtFile;
        private System.Windows.Forms.CheckBox chkAttach;
        private System.Windows.Forms.TextBox txtFolder;
        private System.Windows.Forms.LinkLabel lnkFolder;
        private System.Windows.Forms.ComboBox cboCC;
        private System.Windows.Forms.CheckBox chkGUID;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.NumericUpDown nudLoop;
        private System.Windows.Forms.RichTextBox richBox;
        private System.Windows.Forms.Label lblLoop;
        private System.Windows.Forms.Label lblSubject;
        private System.Windows.Forms.TextBox txtSubject;
        private System.Windows.Forms.LinkLabel lnkBCC;
        private System.Windows.Forms.LinkLabel lnkCC;
        private System.Windows.Forms.Label lblDelay;
        private System.Windows.Forms.ComboBox cboTo;
        private System.Windows.Forms.LinkLabel lnkTo;
        private System.Windows.Forms.NumericUpDown nudDelay;
        private System.Windows.Forms.ComboBox cboBCC;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rdoUI;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtAttach;
        private System.Windows.Forms.CheckBox chkMultiAttach;
        private System.Windows.Forms.LinkLabel lnkAttach;
        private System.Windows.Forms.Label lblMemo;
        private System.Windows.Forms.ComboBox cboMemo;
        private System.Windows.Forms.CheckBox chkUseProfile;
        private System.Windows.Forms.Label lblServer;
        private System.Windows.Forms.Label lblSenderNsf;
        private System.Windows.Forms.ComboBox cboServer;
        private System.Windows.Forms.ComboBox cboSenderDB;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton rdoCleanUp;
        private System.Windows.Forms.Button btnCleanUp;
        private System.Windows.Forms.TextBox txtPwd;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.ComboBox cboInNsfFile;
        private System.Windows.Forms.ComboBox cboClnUpServer;
        private System.Windows.Forms.Label lblClnUpServer;
        private System.Windows.Forms.Button btnClnUpAbort;
    }
}
