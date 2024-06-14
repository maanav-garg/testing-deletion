namespace AutosarBCM.Forms.Monitor
{
    partial class FormDTCPanel
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
            this.pnlMonitor = new System.Windows.Forms.FlowLayoutPanel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnReadDTC = new System.Windows.Forms.ToolStripButton();
            this.btnClearDTC = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMonitor
            // 
            this.pnlMonitor.AutoScroll = true;
            this.pnlMonitor.BackColor = System.Drawing.Color.Transparent;
            this.pnlMonitor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMonitor.Enabled = false;
            this.pnlMonitor.Location = new System.Drawing.Point(0, 30);
            this.pnlMonitor.Margin = new System.Windows.Forms.Padding(2);
            this.pnlMonitor.Name = "pnlMonitor";
            this.pnlMonitor.Size = new System.Drawing.Size(800, 420);
            this.pnlMonitor.TabIndex = 5;
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnReadDTC,
            this.btnClearDTC});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(800, 30);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnReadDTC
            // 
            this.btnReadDTC.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReadDTC.Image = global::AutosarBCM.Properties.Resources.dtc;
            this.btnReadDTC.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnReadDTC.Name = "btnReadDTC";
            this.btnReadDTC.Size = new System.Drawing.Size(84, 27);
            this.btnReadDTC.Text = "Read DTC";
            this.btnReadDTC.Click += new System.EventHandler(this.btnReadDTC_Click);
            // 
            // btnClearDTC
            // 
            this.btnClearDTC.Image = global::AutosarBCM.Properties.Resources.reset;
            this.btnClearDTC.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnClearDTC.Name = "btnClearDTC";
            this.btnClearDTC.Size = new System.Drawing.Size(77, 27);
            this.btnClearDTC.Text = "Clear DTC";
            this.btnClearDTC.Click += new System.EventHandler(this.btnClearDTC_Click);
            // 
            // FormDTCPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.pnlMonitor);
            this.Controls.Add(this.toolStrip1);
            this.Name = "FormDTCPanel";
            this.Text = "DTC";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel pnlMonitor;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnReadDTC;
        private System.Windows.Forms.ToolStripButton btnClearDTC;
    }
}