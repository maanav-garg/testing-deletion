using System.Windows.Forms;

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
            System.Windows.Forms.TreeNode treeNode15 = new System.Windows.Forms.TreeNode("General");
            System.Windows.Forms.TreeNode treeNode16 = new System.Windows.Forms.TreeNode("Tool", new System.Windows.Forms.TreeNode[] {
            treeNode15});
            System.Windows.Forms.TreeNode treeNode17 = new System.Windows.Forms.TreeNode("General");
            System.Windows.Forms.TreeNode treeNode18 = new System.Windows.Forms.TreeNode("Serial Port");
            System.Windows.Forms.TreeNode treeNode19 = new System.Windows.Forms.TreeNode("Can Hardware");
            System.Windows.Forms.TreeNode treeNode20 = new System.Windows.Forms.TreeNode("TX/RX Filter");
            System.Windows.Forms.TreeNode treeNode21 = new System.Windows.Forms.TreeNode("Communication", new System.Windows.Forms.TreeNode[] {
            treeNode17,
            treeNode18,
            treeNode19,
            treeNode20});
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
            this.txtPaddingByte = new System.Windows.Forms.TextBox();
            this.lblPaddingByte = new System.Windows.Forms.Label();
            this.txtStMin = new System.Windows.Forms.TextBox();
            this.lblStMin = new System.Windows.Forms.Label();
            this.txtBlockSize = new System.Windows.Forms.TextBox();
            this.lblBlockSize = new System.Windows.Forms.Label();
            this.txtReceiveAdress = new System.Windows.Forms.TextBox();
            this.lblReceiveAdress = new System.Windows.Forms.Label();
            this.txtTransmitAdress = new System.Windows.Forms.TextBox();
            this.lblTransmitAdress = new System.Windows.Forms.Label();
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
            this.tabFilterPage = new System.Windows.Forms.TabPage();
            this.btnAddFilter = new System.Windows.Forms.Button();
            this.tbFilter = new System.Windows.Forms.TextBox();
            this.btnClearFilter = new System.Windows.Forms.Button();
            this.btnDeleteFilter = new System.Windows.Forms.Button();
            this.lbFilterPage = new System.Windows.Forms.ListBox();
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
            this.tabFilterPage.SuspendLayout();
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
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(832, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileTsmi
            // 
            this.fileTsmi.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadTsmi,
            this.exportTsmi});
            this.fileTsmi.Name = "fileTsmi";
            this.fileTsmi.Size = new System.Drawing.Size(46, 24);
            this.fileTsmi.Text = "File";
            this.fileTsmi.Visible = false;
            // 
            // loadTsmi
            // 
            this.loadTsmi.Image = global::AutosarBCM.Properties.Resources.Open_6529;
            this.loadTsmi.Name = "loadTsmi";
            this.loadTsmi.Size = new System.Drawing.Size(144, 26);
            this.loadTsmi.Text = "Load...";
            this.loadTsmi.Click += new System.EventHandler(this.loadTsmi_Click);
            // 
            // exportTsmi
            // 
            this.exportTsmi.Name = "exportTsmi";
            this.exportTsmi.Size = new System.Drawing.Size(144, 26);
            this.exportTsmi.Text = "Export...";
            this.exportTsmi.Click += new System.EventHandler(this.exportTsmi_Click);
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 24);
            this.splitContainer.Margin = new System.Windows.Forms.Padding(4);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.treeView);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.tabControl);
            this.splitContainer.Size = new System.Drawing.Size(832, 429);
            this.splitContainer.SplitterDistance = 276;
            this.splitContainer.SplitterWidth = 5;
            this.splitContainer.TabIndex = 1;
            // 
            // treeView
            // 
            this.treeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView.FullRowSelect = true;
            this.treeView.HideSelection = false;
            this.treeView.Location = new System.Drawing.Point(0, 0);
            this.treeView.Margin = new System.Windows.Forms.Padding(4);
            this.treeView.Name = "treeView";
            treeNode15.Name = "NodeToolGeneral";
            treeNode15.Text = "General";
            treeNode16.Name = "NodeTool";
            treeNode16.Text = "Tool";
            treeNode17.Name = "NodeCommGeneral";
            treeNode17.Text = "General";
            treeNode18.Name = "NodeCommSerial";
            treeNode18.Text = "Serial Port";
            treeNode19.Name = "nodeCommCanHardware";
            treeNode19.Text = "Can Hardware";
            treeNode20.Name = "nodeFilter";
            treeNode20.Text = "TX/RX Filter";
            treeNode21.Name = "NodeComm";
            treeNode21.Text = "Communication";
            this.treeView.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode16,
            treeNode21});
            this.treeView.ShowLines = false;
            this.treeView.Size = new System.Drawing.Size(276, 429);
            this.treeView.TabIndex = 0;
            this.treeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView_AfterSelect);
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPageProp1);
            this.tabControl.Controls.Add(this.tabPageProp2);
            this.tabControl.Controls.Add(this.tabSerialPort);
            this.tabControl.Controls.Add(this.tabCanHardware);
            this.tabControl.Controls.Add(this.tabFilterPage);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Margin = new System.Windows.Forms.Padding(4);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(551, 429);
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
            this.tabPageProp1.Location = new System.Drawing.Point(4, 25);
            this.tabPageProp1.Margin = new System.Windows.Forms.Padding(4);
            this.tabPageProp1.Name = "tabPageProp1";
            this.tabPageProp1.Padding = new System.Windows.Forms.Padding(4);
            this.tabPageProp1.Size = new System.Drawing.Size(543, 400);
            this.tabPageProp1.TabIndex = 0;
            this.tabPageProp1.Text = "Config group 1";
            this.tabPageProp1.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(244, 140);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(35, 16);
            this.label11.TabIndex = 13;
            this.label11.Text = "(MB)";
            // 
            // numRollingAfter
            // 
            this.numRollingAfter.Location = new System.Drawing.Point(103, 132);
            this.numRollingAfter.Margin = new System.Windows.Forms.Padding(4);
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
            this.numRollingAfter.Size = new System.Drawing.Size(133, 22);
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
            this.label12.Location = new System.Drawing.Point(8, 134);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(78, 16);
            this.label12.TabIndex = 11;
            this.label12.Text = "Rolling after";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(244, 75);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(70, 16);
            this.label10.TabIndex = 10;
            this.label10.Text = "(Log entry)";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(244, 108);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(47, 16);
            this.label9.TabIndex = 9;
            this.label9.Text = "(Lines)";
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(103, 206);
            this.btnBrowse.Margin = new System.Windows.Forms.Padding(4);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(100, 28);
            this.btnBrowse.TabIndex = 8;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // txtFilePath
            // 
            this.txtFilePath.Location = new System.Drawing.Point(103, 165);
            this.txtFilePath.Margin = new System.Windows.Forms.Padding(4);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.Size = new System.Drawing.Size(272, 22);
            this.txtFilePath.TabIndex = 7;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(8, 169);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(59, 16);
            this.label8.TabIndex = 6;
            this.label8.Text = "File Path";
            // 
            // numFlushToFile
            // 
            this.numFlushToFile.Location = new System.Drawing.Point(103, 100);
            this.numFlushToFile.Margin = new System.Windows.Forms.Padding(4);
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
            this.numFlushToFile.Size = new System.Drawing.Size(133, 22);
            this.numFlushToFile.TabIndex = 5;
            this.numFlushToFile.Value = new decimal(new int[] {
            200,
            0,
            0,
            0});
            // 
            // numFlushToUI
            // 
            this.numFlushToUI.Location = new System.Drawing.Point(103, 66);
            this.numFlushToUI.Margin = new System.Windows.Forms.Padding(4);
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
            this.numFlushToUI.Size = new System.Drawing.Size(133, 22);
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
            this.label7.Location = new System.Drawing.Point(8, 102);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(73, 16);
            this.label7.TabIndex = 3;
            this.label7.Text = "Flush to file";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 69);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(69, 16);
            this.label6.TabIndex = 2;
            this.label6.Text = "Flush to UI";
            // 
            // textBoxUserName
            // 
            this.textBoxUserName.Location = new System.Drawing.Point(103, 23);
            this.textBoxUserName.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxUserName.Name = "textBoxUserName";
            this.textBoxUserName.Size = new System.Drawing.Size(132, 22);
            this.textBoxUserName.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 27);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "User Name";
            // 
            // tabPageProp2
            // 
            this.tabPageProp2.Controls.Add(this.txtPaddingByte);
            this.tabPageProp2.Controls.Add(this.lblPaddingByte);
            this.tabPageProp2.Controls.Add(this.txtStMin);
            this.tabPageProp2.Controls.Add(this.lblStMin);
            this.tabPageProp2.Controls.Add(this.txtBlockSize);
            this.tabPageProp2.Controls.Add(this.lblBlockSize);
            this.tabPageProp2.Controls.Add(this.txtReceiveAdress);
            this.tabPageProp2.Controls.Add(this.lblReceiveAdress);
            this.tabPageProp2.Controls.Add(this.txtTransmitAdress);
            this.tabPageProp2.Controls.Add(this.lblTransmitAdress);
            this.tabPageProp2.Location = new System.Drawing.Point(4, 25);
            this.tabPageProp2.Margin = new System.Windows.Forms.Padding(4);
            this.tabPageProp2.Name = "tabPageProp2";
            this.tabPageProp2.Padding = new System.Windows.Forms.Padding(4);
            this.tabPageProp2.Size = new System.Drawing.Size(543, 400);
            this.tabPageProp2.TabIndex = 1;
            this.tabPageProp2.Text = "General";
            this.tabPageProp2.UseVisualStyleBackColor = true;
            // 
            // txtPaddingByte
            // 
            this.txtPaddingByte.Location = new System.Drawing.Point(125, 150);
            this.txtPaddingByte.Margin = new System.Windows.Forms.Padding(4);
            this.txtPaddingByte.Name = "txtPaddingByte";
            this.txtPaddingByte.Size = new System.Drawing.Size(132, 22);
            this.txtPaddingByte.TabIndex = 5;
            // 
            // lblPaddingByte
            // 
            this.lblPaddingByte.AutoSize = true;
            this.lblPaddingByte.Location = new System.Drawing.Point(8, 154);
            this.lblPaddingByte.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPaddingByte.Name = "lblPaddingByte";
            this.lblPaddingByte.Size = new System.Drawing.Size(88, 16);
            this.lblPaddingByte.TabIndex = 4;
            this.lblPaddingByte.Text = "Padding Byte";
            // 
            // txtStMin
            // 
            this.txtStMin.Location = new System.Drawing.Point(125, 118);
            this.txtStMin.Margin = new System.Windows.Forms.Padding(4);
            this.txtStMin.Name = "txtStMin";
            this.txtStMin.Size = new System.Drawing.Size(132, 22);
            this.txtStMin.TabIndex = 5;
            // 
            // lblStMin
            // 
            this.lblStMin.AutoSize = true;
            this.lblStMin.Location = new System.Drawing.Point(8, 122);
            this.lblStMin.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStMin.Name = "lblStMin";
            this.lblStMin.Size = new System.Drawing.Size(40, 16);
            this.lblStMin.TabIndex = 4;
            this.lblStMin.Text = "StMin";
            // 
            // txtBlockSize
            // 
            this.txtBlockSize.Location = new System.Drawing.Point(125, 86);
            this.txtBlockSize.Margin = new System.Windows.Forms.Padding(4);
            this.txtBlockSize.Name = "txtBlockSize";
            this.txtBlockSize.Size = new System.Drawing.Size(132, 22);
            this.txtBlockSize.TabIndex = 5;
            // 
            // lblBlockSize
            // 
            this.lblBlockSize.AutoSize = true;
            this.lblBlockSize.Location = new System.Drawing.Point(8, 90);
            this.lblBlockSize.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBlockSize.Name = "lblBlockSize";
            this.lblBlockSize.Size = new System.Drawing.Size(70, 16);
            this.lblBlockSize.TabIndex = 4;
            this.lblBlockSize.Text = "Block Size";
            // 
            // txtReceiveAdress
            // 
            this.txtReceiveAdress.Location = new System.Drawing.Point(125, 54);
            this.txtReceiveAdress.Margin = new System.Windows.Forms.Padding(4);
            this.txtReceiveAdress.Name = "txtReceiveAdress";
            this.txtReceiveAdress.Size = new System.Drawing.Size(132, 22);
            this.txtReceiveAdress.TabIndex = 3;
            // 
            // lblReceiveAdress
            // 
            this.lblReceiveAdress.AutoSize = true;
            this.lblReceiveAdress.Location = new System.Drawing.Point(8, 58);
            this.lblReceiveAdress.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblReceiveAdress.Name = "lblReceiveAdress";
            this.lblReceiveAdress.Size = new System.Drawing.Size(104, 16);
            this.lblReceiveAdress.TabIndex = 2;
            this.lblReceiveAdress.Text = "Receive Adress";
            // 
            // txtTransmitAdress
            // 
            this.txtTransmitAdress.Location = new System.Drawing.Point(125, 22);
            this.txtTransmitAdress.Margin = new System.Windows.Forms.Padding(4);
            this.txtTransmitAdress.Name = "txtTransmitAdress";
            this.txtTransmitAdress.Size = new System.Drawing.Size(132, 22);
            this.txtTransmitAdress.TabIndex = 3;
            // 
            // lblTransmitAdress
            // 
            this.lblTransmitAdress.AutoSize = true;
            this.lblTransmitAdress.Location = new System.Drawing.Point(8, 26);
            this.lblTransmitAdress.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTransmitAdress.Name = "lblTransmitAdress";
            this.lblTransmitAdress.Size = new System.Drawing.Size(105, 16);
            this.lblTransmitAdress.TabIndex = 2;
            this.lblTransmitAdress.Text = "Transmit Adress";
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
            this.tabSerialPort.Location = new System.Drawing.Point(4, 25);
            this.tabSerialPort.Margin = new System.Windows.Forms.Padding(4);
            this.tabSerialPort.Name = "tabSerialPort";
            this.tabSerialPort.Padding = new System.Windows.Forms.Padding(4);
            this.tabSerialPort.Size = new System.Drawing.Size(543, 400);
            this.tabSerialPort.TabIndex = 2;
            this.tabSerialPort.Text = "Serial Port";
            this.tabSerialPort.UseVisualStyleBackColor = true;
            // 
            // lblSerialPortType
            // 
            this.lblSerialPortType.AutoSize = true;
            this.lblSerialPortType.Location = new System.Drawing.Point(8, 18);
            this.lblSerialPortType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSerialPortType.Name = "lblSerialPortType";
            this.lblSerialPortType.Size = new System.Drawing.Size(104, 16);
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
            this.cmbSerialPortType.Location = new System.Drawing.Point(125, 15);
            this.cmbSerialPortType.Margin = new System.Windows.Forms.Padding(4);
            this.cmbSerialPortType.Name = "cmbSerialPortType";
            this.cmbSerialPortType.Size = new System.Drawing.Size(132, 24);
            this.cmbSerialPortType.TabIndex = 17;
            // 
            // numWriteTimeout
            // 
            this.numWriteTimeout.Location = new System.Drawing.Point(125, 242);
            this.numWriteTimeout.Margin = new System.Windows.Forms.Padding(4);
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
            this.numWriteTimeout.Size = new System.Drawing.Size(133, 22);
            this.numWriteTimeout.TabIndex = 16;
            // 
            // lblWriteTimeout
            // 
            this.lblWriteTimeout.AutoSize = true;
            this.lblWriteTimeout.Location = new System.Drawing.Point(8, 249);
            this.lblWriteTimeout.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblWriteTimeout.Name = "lblWriteTimeout";
            this.lblWriteTimeout.Size = new System.Drawing.Size(90, 16);
            this.lblWriteTimeout.TabIndex = 15;
            this.lblWriteTimeout.Text = "Write Timeout";
            // 
            // numReadTimeout
            // 
            this.numReadTimeout.Location = new System.Drawing.Point(125, 210);
            this.numReadTimeout.Margin = new System.Windows.Forms.Padding(4);
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
            this.numReadTimeout.Size = new System.Drawing.Size(133, 22);
            this.numReadTimeout.TabIndex = 14;
            // 
            // lblReadTimeout
            // 
            this.lblReadTimeout.AutoSize = true;
            this.lblReadTimeout.Location = new System.Drawing.Point(8, 217);
            this.lblReadTimeout.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblReadTimeout.Name = "lblReadTimeout";
            this.lblReadTimeout.Size = new System.Drawing.Size(93, 16);
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
            this.cmbStopBits.Location = new System.Drawing.Point(125, 175);
            this.cmbStopBits.Margin = new System.Windows.Forms.Padding(4);
            this.cmbStopBits.Name = "cmbStopBits";
            this.cmbStopBits.Size = new System.Drawing.Size(132, 24);
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
            this.cmbParity.Location = new System.Drawing.Point(125, 143);
            this.cmbParity.Margin = new System.Windows.Forms.Padding(4);
            this.cmbParity.Name = "cmbParity";
            this.cmbParity.Size = new System.Drawing.Size(132, 24);
            this.cmbParity.TabIndex = 11;
            // 
            // numDataBits
            // 
            this.numDataBits.Location = new System.Drawing.Point(125, 112);
            this.numDataBits.Margin = new System.Windows.Forms.Padding(4);
            this.numDataBits.Name = "numDataBits";
            this.numDataBits.Size = new System.Drawing.Size(133, 22);
            this.numDataBits.TabIndex = 10;
            // 
            // numBaudRate
            // 
            this.numBaudRate.Location = new System.Drawing.Point(125, 80);
            this.numBaudRate.Margin = new System.Windows.Forms.Padding(4);
            this.numBaudRate.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.numBaudRate.Name = "numBaudRate";
            this.numBaudRate.Size = new System.Drawing.Size(133, 22);
            this.numBaudRate.TabIndex = 9;
            // 
            // lblStopBits
            // 
            this.lblStopBits.AutoSize = true;
            this.lblStopBits.Location = new System.Drawing.Point(8, 185);
            this.lblStopBits.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStopBits.Name = "lblStopBits";
            this.lblStopBits.Size = new System.Drawing.Size(60, 16);
            this.lblStopBits.TabIndex = 8;
            this.lblStopBits.Text = "Stop Bits";
            // 
            // lblParity
            // 
            this.lblParity.AutoSize = true;
            this.lblParity.Location = new System.Drawing.Point(8, 153);
            this.lblParity.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblParity.Name = "lblParity";
            this.lblParity.Size = new System.Drawing.Size(41, 16);
            this.lblParity.TabIndex = 6;
            this.lblParity.Text = "Parity";
            // 
            // lblDataBits
            // 
            this.lblDataBits.AutoSize = true;
            this.lblDataBits.Location = new System.Drawing.Point(8, 121);
            this.lblDataBits.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDataBits.Name = "lblDataBits";
            this.lblDataBits.Size = new System.Drawing.Size(61, 16);
            this.lblDataBits.TabIndex = 4;
            this.lblDataBits.Text = "Data Bits";
            // 
            // lblBaudRate
            // 
            this.lblBaudRate.AutoSize = true;
            this.lblBaudRate.Location = new System.Drawing.Point(8, 89);
            this.lblBaudRate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBaudRate.Name = "lblBaudRate";
            this.lblBaudRate.Size = new System.Drawing.Size(71, 16);
            this.lblBaudRate.TabIndex = 2;
            this.lblBaudRate.Text = "Baud Rate";
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(125, 48);
            this.txtPort.Margin = new System.Windows.Forms.Padding(4);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(132, 22);
            this.txtPort.TabIndex = 1;
            // 
            // lblPort
            // 
            this.lblPort.AutoSize = true;
            this.lblPort.Location = new System.Drawing.Point(8, 57);
            this.lblPort.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(31, 16);
            this.lblPort.TabIndex = 0;
            this.lblPort.Text = "Port";
            // 
            // tabCanHardware
            // 
            this.tabCanHardware.Controls.Add(this.flowLayoutPanel1);
            this.tabCanHardware.Controls.Add(this.label3);
            this.tabCanHardware.Controls.Add(this.tabCanHardware_cmbDevice);
            this.tabCanHardware.Location = new System.Drawing.Point(4, 25);
            this.tabCanHardware.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabCanHardware.Name = "tabCanHardware";
            this.tabCanHardware.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabCanHardware.Size = new System.Drawing.Size(543, 400);
            this.tabCanHardware.TabIndex = 3;
            this.tabCanHardware.Text = "Can Hardware";
            this.tabCanHardware.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.tabCanHardware_grpIntrepid);
            this.flowLayoutPanel1.Controls.Add(this.tabCanHardware_grpKvaser);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(4, 66);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(535, 325);
            this.flowLayoutPanel1.TabIndex = 2;
            // 
            // tabCanHardware_grpIntrepid
            // 
            this.tabCanHardware_grpIntrepid.Controls.Add(this.label5);
            this.tabCanHardware_grpIntrepid.Controls.Add(this.tabCanHardware_cmbNetworkId);
            this.tabCanHardware_grpIntrepid.Controls.Add(this.label4);
            this.tabCanHardware_grpIntrepid.Controls.Add(this.tabCanHardware_cmbBitRate);
            this.tabCanHardware_grpIntrepid.Location = new System.Drawing.Point(3, 2);
            this.tabCanHardware_grpIntrepid.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabCanHardware_grpIntrepid.Name = "tabCanHardware_grpIntrepid";
            this.tabCanHardware_grpIntrepid.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabCanHardware_grpIntrepid.Size = new System.Drawing.Size(529, 100);
            this.tabCanHardware_grpIntrepid.TabIndex = 0;
            this.tabCanHardware_grpIntrepid.TabStop = false;
            this.tabCanHardware_grpIntrepid.Text = "Intrepid";
            this.tabCanHardware_grpIntrepid.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 54);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 16);
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
            this.tabCanHardware_cmbNetworkId.Location = new System.Drawing.Point(117, 50);
            this.tabCanHardware_cmbNetworkId.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabCanHardware_cmbNetworkId.Name = "tabCanHardware_cmbNetworkId";
            this.tabCanHardware_cmbNetworkId.Size = new System.Drawing.Size(203, 24);
            this.tabCanHardware_cmbNetworkId.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 16);
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
            this.tabCanHardware_cmbBitRate.Location = new System.Drawing.Point(117, 21);
            this.tabCanHardware_cmbBitRate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabCanHardware_cmbBitRate.Name = "tabCanHardware_cmbBitRate";
            this.tabCanHardware_cmbBitRate.Size = new System.Drawing.Size(203, 24);
            this.tabCanHardware_cmbBitRate.TabIndex = 1;
            // 
            // tabCanHardware_grpKvaser
            // 
            this.tabCanHardware_grpKvaser.Controls.Add(this.label14);
            this.tabCanHardware_grpKvaser.Controls.Add(this.tabCanHardware_cmbKvaserBitRate);
            this.tabCanHardware_grpKvaser.Location = new System.Drawing.Point(3, 106);
            this.tabCanHardware_grpKvaser.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabCanHardware_grpKvaser.Name = "tabCanHardware_grpKvaser";
            this.tabCanHardware_grpKvaser.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabCanHardware_grpKvaser.Size = new System.Drawing.Size(529, 63);
            this.tabCanHardware_grpKvaser.TabIndex = 1;
            this.tabCanHardware_grpKvaser.TabStop = false;
            this.tabCanHardware_grpKvaser.Text = "Kvaser";
            this.tabCanHardware_grpKvaser.Visible = false;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(15, 25);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(54, 16);
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
            this.tabCanHardware_cmbKvaserBitRate.Location = new System.Drawing.Point(117, 21);
            this.tabCanHardware_cmbKvaserBitRate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabCanHardware_cmbKvaserBitRate.Name = "tabCanHardware_cmbKvaserBitRate";
            this.tabCanHardware_cmbKvaserBitRate.Size = new System.Drawing.Size(203, 24);
            this.tabCanHardware_cmbKvaserBitRate.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(32, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 16);
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
            this.tabCanHardware_cmbDevice.Location = new System.Drawing.Point(124, 18);
            this.tabCanHardware_cmbDevice.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabCanHardware_cmbDevice.Name = "tabCanHardware_cmbDevice";
            this.tabCanHardware_cmbDevice.Size = new System.Drawing.Size(121, 24);
            this.tabCanHardware_cmbDevice.TabIndex = 0;
            this.tabCanHardware_cmbDevice.SelectedIndexChanged += new System.EventHandler(this.tabCanHardware_cmbDevice_SelectedIndexChanged);
            // 
            // tabFilterPage
            // 
            this.tabFilterPage.Controls.Add(this.btnAddFilter);
            this.tabFilterPage.Controls.Add(this.tbFilter);
            this.tabFilterPage.Controls.Add(this.btnClearFilter);
            this.tabFilterPage.Controls.Add(this.btnDeleteFilter);
            this.tabFilterPage.Controls.Add(this.lbFilterPage);
            this.tabFilterPage.Location = new System.Drawing.Point(4, 25);
            this.tabFilterPage.Name = "tabFilterPage";
            this.tabFilterPage.Padding = new System.Windows.Forms.Padding(3);
            this.tabFilterPage.Size = new System.Drawing.Size(543, 400);
            this.tabFilterPage.TabIndex = 4;
            this.tabFilterPage.Text = "TX/RX Filter";
            this.tabFilterPage.UseVisualStyleBackColor = true;
            // 
            // btnAddFilter
            // 
            this.btnAddFilter.Location = new System.Drawing.Point(234, 9);
            this.btnAddFilter.Name = "btnAddFilter";
            this.btnAddFilter.Size = new System.Drawing.Size(45, 23);
            this.btnAddFilter.TabIndex = 5;
            this.btnAddFilter.Text = "Add";
            this.btnAddFilter.UseVisualStyleBackColor = true;
            this.btnAddFilter.Click += new System.EventHandler(this.btnAddFilter_Click);
            // 
            // tbFilter
            // 
            this.tbFilter.Location = new System.Drawing.Point(182, 8);
            this.tbFilter.Name = "tbFilter";
            this.tbFilter.Size = new System.Drawing.Size(41, 22);
            this.tbFilter.TabIndex = 4;
            this.tbFilter.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbFilter_KeyPress);
            // 
            // btnClearFilter
            // 
            this.btnClearFilter.Location = new System.Drawing.Point(6, 288);
            this.btnClearFilter.Name = "btnClearFilter";
            this.btnClearFilter.Size = new System.Drawing.Size(75, 23);
            this.btnClearFilter.TabIndex = 2;
            this.btnClearFilter.Text = "Clear";
            this.btnClearFilter.UseVisualStyleBackColor = true;
            this.btnClearFilter.Click += new System.EventHandler(this.btnClearFilter_Click);
            // 
            // btnDeleteFilter
            // 
            this.btnDeleteFilter.Location = new System.Drawing.Point(99, 288);
            this.btnDeleteFilter.Name = "btnDeleteFilter";
            this.btnDeleteFilter.Size = new System.Drawing.Size(75, 23);
            this.btnDeleteFilter.TabIndex = 1;
            this.btnDeleteFilter.Text = "Delete";
            this.btnDeleteFilter.UseVisualStyleBackColor = true;
            this.btnDeleteFilter.Click += new System.EventHandler(this.btnDeleteFilter_Click);
            // 
            // lbFilterPage
            // 
            this.lbFilterPage.FormattingEnabled = true;
            this.lbFilterPage.ItemHeight = 16;
            this.lbFilterPage.Location = new System.Drawing.Point(6, 6);
            this.lbFilterPage.Name = "lbFilterPage";
            this.lbFilterPage.Size = new System.Drawing.Size(168, 276);
            this.lbFilterPage.TabIndex = 0;
            this.lbFilterPage.SelectedIndexChanged += new System.EventHandler(this.lbFilterPage_SelectedIndexChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonOk);
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 453);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(832, 44);
            this.panel1.TabIndex = 2;
            // 
            // buttonOk
            // 
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Location = new System.Drawing.Point(612, 9);
            this.buttonOk.Margin = new System.Windows.Forms.Padding(4);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(100, 28);
            this.buttonOk.TabIndex = 1;
            this.buttonOk.Text = "OK";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(720, 9);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(4);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(100, 28);
            this.buttonCancel.TabIndex = 0;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // FormOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(832, 497);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.panel1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
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
            this.tabFilterPage.ResumeLayout(false);
            this.tabFilterPage.PerformLayout();
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
        private System.Windows.Forms.TextBox txtBlockSize;
        private System.Windows.Forms.Label lblBlockSize;
        private System.Windows.Forms.TextBox txtReceiveAdress;
        private System.Windows.Forms.Label lblReceiveAdress;
        private System.Windows.Forms.TextBox txtTransmitAdress;
        private System.Windows.Forms.Label lblTransmitAdress;
        private System.Windows.Forms.TextBox txtPaddingByte;
        private System.Windows.Forms.Label lblPaddingByte;
        private System.Windows.Forms.TextBox txtStMin;
        private System.Windows.Forms.Label lblStMin;
        private System.Windows.Forms.TabPage tabFilterPage;
        private System.Windows.Forms.Button btnClearFilter;
        private System.Windows.Forms.Button btnDeleteFilter;
        private System.Windows.Forms.ListBox lbFilterPage;
        private System.Windows.Forms.TextBox tbFilter;
        private System.Windows.Forms.Button btnAddFilter;
    }
}