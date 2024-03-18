namespace AutosarBCM
{
    partial class FormOptions
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("General");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Tool", new System.Windows.Forms.TreeNode[] {
            treeNode1});
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("General");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Serial Port");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Can Hardware");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Communication", new System.Windows.Forms.TreeNode[] {
            treeNode3,
            treeNode4,
            treeNode5});
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileTsmi = new System.Windows.Forms.ToolStripMenuItem();
            this.loadTsmi = new System.Windows.Forms.ToolStripMenuItem();
            this.exportTsmi = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.treeView = new System.Windows.Forms.TreeView();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageProp1 = new System.Windows.Forms.TabPage();
            this.label11 = new System.Windows.Forms.Label();
            this.numRollingAfter = new System.Windows.Forms.NumericUpDown();
            this.label12 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.numFlushToFile = new System.Windows.Forms.NumericUpDown();
            this.numFlushToUI = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxUserName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPageProp2 = new System.Windows.Forms.TabPage();
            this.textBoxProject = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tabSerialPort = new System.Windows.Forms.TabPage();
            this.lblSerialPortType = new System.Windows.Forms.Label();
            this.cmbSerialPortType = new System.Windows.Forms.ComboBox();
            this.numWriteTimeout = new System.Windows.Forms.NumericUpDown();
            this.lblWriteTimeout = new System.Windows.Forms.Label();
            this.numReadTimeout = new System.Windows.Forms.NumericUpDown();
            this.lblReadTimeout = new System.Windows.Forms.Label();
            this.cmbStopBits = new System.Windows.Forms.ComboBox();
            this.cmbParity = new System.Windows.Forms.ComboBox();
            this.numDataBits = new System.Windows.Forms.NumericUpDown();
            this.numBaudRate = new System.Windows.Forms.NumericUpDown();
            this.lblStopBits = new System.Windows.Forms.Label();
            this.lblParity = new System.Windows.Forms.Label();
            this.lblDataBits = new System.Windows.Forms.Label();
            this.lblBaudRate = new System.Windows.Forms.Label();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.lblPort = new System.Windows.Forms.Label();
            this.tabCanHardware = new System.Windows.Forms.TabPage();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.tabCanHardware_grpIntrepid = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tabCanHardware_cmbNetworkId = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tabCanHardware_cmbBitRate = new System.Windows.Forms.ComboBox();
            this.tabCanHardware_grpKvaser = new System.Windows.Forms.GroupBox();
            this.label14 = new System.Windows.Forms.Label();
            this.tabCanHardware_cmbKvaserBitRate = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tabCanHardware_cmbDevice = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabPageProp1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRollingAfter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFlushToFile)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFlushToUI)).BeginInit();
            this.tabPageProp2.SuspendLayout();
            this.tabSerialPort.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numWriteTimeout)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numReadTimeout)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDataBits)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numBaudRate)).BeginInit();
            this.tabCanHardware.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.tabCanHardware_grpIntrepid.SuspendLayout();
            this.tabCanHardware_grpKvaser.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileTsmi});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(624, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileTsmi
            // 
            this.fileTsmi.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadTsmi,
            this.exportTsmi});
            this.fileTsmi.Name = "fileTsmi";
            this.fileTsmi.Size = new System.Drawing.Size(37, 20);
            this.fileTsmi.Text = "File";
            this.fileTsmi.Visible = false;
            // 
            // loadTsmi
            // 
            this.loadTsmi.Image = global::AutosarBCM.Properties.Resources.Open_6529;
            this.loadTsmi.Name = "loadTsmi";
            this.loadTsmi.Size = new System.Drawing.Size(184, 26);
            this.loadTsmi.Text = "Load...";
            this.loadTsmi.Click += new System.EventHandler(this.loadTsmi_Click);
            // 
            // exportTsmi
            // 
            this.exportTsmi.Name = "exportTsmi";
            this.exportTsmi.Size = new System.Drawing.Size(184, 26);
            this.exportTsmi.Text = "Export...";
            this.exportTsmi.Click += new System.EventHandler(this.exportTsmi_Click);
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 24);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.treeView);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.tabControl);
            this.splitContainer.Size = new System.Drawing.Size(624, 344);
            this.splitContainer.SplitterDistance = 207;
            this.splitContainer.TabIndex = 1;
            // 
            // treeView
            // 
            this.treeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView.FullRowSelect = true;
            this.treeView.HideSelection = false;
            this.treeView.Location = new System.Drawing.Point(0, 0);
            this.treeView.Name = "treeView";
            treeNode1.Name = "NodeToolGeneral";
            treeNode1.Text = "General";
            treeNode2.Name = "NodeTool";
            treeNode2.Text = "Tool";
            treeNode3.Name = "NodeCommGeneral";
            treeNode3.Text = "General";
            treeNode4.Name = "NodeCommSerial";
            treeNode4.Text = "Serial Port";
            treeNode5.Name = "nodeCommCanHardware";
            treeNode5.Text = "Can Hardware";
            treeNode6.Name = "NodeComm";
            treeNode6.Text = "Communication";
            this.treeView.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode2,
            treeNode6});
            this.treeView.ShowLines = false;
            this.treeView.Size = new System.Drawing.Size(207, 344);
            this.treeView.TabIndex = 0;
            this.treeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView_AfterSelect);
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPageProp1);
            this.tabControl.Controls.Add(this.tabPageProp2);
            this.tabControl.Controls.Add(this.tabSerialPort);
            this.tabControl.Controls.Add(this.tabCanHardware);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(413, 344);
            this.tabControl.TabIndex = 0;
            // 
            // tabPageProp1
            // 
            this.tabPageProp1.Controls.Add(this.label11);
            this.tabPageProp1.Controls.Add(this.numRollingAfter);
            this.tabPageProp1.Controls.Add(this.label12);
            this.tabPageProp1.Controls.Add(this.label10);
            this.tabPageProp1.Controls.Add(this.label9);
            this.tabPageProp1.Controls.Add(this.btnBrowse);
            this.tabPageProp1.Controls.Add(this.txtFilePath);
            this.tabPageProp1.Controls.Add(this.label8);
            this.tabPageProp1.Controls.Add(this.numFlushToFile);
            this.tabPageProp1.Controls.Add(this.numFlushToUI);
            this.tabPageProp1.Controls.Add(this.label7);
            this.tabPageProp1.Controls.Add(this.label6);
            this.tabPageProp1.Controls.Add(this.textBoxUserName);
            this.tabPageProp1.Controls.Add(this.label1);
            this.tabPageProp1.Location = new System.Drawing.Point(4, 22);
            this.tabPageProp1.Name = "tabPageProp1";
            this.tabPageProp1.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.tabPageProp1.Size = new System.Drawing.Size(405, 318);
            this.tabPageProp1.TabIndex = 0;
            this.tabPageProp1.Text = "Config group 1";
            this.tabPageProp1.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(183, 114);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(29, 13);
            this.label11.TabIndex = 13;
            this.label11.Text = "(MB)";
            // 
            // numRollingAfter
            // 
            this.numRollingAfter.Location = new System.Drawing.Point(77, 107);
            this.numRollingAfter.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numRollingAfter.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numRollingAfter.Name = "numRollingAfter";
            this.numRollingAfter.Size = new System.Drawing.Size(100, 20);
            this.numRollingAfter.TabIndex = 12;
            this.numRollingAfter.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(6, 109);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(63, 13);
            this.label12.TabIndex = 11;
            this.label12.Text = "Rolling after";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(183, 61);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(57, 13);
            this.label10.TabIndex = 10;
            this.label10.Text = "(Log entry)";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(183, 88);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(38, 13);
            this.label9.TabIndex = 9;
            this.label9.Text = "(Lines)";
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(77, 167);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnBrowse.TabIndex = 8;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // txtFilePath
            // 
            this.txtFilePath.Location = new System.Drawing.Point(77, 134);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.Size = new System.Drawing.Size(205, 20);
            this.txtFilePath.TabIndex = 7;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 137);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(48, 13);
            this.label8.TabIndex = 6;
            this.label8.Text = "File Path";
            // 
            // numFlushToFile
            // 
            this.numFlushToFile.Location = new System.Drawing.Point(77, 81);
            this.numFlushToFile.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.numFlushToFile.Minimum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numFlushToFile.Name = "numFlushToFile";
            this.numFlushToFile.Size = new System.Drawing.Size(100, 20);
            this.numFlushToFile.TabIndex = 5;
            this.numFlushToFile.Value = new decimal(new int[] {
            200,
            0,
            0,
            0});
            // 
            // numFlushToUI
            // 
            this.numFlushToUI.Location = new System.Drawing.Point(77, 54);
            this.numFlushToUI.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numFlushToUI.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numFlushToUI.Name = "numFlushToUI";
            this.numFlushToUI.Size = new System.Drawing.Size(100, 20);
            this.numFlushToUI.TabIndex = 4;
            this.numFlushToUI.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 83);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(60, 13);
            this.label7.TabIndex = 3;
            this.label7.Text = "Flush to file";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 56);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(58, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "Flush to UI";
            // 
            // textBoxUserName
            // 
            this.textBoxUserName.Location = new System.Drawing.Point(77, 19);
            this.textBoxUserName.Name = "textBoxUserName";
            this.textBoxUserName.Size = new System.Drawing.Size(100, 20);
            this.textBoxUserName.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "User Name";
            // 
            // tabPageProp2
            // 
            this.tabPageProp2.Controls.Add(this.textBoxProject);
            this.tabPageProp2.Controls.Add(this.label2);
            this.tabPageProp2.Location = new System.Drawing.Point(4, 22);
            this.tabPageProp2.Name = "tabPageProp2";
            this.tabPageProp2.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.tabPageProp2.Size = new System.Drawing.Size(405, 319);
            this.tabPageProp2.TabIndex = 1;
            this.tabPageProp2.Text = "Config group 2";
            this.tabPageProp2.UseVisualStyleBackColor = true;
            // 
            // textBoxProject
            // 
            this.textBoxProject.Location = new System.Drawing.Point(77, 19);
            this.textBoxProject.Name = "textBoxProject";
            this.textBoxProject.Size = new System.Drawing.Size(100, 20);
            this.textBoxProject.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Project";
            // 
            // tabSerialPort
            // 
            this.tabSerialPort.Controls.Add(this.lblSerialPortType);
            this.tabSerialPort.Controls.Add(this.cmbSerialPortType);
            this.tabSerialPort.Controls.Add(this.numWriteTimeout);
            this.tabSerialPort.Controls.Add(this.lblWriteTimeout);
            this.tabSerialPort.Controls.Add(this.numReadTimeout);
            this.tabSerialPort.Controls.Add(this.lblReadTimeout);
            this.tabSerialPort.Controls.Add(this.cmbStopBits);
            this.tabSerialPort.Controls.Add(this.cmbParity);
            this.tabSerialPort.Controls.Add(this.numDataBits);
            this.tabSerialPort.Controls.Add(this.numBaudRate);
            this.tabSerialPort.Controls.Add(this.lblStopBits);
            this.tabSerialPort.Controls.Add(this.lblParity);
            this.tabSerialPort.Controls.Add(this.lblDataBits);
            this.tabSerialPort.Controls.Add(this.lblBaudRate);
            this.tabSerialPort.Controls.Add(this.txtPort);
            this.tabSerialPort.Controls.Add(this.lblPort);
            this.tabSerialPort.Location = new System.Drawing.Point(4, 22);
            this.tabSerialPort.Name = "tabSerialPort";
            this.tabSerialPort.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.tabSerialPort.Size = new System.Drawing.Size(405, 319);
            this.tabSerialPort.TabIndex = 2;
            this.tabSerialPort.Text = "Serial Port";
            this.tabSerialPort.UseVisualStyleBackColor = true;
            // 
            // lblSerialPortType
            // 
            this.lblSerialPortType.AutoSize = true;
            this.lblSerialPortType.Location = new System.Drawing.Point(6, 15);
            this.lblSerialPortType.Name = "lblSerialPortType";
            this.lblSerialPortType.Size = new System.Drawing.Size(82, 13);
            this.lblSerialPortType.TabIndex = 18;
            this.lblSerialPortType.Text = "Serial Port Type";
            // 
            // cmbSerialPortType
            // 
            this.cmbSerialPortType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSerialPortType.FormattingEnabled = true;
            this.cmbSerialPortType.Items.AddRange(new object[] {
            "None",
            "Odd",
            "Even",
            "Mark",
            "Space"});
            this.cmbSerialPortType.Location = new System.Drawing.Point(94, 12);
            this.cmbSerialPortType.Name = "cmbSerialPortType";
            this.cmbSerialPortType.Size = new System.Drawing.Size(100, 21);
            this.cmbSerialPortType.TabIndex = 17;
            // 
            // numWriteTimeout
            // 
            this.numWriteTimeout.Location = new System.Drawing.Point(94, 197);
            this.numWriteTimeout.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numWriteTimeout.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.numWriteTimeout.Name = "numWriteTimeout";
            this.numWriteTimeout.Size = new System.Drawing.Size(100, 20);
            this.numWriteTimeout.TabIndex = 16;
            // 
            // lblWriteTimeout
            // 
            this.lblWriteTimeout.AutoSize = true;
            this.lblWriteTimeout.Location = new System.Drawing.Point(6, 202);
            this.lblWriteTimeout.Name = "lblWriteTimeout";
            this.lblWriteTimeout.Size = new System.Drawing.Size(73, 13);
            this.lblWriteTimeout.TabIndex = 15;
            this.lblWriteTimeout.Text = "Write Timeout";
            // 
            // numReadTimeout
            // 
            this.numReadTimeout.Location = new System.Drawing.Point(94, 171);
            this.numReadTimeout.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numReadTimeout.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.numReadTimeout.Name = "numReadTimeout";
            this.numReadTimeout.Size = new System.Drawing.Size(100, 20);
            this.numReadTimeout.TabIndex = 14;
            // 
            // lblReadTimeout
            // 
            this.lblReadTimeout.AutoSize = true;
            this.lblReadTimeout.Location = new System.Drawing.Point(6, 176);
            this.lblReadTimeout.Name = "lblReadTimeout";
            this.lblReadTimeout.Size = new System.Drawing.Size(74, 13);
            this.lblReadTimeout.TabIndex = 13;
            this.lblReadTimeout.Text = "Read Timeout";
            // 
            // cmbStopBits
            // 
            this.cmbStopBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStopBits.FormattingEnabled = true;
            this.cmbStopBits.Items.AddRange(new object[] {
            "None",
            "One",
            "Two",
            "OnePointFive"});
            this.cmbStopBits.Location = new System.Drawing.Point(94, 142);
            this.cmbStopBits.Name = "cmbStopBits";
            this.cmbStopBits.Size = new System.Drawing.Size(100, 21);
            this.cmbStopBits.TabIndex = 12;
            // 
            // cmbParity
            // 
            this.cmbParity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbParity.FormattingEnabled = true;
            this.cmbParity.Items.AddRange(new object[] {
            "None",
            "Odd",
            "Even",
            "Mark",
            "Space"});
            this.cmbParity.Location = new System.Drawing.Point(94, 116);
            this.cmbParity.Name = "cmbParity";
            this.cmbParity.Size = new System.Drawing.Size(100, 21);
            this.cmbParity.TabIndex = 11;
            // 
            // numDataBits
            // 
            this.numDataBits.Location = new System.Drawing.Point(94, 91);
            this.numDataBits.Name = "numDataBits";
            this.numDataBits.Size = new System.Drawing.Size(100, 20);
            this.numDataBits.TabIndex = 10;
            // 
            // numBaudRate
            // 
            this.numBaudRate.Location = new System.Drawing.Point(94, 65);
            this.numBaudRate.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.numBaudRate.Name = "numBaudRate";
            this.numBaudRate.Size = new System.Drawing.Size(100, 20);
            this.numBaudRate.TabIndex = 9;
            // 
            // lblStopBits
            // 
            this.lblStopBits.AutoSize = true;
            this.lblStopBits.Location = new System.Drawing.Point(6, 150);
            this.lblStopBits.Name = "lblStopBits";
            this.lblStopBits.Size = new System.Drawing.Size(49, 13);
            this.lblStopBits.TabIndex = 8;
            this.lblStopBits.Text = "Stop Bits";
            // 
            // lblParity
            // 
            this.lblParity.AutoSize = true;
            this.lblParity.Location = new System.Drawing.Point(6, 124);
            this.lblParity.Name = "lblParity";
            this.lblParity.Size = new System.Drawing.Size(33, 13);
            this.lblParity.TabIndex = 6;
            this.lblParity.Text = "Parity";
            // 
            // lblDataBits
            // 
            this.lblDataBits.AutoSize = true;
            this.lblDataBits.Location = new System.Drawing.Point(6, 98);
            this.lblDataBits.Name = "lblDataBits";
            this.lblDataBits.Size = new System.Drawing.Size(50, 13);
            this.lblDataBits.TabIndex = 4;
            this.lblDataBits.Text = "Data Bits";
            // 
            // lblBaudRate
            // 
            this.lblBaudRate.AutoSize = true;
            this.lblBaudRate.Location = new System.Drawing.Point(6, 72);
            this.lblBaudRate.Name = "lblBaudRate";
            this.lblBaudRate.Size = new System.Drawing.Size(58, 13);
            this.lblBaudRate.TabIndex = 2;
            this.lblBaudRate.Text = "Baud Rate";
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(94, 39);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(100, 20);
            this.txtPort.TabIndex = 1;
            // 
            // lblPort
            // 
            this.lblPort.AutoSize = true;
            this.lblPort.Location = new System.Drawing.Point(6, 46);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(26, 13);
            this.lblPort.TabIndex = 0;
            this.lblPort.Text = "Port";
            // 
            // tabCanHardware
            // 
            this.tabCanHardware.Controls.Add(this.flowLayoutPanel1);
            this.tabCanHardware.Controls.Add(this.label3);
            this.tabCanHardware.Controls.Add(this.tabCanHardware_cmbDevice);
            this.tabCanHardware.Location = new System.Drawing.Point(4, 22);
            this.tabCanHardware.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabCanHardware.Name = "tabCanHardware";
            this.tabCanHardware.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabCanHardware.Size = new System.Drawing.Size(405, 319);
            this.tabCanHardware.TabIndex = 3;
            this.tabCanHardware.Text = "Can Hardware";
            this.tabCanHardware.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.tabCanHardware_grpIntrepid);
            this.flowLayoutPanel1.Controls.Add(this.tabCanHardware_grpKvaser);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 54);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(401, 264);
            this.flowLayoutPanel1.TabIndex = 2;
            // 
            // tabCanHardware_grpIntrepid
            // 
            this.tabCanHardware_grpIntrepid.Controls.Add(this.label5);
            this.tabCanHardware_grpIntrepid.Controls.Add(this.tabCanHardware_cmbNetworkId);
            this.tabCanHardware_grpIntrepid.Controls.Add(this.label4);
            this.tabCanHardware_grpIntrepid.Controls.Add(this.tabCanHardware_cmbBitRate);
            this.tabCanHardware_grpIntrepid.Location = new System.Drawing.Point(2, 2);
            this.tabCanHardware_grpIntrepid.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabCanHardware_grpIntrepid.Name = "tabCanHardware_grpIntrepid";
            this.tabCanHardware_grpIntrepid.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabCanHardware_grpIntrepid.Size = new System.Drawing.Size(397, 81);
            this.tabCanHardware_grpIntrepid.TabIndex = 0;
            this.tabCanHardware_grpIntrepid.TabStop = false;
            this.tabCanHardware_grpIntrepid.Text = "Intrepid";
            this.tabCanHardware_grpIntrepid.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 44);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Can Network";
            // 
            // tabCanHardware_cmbNetworkId
            // 
            this.tabCanHardware_cmbNetworkId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tabCanHardware_cmbNetworkId.FormattingEnabled = true;
            this.tabCanHardware_cmbNetworkId.Items.AddRange(new object[] {
            "NETID_HSCAN",
            "NETID_MSCAN",
            "NETID_SWCAN",
            "NETID_HSCAN2",
            "NETID_HSCAN3",
            "NETID_LSFTCAN",
            "NETID_LIN",
            "NETID_ISO2",
            "NETID_FIRE_LIN2",
            "NETID_FIRE_LIN3",
            "NETID_FIRE_LIN4",
            "NETID_FIRE_CGI"});
            this.tabCanHardware_cmbNetworkId.Location = new System.Drawing.Point(88, 41);
            this.tabCanHardware_cmbNetworkId.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabCanHardware_cmbNetworkId.Name = "tabCanHardware_cmbNetworkId";
            this.tabCanHardware_cmbNetworkId.Size = new System.Drawing.Size(153, 21);
            this.tabCanHardware_cmbNetworkId.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 20);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Bit Rate";
            // 
            // tabCanHardware_cmbBitRate
            // 
            this.tabCanHardware_cmbBitRate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tabCanHardware_cmbBitRate.FormattingEnabled = true;
            this.tabCanHardware_cmbBitRate.Items.AddRange(new object[] {
            "0",
            "2000",
            "33333",
            "50000",
            "62500",
            "83333",
            "100000",
            "125000",
            "250000",
            "500000",
            "800000",
            "1000000"});
            this.tabCanHardware_cmbBitRate.Location = new System.Drawing.Point(88, 17);
            this.tabCanHardware_cmbBitRate.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabCanHardware_cmbBitRate.Name = "tabCanHardware_cmbBitRate";
            this.tabCanHardware_cmbBitRate.Size = new System.Drawing.Size(153, 21);
            this.tabCanHardware_cmbBitRate.TabIndex = 1;
            // 
            // tabCanHardware_grpKvaser
            // 
            this.tabCanHardware_grpKvaser.Controls.Add(this.label14);
            this.tabCanHardware_grpKvaser.Controls.Add(this.tabCanHardware_cmbKvaserBitRate);
            this.tabCanHardware_grpKvaser.Location = new System.Drawing.Point(2, 87);
            this.tabCanHardware_grpKvaser.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabCanHardware_grpKvaser.Name = "tabCanHardware_grpKvaser";
            this.tabCanHardware_grpKvaser.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabCanHardware_grpKvaser.Size = new System.Drawing.Size(397, 51);
            this.tabCanHardware_grpKvaser.TabIndex = 1;
            this.tabCanHardware_grpKvaser.TabStop = false;
            this.tabCanHardware_grpKvaser.Text = "Kvaser";
            this.tabCanHardware_grpKvaser.Visible = false;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(11, 20);
            this.label14.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(45, 13);
            this.label14.TabIndex = 2;
            this.label14.Text = "Bit Rate";
            // 
            // tabCanHardware_cmbKvaserBitRate
            // 
            this.tabCanHardware_cmbKvaserBitRate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tabCanHardware_cmbKvaserBitRate.FormattingEnabled = true;
            this.tabCanHardware_cmbKvaserBitRate.Items.AddRange(new object[] {
            "50000",
            "62500",
            "100000",
            "125000",
            "250000",
            "500000",
            "1000000"});
            this.tabCanHardware_cmbKvaserBitRate.Location = new System.Drawing.Point(88, 17);
            this.tabCanHardware_cmbKvaserBitRate.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabCanHardware_cmbKvaserBitRate.Name = "tabCanHardware_cmbKvaserBitRate";
            this.tabCanHardware_cmbKvaserBitRate.Size = new System.Drawing.Size(153, 21);
            this.tabCanHardware_cmbKvaserBitRate.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 17);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Can Device";
            // 
            // tabCanHardware_cmbDevice
            // 
            this.tabCanHardware_cmbDevice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tabCanHardware_cmbDevice.FormattingEnabled = true;
            this.tabCanHardware_cmbDevice.Items.AddRange(new object[] {
            "Intrepid",
            "Kvaser",
            "Vector"});
            this.tabCanHardware_cmbDevice.Location = new System.Drawing.Point(93, 15);
            this.tabCanHardware_cmbDevice.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabCanHardware_cmbDevice.Name = "tabCanHardware_cmbDevice";
            this.tabCanHardware_cmbDevice.Size = new System.Drawing.Size(92, 21);
            this.tabCanHardware_cmbDevice.TabIndex = 0;
            this.tabCanHardware_cmbDevice.SelectedIndexChanged += new System.EventHandler(this.tabCanHardware_cmbDevice_SelectedIndexChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonOk);
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 368);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(624, 36);
            this.panel1.TabIndex = 2;
            // 
            // buttonOk
            // 
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Location = new System.Drawing.Point(459, 7);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 1;
            this.buttonOk.Text = "OK";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(540, 7);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 0;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // FormOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(624, 404);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.panel1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormOptions";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Options";
            this.Load += new System.EventHandler(this.FormOptions_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.tabPageProp1.ResumeLayout(false);
            this.tabPageProp1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRollingAfter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFlushToFile)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFlushToUI)).EndInit();
            this.tabPageProp2.ResumeLayout(false);
            this.tabPageProp2.PerformLayout();
            this.tabSerialPort.ResumeLayout(false);
            this.tabSerialPort.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numWriteTimeout)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numReadTimeout)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDataBits)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numBaudRate)).EndInit();
            this.tabCanHardware.ResumeLayout(false);
            this.tabCanHardware.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.tabCanHardware_grpIntrepid.ResumeLayout(false);
            this.tabCanHardware_grpIntrepid.PerformLayout();
            this.tabCanHardware_grpKvaser.ResumeLayout(false);
            this.tabCanHardware_grpKvaser.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileTsmi;
        private System.Windows.Forms.ToolStripMenuItem loadTsmi;
        private System.Windows.Forms.ToolStripMenuItem exportTsmi;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.TreeView treeView;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageProp1;
        private System.Windows.Forms.TabPage tabPageProp2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxProject;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.TabPage tabSerialPort;
        private System.Windows.Forms.Label lblPort;
        private System.Windows.Forms.Label lblStopBits;
        private System.Windows.Forms.Label lblParity;
        private System.Windows.Forms.Label lblDataBits;
        private System.Windows.Forms.Label lblBaudRate;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.NumericUpDown numDataBits;
        private System.Windows.Forms.NumericUpDown numBaudRate;
        private System.Windows.Forms.ComboBox cmbStopBits;
        private System.Windows.Forms.ComboBox cmbParity;
        private System.Windows.Forms.NumericUpDown numReadTimeout;
        private System.Windows.Forms.Label lblReadTimeout;
        private System.Windows.Forms.NumericUpDown numWriteTimeout;
        private System.Windows.Forms.Label lblWriteTimeout;
        private System.Windows.Forms.TabPage tabCanHardware;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox tabCanHardware_cmbDevice;
        private System.Windows.Forms.TextBox textBoxUserName;
        private System.Windows.Forms.GroupBox tabCanHardware_grpIntrepid;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox tabCanHardware_cmbNetworkId;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox tabCanHardware_cmbBitRate;
        private System.Windows.Forms.Label lblSerialPortType;
        private System.Windows.Forms.ComboBox cmbSerialPortType;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numFlushToFile;
        private System.Windows.Forms.NumericUpDown numFlushToUI;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.TextBox txtFilePath;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.NumericUpDown numRollingAfter;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox tabCanHardware_grpKvaser;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox tabCanHardware_cmbKvaserBitRate;
    }
}