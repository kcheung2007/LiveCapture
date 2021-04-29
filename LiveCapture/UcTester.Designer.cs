namespace LiveCapture
{
    partial class UcTester
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager( typeof( UcTester ) );
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tabCtrlTester = new System.Windows.Forms.TabControl();
            this.tabPgTC1 = new System.Windows.Forms.TabPage();
            this.btnProperty = new System.Windows.Forms.Button();
            this.txtDelimiter = new System.Windows.Forms.TextBox();
            this.cboDelimiter = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtTotalTC = new System.Windows.Forms.TextBox();
            this.lblTotalTC = new System.Windows.Forms.Label();
            this.txtFail = new System.Windows.Forms.TextBox();
            this.txtPass = new System.Windows.Forms.TextBox();
            this.lblTotalEml = new System.Windows.Forms.Label();
            this.lblFail = new System.Windows.Forms.Label();
            this.txtTotalEml = new System.Windows.Forms.TextBox();
            this.lblPass = new System.Windows.Forms.Label();
            this.btnAbort = new System.Windows.Forms.Button();
            this.cboMDrive = new System.Windows.Forms.ComboBox();
            this.cboInFile = new System.Windows.Forms.ComboBox();
            this.lnkMDrive = new System.Windows.Forms.LinkLabel();
            this.lnkInFile = new System.Windows.Forms.LinkLabel();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.testerContextMnuStrip = new System.Windows.Forms.ContextMenuStrip( this.components );
            this.btnRun = new System.Windows.Forms.Button();
            this.tabPgCmd = new System.Windows.Forms.TabPage();
            this.button1 = new System.Windows.Forms.Button();
            this.ttpTester = new System.Windows.Forms.ToolTip( this.components );
            this.tabCtrlTester.SuspendLayout();
            this.tabPgTC1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.tabPgCmd.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabCtrlTester
            // 
            this.tabCtrlTester.Alignment = System.Windows.Forms.TabAlignment.Right;
            this.tabCtrlTester.Controls.Add( this.tabPgTC1 );
            this.tabCtrlTester.Controls.Add( this.tabPgCmd );
            this.tabCtrlTester.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabCtrlTester.Location = new System.Drawing.Point( 0, 0 );
            this.tabCtrlTester.Multiline = true;
            this.tabCtrlTester.Name = "tabCtrlTester";
            this.tabCtrlTester.SelectedIndex = 0;
            this.tabCtrlTester.Size = new System.Drawing.Size( 668, 390 );
            this.tabCtrlTester.TabIndex = 0;
            this.tabCtrlTester.SelectedIndexChanged += new System.EventHandler( this.tabCtrlTester_SelectedIndexChanged );
            // 
            // tabPgTC1
            // 
            this.tabPgTC1.Controls.Add( this.btnProperty );
            this.tabPgTC1.Controls.Add( this.txtDelimiter );
            this.tabPgTC1.Controls.Add( this.cboDelimiter );
            this.tabPgTC1.Controls.Add( this.groupBox1 );
            this.tabPgTC1.Controls.Add( this.btnAbort );
            this.tabPgTC1.Controls.Add( this.cboMDrive );
            this.tabPgTC1.Controls.Add( this.cboInFile );
            this.tabPgTC1.Controls.Add( this.lnkMDrive );
            this.tabPgTC1.Controls.Add( this.lnkInFile );
            this.tabPgTC1.Controls.Add( this.dataGridView );
            this.tabPgTC1.Controls.Add( this.btnRun );
            this.tabPgTC1.Location = new System.Drawing.Point( 4, 4 );
            this.tabPgTC1.Name = "tabPgTC1";
            this.tabPgTC1.Padding = new System.Windows.Forms.Padding( 3 );
            this.tabPgTC1.Size = new System.Drawing.Size( 641, 382 );
            this.tabPgTC1.TabIndex = 0;
            this.tabPgTC1.Text = "TC 1";
            this.tabPgTC1.ToolTipText = "Check the header info";
            this.tabPgTC1.UseVisualStyleBackColor = true;
            // 
            // btnProperty
            // 
            this.btnProperty.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnProperty.Image = ((System.Drawing.Image)(resources.GetObject( "btnProperty.Image" )));
            this.btnProperty.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
            this.btnProperty.Location = new System.Drawing.Point( 356, 25 );
            this.btnProperty.Name = "btnProperty";
            this.btnProperty.Size = new System.Drawing.Size( 24, 24 );
            this.btnProperty.TabIndex = 16;
            this.ttpTester.SetToolTip( this.btnProperty, "Set Mail Sender Property" );
            this.btnProperty.UseVisualStyleBackColor = true;
            this.btnProperty.Click += new System.EventHandler( this.btnProperty_Click );
            // 
            // txtDelimiter
            // 
            this.txtDelimiter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDelimiter.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.txtDelimiter.Enabled = false;
            this.txtDelimiter.Location = new System.Drawing.Point( 356, 4 );
            this.txtDelimiter.MaxLength = 1;
            this.txtDelimiter.Name = "txtDelimiter";
            this.txtDelimiter.Size = new System.Drawing.Size( 24, 20 );
            this.txtDelimiter.TabIndex = 15;
            this.ttpTester.SetToolTip( this.txtDelimiter, "Input custom CSV delimiter" );
            // 
            // cboDelimiter
            // 
            this.cboDelimiter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cboDelimiter.BackColor = System.Drawing.SystemColors.Info;
            this.cboDelimiter.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cboDelimiter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDelimiter.Items.AddRange( new object[] {
            "CSV Delimited",
            "Tab Delimited",
            "Custom Delimited"} );
            this.cboDelimiter.Location = new System.Drawing.Point( 232, 4 );
            this.cboDelimiter.Name = "cboDelimiter";
            this.cboDelimiter.Size = new System.Drawing.Size( 118, 21 );
            this.cboDelimiter.TabIndex = 13;
            this.ttpTester.SetToolTip( this.cboDelimiter, "Select CSV delimiter" );
            this.cboDelimiter.SelectionChangeCommitted += new System.EventHandler( this.cboDelimiter_SelectionChangeCommitted );
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add( this.txtTotalTC );
            this.groupBox1.Controls.Add( this.lblTotalTC );
            this.groupBox1.Controls.Add( this.txtFail );
            this.groupBox1.Controls.Add( this.txtPass );
            this.groupBox1.Controls.Add( this.lblTotalEml );
            this.groupBox1.Controls.Add( this.lblFail );
            this.groupBox1.Controls.Add( this.txtTotalEml );
            this.groupBox1.Controls.Add( this.lblPass );
            this.groupBox1.Location = new System.Drawing.Point( 387, -4 );
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size( 192, 56 );
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            // 
            // txtTotalTC
            // 
            this.txtTotalTC.Location = new System.Drawing.Point( 73, 10 );
            this.txtTotalTC.Name = "txtTotalTC";
            this.txtTotalTC.ReadOnly = true;
            this.txtTotalTC.Size = new System.Drawing.Size( 39, 20 );
            this.txtTotalTC.TabIndex = 14;
            this.ttpTester.SetToolTip( this.txtTotalTC, "Total number of TC" );
            // 
            // lblTotalTC
            // 
            this.lblTotalTC.Location = new System.Drawing.Point( 6, 13 );
            this.lblTotalTC.Name = "lblTotalTC";
            this.lblTotalTC.Size = new System.Drawing.Size( 62, 13 );
            this.lblTotalTC.TabIndex = 13;
            this.lblTotalTC.Text = "Total TC";
            this.lblTotalTC.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ttpTester.SetToolTip( this.lblTotalTC, "Current process EML" );
            // 
            // txtFail
            // 
            this.txtFail.Location = new System.Drawing.Point( 147, 32 );
            this.txtFail.Name = "txtFail";
            this.txtFail.ReadOnly = true;
            this.txtFail.Size = new System.Drawing.Size( 39, 20 );
            this.txtFail.TabIndex = 12;
            // 
            // txtPass
            // 
            this.txtPass.Location = new System.Drawing.Point( 147, 10 );
            this.txtPass.Name = "txtPass";
            this.txtPass.ReadOnly = true;
            this.txtPass.Size = new System.Drawing.Size( 39, 20 );
            this.txtPass.TabIndex = 12;
            // 
            // lblTotalEml
            // 
            this.lblTotalEml.Location = new System.Drawing.Point( 6, 36 );
            this.lblTotalEml.Name = "lblTotalEml";
            this.lblTotalEml.Size = new System.Drawing.Size( 62, 13 );
            this.lblTotalEml.TabIndex = 7;
            this.lblTotalEml.Text = "Total EML";
            this.lblTotalEml.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ttpTester.SetToolTip( this.lblTotalEml, "Total number of emal in current M drive folder" );
            // 
            // lblFail
            // 
            this.lblFail.AutoSize = true;
            this.lblFail.Location = new System.Drawing.Point( 120, 36 );
            this.lblFail.Name = "lblFail";
            this.lblFail.Size = new System.Drawing.Size( 23, 13 );
            this.lblFail.TabIndex = 9;
            this.lblFail.Text = "Fail";
            // 
            // txtTotalEml
            // 
            this.txtTotalEml.Location = new System.Drawing.Point( 73, 32 );
            this.txtTotalEml.Name = "txtTotalEml";
            this.txtTotalEml.ReadOnly = true;
            this.txtTotalEml.Size = new System.Drawing.Size( 39, 20 );
            this.txtTotalEml.TabIndex = 10;
            this.ttpTester.SetToolTip( this.txtTotalEml, "Total number of eml files" );
            // 
            // lblPass
            // 
            this.lblPass.AutoSize = true;
            this.lblPass.Location = new System.Drawing.Point( 113, 13 );
            this.lblPass.Name = "lblPass";
            this.lblPass.Size = new System.Drawing.Size( 30, 13 );
            this.lblPass.TabIndex = 8;
            this.lblPass.Text = "Pass";
            // 
            // btnAbort
            // 
            this.btnAbort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAbort.Location = new System.Drawing.Point( 581, 27 );
            this.btnAbort.Name = "btnAbort";
            this.btnAbort.Size = new System.Drawing.Size( 57, 22 );
            this.btnAbort.TabIndex = 6;
            this.btnAbort.Text = "Abort";
            this.btnAbort.UseVisualStyleBackColor = true;
            // 
            // cboMDrive
            // 
            this.cboMDrive.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cboMDrive.BackColor = System.Drawing.SystemColors.Info;
            this.cboMDrive.FormattingEnabled = true;
            this.cboMDrive.Location = new System.Drawing.Point( 54, 27 );
            this.cboMDrive.Name = "cboMDrive";
            this.cboMDrive.Size = new System.Drawing.Size( 296, 21 );
            this.cboMDrive.TabIndex = 5;
            this.cboMDrive.KeyPress += new System.Windows.Forms.KeyPressEventHandler( this.cboMDrive_KeyPress );
            this.cboMDrive.KeyDown += new System.Windows.Forms.KeyEventHandler( this.cboMDrive_KeyDown );
            // 
            // cboInFile
            // 
            this.cboInFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cboInFile.BackColor = System.Drawing.SystemColors.Info;
            this.cboInFile.FormattingEnabled = true;
            this.cboInFile.Location = new System.Drawing.Point( 54, 4 );
            this.cboInFile.Name = "cboInFile";
            this.cboInFile.Size = new System.Drawing.Size( 175, 21 );
            this.cboInFile.TabIndex = 4;
            this.cboInFile.KeyPress += new System.Windows.Forms.KeyPressEventHandler( this.cboInFile_KeyPress );
            this.cboInFile.KeyDown += new System.Windows.Forms.KeyEventHandler( this.cboInFile_KeyDown );
            // 
            // lnkMDrive
            // 
            this.lnkMDrive.AutoSize = true;
            this.lnkMDrive.Location = new System.Drawing.Point( 4, 29 );
            this.lnkMDrive.Name = "lnkMDrive";
            this.lnkMDrive.Size = new System.Drawing.Size( 47, 13 );
            this.lnkMDrive.TabIndex = 3;
            this.lnkMDrive.TabStop = true;
            this.lnkMDrive.Text = "M: Drive";
            this.lnkMDrive.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ttpTester.SetToolTip( this.lnkMDrive, "Exchange Map Drive" );
            this.lnkMDrive.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler( this.lnkMDrive_LinkClicked );
            // 
            // lnkInFile
            // 
            this.lnkInFile.AutoSize = true;
            this.lnkInFile.Location = new System.Drawing.Point( 16, 7 );
            this.lnkInFile.Name = "lnkInFile";
            this.lnkInFile.Size = new System.Drawing.Size( 35, 13 );
            this.lnkInFile.TabIndex = 2;
            this.lnkInFile.TabStop = true;
            this.lnkInFile.Text = "In File";
            this.lnkInFile.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ttpTester.SetToolTip( this.lnkInFile, "Pre-defined control file." );
            this.lnkInFile.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler( this.lnkInFile_LinkClicked );
            // 
            // dataGridView
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.DarkGray;
            this.dataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.ContextMenuStrip = this.testerContextMnuStrip;
            this.dataGridView.Location = new System.Drawing.Point( 0, 55 );
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.Size = new System.Drawing.Size( 641, 328 );
            this.dataGridView.TabIndex = 1;
            this.dataGridView.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler( this.dataGridView_RowPostPaint );
            // 
            // testerContextMnuStrip
            // 
            this.testerContextMnuStrip.Name = "testerContextMnuStrip";
            this.testerContextMnuStrip.Size = new System.Drawing.Size( 61, 4 );
            // 
            // btnRun
            // 
            this.btnRun.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRun.Location = new System.Drawing.Point( 581, 2 );
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size( 57, 22 );
            this.btnRun.TabIndex = 0;
            this.btnRun.Text = "Run";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler( this.btnTest_Click );
            // 
            // tabPgCmd
            // 
            this.tabPgCmd.Controls.Add( this.button1 );
            this.tabPgCmd.Location = new System.Drawing.Point( 4, 4 );
            this.tabPgCmd.Name = "tabPgCmd";
            this.tabPgCmd.Padding = new System.Windows.Forms.Padding( 3 );
            this.tabPgCmd.Size = new System.Drawing.Size( 641, 382 );
            this.tabPgCmd.TabIndex = 1;
            this.tabPgCmd.Text = "tabPage2";
            this.tabPgCmd.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point( 0, 0 );
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size( 75, 23 );
            this.button1.TabIndex = 0;
            // 
            // UcTester
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add( this.tabCtrlTester );
            this.Name = "UcTester";
            this.Size = new System.Drawing.Size( 668, 390 );
            this.Load += new System.EventHandler( this.UcTester_Load );
            this.tabCtrlTester.ResumeLayout( false );
            this.tabPgTC1.ResumeLayout( false );
            this.tabPgTC1.PerformLayout();
            this.groupBox1.ResumeLayout( false );
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.tabPgCmd.ResumeLayout( false );
            this.ResumeLayout( false );

        }

        #endregion

        private System.Windows.Forms.TabControl tabCtrlTester;
        private System.Windows.Forms.TabPage tabPgTC1;
        private System.Windows.Forms.TabPage tabPgCmd;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.ToolTip ttpTester;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.LinkLabel lnkInFile;
        private System.Windows.Forms.LinkLabel lnkMDrive;
        private System.Windows.Forms.ComboBox cboMDrive;
        private System.Windows.Forms.ComboBox cboInFile;
        private System.Windows.Forms.Button btnAbort;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblTotalEml;
        private System.Windows.Forms.TextBox txtTotalEml;
        private System.Windows.Forms.Label lblFail;
        private System.Windows.Forms.Label lblPass;
        private System.Windows.Forms.TextBox txtFail;
        private System.Windows.Forms.TextBox txtPass;
        private System.Windows.Forms.TextBox txtTotalTC;
        private System.Windows.Forms.Label lblTotalTC;
        private System.Windows.Forms.TextBox txtDelimiter;
        private System.Windows.Forms.ComboBox cboDelimiter;
        private System.Windows.Forms.Button btnProperty;
        private System.Windows.Forms.ContextMenuStrip testerContextMnuStrip;
    }
}
