namespace AutosarBCM
{
    partial class FormTracePopup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormTracePopup));
            this.txtTrace = new System.Windows.Forms.RichTextBox();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.tsbClearLog = new System.Windows.Forms.ToolStripButton();
            this.buttonCancel = new System.Windows.Forms.Button();

            this.toolStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtTrace
            // 
            this.txtTrace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtTrace.Location = new System.Drawing.Point(0, 27);
            this.txtTrace.Margin = new System.Windows.Forms.Padding(2);
            this.txtTrace.Name = "txtTrace";
            this.txtTrace.ReadOnly = true;
            this.txtTrace.Size = new System.Drawing.Size(800, 423);
            this.txtTrace.TabIndex = 11;
            this.txtTrace.Text = "";
            // 
            // toolStrip2
            // 
            this.toolStrip2.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbClearLog});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(800, 27);
            this.toolStrip2.TabIndex = 12;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // tsbClearLog
            // 
            this.tsbClearLog.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.tsbClearLog.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tsbClearLog.Image = ((System.Drawing.Image)(resources.GetObject("tsbClearLog.Image")));
            this.tsbClearLog.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbClearLog.Name = "tsbClearLog";
            this.tsbClearLog.Size = new System.Drawing.Size(58, 24);
            this.tsbClearLog.Text = "Clear";
            this.tsbClearLog.Click += new System.EventHandler(this.tsbClearLog_Click);
            // 
            // FormTracePopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.txtTrace);
            this.Controls.Add(this.toolStrip2);
            this.Controls.Add(this.buttonCancel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormTracePopup";
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            this.CancelButton = this.buttonCancel;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Trace";
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.RichTextBox txtTrace;
        internal System.Windows.Forms.Button buttonCancel;

        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton tsbClearLog;
    }
}