namespace LiveCapture
{
    partial class UcCalc
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
            this.ttpCalc = new System.Windows.Forms.ToolTip( this.components );
            this.txtNotepad = new System.Windows.Forms.TextBox();
            this.notepadPanel = new System.Windows.Forms.Panel();
            this.txtResult = new System.Windows.Forms.TextBox();
            this.btnBack = new System.Windows.Forms.Button();
            this.btnEqual = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnDot = new System.Windows.Forms.Button();
            this.btnPlusMinus = new System.Windows.Forms.Button();
            this.btnPlus = new System.Windows.Forms.Button();
            this.btnZero = new System.Windows.Forms.Button();
            this.btnTwo = new System.Windows.Forms.Button();
            this.btnThree = new System.Windows.Forms.Button();
            this.btnMinus = new System.Windows.Forms.Button();
            this.btnOne = new System.Windows.Forms.Button();
            this.btnFive = new System.Windows.Forms.Button();
            this.btnSix = new System.Windows.Forms.Button();
            this.btnMultiply = new System.Windows.Forms.Button();
            this.btnFour = new System.Windows.Forms.Button();
            this.btnEight = new System.Windows.Forms.Button();
            this.btnNine = new System.Windows.Forms.Button();
            this.btnDivide = new System.Windows.Forms.Button();
            this.btnSeven = new System.Windows.Forms.Button();
            this.notepadPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtNotepad
            // 
            this.txtNotepad.AllowDrop = true;
            this.txtNotepad.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtNotepad.Location = new System.Drawing.Point( 0, 0 );
            this.txtNotepad.Multiline = true;
            this.txtNotepad.Name = "txtNotepad";
            this.txtNotepad.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtNotepad.Size = new System.Drawing.Size( 544, 153 );
            this.txtNotepad.TabIndex = 44;
            this.ttpCalc.SetToolTip( this.txtNotepad, "Max 32767 Characters" );
            // 
            // notepadPanel
            // 
            this.notepadPanel.Controls.Add( this.txtResult );
            this.notepadPanel.Controls.Add( this.btnBack );
            this.notepadPanel.Controls.Add( this.btnEqual );
            this.notepadPanel.Controls.Add( this.btnClear );
            this.notepadPanel.Controls.Add( this.btnDot );
            this.notepadPanel.Controls.Add( this.btnPlusMinus );
            this.notepadPanel.Controls.Add( this.btnPlus );
            this.notepadPanel.Controls.Add( this.btnZero );
            this.notepadPanel.Controls.Add( this.btnTwo );
            this.notepadPanel.Controls.Add( this.btnThree );
            this.notepadPanel.Controls.Add( this.btnMinus );
            this.notepadPanel.Controls.Add( this.btnOne );
            this.notepadPanel.Controls.Add( this.btnFive );
            this.notepadPanel.Controls.Add( this.btnSix );
            this.notepadPanel.Controls.Add( this.btnMultiply );
            this.notepadPanel.Controls.Add( this.btnFour );
            this.notepadPanel.Controls.Add( this.btnEight );
            this.notepadPanel.Controls.Add( this.btnNine );
            this.notepadPanel.Controls.Add( this.btnDivide );
            this.notepadPanel.Controls.Add( this.btnSeven );
            this.notepadPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.notepadPanel.Location = new System.Drawing.Point( 544, 0 );
            this.notepadPanel.Name = "notepadPanel";
            this.notepadPanel.Size = new System.Drawing.Size( 110, 153 );
            this.notepadPanel.TabIndex = 43;
            // 
            // txtResult
            // 
            this.txtResult.Location = new System.Drawing.Point( 5, 3 );
            this.txtResult.Name = "txtResult";
            this.txtResult.Size = new System.Drawing.Size( 100, 20 );
            this.txtResult.TabIndex = 62;
            // 
            // btnBack
            // 
            this.btnBack.BackColor = System.Drawing.Color.OliveDrab;
            this.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBack.Font = new System.Drawing.Font( "Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)) );
            this.btnBack.Location = new System.Drawing.Point( 30, 126 );
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size( 23, 23 );
            this.btnBack.TabIndex = 61;
            this.btnBack.TabStop = false;
            this.btnBack.Tag = "Back Space";
            this.btnBack.Text = "<";
            this.btnBack.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnBack.UseVisualStyleBackColor = true;
            // 
            // btnEqual
            // 
            this.btnEqual.BackColor = System.Drawing.Color.DarkGray;
            this.btnEqual.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEqual.Location = new System.Drawing.Point( 56, 126 );
            this.btnEqual.Name = "btnEqual";
            this.btnEqual.Size = new System.Drawing.Size( 49, 23 );
            this.btnEqual.TabIndex = 60;
            this.btnEqual.TabStop = false;
            this.btnEqual.Text = "=";
            this.btnEqual.UseVisualStyleBackColor = true;
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.OliveDrab;
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.Location = new System.Drawing.Point( 4, 126 );
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size( 23, 23 );
            this.btnClear.TabIndex = 59;
            this.btnClear.TabStop = false;
            this.btnClear.Text = "C";
            this.btnClear.UseVisualStyleBackColor = true;
            // 
            // btnDot
            // 
            this.btnDot.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnDot.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDot.Location = new System.Drawing.Point( 30, 101 );
            this.btnDot.Name = "btnDot";
            this.btnDot.Size = new System.Drawing.Size( 23, 23 );
            this.btnDot.TabIndex = 58;
            this.btnDot.TabStop = false;
            this.btnDot.Text = ".";
            this.btnDot.UseVisualStyleBackColor = true;
            // 
            // btnPlusMinus
            // 
            this.btnPlusMinus.BackColor = System.Drawing.Color.DarkGray;
            this.btnPlusMinus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPlusMinus.Location = new System.Drawing.Point( 56, 101 );
            this.btnPlusMinus.Name = "btnPlusMinus";
            this.btnPlusMinus.Size = new System.Drawing.Size( 23, 23 );
            this.btnPlusMinus.TabIndex = 57;
            this.btnPlusMinus.TabStop = false;
            this.btnPlusMinus.Text = "±";
            this.btnPlusMinus.UseVisualStyleBackColor = true;
            // 
            // btnPlus
            // 
            this.btnPlus.BackColor = System.Drawing.Color.DarkGray;
            this.btnPlus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPlus.Location = new System.Drawing.Point( 82, 101 );
            this.btnPlus.Name = "btnPlus";
            this.btnPlus.Size = new System.Drawing.Size( 23, 23 );
            this.btnPlus.TabIndex = 56;
            this.btnPlus.TabStop = false;
            this.btnPlus.Text = "+";
            this.btnPlus.UseVisualStyleBackColor = true;
            // 
            // btnZero
            // 
            this.btnZero.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnZero.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnZero.Location = new System.Drawing.Point( 4, 101 );
            this.btnZero.Name = "btnZero";
            this.btnZero.Size = new System.Drawing.Size( 23, 23 );
            this.btnZero.TabIndex = 55;
            this.btnZero.TabStop = false;
            this.btnZero.Text = "0";
            this.btnZero.UseVisualStyleBackColor = true;
            // 
            // btnTwo
            // 
            this.btnTwo.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnTwo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTwo.Location = new System.Drawing.Point( 30, 76 );
            this.btnTwo.Name = "btnTwo";
            this.btnTwo.Size = new System.Drawing.Size( 23, 23 );
            this.btnTwo.TabIndex = 54;
            this.btnTwo.TabStop = false;
            this.btnTwo.Text = "2";
            this.btnTwo.UseVisualStyleBackColor = true;
            // 
            // btnThree
            // 
            this.btnThree.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnThree.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThree.Location = new System.Drawing.Point( 56, 76 );
            this.btnThree.Name = "btnThree";
            this.btnThree.Size = new System.Drawing.Size( 23, 23 );
            this.btnThree.TabIndex = 53;
            this.btnThree.TabStop = false;
            this.btnThree.Text = "3";
            this.btnThree.UseVisualStyleBackColor = true;
            // 
            // btnMinus
            // 
            this.btnMinus.BackColor = System.Drawing.Color.DarkGray;
            this.btnMinus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMinus.Location = new System.Drawing.Point( 82, 76 );
            this.btnMinus.Name = "btnMinus";
            this.btnMinus.Size = new System.Drawing.Size( 23, 23 );
            this.btnMinus.TabIndex = 52;
            this.btnMinus.TabStop = false;
            this.btnMinus.Text = "-";
            this.btnMinus.UseVisualStyleBackColor = true;
            // 
            // btnOne
            // 
            this.btnOne.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnOne.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOne.Location = new System.Drawing.Point( 4, 76 );
            this.btnOne.Name = "btnOne";
            this.btnOne.Size = new System.Drawing.Size( 23, 23 );
            this.btnOne.TabIndex = 51;
            this.btnOne.TabStop = false;
            this.btnOne.Text = "1";
            this.btnOne.UseVisualStyleBackColor = true;
            // 
            // btnFive
            // 
            this.btnFive.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnFive.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFive.Location = new System.Drawing.Point( 30, 51 );
            this.btnFive.Name = "btnFive";
            this.btnFive.Size = new System.Drawing.Size( 23, 23 );
            this.btnFive.TabIndex = 50;
            this.btnFive.TabStop = false;
            this.btnFive.Text = "5";
            this.btnFive.UseVisualStyleBackColor = true;
            // 
            // btnSix
            // 
            this.btnSix.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnSix.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSix.Location = new System.Drawing.Point( 56, 51 );
            this.btnSix.Name = "btnSix";
            this.btnSix.Size = new System.Drawing.Size( 23, 23 );
            this.btnSix.TabIndex = 49;
            this.btnSix.TabStop = false;
            this.btnSix.Text = "6";
            this.btnSix.UseVisualStyleBackColor = true;
            // 
            // btnMultiply
            // 
            this.btnMultiply.BackColor = System.Drawing.Color.DarkGray;
            this.btnMultiply.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMultiply.Location = new System.Drawing.Point( 82, 51 );
            this.btnMultiply.Name = "btnMultiply";
            this.btnMultiply.Size = new System.Drawing.Size( 23, 23 );
            this.btnMultiply.TabIndex = 48;
            this.btnMultiply.TabStop = false;
            this.btnMultiply.Text = "*";
            this.btnMultiply.UseVisualStyleBackColor = true;
            // 
            // btnFour
            // 
            this.btnFour.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnFour.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFour.Location = new System.Drawing.Point( 4, 51 );
            this.btnFour.Name = "btnFour";
            this.btnFour.Size = new System.Drawing.Size( 23, 23 );
            this.btnFour.TabIndex = 47;
            this.btnFour.TabStop = false;
            this.btnFour.Text = "4";
            this.btnFour.UseVisualStyleBackColor = true;
            // 
            // btnEight
            // 
            this.btnEight.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnEight.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEight.Location = new System.Drawing.Point( 30, 26 );
            this.btnEight.Name = "btnEight";
            this.btnEight.Size = new System.Drawing.Size( 23, 23 );
            this.btnEight.TabIndex = 46;
            this.btnEight.TabStop = false;
            this.btnEight.Text = "8";
            this.btnEight.UseVisualStyleBackColor = true;
            // 
            // btnNine
            // 
            this.btnNine.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnNine.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNine.Location = new System.Drawing.Point( 56, 26 );
            this.btnNine.Name = "btnNine";
            this.btnNine.Size = new System.Drawing.Size( 23, 23 );
            this.btnNine.TabIndex = 45;
            this.btnNine.TabStop = false;
            this.btnNine.Text = "9";
            this.btnNine.UseVisualStyleBackColor = true;
            // 
            // btnDivide
            // 
            this.btnDivide.BackColor = System.Drawing.Color.DarkGray;
            this.btnDivide.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDivide.Location = new System.Drawing.Point( 82, 26 );
            this.btnDivide.Name = "btnDivide";
            this.btnDivide.Size = new System.Drawing.Size( 23, 23 );
            this.btnDivide.TabIndex = 44;
            this.btnDivide.TabStop = false;
            this.btnDivide.Text = "/";
            this.btnDivide.UseVisualStyleBackColor = true;
            // 
            // btnSeven
            // 
            this.btnSeven.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnSeven.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSeven.Location = new System.Drawing.Point( 4, 26 );
            this.btnSeven.Name = "btnSeven";
            this.btnSeven.Size = new System.Drawing.Size( 23, 23 );
            this.btnSeven.TabIndex = 43;
            this.btnSeven.TabStop = false;
            this.btnSeven.Text = "7";
            this.btnSeven.UseVisualStyleBackColor = true;
            // 
            // UcCalc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.Controls.Add( this.txtNotepad );
            this.Controls.Add( this.notepadPanel );
            this.Name = "UcCalc";
            this.Size = new System.Drawing.Size( 654, 153 );
            this.Load += new System.EventHandler( this.UcCalc_Load );
            this.notepadPanel.ResumeLayout( false );
            this.notepadPanel.PerformLayout();
            this.ResumeLayout( false );
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolTip ttpCalc;
        private System.Windows.Forms.Panel notepadPanel;
        private System.Windows.Forms.TextBox txtResult;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Button btnEqual;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnDot;
        private System.Windows.Forms.Button btnPlusMinus;
        private System.Windows.Forms.Button btnPlus;
        private System.Windows.Forms.Button btnZero;
        private System.Windows.Forms.Button btnTwo;
        private System.Windows.Forms.Button btnThree;
        private System.Windows.Forms.Button btnMinus;
        private System.Windows.Forms.Button btnOne;
        private System.Windows.Forms.Button btnFive;
        private System.Windows.Forms.Button btnSix;
        private System.Windows.Forms.Button btnMultiply;
        private System.Windows.Forms.Button btnFour;
        private System.Windows.Forms.Button btnEight;
        private System.Windows.Forms.Button btnNine;
        private System.Windows.Forms.Button btnDivide;
        private System.Windows.Forms.Button btnSeven;
        private System.Windows.Forms.TextBox txtNotepad;
    }
}
