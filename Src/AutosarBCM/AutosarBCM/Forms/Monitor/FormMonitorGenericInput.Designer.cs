namespace AutosarBCM.Forms.Monitor
{
    partial class FormMonitorGenericInput
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
            this.pnlMonitorInput = new System.Windows.Forms.FlowLayoutPanel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblItemName = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsLowerLimitLbl = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblLowerLimit = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsUpperLimitLbl = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblUpperLimit = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsCoefficientLbl = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblCoefficient = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel8 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblData = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.btnStart = new System.Windows.Forms.ToolStripButton();
            this.ucControlByIdentifierItem = new AutosarBCM.UserControls.Monitor.UCControlByIdentifierItem();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.pnlMonitorInput.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMonitorInput
            // 
            this.pnlMonitorInput.Controls.Add(this.toolStrip1);
            this.pnlMonitorInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMonitorInput.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.pnlMonitorInput.Location = new System.Drawing.Point(0, 0);
            this.pnlMonitorInput.Margin = new System.Windows.Forms.Padding(4);
            this.pnlMonitorInput.Name = "pnlMonitorInput";
            this.pnlMonitorInput.Padding = new System.Windows.Forms.Padding(0, 30, 0, 0);
            this.pnlMonitorInput.Size = new System.Drawing.Size(818, 554);
            this.pnlMonitorInput.TabIndex = 0;
            this.pnlMonitorInput.WrapContents = false;
            // 
            // toolStrip1
            // 
            this.toolStrip1.AllowDrop = true;
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnStart});
            this.toolStrip1.Location = new System.Drawing.Point(0, 30);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(0, 45);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblItemName,
            this.tsLowerLimitLbl,
            this.lblLowerLimit,
            this.tsUpperLimitLbl,
            this.lblUpperLimit,
            this.tsCoefficientLbl,
            this.lblCoefficient,
            this.toolStripStatusLabel8,
            this.lblData});
            this.statusStrip1.Location = new System.Drawing.Point(0, 525);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1067, 29);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            this.statusStrip1.Visible = false;
            // 
            // lblItemName
            // 
            this.lblItemName.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblItemName.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblItemName.Name = "lblItemName";
            this.lblItemName.Size = new System.Drawing.Size(99, 23);
            this.lblItemName.Text = "Item Name";
            // 
            // tsLowerLimitLbl
            // 
            this.tsLowerLimitLbl.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.tsLowerLimitLbl.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tsLowerLimitLbl.Name = "tsLowerLimitLbl";
            this.tsLowerLimitLbl.Size = new System.Drawing.Size(97, 23);
            this.tsLowerLimitLbl.Text = "Lower Limit";
            // 
            // lblLowerLimit
            // 
            this.lblLowerLimit.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblLowerLimit.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblLowerLimit.Name = "lblLowerLimit";
            this.lblLowerLimit.Size = new System.Drawing.Size(17, 23);
            this.lblLowerLimit.Text = "-";
            // 
            // tsUpperLimitLbl
            // 
            this.tsUpperLimitLbl.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.tsUpperLimitLbl.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tsUpperLimitLbl.Name = "tsUpperLimitLbl";
            this.tsUpperLimitLbl.Size = new System.Drawing.Size(99, 23);
            this.tsUpperLimitLbl.Text = "Upper Limit";
            // 
            // lblUpperLimit
            // 
            this.lblUpperLimit.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblUpperLimit.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblUpperLimit.Name = "lblUpperLimit";
            this.lblUpperLimit.Size = new System.Drawing.Size(17, 23);
            this.lblUpperLimit.Text = "-";
            // 
            // tsCoefficientLbl
            // 
            this.tsCoefficientLbl.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.tsCoefficientLbl.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tsCoefficientLbl.Name = "tsCoefficientLbl";
            this.tsCoefficientLbl.Size = new System.Drawing.Size(91, 23);
            this.tsCoefficientLbl.Text = "Coefficient";
            // 
            // lblCoefficient
            // 
            this.lblCoefficient.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblCoefficient.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblCoefficient.Name = "lblCoefficient";
            this.lblCoefficient.Size = new System.Drawing.Size(17, 23);
            this.lblCoefficient.Text = "-";
            // 
            // toolStripStatusLabel8
            // 
            this.toolStripStatusLabel8.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.toolStripStatusLabel8.Name = "toolStripStatusLabel8";
            this.toolStripStatusLabel8.Size = new System.Drawing.Size(46, 23);
            this.toolStripStatusLabel8.Text = "Data";
            // 
            // lblData
            // 
            this.lblData.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblData.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblData.Name = "lblData";
            this.lblData.Size = new System.Drawing.Size(17, 23);
            this.lblData.Text = "-";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.AutoScroll = true;
            this.splitContainer1.Panel1.Controls.Add(this.toolStrip2);
            this.splitContainer1.Panel1.Controls.Add(this.pnlMonitorInput);
            this.splitContainer1.Panel1MinSize = 85;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.splitContainer1.Panel2.Controls.Add(this.ucControlByIdentifierItem);
            this.splitContainer1.Panel2MinSize = 15;
            this.splitContainer1.Size = new System.Drawing.Size(1100, 554);
            this.splitContainer1.SplitterDistance = 818;
            this.splitContainer1.TabIndex = 2;
            // 
            // toolStrip2
            // 
            this.toolStrip2.AutoSize = false;
            this.toolStrip2.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnStart});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(818, 29);
            this.toolStrip2.TabIndex = 1;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // btnStart
            // 
            this.btnStart.Image = global::AutosarBCM.Properties.Resources.play_pause;
            this.btnStart.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnStart.Margin = new System.Windows.Forms.Padding(5, 1, 10, 2);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(64, 26);
            this.btnStart.Text = "Start";
            this.btnStart.Click += new System.EventHandler(this.btnStartInput_Click);
            // 
            // ucControlByIdentifierItem
            // 
            this.ucControlByIdentifierItem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucControlByIdentifierItem.Location = new System.Drawing.Point(0, 0);
            this.ucControlByIdentifierItem.Margin = new System.Windows.Forms.Padding(5);
            this.ucControlByIdentifierItem.Name = "ucControlByIdentifierItem";
            this.ucControlByIdentifierItem.Size = new System.Drawing.Size(245, 554);
            this.ucControlByIdentifierItem.TabIndex = 7;
            // 
            // FormMonitorGenericInput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 554);
            this.CloseButton = false;
            this.CloseButtonVisible = false;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FormMonitorGenericInput";
            this.Text = "Input";
            this.pnlMonitorInput.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel pnlMonitorInput;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tsLowerLimitLbl;
        private System.Windows.Forms.ToolStripStatusLabel lblLowerLimit;
        private System.Windows.Forms.ToolStripStatusLabel tsUpperLimitLbl;
        private System.Windows.Forms.ToolStripStatusLabel lblUpperLimit;
        private System.Windows.Forms.ToolStripStatusLabel tsCoefficientLbl;
        private System.Windows.Forms.ToolStripStatusLabel lblCoefficient;
        private System.Windows.Forms.ToolStripStatusLabel lblItemName;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel8;
        private System.Windows.Forms.ToolStripStatusLabel lblData;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private UserControls.Monitor.UCControlByIdentifierItem ucControlByIdentifierItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton btnStart;
    }
}