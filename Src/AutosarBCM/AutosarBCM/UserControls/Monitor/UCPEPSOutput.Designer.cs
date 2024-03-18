namespace AutosarBCM.UserControls.Monitor
{
    partial class UCPEPSOutput
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
            this.lblName = new System.Windows.Forms.Label();
            this.btnRead = new System.Windows.Forms.Button();
            this.btnRSSI = new System.Windows.Forms.Button();
            this.grpRSSI = new System.Windows.Forms.GroupBox();
            this.grpNCK2910 = new System.Windows.Forms.GroupBox();
            this.cmbLevel = new System.Windows.Forms.ComboBox();
            this.cmbPIN = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblD = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.lblC = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lblB = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblA = new System.Windows.Forms.Label();
            this.btnNCK2910 = new System.Windows.Forms.Button();
            this.label26 = new System.Windows.Forms.Label();
            this.lblSQSUM = new System.Windows.Forms.Label();
            this.lblY = new System.Windows.Forms.Label();
            this.lblZ = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblX = new System.Windows.Forms.Label();
            this.txtKeyfobID4 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtKeyfobID1 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtKeyfobID3 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtKeyfobID2 = new System.Windows.Forms.TextBox();
            this.grpDoorCap = new System.Windows.Forms.GroupBox();
            this.lblCapSensor2WupCount = new System.Windows.Forms.Label();
            this.lblCapSensor1WupCount = new System.Windows.Forms.Label();
            this.lblCapSensor1Wup = new System.Windows.Forms.Label();
            this.lblCapSensor2Wup = new System.Windows.Forms.Label();
            this.grpMain = new System.Windows.Forms.GroupBox();
            this.lblBatteryLevelValue = new System.Windows.Forms.Label();
            this.lblBatteryLevelName = new System.Windows.Forms.Label();
            this.lblKeyfobId = new System.Windows.Forms.Label();
            this.lblKeyfobName = new System.Windows.Forms.Label();
            this.lblValue2 = new System.Windows.Forms.Label();
            this.lblButtonIdenValue = new System.Windows.Forms.Label();
            this.lblValue1 = new System.Windows.Forms.Label();
            this.lblButtonIden = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.grpRSSI.SuspendLayout();
            this.grpNCK2910.SuspendLayout();
            this.grpDoorCap.SuspendLayout();
            this.grpMain.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblName
            // 
            this.lblName.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblName.Location = new System.Drawing.Point(17, 9);
            this.lblName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(389, 25);
            this.lblName.TabIndex = 1;
            this.lblName.Text = "label1";
            this.lblName.Click += new System.EventHandler(this.lblName_Click);
            // 
            // btnRead
            // 
            this.btnRead.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnRead.ForeColor = System.Drawing.Color.Black;
            this.btnRead.Location = new System.Drawing.Point(280, 17);
            this.btnRead.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnRead.Name = "btnRead";
            this.btnRead.Size = new System.Drawing.Size(93, 30);
            this.btnRead.TabIndex = 9;
            this.btnRead.Text = "READ";
            this.btnRead.UseVisualStyleBackColor = true;
            this.btnRead.Click += new System.EventHandler(this.btnRead_Click);
            // 
            // btnRSSI
            // 
            this.btnRSSI.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnRSSI.ForeColor = System.Drawing.Color.Black;
            this.btnRSSI.Location = new System.Drawing.Point(241, 33);
            this.btnRSSI.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnRSSI.Name = "btnRSSI";
            this.btnRSSI.Size = new System.Drawing.Size(132, 27);
            this.btnRSSI.TabIndex = 14;
            this.btnRSSI.Text = "Start Measure";
            this.btnRSSI.UseVisualStyleBackColor = true;
            this.btnRSSI.Click += new System.EventHandler(this.btnRSSI_Click);
            // 
            // grpRSSI
            // 
            this.grpRSSI.Controls.Add(this.lblSQSUM);
            this.grpRSSI.Controls.Add(this.btnRSSI);
            this.grpRSSI.Controls.Add(this.lblY);
            this.grpRSSI.Controls.Add(this.lblZ);
            this.grpRSSI.Controls.Add(this.label1);
            this.grpRSSI.Controls.Add(this.label8);
            this.grpRSSI.Controls.Add(this.lblX);
            this.grpRSSI.Controls.Add(this.txtKeyfobID4);
            this.grpRSSI.Controls.Add(this.label5);
            this.grpRSSI.Controls.Add(this.txtKeyfobID1);
            this.grpRSSI.Controls.Add(this.label6);
            this.grpRSSI.Controls.Add(this.txtKeyfobID3);
            this.grpRSSI.Controls.Add(this.label7);
            this.grpRSSI.Controls.Add(this.txtKeyfobID2);
            this.grpRSSI.Location = new System.Drawing.Point(4, 268);
            this.grpRSSI.Margin = new System.Windows.Forms.Padding(4);
            this.grpRSSI.Name = "grpRSSI";
            this.grpRSSI.Padding = new System.Windows.Forms.Padding(4);
            this.grpRSSI.Size = new System.Drawing.Size(391, 124);
            this.grpRSSI.TabIndex = 17;
            this.grpRSSI.TabStop = false;
            // 
            // grpNCK2910
            // 
            this.grpNCK2910.Controls.Add(this.cmbLevel);
            this.grpNCK2910.Controls.Add(this.cmbPIN);
            this.grpNCK2910.Controls.Add(this.label4);
            this.grpNCK2910.Controls.Add(this.label2);
            this.grpNCK2910.Controls.Add(this.lblD);
            this.grpNCK2910.Controls.Add(this.label11);
            this.grpNCK2910.Controls.Add(this.lblC);
            this.grpNCK2910.Controls.Add(this.label9);
            this.grpNCK2910.Controls.Add(this.lblB);
            this.grpNCK2910.Controls.Add(this.label3);
            this.grpNCK2910.Controls.Add(this.lblA);
            this.grpNCK2910.Controls.Add(this.btnNCK2910);
            this.grpNCK2910.Controls.Add(this.label26);
            this.grpNCK2910.Location = new System.Drawing.Point(4, 400);
            this.grpNCK2910.Margin = new System.Windows.Forms.Padding(4);
            this.grpNCK2910.Name = "grpNCK2910";
            this.grpNCK2910.Padding = new System.Windows.Forms.Padding(4);
            this.grpNCK2910.Size = new System.Drawing.Size(391, 124);
            this.grpNCK2910.TabIndex = 35;
            this.grpNCK2910.TabStop = false;
            // 
            // cmbLevel
            // 
            this.cmbLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLevel.FormattingEnabled = true;
            this.cmbLevel.Location = new System.Drawing.Point(154, 22);
            this.cmbLevel.Name = "cmbLevel";
            this.cmbLevel.Size = new System.Drawing.Size(89, 24);
            this.cmbLevel.TabIndex = 34;
            // 
            // cmbPIN
            // 
            this.cmbPIN.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPIN.FormattingEnabled = true;
            this.cmbPIN.Location = new System.Drawing.Point(154, 59);
            this.cmbPIN.Name = "cmbPIN";
            this.cmbPIN.Size = new System.Drawing.Size(171, 24);
            this.cmbPIN.TabIndex = 33;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(96, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 16);
            this.label4.TabIndex = 32;
            this.label4.Text = "LEVEL:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(115, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 16);
            this.label2.TabIndex = 31;
            this.label2.Text = "PIN:";
            // 
            // lblD
            // 
            this.lblD.AutoSize = true;
            this.lblD.Location = new System.Drawing.Point(45, 83);
            this.lblD.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblD.Name = "lblD";
            this.lblD.Size = new System.Drawing.Size(11, 16);
            this.lblD.TabIndex = 29;
            this.lblD.Text = "-";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(18, 83);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(20, 16);
            this.label11.TabIndex = 30;
            this.label11.Text = "D:";
            // 
            // lblC
            // 
            this.lblC.AutoSize = true;
            this.lblC.Location = new System.Drawing.Point(45, 63);
            this.lblC.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblC.Name = "lblC";
            this.lblC.Size = new System.Drawing.Size(11, 16);
            this.lblC.TabIndex = 27;
            this.lblC.Text = "-";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(18, 63);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(19, 16);
            this.label9.TabIndex = 28;
            this.label9.Text = "C:";
            // 
            // lblB
            // 
            this.lblB.AutoSize = true;
            this.lblB.Location = new System.Drawing.Point(45, 44);
            this.lblB.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblB.Name = "lblB";
            this.lblB.Size = new System.Drawing.Size(11, 16);
            this.lblB.TabIndex = 25;
            this.lblB.Text = "-";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 44);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(19, 16);
            this.label3.TabIndex = 26;
            this.label3.Text = "B:";
            // 
            // lblA
            // 
            this.lblA.AutoSize = true;
            this.lblA.Location = new System.Drawing.Point(45, 25);
            this.lblA.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblA.Name = "lblA";
            this.lblA.Size = new System.Drawing.Size(11, 16);
            this.lblA.TabIndex = 18;
            this.lblA.Text = "-";
            // 
            // btnNCK2910
            // 
            this.btnNCK2910.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnNCK2910.ForeColor = System.Drawing.Color.Black;
            this.btnNCK2910.Location = new System.Drawing.Point(280, 17);
            this.btnNCK2910.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnNCK2910.Name = "btnNCK2910";
            this.btnNCK2910.Size = new System.Drawing.Size(93, 30);
            this.btnNCK2910.TabIndex = 9;
            this.btnNCK2910.Text = "READ";
            this.btnNCK2910.UseVisualStyleBackColor = true;
            this.btnNCK2910.Click += new System.EventHandler(this.btnNCK2910_Click);
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(18, 25);
            this.label26.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(19, 16);
            this.label26.TabIndex = 23;
            this.label26.Text = "A:";
            // 
            // lblSQSUM
            // 
            this.lblSQSUM.AutoSize = true;
            this.lblSQSUM.Location = new System.Drawing.Point(227, 103);
            this.lblSQSUM.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSQSUM.Name = "lblSQSUM";
            this.lblSQSUM.Size = new System.Drawing.Size(14, 16);
            this.lblSQSUM.TabIndex = 31;
            this.lblSQSUM.Text = "0";
            // 
            // lblY
            // 
            this.lblY.AutoSize = true;
            this.lblY.Location = new System.Drawing.Point(40, 103);
            this.lblY.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblY.Name = "lblY";
            this.lblY.Size = new System.Drawing.Size(14, 16);
            this.lblY.TabIndex = 30;
            this.lblY.Text = "0";
            // 
            // lblZ
            // 
            this.lblZ.AutoSize = true;
            this.lblZ.Location = new System.Drawing.Point(181, 75);
            this.lblZ.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblZ.Name = "lblZ";
            this.lblZ.Size = new System.Drawing.Size(14, 16);
            this.lblZ.TabIndex = 29;
            this.lblZ.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 16);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 16);
            this.label1.TabIndex = 18;
            this.label1.Text = "Keyfob Id";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(153, 103);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(59, 16);
            this.label8.TabIndex = 27;
            this.label8.Text = "SQSUM:";
            // 
            // lblX
            // 
            this.lblX.AutoSize = true;
            this.lblX.Location = new System.Drawing.Point(40, 75);
            this.lblX.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblX.Name = "lblX";
            this.lblX.Size = new System.Drawing.Size(14, 16);
            this.lblX.TabIndex = 28;
            this.lblX.Text = "0";
            // 
            // txtKeyfobID4
            // 
            this.txtKeyfobID4.Location = new System.Drawing.Point(156, 41);
            this.txtKeyfobID4.Margin = new System.Windows.Forms.Padding(4);
            this.txtKeyfobID4.Name = "txtKeyfobID4";
            this.txtKeyfobID4.Size = new System.Drawing.Size(56, 22);
            this.txtKeyfobID4.TabIndex = 19;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(5, 75);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(21, 16);
            this.label5.TabIndex = 23;
            this.label5.Text = "X: ";
            // 
            // txtKeyfobID1
            // 
            this.txtKeyfobID1.Location = new System.Drawing.Point(8, 41);
            this.txtKeyfobID1.Margin = new System.Windows.Forms.Padding(4);
            this.txtKeyfobID1.Name = "txtKeyfobID1";
            this.txtKeyfobID1.Size = new System.Drawing.Size(40, 22);
            this.txtKeyfobID1.TabIndex = 16;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(5, 103);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(19, 16);
            this.label6.TabIndex = 24;
            this.label6.Text = "Y:";
            // 
            // txtKeyfobID3
            // 
            this.txtKeyfobID3.Location = new System.Drawing.Point(107, 41);
            this.txtKeyfobID3.Margin = new System.Windows.Forms.Padding(4);
            this.txtKeyfobID3.Name = "txtKeyfobID3";
            this.txtKeyfobID3.Size = new System.Drawing.Size(40, 22);
            this.txtKeyfobID3.TabIndex = 18;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(153, 75);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(18, 16);
            this.label7.TabIndex = 26;
            this.label7.Text = "Z:";
            // 
            // txtKeyfobID2
            // 
            this.txtKeyfobID2.Location = new System.Drawing.Point(57, 41);
            this.txtKeyfobID2.Margin = new System.Windows.Forms.Padding(4);
            this.txtKeyfobID2.Name = "txtKeyfobID2";
            this.txtKeyfobID2.Size = new System.Drawing.Size(40, 22);
            this.txtKeyfobID2.TabIndex = 17;
            // 
            // grpDoorCap
            // 
            this.grpDoorCap.Controls.Add(this.lblCapSensor2WupCount);
            this.grpDoorCap.Controls.Add(this.lblCapSensor1WupCount);
            this.grpDoorCap.Controls.Add(this.lblCapSensor1Wup);
            this.grpDoorCap.Controls.Add(this.lblCapSensor2Wup);
            this.grpDoorCap.Location = new System.Drawing.Point(4, 136);
            this.grpDoorCap.Margin = new System.Windows.Forms.Padding(4);
            this.grpDoorCap.Name = "grpDoorCap";
            this.grpDoorCap.Padding = new System.Windows.Forms.Padding(4);
            this.grpDoorCap.Size = new System.Drawing.Size(391, 124);
            this.grpDoorCap.TabIndex = 34;
            this.grpDoorCap.TabStop = false;
            // 
            // lblCapSensor2WupCount
            // 
            this.lblCapSensor2WupCount.AutoSize = true;
            this.lblCapSensor2WupCount.Location = new System.Drawing.Point(273, 71);
            this.lblCapSensor2WupCount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCapSensor2WupCount.Name = "lblCapSensor2WupCount";
            this.lblCapSensor2WupCount.Size = new System.Drawing.Size(14, 16);
            this.lblCapSensor2WupCount.TabIndex = 29;
            this.lblCapSensor2WupCount.Text = "0";
            // 
            // lblCapSensor1WupCount
            // 
            this.lblCapSensor1WupCount.AutoSize = true;
            this.lblCapSensor1WupCount.Location = new System.Drawing.Point(273, 30);
            this.lblCapSensor1WupCount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCapSensor1WupCount.Name = "lblCapSensor1WupCount";
            this.lblCapSensor1WupCount.Size = new System.Drawing.Size(14, 16);
            this.lblCapSensor1WupCount.TabIndex = 28;
            this.lblCapSensor1WupCount.Text = "0";
            // 
            // lblCapSensor1Wup
            // 
            this.lblCapSensor1Wup.AutoSize = true;
            this.lblCapSensor1Wup.Location = new System.Drawing.Point(39, 28);
            this.lblCapSensor1Wup.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCapSensor1Wup.Name = "lblCapSensor1Wup";
            this.lblCapSensor1Wup.Size = new System.Drawing.Size(215, 16);
            this.lblCapSensor1Wup.TabIndex = 23;
            this.lblCapSensor1Wup.Text = "DoorCapSensor1 Wake Up Count: ";
            // 
            // lblCapSensor2Wup
            // 
            this.lblCapSensor2Wup.AutoSize = true;
            this.lblCapSensor2Wup.Location = new System.Drawing.Point(39, 71);
            this.lblCapSensor2Wup.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCapSensor2Wup.Name = "lblCapSensor2Wup";
            this.lblCapSensor2Wup.Size = new System.Drawing.Size(215, 16);
            this.lblCapSensor2Wup.TabIndex = 26;
            this.lblCapSensor2Wup.Text = "DoorCapSensor2 Wake Up Count: ";
            // 
            // grpMain
            // 
            this.grpMain.Controls.Add(this.lblBatteryLevelValue);
            this.grpMain.Controls.Add(this.lblBatteryLevelName);
            this.grpMain.Controls.Add(this.lblKeyfobId);
            this.grpMain.Controls.Add(this.lblKeyfobName);
            this.grpMain.Controls.Add(this.lblValue2);
            this.grpMain.Controls.Add(this.lblButtonIdenValue);
            this.grpMain.Controls.Add(this.btnRead);
            this.grpMain.Controls.Add(this.lblValue1);
            this.grpMain.Controls.Add(this.lblButtonIden);
            this.grpMain.Location = new System.Drawing.Point(4, 4);
            this.grpMain.Margin = new System.Windows.Forms.Padding(4);
            this.grpMain.Name = "grpMain";
            this.grpMain.Padding = new System.Windows.Forms.Padding(4);
            this.grpMain.Size = new System.Drawing.Size(391, 124);
            this.grpMain.TabIndex = 18;
            this.grpMain.TabStop = false;
            // 
            // lblBatteryLevelValue
            // 
            this.lblBatteryLevelValue.AutoSize = true;
            this.lblBatteryLevelValue.Location = new System.Drawing.Point(128, 102);
            this.lblBatteryLevelValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBatteryLevelValue.Name = "lblBatteryLevelValue";
            this.lblBatteryLevelValue.Size = new System.Drawing.Size(11, 16);
            this.lblBatteryLevelValue.TabIndex = 28;
            this.lblBatteryLevelValue.Text = "-";
            // 
            // lblBatteryLevelName
            // 
            this.lblBatteryLevelName.AutoSize = true;
            this.lblBatteryLevelName.Location = new System.Drawing.Point(9, 102);
            this.lblBatteryLevelName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBatteryLevelName.Name = "lblBatteryLevelName";
            this.lblBatteryLevelName.Size = new System.Drawing.Size(91, 16);
            this.lblBatteryLevelName.TabIndex = 27;
            this.lblBatteryLevelName.Text = "Battery Level: ";
            // 
            // lblKeyfobId
            // 
            this.lblKeyfobId.AutoSize = true;
            this.lblKeyfobId.Location = new System.Drawing.Point(128, 76);
            this.lblKeyfobId.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblKeyfobId.Name = "lblKeyfobId";
            this.lblKeyfobId.Size = new System.Drawing.Size(11, 16);
            this.lblKeyfobId.TabIndex = 26;
            this.lblKeyfobId.Text = "-";
            // 
            // lblKeyfobName
            // 
            this.lblKeyfobName.AutoSize = true;
            this.lblKeyfobName.Location = new System.Drawing.Point(9, 76);
            this.lblKeyfobName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblKeyfobName.Name = "lblKeyfobName";
            this.lblKeyfobName.Size = new System.Drawing.Size(68, 16);
            this.lblKeyfobName.TabIndex = 25;
            this.lblKeyfobName.Text = "Keyfob ID:";
            // 
            // lblValue2
            // 
            this.lblValue2.AutoSize = true;
            this.lblValue2.Location = new System.Drawing.Point(68, 27);
            this.lblValue2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblValue2.Name = "lblValue2";
            this.lblValue2.Size = new System.Drawing.Size(14, 16);
            this.lblValue2.TabIndex = 24;
            this.lblValue2.Text = "0";
            // 
            // lblButtonIdenValue
            // 
            this.lblButtonIdenValue.AutoSize = true;
            this.lblButtonIdenValue.Location = new System.Drawing.Point(128, 53);
            this.lblButtonIdenValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblButtonIdenValue.Name = "lblButtonIdenValue";
            this.lblButtonIdenValue.Size = new System.Drawing.Size(11, 16);
            this.lblButtonIdenValue.TabIndex = 18;
            this.lblButtonIdenValue.Text = "-";
            // 
            // lblValue1
            // 
            this.lblValue1.AutoSize = true;
            this.lblValue1.Location = new System.Drawing.Point(8, 27);
            this.lblValue1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblValue1.Name = "lblValue1";
            this.lblValue1.Size = new System.Drawing.Size(14, 16);
            this.lblValue1.TabIndex = 15;
            this.lblValue1.Text = "0";
            // 
            // lblButtonIden
            // 
            this.lblButtonIden.AutoSize = true;
            this.lblButtonIden.Location = new System.Drawing.Point(8, 52);
            this.lblButtonIden.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblButtonIden.Name = "lblButtonIden";
            this.lblButtonIden.Size = new System.Drawing.Size(100, 16);
            this.lblButtonIden.TabIndex = 23;
            this.lblButtonIden.Text = "Button Identifier:";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel1.Controls.Add(this.grpMain);
            this.flowLayoutPanel1.Controls.Add(this.grpDoorCap);
            this.flowLayoutPanel1.Controls.Add(this.grpRSSI);
            this.flowLayoutPanel1.Controls.Add(this.grpNCK2910);
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(11, 37);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(399, 528);
            this.flowLayoutPanel1.TabIndex = 36;
            // 
            // UCPEPSOutput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.lblName);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "UCPEPSOutput";
            this.Size = new System.Drawing.Size(413, 568);
            this.grpRSSI.ResumeLayout(false);
            this.grpRSSI.PerformLayout();
            this.grpNCK2910.ResumeLayout(false);
            this.grpNCK2910.PerformLayout();
            this.grpDoorCap.ResumeLayout(false);
            this.grpDoorCap.PerformLayout();
            this.grpMain.ResumeLayout(false);
            this.grpMain.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Button btnRead;
        private System.Windows.Forms.Button btnRSSI;
        private System.Windows.Forms.Label lblValue1;
        private System.Windows.Forms.TextBox txtKeyfobID4;
        private System.Windows.Forms.TextBox txtKeyfobID3;
        private System.Windows.Forms.TextBox txtKeyfobID2;
        private System.Windows.Forms.TextBox txtKeyfobID1;
        private System.Windows.Forms.GroupBox grpRSSI;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblButtonIden;
        private System.Windows.Forms.Label lblButtonIdenValue;
        private System.Windows.Forms.GroupBox grpMain;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblSQSUM;
        private System.Windows.Forms.Label lblY;
        private System.Windows.Forms.Label lblZ;
        private System.Windows.Forms.Label lblX;
        private System.Windows.Forms.Label lblValue2;
        private System.Windows.Forms.Label lblKeyfobName;
        private System.Windows.Forms.Label lblKeyfobId;
        private System.Windows.Forms.GroupBox grpDoorCap;
        private System.Windows.Forms.Label lblCapSensor2WupCount;
        private System.Windows.Forms.Label lblCapSensor1WupCount;
        private System.Windows.Forms.Label lblCapSensor1Wup;
        private System.Windows.Forms.Label lblCapSensor2Wup;
        private System.Windows.Forms.Label lblBatteryLevelValue;
        private System.Windows.Forms.Label lblBatteryLevelName;
        private System.Windows.Forms.GroupBox grpNCK2910;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblD;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lblC;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblB;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblA;
        private System.Windows.Forms.Button btnNCK2910;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.ComboBox cmbLevel;
        private System.Windows.Forms.ComboBox cmbPIN;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    }
}
