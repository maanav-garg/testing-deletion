namespace AutosarBCM.UserControls.Monitor
{
    partial class UCWiper
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
            this.btnStop = new System.Windows.Forms.Button();
            this.btnLow = new System.Windows.Forms.Button();
            this.btnHigh = new System.Windows.Forms.Button();
            this.grpADC = new System.Windows.Forms.GroupBox();
            this.btnAdcRead = new System.Windows.Forms.Button();
            this.lblADC = new System.Windows.Forms.Label();
            this.grpDIAG = new System.Windows.Forms.GroupBox();
            this.btnDiagRead = new System.Windows.Forms.Button();
            this.lblDIAG = new System.Windows.Forms.Label();
            this.grpCurrent = new System.Windows.Forms.GroupBox();
            this.btnCurrentRead = new System.Windows.Forms.Button();
            this.lblCurrent = new System.Windows.Forms.Label();
            this.grpADC.SuspendLayout();
            this.grpDIAG.SuspendLayout();
            this.grpCurrent.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(17, 17);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 1;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnLow
            // 
            this.btnLow.Location = new System.Drawing.Point(109, 17);
            this.btnLow.Name = "btnLow";
            this.btnLow.Size = new System.Drawing.Size(75, 23);
            this.btnLow.TabIndex = 2;
            this.btnLow.Text = "Low";
            this.btnLow.UseVisualStyleBackColor = true;
            this.btnLow.Click += new System.EventHandler(this.btnLow_Click);
            // 
            // btnHigh
            // 
            this.btnHigh.Location = new System.Drawing.Point(203, 17);
            this.btnHigh.Name = "btnHigh";
            this.btnHigh.Size = new System.Drawing.Size(75, 23);
            this.btnHigh.TabIndex = 3;
            this.btnHigh.Text = "High";
            this.btnHigh.UseVisualStyleBackColor = true;
            this.btnHigh.Click += new System.EventHandler(this.btnHigh_Click);
            // 
            // grpADC
            // 
            this.grpADC.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.grpADC.Controls.Add(this.btnAdcRead);
            this.grpADC.Controls.Add(this.lblADC);
            this.grpADC.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.grpADC.Location = new System.Drawing.Point(103, 44);
            this.grpADC.Margin = new System.Windows.Forms.Padding(2);
            this.grpADC.Name = "grpADC";
            this.grpADC.Padding = new System.Windows.Forms.Padding(2);
            this.grpADC.Size = new System.Drawing.Size(98, 42);
            this.grpADC.TabIndex = 7;
            this.grpADC.TabStop = false;
            this.grpADC.Text = "ADC";
            // 
            // btnAdcRead
            // 
            this.btnAdcRead.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnAdcRead.ForeColor = System.Drawing.Color.Black;
            this.btnAdcRead.Location = new System.Drawing.Point(61, 20);
            this.btnAdcRead.Margin = new System.Windows.Forms.Padding(2);
            this.btnAdcRead.Name = "btnAdcRead";
            this.btnAdcRead.Size = new System.Drawing.Size(35, 20);
            this.btnAdcRead.TabIndex = 8;
            this.btnAdcRead.Text = "READ";
            this.btnAdcRead.UseVisualStyleBackColor = true;
            this.btnAdcRead.Click += new System.EventHandler(this.btnAdcRead_Click);
            // 
            // lblADC
            // 
            this.lblADC.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblADC.Location = new System.Drawing.Point(5, 25);
            this.lblADC.Name = "lblADC";
            this.lblADC.Size = new System.Drawing.Size(50, 12);
            this.lblADC.TabIndex = 7;
            // 
            // grpDIAG
            // 
            this.grpDIAG.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.grpDIAG.Controls.Add(this.btnDiagRead);
            this.grpDIAG.Controls.Add(this.lblDIAG);
            this.grpDIAG.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.grpDIAG.Location = new System.Drawing.Point(203, 44);
            this.grpDIAG.Margin = new System.Windows.Forms.Padding(2);
            this.grpDIAG.Name = "grpDIAG";
            this.grpDIAG.Padding = new System.Windows.Forms.Padding(2);
            this.grpDIAG.Size = new System.Drawing.Size(103, 42);
            this.grpDIAG.TabIndex = 8;
            this.grpDIAG.TabStop = false;
            this.grpDIAG.Text = "DIAG";
            // 
            // btnDiagRead
            // 
            this.btnDiagRead.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnDiagRead.ForeColor = System.Drawing.Color.Black;
            this.btnDiagRead.Location = new System.Drawing.Point(66, 20);
            this.btnDiagRead.Margin = new System.Windows.Forms.Padding(2);
            this.btnDiagRead.Name = "btnDiagRead";
            this.btnDiagRead.Size = new System.Drawing.Size(35, 20);
            this.btnDiagRead.TabIndex = 9;
            this.btnDiagRead.Text = "READ";
            this.btnDiagRead.UseVisualStyleBackColor = true;
            this.btnDiagRead.Click += new System.EventHandler(this.btnDiagRead_Click);
            // 
            // lblDIAG
            // 
            this.lblDIAG.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblDIAG.Location = new System.Drawing.Point(6, 25);
            this.lblDIAG.Name = "lblDIAG";
            this.lblDIAG.Size = new System.Drawing.Size(57, 12);
            this.lblDIAG.TabIndex = 8;
            // 
            // grpCurrent
            // 
            this.grpCurrent.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.grpCurrent.Controls.Add(this.btnCurrentRead);
            this.grpCurrent.Controls.Add(this.lblCurrent);
            this.grpCurrent.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.grpCurrent.Location = new System.Drawing.Point(3, 44);
            this.grpCurrent.Margin = new System.Windows.Forms.Padding(2);
            this.grpCurrent.Name = "grpCurrent";
            this.grpCurrent.Padding = new System.Windows.Forms.Padding(2);
            this.grpCurrent.Size = new System.Drawing.Size(98, 42);
            this.grpCurrent.TabIndex = 7;
            this.grpCurrent.TabStop = false;
            this.grpCurrent.Text = "Current";
            // 
            // btnCurrentRead
            // 
            this.btnCurrentRead.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnCurrentRead.ForeColor = System.Drawing.Color.Black;
            this.btnCurrentRead.Location = new System.Drawing.Point(61, 20);
            this.btnCurrentRead.Margin = new System.Windows.Forms.Padding(2);
            this.btnCurrentRead.Name = "btnCurrentRead";
            this.btnCurrentRead.Size = new System.Drawing.Size(35, 20);
            this.btnCurrentRead.TabIndex = 8;
            this.btnCurrentRead.Text = "READ";
            this.btnCurrentRead.UseVisualStyleBackColor = true;
            this.btnCurrentRead.Click += new System.EventHandler(this.btnCurrentRead_Click);
            // 
            // lblCurrent
            // 
            this.lblCurrent.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblCurrent.Location = new System.Drawing.Point(6, 24);
            this.lblCurrent.Name = "lblCurrent";
            this.lblCurrent.Size = new System.Drawing.Size(50, 12);
            this.lblCurrent.TabIndex = 7;
            // 
            // UCWiper
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpCurrent);
            this.Controls.Add(this.grpADC);
            this.Controls.Add(this.grpDIAG);
            this.Controls.Add(this.btnHigh);
            this.Controls.Add(this.btnLow);
            this.Controls.Add(this.btnStop);
            this.Name = "UCWiper";
            this.Size = new System.Drawing.Size(308, 96);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.UCWiper_MouseClick);
            this.grpADC.ResumeLayout(false);
            this.grpDIAG.ResumeLayout(false);
            this.grpCurrent.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnLow;
        private System.Windows.Forms.Button btnHigh;
        private System.Windows.Forms.GroupBox grpADC;
        private System.Windows.Forms.Button btnAdcRead;
        private System.Windows.Forms.Label lblADC;
        private System.Windows.Forms.GroupBox grpDIAG;
        private System.Windows.Forms.Button btnDiagRead;
        private System.Windows.Forms.Label lblDIAG;
        private System.Windows.Forms.GroupBox grpCurrent;
        private System.Windows.Forms.Button btnCurrentRead;
        private System.Windows.Forms.Label lblCurrent;
    }
}
