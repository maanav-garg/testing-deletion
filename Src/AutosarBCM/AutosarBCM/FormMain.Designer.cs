using System.Windows.Forms;

namespace AutosarBCM
{
    partial class FormMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.toolsTsmi = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsTsmi = new System.Windows.Forms.ToolStripMenuItem();
            this.transmitTsmi = new System.Windows.Forms.ToolStripMenuItem();
            this.traceDialogtsmi = new System.Windows.Forms.ToolStripMenuItem();
            this.testLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCheck = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiEMCView = new System.Windows.Forms.ToolStripMenuItem();
            this.environmentalTestTsmi = new System.Windows.Forms.ToolStripMenuItem();
            this.helpTsmi = new System.Windows.Forms.ToolStripMenuItem();
            this.userGuideTsmi = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutTsmi = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.openConnection = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.lblConnection = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbOpen = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbOptions = new System.Windows.Forms.ToolStripButton();
            this.tslReceived = new System.Windows.Forms.ToolStripLabel();
            this.tslTransmitted = new System.Windows.Forms.ToolStripLabel();
            this.tslDiff = new System.Windows.Forms.ToolStripLabel();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileTsmi = new System.Windows.Forms.ToolStripMenuItem();
            this.openTsmi = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.recentFilesTsmi = new System.Windows.Forms.ToolStripMenuItem();
            this.importMDXToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vS2015LightTheme1 = new WeifenLuo.WinFormsUI.Docking.VS2015LightTheme();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.txtTrace = new System.Windows.Forms.RichTextBox();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.tsbClearLog = new System.Windows.Forms.ToolStripButton();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.dockMonitor = new WeifenLuo.WinFormsUI.Docking.DockPanel();
            this.toolStrip3 = new System.Windows.Forms.ToolStrip();
            this.tsbMonitorLoad = new System.Windows.Forms.ToolStripButton();
            this.tsbSession = new System.Windows.Forms.ToolStripDropDownButton();
            this.nullToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbECUReset = new System.Windows.Forms.ToolStripButton();
            this.btnStart = new System.Windows.Forms.ToolStripButton();
            this.btnClear = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.testerPresentDropDownButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.activeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.inactiveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.tspFilterTxb = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.btnShowEmbSwVer = new System.Windows.Forms.ToolStripButton();
            this.lblEmbSwVer = new System.Windows.Forms.ToolStripLabel();
            this.tsbToggle = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.toolStrip3.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolsTsmi
            // 
            this.toolsTsmi.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionsTsmi,
            this.transmitTsmi,
            this.traceDialogtsmi,
            this.testLogToolStripMenuItem,
            this.tsmiCheck,
            this.tsmiEMCView,
            this.environmentalTestTsmi});
            this.toolsTsmi.Name = "toolsTsmi";
            this.toolsTsmi.Size = new System.Drawing.Size(58, 24);
            this.toolsTsmi.Text = "Tools";
            // 
            // optionsTsmi
            // 
            this.optionsTsmi.Image = ((System.Drawing.Image)(resources.GetObject("optionsTsmi.Image")));
            this.optionsTsmi.Name = "optionsTsmi";
            this.optionsTsmi.Size = new System.Drawing.Size(217, 26);
            this.optionsTsmi.Text = "Options";
            this.optionsTsmi.Click += new System.EventHandler(this.optionsTsmi_Click);
            // 
            // transmitTsmi
            // 
            this.transmitTsmi.Image = global::AutosarBCM.Properties.Resources.ArrowUp;
            this.transmitTsmi.Name = "transmitTsmi";
            this.transmitTsmi.Size = new System.Drawing.Size(217, 26);
            this.transmitTsmi.Text = "Transmit";
            this.transmitTsmi.Click += new System.EventHandler(this.tsmiTransmit_Click);
            // 
            // traceDialogtsmi
            // 
            this.traceDialogtsmi.Image = ((System.Drawing.Image)(resources.GetObject("traceDialogtsmi.Image")));
            this.traceDialogtsmi.Name = "traceDialogtsmi";
            this.traceDialogtsmi.Size = new System.Drawing.Size(217, 26);
            this.traceDialogtsmi.Text = "Trace Dialog";
            this.traceDialogtsmi.Click += new System.EventHandler(this.traceDialogtsmi_Click);
            // 
            // testLogToolStripMenuItem
            // 
            this.testLogToolStripMenuItem.Image = global::AutosarBCM.Properties.Resources.EditWindow;
            this.testLogToolStripMenuItem.Name = "testLogToolStripMenuItem";
            this.testLogToolStripMenuItem.Size = new System.Drawing.Size(217, 26);
            this.testLogToolStripMenuItem.Text = "Test Log Viewer";
            this.testLogToolStripMenuItem.Click += new System.EventHandler(this.testLogToolStripMenuItem_Click);
            // 
            // tsmiCheck
            // 
            this.tsmiCheck.Image = global::AutosarBCM.Properties.Resources.pass;
            this.tsmiCheck.Name = "tsmiCheck";
            this.tsmiCheck.Size = new System.Drawing.Size(217, 26);
            this.tsmiCheck.Text = "Control Check";
            this.tsmiCheck.Click += new System.EventHandler(this.tsmiCheck_Click);
            // 
            // tsmiEMCView
            // 
            this.tsmiEMCView.Image = global::AutosarBCM.Properties.Resources.DiskDiag_30222_1109286131;
            this.tsmiEMCView.Name = "tsmiEMCView";
            this.tsmiEMCView.Size = new System.Drawing.Size(217, 26);
            this.tsmiEMCView.Text = "EMC Monitor";
            this.tsmiEMCView.Click += new System.EventHandler(this.tsmiEMCView_Click);
            // 
            // environmentalTestTsmi
            // 
            this.environmentalTestTsmi.Name = "environmentalTestTsmi";
            this.environmentalTestTsmi.Size = new System.Drawing.Size(217, 26);
            this.environmentalTestTsmi.Text = "Environmental Test";
            this.environmentalTestTsmi.Click += new System.EventHandler(this.environmentalTestTsmi_Click);
            // 
            // helpTsmi
            // 
            this.helpTsmi.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.userGuideTsmi,
            this.aboutTsmi});
            this.helpTsmi.Name = "helpTsmi";
            this.helpTsmi.Size = new System.Drawing.Size(55, 24);
            this.helpTsmi.Text = "Help";
            // 
            // userGuideTsmi
            // 
            this.userGuideTsmi.Name = "userGuideTsmi";
            this.userGuideTsmi.Size = new System.Drawing.Size(164, 26);
            this.userGuideTsmi.Text = "User Guide";
            this.userGuideTsmi.Click += new System.EventHandler(this.userGuideTsmi_Click);
            // 
            // aboutTsmi
            // 
            this.aboutTsmi.Name = "aboutTsmi";
            this.aboutTsmi.Size = new System.Drawing.Size(164, 26);
            this.aboutTsmi.Text = "About";
            this.aboutTsmi.Click += new System.EventHandler(this.aboutTsmi_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openConnection,
            this.toolStripSeparator7,
            this.lblConnection,
            this.toolStripSeparator8,
            this.tsbOpen,
            this.toolStripSeparator6,
            this.tsbOptions,
            this.tslReceived,
            this.tslTransmitted,
            this.tslDiff});
            this.toolStrip1.Location = new System.Drawing.Point(0, 30);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(1889, 31);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // openConnection
            // 
            this.openConnection.Image = ((System.Drawing.Image)(resources.GetObject("openConnection.Image")));
            this.openConnection.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openConnection.Name = "openConnection";
            this.openConnection.Size = new System.Drawing.Size(143, 24);
            this.openConnection.Text = "Start Connection";
            this.openConnection.ToolTipText = "Start Connection (CTRL+C)";
            this.openConnection.Click += new System.EventHandler(this.openConnection_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 27);
            // 
            // lblConnection
            // 
            this.lblConnection.BackColor = System.Drawing.Color.Transparent;
            this.lblConnection.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblConnection.ForeColor = System.Drawing.Color.Red;
            this.lblConnection.Name = "lblConnection";
            this.lblConnection.Size = new System.Drawing.Size(57, 24);
            this.lblConnection.Text = "Offline";
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(6, 27);
            // 
            // tsbOpen
            // 
            this.tsbOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbOpen.Image = ((System.Drawing.Image)(resources.GetObject("tsbOpen.Image")));
            this.tsbOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbOpen.Name = "tsbOpen";
            this.tsbOpen.Size = new System.Drawing.Size(29, 24);
            this.tsbOpen.Text = "Open... (Ctrl+O)";
            this.tsbOpen.Click += new System.EventHandler(this.openTsmi_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 27);
            // 
            // tsbOptions
            // 
            this.tsbOptions.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbOptions.Image = ((System.Drawing.Image)(resources.GetObject("tsbOptions.Image")));
            this.tsbOptions.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbOptions.Name = "tsbOptions";
            this.tsbOptions.Size = new System.Drawing.Size(29, 24);
            this.tsbOptions.Text = "Options";
            this.tsbOptions.Click += new System.EventHandler(this.optionsTsmi_Click);
            // 
            // tslReceived
            // 
            this.tslReceived.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tslReceived.BackColor = System.Drawing.Color.Transparent;
            this.tslReceived.Image = global::AutosarBCM.Properties.Resources.arrowdown2;
            this.tslReceived.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tslReceived.Margin = new System.Windows.Forms.Padding(0, 1, 10, 2);
            this.tslReceived.Name = "tslReceived";
            this.tslReceived.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tslReceived.Size = new System.Drawing.Size(35, 24);
            this.tslReceived.Text = "0";
            // 
            // tslTransmitted
            // 
            this.tslTransmitted.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tslTransmitted.BackColor = System.Drawing.Color.Transparent;
            this.tslTransmitted.Image = global::AutosarBCM.Properties.Resources.arrowup2;
            this.tslTransmitted.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tslTransmitted.Margin = new System.Windows.Forms.Padding(0, 1, 5, 2);
            this.tslTransmitted.Name = "tslTransmitted";
            this.tslTransmitted.Size = new System.Drawing.Size(35, 24);
            this.tslTransmitted.Text = "0";
            this.tslTransmitted.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            // 
            // tslDiff
            // 
            this.tslDiff.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tslDiff.BackColor = System.Drawing.Color.Transparent;
            this.tslDiff.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tslDiff.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.tslDiff.ForeColor = System.Drawing.Color.White;
            this.tslDiff.Margin = new System.Windows.Forms.Padding(0, 1, 5, 2);
            this.tslDiff.Name = "tslDiff";
            this.tslDiff.Size = new System.Drawing.Size(20, 24);
            this.tslDiff.Text = "0";
            // 
            // splitter1
            // 
            this.splitter1.Cursor = System.Windows.Forms.Cursors.HSplit;
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter1.Location = new System.Drawing.Point(0, 977);
            this.splitter1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(1889, 4);
            this.splitter1.TabIndex = 5;
            this.splitter1.TabStop = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileTsmi,
            this.toolsTsmi,
            this.helpTsmi});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1889, 30);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileTsmi
            // 
            this.fileTsmi.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openTsmi,
            this.toolStripSeparator4,
            this.recentFilesTsmi,
            this.importMDXToolStripMenuItem});
            this.fileTsmi.Name = "fileTsmi";
            this.fileTsmi.Size = new System.Drawing.Size(46, 24);
            this.fileTsmi.Text = "File";
            // 
            // openTsmi
            // 
            this.openTsmi.Image = ((System.Drawing.Image)(resources.GetObject("openTsmi.Image")));
            this.openTsmi.Name = "openTsmi";
            this.openTsmi.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openTsmi.Size = new System.Drawing.Size(190, 26);
            this.openTsmi.Text = "Open...";
            this.openTsmi.Click += new System.EventHandler(this.openTsmi_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(187, 6);
            // 
            // recentFilesTsmi
            // 
            this.recentFilesTsmi.Name = "recentFilesTsmi";
            this.recentFilesTsmi.Size = new System.Drawing.Size(190, 26);
            this.recentFilesTsmi.Text = "Recent Files";
            // 
            // importMDXToolStripMenuItem
            // 
            this.importMDXToolStripMenuItem.Image = global::AutosarBCM.Properties.Resources.gear_load;
            this.importMDXToolStripMenuItem.Name = "importMDXToolStripMenuItem";
            this.importMDXToolStripMenuItem.Size = new System.Drawing.Size(190, 26);
            this.importMDXToolStripMenuItem.Text = "Import MDX ";
            this.importMDXToolStripMenuItem.Click += new System.EventHandler(this.tsmiImpMDX_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "diagnostic-tool.png");
            this.imageList1.Images.SetKeyName(1, "checkup.png");
            this.imageList1.Images.SetKeyName(2, "run.png");
            this.imageList1.Images.SetKeyName(3, "stop.png");
            this.imageList1.Images.SetKeyName(4, "textfile.png");
            this.imageList1.Images.SetKeyName(5, "Monitor_Screen_16xLG.png");
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.txtTrace);
            this.tabPage2.Controls.Add(this.toolStrip2);
            this.tabPage2.ImageIndex = 4;
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage2.Size = new System.Drawing.Size(1881, 885);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Trace";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // txtTrace
            // 
            this.txtTrace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtTrace.Location = new System.Drawing.Point(3, 29);
            this.txtTrace.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtTrace.Name = "txtTrace";
            this.txtTrace.ReadOnly = true;
            this.txtTrace.Size = new System.Drawing.Size(1875, 854);
            this.txtTrace.TabIndex = 10;
            this.txtTrace.Text = "";
            // 
            // toolStrip2
            // 
            this.toolStrip2.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbClearLog});
            this.toolStrip2.Location = new System.Drawing.Point(3, 2);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(1875, 27);
            this.toolStrip2.TabIndex = 11;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // tsbClearLog
            // 
            this.tsbClearLog.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.tsbClearLog.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tsbClearLog.Image = ((System.Drawing.Image)(resources.GetObject("tsbClearLog.Image")));
            this.tsbClearLog.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbClearLog.Name = "tsbClearLog";
            this.tsbClearLog.Size = new System.Drawing.Size(67, 24);
            this.tsbClearLog.Text = "Clear";
            this.tsbClearLog.Click += new System.EventHandler(this.tsbClearLog_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.ImageList = this.imageList1;
            this.tabControl1.Location = new System.Drawing.Point(0, 61);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1889, 916);
            this.tabControl1.TabIndex = 15;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.dockMonitor);
            this.tabPage3.Controls.Add(this.toolStrip3);
            this.tabPage3.ImageIndex = 5;
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(1881, 887);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Monitor";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // dockMonitor
            // 
            this.dockMonitor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dockMonitor.DockBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(242)))));
            this.dockMonitor.Location = new System.Drawing.Point(0, 31);
            this.dockMonitor.Margin = new System.Windows.Forms.Padding(4, 0, 4, 4);
            this.dockMonitor.Name = "dockMonitor";
            this.dockMonitor.Padding = new System.Windows.Forms.Padding(6);
            this.dockMonitor.ShowAutoHideContentOnHover = false;
            this.dockMonitor.Size = new System.Drawing.Size(1881, 856);
            this.dockMonitor.TabIndex = 2;
            this.dockMonitor.Theme = this.vS2015LightTheme1;
            this.dockMonitor.ActiveDocumentChanged += new System.EventHandler(this.dockMonitor_ActiveDocumentChanged);
            // 
            // toolStrip3
            // 
            this.toolStrip3.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbMonitorLoad,
            this.tsbSession,
            this.tsbECUReset,
            this.btnStart,
            this.btnClear,
            this.toolStripSeparator1,
            this.testerPresentDropDownButton,
            this.toolStripLabel2,
            this.tspFilterTxb,
            this.toolStripSeparator5,
            this.btnShowEmbSwVer,
            this.lblEmbSwVer,
            this.tsbToggle});
            this.toolStrip3.Location = new System.Drawing.Point(0, 0);
            this.toolStrip3.Name = "toolStrip3";
            this.toolStrip3.Size = new System.Drawing.Size(1881, 31);
            this.toolStrip3.TabIndex = 1;
            this.toolStrip3.Text = "toolStrip3";
            // 
            // tsbMonitorLoad
            // 
            this.tsbMonitorLoad.Image = global::AutosarBCM.Properties.Resources.gear_load;
            this.tsbMonitorLoad.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbMonitorLoad.Name = "tsbMonitorLoad";
            this.tsbMonitorLoad.Size = new System.Drawing.Size(78, 24);
            this.tsbMonitorLoad.Text = "Import";
            this.tsbMonitorLoad.Click += new System.EventHandler(this.tsbMonitorLoad_Click);
            // 
            // tsbSession
            // 
            this.tsbSession.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nullToolStripMenuItem});
            this.tsbSession.Image = global::AutosarBCM.Properties.Resources.pass;
            this.tsbSession.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSession.Name = "tsbSession";
            this.tsbSession.Size = new System.Drawing.Size(126, 24);
            this.tsbSession.Text = "Session: N/A";
            // 
            // nullToolStripMenuItem
            // 
            this.nullToolStripMenuItem.Name = "nullToolStripMenuItem";
            this.nullToolStripMenuItem.Size = new System.Drawing.Size(119, 26);
            this.nullToolStripMenuItem.Text = "N/A";
            // 
            // tsbECUReset
            // 
            this.tsbECUReset.Image = global::AutosarBCM.Properties.Resources.reset;
            this.tsbECUReset.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbECUReset.Name = "tsbECUReset";
            this.tsbECUReset.Size = new System.Drawing.Size(96, 24);
            this.tsbECUReset.Text = "ECUReset";
            this.tsbECUReset.Click += new System.EventHandler(this.tsbECUReset_Click);
            // 
            // btnStart
            // 
            this.btnStart.Image = global::AutosarBCM.Properties.Resources.play_pause;
            this.btnStart.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnStart.Margin = new System.Windows.Forms.Padding(5, 1, 10, 2);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(64, 25);
            this.btnStart.Text = "Start";
            this.btnStart.Visible = false;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnClear
            // 
            this.btnClear.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnClear.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnClear.Image = ((System.Drawing.Image)(resources.GetObject("btnClear.Image")));
            this.btnClear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(109, 24);
            this.btnClear.Text = "Clear Fields";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // testerPresentDropDownButton
            // 
            this.testerPresentDropDownButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.activeToolStripMenuItem,
            this.inactiveToolStripMenuItem});
            this.testerPresentDropDownButton.Image = global::AutosarBCM.Properties.Resources.testerPresent;
            this.testerPresentDropDownButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.testerPresentDropDownButton.Name = "testerPresentDropDownButton";
            this.testerPresentDropDownButton.Size = new System.Drawing.Size(134, 24);
            this.testerPresentDropDownButton.Text = "Tester Present";
            // 
            // activeToolStripMenuItem
            // 
            this.activeToolStripMenuItem.Name = "activeToolStripMenuItem";
            this.activeToolStripMenuItem.Size = new System.Drawing.Size(143, 26);
            this.activeToolStripMenuItem.Text = "Active";
            this.activeToolStripMenuItem.Click += new System.EventHandler(this.activeToolStripMenuItem_Click);
            // 
            // inactiveToolStripMenuItem
            // 
            this.inactiveToolStripMenuItem.Checked = true;
            this.inactiveToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.inactiveToolStripMenuItem.Name = "inactiveToolStripMenuItem";
            this.inactiveToolStripMenuItem.Size = new System.Drawing.Size(143, 26);
            this.inactiveToolStripMenuItem.Text = "Inactive";
            this.inactiveToolStripMenuItem.Click += new System.EventHandler(this.inactiveToolStripMenuItem_Click);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(49, 24);
            this.toolStripLabel2.Text = "Filter: ";
            // 
            // tspFilterTxb
            // 
            this.tspFilterTxb.Enabled = false;
            this.tspFilterTxb.Name = "tspFilterTxb";
            this.tspFilterTxb.Size = new System.Drawing.Size(100, 27);
            this.tspFilterTxb.TextChanged += new System.EventHandler(this.tspFilterTxb_TextChanged);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 27);
            this.toolStripSeparator5.Visible = false;
            // 
            // btnShowEmbSwVer
            // 
            this.btnShowEmbSwVer.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnShowEmbSwVer.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnShowEmbSwVer.Image = ((System.Drawing.Image)(resources.GetObject("btnShowEmbSwVer.Image")));
            this.btnShowEmbSwVer.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnShowEmbSwVer.Name = "btnShowEmbSwVer";
            this.btnShowEmbSwVer.Size = new System.Drawing.Size(107, 24);
            this.btnShowEmbSwVer.Text = "SW Version";
            this.btnShowEmbSwVer.Click += new System.EventHandler(this.btnShowEmbSwVer_Click);
            // 
            // lblEmbSwVer
            // 
            this.lblEmbSwVer.ForeColor = System.Drawing.Color.Green;
            this.lblEmbSwVer.Name = "lblEmbSwVer";
            this.lblEmbSwVer.Size = new System.Drawing.Size(0, 24);
            // 
            // tsbToggle
            // 
            this.tsbToggle.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbToggle.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbToggle.Image = global::AutosarBCM.Properties.Resources.msg_28364812;
            this.tsbToggle.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbToggle.Name = "tsbToggle";
            this.tsbToggle.Size = new System.Drawing.Size(29, 24);
            this.tsbToggle.Text = "toolStripButton1";
            this.tsbToggle.Click += new System.EventHandler(this.tsbToggle_Click);
            // 
            // FormMain
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1889, 981);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MinimumSize = new System.Drawing.Size(770, 616);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TransparencyKey = System.Drawing.Color.White;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ClientSizeChanged += new System.EventHandler(this.FormMain_ClientSizeChanged);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.FormMain_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.FormMain_DragEnter);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.toolStrip3.ResumeLayout(false);
            this.toolStrip3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStripMenuItem helpTsmi;
        private System.Windows.Forms.ToolStripMenuItem aboutTsmi;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolsTsmi;
        private System.Windows.Forms.ToolStripMenuItem optionsTsmi;
        private System.Windows.Forms.ToolStripButton tsbOpen;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripButton tsbOptions;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private WeifenLuo.WinFormsUI.Docking.VS2015LightTheme vS2015LightTheme1;
        public System.Windows.Forms.ToolStripButton openConnection;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        internal System.Windows.Forms.ToolStripLabel lblConnection;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        internal System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolStripMenuItem fileTsmi;
        private System.Windows.Forms.ToolStripMenuItem openTsmi;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem recentFilesTsmi;
        private System.Windows.Forms.TabPage tabPage2;
        internal System.Windows.Forms.RichTextBox txtTrace;
        private System.Windows.Forms.TabControl tabControl1;
        private ToolStrip toolStrip2;
        private ToolStripButton tsbClearLog;
        private TabPage tabPage3;
        private ToolStrip toolStrip3;
        private ToolStripButton tsbMonitorLoad;
        private ToolStripButton btnStart;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem userGuideTsmi;
        private ToolStripButton tsbECUReset;
        private ToolStripLabel toolStripLabel2;
        private ToolStripTextBox tspFilterTxb;
        private ToolStripSeparator toolStripSeparator5;
        private ToolStripButton btnClear;
        private ToolStripButton btnShowEmbSwVer;
        private ToolStripLabel lblEmbSwVer;
        private ToolStripMenuItem traceDialogtsmi;
        private ToolStripLabel tslReceived;
        private ToolStripLabel tslTransmitted;
        private ToolStripLabel tslDiff;
        private ToolStripMenuItem testLogToolStripMenuItem;
        private ToolStripMenuItem tsmiCheck;
        private ToolStripButton tsbToggle;
        private ToolStripMenuItem nullToolStripMenuItem;
        private ToolStripDropDownButton testerPresentDropDownButton;
        public ToolStripMenuItem activeToolStripMenuItem;
        public ToolStripMenuItem inactiveToolStripMenuItem;
        private ToolStripMenuItem environmentalTestTsmi;
        internal WeifenLuo.WinFormsUI.Docking.DockPanel dockMonitor;
        private ToolStripMenuItem tsmiEMCView;
        public ToolStripDropDownButton tsbSession;
        private ToolStripMenuItem transmitTsmi;
        private ToolStripMenuItem importMDXToolStripMenuItem;
    }
}

