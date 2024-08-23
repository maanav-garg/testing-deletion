namespace AutosarBCM
{
    partial class FormTransmit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormTransmit));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbNewMsg = new System.Windows.Forms.ToolStripButton();
            this.tsbTransmit = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.fileNameSeperator = new System.Windows.Forms.ToolStripSeparator();
            this.openFile = new System.Windows.Forms.ToolStripLabel();
            this.tsbMultiTransmit = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.txtFilter = new System.Windows.Forms.ToolStripTextBox();
            this.tsbImportData = new System.Windows.Forms.ToolStripButton();
            this.tsbSaveData = new System.Windows.Forms.ToolStripButton();
            this.dgvMessages = new System.Windows.Forms.DataGridView();
            this.comment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.messageID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.length = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.data = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cycleTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cycleCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.count = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.trigger = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.moveDownToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlMovementBtn = new System.Windows.Forms.Panel();
            this.btnUp = new System.Windows.Forms.Button();
            this.btnDown = new System.Windows.Forms.Button();
            this.tsbStopPeriodicMessage = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMessages)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.pnlMovementBtn.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbNewMsg,
            this.tsbTransmit,
            this.toolStripButton1,
            this.fileNameSeperator,
            this.openFile,
            this.tsbMultiTransmit,
            this.toolStripLabel1,
            this.txtFilter,
            this.tsbImportData,
            this.tsbSaveData,
            this.tsbStopPeriodicMessage});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1408, 27);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbNewMsg
            // 
            this.tsbNewMsg.Image = global::AutosarBCM.Properties.Resources.add_icon_2688193755;
            this.tsbNewMsg.Name = "tsbNewMsg";
            this.tsbNewMsg.Size = new System.Drawing.Size(125, 24);
            this.tsbNewMsg.Text = "New Message";
            this.tsbNewMsg.Click += new System.EventHandler(this.tsbNewMsg_Click);
            // 
            // tsbTransmit
            // 
            this.tsbTransmit.Image = ((System.Drawing.Image)(resources.GetObject("tsbTransmit.Image")));
            this.tsbTransmit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbTransmit.Name = "tsbTransmit";
            this.tsbTransmit.Size = new System.Drawing.Size(89, 24);
            this.tsbTransmit.Text = "Transmit";
            this.tsbTransmit.Click += new System.EventHandler(this.tsbTransmit_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(125, 24);
            this.toolStripButton1.Text = "Reset Counter";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // fileNameSeperator
            // 
            this.fileNameSeperator.Name = "fileNameSeperator";
            this.fileNameSeperator.Size = new System.Drawing.Size(6, 27);
            this.fileNameSeperator.Visible = false;
            // 
            // openFile
            // 
            this.openFile.ForeColor = System.Drawing.Color.DarkGray;
            this.openFile.Name = "openFile";
            this.openFile.Size = new System.Drawing.Size(116, 24);
            this.openFile.Text = "Open File Name";
            this.openFile.Visible = false;
            // 
            // tsbMultiTransmit
            // 
            this.tsbMultiTransmit.Image = global::AutosarBCM.Properties.Resources.msg_28364812;
            this.tsbMultiTransmit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbMultiTransmit.Name = "tsbMultiTransmit";
            this.tsbMultiTransmit.Size = new System.Drawing.Size(135, 24);
            this.tsbMultiTransmit.Text = "Multi Messages";
            this.tsbMultiTransmit.Click += new System.EventHandler(this.tsbMultiTransmit_Click);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(69, 24);
            this.toolStripLabel1.Text = "Filter by: ";
            // 
            // txtFilter
            // 
            this.txtFilter.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.txtFilter.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(232, 27);
            this.txtFilter.TextChanged += new System.EventHandler(this.txtFilter_TextChanged);
            // 
            // tsbImportData
            // 
            this.tsbImportData.Image = global::AutosarBCM.Properties.Resources.ImportOrLoad_8600_24;
            this.tsbImportData.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbImportData.Name = "tsbImportData";
            this.tsbImportData.Size = new System.Drawing.Size(114, 24);
            this.tsbImportData.Text = "Import Data";
            this.tsbImportData.Click += new System.EventHandler(this.tsbImportData_Click);
            // 
            // tsbSaveData
            // 
            this.tsbSaveData.Image = global::AutosarBCM.Properties.Resources.save_16xLG;
            this.tsbSaveData.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSaveData.Name = "tsbSaveData";
            this.tsbSaveData.Size = new System.Drawing.Size(100, 24);
            this.tsbSaveData.Text = "Save Data";
            this.tsbSaveData.Click += new System.EventHandler(this.tsbCsvTemplate_Click);
            // 
            // dgvMessages
            // 
            this.dgvMessages.AllowUserToAddRows = false;
            this.dgvMessages.AllowUserToResizeRows = false;
            this.dgvMessages.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvMessages.BackgroundColor = System.Drawing.Color.White;
            this.dgvMessages.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvMessages.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgvMessages.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMessages.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.comment,
            this.messageID,
            this.type,
            this.length,
            this.data,
            this.cycleTime,
            this.cycleCount,
            this.count,
            this.trigger});
            this.dgvMessages.ContextMenuStrip = this.contextMenuStrip1;
            this.dgvMessages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMessages.Location = new System.Drawing.Point(39, 27);
            this.dgvMessages.Margin = new System.Windows.Forms.Padding(9, 7, 9, 7);
            this.dgvMessages.MultiSelect = false;
            this.dgvMessages.Name = "dgvMessages";
            this.dgvMessages.RowHeadersVisible = false;
            this.dgvMessages.RowHeadersWidth = 51;
            this.dgvMessages.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMessages.Size = new System.Drawing.Size(1369, 332);
            this.dgvMessages.TabIndex = 54;
            this.dgvMessages.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMessages_CellDoubleClick);
            this.dgvMessages.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvMessages_CellMouseDown);
            this.dgvMessages.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvMessages_KeyDown);
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
            this.cycleTime.HeaderText = "CycleTime";
            this.cycleTime.MinimumWidth = 6;
            this.cycleTime.Name = "cycleTime";
            this.cycleTime.ReadOnly = true;
            // 
            // cycleCount
            // 
            this.cycleCount.DataPropertyName = "CycleCount";
            this.cycleCount.HeaderText = "Cycle Count";
            this.cycleCount.MinimumWidth = 6;
            this.cycleCount.Name = "cycleCount";
            this.cycleCount.ReadOnly = true;
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
            this.tsmiDelete,
            this.tsmiEdit});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(123, 76);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // tsmiCopy
            // 
            this.tsmiCopy.Name = "tsmiCopy";
            this.tsmiCopy.Size = new System.Drawing.Size(122, 24);
            this.tsmiCopy.Text = "Copy";
            this.tsmiCopy.Click += new System.EventHandler(this.tsmiCopy_Click);
            // 
            // tsmiDelete
            // 
            this.tsmiDelete.Name = "tsmiDelete";
            this.tsmiDelete.Size = new System.Drawing.Size(122, 24);
            this.tsmiDelete.Text = "Delete";
            this.tsmiDelete.Click += new System.EventHandler(this.tsmiDelete_Click);
            // 
            // tsmiEdit
            // 
            this.tsmiEdit.Name = "tsmiEdit";
            this.tsmiEdit.Size = new System.Drawing.Size(122, 24);
            this.tsmiEdit.Text = "Edit";
            this.tsmiEdit.Click += new System.EventHandler(this.tsmiEdit_Click);
            // 
            // moveDownToolStripMenuItem
            // 
            this.moveDownToolStripMenuItem.Name = "moveDownToolStripMenuItem";
            this.moveDownToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            this.moveDownToolStripMenuItem.Text = "Move Down";
            // 
            // pnlMovementBtn
            // 
            this.pnlMovementBtn.Controls.Add(this.btnUp);
            this.pnlMovementBtn.Controls.Add(this.btnDown);
            this.pnlMovementBtn.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlMovementBtn.Location = new System.Drawing.Point(0, 27);
            this.pnlMovementBtn.Margin = new System.Windows.Forms.Padding(4);
            this.pnlMovementBtn.Name = "pnlMovementBtn";
            this.pnlMovementBtn.Size = new System.Drawing.Size(39, 332);
            this.pnlMovementBtn.TabIndex = 59;
            // 
            // btnUp
            // 
            this.btnUp.BackColor = System.Drawing.Color.White;
            this.btnUp.Image = global::AutosarBCM.Properties.Resources.ArrowUpEnd;
            this.btnUp.Location = new System.Drawing.Point(4, 27);
            this.btnUp.Margin = new System.Windows.Forms.Padding(4);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(29, 42);
            this.btnUp.TabIndex = 56;
            this.btnUp.UseVisualStyleBackColor = false;
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // btnDown
            // 
            this.btnDown.BackColor = System.Drawing.Color.White;
            this.btnDown.Image = global::AutosarBCM.Properties.Resources.ArrowDownEnd;
            this.btnDown.Location = new System.Drawing.Point(4, 76);
            this.btnDown.Margin = new System.Windows.Forms.Padding(4);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(29, 42);
            this.btnDown.TabIndex = 57;
            this.btnDown.UseVisualStyleBackColor = false;
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // tsbStopPeriodicMessage
            // 
            this.tsbStopPeriodicMessage.Image = global::AutosarBCM.Properties.Resources.delete_icon_12_795858790;
            this.tsbStopPeriodicMessage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbStopPeriodicMessage.Name = "tsbStopPeriodicMessage";
            this.tsbStopPeriodicMessage.Size = new System.Drawing.Size(183, 24);
            this.tsbStopPeriodicMessage.Text = "Stop Periodic Message";
            this.tsbStopPeriodicMessage.Click += new System.EventHandler(this.stopPeriodicMessageBtn_Click);
            // 
            // FormTransmit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1408, 359);
            this.Controls.Add(this.dgvMessages);
            this.Controls.Add(this.pnlMovementBtn);
            this.Controls.Add(this.toolStrip1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FormTransmit";
            this.Text = "Transmit";
            this.Load += new System.EventHandler(this.FormTransmit_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMessages)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.pnlMovementBtn.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        public System.Windows.Forms.DataGridView dgvMessages;
        private System.Windows.Forms.ToolStripButton tsbTransmit;

        private System.Windows.Forms.ToolStripButton tsbNewMsg;

        private System.Windows.Forms.ToolStripButton toolStripButton1;
        internal System.Windows.Forms.ToolStripSeparator fileNameSeperator;
        internal System.Windows.Forms.ToolStripLabel openFile;

        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox txtFilter;
        private System.Windows.Forms.ToolStripButton tsbMultiTransmit;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsmiCopy;
        private System.Windows.Forms.ToolStripMenuItem tsmiDelete;
        private System.Windows.Forms.ToolStripMenuItem tsmiEdit;
        private System.Windows.Forms.ToolStripButton tsbSaveData;
        private System.Windows.Forms.ToolStripButton tsbImportData;
        private System.Windows.Forms.ToolStripMenuItem moveDownToolStripMenuItem;
        private System.Windows.Forms.Panel pnlMovementBtn;
        private System.Windows.Forms.Button btnUp;
        private System.Windows.Forms.Button btnDown;
        private System.Windows.Forms.DataGridViewTextBoxColumn comment;
        private System.Windows.Forms.DataGridViewTextBoxColumn messageID;
        private System.Windows.Forms.DataGridViewTextBoxColumn type;
        private System.Windows.Forms.DataGridViewTextBoxColumn length;
        private System.Windows.Forms.DataGridViewTextBoxColumn data;
        private System.Windows.Forms.DataGridViewTextBoxColumn cycleTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn cycleCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn count;
        private System.Windows.Forms.DataGridViewTextBoxColumn trigger;
        private System.Windows.Forms.ToolStripButton tsbStopPeriodicMessage;
    }
}