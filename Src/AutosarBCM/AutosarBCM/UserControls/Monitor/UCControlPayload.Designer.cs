namespace AutosarBCM.UserControls.Monitor
{
    partial class UCControlPayload
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
            this.lblName = new System.Windows.Forms.Label();
            this.chkSelected = new System.Windows.Forms.CheckBox();
            this.cmbValue = new System.Windows.Forms.ComboBox();
            this.pnlHexBytes = new System.Windows.Forms.Panel();
            this.txtDataByte4 = new System.Windows.Forms.TextBox();
            this.txtDataByte3 = new System.Windows.Forms.TextBox();
            this.txtDataByte2 = new System.Windows.Forms.TextBox();
            this.txtDataByte1 = new System.Windows.Forms.TextBox();
            this.pnlHexBytes.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblName.Location = new System.Drawing.Point(24, 10);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(41, 13);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "label1";
            // 
            // chkSelected
            // 
            this.chkSelected.AutoSize = true;
            this.chkSelected.Location = new System.Drawing.Point(3, 9);
            this.chkSelected.Name = "chkSelected";
            this.chkSelected.Size = new System.Drawing.Size(15, 14);
            this.chkSelected.TabIndex = 1;
            this.chkSelected.UseVisualStyleBackColor = true;
            // 
            // cmbValue
            // 
            this.cmbValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbValue.FormattingEnabled = true;
            this.cmbValue.Location = new System.Drawing.Point(211, 6);
            this.cmbValue.Name = "cmbValue";
            this.cmbValue.Size = new System.Drawing.Size(121, 21);
            this.cmbValue.TabIndex = 2;
            this.cmbValue.Visible = false;
            // 
            // pnlHexBytes
            // 
            this.pnlHexBytes.Controls.Add(this.txtDataByte4);
            this.pnlHexBytes.Controls.Add(this.txtDataByte3);
            this.pnlHexBytes.Controls.Add(this.txtDataByte2);
            this.pnlHexBytes.Controls.Add(this.txtDataByte1);
            this.pnlHexBytes.Location = new System.Drawing.Point(231, 0);
            this.pnlHexBytes.Name = "pnlHexBytes";
            this.pnlHexBytes.Size = new System.Drawing.Size(101, 34);
            this.pnlHexBytes.TabIndex = 3;
            this.pnlHexBytes.Visible = false;
            // 
            // txtDataByte4
            // 
            this.txtDataByte4.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDataByte4.Location = new System.Drawing.Point(75, 6);
            this.txtDataByte4.Margin = new System.Windows.Forms.Padding(2);
            this.txtDataByte4.Name = "txtDataByte4";
            this.txtDataByte4.Size = new System.Drawing.Size(22, 20);
            this.txtDataByte4.TabIndex = 78;
            this.txtDataByte4.Text = "00";
            this.txtDataByte4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtDataByte4.Click += new System.EventHandler(this.TextBoxOnClick);
            this.txtDataByte4.TextChanged += new System.EventHandler(this.txtDataByte_TextChanged);
            this.txtDataByte4.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxKeyPress);
            // 
            // txtDataByte3
            // 
            this.txtDataByte3.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDataByte3.Location = new System.Drawing.Point(51, 6);
            this.txtDataByte3.Margin = new System.Windows.Forms.Padding(2);
            this.txtDataByte3.Name = "txtDataByte3";
            this.txtDataByte3.Size = new System.Drawing.Size(22, 20);
            this.txtDataByte3.TabIndex = 77;
            this.txtDataByte3.Text = "00";
            this.txtDataByte3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtDataByte3.Click += new System.EventHandler(this.TextBoxOnClick);
            this.txtDataByte3.TextChanged += new System.EventHandler(this.txtDataByte_TextChanged);
            this.txtDataByte3.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxKeyPress);
            // 
            // txtDataByte2
            // 
            this.txtDataByte2.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDataByte2.Location = new System.Drawing.Point(27, 6);
            this.txtDataByte2.Margin = new System.Windows.Forms.Padding(2);
            this.txtDataByte2.Name = "txtDataByte2";
            this.txtDataByte2.Size = new System.Drawing.Size(22, 20);
            this.txtDataByte2.TabIndex = 76;
            this.txtDataByte2.Text = "00";
            this.txtDataByte2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtDataByte2.Click += new System.EventHandler(this.TextBoxOnClick);
            this.txtDataByte2.TextChanged += new System.EventHandler(this.txtDataByte_TextChanged);
            this.txtDataByte2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxKeyPress);
            // 
            // txtDataByte1
            // 
            this.txtDataByte1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDataByte1.Location = new System.Drawing.Point(2, 6);
            this.txtDataByte1.Margin = new System.Windows.Forms.Padding(2);
            this.txtDataByte1.Name = "txtDataByte1";
            this.txtDataByte1.Size = new System.Drawing.Size(22, 20);
            this.txtDataByte1.TabIndex = 75;
            this.txtDataByte1.Text = "00";
            this.txtDataByte1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtDataByte1.Click += new System.EventHandler(this.TextBoxOnClick);
            this.txtDataByte1.TextChanged += new System.EventHandler(this.txtDataByte_TextChanged);
            this.txtDataByte1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxKeyPress);
            // 
            // UCControlPayload
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlHexBytes);
            this.Controls.Add(this.cmbValue);
            this.Controls.Add(this.chkSelected);
            this.Controls.Add(this.lblName);
            this.Name = "UCControlPayload";
            this.Size = new System.Drawing.Size(335, 34);
            this.pnlHexBytes.ResumeLayout(false);
            this.pnlHexBytes.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.CheckBox chkSelected;
        private System.Windows.Forms.ComboBox cmbValue;
        private System.Windows.Forms.Panel pnlHexBytes;
        internal System.Windows.Forms.TextBox txtDataByte4;
        internal System.Windows.Forms.TextBox txtDataByte3;
        internal System.Windows.Forms.TextBox txtDataByte2;
        internal System.Windows.Forms.TextBox txtDataByte1;
    }
}
