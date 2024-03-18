namespace AutosarBCM.UserControls.Monitor
{
    partial class UCItem
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
            this.txtStatus = new System.Windows.Forms.NumericUpDown();
            this.btnSetStatus = new System.Windows.Forms.Button();
            this.grpStatus = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblTransmitted = new System.Windows.Forms.Label();
            this.lblReceived = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblDiff = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.txtStatus)).BeginInit();
            this.grpStatus.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblName.Location = new System.Drawing.Point(3, 4);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(52, 18);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "label1";
            this.lblName.Click += new System.EventHandler(this.lblName_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblStatus.Location = new System.Drawing.Point(211, 4);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(13, 18);
            this.lblStatus.TabIndex = 1;
            this.lblStatus.Text = "-";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtStatus
            // 
            this.txtStatus.Location = new System.Drawing.Point(5, 10);
            this.txtStatus.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.Size = new System.Drawing.Size(33, 20);
            this.txtStatus.TabIndex = 2;
            // 
            // btnSetStatus
            // 
            this.btnSetStatus.Location = new System.Drawing.Point(41, 10);
            this.btnSetStatus.Name = "btnSetStatus";
            this.btnSetStatus.Size = new System.Drawing.Size(37, 20);
            this.btnSetStatus.TabIndex = 3;
            this.btnSetStatus.Text = "Set";
            this.btnSetStatus.UseVisualStyleBackColor = true;
            this.btnSetStatus.Click += new System.EventHandler(this.btnSetStatus_Click);
            // 
            // grpStatus
            // 
            this.grpStatus.Controls.Add(this.txtStatus);
            this.grpStatus.Controls.Add(this.btnSetStatus);
            this.grpStatus.Location = new System.Drawing.Point(265, -6);
            this.grpStatus.Name = "grpStatus";
            this.grpStatus.Size = new System.Drawing.Size(84, 36);
            this.grpStatus.TabIndex = 4;
            this.grpStatus.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(268, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 20);
            this.label1.TabIndex = 5;
            this.label1.Text = "↑";
            // 
            // lblTransmitted
            // 
            this.lblTransmitted.AutoSize = true;
            this.lblTransmitted.Location = new System.Drawing.Point(281, 36);
            this.lblTransmitted.Name = "lblTransmitted";
            this.lblTransmitted.Size = new System.Drawing.Size(13, 13);
            this.lblTransmitted.TabIndex = 6;
            this.lblTransmitted.Text = "0";
            // 
            // lblReceived
            // 
            this.lblReceived.AutoSize = true;
            this.lblReceived.Location = new System.Drawing.Point(326, 36);
            this.lblReceived.Name = "lblReceived";
            this.lblReceived.Size = new System.Drawing.Size(13, 13);
            this.lblReceived.TabIndex = 7;
            this.lblReceived.Text = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.Location = new System.Drawing.Point(311, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 20);
            this.label2.TabIndex = 8;
            this.label2.Text = "↓";
            // 
            // lblDiff
            // 
            this.lblDiff.AutoSize = true;
            this.lblDiff.ForeColor = System.Drawing.Color.White;
            this.lblDiff.Location = new System.Drawing.Point(216, 35);
            this.lblDiff.Name = "lblDiff";
            this.lblDiff.Size = new System.Drawing.Size(0, 13);
            this.lblDiff.TabIndex = 9;
            // 
            // UCItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.lblDiff);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblReceived);
            this.Controls.Add(this.lblTransmitted);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.grpStatus);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.lblName);
            this.Name = "UCItem";
            this.Size = new System.Drawing.Size(355, 62);
            ((System.ComponentModel.ISupportInitialize)(this.txtStatus)).EndInit();
            this.grpStatus.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.NumericUpDown txtStatus;
        private System.Windows.Forms.Button btnSetStatus;
        private System.Windows.Forms.GroupBox grpStatus;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblTransmitted;
        private System.Windows.Forms.Label lblReceived;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblDiff;
    }
}
