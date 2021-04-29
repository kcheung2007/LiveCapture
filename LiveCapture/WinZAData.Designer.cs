namespace LiveCapture
{
    partial class WinZAData
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
            this.ttpZAData = new System.Windows.Forms.ToolTip( this.components );
            this.lnkEmlFolder = new System.Windows.Forms.LinkLabel();
            this.lnkInCSVFile = new System.Windows.Forms.LinkLabel();
            this.lnkOutCSVFile = new System.Windows.Forms.LinkLabel();
            this.txtEmlFolder = new System.Windows.Forms.TextBox();
            this.txtInFile = new System.Windows.Forms.TextBox();
            this.txtOutFile = new System.Windows.Forms.TextBox();
            this.folderBrowserDlg = new System.Windows.Forms.FolderBrowserDialog();
            this.openFileDlg = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDlg = new System.Windows.Forms.SaveFileDialog();
            this.txtDisplay = new System.Windows.Forms.TextBox();
            this.btnDoIt = new System.Windows.Forms.Button();
            this.btnDone = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lnkEmlFolder
            // 
            this.lnkEmlFolder.Location = new System.Drawing.Point( 3, 8 );
            this.lnkEmlFolder.Name = "lnkEmlFolder";
            this.lnkEmlFolder.Size = new System.Drawing.Size( 58, 13 );
            this.lnkEmlFolder.TabIndex = 0;
            this.lnkEmlFolder.TabStop = true;
            this.lnkEmlFolder.Text = "Eml Folder";
            this.lnkEmlFolder.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ttpZAData.SetToolTip( this.lnkEmlFolder, "Browse eml files folder" );
            this.lnkEmlFolder.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler( this.lnkEmlFolder_LinkClicked );
            // 
            // lnkInCSVFile
            // 
            this.lnkInCSVFile.Location = new System.Drawing.Point( 3, 30 );
            this.lnkInCSVFile.Name = "lnkInCSVFile";
            this.lnkInCSVFile.Size = new System.Drawing.Size( 58, 13 );
            this.lnkInCSVFile.TabIndex = 1;
            this.lnkInCSVFile.TabStop = true;
            this.lnkInCSVFile.Text = "Input File";
            this.lnkInCSVFile.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ttpZAData.SetToolTip( this.lnkInCSVFile, "Point to csv input file" );
            this.lnkInCSVFile.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler( this.lnkInCSVFile_LinkClicked );
            // 
            // lnkOutCSVFile
            // 
            this.lnkOutCSVFile.Enabled = false;
            this.lnkOutCSVFile.Location = new System.Drawing.Point( 3, 51 );
            this.lnkOutCSVFile.Name = "lnkOutCSVFile";
            this.lnkOutCSVFile.Size = new System.Drawing.Size( 58, 13 );
            this.lnkOutCSVFile.TabIndex = 2;
            this.lnkOutCSVFile.TabStop = true;
            this.lnkOutCSVFile.Text = "Output File";
            this.lnkOutCSVFile.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ttpZAData.SetToolTip( this.lnkOutCSVFile, "Point to csv output file" );
            this.lnkOutCSVFile.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler( this.lnkOutCSVFile_LinkClicked );
            // 
            // txtEmlFolder
            // 
            this.txtEmlFolder.Location = new System.Drawing.Point( 65, 4 );
            this.txtEmlFolder.Name = "txtEmlFolder";
            this.txtEmlFolder.Size = new System.Drawing.Size( 389, 20 );
            this.txtEmlFolder.TabIndex = 3;
            this.txtEmlFolder.Text = "C:\\WorkingFolder\\Li\\emls";
            this.ttpZAData.SetToolTip( this.txtEmlFolder, "Folder that contains eml files" );
            // 
            // txtInFile
            // 
            this.txtInFile.Location = new System.Drawing.Point( 65, 26 );
            this.txtInFile.Name = "txtInFile";
            this.txtInFile.Size = new System.Drawing.Size( 389, 20 );
            this.txtInFile.TabIndex = 4;
            this.txtInFile.Text = "C:\\WorkingFolder\\Li\\Lithium.csv";
            this.ttpZAData.SetToolTip( this.txtInFile, "Input CSV Test Case File" );
            // 
            // txtOutFile
            // 
            this.txtOutFile.Enabled = false;
            this.txtOutFile.Location = new System.Drawing.Point( 65, 48 );
            this.txtOutFile.Name = "txtOutFile";
            this.txtOutFile.Size = new System.Drawing.Size( 389, 20 );
            this.txtOutFile.TabIndex = 5;
            this.ttpZAData.SetToolTip( this.txtOutFile, "Output CSV Test Case File with header/value" );
            // 
            // folderBrowserDlg
            // 
            this.folderBrowserDlg.Description = "Browser the folder that contains eml files";
            this.folderBrowserDlg.RootFolder = System.Environment.SpecialFolder.MyComputer;
            this.folderBrowserDlg.SelectedPath = "C:\\";
            // 
            // openFileDlg
            // 
            this.openFileDlg.Filter = "csv file|*.csv|All files|*.*";
            this.openFileDlg.InitialDirectory = "c:\\";
            // 
            // saveFileDlg
            // 
            this.saveFileDlg.DefaultExt = "csv";
            this.saveFileDlg.Filter = "csv files|*.csv|All files|*.*";
            // 
            // txtDisplay
            // 
            this.txtDisplay.Location = new System.Drawing.Point( 6, 75 );
            this.txtDisplay.MaxLength = 65534;
            this.txtDisplay.Multiline = true;
            this.txtDisplay.Name = "txtDisplay";
            this.txtDisplay.Size = new System.Drawing.Size( 381, 72 );
            this.txtDisplay.TabIndex = 6;
            // 
            // btnDoIt
            // 
            this.btnDoIt.Location = new System.Drawing.Point( 394, 75 );
            this.btnDoIt.Name = "btnDoIt";
            this.btnDoIt.Size = new System.Drawing.Size( 60, 23 );
            this.btnDoIt.TabIndex = 7;
            this.btnDoIt.Text = "Do it";
            this.btnDoIt.UseVisualStyleBackColor = true;
            this.btnDoIt.Click += new System.EventHandler( this.btnDoIt_Click );
            // 
            // btnDone
            // 
            this.btnDone.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnDone.Enabled = false;
            this.btnDone.Location = new System.Drawing.Point( 394, 104 );
            this.btnDone.Name = "btnDone";
            this.btnDone.Size = new System.Drawing.Size( 60, 23 );
            this.btnDone.TabIndex = 8;
            this.btnDone.Text = "Done";
            this.btnDone.UseVisualStyleBackColor = true;
            // 
            // WinZAData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnDone;
            this.ClientSize = new System.Drawing.Size( 460, 214 );
            this.Controls.Add( this.btnDone );
            this.Controls.Add( this.btnDoIt );
            this.Controls.Add( this.txtDisplay );
            this.Controls.Add( this.txtOutFile );
            this.Controls.Add( this.txtInFile );
            this.Controls.Add( this.txtEmlFolder );
            this.Controls.Add( this.lnkOutCSVFile );
            this.Controls.Add( this.lnkInCSVFile );
            this.Controls.Add( this.lnkEmlFolder );
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MinimizeBox = false;
            this.Name = "WinZAData";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ZA Extractor";
            this.ResumeLayout( false );
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolTip ttpZAData;
        private System.Windows.Forms.LinkLabel lnkEmlFolder;
        private System.Windows.Forms.LinkLabel lnkInCSVFile;
        private System.Windows.Forms.LinkLabel lnkOutCSVFile;
        private System.Windows.Forms.TextBox txtEmlFolder;
        private System.Windows.Forms.TextBox txtInFile;
        private System.Windows.Forms.TextBox txtOutFile;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDlg;
        private System.Windows.Forms.OpenFileDialog openFileDlg;
        private System.Windows.Forms.SaveFileDialog saveFileDlg;
        private System.Windows.Forms.TextBox txtDisplay;
        private System.Windows.Forms.Button btnDoIt;
        private System.Windows.Forms.Button btnDone;
    }
}