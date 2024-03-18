namespace AutosarBCM
{
    partial class FormControlChecker
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormControlChecker));
            this.btnStart = new System.Windows.Forms.Button();
            this.dgvOutput = new System.Windows.Forms.DataGridView();
            this.Select = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnCurrent1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnCurrent2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtFilter = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnImport = new System.Windows.Forms.Button();
            this.rdoOutput = new System.Windows.Forms.RadioButton();
            this.grpType = new System.Windows.Forms.GroupBox();
            this.rdoInput = new System.Windows.Forms.RadioButton();
            this.dgvInput = new System.Windows.Forms.DataGridView();
            this.Column15 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.numInterval = new System.Windows.Forms.NumericUpDown();
            this.lblInterval = new System.Windows.Forms.Label();
            this.grpOrder = new System.Windows.Forms.GroupBox();
            this.numWaitTime = new System.Windows.Forms.NumericUpDown();
            this.lblWaitTime = new System.Windows.Forms.Label();
            this.rdoVertical = new System.Windows.Forms.RadioButton();
            this.rdoHorizontal = new System.Windows.Forms.RadioButton();
            this.lblOrderNote = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOutput)).BeginInit();
            this.grpType.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numInterval)).BeginInit();
            this.grpOrder.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numWaitTime)).BeginInit();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Image = global::AutosarBCM.Properties.Resources.play_pause;
            this.btnStart.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnStart.Location = new System.Drawing.Point(108, 17);
            this.btnStart.Name = "btnStart";
            this.btnStart.Padding = new System.Windows.Forms.Padding(5);
            this.btnStart.Size = new System.Drawing.Size(96, 34);
            this.btnStart.TabIndex = 2;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // dgvOutput
            // 
            this.dgvOutput.AllowUserToAddRows = false;
            this.dgvOutput.AllowUserToDeleteRows = false;
            this.dgvOutput.AllowUserToResizeRows = false;
            this.dgvOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvOutput.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvOutput.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOutput.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Select,
            this.Column1,
            this.Column8,
            this.Column5,
            this.Column2,
            this.Column3,
            this.Column7,
            this.columnCurrent1,
            this.Column6,
            this.Column4,
            this.Column9,
            this.Column10,
            this.columnCurrent2,
            this.Column11});
            this.dgvOutput.Location = new System.Drawing.Point(12, 76);
            this.dgvOutput.Name = "dgvOutput";
            this.dgvOutput.RowHeadersVisible = false;
            this.dgvOutput.Size = new System.Drawing.Size(1530, 567);
            this.dgvOutput.TabIndex = 6;
            this.dgvOutput.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgv_CellMouseClick);
            // 
            // Select
            // 
            this.Select.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Select.FillWeight = 4.325838F;
            this.Select.HeaderText = "";
            this.Select.Name = "Select";
            this.Select.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Select.Width = 39;
            // 
            // Column1
            // 
            this.Column1.FillWeight = 5.445455F;
            this.Column1.HeaderText = "Output Name";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column8
            // 
            this.Column8.FillWeight = 5.445455F;
            this.Column8.HeaderText = "Type";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            this.Column8.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column5
            // 
            this.Column5.FillWeight = 5.445455F;
            this.Column5.HeaderText = "Input Name";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column2
            // 
            this.Column2.FillWeight = 5.445455F;
            this.Column2.HeaderText = "Open";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column3
            // 
            this.Column3.FillWeight = 5.445455F;
            this.Column3.HeaderText = "Diag";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column7
            // 
            this.Column7.FillWeight = 5.445455F;
            this.Column7.HeaderText = "ADC";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            this.Column7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // columnCurrent1
            // 
            this.columnCurrent1.FillWeight = 5.889838F;
            this.columnCurrent1.HeaderText = "Current";
            this.columnCurrent1.Name = "columnCurrent1";
            this.columnCurrent1.ReadOnly = true;
            this.columnCurrent1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column6
            // 
            this.Column6.FillWeight = 5.445455F;
            this.Column6.HeaderText = "Input";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column4
            // 
            this.Column4.FillWeight = 5.445455F;
            this.Column4.HeaderText = "Close";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column9
            // 
            this.Column9.FillWeight = 5.445455F;
            this.Column9.HeaderText = "Diag";
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            this.Column9.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column10
            // 
            this.Column10.FillWeight = 5.445455F;
            this.Column10.HeaderText = "ADC";
            this.Column10.Name = "Column10";
            this.Column10.ReadOnly = true;
            this.Column10.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // columnCurrent2
            // 
            this.columnCurrent2.FillWeight = 4.648195F;
            this.columnCurrent2.HeaderText = "Current";
            this.columnCurrent2.Name = "columnCurrent2";
            this.columnCurrent2.ReadOnly = true;
            this.columnCurrent2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column11
            // 
            this.Column11.FillWeight = 5.445455F;
            this.Column11.HeaderText = "Input";
            this.Column11.Name = "Column11";
            this.Column11.ReadOnly = true;
            this.Column11.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // btnSave
            // 
            this.btnSave.Image = global::AutosarBCM.Properties.Resources.save_16xLG;
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(210, 17);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(5);
            this.btnSave.Size = new System.Drawing.Size(89, 34);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtFilter
            // 
            this.txtFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFilter.Location = new System.Drawing.Point(1292, 24);
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(250, 20);
            this.txtFilter.TabIndex = 4;
            this.txtFilter.TextChanged += new System.EventHandler(this.txtFilter_TextChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1254, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Filter:";
            // 
            // btnImport
            // 
            this.btnImport.Image = global::AutosarBCM.Properties.Resources.Open_6529;
            this.btnImport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnImport.Location = new System.Drawing.Point(12, 17);
            this.btnImport.Name = "btnImport";
            this.btnImport.Padding = new System.Windows.Forms.Padding(5);
            this.btnImport.Size = new System.Drawing.Size(90, 33);
            this.btnImport.TabIndex = 1;
            this.btnImport.Text = "Import";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // rdoOutput
            // 
            this.rdoOutput.AutoSize = true;
            this.rdoOutput.Checked = true;
            this.rdoOutput.Location = new System.Drawing.Point(6, 19);
            this.rdoOutput.Name = "rdoOutput";
            this.rdoOutput.Size = new System.Drawing.Size(57, 17);
            this.rdoOutput.TabIndex = 7;
            this.rdoOutput.TabStop = true;
            this.rdoOutput.Text = "Output";
            this.rdoOutput.UseVisualStyleBackColor = true;
            this.rdoOutput.CheckedChanged += new System.EventHandler(this.rdoControl_CheckedChanged);
            // 
            // grpType
            // 
            this.grpType.Controls.Add(this.rdoInput);
            this.grpType.Controls.Add(this.rdoOutput);
            this.grpType.Location = new System.Drawing.Point(305, 12);
            this.grpType.Name = "grpType";
            this.grpType.Size = new System.Drawing.Size(136, 45);
            this.grpType.TabIndex = 8;
            this.grpType.TabStop = false;
            this.grpType.Text = "Control Type";
            // 
            // rdoInput
            // 
            this.rdoInput.AutoSize = true;
            this.rdoInput.Location = new System.Drawing.Point(69, 19);
            this.rdoInput.Name = "rdoInput";
            this.rdoInput.Size = new System.Drawing.Size(49, 17);
            this.rdoInput.TabIndex = 8;
            this.rdoInput.Text = "Input";
            this.rdoInput.UseVisualStyleBackColor = true;
            this.rdoInput.CheckedChanged += new System.EventHandler(this.rdoControl_CheckedChanged);
            // 
            // dgvInput
            // 
            this.dgvInput.AllowUserToAddRows = false;
            this.dgvInput.AllowUserToDeleteRows = false;
            this.dgvInput.AllowUserToResizeRows = false;
            this.dgvInput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvInput.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvInput.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInput.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column15,
            this.Column12,
            this.Column14,
            this.Column13});
            this.dgvInput.Location = new System.Drawing.Point(12, 76);
            this.dgvInput.Name = "dgvInput";
            this.dgvInput.RowHeadersVisible = false;
            this.dgvInput.Size = new System.Drawing.Size(1530, 567);
            this.dgvInput.TabIndex = 9;
            this.dgvInput.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgv_CellMouseClick);
            // 
            // Column15
            // 
            this.Column15.FillWeight = 9.979669F;
            this.Column15.HeaderText = "";
            this.Column15.Name = "Column15";
            this.Column15.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // Column12
            // 
            this.Column12.FillWeight = 127.7397F;
            this.Column12.HeaderText = "Name";
            this.Column12.Name = "Column12";
            this.Column12.ReadOnly = true;
            this.Column12.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column14
            // 
            this.Column14.FillWeight = 127.7397F;
            this.Column14.HeaderText = "Type";
            this.Column14.Name = "Column14";
            this.Column14.ReadOnly = true;
            this.Column14.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column13
            // 
            this.Column13.FillWeight = 127.7397F;
            this.Column13.HeaderText = "Value";
            this.Column13.Name = "Column13";
            this.Column13.ReadOnly = true;
            this.Column13.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // numInterval
            // 
            this.numInterval.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.numInterval.Location = new System.Drawing.Point(498, 25);
            this.numInterval.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numInterval.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numInterval.Name = "numInterval";
            this.numInterval.Size = new System.Drawing.Size(70, 21);
            this.numInterval.TabIndex = 10;
            this.numInterval.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // lblInterval
            // 
            this.lblInterval.AutoSize = true;
            this.lblInterval.Location = new System.Drawing.Point(447, 28);
            this.lblInterval.Name = "lblInterval";
            this.lblInterval.Size = new System.Drawing.Size(45, 13);
            this.lblInterval.TabIndex = 4;
            this.lblInterval.Text = "Interval:";
            // 
            // grpOrder
            // 
            this.grpOrder.Controls.Add(this.numWaitTime);
            this.grpOrder.Controls.Add(this.lblWaitTime);
            this.grpOrder.Controls.Add(this.rdoVertical);
            this.grpOrder.Controls.Add(this.rdoHorizontal);
            this.grpOrder.Location = new System.Drawing.Point(574, 12);
            this.grpOrder.Name = "grpOrder";
            this.grpOrder.Size = new System.Drawing.Size(303, 45);
            this.grpOrder.TabIndex = 11;
            this.grpOrder.TabStop = false;
            this.grpOrder.Text = "Control Order";
            // 
            // numWaitTime
            // 
            this.numWaitTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.numWaitTime.Location = new System.Drawing.Point(227, 18);
            this.numWaitTime.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numWaitTime.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numWaitTime.Name = "numWaitTime";
            this.numWaitTime.Size = new System.Drawing.Size(70, 21);
            this.numWaitTime.TabIndex = 13;
            this.numWaitTime.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // lblWaitTime
            // 
            this.lblWaitTime.AutoSize = true;
            this.lblWaitTime.Location = new System.Drawing.Point(147, 21);
            this.lblWaitTime.Name = "lblWaitTime";
            this.lblWaitTime.Size = new System.Drawing.Size(77, 13);
            this.lblWaitTime.TabIndex = 12;
            this.lblWaitTime.Text = "Wait Time (ms)";
            // 
            // rdoVertical
            // 
            this.rdoVertical.AutoSize = true;
            this.rdoVertical.Location = new System.Drawing.Point(81, 19);
            this.rdoVertical.Name = "rdoVertical";
            this.rdoVertical.Size = new System.Drawing.Size(60, 17);
            this.rdoVertical.TabIndex = 8;
            this.rdoVertical.Text = "Vertical";
            this.rdoVertical.UseVisualStyleBackColor = true;
            this.rdoVertical.CheckedChanged += new System.EventHandler(this.rdoOrder_CheckedChanged);
            // 
            // rdoHorizontal
            // 
            this.rdoHorizontal.AutoSize = true;
            this.rdoHorizontal.Checked = true;
            this.rdoHorizontal.Location = new System.Drawing.Point(6, 19);
            this.rdoHorizontal.Name = "rdoHorizontal";
            this.rdoHorizontal.Size = new System.Drawing.Size(72, 17);
            this.rdoHorizontal.TabIndex = 7;
            this.rdoHorizontal.TabStop = true;
            this.rdoHorizontal.Text = "Horizontal";
            this.rdoHorizontal.UseVisualStyleBackColor = true;
            this.rdoHorizontal.CheckedChanged += new System.EventHandler(this.rdoOrder_CheckedChanged);
            // 
            // lblOrderNote
            // 
            this.lblOrderNote.AutoSize = true;
            this.lblOrderNote.ForeColor = System.Drawing.Color.Red;
            this.lblOrderNote.Location = new System.Drawing.Point(571, 60);
            this.lblOrderNote.Name = "lblOrderNote";
            this.lblOrderNote.Size = new System.Drawing.Size(33, 13);
            this.lblOrderNote.TabIndex = 12;
            this.lblOrderNote.Text = "Note:";
            // 
            // FormControlChecker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1554, 655);
            this.Controls.Add(this.lblOrderNote);
            this.Controls.Add(this.grpOrder);
            this.Controls.Add(this.numInterval);
            this.Controls.Add(this.grpType);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.lblInterval);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtFilter);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.dgvOutput);
            this.Controls.Add(this.dgvInput);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(1000, 450);
            this.Name = "FormControlChecker";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Control Checker";
            ((System.ComponentModel.ISupportInitialize)(this.dgvOutput)).EndInit();
            this.grpType.ResumeLayout(false);
            this.grpType.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numInterval)).EndInit();
            this.grpOrder.ResumeLayout(false);
            this.grpOrder.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numWaitTime)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.DataGridView dgvOutput;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtFilter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.RadioButton rdoOutput;
        private System.Windows.Forms.GroupBox grpType;
        private System.Windows.Forms.RadioButton rdoInput;
        private System.Windows.Forms.DataGridView dgvInput;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Select;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnCurrent1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnCurrent2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
        private System.Windows.Forms.NumericUpDown numInterval;
        private System.Windows.Forms.Label lblInterval;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column15;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column12;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column14;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column13;
        private System.Windows.Forms.GroupBox grpOrder;
        private System.Windows.Forms.NumericUpDown numWaitTime;
        private System.Windows.Forms.Label lblWaitTime;
        private System.Windows.Forms.RadioButton rdoVertical;
        private System.Windows.Forms.RadioButton rdoHorizontal;
        private System.Windows.Forms.Label lblOrderNote;
    }
}