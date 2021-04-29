namespace LiveCapture
{
    partial class QATool
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager( typeof( QATool ) );
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.hSplitContainer = new System.Windows.Forms.SplitContainer();
            this.vSplitContainer = new System.Windows.Forms.SplitContainer();
            this.tabCtrlLeft = new System.Windows.Forms.TabControl();
            this.tabPgSMTP = new System.Windows.Forms.TabPage();
            this.tabPgNotesClient = new System.Windows.Forms.TabPage();
            this.tabPgTester = new System.Windows.Forms.TabPage();
            this.tabPgCounter = new System.Windows.Forms.TabPage();
            this.tabPgCmd = new System.Windows.Forms.TabPage();
            this.tabCtrlRight = new System.Windows.Forms.TabControl();
            this.tabPgData = new System.Windows.Forms.TabPage();
            this.btnDXLData = new System.Windows.Forms.Button();
            this.btnNotesData = new System.Windows.Forms.Button();
            this.btnZAData = new System.Windows.Forms.Button();
            this.btnBloomData = new System.Windows.Forms.Button();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.tabCtrlBottom = new System.Windows.Forms.TabControl();
            this.tabPgNotePad = new System.Windows.Forms.TabPage();
            this.tabPgExplorer = new System.Windows.Forms.TabPage();
            this.QATimer = new System.Windows.Forms.Timer( this.components );
            this.QAToolTip = new System.Windows.Forms.ToolTip( this.components );
            this.statusStrip.SuspendLayout();
            this.hSplitContainer.Panel1.SuspendLayout();
            this.hSplitContainer.Panel2.SuspendLayout();
            this.hSplitContainer.SuspendLayout();
            this.vSplitContainer.Panel1.SuspendLayout();
            this.vSplitContainer.Panel2.SuspendLayout();
            this.vSplitContainer.SuspendLayout();
            this.tabCtrlLeft.SuspendLayout();
            this.tabCtrlRight.SuspendLayout();
            this.tabPgData.SuspendLayout();
            this.tabCtrlBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange( new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1} );
            this.statusStrip.Location = new System.Drawing.Point( 0, 601 );
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size( 842, 22 );
            this.statusStrip.TabIndex = 0;
            this.statusStrip.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size( 50, 17 );
            this.toolStripStatusLabel1.Text = "Ready...";
            // 
            // hSplitContainer
            // 
            this.hSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hSplitContainer.Location = new System.Drawing.Point( 0, 0 );
            this.hSplitContainer.Name = "hSplitContainer";
            this.hSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // hSplitContainer.Panel1
            // 
            this.hSplitContainer.Panel1.Controls.Add( this.vSplitContainer );
            // 
            // hSplitContainer.Panel2
            // 
            this.hSplitContainer.Panel2.Controls.Add( this.tabCtrlBottom );
            this.hSplitContainer.Size = new System.Drawing.Size( 842, 601 );
            this.hSplitContainer.SplitterDistance = 415;
            this.hSplitContainer.TabIndex = 1;
            // 
            // vSplitContainer
            // 
            this.vSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.vSplitContainer.Location = new System.Drawing.Point( 0, 0 );
            this.vSplitContainer.Name = "vSplitContainer";
            // 
            // vSplitContainer.Panel1
            // 
            this.vSplitContainer.Panel1.Controls.Add( this.tabCtrlLeft );
            // 
            // vSplitContainer.Panel2
            // 
            this.vSplitContainer.Panel2.Controls.Add( this.tabCtrlRight );
            this.vSplitContainer.Size = new System.Drawing.Size( 842, 415 );
            this.vSplitContainer.SplitterDistance = 676;
            this.vSplitContainer.TabIndex = 0;
            // 
            // tabCtrlLeft
            // 
            this.tabCtrlLeft.Controls.Add( this.tabPgSMTP );
            this.tabCtrlLeft.Controls.Add( this.tabPgNotesClient );
            this.tabCtrlLeft.Controls.Add( this.tabPgTester );
            this.tabCtrlLeft.Controls.Add( this.tabPgCounter );
            this.tabCtrlLeft.Controls.Add( this.tabPgCmd );
            this.tabCtrlLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabCtrlLeft.Location = new System.Drawing.Point( 0, 0 );
            this.tabCtrlLeft.Name = "tabCtrlLeft";
            this.tabCtrlLeft.SelectedIndex = 0;
            this.tabCtrlLeft.ShowToolTips = true;
            this.tabCtrlLeft.Size = new System.Drawing.Size( 676, 415 );
            this.tabCtrlLeft.TabIndex = 0;
            this.tabCtrlLeft.Enter += new System.EventHandler( this.tabCtrlLeft_Enter );
            this.tabCtrlLeft.SelectedIndexChanged += new System.EventHandler( this.tabCtrlLeft_SelectedIndexChanged );
            // 
            // tabPgSMTP
            // 
            this.tabPgSMTP.Location = new System.Drawing.Point( 4, 22 );
            this.tabPgSMTP.Name = "tabPgSMTP";
            this.tabPgSMTP.Padding = new System.Windows.Forms.Padding( 3 );
            this.tabPgSMTP.Size = new System.Drawing.Size( 668, 389 );
            this.tabPgSMTP.TabIndex = 0;
            this.tabPgSMTP.Text = "SMTP Sender";
            this.tabPgSMTP.UseVisualStyleBackColor = true;
            // 
            // tabPgNotesClient
            // 
            this.tabPgNotesClient.Location = new System.Drawing.Point( 4, 22 );
            this.tabPgNotesClient.Name = "tabPgNotesClient";
            this.tabPgNotesClient.Padding = new System.Windows.Forms.Padding( 3 );
            this.tabPgNotesClient.Size = new System.Drawing.Size( 668, 389 );
            this.tabPgNotesClient.TabIndex = 1;
            this.tabPgNotesClient.Text = "Notes Client";
            this.tabPgNotesClient.UseVisualStyleBackColor = true;
            // 
            // tabPgTester
            // 
            this.tabPgTester.Location = new System.Drawing.Point( 4, 22 );
            this.tabPgTester.Name = "tabPgTester";
            this.tabPgTester.Size = new System.Drawing.Size( 668, 389 );
            this.tabPgTester.TabIndex = 2;
            this.tabPgTester.Text = "Tester";
            this.tabPgTester.UseVisualStyleBackColor = true;
            // 
            // tabPgCounter
            // 
            this.tabPgCounter.Location = new System.Drawing.Point( 4, 22 );
            this.tabPgCounter.Name = "tabPgCounter";
            this.tabPgCounter.Size = new System.Drawing.Size( 668, 389 );
            this.tabPgCounter.TabIndex = 3;
            this.tabPgCounter.Text = "Counter";
            this.tabPgCounter.UseVisualStyleBackColor = true;
            // 
            // tabPgCmd
            // 
            this.tabPgCmd.Location = new System.Drawing.Point( 4, 22 );
            this.tabPgCmd.Name = "tabPgCmd";
            this.tabPgCmd.Size = new System.Drawing.Size( 668, 389 );
            this.tabPgCmd.TabIndex = 4;
            this.tabPgCmd.Text = "Auto Cmd";
            this.tabPgCmd.UseVisualStyleBackColor = true;
            // 
            // tabCtrlRight
            // 
            this.tabCtrlRight.Controls.Add( this.tabPgData );
            this.tabCtrlRight.Controls.Add( this.tabPage4 );
            this.tabCtrlRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabCtrlRight.Location = new System.Drawing.Point( 0, 0 );
            this.tabCtrlRight.Multiline = true;
            this.tabCtrlRight.Name = "tabCtrlRight";
            this.tabCtrlRight.SelectedIndex = 0;
            this.tabCtrlRight.ShowToolTips = true;
            this.tabCtrlRight.Size = new System.Drawing.Size( 162, 415 );
            this.tabCtrlRight.TabIndex = 1;
            this.tabCtrlRight.SelectedIndexChanged += new System.EventHandler( this.tabCtrlRight_SelectedIndexChanged );
            // 
            // tabPgData
            // 
            this.tabPgData.Controls.Add( this.btnDXLData );
            this.tabPgData.Controls.Add( this.btnNotesData );
            this.tabPgData.Controls.Add( this.btnZAData );
            this.tabPgData.Controls.Add( this.btnBloomData );
            this.tabPgData.Location = new System.Drawing.Point( 4, 22 );
            this.tabPgData.Name = "tabPgData";
            this.tabPgData.Padding = new System.Windows.Forms.Padding( 3 );
            this.tabPgData.Size = new System.Drawing.Size( 154, 389 );
            this.tabPgData.TabIndex = 0;
            this.tabPgData.Text = "Data";
            this.tabPgData.ToolTipText = "Generate Test Data";
            this.tabPgData.UseVisualStyleBackColor = true;
            // 
            // btnDXLData
            // 
            this.btnDXLData.Location = new System.Drawing.Point( 9, 84 );
            this.btnDXLData.Name = "btnDXLData";
            this.btnDXLData.Size = new System.Drawing.Size( 139, 23 );
            this.btnDXLData.TabIndex = 3;
            this.btnDXLData.Text = "DXL Data";
            this.btnDXLData.UseVisualStyleBackColor = true;
            this.btnDXLData.Click += new System.EventHandler( this.btnDXLData_Click );
            // 
            // btnNotesData
            // 
            this.btnNotesData.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNotesData.Location = new System.Drawing.Point( 9, 58 );
            this.btnNotesData.Name = "btnNotesData";
            this.btnNotesData.Size = new System.Drawing.Size( 139, 23 );
            this.btnNotesData.TabIndex = 2;
            this.btnNotesData.Text = "Notes ini Data";
            this.QAToolTip.SetToolTip( this.btnNotesData, "Append zArchive Items at the end of notes.ini" );
            this.btnNotesData.UseVisualStyleBackColor = true;
            this.btnNotesData.Click += new System.EventHandler( this.btnNotesData_Click );
            // 
            // btnZAData
            // 
            this.btnZAData.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnZAData.Location = new System.Drawing.Point( 9, 32 );
            this.btnZAData.Name = "btnZAData";
            this.btnZAData.Size = new System.Drawing.Size( 139, 23 );
            this.btnZAData.TabIndex = 1;
            this.btnZAData.Text = "zArchive Data";
            this.QAToolTip.SetToolTip( this.btnZAData, "Extract zArchive data from eml file" );
            this.btnZAData.UseVisualStyleBackColor = true;
            this.btnZAData.Click += new System.EventHandler( this.btnZAData_Click );
            // 
            // btnBloomData
            // 
            this.btnBloomData.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBloomData.Location = new System.Drawing.Point( 7, 7 );
            this.btnBloomData.Name = "btnBloomData";
            this.btnBloomData.Size = new System.Drawing.Size( 139, 23 );
            this.btnBloomData.TabIndex = 0;
            this.btnBloomData.Text = "Bloomger Data";
            this.QAToolTip.SetToolTip( this.btnBloomData, "Generate Bloomberg XML data" );
            this.btnBloomData.UseVisualStyleBackColor = true;
            this.btnBloomData.Click += new System.EventHandler( this.btnBloomData_Click );
            // 
            // tabPage4
            // 
            this.tabPage4.Location = new System.Drawing.Point( 4, 22 );
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding( 3 );
            this.tabPage4.Size = new System.Drawing.Size( 154, 389 );
            this.tabPage4.TabIndex = 1;
            this.tabPage4.Text = "tabPage4";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // tabCtrlBottom
            // 
            this.tabCtrlBottom.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabCtrlBottom.Controls.Add( this.tabPgNotePad );
            this.tabCtrlBottom.Controls.Add( this.tabPgExplorer );
            this.tabCtrlBottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabCtrlBottom.Location = new System.Drawing.Point( 0, 0 );
            this.tabCtrlBottom.Name = "tabCtrlBottom";
            this.tabCtrlBottom.SelectedIndex = 0;
            this.tabCtrlBottom.Size = new System.Drawing.Size( 842, 182 );
            this.tabCtrlBottom.TabIndex = 1;
            this.tabCtrlBottom.SelectedIndexChanged += new System.EventHandler( this.tabCtrlBottom_SelectedIndexChanged );
            // 
            // tabPgNotePad
            // 
            this.tabPgNotePad.Location = new System.Drawing.Point( 4, 4 );
            this.tabPgNotePad.Name = "tabPgNotePad";
            this.tabPgNotePad.Padding = new System.Windows.Forms.Padding( 3 );
            this.tabPgNotePad.Size = new System.Drawing.Size( 834, 156 );
            this.tabPgNotePad.TabIndex = 0;
            this.tabPgNotePad.Text = "NotePad";
            this.tabPgNotePad.UseVisualStyleBackColor = true;
            // 
            // tabPgExplorer
            // 
            this.tabPgExplorer.Location = new System.Drawing.Point( 4, 4 );
            this.tabPgExplorer.Name = "tabPgExplorer";
            this.tabPgExplorer.Padding = new System.Windows.Forms.Padding( 3 );
            this.tabPgExplorer.Size = new System.Drawing.Size( 834, 156 );
            this.tabPgExplorer.TabIndex = 1;
            this.tabPgExplorer.Text = "Explorer";
            this.tabPgExplorer.UseVisualStyleBackColor = true;
            // 
            // QATool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size( 842, 623 );
            this.Controls.Add( this.hSplitContainer );
            this.Controls.Add( this.statusStrip );
            this.Icon = ((System.Drawing.Icon)(resources.GetObject( "$this.Icon" )));
            this.Name = "QATool";
            this.Text = "Form1";
            this.Load += new System.EventHandler( this.QATool_Load );
            this.ResizeEnd += new System.EventHandler( this.QATool_ResizeEnd );
            this.statusStrip.ResumeLayout( false );
            this.statusStrip.PerformLayout();
            this.hSplitContainer.Panel1.ResumeLayout( false );
            this.hSplitContainer.Panel2.ResumeLayout( false );
            this.hSplitContainer.ResumeLayout( false );
            this.vSplitContainer.Panel1.ResumeLayout( false );
            this.vSplitContainer.Panel2.ResumeLayout( false );
            this.vSplitContainer.ResumeLayout( false );
            this.tabCtrlLeft.ResumeLayout( false );
            this.tabCtrlRight.ResumeLayout( false );
            this.tabPgData.ResumeLayout( false );
            this.tabCtrlBottom.ResumeLayout( false );
            this.ResumeLayout( false );
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.SplitContainer hSplitContainer;
        private System.Windows.Forms.SplitContainer vSplitContainer;
        private System.Windows.Forms.TabControl tabCtrlLeft;
        private System.Windows.Forms.TabPage tabPgSMTP;
        private System.Windows.Forms.TabPage tabPgNotesClient;
        private System.Windows.Forms.TabControl tabCtrlRight;
        private System.Windows.Forms.TabPage tabPgData;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TabControl tabCtrlBottom;
        private System.Windows.Forms.TabPage tabPgNotePad;
        private System.Windows.Forms.TabPage tabPgExplorer;
        private System.Windows.Forms.Timer QATimer;
        private System.Windows.Forms.ToolTip QAToolTip;
        private System.Windows.Forms.TabPage tabPgTester;
        private System.Windows.Forms.TabPage tabPgCounter;
        private System.Windows.Forms.Button btnBloomData;
        private System.Windows.Forms.Button btnZAData;
        private System.Windows.Forms.Button btnNotesData;
        private System.Windows.Forms.Button btnDXLData;
        private System.Windows.Forms.TabPage tabPgCmd;
    }
}

