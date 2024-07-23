using AutosarBCM.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace AutosarBCM
{
    partial class FormMessageAddition
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMessageAddition));
            this.btnMsgAdd = new System.Windows.Forms.Button();
            this.txtArbID = new System.Windows.Forms.TextBox();
            this.lblArbID = new System.Windows.Forms.Label();
            this.byteLabel = new System.Windows.Forms.Label();
            this.msgPanel = new System.Windows.Forms.Panel();
            this.pnlHexBytes = new System.Windows.Forms.Panel();
            this.lblDelayTime = new System.Windows.Forms.Label();
            this.numMessageLength = new System.Windows.Forms.NumericUpDown();
            this.numDelayTime = new System.Windows.Forms.NumericUpDown();
            this.numCycleCount = new System.Windows.Forms.NumericUpDown();
            this.numCycleTime = new System.Windows.Forms.NumericUpDown();
            this.lblCycleCount = new System.Windows.Forms.Label();
            this.lblMessageLength = new System.Windows.Forms.Label();
            this.cbTrigger = new System.Windows.Forms.ComboBox();
            this.triggerLabel = new System.Windows.Forms.Label();
            this.txtComment = new System.Windows.Forms.TextBox();
            this.lblComment = new System.Windows.Forms.Label();
            this.lblCycleTime = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.msgPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMessageLength)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDelayTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCycleCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCycleTime)).BeginInit();
            this.SuspendLayout();
            // 
            // btnMsgAdd
            // 
            this.btnMsgAdd.BackColor = System.Drawing.Color.White;
            this.btnMsgAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMsgAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnMsgAdd.Image")));
            this.btnMsgAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMsgAdd.Location = new System.Drawing.Point(268, 218);
            this.btnMsgAdd.Margin = new System.Windows.Forms.Padding(5);
            this.btnMsgAdd.Name = "btnMsgAdd";
            this.btnMsgAdd.Size = new System.Drawing.Size(89, 38);
            this.btnMsgAdd.TabIndex = 85;
            this.btnMsgAdd.Text = "Add";
            this.btnMsgAdd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnMsgAdd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnMsgAdd.UseVisualStyleBackColor = false;
            this.btnMsgAdd.Click += new System.EventHandler(this.btnMsgAdd_Click);
            // 
            // txtArbID
            // 
            this.txtArbID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtArbID.Location = new System.Drawing.Point(21, 32);
            this.txtArbID.Margin = new System.Windows.Forms.Padding(5);
            this.txtArbID.Name = "txtArbID";
            this.txtArbID.Size = new System.Drawing.Size(97, 22);
            this.txtArbID.TabIndex = 69;
            this.txtArbID.Text = "726";
            // 
            // lblArbID
            // 
            this.lblArbID.AutoSize = true;
            this.lblArbID.Location = new System.Drawing.Point(17, 11);
            this.lblArbID.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblArbID.Name = "lblArbID";
            this.lblArbID.Size = new System.Drawing.Size(82, 16);
            this.lblArbID.TabIndex = 87;
            this.lblArbID.Text = "Arb ID (HEX)";
            // 
            // byteLabel
            // 
            this.byteLabel.AutoSize = true;
            this.byteLabel.Location = new System.Drawing.Point(181, 11);
            this.byteLabel.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.byteLabel.Name = "byteLabel";
            this.byteLabel.Size = new System.Drawing.Size(111, 16);
            this.byteLabel.TabIndex = 88;
            this.byteLabel.Text = "Data Bytes (HEX)";
            // 
            // msgPanel
            // 
            this.msgPanel.BackColor = System.Drawing.SystemColors.Control;
            this.msgPanel.Controls.Add(this.pnlHexBytes);
            this.msgPanel.Controls.Add(this.lblDelayTime);
            this.msgPanel.Controls.Add(this.numMessageLength);
            this.msgPanel.Controls.Add(this.numDelayTime);
            this.msgPanel.Controls.Add(this.numCycleCount);
            this.msgPanel.Controls.Add(this.numCycleTime);
            this.msgPanel.Controls.Add(this.lblCycleCount);
            this.msgPanel.Controls.Add(this.lblMessageLength);
            this.msgPanel.Controls.Add(this.cbTrigger);
            this.msgPanel.Controls.Add(this.triggerLabel);
            this.msgPanel.Controls.Add(this.txtComment);
            this.msgPanel.Controls.Add(this.lblComment);
            this.msgPanel.Controls.Add(this.lblCycleTime);
            this.msgPanel.Controls.Add(this.btnCancel);
            this.msgPanel.Controls.Add(this.btnMsgAdd);
            this.msgPanel.Controls.Add(this.txtArbID);
            this.msgPanel.Controls.Add(this.lblArbID);
            this.msgPanel.Controls.Add(this.byteLabel);
            this.msgPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.msgPanel.Location = new System.Drawing.Point(0, 0);
            this.msgPanel.Margin = new System.Windows.Forms.Padding(4);
            this.msgPanel.Name = "msgPanel";
            this.msgPanel.Size = new System.Drawing.Size(472, 279);
            this.msgPanel.TabIndex = 0;
            // 
            // pnlHexBytes
            // 
            this.pnlHexBytes.Location = new System.Drawing.Point(184, 32);
            this.pnlHexBytes.Name = "pnlHexBytes";
            this.pnlHexBytes.Size = new System.Drawing.Size(285, 75);
            this.pnlHexBytes.TabIndex = 80;
            // 
            // lblDelayTime
            // 
            this.lblDelayTime.AutoSize = true;
            this.lblDelayTime.Location = new System.Drawing.Point(337, 120);
            this.lblDelayTime.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblDelayTime.Name = "lblDelayTime";
            this.lblDelayTime.Size = new System.Drawing.Size(106, 16);
            this.lblDelayTime.TabIndex = 102;
            this.lblDelayTime.Text = "Delay Time (ms)";
            // 
            // numMessageLength
            // 
            this.numMessageLength.Location = new System.Drawing.Point(129, 31);
            this.numMessageLength.Margin = new System.Windows.Forms.Padding(4);
            this.numMessageLength.Maximum = new decimal(new int[] {
            64,
            0,
            0,
            0});
            this.numMessageLength.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numMessageLength.Name = "numMessageLength";
            this.numMessageLength.Size = new System.Drawing.Size(48, 22);
            this.numMessageLength.TabIndex = 70;
            this.numMessageLength.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.numMessageLength.ValueChanged += new System.EventHandler(this.numMsgLen_ValChanged);
            // 
            // numDelayTime
            // 
            this.numDelayTime.Location = new System.Drawing.Point(340, 140);
            this.numDelayTime.Margin = new System.Windows.Forms.Padding(4);
            this.numDelayTime.Maximum = new decimal(new int[] {
            1661992959,
            1808227885,
            5,
            0});
            this.numDelayTime.Name = "numDelayTime";
            this.numDelayTime.Size = new System.Drawing.Size(100, 22);
            this.numDelayTime.TabIndex = 102;
            // 
            // numCycleCount
            // 
            this.numCycleCount.Location = new System.Drawing.Point(228, 140);
            this.numCycleCount.Margin = new System.Windows.Forms.Padding(4);
            this.numCycleCount.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numCycleCount.Name = "numCycleCount";
            this.numCycleCount.Size = new System.Drawing.Size(100, 22);
            this.numCycleCount.TabIndex = 101;
            // 
            // numCycleTime
            // 
            this.numCycleTime.Location = new System.Drawing.Point(118, 138);
            this.numCycleTime.Margin = new System.Windows.Forms.Padding(4);
            this.numCycleTime.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.numCycleTime.Name = "numCycleTime";
            this.numCycleTime.Size = new System.Drawing.Size(100, 22);
            this.numCycleTime.TabIndex = 100;
            // 
            // lblCycleCount
            // 
            this.lblCycleCount.AutoSize = true;
            this.lblCycleCount.Location = new System.Drawing.Point(227, 120);
            this.lblCycleCount.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblCycleCount.Name = "lblCycleCount";
            this.lblCycleCount.Size = new System.Drawing.Size(78, 16);
            this.lblCycleCount.TabIndex = 98;
            this.lblCycleCount.Text = "Cycle Count";
            // 
            // lblMessageLength
            // 
            this.lblMessageLength.AutoSize = true;
            this.lblMessageLength.Location = new System.Drawing.Point(125, 11);
            this.lblMessageLength.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblMessageLength.Name = "lblMessageLength";
            this.lblMessageLength.Size = new System.Drawing.Size(47, 16);
            this.lblMessageLength.TabIndex = 0;
            this.lblMessageLength.Text = "Length";
            // 
            // cbTrigger
            // 
            this.cbTrigger.FormattingEnabled = true;
            this.cbTrigger.Items.AddRange(new object[] {
            "Manual",
            "Periodic"});
            this.cbTrigger.Location = new System.Drawing.Point(23, 138);
            this.cbTrigger.Margin = new System.Windows.Forms.Padding(4);
            this.cbTrigger.Name = "cbTrigger";
            this.cbTrigger.Size = new System.Drawing.Size(83, 24);
            this.cbTrigger.TabIndex = 81;
            this.cbTrigger.Text = "Manual";
            this.cbTrigger.SelectedIndexChanged += new System.EventHandler(this.cbTrigger_SelectedIndexChanged);
            // 
            // triggerLabel
            // 
            this.triggerLabel.AutoSize = true;
            this.triggerLabel.Location = new System.Drawing.Point(19, 118);
            this.triggerLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.triggerLabel.Name = "triggerLabel";
            this.triggerLabel.Size = new System.Drawing.Size(51, 16);
            this.triggerLabel.TabIndex = 82;
            this.triggerLabel.Text = "Trigger";
            // 
            // txtComment
            // 
            this.txtComment.Location = new System.Drawing.Point(23, 187);
            this.txtComment.Margin = new System.Windows.Forms.Padding(5);
            this.txtComment.Name = "txtComment";
            this.txtComment.Size = new System.Drawing.Size(417, 22);
            this.txtComment.TabIndex = 84;
            // 
            // lblComment
            // 
            this.lblComment.AutoSize = true;
            this.lblComment.Location = new System.Drawing.Point(19, 168);
            this.lblComment.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblComment.Name = "lblComment";
            this.lblComment.Size = new System.Drawing.Size(64, 16);
            this.lblComment.TabIndex = 86;
            this.lblComment.Text = "Comment";
            // 
            // lblCycleTime
            // 
            this.lblCycleTime.AutoSize = true;
            this.lblCycleTime.Location = new System.Drawing.Point(117, 120);
            this.lblCycleTime.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblCycleTime.Name = "lblCycleTime";
            this.lblCycleTime.Size = new System.Drawing.Size(104, 16);
            this.lblCycleTime.TabIndex = 86;
            this.lblCycleTime.Text = "Cycle Time (ms)";
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.White;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(352, 218);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(5);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(89, 38);
            this.btnCancel.TabIndex = 86;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // FormMessageAddition
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(472, 279);
            this.ControlBox = false;
            this.Controls.Add(this.msgPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FormMessageAddition";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "New Message";
            this.Load += new System.EventHandler(this.FormMessageAddition_Load);
            this.msgPanel.ResumeLayout(false);
            this.msgPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMessageLength)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDelayTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCycleCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCycleTime)).EndInit();
            this.ResumeLayout(false);

        }
       

        #endregion

       
        internal System.Windows.Forms.Button btnMsgAdd;
        internal System.Windows.Forms.TextBox txtArbID;
        internal System.Windows.Forms.Label lblArbID;
        internal System.Windows.Forms.Label byteLabel;
        private System.Windows.Forms.Panel msgPanel;
        private System.Windows.Forms.Button btnCancel;
        internal System.Windows.Forms.Label lblCycleTime;
        private System.Windows.Forms.TextBox txtComment;
        private System.Windows.Forms.Label lblComment;
        internal System.Windows.Forms.ComboBox cbTrigger;
        private System.Windows.Forms.Label triggerLabel;
        internal System.Windows.Forms.Label lblMessageLength;
        internal System.Windows.Forms.ComboBox cbMessageLength;
        internal System.Windows.Forms.Label lblCycleCount;
        private System.Windows.Forms.NumericUpDown numMessageLength;
        private System.Windows.Forms.NumericUpDown numCycleCount;
        private System.Windows.Forms.NumericUpDown numCycleTime;
        private System.Windows.Forms.NumericUpDown numDelayTime;
        internal System.Windows.Forms.Label lblDelayTime;
        private System.Windows.Forms.Panel pnlHexBytes;
    }
}