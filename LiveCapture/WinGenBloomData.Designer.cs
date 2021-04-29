namespace LiveCapture
{
    partial class WinGenBloomData
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
            if(disposing && (components != null))
            {

                if(m_thdGenData != null && m_thdGenData.IsAlive)
                {
                    this.KillGenDataThread();
                    commObj.LogToFile( "Thread.log", "   KillGenDataThread Killed" );
                }

                components.Dispose();
            }
            base.Dispose( disposing );
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lblDocType = new System.Windows.Forms.Label();
            this.lblTypeVer = new System.Windows.Forms.Label();
            this.ttpBData = new System.Windows.Forms.ToolTip( this.components );
            this.cboDocType = new System.Windows.Forms.ComboBox();
            this.lnkSampleXml = new System.Windows.Forms.LinkLabel();
            this.txtXmlFile = new System.Windows.Forms.TextBox();
            this.lnkOutputXml = new System.Windows.Forms.LinkLabel();
            this.txtOutputXml = new System.Windows.Forms.TextBox();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.btnAbort = new System.Windows.Forms.Button();
            this.lnkInXsd = new System.Windows.Forms.LinkLabel();
            this.lnkOutXml = new System.Windows.Forms.LinkLabel();
            this.txtInXsd = new System.Windows.Forms.TextBox();
            this.txtOutXmlTemplate = new System.Windows.Forms.TextBox();
            this.btnCreate = new System.Windows.Forms.Button();
            this.btnHelp = new System.Windows.Forms.Button();
            this.btnDone = new System.Windows.Forms.Button();
            this.cboTypeVer = new System.Windows.Forms.ComboBox();
            this.numMsg = new System.Windows.Forms.NumericUpDown();
            this.lblNumMsg = new System.Windows.Forms.Label();
            this.txtDisplay = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.numMsg)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblDocType
            // 
            this.lblDocType.AutoSize = true;
            this.lblDocType.Location = new System.Drawing.Point( 12, 8 );
            this.lblDocType.Name = "lblDocType";
            this.lblDocType.Size = new System.Drawing.Size( 54, 13 );
            this.lblDocType.TabIndex = 0;
            this.lblDocType.Text = "Doc Type";
            // 
            // lblTypeVer
            // 
            this.lblTypeVer.AutoSize = true;
            this.lblTypeVer.Location = new System.Drawing.Point( 189, 8 );
            this.lblTypeVer.Name = "lblTypeVer";
            this.lblTypeVer.Size = new System.Drawing.Size( 69, 13 );
            this.lblTypeVer.TabIndex = 1;
            this.lblTypeVer.Text = "Type Version";
            // 
            // cboDocType
            // 
            this.cboDocType.FormattingEnabled = true;
            this.cboDocType.Items.AddRange( new object[] {
            "BLOGXML",
            "IBXML",
            "MSGXML"} );
            this.cboDocType.Location = new System.Drawing.Point( 69, 4 );
            this.cboDocType.Name = "cboDocType";
            this.cboDocType.Size = new System.Drawing.Size( 121, 21 );
            this.cboDocType.Sorted = true;
            this.cboDocType.TabIndex = 2;
            this.ttpBData.SetToolTip( this.cboDocType, "Bloomber Document Type" );
            this.cboDocType.SelectedIndexChanged += new System.EventHandler( this.cboDocType_SelectedIndexChanged );
            // 
            // lnkSampleXml
            // 
            this.lnkSampleXml.AutoSize = true;
            this.lnkSampleXml.Location = new System.Drawing.Point( 2, 33 );
            this.lnkSampleXml.Name = "lnkSampleXml";
            this.lnkSampleXml.Size = new System.Drawing.Size( 67, 13 );
            this.lnkSampleXml.TabIndex = 4;
            this.lnkSampleXml.TabStop = true;
            this.lnkSampleXml.Text = "Sample XML";
            this.ttpBData.SetToolTip( this.lnkSampleXml, "Browse the sample XML file" );
            this.lnkSampleXml.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler( this.lnkSampleXml_LinkClicked );
            // 
            // txtXmlFile
            // 
            this.txtXmlFile.Location = new System.Drawing.Point( 69, 30 );
            this.txtXmlFile.Name = "txtXmlFile";
            this.txtXmlFile.Size = new System.Drawing.Size( 313, 20 );
            this.txtXmlFile.TabIndex = 5;
            this.ttpBData.SetToolTip( this.txtXmlFile, "Input XML File" );
            // 
            // lnkOutputXml
            // 
            this.lnkOutputXml.AutoSize = true;
            this.lnkOutputXml.Location = new System.Drawing.Point( 5, 56 );
            this.lnkOutputXml.Name = "lnkOutputXml";
            this.lnkOutputXml.Size = new System.Drawing.Size( 64, 13 );
            this.lnkOutputXml.TabIndex = 6;
            this.lnkOutputXml.TabStop = true;
            this.lnkOutputXml.Text = "Output XML";
            this.ttpBData.SetToolTip( this.lnkOutputXml, "Browse the output XML location" );
            this.lnkOutputXml.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler( this.lnkOutputXml_LinkClicked );
            // 
            // txtOutputXml
            // 
            this.txtOutputXml.Location = new System.Drawing.Point( 69, 53 );
            this.txtOutputXml.Name = "txtOutputXml";
            this.txtOutputXml.Size = new System.Drawing.Size( 313, 20 );
            this.txtOutputXml.TabIndex = 7;
            this.ttpBData.SetToolTip( this.txtOutputXml, "Point to output folder" );
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point( 388, 28 );
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size( 70, 23 );
            this.btnGenerate.TabIndex = 10;
            this.btnGenerate.Text = "Generate";
            this.ttpBData.SetToolTip( this.btnGenerate, "Let do it" );
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler( this.btnGenerate_Click );
            // 
            // btnAbort
            // 
            this.btnAbort.Location = new System.Drawing.Point( 388, 52 );
            this.btnAbort.Name = "btnAbort";
            this.btnAbort.Size = new System.Drawing.Size( 70, 23 );
            this.btnAbort.TabIndex = 11;
            this.btnAbort.Text = "Abort";
            this.ttpBData.SetToolTip( this.btnAbort, "Stop creating the data" );
            this.btnAbort.UseVisualStyleBackColor = true;
            this.btnAbort.Click += new System.EventHandler( this.btnAbort_Click );
            // 
            // lnkInXsd
            // 
            this.lnkInXsd.AutoSize = true;
            this.lnkInXsd.Location = new System.Drawing.Point( 10, 22 );
            this.lnkInXsd.Name = "lnkInXsd";
            this.lnkInXsd.Size = new System.Drawing.Size( 56, 13 );
            this.lnkInXsd.TabIndex = 5;
            this.lnkInXsd.TabStop = true;
            this.lnkInXsd.Text = "Input XSD";
            this.ttpBData.SetToolTip( this.lnkInXsd, "Browse the sample XML file" );
            this.lnkInXsd.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler( this.lnkInXsd_LinkClicked );
            // 
            // lnkOutXml
            // 
            this.lnkOutXml.AutoSize = true;
            this.lnkOutXml.Location = new System.Drawing.Point( 3, 47 );
            this.lnkOutXml.Name = "lnkOutXml";
            this.lnkOutXml.Size = new System.Drawing.Size( 64, 13 );
            this.lnkOutXml.TabIndex = 6;
            this.lnkOutXml.TabStop = true;
            this.lnkOutXml.Text = "Output XML";
            this.ttpBData.SetToolTip( this.lnkOutXml, "Browse the sample XML file" );
            this.lnkOutXml.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler( this.lnkOutXml_LinkClicked );
            // 
            // txtInXsd
            // 
            this.txtInXsd.Location = new System.Drawing.Point( 68, 18 );
            this.txtInXsd.Name = "txtInXsd";
            this.txtInXsd.Size = new System.Drawing.Size( 313, 20 );
            this.txtInXsd.TabIndex = 8;
            this.ttpBData.SetToolTip( this.txtInXsd, "Point to output folder" );
            // 
            // txtOutXmlTemplate
            // 
            this.txtOutXmlTemplate.Location = new System.Drawing.Point( 67, 44 );
            this.txtOutXmlTemplate.Name = "txtOutXmlTemplate";
            this.txtOutXmlTemplate.Size = new System.Drawing.Size( 313, 20 );
            this.txtOutXmlTemplate.TabIndex = 9;
            this.ttpBData.SetToolTip( this.txtOutXmlTemplate, "Point to output folder" );
            // 
            // btnCreate
            // 
            this.btnCreate.Location = new System.Drawing.Point( 386, 16 );
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size( 70, 23 );
            this.btnCreate.TabIndex = 11;
            this.btnCreate.Text = "Make";
            this.ttpBData.SetToolTip( this.btnCreate, "Create XML template from XSD file" );
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler( this.btnCreate_Click );
            // 
            // btnHelp
            // 
            this.btnHelp.Location = new System.Drawing.Point( 388, 161 );
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size( 70, 23 );
            this.btnHelp.TabIndex = 14;
            this.btnHelp.Text = "Help";
            this.ttpBData.SetToolTip( this.btnHelp, "Launch simple help message" );
            this.btnHelp.UseVisualStyleBackColor = true;
            this.btnHelp.Click += new System.EventHandler( this.btnHelp_Click );
            // 
            // btnDone
            // 
            this.btnDone.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnDone.Location = new System.Drawing.Point( 388, 186 );
            this.btnDone.Name = "btnDone";
            this.btnDone.Size = new System.Drawing.Size( 70, 23 );
            this.btnDone.TabIndex = 15;
            this.btnDone.Text = "Done";
            this.ttpBData.SetToolTip( this.btnDone, "Done - closed the window" );
            this.btnDone.UseVisualStyleBackColor = true;
            // 
            // cboTypeVer
            // 
            this.cboTypeVer.FormattingEnabled = true;
            this.cboTypeVer.Location = new System.Drawing.Point( 261, 4 );
            this.cboTypeVer.Name = "cboTypeVer";
            this.cboTypeVer.Size = new System.Drawing.Size( 74, 21 );
            this.cboTypeVer.TabIndex = 3;
            this.cboTypeVer.SelectedIndexChanged += new System.EventHandler( this.cboTypeVer_SelectedIndexChanged );
            // 
            // numMsg
            // 
            this.numMsg.Location = new System.Drawing.Point( 389, 4 );
            this.numMsg.Maximum = new decimal( new int[] {
            999999,
            0,
            0,
            0} );
            this.numMsg.Minimum = new decimal( new int[] {
            1,
            0,
            0,
            0} );
            this.numMsg.Name = "numMsg";
            this.numMsg.Size = new System.Drawing.Size( 69, 20 );
            this.numMsg.TabIndex = 8;
            this.numMsg.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numMsg.Value = new decimal( new int[] {
            1,
            0,
            0,
            0} );
            // 
            // lblNumMsg
            // 
            this.lblNumMsg.AutoSize = true;
            this.lblNumMsg.Location = new System.Drawing.Point( 336, 8 );
            this.lblNumMsg.Name = "lblNumMsg";
            this.lblNumMsg.Size = new System.Drawing.Size( 52, 13 );
            this.lblNumMsg.TabIndex = 9;
            this.lblNumMsg.Text = "Num Msg";
            // 
            // txtDisplay
            // 
            this.txtDisplay.Location = new System.Drawing.Point( 5, 161 );
            this.txtDisplay.Multiline = true;
            this.txtDisplay.Name = "txtDisplay";
            this.txtDisplay.Size = new System.Drawing.Size( 379, 59 );
            this.txtDisplay.TabIndex = 12;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add( this.btnCreate );
            this.groupBox1.Controls.Add( this.txtOutXmlTemplate );
            this.groupBox1.Controls.Add( this.txtInXsd );
            this.groupBox1.Controls.Add( this.lnkOutXml );
            this.groupBox1.Controls.Add( this.lnkInXsd );
            this.groupBox1.Location = new System.Drawing.Point( 2, 83 );
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size( 456, 72 );
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Make XML template  from XSD";
            // 
            // WinGenBloomData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnDone;
            this.ClientSize = new System.Drawing.Size( 462, 224 );
            this.Controls.Add( this.btnDone );
            this.Controls.Add( this.btnHelp );
            this.Controls.Add( this.groupBox1 );
            this.Controls.Add( this.txtDisplay );
            this.Controls.Add( this.btnAbort );
            this.Controls.Add( this.btnGenerate );
            this.Controls.Add( this.lblNumMsg );
            this.Controls.Add( this.numMsg );
            this.Controls.Add( this.txtOutputXml );
            this.Controls.Add( this.lnkOutputXml );
            this.Controls.Add( this.txtXmlFile );
            this.Controls.Add( this.lnkSampleXml );
            this.Controls.Add( this.cboTypeVer );
            this.Controls.Add( this.cboDocType );
            this.Controls.Add( this.lblTypeVer );
            this.Controls.Add( this.lblDocType );
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "WinGenBloomData";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Generate Bloomberg Data";
            this.ttpBData.SetToolTip( this, "Number of message in the xml file" );
            this.Load += new System.EventHandler( this.WinGenBloomData_Load );
            ((System.ComponentModel.ISupportInitialize)(this.numMsg)).EndInit();
            this.groupBox1.ResumeLayout( false );
            this.groupBox1.PerformLayout();
            this.ResumeLayout( false );
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblDocType;
        private System.Windows.Forms.Label lblTypeVer;
        private System.Windows.Forms.ToolTip ttpBData;
        private System.Windows.Forms.ComboBox cboDocType;
        private System.Windows.Forms.ComboBox cboTypeVer;
        private System.Windows.Forms.LinkLabel lnkSampleXml;
        private System.Windows.Forms.TextBox txtXmlFile;
        private System.Windows.Forms.LinkLabel lnkOutputXml;
        private System.Windows.Forms.TextBox txtOutputXml;
        private System.Windows.Forms.NumericUpDown numMsg;
        private System.Windows.Forms.Label lblNumMsg;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.Button btnAbort;
        private System.Windows.Forms.TextBox txtDisplay;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.LinkLabel lnkInXsd;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.TextBox txtOutXmlTemplate;
        private System.Windows.Forms.TextBox txtInXsd;
        private System.Windows.Forms.LinkLabel lnkOutXml;
        private System.Windows.Forms.Button btnHelp;
        private System.Windows.Forms.Button btnDone;
    }
}