namespace AutosarBCM.Common
{
    partial class FormHardwareList
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
            this.cmbDevices = new System.Windows.Forms.ComboBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.gbHardware = new System.Windows.Forms.GroupBox();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.grpIntrepidCanProperties = new System.Windows.Forms.GroupBox();
            this.chkIntrepidMultiChanel = new System.Windows.Forms.CheckBox();
            this.grpIntrepidCanProperties_cmbNetworkId = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.grpIntrepidCanProperties_cmbBitRate = new System.Windows.Forms.ComboBox();
            this.grpIntrepidCanMultiChannel = new System.Windows.Forms.GroupBox();
            this.txtMultiChannelSelectedName = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtTransmitAdress = new System.Windows.Forms.TextBox();
            this.chkMultiChannelList = new System.Windows.Forms.CheckedListBox();
            this.grpIntrepidCanMultiChannel_cmbBitRate = new System.Windows.Forms.ComboBox();
            this.grpSerialProperties = new System.Windows.Forms.GroupBox();
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
            this.pnlProperties = new System.Windows.Forms.FlowLayoutPanel();
            this.grpVectorCanProperties = new System.Windows.Forms.GroupBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.grpKvaserCanProperties = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.grpKvaserCanProperties_cmbBitRate = new System.Windows.Forms.ComboBox();
            this.pnlButtonGroup = new System.Windows.Forms.Panel();
            this.gbHardware.SuspendLayout();
            this.grpIntrepidCanProperties.SuspendLayout();
            this.grpIntrepidCanMultiChannel.SuspendLayout();
            this.grpSerialProperties.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numWriteTimeout)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numReadTimeout)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDataBits)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numBaudRate)).BeginInit();
            this.pnlProperties.SuspendLayout();
            this.grpVectorCanProperties.SuspendLayout();
            this.grpKvaserCanProperties.SuspendLayout();
            this.pnlButtonGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbDevices
            // 
            this.cmbDevices.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDevices.FormattingEnabled = true;
            this.cmbDevices.Location = new System.Drawing.Point(87, 6);
            this.cmbDevices.Name = "cmbDevices";
            this.cmbDevices.Size = new System.Drawing.Size(204, 21);
            this.cmbDevices.TabIndex = 0;
            this.cmbDevices.SelectedIndexChanged += new System.EventHandler(this.cmbDevices_SelectedIndexChanged);
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(213, 3);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 23);
            this.btnConnect.TabIndex = 1;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Name";
            // 
            // gbHardware
            // 
            this.gbHardware.Controls.Add(this.txtDescription);
            this.gbHardware.Controls.Add(this.txtName);
            this.gbHardware.Controls.Add(this.label3);
            this.gbHardware.Controls.Add(this.label2);
            this.gbHardware.Location = new System.Drawing.Point(15, 33);
            this.gbHardware.Name = "gbHardware";
            this.gbHardware.Size = new System.Drawing.Size(291, 72);
            this.gbHardware.TabIndex = 4;
            this.gbHardware.TabStop = false;
            this.gbHardware.Text = "Hardware";
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(72, 39);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.ReadOnly = true;
            this.txtDescription.Size = new System.Drawing.Size(204, 20);
            this.txtDescription.TabIndex = 4;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(72, 13);
            this.txtName.Name = "txtName";
            this.txtName.ReadOnly = true;
            this.txtName.Size = new System.Drawing.Size(204, 20);
            this.txtName.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Description";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Name";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 137);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Bit Rate";
            this.label4.Visible = false;
            // 
            // grpIntrepidCanProperties
            // 
            this.grpIntrepidCanProperties.Controls.Add(this.chkIntrepidMultiChanel);
            this.grpIntrepidCanProperties.Controls.Add(this.grpIntrepidCanProperties_cmbNetworkId);
            this.grpIntrepidCanProperties.Controls.Add(this.label8);
            this.grpIntrepidCanProperties.Controls.Add(this.label9);
            this.grpIntrepidCanProperties.Controls.Add(this.label10);
            this.grpIntrepidCanProperties.Controls.Add(this.grpIntrepidCanProperties_cmbBitRate);
            this.grpIntrepidCanProperties.Location = new System.Drawing.Point(3, 3);
            this.grpIntrepidCanProperties.Name = "grpIntrepidCanProperties";
            this.grpIntrepidCanProperties.Size = new System.Drawing.Size(291, 102);
            this.grpIntrepidCanProperties.TabIndex = 5;
            this.grpIntrepidCanProperties.TabStop = false;
            this.grpIntrepidCanProperties.Text = "Intrepid Can Device Properties";
            // 
            // chkIntrepidMultiChanel
            // 
            this.chkIntrepidMultiChanel.AutoSize = true;
            this.chkIntrepidMultiChanel.Location = new System.Drawing.Point(69, 75);
            this.chkIntrepidMultiChanel.Name = "chkIntrepidMultiChanel";
            this.chkIntrepidMultiChanel.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chkIntrepidMultiChanel.Size = new System.Drawing.Size(15, 14);
            this.chkIntrepidMultiChanel.TabIndex = 8;
            this.chkIntrepidMultiChanel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkIntrepidMultiChanel.UseVisualStyleBackColor = true;
            this.chkIntrepidMultiChanel.CheckedChanged += new System.EventHandler(this.chkIntrepidMultiChanel_CheckedChanged);
            // 
            // grpIntrepidCanProperties_cmbNetworkId
            // 
            this.grpIntrepidCanProperties_cmbNetworkId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.grpIntrepidCanProperties_cmbNetworkId.FormattingEnabled = true;
            this.grpIntrepidCanProperties_cmbNetworkId.Items.AddRange(new object[] {
            "NETID_HSCAN",
            "NETID_MSCAN",
            "NETID_SWCAN",
            "NETID_HSCAN2",
            "NETID_HSCAN3",
            "NETID_HSCAN4",
            "NETID_LSFTCAN",
            "NETID_LIN",
            "NETID_ISO2",
            "NETID_FIRE_LIN2",
            "NETID_FIRE_LIN3",
            "NETID_FIRE_LIN4",
            "NETID_FIRE_CGI"});
            this.grpIntrepidCanProperties_cmbNetworkId.Location = new System.Drawing.Point(69, 46);
            this.grpIntrepidCanProperties_cmbNetworkId.Margin = new System.Windows.Forms.Padding(2);
            this.grpIntrepidCanProperties_cmbNetworkId.Name = "grpIntrepidCanProperties_cmbNetworkId";
            this.grpIntrepidCanProperties_cmbNetworkId.Size = new System.Drawing.Size(153, 21);
            this.grpIntrepidCanProperties_cmbNetworkId.TabIndex = 7;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 112);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "Network Id";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 48);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(59, 13);
            this.label8.TabIndex = 6;
            this.label8.Text = "Network Id";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 22);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(45, 13);
            this.label9.TabIndex = 2;
            this.label9.Text = "Bit Rate";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(3, 75);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(65, 13);
            this.label10.TabIndex = 6;
            this.label10.Text = "Multi Chanel";
            // 
            // grpIntrepidCanProperties_cmbBitRate
            // 
            this.grpIntrepidCanProperties_cmbBitRate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.grpIntrepidCanProperties_cmbBitRate.FormattingEnabled = true;
            this.grpIntrepidCanProperties_cmbBitRate.Items.AddRange(new object[] {
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
            this.grpIntrepidCanProperties_cmbBitRate.Location = new System.Drawing.Point(69, 20);
            this.grpIntrepidCanProperties_cmbBitRate.Margin = new System.Windows.Forms.Padding(2);
            this.grpIntrepidCanProperties_cmbBitRate.Name = "grpIntrepidCanProperties_cmbBitRate";
            this.grpIntrepidCanProperties_cmbBitRate.Size = new System.Drawing.Size(153, 21);
            this.grpIntrepidCanProperties_cmbBitRate.TabIndex = 3;
            // 
            // grpIntrepidCanMultiChannel
            // 
            this.grpIntrepidCanMultiChannel.AutoSize = true;
            this.grpIntrepidCanMultiChannel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.grpIntrepidCanMultiChannel.Controls.Add(this.txtMultiChannelSelectedName);
            this.grpIntrepidCanMultiChannel.Controls.Add(this.label11);
            this.grpIntrepidCanMultiChannel.Controls.Add(this.label7);
            this.grpIntrepidCanMultiChannel.Controls.Add(this.txtTransmitAdress);
            this.grpIntrepidCanMultiChannel.Controls.Add(this.chkMultiChannelList);
            this.grpIntrepidCanMultiChannel.Controls.Add(this.grpIntrepidCanMultiChannel_cmbBitRate);
            this.grpIntrepidCanMultiChannel.Controls.Add(this.label4);
            this.grpIntrepidCanMultiChannel.Location = new System.Drawing.Point(3, 111);
            this.grpIntrepidCanMultiChannel.Name = "grpIntrepidCanMultiChannel";
            this.grpIntrepidCanMultiChannel.Size = new System.Drawing.Size(279, 199);
            this.grpIntrepidCanMultiChannel.TabIndex = 5;
            this.grpIntrepidCanMultiChannel.TabStop = false;
            this.grpIntrepidCanMultiChannel.Text = "Intrepid Can Multi Channel Properties";
            this.grpIntrepidCanMultiChannel.Visible = false;
            // 
            // txtMultiChannelSelectedName
            // 
            this.txtMultiChannelSelectedName.Location = new System.Drawing.Point(81, 109);
            this.txtMultiChannelSelectedName.Name = "txtMultiChannelSelectedName";
            this.txtMultiChannelSelectedName.ReadOnly = true;
            this.txtMultiChannelSelectedName.Size = new System.Drawing.Size(99, 20);
            this.txtMultiChannelSelectedName.TabIndex = 13;
            this.txtMultiChannelSelectedName.Visible = false;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 166);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(58, 13);
            this.label11.TabIndex = 11;
            this.label11.Text = "Arb. Id (0x)";
            this.label11.Visible = false;
            // 
            // txtTransmitAdress
            // 
            this.txtTransmitAdress.Location = new System.Drawing.Point(81, 160);
            this.txtTransmitAdress.MaxLength = 3;
            this.txtTransmitAdress.Name = "txtTransmitAdress";
            this.txtTransmitAdress.Size = new System.Drawing.Size(99, 20);
            this.txtTransmitAdress.TabIndex = 9;
            this.txtTransmitAdress.Visible = false;
            this.txtTransmitAdress.TextChanged += new System.EventHandler(this.AddChannelToDictionary);
            this.txtTransmitAdress.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTransmitAdress_KeyPress);
            // 
            // chkMultiChannelList
            // 
            this.chkMultiChannelList.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.chkMultiChannelList.FormattingEnabled = true;
            this.chkMultiChannelList.Location = new System.Drawing.Point(9, 19);
            this.chkMultiChannelList.Name = "chkMultiChannelList";
            this.chkMultiChannelList.Size = new System.Drawing.Size(264, 79);
            this.chkMultiChannelList.TabIndex = 8;
            this.chkMultiChannelList.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.chkMultiChannelList_ItemCheck);
            this.chkMultiChannelList.MouseClick += new System.Windows.Forms.MouseEventHandler(this.chkMultiChannelList_MouseClick);
            // 
            // grpIntrepidCanMultiChannel_cmbBitRate
            // 
            this.grpIntrepidCanMultiChannel_cmbBitRate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.grpIntrepidCanMultiChannel_cmbBitRate.FormattingEnabled = true;
            this.grpIntrepidCanMultiChannel_cmbBitRate.Items.AddRange(new object[] {
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
            this.grpIntrepidCanMultiChannel_cmbBitRate.Location = new System.Drawing.Point(81, 134);
            this.grpIntrepidCanMultiChannel_cmbBitRate.Margin = new System.Windows.Forms.Padding(2);
            this.grpIntrepidCanMultiChannel_cmbBitRate.Name = "grpIntrepidCanMultiChannel_cmbBitRate";
            this.grpIntrepidCanMultiChannel_cmbBitRate.Size = new System.Drawing.Size(99, 21);
            this.grpIntrepidCanMultiChannel_cmbBitRate.TabIndex = 3;
            this.grpIntrepidCanMultiChannel_cmbBitRate.Visible = false;
            this.grpIntrepidCanMultiChannel_cmbBitRate.SelectedIndexChanged += new System.EventHandler(this.AddChannelToDictionary);
            // 
            // grpSerialProperties
            // 
            this.grpSerialProperties.Controls.Add(this.lblSerialPortType);
            this.grpSerialProperties.Controls.Add(this.cmbSerialPortType);
            this.grpSerialProperties.Controls.Add(this.numWriteTimeout);
            this.grpSerialProperties.Controls.Add(this.lblWriteTimeout);
            this.grpSerialProperties.Controls.Add(this.numReadTimeout);
            this.grpSerialProperties.Controls.Add(this.lblReadTimeout);
            this.grpSerialProperties.Controls.Add(this.cmbStopBits);
            this.grpSerialProperties.Controls.Add(this.cmbParity);
            this.grpSerialProperties.Controls.Add(this.numDataBits);
            this.grpSerialProperties.Controls.Add(this.numBaudRate);
            this.grpSerialProperties.Controls.Add(this.lblStopBits);
            this.grpSerialProperties.Controls.Add(this.lblParity);
            this.grpSerialProperties.Controls.Add(this.lblDataBits);
            this.grpSerialProperties.Controls.Add(this.lblBaudRate);
            this.grpSerialProperties.Controls.Add(this.txtPort);
            this.grpSerialProperties.Controls.Add(this.lblPort);
            this.grpSerialProperties.Location = new System.Drawing.Point(3, 429);
            this.grpSerialProperties.Name = "grpSerialProperties";
            this.grpSerialProperties.Size = new System.Drawing.Size(291, 239);
            this.grpSerialProperties.TabIndex = 6;
            this.grpSerialProperties.TabStop = false;
            this.grpSerialProperties.Text = "SerialPort Properties";
            // 
            // lblSerialPortType
            // 
            this.lblSerialPortType.AutoSize = true;
            this.lblSerialPortType.Location = new System.Drawing.Point(5, 22);
            this.lblSerialPortType.Name = "lblSerialPortType";
            this.lblSerialPortType.Size = new System.Drawing.Size(82, 13);
            this.lblSerialPortType.TabIndex = 28;
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
            this.cmbSerialPortType.Location = new System.Drawing.Point(93, 19);
            this.cmbSerialPortType.Name = "cmbSerialPortType";
            this.cmbSerialPortType.Size = new System.Drawing.Size(100, 21);
            this.cmbSerialPortType.TabIndex = 27;
            // 
            // numWriteTimeout
            // 
            this.numWriteTimeout.Location = new System.Drawing.Point(93, 203);
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
            this.numWriteTimeout.TabIndex = 26;
            // 
            // lblWriteTimeout
            // 
            this.lblWriteTimeout.AutoSize = true;
            this.lblWriteTimeout.Location = new System.Drawing.Point(5, 208);
            this.lblWriteTimeout.Name = "lblWriteTimeout";
            this.lblWriteTimeout.Size = new System.Drawing.Size(73, 13);
            this.lblWriteTimeout.TabIndex = 25;
            this.lblWriteTimeout.Text = "Write Timeout";
            // 
            // numReadTimeout
            // 
            this.numReadTimeout.Location = new System.Drawing.Point(93, 177);
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
            this.numReadTimeout.TabIndex = 24;
            // 
            // lblReadTimeout
            // 
            this.lblReadTimeout.AutoSize = true;
            this.lblReadTimeout.Location = new System.Drawing.Point(5, 182);
            this.lblReadTimeout.Name = "lblReadTimeout";
            this.lblReadTimeout.Size = new System.Drawing.Size(74, 13);
            this.lblReadTimeout.TabIndex = 23;
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
            this.cmbStopBits.Location = new System.Drawing.Point(93, 149);
            this.cmbStopBits.Name = "cmbStopBits";
            this.cmbStopBits.Size = new System.Drawing.Size(100, 21);
            this.cmbStopBits.TabIndex = 22;
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
            this.cmbParity.Location = new System.Drawing.Point(93, 123);
            this.cmbParity.Name = "cmbParity";
            this.cmbParity.Size = new System.Drawing.Size(100, 21);
            this.cmbParity.TabIndex = 21;
            // 
            // numDataBits
            // 
            this.numDataBits.Location = new System.Drawing.Point(93, 98);
            this.numDataBits.Name = "numDataBits";
            this.numDataBits.Size = new System.Drawing.Size(100, 20);
            this.numDataBits.TabIndex = 20;
            // 
            // numBaudRate
            // 
            this.numBaudRate.Location = new System.Drawing.Point(93, 72);
            this.numBaudRate.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.numBaudRate.Name = "numBaudRate";
            this.numBaudRate.Size = new System.Drawing.Size(100, 20);
            this.numBaudRate.TabIndex = 19;
            // 
            // lblStopBits
            // 
            this.lblStopBits.AutoSize = true;
            this.lblStopBits.Location = new System.Drawing.Point(5, 157);
            this.lblStopBits.Name = "lblStopBits";
            this.lblStopBits.Size = new System.Drawing.Size(49, 13);
            this.lblStopBits.TabIndex = 18;
            this.lblStopBits.Text = "Stop Bits";
            // 
            // lblParity
            // 
            this.lblParity.AutoSize = true;
            this.lblParity.Location = new System.Drawing.Point(5, 131);
            this.lblParity.Name = "lblParity";
            this.lblParity.Size = new System.Drawing.Size(33, 13);
            this.lblParity.TabIndex = 17;
            this.lblParity.Text = "Parity";
            // 
            // lblDataBits
            // 
            this.lblDataBits.AutoSize = true;
            this.lblDataBits.Location = new System.Drawing.Point(5, 105);
            this.lblDataBits.Name = "lblDataBits";
            this.lblDataBits.Size = new System.Drawing.Size(50, 13);
            this.lblDataBits.TabIndex = 16;
            this.lblDataBits.Text = "Data Bits";
            // 
            // lblBaudRate
            // 
            this.lblBaudRate.AutoSize = true;
            this.lblBaudRate.Location = new System.Drawing.Point(5, 79);
            this.lblBaudRate.Name = "lblBaudRate";
            this.lblBaudRate.Size = new System.Drawing.Size(58, 13);
            this.lblBaudRate.TabIndex = 15;
            this.lblBaudRate.Text = "Baud Rate";
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(93, 46);
            this.txtPort.Name = "txtPort";
            this.txtPort.ReadOnly = true;
            this.txtPort.Size = new System.Drawing.Size(100, 20);
            this.txtPort.TabIndex = 14;
            // 
            // lblPort
            // 
            this.lblPort.AutoSize = true;
            this.lblPort.Location = new System.Drawing.Point(5, 53);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(26, 13);
            this.lblPort.TabIndex = 13;
            this.lblPort.Text = "Port";
            // 
            // pnlProperties
            // 
            this.pnlProperties.AutoSize = true;
            this.pnlProperties.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnlProperties.BackColor = System.Drawing.SystemColors.Control;
            this.pnlProperties.Controls.Add(this.grpIntrepidCanProperties);
            this.pnlProperties.Controls.Add(this.grpIntrepidCanMultiChannel);
            this.pnlProperties.Controls.Add(this.grpVectorCanProperties);
            this.pnlProperties.Controls.Add(this.grpKvaserCanProperties);
            this.pnlProperties.Controls.Add(this.grpSerialProperties);
            this.pnlProperties.Controls.Add(this.pnlButtonGroup);
            this.pnlProperties.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.pnlProperties.Location = new System.Drawing.Point(15, 111);
            this.pnlProperties.Name = "pnlProperties";
            this.pnlProperties.Size = new System.Drawing.Size(297, 707);
            this.pnlProperties.TabIndex = 7;
            // 
            // grpVectorCanProperties
            // 
            this.grpVectorCanProperties.Controls.Add(this.textBox1);
            this.grpVectorCanProperties.Controls.Add(this.label5);
            this.grpVectorCanProperties.Enabled = false;
            this.grpVectorCanProperties.Location = new System.Drawing.Point(3, 316);
            this.grpVectorCanProperties.Name = "grpVectorCanProperties";
            this.grpVectorCanProperties.Size = new System.Drawing.Size(291, 49);
            this.grpVectorCanProperties.TabIndex = 8;
            this.grpVectorCanProperties.TabStop = false;
            this.grpVectorCanProperties.Text = "Vector Can Device Properties";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(72, 19);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(201, 20);
            this.textBox1.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(45, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Bit Rate";
            // 
            // grpKvaserCanProperties
            // 
            this.grpKvaserCanProperties.Controls.Add(this.label6);
            this.grpKvaserCanProperties.Controls.Add(this.grpKvaserCanProperties_cmbBitRate);
            this.grpKvaserCanProperties.Location = new System.Drawing.Point(3, 371);
            this.grpKvaserCanProperties.Name = "grpKvaserCanProperties";
            this.grpKvaserCanProperties.Size = new System.Drawing.Size(291, 52);
            this.grpKvaserCanProperties.TabIndex = 9;
            this.grpKvaserCanProperties.TabStop = false;
            this.grpKvaserCanProperties.Text = "Kvaser Can Device Properties";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(45, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "Bit Rate";
            // 
            // grpKvaserCanProperties_cmbBitRate
            // 
            this.grpKvaserCanProperties_cmbBitRate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.grpKvaserCanProperties_cmbBitRate.FormattingEnabled = true;
            this.grpKvaserCanProperties_cmbBitRate.Items.AddRange(new object[] {
            "50000",
            "62500",
            "100000",
            "125000",
            "250000",
            "500000",
            "1000000"});
            this.grpKvaserCanProperties_cmbBitRate.Location = new System.Drawing.Point(69, 19);
            this.grpKvaserCanProperties_cmbBitRate.Margin = new System.Windows.Forms.Padding(2);
            this.grpKvaserCanProperties_cmbBitRate.Name = "grpKvaserCanProperties_cmbBitRate";
            this.grpKvaserCanProperties_cmbBitRate.Size = new System.Drawing.Size(153, 21);
            this.grpKvaserCanProperties_cmbBitRate.TabIndex = 3;
            // 
            // pnlButtonGroup
            // 
            this.pnlButtonGroup.Controls.Add(this.btnConnect);
            this.pnlButtonGroup.Location = new System.Drawing.Point(3, 674);
            this.pnlButtonGroup.Name = "pnlButtonGroup";
            this.pnlButtonGroup.Size = new System.Drawing.Size(291, 30);
            this.pnlButtonGroup.TabIndex = 7;
            // 
            // FormHardwareList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(319, 823);
            this.Controls.Add(this.pnlProperties);
            this.Controls.Add(this.gbHardware);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbDevices);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FormHardwareList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Hardware List";
            this.gbHardware.ResumeLayout(false);
            this.gbHardware.PerformLayout();
            this.grpIntrepidCanProperties.ResumeLayout(false);
            this.grpIntrepidCanProperties.PerformLayout();
            this.grpIntrepidCanMultiChannel.ResumeLayout(false);
            this.grpIntrepidCanMultiChannel.PerformLayout();
            this.grpSerialProperties.ResumeLayout(false);
            this.grpSerialProperties.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numWriteTimeout)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numReadTimeout)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDataBits)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numBaudRate)).EndInit();
            this.pnlProperties.ResumeLayout(false);
            this.pnlProperties.PerformLayout();
            this.grpVectorCanProperties.ResumeLayout(false);
            this.grpVectorCanProperties.PerformLayout();
            this.grpKvaserCanProperties.ResumeLayout(false);
            this.grpKvaserCanProperties.PerformLayout();
            this.pnlButtonGroup.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbDevices;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox gbHardware;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox grpIntrepidCanProperties;
        private System.Windows.Forms.GroupBox grpIntrepidCanMultiChannel;
        private System.Windows.Forms.GroupBox grpSerialProperties;
        private System.Windows.Forms.ComboBox cmbStopBits;
        private System.Windows.Forms.ComboBox cmbParity;
        private System.Windows.Forms.NumericUpDown numDataBits;
        private System.Windows.Forms.NumericUpDown numBaudRate;
        private System.Windows.Forms.Label lblStopBits;
        private System.Windows.Forms.Label lblParity;
        private System.Windows.Forms.Label lblDataBits;
        private System.Windows.Forms.Label lblBaudRate;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Label lblPort;
        private System.Windows.Forms.FlowLayoutPanel pnlProperties;
        private System.Windows.Forms.Panel pnlButtonGroup;
        private System.Windows.Forms.NumericUpDown numWriteTimeout;
        private System.Windows.Forms.Label lblWriteTimeout;
        private System.Windows.Forms.NumericUpDown numReadTimeout;
        private System.Windows.Forms.Label lblReadTimeout;
        private System.Windows.Forms.GroupBox grpVectorCanProperties;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox grpKvaserCanProperties;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox grpIntrepidCanProperties_cmbBitRate;
        private System.Windows.Forms.Label lblSerialPortType;
        private System.Windows.Forms.ComboBox cmbSerialPortType;
        private System.Windows.Forms.ComboBox grpIntrepidCanProperties_cmbNetworkId;
        private System.Windows.Forms.ComboBox grpKvaserCanProperties_cmbBitRate;
        private System.Windows.Forms.ComboBox grpIntrepidCanMultiChannel_cmbBitRate;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckedListBox chkMultiChannelList;
        private System.Windows.Forms.CheckBox chkIntrepidMultiChanel;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtTransmitAdress;
        private System.Windows.Forms.TextBox txtMultiChannelSelectedName;
    }
}