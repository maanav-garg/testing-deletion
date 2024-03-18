namespace AutosarBCM.Forms
{
    partial class FormTestLogView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormTestLogView));
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.dateTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.itemType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.operation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.response = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rowData = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.treeView = new System.Windows.Forms.TreeView();
            this.dgvSettings = new System.Windows.Forms.Panel();
            this.btnReadFile = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtFilter = new System.Windows.Forms.TextBox();
            this.pnlStats = new System.Windows.Forms.Panel();
            this.lblTotalCountHeader = new System.Windows.Forms.Label();
            this.lblTotalCount = new System.Windows.Forms.Label();
            this.lblShortestLoopNameHeader = new System.Windows.Forms.Label();
            this.lblMinLoopTime = new System.Windows.Forms.Label();
            this.lblRxCount = new System.Windows.Forms.Label();
            this.lblAvgLoopTimeHeader = new System.Windows.Forms.Label();
            this.lblMinLoopTimeHeader = new System.Windows.Forms.Label();
            this.lblLongestLoopNameHeader = new System.Windows.Forms.Label();
            this.lblMaxLoopTime = new System.Windows.Forms.Label();
            this.lblTxCountHeader = new System.Windows.Forms.Label();
            this.lblRxCountHeader = new System.Windows.Forms.Label();
            this.lblAvgLoopTime = new System.Windows.Forms.Label();
            this.lblMaxLoopTimeHeader = new System.Windows.Forms.Label();
            this.lblLongestLoopName = new System.Windows.Forms.Label();
            this.lblTxCount = new System.Windows.Forms.Label();
            this.lblShortestLoopName = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.dgvSettings.SuspendLayout();
            this.pnlStats.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.AllowUserToOrderColumns = true;
            this.dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dataGridView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dateTime,
            this.name,
            this.itemType,
            this.operation,
            this.response,
            this.rowData});
            this.dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView.Location = new System.Drawing.Point(254, 66);
            this.dataGridView.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.RowHeadersVisible = false;
            this.dataGridView.RowHeadersWidth = 51;
            this.dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView.Size = new System.Drawing.Size(824, 571);
            this.dataGridView.TabIndex = 0;
            this.dataGridView.DataSourceChanged += new System.EventHandler(this.dataGridView_DataSourceChanged);
            // 
            // dateTime
            // 
            this.dateTime.DataPropertyName = "DateTime";
            this.dateTime.HeaderText = "Time";
            this.dateTime.MinimumWidth = 6;
            this.dateTime.Name = "dateTime";
            this.dateTime.ReadOnly = true;
            // 
            // name
            // 
            this.name.DataPropertyName = "Name";
            this.name.HeaderText = "Name";
            this.name.MinimumWidth = 6;
            this.name.Name = "name";
            this.name.ReadOnly = true;
            // 
            // itemType
            // 
            this.itemType.DataPropertyName = "ItemType";
            this.itemType.HeaderText = "Item Type";
            this.itemType.MinimumWidth = 6;
            this.itemType.Name = "itemType";
            this.itemType.ReadOnly = true;
            // 
            // operation
            // 
            this.operation.DataPropertyName = "Operation";
            this.operation.HeaderText = "Operation";
            this.operation.MinimumWidth = 6;
            this.operation.Name = "operation";
            this.operation.ReadOnly = true;
            // 
            // response
            // 
            this.response.DataPropertyName = "Response";
            this.response.HeaderText = "Response";
            this.response.MinimumWidth = 6;
            this.response.Name = "response";
            this.response.ReadOnly = true;
            // 
            // rowData
            // 
            this.rowData.DataPropertyName = "_RowData";
            this.rowData.HeaderText = "RowData";
            this.rowData.MinimumWidth = 6;
            this.rowData.Name = "rowData";
            this.rowData.ReadOnly = true;
            this.rowData.Visible = false;
            // 
            // treeView
            // 
            this.treeView.Dock = System.Windows.Forms.DockStyle.Left;
            this.treeView.Location = new System.Drawing.Point(0, 0);
            this.treeView.Margin = new System.Windows.Forms.Padding(2);
            this.treeView.Name = "treeView";
            this.treeView.Size = new System.Drawing.Size(254, 637);
            this.treeView.TabIndex = 1;
            this.treeView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.treeView_MouseDoubleClick);
            // 
            // dgvSettings
            // 
            this.dgvSettings.Controls.Add(this.btnReadFile);
            this.dgvSettings.Controls.Add(this.label1);
            this.dgvSettings.Controls.Add(this.txtFilter);
            this.dgvSettings.Controls.Add(this.pnlStats);
            this.dgvSettings.Dock = System.Windows.Forms.DockStyle.Top;
            this.dgvSettings.Location = new System.Drawing.Point(254, 0);
            this.dgvSettings.Margin = new System.Windows.Forms.Padding(2);
            this.dgvSettings.Name = "dgvSettings";
            this.dgvSettings.Size = new System.Drawing.Size(824, 66);
            this.dgvSettings.TabIndex = 2;
            // 
            // btnReadFile
            // 
            this.btnReadFile.Location = new System.Drawing.Point(2, 5);
            this.btnReadFile.Margin = new System.Windows.Forms.Padding(2);
            this.btnReadFile.Name = "btnReadFile";
            this.btnReadFile.Size = new System.Drawing.Size(76, 38);
            this.btnReadFile.TabIndex = 8;
            this.btnReadFile.Text = "Read File";
            this.btnReadFile.UseVisualStyleBackColor = true;
            this.btnReadFile.Click += new System.EventHandler(this.btnReadFile_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(582, 17);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Filter: ";
            // 
            // txtFilter
            // 
            this.txtFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFilter.Location = new System.Drawing.Point(619, 15);
            this.txtFilter.Margin = new System.Windows.Forms.Padding(2);
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(189, 20);
            this.txtFilter.TabIndex = 0;
            this.txtFilter.TextChanged += new System.EventHandler(this.txtFilter_TextChanged);
            // 
            // pnlStats
            // 
            this.pnlStats.Controls.Add(this.lblTotalCountHeader);
            this.pnlStats.Controls.Add(this.lblTotalCount);
            this.pnlStats.Controls.Add(this.lblShortestLoopNameHeader);
            this.pnlStats.Controls.Add(this.lblMinLoopTime);
            this.pnlStats.Controls.Add(this.lblRxCount);
            this.pnlStats.Controls.Add(this.lblAvgLoopTimeHeader);
            this.pnlStats.Controls.Add(this.lblMinLoopTimeHeader);
            this.pnlStats.Controls.Add(this.lblLongestLoopNameHeader);
            this.pnlStats.Controls.Add(this.lblMaxLoopTime);
            this.pnlStats.Controls.Add(this.lblTxCountHeader);
            this.pnlStats.Controls.Add(this.lblRxCountHeader);
            this.pnlStats.Controls.Add(this.lblAvgLoopTime);
            this.pnlStats.Controls.Add(this.lblMaxLoopTimeHeader);
            this.pnlStats.Controls.Add(this.lblLongestLoopName);
            this.pnlStats.Controls.Add(this.lblTxCount);
            this.pnlStats.Controls.Add(this.lblShortestLoopName);
            this.pnlStats.Location = new System.Drawing.Point(80, 6);
            this.pnlStats.Name = "pnlStats";
            this.pnlStats.Size = new System.Drawing.Size(498, 57);
            this.pnlStats.TabIndex = 9;
            this.pnlStats.Visible = false;
            // 
            // lblTotalCountHeader
            // 
            this.lblTotalCountHeader.AutoSize = true;
            this.lblTotalCountHeader.Location = new System.Drawing.Point(3, 1);
            this.lblTotalCountHeader.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTotalCountHeader.Name = "lblTotalCountHeader";
            this.lblTotalCountHeader.Size = new System.Drawing.Size(65, 13);
            this.lblTotalCountHeader.TabIndex = 2;
            this.lblTotalCountHeader.Text = "Total Count:";
            // 
            // lblTotalCount
            // 
            this.lblTotalCount.AutoSize = true;
            this.lblTotalCount.Location = new System.Drawing.Point(69, 1);
            this.lblTotalCount.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTotalCount.Name = "lblTotalCount";
            this.lblTotalCount.Size = new System.Drawing.Size(0, 13);
            this.lblTotalCount.TabIndex = 3;
            // 
            // lblShortestLoopNameHeader
            // 
            this.lblShortestLoopNameHeader.AutoSize = true;
            this.lblShortestLoopNameHeader.Location = new System.Drawing.Point(214, 18);
            this.lblShortestLoopNameHeader.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblShortestLoopNameHeader.Name = "lblShortestLoopNameHeader";
            this.lblShortestLoopNameHeader.Size = new System.Drawing.Size(76, 13);
            this.lblShortestLoopNameHeader.TabIndex = 2;
            this.lblShortestLoopNameHeader.Text = "Shortest Loop:";
            // 
            // lblMinLoopTime
            // 
            this.lblMinLoopTime.AutoSize = true;
            this.lblMinLoopTime.Location = new System.Drawing.Point(163, 17);
            this.lblMinLoopTime.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMinLoopTime.Name = "lblMinLoopTime";
            this.lblMinLoopTime.Size = new System.Drawing.Size(0, 13);
            this.lblMinLoopTime.TabIndex = 7;
            // 
            // lblRxCount
            // 
            this.lblRxCount.AutoSize = true;
            this.lblRxCount.Location = new System.Drawing.Point(65, 35);
            this.lblRxCount.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblRxCount.Name = "lblRxCount";
            this.lblRxCount.Size = new System.Drawing.Size(0, 13);
            this.lblRxCount.TabIndex = 7;
            // 
            // lblAvgLoopTimeHeader
            // 
            this.lblAvgLoopTimeHeader.AutoSize = true;
            this.lblAvgLoopTimeHeader.Location = new System.Drawing.Point(104, 34);
            this.lblAvgLoopTimeHeader.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblAvgLoopTimeHeader.Name = "lblAvgLoopTimeHeader";
            this.lblAvgLoopTimeHeader.Size = new System.Drawing.Size(59, 13);
            this.lblAvgLoopTimeHeader.TabIndex = 2;
            this.lblAvgLoopTimeHeader.Text = "Avg. Loop:";
            // 
            // lblMinLoopTimeHeader
            // 
            this.lblMinLoopTimeHeader.AutoSize = true;
            this.lblMinLoopTimeHeader.Location = new System.Drawing.Point(109, 17);
            this.lblMinLoopTimeHeader.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMinLoopTimeHeader.Name = "lblMinLoopTimeHeader";
            this.lblMinLoopTimeHeader.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblMinLoopTimeHeader.Size = new System.Drawing.Size(54, 13);
            this.lblMinLoopTimeHeader.TabIndex = 6;
            this.lblMinLoopTimeHeader.Text = "Min Loop:";
            // 
            // lblLongestLoopNameHeader
            // 
            this.lblLongestLoopNameHeader.AutoSize = true;
            this.lblLongestLoopNameHeader.Location = new System.Drawing.Point(215, 1);
            this.lblLongestLoopNameHeader.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblLongestLoopNameHeader.Name = "lblLongestLoopNameHeader";
            this.lblLongestLoopNameHeader.Size = new System.Drawing.Size(75, 13);
            this.lblLongestLoopNameHeader.TabIndex = 2;
            this.lblLongestLoopNameHeader.Text = "Longest Loop:";
            // 
            // lblMaxLoopTime
            // 
            this.lblMaxLoopTime.AutoSize = true;
            this.lblMaxLoopTime.Location = new System.Drawing.Point(163, 1);
            this.lblMaxLoopTime.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMaxLoopTime.Name = "lblMaxLoopTime";
            this.lblMaxLoopTime.Size = new System.Drawing.Size(0, 13);
            this.lblMaxLoopTime.TabIndex = 5;
            // 
            // lblTxCountHeader
            // 
            this.lblTxCountHeader.AutoSize = true;
            this.lblTxCountHeader.Location = new System.Drawing.Point(14, 17);
            this.lblTxCountHeader.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTxCountHeader.Name = "lblTxCountHeader";
            this.lblTxCountHeader.Size = new System.Drawing.Size(53, 13);
            this.lblTxCountHeader.TabIndex = 4;
            this.lblTxCountHeader.Text = "Tx Count:";
            // 
            // lblRxCountHeader
            // 
            this.lblRxCountHeader.AutoSize = true;
            this.lblRxCountHeader.Location = new System.Drawing.Point(13, 35);
            this.lblRxCountHeader.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblRxCountHeader.Name = "lblRxCountHeader";
            this.lblRxCountHeader.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblRxCountHeader.Size = new System.Drawing.Size(54, 13);
            this.lblRxCountHeader.TabIndex = 6;
            this.lblRxCountHeader.Text = "Rx Count:";
            // 
            // lblAvgLoopTime
            // 
            this.lblAvgLoopTime.AutoSize = true;
            this.lblAvgLoopTime.Location = new System.Drawing.Point(163, 35);
            this.lblAvgLoopTime.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblAvgLoopTime.Name = "lblAvgLoopTime";
            this.lblAvgLoopTime.Size = new System.Drawing.Size(0, 13);
            this.lblAvgLoopTime.TabIndex = 3;
            // 
            // lblMaxLoopTimeHeader
            // 
            this.lblMaxLoopTimeHeader.AutoSize = true;
            this.lblMaxLoopTimeHeader.Location = new System.Drawing.Point(107, 0);
            this.lblMaxLoopTimeHeader.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMaxLoopTimeHeader.Name = "lblMaxLoopTimeHeader";
            this.lblMaxLoopTimeHeader.Size = new System.Drawing.Size(57, 13);
            this.lblMaxLoopTimeHeader.TabIndex = 4;
            this.lblMaxLoopTimeHeader.Text = "Max Loop:";
            // 
            // lblLongestLoopName
            // 
            this.lblLongestLoopName.AutoSize = true;
            this.lblLongestLoopName.Location = new System.Drawing.Point(288, 2);
            this.lblLongestLoopName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblLongestLoopName.Name = "lblLongestLoopName";
            this.lblLongestLoopName.Size = new System.Drawing.Size(0, 13);
            this.lblLongestLoopName.TabIndex = 3;
            // 
            // lblTxCount
            // 
            this.lblTxCount.AutoSize = true;
            this.lblTxCount.Location = new System.Drawing.Point(65, 18);
            this.lblTxCount.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTxCount.Name = "lblTxCount";
            this.lblTxCount.Size = new System.Drawing.Size(0, 13);
            this.lblTxCount.TabIndex = 5;
            // 
            // lblShortestLoopName
            // 
            this.lblShortestLoopName.AutoSize = true;
            this.lblShortestLoopName.Location = new System.Drawing.Point(288, 19);
            this.lblShortestLoopName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblShortestLoopName.Name = "lblShortestLoopName";
            this.lblShortestLoopName.Size = new System.Drawing.Size(0, 13);
            this.lblShortestLoopName.TabIndex = 3;
            // 
            // progressBar
            // 
            this.progressBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBar.Location = new System.Drawing.Point(0, 637);
            this.progressBar.Margin = new System.Windows.Forms.Padding(2);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(1078, 18);
            this.progressBar.TabIndex = 3;
            // 
            // backgroundWorker
            // 
            this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_DoWork);
            this.backgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker_ProgressChanged);
            this.backgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker_RunWorkerCompleted);
            // 
            // FormTestLogView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1078, 655);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.dgvSettings);
            this.Controls.Add(this.treeView);
            this.Controls.Add(this.progressBar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MinimumSize = new System.Drawing.Size(700, 300);
            this.Name = "FormTestLogView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Test Log Viewer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormTestLog_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.dgvSettings.ResumeLayout(false);
            this.dgvSettings.PerformLayout();
            this.pnlStats.ResumeLayout(false);
            this.pnlStats.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TreeView treeView;
        public System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Panel dgvSettings;
        private System.Windows.Forms.TextBox txtFilter;
        private System.Windows.Forms.DataGridViewTextBoxColumn dateTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn name;
        private System.Windows.Forms.DataGridViewTextBoxColumn itemType;
        private System.Windows.Forms.DataGridViewTextBoxColumn operation;
        private System.Windows.Forms.DataGridViewTextBoxColumn response;
        private System.Windows.Forms.DataGridViewTextBoxColumn rowData;
        private System.Windows.Forms.Label lblTotalCount;
        private System.Windows.Forms.Label lblTotalCountHeader;
        private System.Windows.Forms.Label label1;
        private System.ComponentModel.BackgroundWorker backgroundWorker;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label lblRxCount;
        private System.Windows.Forms.Label lblRxCountHeader;
        private System.Windows.Forms.Label lblTxCount;
        private System.Windows.Forms.Label lblTxCountHeader;
        private System.Windows.Forms.Button btnReadFile;
        private System.Windows.Forms.Label lblMinLoopTime;
        private System.Windows.Forms.Label lblMinLoopTimeHeader;
        private System.Windows.Forms.Label lblMaxLoopTime;
        private System.Windows.Forms.Label lblMaxLoopTimeHeader;
        private System.Windows.Forms.Label lblAvgLoopTime;
        private System.Windows.Forms.Label lblAvgLoopTimeHeader;
        private System.Windows.Forms.Label lblLongestLoopName;
        private System.Windows.Forms.Label lblLongestLoopNameHeader;
        private System.Windows.Forms.Label lblShortestLoopName;
        private System.Windows.Forms.Label lblShortestLoopNameHeader;
        private System.Windows.Forms.Panel pnlStats;
    }
}