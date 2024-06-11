namespace AutosarBCM.Forms.Monitor
{
    partial class FormEnvironmentalTest
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormEnvironmentalTest));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnStart = new System.Windows.Forms.ToolStripButton();
            this.btnClear = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.tspFilterTxb = new System.Windows.Forms.ToolStripTextBox();
            this.pnlMonitor = new System.Windows.Forms.FlowLayoutPanel();
            this.lblMin = new System.Windows.Forms.Label();
            this.lblSec = new System.Windows.Forms.Label();
            this.lblTimeTag = new System.Windows.Forms.Label();
            this.lblLoopTag = new System.Windows.Forms.Label();
            this.lblCycleTag = new System.Windows.Forms.Label();
            this.lblLoopVal = new System.Windows.Forms.Label();
            this.lblCycleVal = new System.Windows.Forms.Label();
            this.lblCol2 = new System.Windows.Forms.Label();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.lblHour = new System.Windows.Forms.Label();
            this.lblCol1 = new System.Windows.Forms.Label();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnStart,
            this.btnClear,
            this.toolStripLabel2,
            this.tspFilterTxb});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1184, 27);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnStart
            // 
            this.btnStart.Image = global::AutosarBCM.Properties.Resources.play_pause;
            this.btnStart.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnStart.Margin = new System.Windows.Forms.Padding(5, 1, 10, 2);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(55, 24);
            this.btnStart.Text = "Start";
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnClear
            // 
            this.btnClear.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnClear.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnClear.Image = ((System.Drawing.Image)(resources.GetObject("btnClear.Image")));
            this.btnClear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(91, 24);
            this.btnClear.Text = "Clear Fields";
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(39, 24);
            this.toolStripLabel2.Text = "Filter: ";
            // 
            // tspFilterTxb
            // 
            this.tspFilterTxb.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.tspFilterTxb.Name = "tspFilterTxb";
            this.tspFilterTxb.Size = new System.Drawing.Size(76, 27);
            this.tspFilterTxb.TextChanged += new System.EventHandler(this.tspFilterTxb_TextChanged);
            // 
            // pnlMonitor
            // 
            this.pnlMonitor.AutoScroll = true;
            this.pnlMonitor.BackColor = System.Drawing.Color.Transparent;
            this.pnlMonitor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMonitor.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.pnlMonitor.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.pnlMonitor.Location = new System.Drawing.Point(0, 27);
            this.pnlMonitor.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pnlMonitor.Name = "pnlMonitor";
            this.pnlMonitor.Size = new System.Drawing.Size(1184, 734);
            this.pnlMonitor.TabIndex = 3;
            this.pnlMonitor.WrapContents = false;
            // 
            // lblMin
            // 
            this.lblMin.AutoSize = true;
            this.lblMin.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblMin.ForeColor = System.Drawing.Color.Orange;
            this.lblMin.Location = new System.Drawing.Point(425, 6);
            this.lblMin.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMin.Name = "lblMin";
            this.lblMin.Size = new System.Drawing.Size(21, 15);
            this.lblMin.TabIndex = 4;
            this.lblMin.Text = "00";
            // 
            // lblSec
            // 
            this.lblSec.AutoSize = true;
            this.lblSec.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblSec.ForeColor = System.Drawing.Color.Orange;
            this.lblSec.Location = new System.Drawing.Point(461, 6);
            this.lblSec.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSec.Name = "lblSec";
            this.lblSec.Size = new System.Drawing.Size(21, 15);
            this.lblSec.TabIndex = 5;
            this.lblSec.Text = "00";
            // 
            // lblTimeTag
            // 
            this.lblTimeTag.AutoSize = true;
            this.lblTimeTag.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblTimeTag.Location = new System.Drawing.Point(351, 6);
            this.lblTimeTag.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTimeTag.Name = "lblTimeTag";
            this.lblTimeTag.Size = new System.Drawing.Size(38, 15);
            this.lblTimeTag.TabIndex = 7;
            this.lblTimeTag.Text = "Time:";
            // 
            // lblLoopTag
            // 
            this.lblLoopTag.AutoSize = true;
            this.lblLoopTag.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblLoopTag.Location = new System.Drawing.Point(548, 6);
            this.lblLoopTag.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblLoopTag.Name = "lblLoopTag";
            this.lblLoopTag.Size = new System.Drawing.Size(38, 15);
            this.lblLoopTag.TabIndex = 8;
            this.lblLoopTag.Text = "Loop:";
            // 
            // lblCycleTag
            // 
            this.lblCycleTag.AutoSize = true;
            this.lblCycleTag.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblCycleTag.Location = new System.Drawing.Point(604, 6);
            this.lblCycleTag.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCycleTag.Name = "lblCycleTag";
            this.lblCycleTag.Size = new System.Drawing.Size(39, 15);
            this.lblCycleTag.TabIndex = 9;
            this.lblCycleTag.Text = "Cycle:";
            // 
            // lblLoopVal
            // 
            this.lblLoopVal.AutoSize = true;
            this.lblLoopVal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblLoopVal.ForeColor = System.Drawing.Color.Orange;
            this.lblLoopVal.Location = new System.Drawing.Point(587, 6);
            this.lblLoopVal.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblLoopVal.Name = "lblLoopVal";
            this.lblLoopVal.Size = new System.Drawing.Size(14, 15);
            this.lblLoopVal.TabIndex = 10;
            this.lblLoopVal.Text = "0";
            // 
            // lblCycleVal
            // 
            this.lblCycleVal.AutoSize = true;
            this.lblCycleVal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblCycleVal.ForeColor = System.Drawing.Color.Orange;
            this.lblCycleVal.Location = new System.Drawing.Point(645, 6);
            this.lblCycleVal.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCycleVal.Name = "lblCycleVal";
            this.lblCycleVal.Size = new System.Drawing.Size(14, 15);
            this.lblCycleVal.TabIndex = 11;
            this.lblCycleVal.Text = "0";
            // 
            // lblCol2
            // 
            this.lblCol2.AutoSize = true;
            this.lblCol2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblCol2.ForeColor = System.Drawing.Color.Orange;
            this.lblCol2.Location = new System.Drawing.Point(448, 6);
            this.lblCol2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCol2.Name = "lblCol2";
            this.lblCol2.Size = new System.Drawing.Size(10, 15);
            this.lblCol2.TabIndex = 13;
            this.lblCol2.Text = ":";
            // 
            // timer
            // 
            this.timer.Enabled = true;
            this.timer.Interval = 10;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // lblHour
            // 
            this.lblHour.AutoSize = true;
            this.lblHour.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblHour.ForeColor = System.Drawing.Color.Orange;
            this.lblHour.Location = new System.Drawing.Point(389, 6);
            this.lblHour.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblHour.Name = "lblHour";
            this.lblHour.Size = new System.Drawing.Size(21, 15);
            this.lblHour.TabIndex = 14;
            this.lblHour.Text = "00";
            // 
            // lblCol1
            // 
            this.lblCol1.AutoSize = true;
            this.lblCol1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblCol1.ForeColor = System.Drawing.Color.Orange;
            this.lblCol1.Location = new System.Drawing.Point(412, 6);
            this.lblCol1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCol1.Name = "lblCol1";
            this.lblCol1.Size = new System.Drawing.Size(10, 15);
            this.lblCol1.TabIndex = 15;
            this.lblCol1.Text = ":";
            // 
            // FormEnvironmentalTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 761);
            this.Controls.Add(this.lblCol1);
            this.Controls.Add(this.lblHour);
            this.Controls.Add(this.lblCol2);
            this.Controls.Add(this.lblCycleVal);
            this.Controls.Add(this.lblLoopVal);
            this.Controls.Add(this.lblCycleTag);
            this.Controls.Add(this.lblLoopTag);
            this.Controls.Add(this.lblTimeTag);
            this.Controls.Add(this.lblSec);
            this.Controls.Add(this.lblMin);
            this.Controls.Add(this.pnlMonitor);
            this.Controls.Add(this.toolStrip1);
            this.MinimumSize = new System.Drawing.Size(800, 599);
            this.Name = "FormEnvironmentalTest";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Environmental Test";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormEnvironmentalTest_FormClosing);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnStart;
        private System.Windows.Forms.ToolStripButton btnClear;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripTextBox tspFilterTxb;
        private System.Windows.Forms.FlowLayoutPanel pnlMonitor;
        private System.Windows.Forms.Label lblMin;
        private System.Windows.Forms.Label lblSec;
        private System.Windows.Forms.Label lblTimeTag;
        private System.Windows.Forms.Label lblLoopTag;
        private System.Windows.Forms.Label lblCycleTag;
        private System.Windows.Forms.Label lblLoopVal;
        private System.Windows.Forms.Label lblCycleVal;
        private System.Windows.Forms.Label lblCol2;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Label lblHour;
        private System.Windows.Forms.Label lblCol1;
    }
}