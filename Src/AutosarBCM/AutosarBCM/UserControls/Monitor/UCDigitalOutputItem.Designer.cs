namespace AutosarBCM.UserControls.Monitor
{
    partial class UCDigitalOutputItem
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
            this.numTimeout = new System.Windows.Forms.NumericUpDown();
            this.btnSwitch = new System.Windows.Forms.Button();
            this.lblName = new System.Windows.Forms.Label();
            this.grpADC = new System.Windows.Forms.GroupBox();
            this.btnAdcRead = new System.Windows.Forms.Button();
            this.lblADC = new System.Windows.Forms.Label();
            this.grpDIAG = new System.Windows.Forms.GroupBox();
            this.btnDiagRead = new System.Windows.Forms.Button();
            this.lblDIAG = new System.Windows.Forms.Label();
            this.lblRevertTime = new System.Windows.Forms.Label();
            this.btnSetPwm = new System.Windows.Forms.Button();
            this.grpDuty = new System.Windows.Forms.GroupBox();
            this.btnMin = new System.Windows.Forms.Button();
            this.btnMax = new System.Windows.Forms.Button();
            this.nudDuty = new System.Windows.Forms.NumericUpDown();
            this.grpFrequency = new System.Windows.Forms.GroupBox();
            this.nudFreq = new System.Windows.Forms.NumericUpDown();
            this.pnlPWM = new System.Windows.Forms.Panel();
            this.pnlRead = new System.Windows.Forms.Panel();
            this.grpCurrent = new System.Windows.Forms.GroupBox();
            this.btnCurrentRead = new System.Windows.Forms.Button();
            this.lblCurrent = new System.Windows.Forms.Label();
            this.pnlDigital = new System.Windows.Forms.Panel();
            this.pcbAccepted = new System.Windows.Forms.PictureBox();
            this.pcbHighRisk = new System.Windows.Forms.PictureBox();
            this.pcbMediumRisk = new System.Windows.Forms.PictureBox();
            this.pnlUC = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlSend = new System.Windows.Forms.Panel();
            this.lblSendName = new System.Windows.Forms.Label();
            this.btnSend = new System.Windows.Forms.Button();
            this.ttValue = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.numTimeout)).BeginInit();
            this.grpADC.SuspendLayout();
            this.grpDIAG.SuspendLayout();
            this.grpDuty.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudDuty)).BeginInit();
            this.grpFrequency.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudFreq)).BeginInit();
            this.pnlPWM.SuspendLayout();
            this.pnlRead.SuspendLayout();
            this.grpCurrent.SuspendLayout();
            this.pnlDigital.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcbAccepted)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcbHighRisk)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcbMediumRisk)).BeginInit();
            this.pnlUC.SuspendLayout();
            this.pnlSend.SuspendLayout();
            this.SuspendLayout();
            // 
            // numTimeout
            // 
            this.numTimeout.Location = new System.Drawing.Point(250, 17);
            this.numTimeout.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numTimeout.Name = "numTimeout";
            this.numTimeout.Size = new System.Drawing.Size(47, 20);
            this.numTimeout.TabIndex = 2;
            this.numTimeout.ValueChanged += new System.EventHandler(this.numTimeout_ValueChanged);
            // 
            // btnSwitch
            // 
            this.btnSwitch.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnSwitch.ForeColor = System.Drawing.Color.Red;
            this.btnSwitch.Location = new System.Drawing.Point(200, 10);
            this.btnSwitch.Margin = new System.Windows.Forms.Padding(2);
            this.btnSwitch.Name = "btnSwitch";
            this.btnSwitch.Size = new System.Drawing.Size(46, 26);
            this.btnSwitch.TabIndex = 3;
            this.btnSwitch.Text = "OFF";
            this.btnSwitch.UseVisualStyleBackColor = true;
            this.btnSwitch.Click += new System.EventHandler(this.btnSwitch_Click);
            // 
            // lblName
            // 
            this.lblName.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblName.Location = new System.Drawing.Point(4, 13);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(193, 20);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "label1";
            this.lblName.Click += new System.EventHandler(this.lblName_Click);
            // 
            // grpADC
            // 
            this.grpADC.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.grpADC.Controls.Add(this.btnAdcRead);
            this.grpADC.Controls.Add(this.lblADC);
            this.grpADC.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.grpADC.Location = new System.Drawing.Point(101, -1);
            this.grpADC.Margin = new System.Windows.Forms.Padding(2);
            this.grpADC.Name = "grpADC";
            this.grpADC.Padding = new System.Windows.Forms.Padding(2);
            this.grpADC.Size = new System.Drawing.Size(104, 36);
            this.grpADC.TabIndex = 5;
            this.grpADC.TabStop = false;
            this.grpADC.Text = "ADC";
            // 
            // btnAdcRead
            // 
            this.btnAdcRead.Font = new System.Drawing.Font("Microsoft Sans Serif", 5.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnAdcRead.ForeColor = System.Drawing.Color.Black;
            this.btnAdcRead.Location = new System.Drawing.Point(66, 13);
            this.btnAdcRead.Margin = new System.Windows.Forms.Padding(2);
            this.btnAdcRead.Name = "btnAdcRead";
            this.btnAdcRead.Size = new System.Drawing.Size(37, 22);
            this.btnAdcRead.TabIndex = 8;
            this.btnAdcRead.Text = "READ";
            this.btnAdcRead.UseVisualStyleBackColor = true;
            this.btnAdcRead.Click += new System.EventHandler(this.btnAdcRead_Click);
            // 
            // lblADC
            // 
            this.lblADC.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblADC.Location = new System.Drawing.Point(13, 18);
            this.lblADC.Name = "lblADC";
            this.lblADC.Size = new System.Drawing.Size(56, 12);
            this.lblADC.TabIndex = 7;
            this.lblADC.Click += new System.EventHandler(this.lblName_Click);
            this.lblADC.MouseHover += new System.EventHandler(this.lblADC_MouseHover);
            // 
            // grpDIAG
            // 
            this.grpDIAG.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.grpDIAG.Controls.Add(this.btnDiagRead);
            this.grpDIAG.Controls.Add(this.lblDIAG);
            this.grpDIAG.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.grpDIAG.Location = new System.Drawing.Point(208, -1);
            this.grpDIAG.Margin = new System.Windows.Forms.Padding(2);
            this.grpDIAG.Name = "grpDIAG";
            this.grpDIAG.Padding = new System.Windows.Forms.Padding(2);
            this.grpDIAG.Size = new System.Drawing.Size(103, 34);
            this.grpDIAG.TabIndex = 6;
            this.grpDIAG.TabStop = false;
            this.grpDIAG.Text = "DIAG";
            // 
            // btnDiagRead
            // 
            this.btnDiagRead.Font = new System.Drawing.Font("Microsoft Sans Serif", 5.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnDiagRead.ForeColor = System.Drawing.Color.Black;
            this.btnDiagRead.Location = new System.Drawing.Point(64, 11);
            this.btnDiagRead.Margin = new System.Windows.Forms.Padding(2);
            this.btnDiagRead.Name = "btnDiagRead";
            this.btnDiagRead.Size = new System.Drawing.Size(37, 22);
            this.btnDiagRead.TabIndex = 9;
            this.btnDiagRead.Text = "READ";
            this.btnDiagRead.UseVisualStyleBackColor = true;
            this.btnDiagRead.Click += new System.EventHandler(this.btnDiagRead_Click);
            // 
            // lblDIAG
            // 
            this.lblDIAG.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblDIAG.Location = new System.Drawing.Point(2, 17);
            this.lblDIAG.Name = "lblDIAG";
            this.lblDIAG.Size = new System.Drawing.Size(59, 12);
            this.lblDIAG.TabIndex = 8;
            this.lblDIAG.Click += new System.EventHandler(this.lblName_Click);
            this.lblDIAG.MouseHover += new System.EventHandler(this.lblDIAG_MouseHover);
            // 
            // lblRevertTime
            // 
            this.lblRevertTime.AutoSize = true;
            this.lblRevertTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F);
            this.lblRevertTime.Location = new System.Drawing.Point(249, 6);
            this.lblRevertTime.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblRevertTime.Name = "lblRevertTime";
            this.lblRevertTime.Size = new System.Drawing.Size(48, 9);
            this.lblRevertTime.TabIndex = 7;
            this.lblRevertTime.Text = "Revert Time";
            // 
            // btnSetPwm
            // 
            this.btnSetPwm.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnSetPwm.ForeColor = System.Drawing.Color.Black;
            this.btnSetPwm.Location = new System.Drawing.Point(215, 11);
            this.btnSetPwm.Margin = new System.Windows.Forms.Padding(2);
            this.btnSetPwm.Name = "btnSetPwm";
            this.btnSetPwm.Size = new System.Drawing.Size(89, 26);
            this.btnSetPwm.TabIndex = 12;
            this.btnSetPwm.Text = "SET PWM";
            this.btnSetPwm.UseVisualStyleBackColor = true;
            this.btnSetPwm.Click += new System.EventHandler(this.btnSetPwm_Click);
            // 
            // grpDuty
            // 
            this.grpDuty.Controls.Add(this.btnMin);
            this.grpDuty.Controls.Add(this.btnMax);
            this.grpDuty.Controls.Add(this.nudDuty);
            this.grpDuty.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.grpDuty.Location = new System.Drawing.Point(94, 2);
            this.grpDuty.Margin = new System.Windows.Forms.Padding(2);
            this.grpDuty.Name = "grpDuty";
            this.grpDuty.Padding = new System.Windows.Forms.Padding(2);
            this.grpDuty.Size = new System.Drawing.Size(117, 38);
            this.grpDuty.TabIndex = 11;
            this.grpDuty.TabStop = false;
            this.grpDuty.Text = "Duty Percent";
            // 
            // btnMin
            // 
            this.btnMin.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnMin.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnMin.ForeColor = System.Drawing.Color.Black;
            this.btnMin.Location = new System.Drawing.Point(81, 23);
            this.btnMin.Margin = new System.Windows.Forms.Padding(2);
            this.btnMin.Name = "btnMin";
            this.btnMin.Size = new System.Drawing.Size(35, 15);
            this.btnMin.TabIndex = 11;
            this.btnMin.Text = "Min";
            this.btnMin.UseVisualStyleBackColor = true;
            this.btnMin.Click += new System.EventHandler(this.btnMin_Click);
            // 
            // btnMax
            // 
            this.btnMax.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnMax.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnMax.ForeColor = System.Drawing.Color.Black;
            this.btnMax.Location = new System.Drawing.Point(81, 7);
            this.btnMax.Margin = new System.Windows.Forms.Padding(2);
            this.btnMax.Name = "btnMax";
            this.btnMax.Size = new System.Drawing.Size(35, 15);
            this.btnMax.TabIndex = 10;
            this.btnMax.Text = "Max";
            this.btnMax.UseVisualStyleBackColor = true;
            this.btnMax.Click += new System.EventHandler(this.btnMax_Click);
            // 
            // nudDuty
            // 
            this.nudDuty.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.nudDuty.Location = new System.Drawing.Point(20, 15);
            this.nudDuty.Name = "nudDuty";
            this.nudDuty.Size = new System.Drawing.Size(56, 21);
            this.nudDuty.TabIndex = 3;
            this.nudDuty.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // grpFrequency
            // 
            this.grpFrequency.Controls.Add(this.nudFreq);
            this.grpFrequency.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.grpFrequency.Location = new System.Drawing.Point(5, 1);
            this.grpFrequency.Margin = new System.Windows.Forms.Padding(2);
            this.grpFrequency.Name = "grpFrequency";
            this.grpFrequency.Padding = new System.Windows.Forms.Padding(2);
            this.grpFrequency.Size = new System.Drawing.Size(85, 40);
            this.grpFrequency.TabIndex = 10;
            this.grpFrequency.TabStop = false;
            this.grpFrequency.Text = "Frequency";
            // 
            // nudFreq
            // 
            this.nudFreq.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.nudFreq.Location = new System.Drawing.Point(14, 15);
            this.nudFreq.Maximum = new decimal(new int[] {
            50000,
            0,
            0,
            0});
            this.nudFreq.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudFreq.Name = "nudFreq";
            this.nudFreq.Size = new System.Drawing.Size(56, 21);
            this.nudFreq.TabIndex = 3;
            this.nudFreq.Value = new decimal(new int[] {
            200,
            0,
            0,
            0});
            // 
            // pnlPWM
            // 
            this.pnlPWM.AutoSize = true;
            this.pnlPWM.Controls.Add(this.btnSetPwm);
            this.pnlPWM.Controls.Add(this.grpDuty);
            this.pnlPWM.Controls.Add(this.grpFrequency);
            this.pnlPWM.Location = new System.Drawing.Point(2, 46);
            this.pnlPWM.Margin = new System.Windows.Forms.Padding(2);
            this.pnlPWM.Name = "pnlPWM";
            this.pnlPWM.Size = new System.Drawing.Size(306, 43);
            this.pnlPWM.TabIndex = 13;
            // 
            // pnlRead
            // 
            this.pnlRead.AutoSize = true;
            this.pnlRead.Controls.Add(this.grpCurrent);
            this.pnlRead.Controls.Add(this.grpADC);
            this.pnlRead.Controls.Add(this.grpDIAG);
            this.pnlRead.Location = new System.Drawing.Point(2, 93);
            this.pnlRead.Margin = new System.Windows.Forms.Padding(2);
            this.pnlRead.Name = "pnlRead";
            this.pnlRead.Size = new System.Drawing.Size(313, 37);
            this.pnlRead.TabIndex = 14;
            // 
            // grpCurrent
            // 
            this.grpCurrent.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.grpCurrent.Controls.Add(this.btnCurrentRead);
            this.grpCurrent.Controls.Add(this.lblCurrent);
            this.grpCurrent.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.grpCurrent.Location = new System.Drawing.Point(3, -1);
            this.grpCurrent.Margin = new System.Windows.Forms.Padding(2);
            this.grpCurrent.Name = "grpCurrent";
            this.grpCurrent.Padding = new System.Windows.Forms.Padding(2);
            this.grpCurrent.Size = new System.Drawing.Size(96, 36);
            this.grpCurrent.TabIndex = 9;
            this.grpCurrent.TabStop = false;
            this.grpCurrent.Text = "Current";
            // 
            // btnCurrentRead
            // 
            this.btnCurrentRead.Font = new System.Drawing.Font("Microsoft Sans Serif", 5.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnCurrentRead.ForeColor = System.Drawing.Color.Black;
            this.btnCurrentRead.Location = new System.Drawing.Point(59, 13);
            this.btnCurrentRead.Margin = new System.Windows.Forms.Padding(2);
            this.btnCurrentRead.Name = "btnCurrentRead";
            this.btnCurrentRead.Size = new System.Drawing.Size(37, 22);
            this.btnCurrentRead.TabIndex = 8;
            this.btnCurrentRead.Text = "READ";
            this.btnCurrentRead.UseVisualStyleBackColor = true;
            this.btnCurrentRead.Click += new System.EventHandler(this.btnCurrentRead_Click);
            // 
            // lblCurrent
            // 
            this.lblCurrent.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblCurrent.Location = new System.Drawing.Point(5, 17);
            this.lblCurrent.Name = "lblCurrent";
            this.lblCurrent.Size = new System.Drawing.Size(66, 12);
            this.lblCurrent.TabIndex = 7;
            this.lblCurrent.MouseHover += new System.EventHandler(this.lblCurrent_MouseHover);
            // 
            // pnlDigital
            // 
            this.pnlDigital.Controls.Add(this.pcbAccepted);
            this.pnlDigital.Controls.Add(this.pcbHighRisk);
            this.pnlDigital.Controls.Add(this.pcbMediumRisk);
            this.pnlDigital.Controls.Add(this.lblName);
            this.pnlDigital.Controls.Add(this.numTimeout);
            this.pnlDigital.Controls.Add(this.btnSwitch);
            this.pnlDigital.Controls.Add(this.lblRevertTime);
            this.pnlDigital.Location = new System.Drawing.Point(2, 2);
            this.pnlDigital.Margin = new System.Windows.Forms.Padding(2);
            this.pnlDigital.Name = "pnlDigital";
            this.pnlDigital.Size = new System.Drawing.Size(315, 40);
            this.pnlDigital.TabIndex = 15;
            // 
            // pcbAccepted
            // 
            this.pcbAccepted.Image = global::AutosarBCM.Properties.Resources.pass;
            this.pcbAccepted.Location = new System.Drawing.Point(301, 24);
            this.pcbAccepted.Margin = new System.Windows.Forms.Padding(2);
            this.pcbAccepted.Name = "pcbAccepted";
            this.pcbAccepted.Size = new System.Drawing.Size(12, 13);
            this.pcbAccepted.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pcbAccepted.TabIndex = 10;
            this.pcbAccepted.TabStop = false;
            this.pcbAccepted.Visible = false;
            this.pcbAccepted.MouseHover += new System.EventHandler(this.pcbAccepted_MouseHover);
            // 
            // pcbHighRisk
            // 
            this.pcbHighRisk.Image = global::AutosarBCM.Properties.Resources.StatusAnnotations_Warning_16xLG_color;
            this.pcbHighRisk.Location = new System.Drawing.Point(301, 6);
            this.pcbHighRisk.Margin = new System.Windows.Forms.Padding(2);
            this.pcbHighRisk.Name = "pcbHighRisk";
            this.pcbHighRisk.Size = new System.Drawing.Size(12, 13);
            this.pcbHighRisk.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pcbHighRisk.TabIndex = 9;
            this.pcbHighRisk.TabStop = false;
            this.pcbHighRisk.MouseHover += new System.EventHandler(this.pcbHighRisk_MouseHover);
            // 
            // pcbMediumRisk
            // 
            this.pcbMediumRisk.Image = global::AutosarBCM.Properties.Resources.Symbols_Alert_and_Warning_16xLG;
            this.pcbMediumRisk.Location = new System.Drawing.Point(301, 6);
            this.pcbMediumRisk.Margin = new System.Windows.Forms.Padding(2);
            this.pcbMediumRisk.Name = "pcbMediumRisk";
            this.pcbMediumRisk.Size = new System.Drawing.Size(12, 13);
            this.pcbMediumRisk.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pcbMediumRisk.TabIndex = 8;
            this.pcbMediumRisk.TabStop = false;
            this.pcbMediumRisk.MouseHover += new System.EventHandler(this.pcbMediumRisk_MouseHover);
            // 
            // pnlUC
            // 
            this.pnlUC.AutoSize = true;
            this.pnlUC.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnlUC.Controls.Add(this.pnlDigital);
            this.pnlUC.Controls.Add(this.pnlPWM);
            this.pnlUC.Controls.Add(this.pnlRead);
            this.pnlUC.Controls.Add(this.pnlSend);
            this.pnlUC.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.pnlUC.Location = new System.Drawing.Point(-1, 2);
            this.pnlUC.Margin = new System.Windows.Forms.Padding(2);
            this.pnlUC.Name = "pnlUC";
            this.pnlUC.Size = new System.Drawing.Size(319, 170);
            this.pnlUC.TabIndex = 16;
            // 
            // pnlSend
            // 
            this.pnlSend.Controls.Add(this.lblSendName);
            this.pnlSend.Controls.Add(this.btnSend);
            this.pnlSend.Location = new System.Drawing.Point(2, 134);
            this.pnlSend.Margin = new System.Windows.Forms.Padding(2);
            this.pnlSend.Name = "pnlSend";
            this.pnlSend.Size = new System.Drawing.Size(302, 34);
            this.pnlSend.TabIndex = 16;
            this.pnlSend.Visible = false;
            // 
            // lblSendName
            // 
            this.lblSendName.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblSendName.Location = new System.Drawing.Point(4, 7);
            this.lblSendName.Name = "lblSendName";
            this.lblSendName.Size = new System.Drawing.Size(193, 20);
            this.lblSendName.TabIndex = 1;
            this.lblSendName.Text = "label1";
            this.lblSendName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblSendName.Click += new System.EventHandler(this.lblSendName_Click);
            // 
            // btnSend
            // 
            this.btnSend.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.btnSend.Location = new System.Drawing.Point(224, 6);
            this.btnSend.Margin = new System.Windows.Forms.Padding(2);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(63, 23);
            this.btnSend.TabIndex = 0;
            this.btnSend.Text = "SEND";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // UCDigitalOutputItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.pnlUC);
            this.Name = "UCDigitalOutputItem";
            this.Size = new System.Drawing.Size(319, 171);
            ((System.ComponentModel.ISupportInitialize)(this.numTimeout)).EndInit();
            this.grpADC.ResumeLayout(false);
            this.grpDIAG.ResumeLayout(false);
            this.grpDuty.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudDuty)).EndInit();
            this.grpFrequency.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudFreq)).EndInit();
            this.pnlPWM.ResumeLayout(false);
            this.pnlRead.ResumeLayout(false);
            this.grpCurrent.ResumeLayout(false);
            this.pnlDigital.ResumeLayout(false);
            this.pnlDigital.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcbAccepted)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcbHighRisk)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcbMediumRisk)).EndInit();
            this.pnlUC.ResumeLayout(false);
            this.pnlUC.PerformLayout();
            this.pnlSend.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.NumericUpDown numTimeout;
        private System.Windows.Forms.Button btnSwitch;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.GroupBox grpADC;
        private System.Windows.Forms.GroupBox grpDIAG;
        private System.Windows.Forms.Label lblADC;
        private System.Windows.Forms.Label lblDIAG;
        private System.Windows.Forms.Label lblRevertTime;
        private System.Windows.Forms.Button btnAdcRead;
        private System.Windows.Forms.Button btnDiagRead;
        private System.Windows.Forms.Button btnSetPwm;
        private System.Windows.Forms.GroupBox grpDuty;
        private System.Windows.Forms.NumericUpDown nudDuty;
        private System.Windows.Forms.GroupBox grpFrequency;
        private System.Windows.Forms.NumericUpDown nudFreq;
        private System.Windows.Forms.Panel pnlPWM;
        private System.Windows.Forms.Panel pnlRead;
        private System.Windows.Forms.Panel pnlDigital;
        private System.Windows.Forms.FlowLayoutPanel pnlUC;
        private System.Windows.Forms.Panel pnlSend;
        private System.Windows.Forms.Label lblSendName;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.PictureBox pcbMediumRisk;
        private System.Windows.Forms.PictureBox pcbHighRisk;
        private System.Windows.Forms.PictureBox pcbAccepted;
        private System.Windows.Forms.ToolTip ttValue;
        private System.Windows.Forms.Button btnMax;
        private System.Windows.Forms.Button btnMin;
        private System.Windows.Forms.GroupBox grpCurrent;
        private System.Windows.Forms.Button btnCurrentRead;
        private System.Windows.Forms.Label lblCurrent;
    }
}
