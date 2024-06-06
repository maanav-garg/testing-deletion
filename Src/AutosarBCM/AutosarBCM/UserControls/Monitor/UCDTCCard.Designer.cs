namespace AutosarBCM.UserControls.Monitor
{
    partial class UCDTCCard
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
            this.lblSubCtrlName = new System.Windows.Forms.Label();
            this.lbxValues = new System.Windows.Forms.ListBox();
            this.lblCtrlName = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblSubCtrlName
            // 
            this.lblSubCtrlName.AutoSize = true;
            this.lblSubCtrlName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubCtrlName.Location = new System.Drawing.Point(3, 27);
            this.lblSubCtrlName.Name = "lblSubCtrlName";
            this.lblSubCtrlName.Size = new System.Drawing.Size(47, 17);
            this.lblSubCtrlName.TabIndex = 0;
            this.lblSubCtrlName.Text = "name";
            // 
            // lbxValues
            // 
            this.lbxValues.BackColor = System.Drawing.SystemColors.Control;
            this.lbxValues.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbxValues.FormattingEnabled = true;
            this.lbxValues.ItemHeight = 15;
            this.lbxValues.Location = new System.Drawing.Point(3, 49);
            this.lbxValues.Name = "lbxValues";
            this.lbxValues.Size = new System.Drawing.Size(380, 64);
            this.lbxValues.TabIndex = 1;
            // 
            // lblCtrlName
            // 
            this.lblCtrlName.AutoSize = true;
            this.lblCtrlName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCtrlName.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblCtrlName.Location = new System.Drawing.Point(3, 4);
            this.lblCtrlName.Name = "lblCtrlName";
            this.lblCtrlName.Size = new System.Drawing.Size(43, 15);
            this.lblCtrlName.TabIndex = 2;
            this.lblCtrlName.Text = "name";
            // 
            // UCDTCCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblCtrlName);
            this.Controls.Add(this.lbxValues);
            this.Controls.Add(this.lblSubCtrlName);
            this.Name = "UCDTCCard";
            this.Size = new System.Drawing.Size(388, 121);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblSubCtrlName;
        private System.Windows.Forms.ListBox lbxValues;
        private System.Windows.Forms.Label lblCtrlName;
    }
}
