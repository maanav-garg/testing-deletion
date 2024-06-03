namespace AutosarBCM.UserControls.Monitor
{
    partial class UCItem
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
            this.lblStatus = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblTransmitted = new System.Windows.Forms.Label();
            this.lblReceived = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblDiff = new System.Windows.Forms.Label();
            this.btnRead = new System.Windows.Forms.Button();
            this.lbResponse = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblName.Location = new System.Drawing.Point(3, 4);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(52, 18);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "label1";
            this.lblName.Click += new System.EventHandler(this.lblName_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblStatus.Location = new System.Drawing.Point(200, 7);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(10, 10);
            this.lblStatus.TabIndex = 1;
            this.lblStatus.Text = "-";
            this.lblStatus.Visible = false;
            this.lblStatus.Click += new System.EventHandler(this.lblStatus_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(268, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 20);
            this.label1.TabIndex = 5;
            this.label1.Text = "↑";
            // 
            // lblTransmitted
            // 
            this.lblTransmitted.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTransmitted.AutoSize = true;
            this.lblTransmitted.Location = new System.Drawing.Point(281, 13);
            this.lblTransmitted.Name = "lblTransmitted";
            this.lblTransmitted.Size = new System.Drawing.Size(13, 13);
            this.lblTransmitted.TabIndex = 6;
            this.lblTransmitted.Text = "0";
            // 
            // lblReceived
            // 
            this.lblReceived.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblReceived.AutoSize = true;
            this.lblReceived.Location = new System.Drawing.Point(326, 13);
            this.lblReceived.Name = "lblReceived";
            this.lblReceived.Size = new System.Drawing.Size(13, 13);
            this.lblReceived.TabIndex = 7;
            this.lblReceived.Text = "0";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.Location = new System.Drawing.Point(311, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 20);
            this.label2.TabIndex = 8;
            this.label2.Text = "↓";
            // 
            // lblDiff
            // 
            this.lblDiff.AutoSize = true;
            this.lblDiff.ForeColor = System.Drawing.Color.White;
            this.lblDiff.Location = new System.Drawing.Point(216, 35);
            this.lblDiff.Name = "lblDiff";
            this.lblDiff.Size = new System.Drawing.Size(0, 13);
            this.lblDiff.TabIndex = 9;
            // 
            // btnRead
            // 
            this.btnRead.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRead.Location = new System.Drawing.Point(216, 9);
            this.btnRead.Name = "btnRead";
            this.btnRead.Size = new System.Drawing.Size(46, 23);
            this.btnRead.TabIndex = 10;
            this.btnRead.Text = "Read";
            this.btnRead.UseVisualStyleBackColor = true;
            this.btnRead.Click += new System.EventHandler(this.btnRead_Click);
            // 
            // lbResponse
            // 
            this.lbResponse.BackColor = System.Drawing.SystemColors.MenuBar;
            this.lbResponse.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lbResponse.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbResponse.FormattingEnabled = true;
            this.lbResponse.ItemHeight = 15;
            this.lbResponse.Location = new System.Drawing.Point(6, 39);
            this.lbResponse.Name = "lbResponse";
            this.lbResponse.Size = new System.Drawing.Size(343, 79);
            this.lbResponse.TabIndex = 0;
            this.lbResponse.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lbResponse_DrawItem);
            // 
            // UCItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lbResponse);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnRead);
            this.Controls.Add(this.lblDiff);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblReceived);
            this.Controls.Add(this.lblTransmitted);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblName);
            this.Name = "UCItem";
            this.Size = new System.Drawing.Size(355, 125);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblTransmitted;
        private System.Windows.Forms.Label lblReceived;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblDiff;
        private System.Windows.Forms.Button btnRead;
        private System.Windows.Forms.ListBox lbResponse;
    }
}
