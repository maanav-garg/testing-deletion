namespace AutosarBCM.Forms.Monitor
{
    partial class FormMonitorEnvOutput
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
            this.lblData = new System.Windows.Forms.Label();
            this.lblDataHeader = new System.Windows.Forms.Label();
            this.lblItemName = new System.Windows.Forms.Label();
            this.flpStatusPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.flpStatusPanel.SuspendLayout();
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
            // lblData
            // 
            this.lblData.AutoSize = true;
            this.lblData.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblData.ForeColor = System.Drawing.Color.Gray;
            this.lblData.Location = new System.Drawing.Point(136, 3);
            this.lblData.Name = "lblData";
            this.lblData.Size = new System.Drawing.Size(15, 19);
            this.lblData.TabIndex = 19;
            this.lblData.Text = "-";
            // 
            // lblDataHeader
            // 
            this.lblDataHeader.AutoSize = true;
            this.lblDataHeader.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblDataHeader.Location = new System.Drawing.Point(92, 3);
            this.lblDataHeader.Name = "lblDataHeader";
            this.lblDataHeader.Size = new System.Drawing.Size(38, 19);
            this.lblDataHeader.TabIndex = 18;
            this.lblDataHeader.Text = "Data";
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
            // flpStatusPanel
            // 
            this.flpStatusPanel.BackColor = System.Drawing.SystemColors.ControlLight;
            this.flpStatusPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flpStatusPanel.Controls.Add(this.lblItemName);
            this.flpStatusPanel.Controls.Add(this.lblDataHeader);
            this.flpStatusPanel.Controls.Add(this.lblData);
            this.flpStatusPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flpStatusPanel.Location = new System.Drawing.Point(0, 425);
            this.flpStatusPanel.Name = "flpStatusPanel";
            this.flpStatusPanel.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.flpStatusPanel.Size = new System.Drawing.Size(800, 25);
            this.flpStatusPanel.TabIndex = 5;
            // 
            // FormMonitorEnvOutput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.CloseButton = false;
            this.CloseButtonVisible = false;
            this.Controls.Add(this.pnlMonitorOutput);
            this.Controls.Add(this.flpStatusPanel);
            this.Name = "FormMonitorEnvOutput";
            this.Text = "Output";
            this.flpStatusPanel.ResumeLayout(false);
            this.flpStatusPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel pnlMonitorOutput;
        private System.Windows.Forms.Label lblData;
        private System.Windows.Forms.Label lblDataHeader;
        private System.Windows.Forms.Label lblItemName;
        private System.Windows.Forms.FlowLayoutPanel flpStatusPanel;
    }
}