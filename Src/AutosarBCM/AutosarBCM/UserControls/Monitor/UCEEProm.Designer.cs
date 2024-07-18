namespace AutosarBCM.UserControls.Monitor
{
    partial class UCEEProm
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
            this.grpWrite = new System.Windows.Forms.GroupBox();
            this.txtDataByte3 = new System.Windows.Forms.TextBox();
            this.txtDataByte2 = new System.Windows.Forms.TextBox();
            this.txtDataByte1 = new System.Windows.Forms.TextBox();
            this.btnWrite = new System.Windows.Forms.Button();
            this.txtWriteAddress = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.grpRead = new System.Windows.Forms.GroupBox();
            this.lblData2 = new System.Windows.Forms.Label();
            this.lblData1 = new System.Windows.Forms.Label();
            this.lblData0 = new System.Windows.Forms.Label();
            this.btnRead = new System.Windows.Forms.Button();
            this.txtReadAddress = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.grpWrite.SuspendLayout();
            this.grpRead.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpWrite
            // 
            this.grpWrite.Controls.Add(this.txtDataByte3);
            this.grpWrite.Controls.Add(this.txtDataByte2);
            this.grpWrite.Controls.Add(this.txtDataByte1);
            this.grpWrite.Controls.Add(this.btnWrite);
            this.grpWrite.Controls.Add(this.txtWriteAddress);
            this.grpWrite.Controls.Add(this.label4);
            this.grpWrite.Controls.Add(this.label3);
            this.grpWrite.Controls.Add(this.label2);
            this.grpWrite.Controls.Add(this.label1);
            this.grpWrite.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.grpWrite.Location = new System.Drawing.Point(3, 3);
            this.grpWrite.Name = "grpWrite";
            this.grpWrite.Size = new System.Drawing.Size(425, 76);
            this.grpWrite.TabIndex = 0;
            this.grpWrite.TabStop = false;
            this.grpWrite.Text = "Write";
            // 
            // txtDataByte3
            // 
            this.txtDataByte3.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDataByte3.Location = new System.Drawing.Point(252, 41);
            this.txtDataByte3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtDataByte3.MaxLength = 2;
            this.txtDataByte3.Name = "txtDataByte3";
            this.txtDataByte3.Size = new System.Drawing.Size(28, 24);
            this.txtDataByte3.TabIndex = 7;
            this.txtDataByte3.Text = "00";
            this.txtDataByte3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtDataByte3.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDataByte_KeyPress);
            // 
            // txtDataByte2
            // 
            this.txtDataByte2.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDataByte2.Location = new System.Drawing.Point(192, 41);
            this.txtDataByte2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtDataByte2.MaxLength = 2;
            this.txtDataByte2.Name = "txtDataByte2";
            this.txtDataByte2.Size = new System.Drawing.Size(28, 24);
            this.txtDataByte2.TabIndex = 6;
            this.txtDataByte2.Text = "00";
            this.txtDataByte2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtDataByte2.TextChanged += new System.EventHandler(this.txtDataByte2_TextChanged);
            this.txtDataByte2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDataByte_KeyPress);
            // 
            // txtDataByte1
            // 
            this.txtDataByte1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDataByte1.Location = new System.Drawing.Point(132, 41);
            this.txtDataByte1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtDataByte1.MaxLength = 2;
            this.txtDataByte1.Name = "txtDataByte1";
            this.txtDataByte1.Size = new System.Drawing.Size(28, 24);
            this.txtDataByte1.TabIndex = 5;
            this.txtDataByte1.Text = "00";
            this.txtDataByte1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtDataByte1.TextChanged += new System.EventHandler(this.txtDataByte1_TextChanged);
            this.txtDataByte1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDataByte_KeyPress);
            // 
            // btnWrite
            // 
            this.btnWrite.Location = new System.Drawing.Point(333, 23);
            this.btnWrite.Name = "btnWrite";
            this.btnWrite.Size = new System.Drawing.Size(81, 34);
            this.btnWrite.TabIndex = 8;
            this.btnWrite.Text = "WRITE";
            this.btnWrite.UseVisualStyleBackColor = true;
            this.btnWrite.Click += new System.EventHandler(this.btnWrite_Click);
            // 
            // txtWriteAddress
            // 
            this.txtWriteAddress.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtWriteAddress.Location = new System.Drawing.Point(6, 41);
            this.txtWriteAddress.MaxLength = 4;
            this.txtWriteAddress.Name = "txtWriteAddress";
            this.txtWriteAddress.Size = new System.Drawing.Size(84, 24);
            this.txtWriteAddress.TabIndex = 4;
            this.txtWriteAddress.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDataByte_KeyPress);
            this.txtWriteAddress.Leave += new System.EventHandler(this.Address_Leave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(249, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 18);
            this.label4.TabIndex = 3;
            this.label4.Text = "Data 3";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(189, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 18);
            this.label3.TabIndex = 2;
            this.label3.Text = "Data 2";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(129, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 18);
            this.label2.TabIndex = 1;
            this.label2.Text = "Data 1";
            // 
            // triggerLabel
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Address";
            // 
            // grpRead
            // 
            this.grpRead.Controls.Add(this.lblData2);
            this.grpRead.Controls.Add(this.lblData1);
            this.grpRead.Controls.Add(this.lblData0);
            this.grpRead.Controls.Add(this.btnRead);
            this.grpRead.Controls.Add(this.txtReadAddress);
            this.grpRead.Controls.Add(this.label5);
            this.grpRead.Controls.Add(this.label6);
            this.grpRead.Controls.Add(this.label7);
            this.grpRead.Controls.Add(this.label8);
            this.grpRead.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.grpRead.Location = new System.Drawing.Point(3, 85);
            this.grpRead.Name = "grpRead";
            this.grpRead.Size = new System.Drawing.Size(425, 76);
            this.grpRead.TabIndex = 9;
            this.grpRead.TabStop = false;
            this.grpRead.Text = "Read";
            // 
            // lblData2
            // 
            this.lblData2.AutoSize = true;
            this.lblData2.Location = new System.Drawing.Point(249, 44);
            this.lblData2.Name = "lblData2";
            this.lblData2.Size = new System.Drawing.Size(13, 18);
            this.lblData2.TabIndex = 11;
            this.lblData2.Text = "-";
            // 
            // lblData1
            // 
            this.lblData1.AutoSize = true;
            this.lblData1.Location = new System.Drawing.Point(189, 44);
            this.lblData1.Name = "lblData1";
            this.lblData1.Size = new System.Drawing.Size(13, 18);
            this.lblData1.TabIndex = 10;
            this.lblData1.Text = "-";
            // 
            // lblData0
            // 
            this.lblData0.AutoSize = true;
            this.lblData0.Location = new System.Drawing.Point(129, 47);
            this.lblData0.Name = "lblData0";
            this.lblData0.Size = new System.Drawing.Size(13, 18);
            this.lblData0.TabIndex = 9;
            this.lblData0.Text = "-";
            // 
            // btnRead
            // 
            this.btnRead.Location = new System.Drawing.Point(333, 23);
            this.btnRead.Name = "btnRead";
            this.btnRead.Size = new System.Drawing.Size(81, 34);
            this.btnRead.TabIndex = 8;
            this.btnRead.Text = "READ";
            this.btnRead.UseVisualStyleBackColor = true;
            this.btnRead.Click += new System.EventHandler(this.btnRead_Click);
            // 
            // txtReadAddress
            // 
            this.txtReadAddress.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtReadAddress.Location = new System.Drawing.Point(6, 41);
            this.txtReadAddress.MaxLength = 4;
            this.txtReadAddress.Name = "txtReadAddress";
            this.txtReadAddress.Size = new System.Drawing.Size(84, 24);
            this.txtReadAddress.TabIndex = 4;
            this.txtReadAddress.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDataByte_KeyPress);
            this.txtReadAddress.Leave += new System.EventHandler(this.Address_Leave);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(249, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 18);
            this.label5.TabIndex = 3;
            this.label5.Text = "Data 3";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(189, 20);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(51, 18);
            this.label6.TabIndex = 2;
            this.label6.Text = "Data 2";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(129, 20);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(51, 18);
            this.label7.TabIndex = 1;
            this.label7.Text = "Data 1";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 20);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(62, 18);
            this.label8.TabIndex = 0;
            this.label8.Text = "Address";
            // 
            // UCEEProm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpRead);
            this.Controls.Add(this.grpWrite);
            this.Name = "UCEEProm";
            this.Size = new System.Drawing.Size(433, 165);
            this.grpWrite.ResumeLayout(false);
            this.grpWrite.PerformLayout();
            this.grpRead.ResumeLayout(false);
            this.grpRead.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpWrite;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Button btnWrite;
        private System.Windows.Forms.TextBox txtWriteAddress;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox grpRead;
        private System.Windows.Forms.Button btnRead;
        private System.Windows.Forms.TextBox txtReadAddress;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblData2;
        private System.Windows.Forms.Label lblData1;
        private System.Windows.Forms.Label lblData0;
        internal System.Windows.Forms.TextBox txtDataByte3;
        internal System.Windows.Forms.TextBox txtDataByte2;
        internal System.Windows.Forms.TextBox txtDataByte1;
    }
}
