﻿namespace AutosarBCM
{
    partial class FormSplashScreen
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
            this.pnlLogo = new System.Windows.Forms.Panel();
            this.lblGroupName = new System.Windows.Forms.Label();
            this.picLogo = new System.Windows.Forms.PictureBox();
            this.lblAppInfo = new System.Windows.Forms.Label();
            this.picAutosarBCMLogo = new System.Windows.Forms.PictureBox();
            this.pnlLogo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picAutosarBCMLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlLogo
            // 
            this.pnlLogo.BackColor = System.Drawing.SystemColors.ControlLight;
            this.pnlLogo.Controls.Add(this.lblGroupName);
            this.pnlLogo.Controls.Add(this.picLogo);
            this.pnlLogo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlLogo.Location = new System.Drawing.Point(0, 0);
            this.pnlLogo.Name = "pnlLogo";
            this.pnlLogo.Size = new System.Drawing.Size(509, 100);
            this.pnlLogo.TabIndex = 0;
            // 
            // lblGroupName
            // 
            this.lblGroupName.AutoSize = true;
            this.lblGroupName.Font = new System.Drawing.Font("Nirmala UI Semilight", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGroupName.Location = new System.Drawing.Point(93, 63);
            this.lblGroupName.Name = "lblGroupName";
            this.lblGroupName.Size = new System.Drawing.Size(322, 30);
            this.lblGroupName.TabIndex = 1;
            this.lblGroupName.Text = "Automotive Software Tools Group";
            // 
            // picLogo
            // 
            this.picLogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.picLogo.Image = global::AutosarBCM.Properties.Resources.vLogo;
            this.picLogo.Location = new System.Drawing.Point(189, 12);
            this.picLogo.Name = "picLogo";
            this.picLogo.Size = new System.Drawing.Size(130, 48);
            this.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picLogo.TabIndex = 0;
            this.picLogo.TabStop = false;
            // 
            // lblAppInfo
            // 
            this.lblAppInfo.AutoSize = true;
            this.lblAppInfo.Font = new System.Drawing.Font("Nirmala UI Semilight", 15.75F);
            this.lblAppInfo.Location = new System.Drawing.Point(214, 170);
            this.lblAppInfo.Name = "lblAppInfo";
            this.lblAppInfo.Size = new System.Drawing.Size(83, 30);
            this.lblAppInfo.TabIndex = 3;
            this.lblAppInfo.Text = "appinfo";
            this.lblAppInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // picAutosarBCMLogo
            // 
            this.picAutosarBCMLogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.picAutosarBCMLogo.Image = global::AutosarBCM.Properties.Resources.tool11;
            this.picAutosarBCMLogo.Location = new System.Drawing.Point(231, 123);
            this.picAutosarBCMLogo.Name = "picAutosarBCMLogo";
            this.picAutosarBCMLogo.Size = new System.Drawing.Size(47, 48);
            this.picAutosarBCMLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picAutosarBCMLogo.TabIndex = 2;
            this.picAutosarBCMLogo.TabStop = false;
            // 
            // FormSplashScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(509, 211);
            this.ControlBox = false;
            this.Controls.Add(this.picAutosarBCMLogo);
            this.Controls.Add(this.lblAppInfo);
            this.Controls.Add(this.pnlLogo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormSplashScreen";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SplashScreen";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormSplashScreen_FormClosing);
            this.Load += new System.EventHandler(this.FormSplashScreen_Load);
            this.pnlLogo.ResumeLayout(false);
            this.pnlLogo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picAutosarBCMLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlLogo;
        private System.Windows.Forms.PictureBox picLogo;
        private System.Windows.Forms.Label lblGroupName;
        private System.Windows.Forms.Label lblAppInfo;
        private System.Windows.Forms.PictureBox picAutosarBCMLogo;
    }
}