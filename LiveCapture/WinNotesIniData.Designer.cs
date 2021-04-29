namespace LiveCapture
{
    partial class WinNotesIniData
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
            this.txtInTruthTable = new System.Windows.Forms.TextBox();
            this.txtInTemplate = new System.Windows.Forms.TextBox();
            this.lnkOutCSVFile = new System.Windows.Forms.LinkLabel();
            this.lnkInTemplate = new System.Windows.Forms.LinkLabel();
            this.txtOutFolder = new System.Windows.Forms.TextBox();
            this.lnkOutFolder = new System.Windows.Forms.LinkLabel();
            this.ttpNotesIniModifier = new System.Windows.Forms.ToolTip( this.components );
            this.btnDone = new System.Windows.Forms.Button();
            this.btnDoIt = new System.Windows.Forms.Button();
            this.txtDisplay = new System.Windows.Forms.TextBox();
            this.folderBrowserDlg = new System.Windows.Forms.FolderBrowserDialog();
            this.openFileDlg = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // txtInTruthTable
            // 
            this.txtInTruthTable.Location = new System.Drawing.Point( 82, 25 );
            this.txtInTruthTable.Name = "txtInTruthTable";
            this.txtInTruthTable.Size = new System.Drawing.Size( 389, 20 );
            this.txtInTruthTable.TabIndex = 9;
            // 
            // txtInTemplate
            // 
            this.txtInTemplate.Location = new System.Drawing.Point( 82, 3 );
            this.txtInTemplate.Name = "txtInTemplate";
            this.txtInTemplate.Size = new System.Drawing.Size( 389, 20 );
            this.txtInTemplate.TabIndex = 8;
            // 
            // lnkOutCSVFile
            // 
            this.lnkOutCSVFile.Location = new System.Drawing.Point( 16, 28 );
            this.lnkOutCSVFile.Name = "lnkOutCSVFile";
            this.lnkOutCSVFile.Size = new System.Drawing.Size( 65, 13 );
            this.lnkOutCSVFile.TabIndex = 7;
            this.lnkOutCSVFile.TabStop = true;
            this.lnkOutCSVFile.Text = "Truth Table";
            this.lnkOutCSVFile.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lnkOutCSVFile.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler( this.lnkOutCSVFile_LinkClicked );
            // 
            // lnkInTemplate
            // 
            this.lnkInTemplate.Location = new System.Drawing.Point( 3, 7 );
            this.lnkInTemplate.Name = "lnkInTemplate";
            this.lnkInTemplate.Size = new System.Drawing.Size( 78, 13 );
            this.lnkInTemplate.TabIndex = 6;
            this.lnkInTemplate.TabStop = true;
            this.lnkInTemplate.Text = "Input Template";
            this.lnkInTemplate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ttpNotesIniModifier.SetToolTip( this.lnkInTemplate, "Append data at the end of file" );
            this.lnkInTemplate.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler( this.lnkInTemplate_LinkClicked );
            // 
            // txtOutFolder
            // 
            this.txtOutFolder.Location = new System.Drawing.Point( 82, 48 );
            this.txtOutFolder.Name = "txtOutFolder";
            this.txtOutFolder.Size = new System.Drawing.Size( 389, 20 );
            this.txtOutFolder.TabIndex = 11;
            this.txtOutFolder.Text = "C:\\WorkingFolder\\Ini";
            this.ttpNotesIniModifier.SetToolTip( this.txtOutFolder, "Place holder for output files" );
            // 
            // lnkOutFolder
            // 
            this.lnkOutFolder.Location = new System.Drawing.Point( 23, 52 );
            this.lnkOutFolder.Name = "lnkOutFolder";
            this.lnkOutFolder.Size = new System.Drawing.Size( 58, 13 );
            this.lnkOutFolder.TabIndex = 10;
            this.lnkOutFolder.TabStop = true;
            this.lnkOutFolder.Text = "Out Folder";
            this.lnkOutFolder.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ttpNotesIniModifier.SetToolTip( this.lnkOutFolder, "Store the output files based on the Truth Table" );
            this.lnkOutFolder.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler( this.lnkOutFolder_LinkClicked );
            // 
            // btnDone
            // 
            this.btnDone.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnDone.Enabled = false;
            this.btnDone.Location = new System.Drawing.Point( 406, 97 );
            this.btnDone.Name = "btnDone";
            this.btnDone.Size = new System.Drawing.Size( 60, 23 );
            this.btnDone.TabIndex = 14;
            this.btnDone.Text = "Done";
            this.btnDone.UseVisualStyleBackColor = true;
            // 
            // btnDoIt
            // 
            this.btnDoIt.Location = new System.Drawing.Point( 406, 74 );
            this.btnDoIt.Name = "btnDoIt";
            this.btnDoIt.Size = new System.Drawing.Size( 60, 23 );
            this.btnDoIt.TabIndex = 13;
            this.btnDoIt.Text = "Do it";
            this.btnDoIt.UseVisualStyleBackColor = true;
            this.btnDoIt.Click += new System.EventHandler( this.btnDoIt_Click );
            // 
            // txtDisplay
            // 
            this.txtDisplay.Location = new System.Drawing.Point( 12, 74 );
            this.txtDisplay.MaxLength = 65534;
            this.txtDisplay.Multiline = true;
            this.txtDisplay.Name = "txtDisplay";
            this.txtDisplay.Size = new System.Drawing.Size( 390, 72 );
            this.txtDisplay.TabIndex = 12;
            // 
            // folderBrowserDlg
            // 
            this.folderBrowserDlg.Description = "Browser the folder that contains eml files";
            this.folderBrowserDlg.RootFolder = System.Environment.SpecialFolder.MyComputer;
            this.folderBrowserDlg.SelectedPath = "C:\\";
            // 
            // openFileDlg
            // 
            this.openFileDlg.InitialDirectory = "c:\\";
            // 
            // WinNotesIniData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size( 474, 151 );
            this.Controls.Add( this.btnDone );
            this.Controls.Add( this.btnDoIt );
            this.Controls.Add( this.txtDisplay );
            this.Controls.Add( this.txtOutFolder );
            this.Controls.Add( this.lnkOutFolder );
            this.Controls.Add( this.txtInTruthTable );
            this.Controls.Add( this.txtInTemplate );
            this.Controls.Add( this.lnkOutCSVFile );
            this.Controls.Add( this.lnkInTemplate );
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "WinNotesIniData";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Notes.ini Modifier";
            this.ttpNotesIniModifier.SetToolTip( this, "Input the Truth Table" );
            this.ResumeLayout( false );
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtInTruthTable;
        private System.Windows.Forms.TextBox txtInTemplate;
        private System.Windows.Forms.LinkLabel lnkOutCSVFile;
        private System.Windows.Forms.LinkLabel lnkInTemplate;
        private System.Windows.Forms.TextBox txtOutFolder;
        private System.Windows.Forms.LinkLabel lnkOutFolder;
        private System.Windows.Forms.ToolTip ttpNotesIniModifier;
        private System.Windows.Forms.Button btnDone;
        private System.Windows.Forms.Button btnDoIt;
        private System.Windows.Forms.TextBox txtDisplay;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDlg;
        private System.Windows.Forms.OpenFileDialog openFileDlg;
    }
}