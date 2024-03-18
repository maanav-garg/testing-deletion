namespace AutosarBCM.Forms.Monitor
{
    partial class FormMonitorGenericOutput
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlMonitorOutput = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.lblItemName = new System.Windows.Forms.Label();
            this.lblStateHeader = new System.Windows.Forms.Label();
            this.lblState = new System.Windows.Forms.Label();
            this.lblRevertTimeHeader = new System.Windows.Forms.Label();
            this.lblRevertTime = new System.Windows.Forms.Label();
            this.lblAdcDataHeader = new System.Windows.Forms.Label();
            this.lblAdcData = new System.Windows.Forms.Label();
            this.lblAdcValueHeader = new System.Windows.Forms.Label();
            this.lblAdcValue = new System.Windows.Forms.Label();
            this.lblDiagDataHeader = new System.Windows.Forms.Label();
            this.lblDiagData = new System.Windows.Forms.Label();
            this.lblDiagValueHeader = new System.Windows.Forms.Label();
            this.lblDiagValue = new System.Windows.Forms.Label();
            this.lblCurrentDataHeader = new System.Windows.Forms.Label();
            this.lblCurrentData = new System.Windows.Forms.Label();
            this.lblCurrentValueHeader = new System.Windows.Forms.Label();
            this.lblCurrentValue = new System.Windows.Forms.Label();
            this.flowLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMonitorOutput
            // 
            this.pnlMonitorOutput.AutoScroll = true;
            this.pnlMonitorOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMonitorOutput.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.pnlMonitorOutput.Location = new System.Drawing.Point(0, 0);
            this.pnlMonitorOutput.Name = "pnlMonitorOutput";
            this.pnlMonitorOutput.Size = new System.Drawing.Size(800, 425);
            this.pnlMonitorOutput.TabIndex = 0;
            this.pnlMonitorOutput.WrapContents = false;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.BackColor = System.Drawing.SystemColors.ControlLight;
            this.flowLayoutPanel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowLayoutPanel2.Controls.Add(this.lblItemName);
            this.flowLayoutPanel2.Controls.Add(this.lblStateHeader);
            this.flowLayoutPanel2.Controls.Add(this.lblState);
            this.flowLayoutPanel2.Controls.Add(this.lblRevertTimeHeader);
            this.flowLayoutPanel2.Controls.Add(this.lblRevertTime);
            this.flowLayoutPanel2.Controls.Add(this.lblAdcDataHeader);
            this.flowLayoutPanel2.Controls.Add(this.lblAdcData);
            this.flowLayoutPanel2.Controls.Add(this.lblAdcValueHeader);
            this.flowLayoutPanel2.Controls.Add(this.lblAdcValue);
            this.flowLayoutPanel2.Controls.Add(this.lblDiagDataHeader);
            this.flowLayoutPanel2.Controls.Add(this.lblDiagData);
            this.flowLayoutPanel2.Controls.Add(this.lblDiagValueHeader);
            this.flowLayoutPanel2.Controls.Add(this.lblDiagValue);
            this.flowLayoutPanel2.Controls.Add(this.lblCurrentDataHeader);
            this.flowLayoutPanel2.Controls.Add(this.lblCurrentData);
            this.flowLayoutPanel2.Controls.Add(this.lblCurrentValueHeader);
            this.flowLayoutPanel2.Controls.Add(this.lblCurrentValue);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(0, 425);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.flowLayoutPanel2.Size = new System.Drawing.Size(800, 25);
            this.flowLayoutPanel2.TabIndex = 0;
            // 
            // lblItemName
            // 
            this.lblItemName.AutoSize = true;
            this.lblItemName.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblItemName.Location = new System.Drawing.Point(3, 3);
            this.lblItemName.Name = "lblItemName";
            this.lblItemName.Size = new System.Drawing.Size(83, 19);
            this.lblItemName.TabIndex = 1;
            this.lblItemName.Text = "Item Name";
            // 
            // lblStateHeader
            // 
            this.lblStateHeader.AutoSize = true;
            this.lblStateHeader.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblStateHeader.Location = new System.Drawing.Point(92, 3);
            this.lblStateHeader.Name = "lblStateHeader";
            this.lblStateHeader.Size = new System.Drawing.Size(40, 19);
            this.lblStateHeader.TabIndex = 3;
            this.lblStateHeader.Text = "State";
            // 
            // lblState
            // 
            this.lblState.AutoSize = true;
            this.lblState.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblState.ForeColor = System.Drawing.Color.Gray;
            this.lblState.Location = new System.Drawing.Point(138, 3);
            this.lblState.Name = "lblState";
            this.lblState.Size = new System.Drawing.Size(15, 19);
            this.lblState.TabIndex = 4;
            this.lblState.Text = "-";
            // 
            // lblRevertTimeHeader
            // 
            this.lblRevertTimeHeader.AutoSize = true;
            this.lblRevertTimeHeader.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblRevertTimeHeader.Location = new System.Drawing.Point(159, 3);
            this.lblRevertTimeHeader.Name = "lblRevertTimeHeader";
            this.lblRevertTimeHeader.Size = new System.Drawing.Size(81, 19);
            this.lblRevertTimeHeader.TabIndex = 11;
            this.lblRevertTimeHeader.Text = "Revert Time";
            // 
            // lblRevertTime
            // 
            this.lblRevertTime.AutoSize = true;
            this.lblRevertTime.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblRevertTime.ForeColor = System.Drawing.Color.Gray;
            this.lblRevertTime.LiveSetting = System.Windows.Forms.Automation.AutomationLiveSetting.Polite;
            this.lblRevertTime.Location = new System.Drawing.Point(246, 3);
            this.lblRevertTime.Name = "lblRevertTime";
            this.lblRevertTime.Size = new System.Drawing.Size(15, 19);
            this.lblRevertTime.TabIndex = 11;
            this.lblRevertTime.Text = "-";
            // 
            // lblAdcDataHeader
            // 
            this.lblAdcDataHeader.AutoSize = true;
            this.lblAdcDataHeader.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblAdcDataHeader.Location = new System.Drawing.Point(267, 3);
            this.lblAdcDataHeader.Name = "lblAdcDataHeader";
            this.lblAdcDataHeader.Size = new System.Drawing.Size(70, 19);
            this.lblAdcDataHeader.TabIndex = 7;
            this.lblAdcDataHeader.Text = "ADC Data";
            // 
            // lblAdcData
            // 
            this.lblAdcData.AutoSize = true;
            this.lblAdcData.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblAdcData.ForeColor = System.Drawing.Color.Gray;
            this.lblAdcData.Location = new System.Drawing.Point(343, 3);
            this.lblAdcData.Name = "lblAdcData";
            this.lblAdcData.Size = new System.Drawing.Size(15, 19);
            this.lblAdcData.TabIndex = 8;
            this.lblAdcData.Text = "-";
            // 
            // lblAdcValueHeader
            // 
            this.lblAdcValueHeader.AutoSize = true;
            this.lblAdcValueHeader.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblAdcValueHeader.Location = new System.Drawing.Point(364, 3);
            this.lblAdcValueHeader.Name = "lblAdcValueHeader";
            this.lblAdcValueHeader.Size = new System.Drawing.Size(74, 19);
            this.lblAdcValueHeader.TabIndex = 12;
            this.lblAdcValueHeader.Text = "ADC Value";
            // 
            // lblAdcValue
            // 
            this.lblAdcValue.AutoSize = true;
            this.lblAdcValue.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblAdcValue.ForeColor = System.Drawing.Color.Gray;
            this.lblAdcValue.Location = new System.Drawing.Point(444, 3);
            this.lblAdcValue.Name = "lblAdcValue";
            this.lblAdcValue.Size = new System.Drawing.Size(15, 19);
            this.lblAdcValue.TabIndex = 13;
            this.lblAdcValue.Text = "-";
            // 
            // lblDiagDataHeader
            // 
            this.lblDiagDataHeader.AutoSize = true;
            this.lblDiagDataHeader.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblDiagDataHeader.Location = new System.Drawing.Point(465, 3);
            this.lblDiagDataHeader.Name = "lblDiagDataHeader";
            this.lblDiagDataHeader.Size = new System.Drawing.Size(75, 19);
            this.lblDiagDataHeader.TabIndex = 9;
            this.lblDiagDataHeader.Text = "DIAG Data";
            // 
            // lblDiagData
            // 
            this.lblDiagData.AutoSize = true;
            this.lblDiagData.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblDiagData.ForeColor = System.Drawing.Color.Gray;
            this.lblDiagData.Location = new System.Drawing.Point(546, 3);
            this.lblDiagData.Name = "lblDiagData";
            this.lblDiagData.Size = new System.Drawing.Size(15, 19);
            this.lblDiagData.TabIndex = 10;
            this.lblDiagData.Text = "-";
            // 
            // lblDiagValueHeader
            // 
            this.lblDiagValueHeader.AutoSize = true;
            this.lblDiagValueHeader.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblDiagValueHeader.Location = new System.Drawing.Point(567, 3);
            this.lblDiagValueHeader.Name = "lblDiagValueHeader";
            this.lblDiagValueHeader.Size = new System.Drawing.Size(79, 19);
            this.lblDiagValueHeader.TabIndex = 14;
            this.lblDiagValueHeader.Text = "DIAG Value";
            // 
            // lblDiagValue
            // 
            this.lblDiagValue.AutoSize = true;
            this.lblDiagValue.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblDiagValue.ForeColor = System.Drawing.Color.Gray;
            this.lblDiagValue.Location = new System.Drawing.Point(652, 3);
            this.lblDiagValue.Name = "lblDiagValue";
            this.lblDiagValue.Size = new System.Drawing.Size(15, 19);
            this.lblDiagValue.TabIndex = 15;
            this.lblDiagValue.Text = "-";
            // 
            // lblCurrentDataHeader
            // 
            this.lblCurrentDataHeader.AutoSize = true;
            this.lblCurrentDataHeader.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblCurrentDataHeader.Location = new System.Drawing.Point(673, 3);
            this.lblCurrentDataHeader.Name = "lblCurrentDataHeader";
            this.lblCurrentDataHeader.Size = new System.Drawing.Size(89, 19);
            this.lblCurrentDataHeader.TabIndex = 16;
            this.lblCurrentDataHeader.Text = "Current Data";
            // 
            // lblCurrentData
            // 
            this.lblCurrentData.AutoSize = true;
            this.lblCurrentData.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblCurrentData.ForeColor = System.Drawing.Color.Gray;
            this.lblCurrentData.Location = new System.Drawing.Point(768, 3);
            this.lblCurrentData.Name = "lblCurrentData";
            this.lblCurrentData.Size = new System.Drawing.Size(15, 19);
            this.lblCurrentData.TabIndex = 17;
            this.lblCurrentData.Text = "-";
            // 
            // lblCurrentValueHeader
            // 
            this.lblCurrentValueHeader.AutoSize = true;
            this.lblCurrentValueHeader.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblCurrentValueHeader.Location = new System.Drawing.Point(3, 22);
            this.lblCurrentValueHeader.Name = "lblCurrentValueHeader";
            this.lblCurrentValueHeader.Size = new System.Drawing.Size(93, 19);
            this.lblCurrentValueHeader.TabIndex = 18;
            this.lblCurrentValueHeader.Text = "Current Value";
            // 
            // lblCurrentValue
            // 
            this.lblCurrentValue.AutoSize = true;
            this.lblCurrentValue.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblCurrentValue.ForeColor = System.Drawing.Color.Gray;
            this.lblCurrentValue.Location = new System.Drawing.Point(102, 22);
            this.lblCurrentValue.Name = "lblCurrentValue";
            this.lblCurrentValue.Size = new System.Drawing.Size(15, 19);
            this.lblCurrentValue.TabIndex = 19;
            this.lblCurrentValue.Text = "-";
            // 
            // FormMonitorGenericOutput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.CloseButton = false;
            this.CloseButtonVisible = false;
            this.Controls.Add(this.pnlMonitorOutput);
            this.Controls.Add(this.flowLayoutPanel2);
            this.Name = "FormMonitorGenericOutput";
            this.Text = "Output";
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel pnlMonitorOutput;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Label lblItemName;
        private System.Windows.Forms.Label lblState;
        private System.Windows.Forms.Label lblStateHeader;
        private System.Windows.Forms.Label lblAdcDataHeader;
        private System.Windows.Forms.Label lblAdcData;
        private System.Windows.Forms.Label lblDiagDataHeader;
        private System.Windows.Forms.Label lblDiagData;
        private System.Windows.Forms.Label lblRevertTimeHeader;
        private System.Windows.Forms.Label lblRevertTime;
        private System.Windows.Forms.Label lblAdcValueHeader;
        private System.Windows.Forms.Label lblAdcValue;
        private System.Windows.Forms.Label lblDiagValueHeader;
        private System.Windows.Forms.Label lblDiagValue;
        private System.Windows.Forms.Label lblCurrentDataHeader;
        private System.Windows.Forms.Label lblCurrentData;
        private System.Windows.Forms.Label lblCurrentValueHeader;
        private System.Windows.Forms.Label lblCurrentValue;
    }
}