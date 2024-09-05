using AutosarBCM.Core;
using AutosarBCM.Core.Config;
using AutosarBCM.UserControls.Monitor;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace AutosarBCM.Forms.Monitor
{
    public partial class FormEnvironmentalTest : Form, IPeriodicTest, IWriteByIdenReceiver, IIOControlByIdenReceiver, IDTCReceiver, IReadDataByIdenReceiver
    {
        #region Variables

        private SortedDictionary<string, List<UCReadOnlyItem>> groups = new SortedDictionary<string, List<UCReadOnlyItem>>();
        internal List<UCReadOnlyItem> ucItems = new List<UCReadOnlyItem>();
        private Dictionary<string, ControlInfo> dtcList = new Dictionary<string, ControlInfo>();
        private Dictionary<int, Cycle> cycles;
        private List<Scenario> scenarios;
        private List<Mapping> mappingData;
        private List<Function> continuousReadData;
        private int cycleRange;
        IEnumerable<List<string>> openPayloadsOfScenario;
        IEnumerable<List<string>> closePayloadsOfScenario;
        public List<Config.SentMessage> sentMessagesList = new List<Config.SentMessage>();
        internal static List<Config.SentMessage> UnopenedControlList = new List<Config.SentMessage>();
        internal static List<Config.SentMessage> OpenedControlList = new List<Config.SentMessage>();
        private int totalMessagesReceived = 0;
        internal int totalMessagesTransmitted = 0;
        private int endCycleIndex;

        /// <summary>
        /// A CancellationTokenSource for managing cancellation of asynchronous operations.
        /// </summary>
        private CancellationTokenSource cancellationTokenSource;

        int timeSec, timeMin, timeHour;
        bool isActive;
        public static string configName;
        #endregion

        #region Constructor
        public FormEnvironmentalTest()
        {
            InitializeComponent();
            LoadConfigSelection();
            LoadControls();
        }
        #endregion

        #region Public Methods

        #endregion

        #region Private Methods

        private void LoadControls()
        {
            if (ASContext.Configuration == null)
            {
                Helper.ShowWarningMessageBox("No configuration file is imported. Please import the file first!");
                this.Close();
                return;
            }

            ResetTime();

            scenarios = ASContext.Configuration.EnvironmentalTest.Environments.First(e => e.Name == EnvironmentalTest.CurrentEnvironment).Scenarios;
            cycleRange = (int)ASContext.Configuration.EnvironmentalTest.Environments.First(e => e.Name == EnvironmentalTest.CurrentEnvironment).EnvironmentalConfig.CycleRange;
            cycles = MonitorUtil.GetCycleDict(ASContext.Configuration.EnvironmentalTest.Environments.First(x => x.Name == EnvironmentalTest.CurrentEnvironment).Cycles);
            scenarios = ASContext.Configuration.EnvironmentalTest.Environments.First(x => x.Name == EnvironmentalTest.CurrentEnvironment).Scenarios;
            mappingData = new List<Mapping>(ASContext.Configuration.EnvironmentalTest.ConnectionMappings);
            continuousReadData = (ASContext.Configuration.EnvironmentalTest.Environments.First(x => x.Name == EnvironmentalTest.CurrentEnvironment).ContinousReadList);
            endCycleIndex = ASContext.Configuration.EnvironmentalTest.Environments.First(x => x.Name == EnvironmentalTest.CurrentEnvironment).EnvironmentalConfig.EndCycleIndex;

            var payloadNamesInCycle = cycles.Values.SelectMany(x => x.OpenItems.SelectMany(a => a.Payloads).Concat(x.CloseItems.SelectMany(a => a.Payloads))).Distinct();

            var controlNamesInCycle = cycles.Values
                .SelectMany(x => x.Functions)
                .Select(a => a.Control).Where(s => s!= null).Distinct();

            var scenarioNamesInCycle = cycles.Values
                .SelectMany(x => x.Functions)
                    .Select(b => b.Scenario).Where(s => s != null).Distinct();

            var openPayloadsOfScenario = from scenario in scenarios
                                         join scenarioName in scenarioNamesInCycle on scenario.Name equals scenarioName
                                         select scenario.OpenPayloads;

            var closePayloadsOfScenario = from scenario in scenarios
                                          join scenarioName in scenarioNamesInCycle on scenario.Name equals scenarioName
                                          select scenario.ClosePayloads;

            groups.Add("DID", new List<UCReadOnlyItem>());

            foreach (var ctrl in ASContext.Configuration.Controls.Where(c => c.Group == "DID"))
            {
                foreach (var payload in ctrl.Responses[0].Payloads)
                {
                    if ((payloadNamesInCycle.Contains(payload.Name) && controlNamesInCycle.Contains(ctrl.Name))||
                        openPayloadsOfScenario.Any(innerList => innerList.Contains(payload.Name)) ||
                        closePayloadsOfScenario.Any(innerList => innerList.Contains(payload.Name)))
                    {
                        var ucItem = new UCReadOnlyItem(ctrl, payload);
                        ucItems.Add(ucItem);
                    }


                    if (string.IsNullOrEmpty(payload.DTCCode))
                        continue;
                    dtcList[payload.DTCCode] = ctrl;
                }
                groups["DID"] = ucItems;
            }

            foreach (var group in groups)
            {
                var flowPanelGroup = new FlowLayoutPanel { AutoSize = true, Margin = Padding = new Padding(3) };

                flowPanelGroup.Paint += pnlMonitorInput_Paint;

                foreach (var ucItem in group.Value)
                {
                    flowPanelGroup.Controls.Add(ucItem);
                }

                pnlMonitor.Controls.Add(flowPanelGroup);
            }
        }

        /// <summary>
        /// Changing border color of the flowpanel groups
        /// </summary>
        /// <param name="sender">Flowlayoutpanel to be painted.</param>
        /// <param name="e">PaintEventArgs of the sender.</param>
        private void pnlMonitorInput_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, ((FlowLayoutPanel)sender).ClientRectangle, Color.LightGray, ButtonBorderStyle.Solid);
        }

        /// <summary>
        /// Handles the form closing event
        /// </summary>
        /// <param name="sender">Form</param>
        /// <param name="e">Argument</param>
        private void FormEnvironmentalTest_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (FormMain.IsTestRunning && !Helper.ShowConfirmationMessageBox("There is an ongoing test. Do you want to proceed"))
            {
                e.Cancel = true;
            }
        }

        #endregion

        /// <summary>
        /// Handles cycle and loop value.
        /// </summary>
        /// <param name="sender">Form</param>
        /// <param name="e">Argument</param>
        internal void SetCounter(int cycleCounter, int loopCounter)
        {
            BeginInvoke(new Action(() =>
            {
                lblCycleVal.Text = cycleCounter.ToString();
                lblLoopVal.Text = loopCounter.ToString();
            }));
            Console.WriteLine(lblLoopVal.Text);

        }

        /// <summary>
        /// Handle time value to default value event.
        /// </summary>
        /// <param name="sender">Form</param>
        /// <param name="e">Argument</param>
        private void ResetTime()
        {
            timeSec = 0;
            timeMin = 0;
            timeHour = 0;
            isActive = false;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            FormMain mainForm = Application.OpenForms.OfType<FormMain>().FirstOrDefault();
            configName = tsbConfigurationSelection.Text;
            if (mainForm.tsbSession.Text != "Session: Extended Diagnostic Session")
            {
                Helper.ShowWarningMessageBox("Must be in Extended Diagnostic Session.");
                return;
            }
            if (FormMain.IsTestRunning)
            {
                if (!Helper.ShowConfirmationMessageBox("There is an ongoing test. Do you want to proceed"))
                {
                    return;
                }
                cancellationTokenSource.Cancel();
                btnStart.Enabled = false;


            }
            else //Start Test
            {
                FormMain.MonitorTestType = MonitorTestType.Environmental;
                cancellationTokenSource = new CancellationTokenSource();
                Task.Run(async () =>
                {
                    Helper.SendExtendedDiagSession();
                    mainForm.UpdateSessionLabel();

                    await Task.Delay(1000);
                });
                StartTest(cancellationTokenSource.Token);
                ResetTime();
                if (mainForm.dockMonitor.ActiveDocument is IPeriodicTest formInput)
                    formInput.SessionControlManagement(false);
            }
            SetStartBtnVisual();
        }
        public void SetStartBtnVisual()
        {

            BeginInvoke(new Action(() =>
            {
                if (FormMain.IsTestRunning)
                {
                    isActive = true;
                    btnStart.Text = "Stop";
                    btnStart.ForeColor = Color.Red;
                    tsbConfigurationSelection.Enabled = false;
                    chkDisableUi.Enabled = false;
                }
                else
                {
                    FormMain mainForm = Application.OpenForms.OfType<FormMain>().FirstOrDefault();
                    if (mainForm.dockMonitor.ActiveDocument is IPeriodicTest formInput)
                        formInput.SessionControlManagement(true);
                    btnStart.Enabled = true;
                    tsbConfigurationSelection.Enabled = true;
                    isActive = false;
                    btnStart.Text = "Start";
                    btnStart.ForeColor = Color.Green;
                    chkDisableUi.Enabled = true;
                }
            }));


        }
        public void StartTest(CancellationToken cancellationToken)
        {
            MonitorUtil.RunTestPeriodically(cancellationToken, MonitorTestType.Environmental);
            FormMain.IsTestRunning = !FormMain.IsTestRunning;
        }

        public bool CanBeRun()
        {
            throw new NotImplementedException();
        }

        public bool ChangeStatus(byte[] receivedData, MessageDirection messageDirection)
        {
            throw new NotImplementedException();
        }

        public void FilterUCItems(string filter)
        {
            pnlMonitor.SuspendLayout();
            foreach (FlowLayoutPanel flowPanel in pnlMonitor.Controls.OfType<FlowLayoutPanel>())
            {
                flowPanel.SuspendLayout();
                foreach (var uc in flowPanel.Controls)
                {
                    if (uc is UCReadOnlyItem ucItem)
                    {
                        bool titleMatch = ucItem.PayloadInfo.Name.IndexOf(filter, StringComparison.OrdinalIgnoreCase) >= 0
                            || ucItem.ControlInfo.Name.IndexOf(filter, StringComparison.OrdinalIgnoreCase) >= 0;

                        if (!chkDisableUi.Checked)
                        { 
                            ucItem.Visible = titleMatch; 
                        }
                    }
                }
                flowPanel.ResumeLayout();
            }
            pnlMonitor.ResumeLayout();
        }

        public void SessionFiltering()
        {
            throw new NotImplementedException();
        }

        public bool Receive(Service baseService)
        {
            if (!FormMain.IsTestRunning)
                return false;

            if (baseService is IOControlByIdentifierService ioService)
            {
                return HandleIOControlByIdentifierReceive(ioService);
            }
            else if (baseService is ReadDTCInformationService dtcService)
            {
                HandleReadDTCInformationReceive(dtcService);
            }
            else if (baseService is ReadDataByIdenService readByIdenService)
            {
                return HandleReadDataByIdenService(readByIdenService);
            }
            else if (baseService is WriteDataByIdentifierService writeByIdenService)
            {
                return HandleWriteDataByIdenService(writeByIdenService);
            }

            return false;
        }

        /// <summary>
        /// Handles received IOControlByIdentifierService type of data.
        /// </summary>
        private bool HandleIOControlByIdentifierReceive(IOControlByIdentifierService ioService)
        {
            CleanupSentMessages();

            var currentTime = DateTime.Now;
            var timeout = TimeSpan.FromSeconds(1);

            for (var i = 0; i < ioService.Payloads.Count; i++)
            {

                if (cancellationTokenSource != null && cancellationTokenSource.IsCancellationRequested && FormMain.IsTestRunning)
                    timeout = TimeSpan.FromSeconds(2);

                var itemsToRemove = new List<Config.SentMessage>();

                foreach (var sentMessage in sentMessagesList.ToList())
                {
                    if (currentTime - sentMessage.timestamp <= timeout)
                    {
                        if (sentMessage.itemType == ioService.Payloads[i].PayloadInfo.Name && sentMessage.itemName == ioService.ControlInfo.Name && !itemsToRemove.Contains(sentMessage))
                        {
                            ProcessSentMessage(ioService, ioService.Payloads[i], sentMessage, itemsToRemove);
                            break;
                        }
                    }
                }

                foreach (var item in itemsToRemove)
                    sentMessagesList.Remove(item);
            }

            UpdateCounters();
            return true;
        }

        /// <summary>
        /// Handles received HandleWriteDataByIdenService type of data.
        /// </summary>
        private bool HandleWriteDataByIdenService(WriteDataByIdentifierService ioService)
        {
            CleanupSentMessages();

            var currentTime = DateTime.Now;
            var timeout = TimeSpan.FromSeconds(1);

            for (var i = 0; i < ioService.Payloads.Count; i++)
            {

                if (cancellationTokenSource != null && cancellationTokenSource.IsCancellationRequested && FormMain.IsTestRunning)
                    timeout = TimeSpan.FromSeconds(2);

                var itemsToRemove = new List<Config.SentMessage>();

                foreach (var sentMessage in sentMessagesList.ToList())
                {
                    if (currentTime - sentMessage.timestamp <= timeout)
                    {
                        if (sentMessage.itemType == ioService.Payloads[i].PayloadInfo.Name && sentMessage.itemName == ioService.ControlInfo.Name && !itemsToRemove.Contains(sentMessage))
                        {
                            ProcessSentMessage(ioService, ioService.Payloads[i], sentMessage, itemsToRemove);
                            break;
                        }
                    }
                }

                foreach (var item in itemsToRemove)
                    sentMessagesList.Remove(item);
            }

            UpdateCounters();
            return true;
        }

        /// <summary>
        /// Processes a sent message, updates relevant lists, logs the message, and updates control status.
        /// </summary>
        /// <typeparam name="T">A type derived from the Service class.</typeparam>
        /// <param name="service">The service handling the message.</param>
        /// <param name="payload">The data associated with the message.</param>
        /// <param name="sentMessage">The configuration of the sent message.</param>
        /// <param name="itemsToRemove">List of messages to be removed after processing.</param>
        private void ProcessSentMessage<T>(T service, Payload payload, Config.SentMessage sentMessage, List<Config.SentMessage> itemsToRemove) where T : Service
        {
            var matchedControl = ucItems.FirstOrDefault(c => c.PayloadInfo.Name == payload.PayloadInfo.Name);
            if (matchedControl == null)
                return;

            if (!OpenedControlList.Any(u => u.itemType == sentMessage.itemType && u.itemName == sentMessage.itemName) && sentMessage.operation == Constants.Opened)
            {
                if (CheckValueIsOpened(payload))
                {
                    OpenedControlList.Add(sentMessage);
                    var fIndex = UnopenedControlList.FindIndex(u => u.itemType == sentMessage.itemType && u.itemName == sentMessage.itemName);
                    if(fIndex != -1)
                        UnopenedControlList.RemoveAt(fIndex);
                }
                else
                {
                    if (!UnopenedControlList.Any(u => u.itemType == sentMessage.itemType && u.itemName == sentMessage.itemName))
                        UnopenedControlList.Add(sentMessage);
                }
            }

            if (!chkDisableUi.Checked)
                totalMessagesReceived++;

            itemsToRemove.Add(sentMessage);
            Helper.WriteCycleMessageToLogFile(sentMessage.itemName, sentMessage.itemType, Constants.Response, "", "", payload.FormattedValue);

            if (service is IOControlByIdentifierService ioService)
            {
                matchedControl.ChangeStatus(ioService);
            }
            else if (service is WriteDataByIdentifierService writeByIdenService)
            {
                matchedControl.ChangeStatus(writeByIdenService);
            }   
        }

        /// <summary>
        /// Checks if the given payload is in an "opened" state based on its type and formatted value.
        /// </summary>
        /// <param name="payload">The payload to be checked.</param>
        /// <returns>
        /// Returns <c>true</c> if the payload is considered "opened" based on its type and value; 
        /// otherwise, <c>false</c>.
        /// </returns>
        public bool CheckValueIsOpened(Payload payload)
        {
            if (payload.FormattedValue == ASContext.Configuration.GetPayloadInfoByType(payload.PayloadInfo.TypeName).Values.FirstOrDefault(x => x.IsOpen == true)?.FormattedValue
                || (payload.PayloadInfo.TypeName == "DID_PWM" && payload.FormattedValue == ASContext.Configuration.EnvironmentalTest.Environments.First(x => x.Name == EnvironmentalTest.CurrentEnvironment).EnvironmentalConfig.PWMDutyOpenValue.ToString()) 
                || (payload.PayloadInfo.TypeName == "HexDump_1Byte" && payload.FormattedValue == ASContext.Configuration.EnvironmentalTest.Environments.First(x => x.Name == EnvironmentalTest.CurrentEnvironment).EnvironmentalConfig.HexDump1ByteOpenValue))
            {
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// Cleans up the sent messages dictionary by removing entries that have exceeded the specified time interval.
        /// </summary>
        internal void CleanupSentMessages()
        {
            var currentTime = DateTime.Now;
            var timeout = TimeSpan.FromSeconds(2);

            var messagesToRemove = new List<Config.SentMessage>();

            foreach (var message in sentMessagesList.ToList())
            {
                if (currentTime - message.timestamp > timeout)
                {
                    messagesToRemove.Add(message);
                }
            }

            foreach (var msg in messagesToRemove)
            {
                
                if (!OpenedControlList.Any(u => u.itemType == msg.itemType && u.itemName == msg.itemName) && msg.operation == Constants.Opened)
                {
                    if (!UnopenedControlList.Any(u => u.itemType == msg.itemType && u.itemName == msg.itemName) && msg.operation == Constants.Opened)
                        UnopenedControlList.Add(msg);
                }
                
                sentMessagesList.Remove(msg);
            }
                
        }

        /// <summary>
        /// Handles received ReadDTCInformationService type of data.
        /// </summary>
        private void HandleReadDTCInformationReceive(ReadDTCInformationService dtcService)
        {
            foreach (var dtcValue in dtcService.Values)
            {
                if (dtcValue.Mask != 0x0B || !dtcList.ContainsKey(dtcValue.Code))
                {
                    continue;
                }

                var control = dtcList[dtcValue.Code];
                var payload = control.Responses?[0].Payloads.First(p => p.DTCCode == dtcValue.Code);

                if (payload == null)
                    continue;
                var uc = ucItems.First(c => c.PayloadInfo.Name == payload.Name);
                if (uc != null && uc.CurrentDtcDescription != dtcValue.Description && !chkDisableUi.Checked)
                    uc?.ChangeDtc(dtcValue.Description);
            }
        }

        /// <summary>
        /// Handles received ReadDataByIdenService type of data.
        /// </summary>
        private bool HandleReadDataByIdenService(ReadDataByIdenService readByIdenService)
        {
            CleanupSentMessages();

            var timeout = TimeSpan.FromSeconds(1);
            var currentTime = DateTime.Now;
            var sentMessages = sentMessagesList.ToList();
            var inputName = mappingData.Where(m => sentMessages.Any(x => x.itemType == m.Output.Name));

            for (var i = 0; i < readByIdenService.Payloads.Count; i++)
            {
                var itemsToRemove = new List<Config.SentMessage>();
                foreach (var sentMessage in sentMessagesList.ToList())
                {
                    if (currentTime - sentMessage.timestamp <= timeout)
                    {

                        if (sentMessage.itemType == readByIdenService.Payloads[i].PayloadInfo.Name && sentMessage.itemName == readByIdenService.ControlInfo.Name && !itemsToRemove.Contains(sentMessage))
                        {
                            if (Program.MappingStateDict.TryGetValue(readByIdenService.Payloads[i].PayloadInfo.Name, out var errorLogDetect))
                            {
                                var mappingItem = inputName.FirstOrDefault(p => p.Input.Name == readByIdenService.Payloads[i].PayloadInfo.Name);
                                if (mappingItem != null)
                                {
                                    Program.MappingStateDict.UpdateValue(readByIdenService.Payloads[i].PayloadInfo.Name, errorLogDetect.UpdateInputResponse(MappingState.InputReceived, GetMappingResponse(readByIdenService.Payloads[i].Value)));

                                    if (errorLogDetect.CheckIsError())
                                        Helper.WriteErrorMessageToLogFile(readByIdenService.ControlInfo.Name, $"O: {mappingItem.Output.Control} ({mappingItem.Output.Name}) - I: {mappingItem.Input.Control} ({mappingItem.Input.Name})", Constants.MappingMismatch, "", "", $"Mapping Output: {string.Format("{0} = {1}", mappingItem.Output.Name, errorLogDetect.OutputResponse)} mismatched with Input: {string.Format("{0} = {1}", mappingItem.Input.Name, errorLogDetect.InputResponse)}");

                                    Program.MappingStateDict.Remove(readByIdenService.Payloads[i].PayloadInfo.Name);
                                }
                            }

                            Helper.WriteCycleMessageToLogFile(readByIdenService.ControlInfo.Name, readByIdenService.Payloads[i].PayloadInfo.Name, Constants.Response, "", "", readByIdenService.Payloads[i].FormattedValue);
                            if (!chkDisableUi.Checked)
                                totalMessagesReceived++;
                            itemsToRemove.Add(sentMessage);
                            break;
                        }
                    }
                }
                foreach (var item in itemsToRemove)
                    sentMessagesList.Remove(item);
            }
            return true;
        }

        private MappingResponse GetMappingResponse(byte[] value)
        {
            if (value[0] == 0) return MappingResponse.InputOff;
            else if (value[0] == 1) return MappingResponse.InputOn;
            else if (value[0] == 1) return MappingResponse.InputError;
            return MappingResponse.NOC;
        }

        /// <summary>
        /// Updates TX/RX counters on UI
        /// </summary>
        private void UpdateCounters()
        {
            if (tslTransmitted.GetCurrentParent().InvokeRequired && !chkDisableUi.Checked)
            {
                tslTransmitted.GetCurrentParent().BeginInvoke((MethodInvoker)delegate ()
                {
                    tslTransmitted.Text = totalMessagesTransmitted.ToString();
                });
                tslReceived.GetCurrentParent().Invoke((MethodInvoker)delegate ()
                {
                    tslReceived.Text = totalMessagesReceived.ToString();
                });
                tslDiff.GetCurrentParent().Invoke((MethodInvoker)delegate ()
                {
                    double diff = (double)totalMessagesReceived / totalMessagesTransmitted;
                    tslDiff.Text = (diff * 100).ToString("F2") + "%";
                    tslDiff.BackColor = diff == 1 ? Color.Green : (diff > 0.9 ? Color.Orange : Color.Red);
                });
            }
            else
            {
                if (!chkDisableUi.Checked)
                {
                    tslTransmitted.Text = totalMessagesTransmitted.ToString();
                    tslReceived.Text = totalMessagesReceived.ToString();
                    double diff = (double)totalMessagesReceived / totalMessagesTransmitted;
                    tslDiff.Text = (diff * 100).ToString("F2") + "%";
                    tslDiff.BackColor = diff == 1 ? Color.Green : (diff > 0.9 ? Color.Orange : Color.Red);
                }
            }
        }

        /// <summary>
        /// Handle transmitted data.
        /// </summary>
        /// <param name="sender">Form</param>
        /// <param name="e">Argument</param>
        public bool Sent(ushort address)
        {
            return true;
        }

        /// <summary>
        /// Timer starting event.
        /// </summary>
        /// <param name="sender">Form</param>
        /// <param name="e">Argument</param>
        private void timer_Tick(object sender, EventArgs e)
        {
            if (isActive)
            {
                timeSec++;
                if (timeSec >= 60)
                {
                    timeMin++;
                    timeSec = 0;
                    if (timeMin >= 60)
                    {
                        timeHour++;
                        timeMin = 0;
                    }
                }

            }
            DrawTime();
        }

        /// <summary>
        /// Handle timer values event.
        /// </summary>
        /// <param name="sender">Form</param>
        /// <param name="e">Argument</param>
        private void DrawTime()
        {
            lblSec.Text = String.Format("{0:00}", timeSec);
            lblMin.Text = String.Format("{0:00}", timeMin);
            lblHour.Text = String.Format("{0:00}", timeHour);
        }
        private void tspFilterTxb_TextChanged(object sender, EventArgs e)
        {
            FilterUCItems(tspFilterTxb.Text);
            pnlMonitor.Refresh();
        }
        private void LoadConfigSelection()
        {
            if (ASContext.Configuration == null)
                return;
            if (EnvironmentalTest.CurrentEnvironment == null)
            {
                var defaultEnvironment = ASContext.Configuration.EnvironmentalTest.Environments.First().Name;
                EnvironmentalTest.CurrentEnvironment = defaultEnvironment;
                tsbConfigurationSelection.Text = $"Configuration: {defaultEnvironment}";
            }

            tsbConfigurationSelection.DropDownItems.Clear();
            foreach (var environment in ASContext.Configuration.EnvironmentalTest.Environments)
                tsbConfigurationSelection.DropDownItems.Add(new ToolStripMenuItem(environment.Name, null, new EventHandler(tsbConfigurationSelection_Click)) { Tag = environment.Name });
        }
        private void tsbConfigurationSelection_Click(object sender, EventArgs e)
        {
            var environmentInfo = (sender as ToolStripMenuItem).Tag as string;
            if (EnvironmentalTest.CurrentEnvironment == environmentInfo)
            {
                Helper.ShowWarningMessageBox(environmentInfo + " configuration is already loaded.");
                return;
            }

            EnvironmentalTest.CurrentEnvironment = environmentInfo;
            tsbConfigurationSelection.Text = $"Configuration: {environmentInfo}";
            SuspendLayout();
            ReloadControls();
            ResumeLayout();
        }
        private void ReloadControls()
        {
            tsbConfigurationSelection.Enabled = false;
            groups.Clear();
            cycles.Clear();
            mappingData.Clear();
            continuousReadData.Clear();
            ucItems.Clear();
            dtcList.Clear();
            pnlMonitor.Controls.Clear();
            lblLoopVal.Text = lblCycleVal.Text = tslTransmitted.Text = tslReceived.Text = tslDiff.Text = "0";
            totalMessagesTransmitted = totalMessagesReceived = 0;
            LoadControls();
            tsbConfigurationSelection.Enabled = true;
        }

        public void SessionControlManagement(bool isActive)
        {
            throw new NotImplementedException();
        }
    }
}
