namespace AutosarBCM.UserControls.Monitor
{
    partial class UCLoopback
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
            this.btnPair1 = new System.Windows.Forms.Button();
            this.btnPair2 = new System.Windows.Forms.Button();
            this.btnRead = new System.Windows.Forms.Button();
            this.lblResponse = new System.Windows.Forms.Label();
            this.lblPair1Response = new System.Windows.Forms.Label();
            this.lblPair2Response = new System.Windows.Forms.Label();
            this.lblLine1 = new System.Windows.Forms.Label();
            this.lblLine2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnPair1
            // 
            this.btnPair1.Location = new System.Drawing.Point(3, 28);
            this.btnPair1.Name = "btnPair1";
            this.btnPair1.Size = new System.Drawing.Size(75, 23);
            this.btnPair1.TabIndex = 0;
            this.btnPair1.Text = "SEND";
            this.btnPair1.UseVisualStyleBackColor = true;
            this.btnPair1.Click += new System.EventHandler(this.btnPair1_Click);
            // 
            // btnPair2
            // 
            this.btnPair2.Location = new System.Drawing.Point(306, 28);
            this.btnPair2.Name = "btnPair2";
            this.btnPair2.Size = new System.Drawing.Size(75, 23);
            this.btnPair2.TabIndex = 1;
            this.btnPair2.Text = "SEND";
            this.btnPair2.UseVisualStyleBackColor = true;
            this.btnPair2.Click += new System.EventHandler(this.btnPair2_Click);
            // 
            // btnRead
            // 
            this.btnRead.Location = new System.Drawing.Point(149, 64);
            this.btnRead.Name = "btnRead";
            this.btnRead.Size = new System.Drawing.Size(84, 23);
            this.btnRead.TabIndex = 2;
            this.btnRead.Text = "VERIFY";
            this.btnRead.UseVisualStyleBackColor = true;
            this.btnRead.Click += new System.EventHandler(this.btnRead_Click);
            // 
            // lblResponse
            // 
            this.lblResponse.AutoSize = true;
            this.lblResponse.Location = new System.Drawing.Point(183, 33);
            this.lblResponse.Name = "lblResponse";
            this.lblResponse.Size = new System.Drawing.Size(13, 13);
            this.lblResponse.TabIndex = 3;
            this.lblResponse.Text = "0";
            // 
            // lblPair1Response
            // 
            this.lblPair1Response.AutoSize = true;
            this.lblPair1Response.Location = new System.Drawing.Point(3, 64);
            this.lblPair1Response.Name = "lblPair1Response";
            this.lblPair1Response.Size = new System.Drawing.Size(13, 13);
            this.lblPair1Response.TabIndex = 4;
            this.lblPair1Response.Text = "0";
            // 
            // lblPair2Response
            // 
            this.lblPair2Response.AutoSize = true;
            this.lblPair2Response.Location = new System.Drawing.Point(303, 64);
            this.lblPair2Response.Name = "lblPair2Response";
            this.lblPair2Response.Size = new System.Drawing.Size(13, 13);
            this.lblPair2Response.TabIndex = 5;
            this.lblPair2Response.Text = "0";
            // 
            // lblLine1
            // 
            this.lblLine1.AutoSize = true;
            this.lblLine1.Location = new System.Drawing.Point(3, 12);
            this.lblLine1.Name = "lblLine1";
            this.lblLine1.Size = new System.Drawing.Size(35, 13);
            this.lblLine1.TabIndex = 6;
            this.lblLine1.Text = "label4";
            this.lblLine1.Click += new System.EventHandler(this.lblLine1_Click);
            // 
            // lblLine2
            // 
            this.lblLine2.AutoSize = true;
            this.lblLine2.Location = new System.Drawing.Point(303, 12);
            this.lblLine2.Name = "lblLine2";
            this.lblLine2.Size = new System.Drawing.Size(35, 13);
            this.lblLine2.TabIndex = 7;
            this.lblLine2.Text = "label5";
            this.lblLine2.Click += new System.EventHandler(this.lblLine2_Click);
            // 
            // UCLoopback
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.lblLine2);
            this.Controls.Add(this.lblLine1);
            this.Controls.Add(this.lblPair2Response);
            this.Controls.Add(this.lblPair1Response);
            this.Controls.Add(this.lblResponse);
            this.Controls.Add(this.btnRead);
            this.Controls.Add(this.btnPair2);
            this.Controls.Add(this.btnPair1);
            this.Name = "UCLoopback";
            this.Size = new System.Drawing.Size(463, 96);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnPair1;
        private System.Windows.Forms.Button btnPair2;
        private System.Windows.Forms.Button btnRead;
        private System.Windows.Forms.Label lblResponse;
        private System.Windows.Forms.Label lblPair1Response;
        private System.Windows.Forms.Label lblPair2Response;
        private System.Windows.Forms.Label lblLine1;
        private System.Windows.Forms.Label lblLine2;
    }
}
