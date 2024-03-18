namespace AutosarBCM.UserControls.Monitor
{
    partial class UCPowerMirror
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
            this.btnPowerMirrorUp = new System.Windows.Forms.Button();
            this.vS2015DarkTheme1 = new WeifenLuo.WinFormsUI.Docking.VS2015DarkTheme();
            this.btnPowerMirrorRight = new System.Windows.Forms.Button();
            this.btnPowerMirrorDown = new System.Windows.Forms.Button();
            this.btnPowerMirrorLeft = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.lblReadPowerMirrorUp = new System.Windows.Forms.Label();
            this.btnReadUp = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblReadPowerMirrorLeft = new System.Windows.Forms.Label();
            this.btnReadLeft = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.lblReadPowerMirrorRight = new System.Windows.Forms.Label();
            this.btnReadRight = new System.Windows.Forms.Button();
            this.numRevertTime = new System.Windows.Forms.NumericUpDown();
            this.lblRevertTime = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numRevertTime)).BeginInit();
            this.SuspendLayout();
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.lblName.Location = new System.Drawing.Point(3, 3);
            this.lblName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(52, 18);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "label1";
            this.lblName.Click += new System.EventHandler(this.lblName_Click);
            // 
            // btnPowerMirrorUp
            // 
            this.btnPowerMirrorUp.BackColor = System.Drawing.Color.Transparent;
            this.btnPowerMirrorUp.BackgroundImage = global::AutosarBCM.Properties.Resources.ArrowUp;
            this.btnPowerMirrorUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPowerMirrorUp.Location = new System.Drawing.Point(67, 38);
            this.btnPowerMirrorUp.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnPowerMirrorUp.Name = "btnPowerMirrorUp";
            this.btnPowerMirrorUp.Size = new System.Drawing.Size(45, 46);
            this.btnPowerMirrorUp.TabIndex = 1;
            this.btnPowerMirrorUp.UseVisualStyleBackColor = false;
            this.btnPowerMirrorUp.Click += new System.EventHandler(this.btnPowerMirrorUp_Click);
            // 
            // btnPowerMirrorRight
            // 
            this.btnPowerMirrorRight.BackColor = System.Drawing.Color.Transparent;
            this.btnPowerMirrorRight.BackgroundImage = global::AutosarBCM.Properties.Resources.ArrowRight;
            this.btnPowerMirrorRight.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPowerMirrorRight.Location = new System.Drawing.Point(116, 89);
            this.btnPowerMirrorRight.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnPowerMirrorRight.Name = "btnPowerMirrorRight";
            this.btnPowerMirrorRight.Size = new System.Drawing.Size(45, 46);
            this.btnPowerMirrorRight.TabIndex = 2;
            this.btnPowerMirrorRight.UseVisualStyleBackColor = false;
            this.btnPowerMirrorRight.Click += new System.EventHandler(this.btnPowerMirrorRight_Click);
            // 
            // btnPowerMirrorDown
            // 
            this.btnPowerMirrorDown.BackColor = System.Drawing.Color.Transparent;
            this.btnPowerMirrorDown.BackgroundImage = global::AutosarBCM.Properties.Resources.ArrowDown;
            this.btnPowerMirrorDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPowerMirrorDown.Location = new System.Drawing.Point(67, 140);
            this.btnPowerMirrorDown.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnPowerMirrorDown.Name = "btnPowerMirrorDown";
            this.btnPowerMirrorDown.Size = new System.Drawing.Size(45, 46);
            this.btnPowerMirrorDown.TabIndex = 3;
            this.btnPowerMirrorDown.UseVisualStyleBackColor = false;
            this.btnPowerMirrorDown.Click += new System.EventHandler(this.btnPowerMirrorDown_Click);
            // 
            // btnPowerMirrorLeft
            // 
            this.btnPowerMirrorLeft.BackColor = System.Drawing.Color.Transparent;
            this.btnPowerMirrorLeft.BackgroundImage = global::AutosarBCM.Properties.Resources.ArrowLeft;
            this.btnPowerMirrorLeft.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPowerMirrorLeft.Location = new System.Drawing.Point(19, 89);
            this.btnPowerMirrorLeft.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnPowerMirrorLeft.Name = "btnPowerMirrorLeft";
            this.btnPowerMirrorLeft.Size = new System.Drawing.Size(45, 46);
            this.btnPowerMirrorLeft.TabIndex = 4;
            this.btnPowerMirrorLeft.UseVisualStyleBackColor = false;
            this.btnPowerMirrorLeft.Click += new System.EventHandler(this.btnPowerMirrorLeft_Click);
            // 
            // lblReadPowerMirrorUp
            // 
            this.lblReadPowerMirrorUp.Location = new System.Drawing.Point(323, 46);
            this.lblReadPowerMirrorUp.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblReadPowerMirrorUp.Name = "lblReadPowerMirrorUp";
            this.lblReadPowerMirrorUp.Size = new System.Drawing.Size(181, 25);
            this.lblReadPowerMirrorUp.TabIndex = 12;
            this.lblReadPowerMirrorUp.Text = " -";
            // 
            // btnReadUp
            // 
            this.btnReadUp.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.btnReadUp.Location = new System.Drawing.Point(243, 46);
            this.btnReadUp.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnReadUp.Name = "btnReadUp";
            this.btnReadUp.Size = new System.Drawing.Size(53, 22);
            this.btnReadUp.TabIndex = 11;
            this.btnReadUp.Text = "Read";
            this.btnReadUp.UseVisualStyleBackColor = true;
            this.btnReadUp.Click += new System.EventHandler(this.btnReadUp_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.label1.Location = new System.Drawing.Point(166, 51);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(22, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "UP";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.label2.Location = new System.Drawing.Point(166, 98);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 16;
            this.label2.Text = "DOWN";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.label4.Location = new System.Drawing.Point(166, 113);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(33, 13);
            this.label4.TabIndex = 19;
            this.label4.Text = "LEFT";
            // 
            // lblReadPowerMirrorLeft
            // 
            this.lblReadPowerMirrorLeft.Location = new System.Drawing.Point(323, 101);
            this.lblReadPowerMirrorLeft.Name = "lblReadPowerMirrorLeft";
            this.lblReadPowerMirrorLeft.Size = new System.Drawing.Size(181, 25);
            this.lblReadPowerMirrorLeft.TabIndex = 18;
            this.lblReadPowerMirrorLeft.Text = " -";
            // 
            // btnReadLeft
            // 
            this.btnReadLeft.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.btnReadLeft.Location = new System.Drawing.Point(243, 98);
            this.btnReadLeft.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnReadLeft.Name = "btnReadLeft";
            this.btnReadLeft.Size = new System.Drawing.Size(53, 22);
            this.btnReadLeft.TabIndex = 17;
            this.btnReadLeft.Text = "Read";
            this.btnReadLeft.UseVisualStyleBackColor = true;
            this.btnReadLeft.Click += new System.EventHandler(this.btnReadLeft_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.label6.Location = new System.Drawing.Point(166, 150);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 13);
            this.label6.TabIndex = 22;
            this.label6.Text = "RIGHT";
            // 
            // lblReadPowerMirrorRight
            // 
            this.lblReadPowerMirrorRight.Location = new System.Drawing.Point(323, 145);
            this.lblReadPowerMirrorRight.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblReadPowerMirrorRight.Name = "lblReadPowerMirrorRight";
            this.lblReadPowerMirrorRight.Size = new System.Drawing.Size(181, 25);
            this.lblReadPowerMirrorRight.TabIndex = 21;
            this.lblReadPowerMirrorRight.Text = " -";
            // 
            // btnReadRight
            // 
            this.btnReadRight.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.btnReadRight.Location = new System.Drawing.Point(243, 145);
            this.btnReadRight.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnReadRight.Name = "btnReadRight";
            this.btnReadRight.Size = new System.Drawing.Size(53, 22);
            this.btnReadRight.TabIndex = 20;
            this.btnReadRight.Text = "Read";
            this.btnReadRight.UseVisualStyleBackColor = true;
            this.btnReadRight.Click += new System.EventHandler(this.btnReadRight_Click);
            // 
            // numRevertTime
            // 
            this.numRevertTime.Location = new System.Drawing.Point(409, 5);
            this.numRevertTime.Margin = new System.Windows.Forms.Padding(5);
            this.numRevertTime.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numRevertTime.Name = "numRevertTime";
            this.numRevertTime.Size = new System.Drawing.Size(95, 20);
            this.numRevertTime.TabIndex = 23;
            this.numRevertTime.ValueChanged += new System.EventHandler(this.numRevertTime_ValueChanged);
            // 
            // lblRevertTime
            // 
            this.lblRevertTime.AutoSize = true;
            this.lblRevertTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F);
            this.lblRevertTime.Location = new System.Drawing.Point(338, 8);
            this.lblRevertTime.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRevertTime.Name = "lblRevertTime";
            this.lblRevertTime.Size = new System.Drawing.Size(62, 13);
            this.lblRevertTime.TabIndex = 24;
            this.lblRevertTime.Text = "Revert Time";
            // 
            // UCPowerMirror
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.numRevertTime);
            this.Controls.Add(this.lblRevertTime);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lblReadPowerMirrorRight);
            this.Controls.Add(this.btnReadRight);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblReadPowerMirrorLeft);
            this.Controls.Add(this.btnReadLeft);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblReadPowerMirrorUp);
            this.Controls.Add(this.btnReadUp);
            this.Controls.Add(this.btnPowerMirrorLeft);
            this.Controls.Add(this.btnPowerMirrorDown);
            this.Controls.Add(this.btnPowerMirrorRight);
            this.Controls.Add(this.btnPowerMirrorUp);
            this.Controls.Add(this.lblName);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "UCPowerMirror";
            this.Size = new System.Drawing.Size(507, 197);
            ((System.ComponentModel.ISupportInitialize)(this.numRevertTime)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Button btnPowerMirrorUp;
        private WeifenLuo.WinFormsUI.Docking.VS2015DarkTheme vS2015DarkTheme1;
        private System.Windows.Forms.Button btnPowerMirrorRight;
        private System.Windows.Forms.Button btnPowerMirrorDown;
        private System.Windows.Forms.Button btnPowerMirrorLeft;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Label lblReadPowerMirrorUp;
        private System.Windows.Forms.Button btnReadUp;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblReadPowerMirrorLeft;
        private System.Windows.Forms.Button btnReadLeft;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblReadPowerMirrorRight;
        private System.Windows.Forms.Button btnReadRight;
        private System.Windows.Forms.NumericUpDown numRevertTime;
        private System.Windows.Forms.Label lblRevertTime;
    }
}
