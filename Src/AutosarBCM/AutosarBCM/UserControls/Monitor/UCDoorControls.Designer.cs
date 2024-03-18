namespace AutosarBCM.UserControls.Monitor
{
    partial class UCDoorControls
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
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblUnlockDiag = new System.Windows.Forms.Label();
            this.btnUnlockDiagRead = new System.Windows.Forms.Button();
            this.numRevertTimeUnlock = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.btnUnlock = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblLockDiag = new System.Windows.Forms.Label();
            this.btnLockDiagRead = new System.Windows.Forms.Button();
            this.numRevertTimeLock = new System.Windows.Forms.NumericUpDown();
            this.lblRevertTime = new System.Windows.Forms.Label();
            this.btnLock = new System.Windows.Forms.Button();
            this.pcbAccepted = new System.Windows.Forms.PictureBox();
            this.pcbHighRisk = new System.Windows.Forms.PictureBox();
            this.pcbMediumRisk = new System.Windows.Forms.PictureBox();
            this.lblName = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRevertTimeUnlock)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRevertTimeLock)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcbAccepted)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcbHighRisk)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcbMediumRisk)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblUnlockDiag);
            this.groupBox2.Controls.Add(this.btnUnlockDiagRead);
            this.groupBox2.Controls.Add(this.numRevertTimeUnlock);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.btnUnlock);
            this.groupBox2.Location = new System.Drawing.Point(7, 113);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(261, 69);
            this.groupBox2.TabIndex = 21;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Unlock";
            // 
            // lblUnlockDiag
            // 
            this.lblUnlockDiag.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblUnlockDiag.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.2F);
            this.lblUnlockDiag.Location = new System.Drawing.Point(131, 40);
            this.lblUnlockDiag.Name = "lblUnlockDiag";
            this.lblUnlockDiag.Size = new System.Drawing.Size(120, 21);
            this.lblUnlockDiag.TabIndex = 20;
            this.lblUnlockDiag.Text = "-";
            this.lblUnlockDiag.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btnUnlockDiagRead
            // 
            this.btnUnlockDiagRead.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnUnlockDiagRead.ForeColor = System.Drawing.Color.Black;
            this.btnUnlockDiagRead.Location = new System.Drawing.Point(160, 13);
            this.btnUnlockDiagRead.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnUnlockDiagRead.Name = "btnUnlockDiagRead";
            this.btnUnlockDiagRead.Size = new System.Drawing.Size(61, 25);
            this.btnUnlockDiagRead.TabIndex = 19;
            this.btnUnlockDiagRead.Text = "READ";
            this.btnUnlockDiagRead.UseVisualStyleBackColor = true;
            this.btnUnlockDiagRead.Click += new System.EventHandler(this.btnUnlockDiagRead_Click);
            // 
            // numRevertTimeUnlock
            // 
            this.numRevertTimeUnlock.Location = new System.Drawing.Point(53, 37);
            this.numRevertTimeUnlock.Margin = new System.Windows.Forms.Padding(4);
            this.numRevertTimeUnlock.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numRevertTimeUnlock.Name = "numRevertTimeUnlock";
            this.numRevertTimeUnlock.Size = new System.Drawing.Size(71, 21);
            this.numRevertTimeUnlock.TabIndex = 16;
            this.numRevertTimeUnlock.ValueChanged += new System.EventHandler(this.numRevertTimeUnlock_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F);
            this.label2.Location = new System.Drawing.Point(51, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "Revert Time";
            // 
            // btnUnlock
            // 
            this.btnUnlock.BackColor = System.Drawing.Color.Transparent;
            this.btnUnlock.BackgroundImage = global::AutosarBCM.Properties.Resources.UnLcok;
            this.btnUnlock.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnUnlock.Location = new System.Drawing.Point(10, 23);
            this.btnUnlock.Name = "btnUnlock";
            this.btnUnlock.Size = new System.Drawing.Size(32, 32);
            this.btnUnlock.TabIndex = 1;
            this.btnUnlock.UseVisualStyleBackColor = false;
            this.btnUnlock.Click += new System.EventHandler(this.btnUnlock_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblLockDiag);
            this.groupBox1.Controls.Add(this.btnLockDiagRead);
            this.groupBox1.Controls.Add(this.numRevertTimeLock);
            this.groupBox1.Controls.Add(this.lblRevertTime);
            this.groupBox1.Controls.Add(this.btnLock);
            this.groupBox1.Location = new System.Drawing.Point(7, 39);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(261, 69);
            this.groupBox1.TabIndex = 20;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Lock";
            // 
            // lblLockDiag
            // 
            this.lblLockDiag.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblLockDiag.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.2F);
            this.lblLockDiag.Location = new System.Drawing.Point(131, 40);
            this.lblLockDiag.Name = "lblLockDiag";
            this.lblLockDiag.Size = new System.Drawing.Size(120, 21);
            this.lblLockDiag.TabIndex = 19;
            this.lblLockDiag.Text = "-";
            this.lblLockDiag.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btnLockDiagRead
            // 
            this.btnLockDiagRead.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnLockDiagRead.ForeColor = System.Drawing.Color.Black;
            this.btnLockDiagRead.Location = new System.Drawing.Point(160, 13);
            this.btnLockDiagRead.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnLockDiagRead.Name = "btnLockDiagRead";
            this.btnLockDiagRead.Size = new System.Drawing.Size(61, 25);
            this.btnLockDiagRead.TabIndex = 18;
            this.btnLockDiagRead.Text = "READ";
            this.btnLockDiagRead.UseVisualStyleBackColor = true;
            this.btnLockDiagRead.Click += new System.EventHandler(this.btnLockDiagRead_Click);
            // 
            // numRevertTimeLock
            // 
            this.numRevertTimeLock.Location = new System.Drawing.Point(54, 37);
            this.numRevertTimeLock.Margin = new System.Windows.Forms.Padding(4);
            this.numRevertTimeLock.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numRevertTimeLock.Name = "numRevertTimeLock";
            this.numRevertTimeLock.Size = new System.Drawing.Size(71, 21);
            this.numRevertTimeLock.TabIndex = 11;
            this.numRevertTimeLock.ValueChanged += new System.EventHandler(this.numRevertTimeLock_ValueChanged);
            // 
            // lblRevertTime
            // 
            this.lblRevertTime.AutoSize = true;
            this.lblRevertTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F);
            this.lblRevertTime.Location = new System.Drawing.Point(51, 20);
            this.lblRevertTime.Name = "lblRevertTime";
            this.lblRevertTime.Size = new System.Drawing.Size(62, 13);
            this.lblRevertTime.TabIndex = 12;
            this.lblRevertTime.Text = "Revert Time";
            // 
            // btnLock
            // 
            this.btnLock.BackColor = System.Drawing.Color.Transparent;
            this.btnLock.BackgroundImage = global::AutosarBCM.Properties.Resources.Lock;
            this.btnLock.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnLock.Location = new System.Drawing.Point(10, 25);
            this.btnLock.Name = "btnLock";
            this.btnLock.Size = new System.Drawing.Size(32, 32);
            this.btnLock.TabIndex = 0;
            this.btnLock.UseVisualStyleBackColor = false;
            this.btnLock.Click += new System.EventHandler(this.btnLock_Click);
            // 
            // pcbAccepted
            // 
            this.pcbAccepted.Image = global::AutosarBCM.Properties.Resources.pass;
            this.pcbAccepted.Location = new System.Drawing.Point(250, 18);
            this.pcbAccepted.Name = "pcbAccepted";
            this.pcbAccepted.Size = new System.Drawing.Size(18, 18);
            this.pcbAccepted.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pcbAccepted.TabIndex = 15;
            this.pcbAccepted.TabStop = false;
            this.pcbAccepted.Visible = false;
            this.pcbAccepted.MouseHover += new System.EventHandler(this.pcbAccepted_MouseHover);
            // 
            // pcbHighRisk
            // 
            this.pcbHighRisk.Image = global::AutosarBCM.Properties.Resources.StatusAnnotations_Warning_16xLG_color;
            this.pcbHighRisk.Location = new System.Drawing.Point(234, 18);
            this.pcbHighRisk.Name = "pcbHighRisk";
            this.pcbHighRisk.Size = new System.Drawing.Size(18, 18);
            this.pcbHighRisk.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pcbHighRisk.TabIndex = 14;
            this.pcbHighRisk.TabStop = false;
            this.pcbHighRisk.MouseHover += new System.EventHandler(this.pcbHighRisk_MouseHover);
            // 
            // pcbMediumRisk
            // 
            this.pcbMediumRisk.Image = global::AutosarBCM.Properties.Resources.Symbols_Alert_and_Warning_16xLG;
            this.pcbMediumRisk.Location = new System.Drawing.Point(234, 18);
            this.pcbMediumRisk.Name = "pcbMediumRisk";
            this.pcbMediumRisk.Size = new System.Drawing.Size(18, 18);
            this.pcbMediumRisk.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pcbMediumRisk.TabIndex = 13;
            this.pcbMediumRisk.TabStop = false;
            this.pcbMediumRisk.MouseHover += new System.EventHandler(this.pcbMediumRisk_MouseHover);
            // 
            // lblName
            // 
            this.lblName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.lblName.Location = new System.Drawing.Point(9, 14);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(219, 22);
            this.lblName.TabIndex = 2;
            this.lblName.Text = "label1";
            this.lblName.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblName.Click += new System.EventHandler(this.lblName_Click);
            // 
            // UCDoorControls
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pcbAccepted);
            this.Controls.Add(this.pcbHighRisk);
            this.Controls.Add(this.pcbMediumRisk);
            this.Controls.Add(this.lblName);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.Name = "UCDoorControls";
            this.Size = new System.Drawing.Size(276, 185);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRevertTimeUnlock)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRevertTimeLock)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcbAccepted)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcbHighRisk)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcbMediumRisk)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnLock;
        private System.Windows.Forms.Button btnUnlock;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.PictureBox pcbAccepted;
        private System.Windows.Forms.PictureBox pcbHighRisk;
        private System.Windows.Forms.PictureBox pcbMediumRisk;
        private System.Windows.Forms.NumericUpDown numRevertTimeLock;
        private System.Windows.Forms.Label lblRevertTime;
        private System.Windows.Forms.NumericUpDown numRevertTimeUnlock;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnLockDiagRead;
        private System.Windows.Forms.Button btnUnlockDiagRead;
        private System.Windows.Forms.Label lblLockDiag;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Label lblUnlockDiag;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}
