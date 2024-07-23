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
            this.btnUCClear = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblName
            // 
            this.lblName.AutoEllipsis = true;
            this.lblName.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblName.Location = new System.Drawing.Point(4, 12);
            this.lblName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblName.MaximumSize = new System.Drawing.Size(227, 25);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(227, 25);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "label1";
            this.lblName.Click += new System.EventHandler(this.lblName_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblStatus.Location = new System.Drawing.Point(267, 9);
            this.lblStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(13, 12);
            this.lblStatus.TabIndex = 1;
            this.lblStatus.Text = "-";
            this.lblStatus.Visible = false;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(357, 7);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(22, 25);
            this.label1.TabIndex = 5;
            this.label1.Text = "↑";
            // 
            // lblTransmitted
            // 
            this.lblTransmitted.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTransmitted.AutoSize = true;
            this.lblTransmitted.Location = new System.Drawing.Point(375, 16);
            this.lblTransmitted.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTransmitted.Name = "lblTransmitted";
            this.lblTransmitted.Size = new System.Drawing.Size(14, 16);
            this.lblTransmitted.TabIndex = 6;
            this.lblTransmitted.Text = "0";
            // 
            // lblReceived
            // 
            this.lblReceived.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblReceived.AutoSize = true;
            this.lblReceived.Location = new System.Drawing.Point(435, 16);
            this.lblReceived.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblReceived.Name = "lblReceived";
            this.lblReceived.Size = new System.Drawing.Size(14, 16);
            this.lblReceived.TabIndex = 7;
            this.lblReceived.Text = "0";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.Location = new System.Drawing.Point(415, 7);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(22, 25);
            this.label2.TabIndex = 8;
            this.label2.Text = "↓";
            // 
            // lblDiff
            // 
            this.lblDiff.AutoSize = true;
            this.lblDiff.ForeColor = System.Drawing.Color.White;
            this.lblDiff.Location = new System.Drawing.Point(288, 43);
            this.lblDiff.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDiff.Name = "lblDiff";
            this.lblDiff.Size = new System.Drawing.Size(0, 16);
            this.lblDiff.TabIndex = 9;
            // 
            // btnRead
            // 
            this.btnRead.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRead.Location = new System.Drawing.Point(288, 11);
            this.btnRead.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnRead.Name = "btnRead";
            this.btnRead.Size = new System.Drawing.Size(61, 28);
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
            this.lbResponse.Location = new System.Drawing.Point(8, 48);
            this.lbResponse.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.lbResponse.Name = "lbResponse";
            this.lbResponse.Size = new System.Drawing.Size(456, 94);
            this.lbResponse.TabIndex = 0;
            this.lbResponse.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lbResponse_DrawItem);
            // 
            // btnUCClear
            // 
            this.btnUCClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUCClear.Image = global::AutosarBCM.Properties.Resources.delete_icon_12_795858790;
            this.btnUCClear.Location = new System.Drawing.Point(241, 11);
            this.btnUCClear.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnUCClear.Name = "btnUCClear";
            this.btnUCClear.Size = new System.Drawing.Size(43, 28);
            this.btnUCClear.TabIndex = 11;
            this.btnUCClear.UseVisualStyleBackColor = true;
            this.btnUCClear.Click += new System.EventHandler(this.btnUCClear_Click);
            // 
            // UCItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnUCClear);
            this.Controls.Add(this.lbResponse);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnRead);
            this.Controls.Add(this.lblDiff);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblReceived);
            this.Controls.Add(this.lblTransmitted);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblName);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "UCItem";
            this.Size = new System.Drawing.Size(473, 154);
            this.Load += new System.EventHandler(this.UCItem_Load);
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
        private System.Windows.Forms.Button btnUCClear;
    }
}
