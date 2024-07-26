namespace AutosarBCM
{
    partial class FormTransmitMulti
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.dgvMessages = new System.Windows.Forms.DataGridView();
            this.comment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.messageID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.length = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.data = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cycleTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CycleCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.delayTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.count = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.trigger = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.btnUp = new System.Windows.Forms.Button();
            this.btnDown = new System.Windows.Forms.Button();
            this.txtComment = new System.Windows.Forms.TextBox();
            this.lblComment = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMessages)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.White;
            this.btnAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnAdd.Image = global::AutosarBCM.Properties.Resources.add_icon_2688193755;
            this.btnAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAdd.Location = new System.Drawing.Point(33, 4);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Padding = new System.Windows.Forms.Padding(2);
            this.btnAdd.Size = new System.Drawing.Size(124, 31);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "New Message";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.White;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnSave.Image = global::AutosarBCM.Properties.Resources.save_16xLG;
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(163, 4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(2);
            this.btnSave.Size = new System.Drawing.Size(77, 31);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // dgvMessages
            // 
            this.dgvMessages.AllowUserToAddRows = false;
            this.dgvMessages.AllowUserToDeleteRows = false;
            this.dgvMessages.AllowUserToResizeRows = false;
            this.dgvMessages.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvMessages.BackgroundColor = System.Drawing.Color.White;
            this.dgvMessages.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgvMessages.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMessages.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.comment,
            this.messageID,
            this.type,
            this.length,
            this.data,
            this.cycleTime,
            this.CycleCount,
            this.delayTime,
            this.count,
            this.trigger});
            this.dgvMessages.ContextMenuStrip = this.contextMenuStrip1;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Orange;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvMessages.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgvMessages.Location = new System.Drawing.Point(33, 41);
            this.dgvMessages.MultiSelect = false;
            this.dgvMessages.Name = "dgvMessages";
            this.dgvMessages.ReadOnly = true;
            this.dgvMessages.RowHeadersVisible = false;
            this.dgvMessages.RowHeadersWidth = 51;
            this.dgvMessages.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMessages.ShowEditingIcon = false;
            this.dgvMessages.Size = new System.Drawing.Size(1271, 331);
            this.dgvMessages.TabIndex = 4;
            this.dgvMessages.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMessages_CellDoubleClick);
            this.dgvMessages.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvMessages_CellMouseDown);
            // 
            // comment
            // 
            this.comment.DataPropertyName = "Comment";
            this.comment.HeaderText = "Comment";
            this.comment.MinimumWidth = 6;
            this.comment.Name = "comment";
            this.comment.ReadOnly = true;
            // 
            // messageID
            // 
            this.messageID.DataPropertyName = "Id";
            this.messageID.HeaderText = "MessageID";
            this.messageID.MinimumWidth = 6;
            this.messageID.Name = "messageID";
            this.messageID.ReadOnly = true;
            // 
            // type
            // 
            this.type.DataPropertyName = "Type";
            this.type.HeaderText = "Type";
            this.type.MinimumWidth = 6;
            this.type.Name = "type";
            this.type.ReadOnly = true;
            // 
            // length
            // 
            this.length.DataPropertyName = "Length";
            this.length.HeaderText = "Length";
            this.length.MinimumWidth = 6;
            this.length.Name = "length";
            this.length.ReadOnly = true;
            // 
            // data
            // 
            this.data.DataPropertyName = "DataString";
            this.data.HeaderText = "Data";
            this.data.MinimumWidth = 6;
            this.data.Name = "data";
            this.data.ReadOnly = true;
            // 
            // cycleTime
            // 
            this.cycleTime.DataPropertyName = "CycleTime";
            this.cycleTime.HeaderText = "Cycle Time";
            this.cycleTime.MinimumWidth = 6;
            this.cycleTime.Name = "cycleTime";
            this.cycleTime.ReadOnly = true;
            // 
            // CycleCount
            // 
            this.CycleCount.DataPropertyName = "CycleCount";
            this.CycleCount.HeaderText = "Cycle Count";
            this.CycleCount.MinimumWidth = 6;
            this.CycleCount.Name = "CycleCount";
            this.CycleCount.ReadOnly = true;
            // 
            // delayTime
            // 
            this.delayTime.DataPropertyName = "DelayTime";
            this.delayTime.HeaderText = "Delay Time";
            this.delayTime.MinimumWidth = 6;
            this.delayTime.Name = "delayTime";
            this.delayTime.ReadOnly = true;
            // 
            // count
            // 
            this.count.DataPropertyName = "Count";
            this.count.HeaderText = "Count";
            this.count.MinimumWidth = 6;
            this.count.Name = "count";
            this.count.ReadOnly = true;
            // 
            // trigger
            // 
            this.trigger.DataPropertyName = "Trigger";
            this.trigger.HeaderText = "Trigger";
            this.trigger.MinimumWidth = 6;
            this.trigger.Name = "trigger";
            this.trigger.ReadOnly = true;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiCopy,
            this.tsmiEdit,
            this.tsmiDelete});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(108, 70);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // tsmiCopy
            // 
            this.tsmiCopy.Name = "tsmiCopy";
            this.tsmiCopy.Size = new System.Drawing.Size(107, 22);
            this.tsmiCopy.Text = "Copy";
            this.tsmiCopy.Click += new System.EventHandler(this.tsmiCopy_Click);
            // 
            // tsmiEdit
            // 
            this.tsmiEdit.Name = "tsmiEdit";
            this.tsmiEdit.Size = new System.Drawing.Size(107, 22);
            this.tsmiEdit.Text = "Edit";
            this.tsmiEdit.Click += new System.EventHandler(this.tsmiEdit_Click);
            // 
            // tsmiDelete
            // 
            this.tsmiDelete.Name = "tsmiDelete";
            this.tsmiDelete.Size = new System.Drawing.Size(107, 22);
            this.tsmiDelete.Text = "Delete";
            this.tsmiDelete.Click += new System.EventHandler(this.tsmiDelete_Click);
            // 
            // btnUp
            // 
            this.btnUp.BackColor = System.Drawing.Color.White;
            this.btnUp.Image = global::AutosarBCM.Properties.Resources.ArrowUpEnd;
            this.btnUp.Location = new System.Drawing.Point(5, 66);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(22, 34);
            this.btnUp.TabIndex = 5;
            this.btnUp.UseVisualStyleBackColor = false;
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // btnDown
            // 
            this.btnDown.BackColor = System.Drawing.Color.White;
            this.btnDown.Image = global::AutosarBCM.Properties.Resources.ArrowDownEnd;
            this.btnDown.Location = new System.Drawing.Point(5, 106);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(22, 34);
            this.btnDown.TabIndex = 6;
            this.btnDown.UseVisualStyleBackColor = false;
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // txtComment
            // 
            this.txtComment.Location = new System.Drawing.Point(302, 11);
            this.txtComment.Margin = new System.Windows.Forms.Padding(2);
            this.txtComment.Name = "txtComment";
            this.txtComment.Size = new System.Drawing.Size(187, 20);
            this.txtComment.TabIndex = 7;
            // 
            // lblComment
            // 
            this.lblComment.AutoSize = true;
            this.lblComment.Location = new System.Drawing.Point(245, 13);
            this.lblComment.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblComment.Name = "lblComment";
            this.lblComment.Size = new System.Drawing.Size(57, 13);
            this.lblComment.TabIndex = 8;
            this.lblComment.Text = "Comment :";
            // 
            // FormTransmitMulti
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1314, 382);
            this.Controls.Add(this.lblComment);
            this.Controls.Add(this.txtComment);
            this.Controls.Add(this.btnDown);
            this.Controls.Add(this.btnUp);
            this.Controls.Add(this.dgvMessages);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnAdd);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.Name = "FormTransmitMulti";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Multi-Messages";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormTransmitMulti_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormTransmitMulti_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMessages)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.DataGridView dgvMessages;
        private System.Windows.Forms.Button btnUp;
        private System.Windows.Forms.Button btnDown;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsmiEdit;
        private System.Windows.Forms.ToolStripMenuItem tsmiDelete;
        private System.Windows.Forms.ToolStripMenuItem tsmiCopy;
        private System.Windows.Forms.Label lblComment;
        internal System.Windows.Forms.TextBox txtComment;
        private System.Windows.Forms.DataGridViewTextBoxColumn comment;
        private System.Windows.Forms.DataGridViewTextBoxColumn messageID;
        private System.Windows.Forms.DataGridViewTextBoxColumn type;
        private System.Windows.Forms.DataGridViewTextBoxColumn length;
        private System.Windows.Forms.DataGridViewTextBoxColumn data;
        private System.Windows.Forms.DataGridViewTextBoxColumn cycleTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn CycleCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn delayTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn count;
        private System.Windows.Forms.DataGridViewTextBoxColumn trigger;
    }
}