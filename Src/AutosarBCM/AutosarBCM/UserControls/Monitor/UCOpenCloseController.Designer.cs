namespace AutosarBCM.UserControls.Monitor
{
    partial class UCOpenCloseController
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
            this.btnUp = new System.Windows.Forms.Button();
            this.btnDown = new System.Windows.Forms.Button();
            this.lblAdc = new System.Windows.Forms.Label();
            this.lblOpenDiag = new System.Windows.Forms.Label();
            this.btnReadOpenDiag = new System.Windows.Forms.Button();
            this.btnReadAdc = new System.Windows.Forms.Button();
            this.btnReadCloseDiag = new System.Windows.Forms.Button();
            this.lblCloseDiag = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblOpen = new System.Windows.Forms.Label();
            this.lblClose = new System.Windows.Forms.Label();
            this.numRevertTime = new System.Windows.Forms.NumericUpDown();
            this.lblRevertTime = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnReadCurrent = new System.Windows.Forms.Button();
            this.lblCurrent = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numRevertTime)).BeginInit();
            this.SuspendLayout();
            // 
            // lblName
            // 
            this.lblName.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.lblName.Location = new System.Drawing.Point(5, 3);
            this.lblName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(266, 24);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "lblName";
            this.lblName.Click += new System.EventHandler(this.lblName_Click);
            // 
            // btnUp
            // 
            this.btnUp.BackColor = System.Drawing.Color.Transparent;
            this.btnUp.BackgroundImage = global::AutosarBCM.Properties.Resources.ArrowUp;
            this.btnUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnUp.Location = new System.Drawing.Point(11, 40);
            this.btnUp.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(34, 37);
            this.btnUp.TabIndex = 2;
            this.btnUp.UseVisualStyleBackColor = false;
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // btnDown
            // 
            this.btnDown.BackColor = System.Drawing.Color.Transparent;
            this.btnDown.BackgroundImage = global::AutosarBCM.Properties.Resources.ArrowDown;
            this.btnDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDown.Location = new System.Drawing.Point(11, 96);
            this.btnDown.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(34, 37);
            this.btnDown.TabIndex = 4;
            this.btnDown.UseVisualStyleBackColor = false;
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // lblAdc
            // 
            this.lblAdc.Location = new System.Drawing.Point(210, 46);
            this.lblAdc.Name = "lblAdc";
            this.lblAdc.Size = new System.Drawing.Size(118, 20);
            this.lblAdc.TabIndex = 13;
            this.lblAdc.Text = " -";
            // 
            // lblOpenDiag
            // 
            this.lblOpenDiag.Location = new System.Drawing.Point(210, 93);
            this.lblOpenDiag.Name = "lblOpenDiag";
            this.lblOpenDiag.Size = new System.Drawing.Size(118, 20);
            this.lblOpenDiag.TabIndex = 14;
            this.lblOpenDiag.Text = " -";
            // 
            // btnReadOpenDiag
            // 
            this.btnReadOpenDiag.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.btnReadOpenDiag.Location = new System.Drawing.Point(147, 92);
            this.btnReadOpenDiag.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnReadOpenDiag.Name = "btnReadOpenDiag";
            this.btnReadOpenDiag.Size = new System.Drawing.Size(42, 20);
            this.btnReadOpenDiag.TabIndex = 15;
            this.btnReadOpenDiag.Text = "Read";
            this.btnReadOpenDiag.UseVisualStyleBackColor = true;
            this.btnReadOpenDiag.Click += new System.EventHandler(this.btnReadOpenDiag_Click);
            // 
            // btnReadAdc
            // 
            this.btnReadAdc.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.btnReadAdc.Location = new System.Drawing.Point(147, 46);
            this.btnReadAdc.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnReadAdc.Name = "btnReadAdc";
            this.btnReadAdc.Size = new System.Drawing.Size(42, 20);
            this.btnReadAdc.TabIndex = 16;
            this.btnReadAdc.Text = "Read";
            this.btnReadAdc.UseVisualStyleBackColor = true;
            this.btnReadAdc.Click += new System.EventHandler(this.btnReadAdc_Click);
            // 
            // btnReadCloseDiag
            // 
            this.btnReadCloseDiag.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.btnReadCloseDiag.Location = new System.Drawing.Point(147, 116);
            this.btnReadCloseDiag.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnReadCloseDiag.Name = "btnReadCloseDiag";
            this.btnReadCloseDiag.Size = new System.Drawing.Size(42, 20);
            this.btnReadCloseDiag.TabIndex = 18;
            this.btnReadCloseDiag.Text = "Read";
            this.btnReadCloseDiag.UseVisualStyleBackColor = true;
            this.btnReadCloseDiag.Click += new System.EventHandler(this.btnReadCloseDiag_Click);
            // 
            // lblCloseDiag
            // 
            this.lblCloseDiag.Location = new System.Drawing.Point(210, 117);
            this.lblCloseDiag.Name = "lblCloseDiag";
            this.lblCloseDiag.Size = new System.Drawing.Size(118, 20);
            this.lblCloseDiag.TabIndex = 17;
            this.lblCloseDiag.Text = " -";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(101, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 19;
            this.label2.Text = "ADC";
            // 
            // lblOpen
            // 
            this.lblOpen.AutoSize = true;
            this.lblOpen.Location = new System.Drawing.Point(72, 96);
            this.lblOpen.Name = "lblOpen";
            this.lblOpen.Size = new System.Drawing.Size(58, 13);
            this.lblOpen.TabIndex = 20;
            this.lblOpen.Text = "Open Diag";
            // 
            // lblClose
            // 
            this.lblClose.AutoSize = true;
            this.lblClose.Location = new System.Drawing.Point(72, 120);
            this.lblClose.Name = "lblClose";
            this.lblClose.Size = new System.Drawing.Size(58, 13);
            this.lblClose.TabIndex = 21;
            this.lblClose.Text = "Close Diag";
            // 
            // numRevertTime
            // 
            this.numRevertTime.Location = new System.Drawing.Point(281, 17);
            this.numRevertTime.Margin = new System.Windows.Forms.Padding(5);
            this.numRevertTime.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numRevertTime.Name = "numRevertTime";
            this.numRevertTime.Size = new System.Drawing.Size(95, 20);
            this.numRevertTime.TabIndex = 25;
            this.numRevertTime.ValueChanged += new System.EventHandler(this.numRevertTime_ValueChanged);
            // 
            // lblRevertTime
            // 
            this.lblRevertTime.AutoSize = true;
            this.lblRevertTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F);
            this.lblRevertTime.Location = new System.Drawing.Point(291, -1);
            this.lblRevertTime.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRevertTime.Name = "lblRevertTime";
            this.lblRevertTime.Size = new System.Drawing.Size(62, 13);
            this.lblRevertTime.TabIndex = 26;
            this.lblRevertTime.Text = "Revert Time";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(89, 73);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 29;
            this.label1.Text = "Current";
            // 
            // btnReadCurrent
            // 
            this.btnReadCurrent.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.btnReadCurrent.Location = new System.Drawing.Point(147, 69);
            this.btnReadCurrent.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnReadCurrent.Name = "btnReadCurrent";
            this.btnReadCurrent.Size = new System.Drawing.Size(42, 20);
            this.btnReadCurrent.TabIndex = 28;
            this.btnReadCurrent.Text = "Read";
            this.btnReadCurrent.UseVisualStyleBackColor = true;
            this.btnReadCurrent.Click += new System.EventHandler(this.btnReadCurrent_Click);
            // 
            // lblCurrent
            // 
            this.lblCurrent.Location = new System.Drawing.Point(210, 69);
            this.lblCurrent.Name = "lblCurrent";
            this.lblCurrent.Size = new System.Drawing.Size(118, 20);
            this.lblCurrent.TabIndex = 27;
            this.lblCurrent.Text = " -";
            // 
            // UCOpenCloseController
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnReadCurrent);
            this.Controls.Add(this.lblCurrent);
            this.Controls.Add(this.numRevertTime);
            this.Controls.Add(this.lblRevertTime);
            this.Controls.Add(this.lblClose);
            this.Controls.Add(this.lblOpen);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnReadCloseDiag);
            this.Controls.Add(this.lblCloseDiag);
            this.Controls.Add(this.btnReadAdc);
            this.Controls.Add(this.btnReadOpenDiag);
            this.Controls.Add(this.lblOpenDiag);
            this.Controls.Add(this.lblAdc);
            this.Controls.Add(this.btnDown);
            this.Controls.Add(this.btnUp);
            this.Controls.Add(this.lblName);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "UCOpenCloseController";
            this.Size = new System.Drawing.Size(375, 145);
            ((System.ComponentModel.ISupportInitialize)(this.numRevertTime)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Button btnUp;
        private System.Windows.Forms.Button btnDown;
        private System.Windows.Forms.Label lblAdc;
        private System.Windows.Forms.Label lblOpenDiag;
        private System.Windows.Forms.Button btnReadOpenDiag;
        private System.Windows.Forms.Button btnReadAdc;
        private System.Windows.Forms.Button btnReadCloseDiag;
        private System.Windows.Forms.Label lblCloseDiag;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblOpen;
        private System.Windows.Forms.Label lblClose;
        private System.Windows.Forms.NumericUpDown numRevertTime;
        private System.Windows.Forms.Label lblRevertTime;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnReadCurrent;
        private System.Windows.Forms.Label lblCurrent;
    }
}
