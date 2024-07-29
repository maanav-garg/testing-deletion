namespace AutosarBCM
{
    partial class FormEMCView
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
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.txtFilter = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.lblElapsedTime = new System.Windows.Forms.Label();
            this.functionEnableDropDownButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.activeFunctionEnable = new System.Windows.Forms.ToolStripMenuItem();
            this.inactiveFunctionEnable = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.pepsFunctionControlDropDownButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.activePepsFunction = new System.Windows.Forms.ToolStripMenuItem();
            this.inactivePepsFunction = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.emcControlToolStrip = new System.Windows.Forms.ToolStrip();
            this.lowBatteryProtectionDropDownButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.activeLowBatteryVoltage = new System.Windows.Forms.ToolStripMenuItem();
            this.inactiveLowBatteryVoltage = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.emcControlToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvData
            // 
            this.dgvData.AllowUserToAddRows = false;
            this.dgvData.AllowUserToDeleteRows = false;
            this.dgvData.AllowUserToResizeRows = false;
            this.dgvData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column6,
            this.Column5,
            this.Column2,
            this.Column3,
            this.Column4});
            this.dgvData.Location = new System.Drawing.Point(12, 77);
            this.dgvData.Name = "dgvData";
            this.dgvData.ReadOnly = true;
            this.dgvData.RowHeadersVisible = false;
            this.dgvData.Size = new System.Drawing.Size(1306, 572);
            this.dgvData.TabIndex = 0;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "Time";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "DID Name";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Payload";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Value";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "DTC Status";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1030, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Filter:";
            // 
            // txtFilter
            // 
            this.txtFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFilter.Location = new System.Drawing.Point(1068, 29);
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(250, 20);
            this.txtFilter.TabIndex = 6;
            this.txtFilter.TextChanged += new System.EventHandler(this.txtFilter_TextChanged);
            // 
            // btnSave
            // 
            this.btnSave.Image = global::AutosarBCM.Properties.Resources.save_16xLG;
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(114, 21);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(5);
            this.btnSave.Size = new System.Drawing.Size(89, 34);
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "Export";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnStart
            // 
            this.btnStart.Image = global::AutosarBCM.Properties.Resources.play_pause;
            this.btnStart.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnStart.Location = new System.Drawing.Point(12, 21);
            this.btnStart.Name = "btnStart";
            this.btnStart.Padding = new System.Windows.Forms.Padding(5);
            this.btnStart.Size = new System.Drawing.Size(96, 34);
            this.btnStart.TabIndex = 7;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(209, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Elapsed Time:";
            // 
            // lblElapsedTime
            // 
            this.lblElapsedTime.AutoSize = true;
            this.lblElapsedTime.Location = new System.Drawing.Point(289, 32);
            this.lblElapsedTime.Name = "lblElapsedTime";
            this.lblElapsedTime.Size = new System.Drawing.Size(34, 13);
            this.lblElapsedTime.TabIndex = 11;
            this.lblElapsedTime.Text = "00:00";
            // 
            // functionEnableDropDownButton
            // 
            this.functionEnableDropDownButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.activeFunctionEnable,
            this.inactiveFunctionEnable});
            this.functionEnableDropDownButton.Image = global::AutosarBCM.Properties.Resources.reset;
            this.functionEnableDropDownButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.functionEnableDropDownButton.Name = "functionEnableDropDownButton";
            this.functionEnableDropDownButton.Size = new System.Drawing.Size(125, 24);
            this.functionEnableDropDownButton.Text = "Function Enable";
            // 
            // activeFunctionEnable
            // 
            this.activeFunctionEnable.Name = "activeFunctionEnable";
            this.activeFunctionEnable.Size = new System.Drawing.Size(180, 22);
            this.activeFunctionEnable.Text = "Active";
            this.activeFunctionEnable.Click += new System.EventHandler(this.activeFunctionEnable_Click);
            // 
            // inactiveFunctionEnable
            // 
            this.inactiveFunctionEnable.Checked = true;
            this.inactiveFunctionEnable.CheckState = System.Windows.Forms.CheckState.Checked;
            this.inactiveFunctionEnable.Name = "inactiveFunctionEnable";
            this.inactiveFunctionEnable.Size = new System.Drawing.Size(180, 22);
            this.inactiveFunctionEnable.Text = "Inactive";
            this.inactiveFunctionEnable.Click += new System.EventHandler(this.inactiveFunctionEnable_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.ForeColor = System.Drawing.SystemColors.Control;
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 27);
            this.toolStripSeparator5.Visible = false;
            // 
            // pepsFunctionControlDropDownButton
            // 
            this.pepsFunctionControlDropDownButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.activePepsFunction,
            this.inactivePepsFunction});
            this.pepsFunctionControlDropDownButton.Image = global::AutosarBCM.Properties.Resources.reset;
            this.pepsFunctionControlDropDownButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.pepsFunctionControlDropDownButton.Name = "pepsFunctionControlDropDownButton";
            this.pepsFunctionControlDropDownButton.Size = new System.Drawing.Size(159, 24);
            this.pepsFunctionControlDropDownButton.Text = "PEPS Function Control";
            // 
            // activePepsFunction
            // 
            this.activePepsFunction.Name = "activePepsFunction";
            this.activePepsFunction.Size = new System.Drawing.Size(115, 22);
            this.activePepsFunction.Text = "Active";
            this.activePepsFunction.Click += new System.EventHandler(this.activePepsFunction_Click);
            // 
            // inactivePepsFunction
            // 
            this.inactivePepsFunction.Checked = true;
            this.inactivePepsFunction.CheckState = System.Windows.Forms.CheckState.Checked;
            this.inactivePepsFunction.Name = "inactivePepsFunction";
            this.inactivePepsFunction.Size = new System.Drawing.Size(115, 22);
            this.inactivePepsFunction.Text = "Inactive";
            this.inactivePepsFunction.Click += new System.EventHandler(this.inactivePepsFunction_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
            this.toolStripSeparator1.Visible = false;
            // 
            // emcControlToolStrip
            // 
            this.emcControlToolStrip.BackColor = System.Drawing.Color.GhostWhite;
            this.emcControlToolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.emcControlToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.emcControlToolStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.emcControlToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.functionEnableDropDownButton,
            this.toolStripSeparator5,
            this.pepsFunctionControlDropDownButton,
            this.toolStripSeparator1,
            this.lowBatteryProtectionDropDownButton});
            this.emcControlToolStrip.Location = new System.Drawing.Point(338, 25);
            this.emcControlToolStrip.Name = "emcControlToolStrip";
            this.emcControlToolStrip.Size = new System.Drawing.Size(489, 27);
            this.emcControlToolStrip.TabIndex = 13;
            this.emcControlToolStrip.Text = "toolStrip3";
            // 
            // lowBatteryProtectionDropDownButton
            // 
            this.lowBatteryProtectionDropDownButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.activeLowBatteryVoltage,
            this.inactiveLowBatteryVoltage});
            this.lowBatteryProtectionDropDownButton.Image = global::AutosarBCM.Properties.Resources.reset;
            this.lowBatteryProtectionDropDownButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.lowBatteryProtectionDropDownButton.Name = "lowBatteryProtectionDropDownButton";
            this.lowBatteryProtectionDropDownButton.Size = new System.Drawing.Size(202, 24);
            this.lowBatteryProtectionDropDownButton.Text = "Low Battery Voltage Protection";
            // 
            // activeLowBatteryVoltage
            // 
            this.activeLowBatteryVoltage.Name = "activeLowBatteryVoltage";
            this.activeLowBatteryVoltage.Size = new System.Drawing.Size(115, 22);
            this.activeLowBatteryVoltage.Text = "Active";
            this.activeLowBatteryVoltage.Click += new System.EventHandler(this.activeLowBatteryVoltage_Click);
            // 
            // inactiveLowBatteryVoltage
            // 
            this.inactiveLowBatteryVoltage.Checked = true;
            this.inactiveLowBatteryVoltage.CheckState = System.Windows.Forms.CheckState.Checked;
            this.inactiveLowBatteryVoltage.Name = "inactiveLowBatteryVoltage";
            this.inactiveLowBatteryVoltage.Size = new System.Drawing.Size(115, 22);
            this.inactiveLowBatteryVoltage.Text = "Inactive";
            this.inactiveLowBatteryVoltage.Click += new System.EventHandler(this.inactiveLowBatteryVoltage_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(115, 22);
            this.toolStripMenuItem3.Text = "Active";
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Checked = true;
            this.toolStripMenuItem4.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(115, 22);
            this.toolStripMenuItem4.Text = "Inactive";
            // 
            // FormEMCView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.GhostWhite;
            this.ClientSize = new System.Drawing.Size(1330, 661);
            this.Controls.Add(this.emcControlToolStrip);
            this.Controls.Add(this.lblElapsedTime);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtFilter);
            this.Controls.Add(this.dgvData);
            this.MinimumSize = new System.Drawing.Size(900, 500);
            this.Name = "FormEMCView";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "EMC Monitor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormEMCView_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.emcControlToolStrip.ResumeLayout(false);
            this.emcControlToolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtFilter;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblElapsedTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.ToolStripDropDownButton functionEnableDropDownButton;
        public System.Windows.Forms.ToolStripMenuItem activeFunctionEnable;
        public System.Windows.Forms.ToolStripMenuItem inactiveFunctionEnable;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripDropDownButton pepsFunctionControlDropDownButton;
        public System.Windows.Forms.ToolStripMenuItem activePepsFunction;
        public System.Windows.Forms.ToolStripMenuItem inactivePepsFunction;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStrip emcControlToolStrip;
        public System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        public System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ToolStripDropDownButton lowBatteryProtectionDropDownButton;
        public System.Windows.Forms.ToolStripMenuItem activeLowBatteryVoltage;
        public System.Windows.Forms.ToolStripMenuItem inactiveLowBatteryVoltage;
    }
}