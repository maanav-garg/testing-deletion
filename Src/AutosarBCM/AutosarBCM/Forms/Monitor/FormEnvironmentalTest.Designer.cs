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
            this.tslReceived = new System.Windows.Forms.ToolStripLabel();
            this.tslTransmitted = new System.Windows.Forms.ToolStripLabel();
            this.tslDiff = new System.Windows.Forms.ToolStripLabel();
            this.pnlMonitor = new System.Windows.Forms.FlowLayoutPanel();
            this.lblMin = new System.Windows.Forms.Label();
            this.lblSec = new System.Windows.Forms.Label();
            this.lblCs = new System.Windows.Forms.Label();
            this.lblTimeTag = new System.Windows.Forms.Label();
            this.lblLoopTag = new System.Windows.Forms.Label();
            this.lblCycleTag = new System.Windows.Forms.Label();
            this.lblLoopVal = new System.Windows.Forms.Label();
            this.lblCycleVal = new System.Windows.Forms.Label();
            this.lblCol3 = new System.Windows.Forms.Label();
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
            this.tspFilterTxb,
            this.tslReceived,
            this.tslTransmitted,
            this.tslDiff});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1579, 27);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnStart
            // 
            this.btnStart.Image = global::AutosarBCM.Properties.Resources.play_pause;
            this.btnStart.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnStart.Margin = new System.Windows.Forms.Padding(5, 1, 10, 2);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(64, 24);
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
            this.btnClear.Size = new System.Drawing.Size(109, 24);
            this.btnClear.Text = "Clear Fields";
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(49, 24);
            this.toolStripLabel2.Text = "Filter: ";
            // 
            // tspFilterTxb
            // 
            this.tspFilterTxb.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.tspFilterTxb.Name = "tspFilterTxb";
            this.tspFilterTxb.Size = new System.Drawing.Size(100, 27);
            this.tspFilterTxb.TextChanged += new System.EventHandler(this.tspFilterTxb_TextChanged);
            // 
            // tslReceived
            // 
            this.tslReceived.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tslReceived.BackColor = System.Drawing.Color.Transparent;
            this.tslReceived.Image = global::AutosarBCM.Properties.Resources.arrowdown2;
            this.tslReceived.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tslReceived.Margin = new System.Windows.Forms.Padding(0, 1, 10, 2);
            this.tslReceived.Name = "tslReceived";
            this.tslReceived.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tslReceived.Size = new System.Drawing.Size(35, 24);
            this.tslReceived.Text = "0";
            // 
            // tslTransmitted
            // 
            this.tslTransmitted.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tslTransmitted.BackColor = System.Drawing.Color.Transparent;
            this.tslTransmitted.Image = global::AutosarBCM.Properties.Resources.arrowup2;
            this.tslTransmitted.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tslTransmitted.Margin = new System.Windows.Forms.Padding(0, 1, 5, 2);
            this.tslTransmitted.Name = "tslTransmitted";
            this.tslTransmitted.Size = new System.Drawing.Size(35, 24);
            this.tslTransmitted.Text = "0";
            this.tslTransmitted.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            // 
            // tslDiff
            // 
            this.tslDiff.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tslDiff.BackColor = System.Drawing.Color.Transparent;
            this.tslDiff.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tslDiff.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.tslDiff.ForeColor = System.Drawing.Color.IndianRed;
            this.tslDiff.Margin = new System.Windows.Forms.Padding(0, 1, 5, 2);
            this.tslDiff.Name = "tslDiff";
            this.tslDiff.Size = new System.Drawing.Size(20, 24);
            this.tslDiff.Text = "0";
            // 
            // pnlMonitor
            // 
            this.pnlMonitor.AutoScroll = true;
            this.pnlMonitor.BackColor = System.Drawing.Color.Transparent;
            this.pnlMonitor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMonitor.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.pnlMonitor.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.pnlMonitor.Location = new System.Drawing.Point(0, 27);
            this.pnlMonitor.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pnlMonitor.Name = "pnlMonitor";
            this.pnlMonitor.Size = new System.Drawing.Size(1579, 910);
            this.pnlMonitor.TabIndex = 3;
            this.pnlMonitor.WrapContents = false;
            // 
            // lblMin
            // 
            this.lblMin.AutoSize = true;
            this.lblMin.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblMin.ForeColor = System.Drawing.Color.Orange;
            this.lblMin.Location = new System.Drawing.Point(567, 7);
            this.lblMin.Name = "lblMin";
            this.lblMin.Size = new System.Drawing.Size(24, 18);
            this.lblMin.TabIndex = 4;
            this.lblMin.Text = "00";
            // 
            // lblSec
            // 
            this.lblSec.AutoSize = true;
            this.lblSec.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblSec.ForeColor = System.Drawing.Color.Orange;
            this.lblSec.Location = new System.Drawing.Point(615, 7);
            this.lblSec.Name = "lblSec";
            this.lblSec.Size = new System.Drawing.Size(24, 18);
            this.lblSec.TabIndex = 5;
            this.lblSec.Text = "00";
            // 
            // lblCs
            // 
            this.lblCs.AutoSize = true;
            this.lblCs.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblCs.ForeColor = System.Drawing.Color.Orange;
            this.lblCs.Location = new System.Drawing.Point(663, 7);
            this.lblCs.Name = "lblCs";
            this.lblCs.Size = new System.Drawing.Size(24, 18);
            this.lblCs.TabIndex = 6;
            this.lblCs.Text = "00";
            // 
            // lblTimeTag
            // 
            this.lblTimeTag.AutoSize = true;
            this.lblTimeTag.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblTimeTag.Location = new System.Drawing.Point(468, 7);
            this.lblTimeTag.Name = "lblTimeTag";
            this.lblTimeTag.Size = new System.Drawing.Size(45, 18);
            this.lblTimeTag.TabIndex = 7;
            this.lblTimeTag.Text = "Time:";
            // 
            // lblLoopTag
            // 
            this.lblLoopTag.AutoSize = true;
            this.lblLoopTag.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblLoopTag.Location = new System.Drawing.Point(731, 7);
            this.lblLoopTag.Name = "lblLoopTag";
            this.lblLoopTag.Size = new System.Drawing.Size(46, 18);
            this.lblLoopTag.TabIndex = 8;
            this.lblLoopTag.Text = "Loop:";
            // 
            // lblCycleTag
            // 
            this.lblCycleTag.AutoSize = true;
            this.lblCycleTag.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblCycleTag.Location = new System.Drawing.Point(805, 7);
            this.lblCycleTag.Name = "lblCycleTag";
            this.lblCycleTag.Size = new System.Drawing.Size(49, 18);
            this.lblCycleTag.TabIndex = 9;
            this.lblCycleTag.Text = "Cycle:";
            // 
            // lblLoopVal
            // 
            this.lblLoopVal.AutoSize = true;
            this.lblLoopVal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblLoopVal.ForeColor = System.Drawing.Color.Orange;
            this.lblLoopVal.Location = new System.Drawing.Point(783, 7);
            this.lblLoopVal.Name = "lblLoopVal";
            this.lblLoopVal.Size = new System.Drawing.Size(16, 18);
            this.lblLoopVal.TabIndex = 10;
            this.lblLoopVal.Text = "0";
            // 
            // lblCycleVal
            // 
            this.lblCycleVal.AutoSize = true;
            this.lblCycleVal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblCycleVal.ForeColor = System.Drawing.Color.Orange;
            this.lblCycleVal.Location = new System.Drawing.Point(860, 7);
            this.lblCycleVal.Name = "lblCycleVal";
            this.lblCycleVal.Size = new System.Drawing.Size(16, 18);
            this.lblCycleVal.TabIndex = 11;
            this.lblCycleVal.Text = "0";
            // 
            // lblCol3
            // 
            this.lblCol3.AutoSize = true;
            this.lblCol3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblCol3.ForeColor = System.Drawing.Color.Orange;
            this.lblCol3.Location = new System.Drawing.Point(645, 7);
            this.lblCol3.Name = "lblCol3";
            this.lblCol3.Size = new System.Drawing.Size(12, 18);
            this.lblCol3.TabIndex = 12;
            this.lblCol3.Text = ":";
            // 
            // lblCol2
            // 
            this.lblCol2.AutoSize = true;
            this.lblCol2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblCol2.ForeColor = System.Drawing.Color.Orange;
            this.lblCol2.Location = new System.Drawing.Point(597, 7);
            this.lblCol2.Name = "lblCol2";
            this.lblCol2.Size = new System.Drawing.Size(12, 18);
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
            this.lblHour.Location = new System.Drawing.Point(519, 7);
            this.lblHour.Name = "lblHour";
            this.lblHour.Size = new System.Drawing.Size(24, 18);
            this.lblHour.TabIndex = 14;
            this.lblHour.Text = "00";
            // 
            // lblCol1
            // 
            this.lblCol1.AutoSize = true;
            this.lblCol1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblCol1.ForeColor = System.Drawing.Color.Orange;
            this.lblCol1.Location = new System.Drawing.Point(549, 7);
            this.lblCol1.Name = "lblCol1";
            this.lblCol1.Size = new System.Drawing.Size(12, 18);
            this.lblCol1.TabIndex = 15;
            this.lblCol1.Text = ":";
            // 
            // FormEnvironmentalTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1579, 937);
            this.Controls.Add(this.lblCol1);
            this.Controls.Add(this.lblHour);
            this.Controls.Add(this.lblCol2);
            this.Controls.Add(this.lblCol3);
            this.Controls.Add(this.lblCycleVal);
            this.Controls.Add(this.lblLoopVal);
            this.Controls.Add(this.lblCycleTag);
            this.Controls.Add(this.lblLoopTag);
            this.Controls.Add(this.lblTimeTag);
            this.Controls.Add(this.lblCs);
            this.Controls.Add(this.lblSec);
            this.Controls.Add(this.lblMin);
            this.Controls.Add(this.pnlMonitor);
            this.Controls.Add(this.toolStrip1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(1061, 728);
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
        private System.Windows.Forms.Label lblCs;
        private System.Windows.Forms.Label lblTimeTag;
        private System.Windows.Forms.Label lblLoopTag;
        private System.Windows.Forms.Label lblCycleTag;
        private System.Windows.Forms.Label lblLoopVal;
        private System.Windows.Forms.Label lblCycleVal;
        private System.Windows.Forms.Label lblCol3;
        private System.Windows.Forms.Label lblCol2;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Label lblHour;
        private System.Windows.Forms.Label lblCol1;
        private System.Windows.Forms.ToolStripLabel tslReceived;
        private System.Windows.Forms.ToolStripLabel tslTransmitted;
        private System.Windows.Forms.ToolStripLabel tslDiff;
    }
}