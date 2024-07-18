namespace AutosarBCM.UserControls.Monitor
{
    partial class UCCycleBar
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
            this.label1 = new System.Windows.Forms.Label();
            this.lblTimeSpent = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblLoop = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblReboots = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // triggerLabel
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(53, 7);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Time Spent:";
            // 
            // lblTimeSpent
            // 
            this.lblTimeSpent.AutoSize = true;
            this.lblTimeSpent.ForeColor = System.Drawing.Color.Coral;
            this.lblTimeSpent.Location = new System.Drawing.Point(149, 7);
            this.lblTimeSpent.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTimeSpent.Name = "lblTimeSpent";
            this.lblTimeSpent.Size = new System.Drawing.Size(55, 16);
            this.lblTimeSpent.TabIndex = 1;
            this.lblTimeSpent.Text = "00:00:00";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.Location = new System.Drawing.Point(283, 7);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Loop:";
            // 
            // lblLoop
            // 
            this.lblLoop.AutoSize = true;
            this.lblLoop.ForeColor = System.Drawing.Color.Coral;
            this.lblLoop.Location = new System.Drawing.Point(327, 7);
            this.lblLoop.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLoop.Name = "lblLoop";
            this.lblLoop.Size = new System.Drawing.Size(14, 16);
            this.lblLoop.TabIndex = 3;
            this.lblLoop.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label3.Location = new System.Drawing.Point(363, 7);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "Cycle:";
            // 
            // lblReboots
            // 
            this.lblReboots.ForeColor = System.Drawing.Color.Coral;
            this.lblReboots.Location = new System.Drawing.Point(433, 7);
            this.lblReboots.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblReboots.Name = "lblReboots";
            this.lblReboots.Size = new System.Drawing.Size(90, 25);
            this.lblReboots.TabIndex = 5;
            this.lblReboots.Text = "0";
            // 
            // UCCycleBar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblReboots);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblLoop);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblTimeSpent);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "UCCycleBar";
            this.Size = new System.Drawing.Size(527, 33);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblTimeSpent;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblLoop;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblReboots;
    }
}
