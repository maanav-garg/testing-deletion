namespace AutosarBCM.UserControls.Monitor
{
    partial class UCEmcReadOnlyItem
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
            this.lblDtcStatus = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblWriteStatus = new System.Windows.Forms.Label();
            this.lblParent = new System.Windows.Forms.Label();
            this.lblLoadFeature = new System.Windows.Forms.Label();
            this.lblFunctionFeature = new System.Windows.Forms.Label();
            this.lblLastDtcTimeText = new System.Windows.Forms.Label();
            this.lblLastDtcTime = new System.Windows.Forms.Label();
            this.lblLastStatusTimeText = new System.Windows.Forms.Label();
            this.lblLastStatusTime = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblName.Location = new System.Drawing.Point(3, 25);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(52, 17);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "label1";
            this.lblName.Click += new System.EventHandler(this.lblName_Click);
            // 
            // lblDtcStatus
            // 
            this.lblDtcStatus.AutoSize = true;
            this.lblDtcStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblDtcStatus.Location = new System.Drawing.Point(65, 77);
            this.lblDtcStatus.Name = "lblDtcStatus";
            this.lblDtcStatus.Size = new System.Drawing.Size(10, 13);
            this.lblDtcStatus.TabIndex = 1;
            this.lblDtcStatus.Text = "-";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "DTC Status:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 53);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "Status:";
            // 
            // lblWriteStatus
            // 
            this.lblWriteStatus.AutoSize = true;
            this.lblWriteStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblWriteStatus.Location = new System.Drawing.Point(40, 51);
            this.lblWriteStatus.Name = "lblWriteStatus";
            this.lblWriteStatus.Size = new System.Drawing.Size(11, 15);
            this.lblWriteStatus.TabIndex = 16;
            this.lblWriteStatus.Text = "-";
            // 
            // lblParent
            // 
            this.lblParent.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblParent.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblParent.Location = new System.Drawing.Point(3, 1);
            this.lblParent.Name = "lblParent";
            this.lblParent.Size = new System.Drawing.Size(100, 25);
            this.lblParent.TabIndex = 18;
            this.lblParent.Text = "label1";
            // 
            // lblLoadFeature
            // 
            this.lblLoadFeature.AutoSize = true;
            this.lblLoadFeature.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblLoadFeature.Location = new System.Drawing.Point(220, 1);
            this.lblLoadFeature.Name = "lblLoadFeature";
            this.lblLoadFeature.Size = new System.Drawing.Size(19, 17);
            this.lblLoadFeature.TabIndex = 19;
            this.lblLoadFeature.Text = " -";
            // 
            // lblFunctionFeature
            // 
            this.lblFunctionFeature.AutoSize = true;
            this.lblFunctionFeature.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblFunctionFeature.Location = new System.Drawing.Point(269, 1);
            this.lblFunctionFeature.Name = "lblFunctionFeature";
            this.lblFunctionFeature.Size = new System.Drawing.Size(19, 17);
            this.lblFunctionFeature.TabIndex = 20;
            this.lblFunctionFeature.Text = " -";
            // 
            // lblLastDtcTimeText
            // 
            this.lblLastDtcTimeText.AutoSize = true;
            this.lblLastDtcTimeText.Location = new System.Drawing.Point(104, 77);
            this.lblLastDtcTimeText.Name = "lblLastDtcTimeText";
            this.lblLastDtcTimeText.Size = new System.Drawing.Size(81, 13);
            this.lblLastDtcTimeText.TabIndex = 21;
            this.lblLastDtcTimeText.Text = "Last DTC Time:";
            // 
            // lblLastDtcTime
            // 
            this.lblLastDtcTime.AutoSize = true;
            this.lblLastDtcTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblLastDtcTime.Location = new System.Drawing.Point(184, 77);
            this.lblLastDtcTime.Name = "lblLastDtcTime";
            this.lblLastDtcTime.Size = new System.Drawing.Size(10, 13);
            this.lblLastDtcTime.TabIndex = 22;
            this.lblLastDtcTime.Text = "-";
            // 
            // lblLastStatusTimeText
            // 
            this.lblLastStatusTimeText.AutoSize = true;
            this.lblLastStatusTimeText.Location = new System.Drawing.Point(104, 53);
            this.lblLastStatusTimeText.Name = "lblLastStatusTimeText";
            this.lblLastStatusTimeText.Size = new System.Drawing.Size(89, 13);
            this.lblLastStatusTimeText.TabIndex = 23;
            this.lblLastStatusTimeText.Text = "Last Status Time:";
            // 
            // lblLastStatusTime
            // 
            this.lblLastStatusTime.AutoSize = true;
            this.lblLastStatusTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblLastStatusTime.Location = new System.Drawing.Point(192, 51);
            this.lblLastStatusTime.Name = "lblLastStatusTime";
            this.lblLastStatusTime.Size = new System.Drawing.Size(11, 15);
            this.lblLastStatusTime.TabIndex = 24;
            this.lblLastStatusTime.Text = "-";
            // 
            // UCEmcReadOnlyItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.lblLastStatusTime);
            this.Controls.Add(this.lblLastStatusTimeText);
            this.Controls.Add(this.lblLastDtcTime);
            this.Controls.Add(this.lblLastDtcTimeText);
            this.Controls.Add(this.lblFunctionFeature);
            this.Controls.Add(this.lblLoadFeature);
            this.Controls.Add(this.lblParent);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblWriteStatus);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblDtcStatus);
            this.Controls.Add(this.lblName);
            this.Name = "UCEmcReadOnlyItem";
            this.Size = new System.Drawing.Size(313, 100);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblDtcStatus;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblWriteStatus;
        private System.Windows.Forms.Label lblParent;
        private System.Windows.Forms.Label lblLoadFeature;
        private System.Windows.Forms.Label lblFunctionFeature;
        private System.Windows.Forms.Label lblLastDtcTimeText;
        private System.Windows.Forms.Label lblLastDtcTime;
        private System.Windows.Forms.Label lblLastStatusTimeText;
        private System.Windows.Forms.Label lblLastStatusTime;
    }
}
