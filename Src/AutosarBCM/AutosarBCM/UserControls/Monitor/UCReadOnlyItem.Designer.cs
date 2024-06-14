namespace AutosarBCM.UserControls.Monitor
{
    partial class UCReadOnlyItem
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
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblDiff = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblReceived = new System.Windows.Forms.Label();
            this.lblTransmitted = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblWriteStatus = new System.Windows.Forms.Label();
            this.lblParent = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblName.Location = new System.Drawing.Point(4, 31);
            this.lblName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(59, 20);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "label1";
            this.lblName.Click += new System.EventHandler(this.lblName_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblStatus.Location = new System.Drawing.Point(96, 60);
            this.lblStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(13, 18);
            this.lblStatus.TabIndex = 1;
            this.lblStatus.Text = "-";
            // 
            // lblDiff
            // 
            this.lblDiff.AutoSize = true;
            this.lblDiff.BackColor = System.Drawing.Color.Green;
            this.lblDiff.ForeColor = System.Drawing.Color.Transparent;
            this.lblDiff.Location = new System.Drawing.Point(266, 5);
            this.lblDiff.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDiff.Name = "lblDiff";
            this.lblDiff.Size = new System.Drawing.Size(11, 16);
            this.lblDiff.TabIndex = 14;
            this.lblDiff.Text = "-";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.Location = new System.Drawing.Point(383, -2);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(22, 25);
            this.label2.TabIndex = 13;
            this.label2.Text = "↓";
            // 
            // lblReceived
            // 
            this.lblReceived.AutoSize = true;
            this.lblReceived.Location = new System.Drawing.Point(403, 6);
            this.lblReceived.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblReceived.Name = "lblReceived";
            this.lblReceived.Size = new System.Drawing.Size(14, 16);
            this.lblReceived.TabIndex = 12;
            this.lblReceived.Text = "0";
            // 
            // lblTransmitted
            // 
            this.lblTransmitted.AutoSize = true;
            this.lblTransmitted.Location = new System.Drawing.Point(343, 6);
            this.lblTransmitted.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTransmitted.Name = "lblTransmitted";
            this.lblTransmitted.Size = new System.Drawing.Size(14, 16);
            this.lblTransmitted.TabIndex = 11;
            this.lblTransmitted.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(325, -2);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(22, 25);
            this.label1.TabIndex = 10;
            this.label1.Text = "↑";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 62);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 16);
            this.label3.TabIndex = 15;
            this.label3.Text = "Read Status:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(266, 62);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 16);
            this.label4.TabIndex = 17;
            this.label4.Text = "Write Status:";
            // 
            // lblWriteStatus
            // 
            this.lblWriteStatus.AutoSize = true;
            this.lblWriteStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblWriteStatus.Location = new System.Drawing.Point(355, 60);
            this.lblWriteStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblWriteStatus.Name = "lblWriteStatus";
            this.lblWriteStatus.Size = new System.Drawing.Size(13, 18);
            this.lblWriteStatus.TabIndex = 16;
            this.lblWriteStatus.Text = "-";
            this.lblWriteStatus.Click += new System.EventHandler(this.lblWriteStatus_Click);
            // 
            // lblParent
            // 
            this.lblParent.AutoSize = true;
            this.lblParent.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblParent.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblParent.Location = new System.Drawing.Point(4, 1);
            this.lblParent.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblParent.Name = "lblParent";
            this.lblParent.Size = new System.Drawing.Size(52, 17);
            this.lblParent.TabIndex = 18;
            this.lblParent.Text = "label1";
            // 
            // UCReadOnlyItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.lblParent);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblWriteStatus);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblDiff);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblReceived);
            this.Controls.Add(this.lblTransmitted);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.lblName);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "UCReadOnlyItem";
            this.Size = new System.Drawing.Size(439, 82);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblDiff;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblReceived;
        private System.Windows.Forms.Label lblTransmitted;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblWriteStatus;
        private System.Windows.Forms.Label lblParent;
    }
}
