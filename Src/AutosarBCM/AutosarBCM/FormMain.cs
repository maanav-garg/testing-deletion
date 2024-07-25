using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using AutosarBCM.Common;
using AutosarBCM.Config;
using AutosarBCM.Forms.Monitor;
using AutosarBCM.Properties;
using AutosarBCM.UserControls.Monitor;
using WeifenLuo.WinFormsUI.Docking;
using AutosarBCM.Forms;
using AutosarBCM.Core.Config;
using AutosarBCM.Core;

namespace AutosarBCM
{
    /// <summary>
    /// Main window
    /// </summary>
    partial class FormMain : Form
    {
        #region Variables

        /// <summary>
        /// Helper for loading/saving/displaying recent tool files.
        /// </summary>
        private readonly RecentFileHelper recentToolFileHelper = new RecentFileHelper();

        /// <summary>
        /// Determines whether a test is running or not.
        /// </summary>
        internal static bool IsTestRunning = false;

        /// <summary>
        /// The name of a log file by instant time.
        /// </summary>
        private static string logFileName = DateTime.Now.ToString("dd-MM-yyyy HH-mm-ss_");

        /// <summary>
        /// Clone of the selected test type
        /// </summary>
        public static MonitorTestType MonitorTestTypeClone;

        /// <summary>
        /// A concurrent queue for storing error log messages.
        /// </summary>
        public ConcurrentQueue<string> LogErrorMessageQueue = new ConcurrentQueue<string>();

        /// <summary>
        /// A concurrent queue for storing cycle log messages.
        /// </summary>
        public ConcurrentQueue<string> LogCycleMessageQueue = new ConcurrentQueue<string>();

        /// <summary>
        /// An instance of the 'UCCycleBar' control.
        /// </summary>
        private UCCycleBar ucCycleBar;

        /// <summary>
        /// Determines how many output test clicked.
        /// </summary>
        public static int TestClickCounter { get { return testClickCounter; } set { testClickCounter = value < 0 ? 0 : value; } }
        public static int testClickCounter = 0;

        /// <summary>
        /// A CancellationTokenSource for managing cancellation of asynchronous operations.
        /// </summary>
        private CancellationTokenSource cancellationTokenSource;

        /// <summary>
        /// An instance of the 'ConnectionUtil' class for managing connections and transmissions.
        /// </summary>
        public ConnectionUtil ConnectionUtil = new ConnectionUtil();

        /// <summary>
        /// A list to store log messages, each represented as a tuple containing color and message text.
        /// </summary>
        private List<(Color, string)> log = new List<(Color, string)>();

        /// <summary>
        /// An instance of the 'FormMonitorGenericInput' control.
        /// </summary>
        private FormMonitorGenericInput formMonitorGenericInput = new FormMonitorGenericInput();

        /// <summary>
        /// An instance of the 'FormMonitorGenericOutput' control.
        /// </summary>
        private FormMonitorGenericOutput formMonitorGenericOutput = new FormMonitorGenericOutput();

        /// <summary>
        /// An instance of the 'FormMonitorEnvInput' control.
        /// </summary>
        private FormMonitorEnvInput formMonitorEnvInput = new FormMonitorEnvInput();

        /// <summary>
        /// An instance of the 'FormMonitorEnvOutput' control.
        /// </summary>
        private FormMonitorEnvOutput formMonitorEnvOutput = new FormMonitorEnvOutput();

        private FormDTCPanel formDTCPanel = new FormDTCPanel();

        /// <summary>
        /// An instance of the 'FormTracePopup' control for displaying trace messages.
        /// </summary>
        private FormTracePopup tracePopup = new FormTracePopup();

        /// <summary>
        /// An instance of the 'FormEnvironmentalTest' control for displaying trace messages.
        /// </summary>
        private FormEnvironmentalTest formEnvironmentalTest;
        /// <summary>
        /// An instance of the 'FormEMCView' control.
        /// </summary>
        private FormEMCView formEMCView;
        /// <summary>
        /// An instance of the 'FormLogReader' control for displaying trace messages.
        /// </summary>
        private FormTestLogView logReader;

        /// <summary>
        /// A timer for periodically processing error log messages.
        /// </summary>
        private System.Timers.Timer errorLogMessageTimer = new System.Timers.Timer();

        /// <summary>
        /// A timer for periodically processing cycle log messages.
        /// </summary>
        private System.Timers.Timer cycleLogMessageTimer = new System.Timers.Timer();

        /// <summary>
        /// Configuration settings for the monitor.
        /// </summary>
        internal static AutosarBcmConfiguration Configuration;

        /// <summary>
        /// The total number of messages received.
        /// </summary>
        private static float MessagesReceived = 0;

        /// <summary>
        /// The total number of messages transmitted.
        /// </summary>
        private static float MessagesTransmitted = 0;

        /// <summary>
        /// The total number of messages received.
        /// </summary>
        private static long errorMessageCountCounter = 0;

        /// <summary>
        /// The total number of messages transmitted.
        /// </summary>
        private static long cycleMessageCountCounter = 0;

        /// <summary>
        /// A Boolean value representing whether the ControlChecker test is running or not.
        /// </summary>
        internal static bool ControlChecker;

        /// <summary>
        /// A Boolean value representing whether the EMCMonitoring test is running or not.
        /// </summary>
        internal static bool EMCMonitoring;

        internal static bool FormGenericInput;


        internal static List<IReceiver> Receivers = new List<IReceiver>();
        internal static ASContext ASContext;

        private TesterPresent TesterPresent;
        private ECUReset ECUReset;
        private System.Timers.Timer TesterPresentTimer;

        /// <summary>
        /// Gets the selected test type
        /// </summary>
        public MonitorTestType MonitorTestType
        {
            get
            {
                return (MonitorTestType)Enum.Parse(typeof(MonitorTestType), cmbTestType.SelectedItem.ToString());
            }
        }

        /// <summary>
        /// Overrides the creation parameters of the control to improve rendering performance.
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }


        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor.
        /// </summary>
        public FormMain()
        {
            InitializeComponent();
            Text = Helper.AppVersionText;
            SetCounter(0, 0);
            AppendTrace(Text);

            cmbTestType.Items.AddRange(Enum.GetValues(typeof(MonitorTestType)).Cast<object>().ToArray());

            ucCycleBar = new UCCycleBar();
            toolStrip3.Items.Add(new ToolStripControlHost(ucCycleBar));

            errorLogMessageTimer.Interval = 1000;
            errorLogMessageTimer.Elapsed += ErrorLogOnTimedEvent;
            errorLogMessageTimer.Start();

            cycleLogMessageTimer.Interval = 1000;
            cycleLogMessageTimer.Elapsed += CycleLogOnTimedEvent;
            cycleLogMessageTimer.Start();

            Receivers.Add(formMonitorGenericInput);
            Receivers.Add(formDTCPanel);

            TesterPresentTimer = new System.Timers.Timer(1000) { };
            TesterPresentTimer.Elapsed += (s, e) => TesterPresent.Transmit();

        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Updates input monitor controls based on received data.
        /// </summary>
        /// <param name="receivedData">The received data to process.</param>
        /// <returns>True if the status was changed; otherwise, false.</returns>
        public bool UpdateInputMonitorControls(byte[] receivedData, MessageDirection messageDirection)
        {
            if (dockMonitor.Documents.ElementAt(0) is IPeriodicTest periodicTest && (FormMain.IsTestRunning || FormMain.TestClickCounter > 0))
            {
                return periodicTest.ChangeStatus(receivedData, messageDirection);
            }
            return false;
        }

        /// <summary>
        /// Updates output monitor controls based on received data.
        /// </summary>
        /// <param name="receivedData">The received data to process.</param>
        /// <returns>True if the status was changed; otherwise, false.</returns>
        public bool UpdateOutputMonitorControls(byte[] receivedData, MessageDirection messageDirection)
        {
            if (dockMonitor.Documents.ElementAt(1) is IClickTest clickTest && (FormMain.IsTestRunning || FormMain.TestClickCounter > 0))
            {
                return clickTest.ChangeStatus(receivedData, messageDirection);
            }
            return false;
        }

        /// <summary>
        /// Appends given text to trace box with timestamp.
        /// </summary>
        /// <param name="text">text to append</param>
        /// <param name="color">Optional text color. Default black</param>
        public void AppendTrace(string text, Color? color = null, bool flush = false)
        {
            log.Add((color ?? Color.Black, $"{DateTime.Now.ToString("HH:mm:ss.fff")}: {text}{Environment.NewLine}"));

            if (flush || !IsTestRunning || log.Count > Settings.Default.FlushToUI)
            {
                if (txtTrace.InvokeRequired)
                    txtTrace.Invoke(new Action(() => FlushLog()));
                else FlushLog();
            }
        }

        /// <summary>
        /// Sets double buffering for a Windows Forms control.
        /// </summary>
        /// <param name="c">The control for which to enable double buffering.</param>
        public static void SetDoubleBuffered(System.Windows.Forms.Control c)
        {
            if (System.Windows.Forms.SystemInformation.TerminalServerSession)
                return;
            System.Reflection.PropertyInfo aProp = typeof(System.Windows.Forms.Control).GetProperty("DoubleBuffered", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            aProp.SetValue(c, true, null);
        }

        /// <summary>
        /// Sets enable status of tabControl control.
        /// </summary>
        /// <param name="status">Status of tabControl.</param>
        public void ChangeTabControlStatus(bool status)
        {
            if (this.InvokeRequired)
                Invoke(new Action(() => { tabControl1.Enabled = status; }));
            else
                tabControl1.Enabled = status;
        }

        /// <summary>
        /// Assigns Embedded software version to label 
        /// </summary>
        /// <param name="array">Hex data array containing the version</param>
        internal void SetEmbeddedSoftwareVersion(byte[] array)
        {
            lblEmbSwVer.Text = string.Join(".", array.Select(b => Convert.ToInt32(b).ToString()));
        }

        /// <summary>
        /// Sets the counter values for transmitted and received messages. Invokes the operation on the UI thread if required.
        /// </summary>
        /// <param name="transmitted">The number of transmitted messages.</param>
        /// <param name="received">The number of received messages.</param>
        internal void SetCounter(float transmitted, float received)
        {
            if (this.InvokeRequired)
                this.Invoke(new Action(() => { SetCounterValues(transmitted, received); }));
            else
                SetCounterValues(transmitted, received);
        }
        public void StartTesterPresent()
        {
            activeToolStripMenuItem.Checked = true;
            inactiveToolStripMenuItem.Checked = false;
            testerPresentDropDownButton.Image = Resources.pass;

            TesterPresent = new TesterPresent();
            TesterPresentTimer.Start();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Handles the event triggered by the error log timer to write error messages to a log file.
        /// </summary>
        /// <param name="source">The source of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void ErrorLogOnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            errorLogMessageTimer.Stop();
            var errorList = new List<string>();

            var queCount = LogErrorMessageQueue.Count;

            for (int i = 0; i < queCount; i++)
                if (LogErrorMessageQueue.TryDequeue(out string message))
                    errorList.Add(message);

            errorMessageCountCounter += errorList.Count;

            if (errorList.Count > 0)
                File.AppendAllLines(AppDomain.CurrentDomain.BaseDirectory + $"/{logFileName}ErrorLog{errorMessageCountCounter / 950000}.txt", errorList);

            errorLogMessageTimer.Start();
        }
        private void LoadXMLDoc()
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Xml|*.xml";
            openFileDialog.Multiselect = false;
            openFileDialog.RestoreDirectory = true;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                var filePath = openFileDialog.FileName;
                ParseMessages(filePath);
                ASContext = new ASContext(filePath);
                LoadSessions();

                if (ASContext.Configuration == null)
                    return;
                else
                    tspFilterTxb.Enabled = true;

                if (dockMonitor.Documents.ElementAt(0) is FormMonitorGenericInput genericInput)
                {
                    genericInput.ClearPreviousConfiguration();
                    genericInput.LoadConfiguration(ASContext.Configuration);
                    formDTCPanel.LoadConfiguration();
                    if (tsbSession.Text != "Session: N/A")
                    {
                        genericInput.SessionFiltering();
                    }
                    //((FormMonitorGenericOutput)dockMonitor.Documents.ElementAt(1)).LoadConfiguration(Configuration);
                }
                else if (dockMonitor.Documents.ElementAt(0) is FormMonitorEnvInput envInput)
                {
                    envInput.LoadConfiguration(Configuration);
                    ((FormMonitorEnvOutput)dockMonitor.Documents.ElementAt(1)).LoadConfiguration(Configuration);
                }
            }
        }

        /// <summary>
        /// Handles the event triggered by the cycle log timer to write cycle messages to a log file.
        /// </summary>
        /// <param name="source">The source of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void CycleLogOnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            cycleLogMessageTimer.Stop();
            var messageList = new List<string>();

            var queCount = LogCycleMessageQueue.Count;

            for (int i = 0; i < queCount; i++)
                if (LogCycleMessageQueue.TryDequeue(out string message))
                    messageList.Add(message);

            cycleMessageCountCounter += messageList.Count;

            if (messageList.Count > 0)
                File.AppendAllLines(AppDomain.CurrentDomain.BaseDirectory + $"/{logFileName}CycleLog{cycleMessageCountCounter / 950000}.txt", messageList);

            cycleLogMessageTimer.Start();
        }

        /// <summary>
        /// Shows a form for given file.
        /// </summary>
        /// <param name="fileName">file path</param>
        private void OpenToolFile(string fileName)
        {
            // create a label
            var label = new Label()
            {
                Dock = DockStyle.Fill,
                Text = fileName
            };

            // open form with label
            var f = new Form() { MdiParent = this };
            f.Controls.Add(label);
            f.Show();   // non-blocking
        }

        /// <summary>
        /// Loads given file in a background operation. Writes logs to trace box.
        /// </summary>
        /// <param name="fileName">File name</param>
        private void LoadFile(string fileName)
        {
            #region The operation to run background

            DoWorkEventHandler doWork = (sender, args) =>
            {
                // parse the file
            };

            #endregion

            var progressForm = new FormProgress();
            var resultArgs = progressForm.Run(doWork);
            if (resultArgs.Error == null)
            {
                #region write to trace
                // initiate the trace
                var traceText = new StringBuilder();
                traceText.Append("File loaded: " + fileName);

                // populate the logs when parsing


                AppendTrace(traceText.ToString());
                #endregion

                // add to recent file list
                recentToolFileHelper.AddToRecentFiles(fileName);    // menu will be updated
            }
            else
            {
                // fatal error occured
                var exception = resultArgs.Error;
                Helper.ShowErrorMessageBox(exception, "Unable to read file");
                AppendTrace("Unable to read file [" + fileName + "]");   // add to trace
                // write to log file
                Helper.Logger.Error("Unable to read file", exception);
            }
        }

        /// <summary>
        /// Flushes the log to the output textbox and file when necessary.
        /// </summary>
        private void FlushLog()
        {
            if (txtTrace.Lines.Length > Settings.Default.FlushToFile)
            {
                txtTrace.Clear();
            }

            txtTrace.Select(txtTrace.TextLength, 0);

            List<(Color, string)> logToBeFlushed;
            lock (log)
            {
                logToBeFlushed = log.ToList();
                log.Clear();
            }
            foreach (var item in logToBeFlushed)
            {
                txtTrace.SelectionColor = item.Item1;
                txtTrace.AppendText(item.Item2);

            }
            if (tracePopup != null && !tracePopup.IsDisposed && tracePopup.Visible)
            {
                foreach (var item in logToBeFlushed)
                {
                    tracePopup.AppendTraceToPopup(item.Item1, item.Item2);
                }
            }

            txtTrace.ScrollToCaret();

            if (txtTrace.Lines.Length > Settings.Default.FlushToFile)
            {
                FileInfo logFile = new FileInfo("log.txt");
                try
                {
                    if (File.Exists(Settings.Default.TraceFilePath) || (!string.IsNullOrWhiteSpace(Settings.Default.TraceFilePath) && !File.Exists(Settings.Default.TraceFilePath)))
                        logFile = new FileInfo(logFileName + Settings.Default.TraceFilePath);
                }
                catch { }

                if (logFile.Exists && logFile.Length > Settings.Default.RollingAfter * 1048576)
                    logFile.MoveTo($"{logFile.DirectoryName}/{Path.GetFileNameWithoutExtension(logFile.Name)}_{DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss")}.txt");
                File.AppendAllText(logFile.FullName, txtTrace.Text);
            }
        }

        /// <summary>
        /// Saves the trace log
        /// </summary>
        private void SaveTraceLog()
        {
            if (txtTrace.Text.Length > 0)
            {
                File.AppendAllText(logFileName + Settings.Default.TraceFilePath, txtTrace.Text);
            }
        }

        /// <summary>
        /// Exports the defined messages
        /// </summary>
        /// <param name="fileName">File path</param>
        private void ExportMessages(string fileName)
        {
            //var serialization = new SerializationService();
            //serialization.CanMessages = ((FormTransmit)dockTransmit.Documents.ElementAt(0)).Messages;
            //serialization.UdsMessages = ((FormUds)dockTransmit.Documents.ElementAt(1)).Messages;
            //serialization.Serialize(fileName);
        }

        /// <summary>
        /// Import messages from config file
        /// </summary>
        /// <param name="fileName">File path</param>
        private void ImportMessages(string fileName)
        {
            //var messages = SerializationService.Deserialize(fileName);
            //((FormTransmit)dockTransmit.Documents.ElementAt(0)).ImportMessages(messages.CanMessages);
            //((FormUds)dockTransmit.Documents.ElementAt(1)).ImportMessages(messages.UdsMessages);
        }

        /// <summary>
        /// Switches panels of the monitor form based on the type of the test.
        /// </summary>
        /// <param name="testType">The type of the test to be displayed.</param>
        private void ShowMonitorPanel(MonitorTestType testType)
        {
            if (IsTestRunning)
            {
                //Test
                Helper.ShowWarningMessageBox("There is an already running test. Please stop it first");
                return;
            }
            foreach (DockContent doc in dockMonitor.Documents.ToList())
                doc.Hide();
            //doc.Dispose();

            switch (testType)
            {
                case MonitorTestType.Generic:
                    formMonitorGenericInput.Show(dockMonitor, DockState.Document);
                    formDTCPanel.Show(dockMonitor, DockState.Document);
                    // visibility settings for generic output tab
                    //formMonitorGenericOutput.Show(dockMonitor, DockState.Document);
                    break;
                case MonitorTestType.Environmental:
                    formMonitorEnvInput.Show(dockMonitor, DockState.Document);
                    formMonitorEnvOutput.Show(dockMonitor, DockState.Document);
                    break;
            }

            foreach (DockContent doc in dockMonitor.Documents)
            {
                doc.DockHandler.AllowEndUserDocking = true;
                doc.DockHandler.CloseButtonVisible = false;
            }

            if (dockMonitor.DocumentsCount > 0)
                ((DockContent)dockMonitor.Documents.ElementAt(0)).Activate();

            ucCycleBar.Visible = testType == MonitorTestType.Environmental;
        }

        /// <summary>
        /// Handles the event when the "Set Ground" button is clicked. It sets the default status for a generic test.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        private void btnSetGround_Click(object sender, EventArgs e)
        {
            if (IsTestRunning)
            {
                Helper.ShowWarningMessageBox("There is an already running test. Please stop it first");
                return;
            }
            if (cmbTestType.SelectedIndex == 1)
            {
                Helper.ShowWarningMessageBox("Please select the generic test first");
                return;
            }

            //if (dockMonitor.Documents.ElementAt(0) is FormMonitorGenericInput input)
            //    input.SetDefaultStatus(((ToolStripMenuItem)sender).Text == "Ground" ? (byte)0 : (byte)1);
        }

        /// <summary>
        /// Updates the counter values for transmitted and received messages on the UI.
        /// </summary>
        /// <param name="transmitted">The number of transmitted messages.</param>
        /// <param name="received">The number of received messages.</param>
        private void SetCounterValues(float transmitted, float received)
        {
            MessagesTransmitted += transmitted;
            MessagesReceived += received;
            tslTransmitted.Text = MessagesTransmitted.ToString();
            tslReceived.Text = MessagesReceived.ToString();

            var diff = MessagesReceived / MessagesTransmitted;
            tslDiff.Text = diff.ToString("P2");
            tslDiff.BackColor = diff == 1 ? Color.Green : (diff > 0.9 ? Color.Orange : Color.Red);
        }

        private void testLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((logReader == null || logReader.IsDisposed) && !IsTestRunning)
            {
                logReader = new FormTestLogView() { Owner = this };

                logReader.Show();
            }
            else if (IsTestRunning)
            {
                MessageBox.Show("Log View can not be open while test is running!");
            }
        }

        private void tsmiCheck_Click(object sender, EventArgs e)
        {
            new FormControlChecker().Show();
        }
        private void activeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!ConnectionUtil.CheckConnection())
                return;
           
            StartTesterPresent();
        }

        private void inactiveToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
            StopTesterPresent();
        }
        private bool CheckConfigurationFile()
        {
            if (ASContext.Configuration == null)
            {
                Helper.ShowWarningMessageBox("No configuration file is imported. Please import the file first!");
                return false;
            }
            else
            {
                return true;
            }
        }

        #endregion

        #region Form events

        /// <summary>
        /// Initializes form after loading.
        /// </summary>
        /// <param name="sender">form</param>
        /// <param name="e">argument</param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            #region configure recent tool files
            recentToolFileHelper.MenuClick += (o, s) => OpenToolFile(s);
            recentToolFileHelper.ListUpdated += (o, s) =>
            {
                Properties.Settings.Default.RecentToolFiles = s;    // update settings. no need to save immediately. save before closing.
                recentToolFileHelper.UpdateRecentFilesMenu(recentFilesTsmi);    // update menu
            };
            recentToolFileHelper.Init(Properties.Settings.Default.RecentToolFiles);
            this.WindowState = FormWindowState.Maximized;
            #endregion

            cmbTestType.SelectedIndex = 0;
        }

        /// <summary>
        /// Saves the settings before closing the form.
        /// </summary>
        /// <param name="sender">form</param>
        /// <param name="e">argument</param>
        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            // save settings
            // Properties.Settings.Default.Save();
            //SaveTraceLog();
            //ConnectionUtil.Disconnect();
            var result = Helper.ShowYesNoCancelMessageBox("Do you want to save the trace log?");

            if (result == DialogResult.Yes)
                SaveTraceLog();
            else if (result == DialogResult.Cancel)
            {
                e.Cancel = true;
                return;
            }

            // save settings
            Properties.Settings.Default.Save();
            ConnectionUtil.Disconnect();
        }

        /// <summary>
        /// Called when a file is dragged into window.
        /// </summary>
        /// <param name="sender">form</param>
        /// <param name="e">arguments</param>
        private void FormMain_DragEnter(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(DataFormats.FileDrop))
                return;
            var files = (string[])e.Data.GetData(DataFormats.FileDrop);
            bool allowed = false;
            if (files.Length == 1)
            {
                var fileName = files[0];
                //if (fileName.EndsWith(".xml", StringComparison.InvariantCultureIgnoreCase))
                //allowed = loadFileTsmi.Enabled;
            }
            else if (files.Length > 1)
            {
                // not allowed
            }

            if (allowed)
                e.Effect = DragDropEffects.Copy;
        }

        /// <summary>
        /// Called when user dropped a dragged file. It opend file according to the file type. File type is detected from extension.
        /// </summary>
        /// <param name="sender">form</param>
        /// <param name="e">not used</param>
        private void FormMain_DragDrop(object sender, DragEventArgs e)
        {
            var files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files.Length != 1)
                return;

            Focus();
            LoadFile(files[0]);
        }

        /// <summary>
        /// Handles the "Open Connection" button click event to start or stop the connection.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        private void openConnection_Click(object sender, EventArgs e)
        {
            try
            {
                string connectionLogger = "";

                if (openConnection.Text == "Start Connection")
                {
                    if (!ConnectionUtil.BaseConnection())
                        return;
                    StartTesterPresent();
                }
                else if (openConnection.Text == "Stop Connection")
                {
                    if (IsTestRunning)
                        btnStart_Click(null, null);
                    StopTesterPresent();
                    ConnectionUtil.Disconnect();
                }
            }
            catch (Exception ex)
            {
                AppendTrace(ex.ToString(), Color.Red);
                MessageBox.Show("Please check the logs", "Error", MessageBoxButtons.OK);
            }
        }

        private void StopTesterPresent()
        {
            activeToolStripMenuItem.Checked = false;
            inactiveToolStripMenuItem.Checked = true;
            testerPresentDropDownButton.Image = Resources.reset;

            TesterPresentTimer?.Stop();
        }
        /// <summary>
        /// Clears the log panel
        /// </summary>
        /// <param name="sender">Control</param>
        /// <param name="e">Event args</param>
        private void tsbClearLog_Click(object sender, EventArgs e)
        {
            if(Helper.ShowConfirmationMessageBox("Do you want to save the trace log?"))
                SaveTraceLog();
            txtTrace.Clear();
        }

        /// <summary>
        /// Handles the click event for the "Load Monitor Configuration" button, allowing the user to load an XML configuration file.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        private void tsbMonitorLoad_Click(object sender, EventArgs e)
        {
            LoadXMLDoc();
            //LoadODXDoc();
        }

        /// <summary>
        /// An event handler to the cmbTestType's SelectedIndexChanged event.
        /// </summary>
        /// <param name="sender">A reference to the cmbTestType instance.</param>
        /// <param name="e">A reference to the SelectedIndexChanged event's arguments.</param>
        private void cmbTestType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Enum.TryParse(cmbTestType.Text, false, out MonitorTestType result))
                ShowMonitorPanel(result);
        }

        /// <summary>
        /// An event handler to the btnStart's Click event.
        /// </summary>
        /// <param name="sender">A reference to the btnStart instance.</param>
        /// <param name="e">A reference to the Click event's arguments.</param>
        private void btnStart_Click(object sender, EventArgs e)
        {
            if (!ConnectionUtil.CheckConnection())
                return;
            if (IsTestRunning)
            {
                cancellationTokenSource.Cancel();

                File.AppendAllText(logFileName + Settings.Default.TraceFilePath, txtTrace.Text);

                TestClickCounter = 0;
            }
            else
            {
                if (dockMonitor.Documents.ElementAt(0) is IPeriodicTest periodicTest)
                {
                    if (!periodicTest.CanBeRun())
                    {
                        Helper.ShowWarningMessageBox("Please, load the configuration file first.");
                        return;
                    }
                    MonitorTestTypeClone = MonitorTestType;
                    if (MonitorTestType == MonitorTestType.Environmental)
                        logFileName = DateTime.Now.ToString("dd-MM-yyyy HH-mm-ss_");
                    cancellationTokenSource = new CancellationTokenSource();
                    periodicTest.StartTest(cancellationTokenSource.Token);
                }
            }

            IsTestRunning = !IsTestRunning;
            ucCycleBar.Running = IsTestRunning;
            if (IsTestRunning)
            {
                btnStart.Text = "Stop";
                btnStart.ForeColor = Color.Red;
            }
            else
            {
                btnStart.Text = "Start";
                btnStart.ForeColor = Color.Green;
            }
        }

        /// <summary>
        /// Diagnostic mode and security authentication messages is handled 
        /// </summary>
        /// <param name="sender">Activate Button</param>
        /// <param name="e">Params</param>
        private void tsbActivateDiagSession_Click(object sender, EventArgs e)
        {
            if (!ConnectionUtil.CheckConnection())
                return;

            //new CanMessage("07E0", "0210610000000000").Transmit();
            //Thread.Sleep(10);
            //new CanMessage("07E0", "0427010000000000").Transmit();
            //Thread.Sleep(10);
            //new CanMessage("07E0", "0427020000000000").Transmit();
        }

        /// <summary>
        /// Transmit an ECUReset Uds message
        /// </summary>
        /// <param name="sender">A reference to the tsbECUReset instance.</param>
        /// <param name="e">A reference to the Click event's arguments.</param>
        private void tsbECUReset_Click(object sender, EventArgs e)

        {
            if (!ConnectionUtil.CheckConnection())
                return;

            ECUReset = new ECUReset();
            ECUReset.Transmit();
        }

        /// <summary>
        /// Clears data on the monitor screen
        /// </summary>
        /// <param name="sender">Clear Button</param>
        /// <param name="e">Params</param>
        private void btnClear_Click(object sender, EventArgs e)
        {
            if (ASContext.Configuration == null)
                return;
            else
                tspFilterTxb.Enabled = true;

            if (dockMonitor.Documents.ElementAt(0) is FormMonitorGenericInput genericInput)
            {
                genericInput.ClearPreviousConfiguration();
                genericInput.LoadConfiguration(ASContext.Configuration);
                if (tsbSession.Text != "Session: N/A")
                {
                    genericInput.SessionFiltering();
                }
                //((FormMonitorGenericOutput)dockMonitor.Documents.ElementAt(1)).LoadConfiguration(Configuration);
            }
            //else if (dockMonitor.Documents.ElementAt(0) is FormMonitorEnvInput envInput)
            //{
            //    envInput.LoadConfiguration(Configuration);
            //    ((FormMonitorEnvOutput)dockMonitor.Documents.ElementAt(1)).LoadConfiguration(Configuration);
            //    ucCycleBar.Clear();
            //}

            SetCounter(-MessagesTransmitted, -MessagesReceived);
        }

        /// <summary>
        /// Sends a UDS Message and captures the response for the embedded software version. 
        /// </summary>
        /// <param name="sender">Show Embedded Software Button</param>
        /// <param name="e">Params</param>
        private void btnShowEmbSwVer_Click(object sender, EventArgs e)
        {
            if (!ConnectionUtil.CheckConnection())
                return;

            if (ASContext.CurrentSession == null)
            {
                Helper.ShowWarningMessageBox("A session must be active.");
                return;
            }
               

            var cInf = ASContext.Configuration.Controls.FirstOrDefault(c => c.Name == "Vestel_Internal_Software_Version");
            if (cInf == null)
                return;
            cInf.Transmit(ServiceInfo.ReadDataByIdentifier);
        }

        /// <summary>
        /// Opens a Trace Popup dialog.
        /// </summary>
        /// <param name="sender">Trace Dialog Button</param>
        /// <param name="e">Params</param>
        private void traceDialogtsmi_Click(object sender, EventArgs e)
        {
            if (tracePopup == null || tracePopup.IsDisposed)
                tracePopup = new FormTracePopup();

            tracePopup.Show();
        }

        /// <summary>
        /// Handles the event when the client size of the main form changes, refreshing the tab control.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        private void FormMain_ClientSizeChanged(object sender, EventArgs e)
        {
            tabControl1.Refresh();
        }

        /// <summary>
        /// Handles the event when the text in the filter text box changes, filtering the items in the active document of the dock monitor.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        private void tspFilterTxb_TextChanged(object sender, EventArgs e)
        {
            var document = dockMonitor.ActiveDocument;

            if (document is IPeriodicTest formInput)
            {
                //if (!formInput.CanBeRun())
                //{
                //    return;
                //}

                formInput.FilterUCItems(tspFilterTxb.Text);
            }
            else if (document is IClickTest formOutput)
            {
                if (!formOutput.CanBeRun())
                {
                    return;
                }

                formOutput.FilterUCItems(tspFilterTxb.Text);
            }
            else if (document is FormDTCPanel formDTCPanel)
                formDTCPanel.FilterItems(tspFilterTxb.Text);

            tabControl1.Refresh();
        }

        /// <summary>
        /// Handles the event when the active document in the dock monitor changes, triggering the filter text box's TextChanged event if it's enabled and not empty.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        private void dockMonitor_ActiveDocumentChanged(object sender, EventArgs e)
        {
            if (tspFilterTxb.Enabled)
                tspFilterTxb_TextChanged(sender, e);
        }



        #endregion

        #region File menu events

        /// <summary>
        /// Opens a new non-modal form.
        /// </summary>
        /// <param name="sender">button</param>
        /// <param name="e">argument</param>
        private void newTsmi_Click(object sender, EventArgs e)
        {
            var f = new Form() { MdiParent = this };
            f.Show();
        }
        private void tsmiTransmit_Click(object sender, EventArgs e)
        {
            var f = new FormTransmit();
            f.Show();
        }

        /// <summary>
        /// Displays file selection form and opens selected file.
        /// </summary>
        /// <param name="sender">button</param>
        /// <param name="e">argument</param>
        private void openTsmi_Click(object sender, EventArgs e)
        {
            //#region File selection

            //var openFileDialog = new OpenFileDialog()
            //{
            //    Title = "Open any file",
            //    Filter = "Xml files(*.xml)| *.xml",
            //    Multiselect = false
            //};

            //if (openFileDialog.ShowDialog() != DialogResult.OK)
            //    return;
            //var fileName = openFileDialog.FileName;
            //#endregion

            //ParseMessages(fileName);

            //// add to recent file list
            //recentToolFileHelper.AddToRecentFiles(fileName);    // menu will be updated
            LoadXMLDoc();
        }

        /// <summary>
        /// Parse XML File to DataGridView
        /// </summary>
        /// <param string="filePath"></param>
        internal void ParseMessages(string filePath)
        {
            ImportMessages(filePath);

            string fileName = Path.GetFileName(filePath);
            LoadFile(fileName);
        }

        /// <summary>
        /// Not used.
        /// </summary>
        /// <param name="sender">button</param>
        /// <param name="e">argument</param>
        /// 
        //private void saveTsmi_Click(object sender, EventArgs e)
        //{
        //    var mostRecentFilePath = recentToolFileHelper.GetMostRecentFile();

        //    if (mostRecentFilePath != null)
        //    {
        //        ExportMessages(mostRecentFilePath);
        //    }
        //    else
        //    {
        //        #region File selection
        //        var saveFileDialog = new SaveFileDialog()
        //        {
        //            Filter = "XML file save|.xml",
        //        };
        //        if (saveFileDialog.ShowDialog() != DialogResult.OK)
        //            return;

        //        var fileName = saveFileDialog.FileName;
        //        #endregion

        //        ExportMessages(fileName);

        //        // add to recent file list
        //        recentToolFileHelper.AddToRecentFiles(fileName);    // menu will be updated
        //    }

        //}

        /// <summary>
        /// Displays file selection form and adds selected form to recent file list.
        /// </summary>
        /// <param name="sender">button</param>
        /// <param name="e">argument</param>
        //private void saveAsTsmi_Click(object sender, EventArgs e)
        //{
        //    #region File selection
        //    var saveFileDialog = new SaveFileDialog()
        //    {
        //        Filter = "XML file save|.xml",
        //    };
        //    if (saveFileDialog.ShowDialog() != DialogResult.OK)
        //        return;

        //    var fileName = saveFileDialog.FileName;
        //    #endregion

        //    ExportMessages(fileName);

        //    // add to recent file list
        //    recentToolFileHelper.AddToRecentFiles(fileName);    // menu will be updated
        //}

        /// <summary>
        /// Displays file selection form and loads selected file.
        /// </summary>
        /// <param name="sender">button</param>
        /// <param name="e">argument</param>
        private void loadFileTsmi_Click(object sender, EventArgs e)
        {
            #region File selection
            var openFileDialog = new OpenFileDialog()
            {
                Filter = "All files (*.*)|*.*",
                Multiselect = false
            };
            if (openFileDialog.ShowDialog() != DialogResult.OK)
                return;
            var fileName = openFileDialog.FileName;
            #endregion

            LoadFile(fileName);
        }

        #endregion

        #region Tool menu events

        /// <summary>
        /// Displays options dialog.
        /// </summary>
        /// <param name="sender">button</param>
        /// <param name="e">argument</param>
        private void optionsTsmi_Click(object sender, EventArgs e)
        {
            var f = new FormOptions();
            f.ShowDialog(); // modal dialog
        }

        /// <summary>
        /// Displays environmental test dialog.
        /// </summary>
        /// <param name="sender">button</param>
        /// <param name="e">argument</param>
        private void environmentalTestTsmi_Click(object sender, EventArgs e)
        {
            if (CheckConfigurationFile())
            {
                if (formEnvironmentalTest == null || formEnvironmentalTest.IsDisposed)
                {
                    formEnvironmentalTest = new FormEnvironmentalTest();
                    Receivers.Add(formEnvironmentalTest);
                    formEnvironmentalTest.Show();
                }
                else
                    formEnvironmentalTest.BringToFront();
            }

        }

        #endregion

        #region Help menu events

        /// <summary>
        /// Displays user guide.
        /// </summary>
        /// <param name="sender">button</param>
        /// <param name="e">argument</param>
        private void userGuideTsmi_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(@".\DiagBox-AutosarBCM-UserManual.pdf");
            }
            catch (Exception ex)
            {
                Helper.ShowErrorMessageBox(ex, "Unable to show user guide");
            }
        }

        /// <summary>
        /// Displays about form.
        /// </summary>
        /// <param name="sender">button</param>
        /// <param name="e">argument</param>
        private void aboutTsmi_Click(object sender, EventArgs e)
        {
            var f = new FormAbout();
            f.ShowDialog(); // modal dialog
        }

        private void LoadSessions()
        {
            tsbSession.DropDownItems.Clear();
            foreach (var session in ASContext.Configuration.Sessions)
                tsbSession.DropDownItems.Add(new ToolStripMenuItem(session.Name, null, new EventHandler(tsbSession_Click)) { Tag = session });
        }
        public void CheckSession()
        {
            if (dockMonitor.ActiveDocument is IPeriodicTest formInput)
                formInput.SessionFiltering();
            formDTCPanel.Session_Changed();
        }

        private void tsbSession_Click(object sender, EventArgs e)
        {
            FormMain.ControlChecker = false;
            if (!ConnectionUtil.CheckConnection())
                return;
            var sessionInfo = (sender as ToolStripMenuItem).Tag as SessionInfo;
            new DiagnosticSessionControl().Transmit(sessionInfo);
            ASContext.CurrentSession = sessionInfo;
            tsbSession.Text = $"Session: {sessionInfo.Name}";
            CheckSession();
        }

        private void tsbToggle_Click(object sender, EventArgs e)
        {
            formMonitorGenericInput.ToggleSidebar();
        }

        #endregion

        private void tsmiEMCView_Click(object sender, EventArgs e)
        {
            if (CheckConfigurationFile())
            {
                if (formEMCView == null || formEMCView.IsDisposed)
                {
                    formEMCView = new FormEMCView();
                    formEMCView.ShowDialog();
                }
                else
                    formEMCView.BringToFront();
            }
        }
        internal void UpdateSessionLabel()
        {

            if (tsbSession.GetCurrentParent().InvokeRequired)
                tsbSession.GetCurrentParent().Invoke(new Action(() => tsbSession.Text = $"Session: {ASContext.CurrentSession.Name}"));
            else tsbSession.Text = $"Session: {ASContext.CurrentSession.Name}";

        }
    }
}