using AutosarBCM.Core;
using AutosarBCM.UserControls.Monitor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace AutosarBCM.Forms.Monitor
{
    public partial class FormEnvironmentalTest : Form, IPeriodicTest, IIOControlByIdenReceiver, IDTCReceiver, IReadDataByIdenReceiver
    {

        #region Variables
        private SortedDictionary<string, List<UCReadOnlyItem>> groups = new SortedDictionary<string, List<UCReadOnlyItem>>();
        private List<UCReadOnlyItem> ucItems = new List<UCReadOnlyItem>();
        private Dictionary<string, Core.ControlInfo> dtcList = new Dictionary<string, Core.ControlInfo>();
        private Dictionary<int, Core.Cycle> cycles;
        private List<Mapping> mappingData;
        private List<Function> continuousReadData;
        /// <summary>
        /// A CancellationTokenSource for managing cancellation of asynchronous operations.
        /// </summary>
        private CancellationTokenSource cancellationTokenSource;

        int timeSec, timeMin, timeHour;
        bool isActive;
        #endregion

        #region Constructor
        public FormEnvironmentalTest()
        {
            InitializeComponent();
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

            cycles = MonitorUtil.GetCycleDict(ASContext.Configuration.EnvironmentalTest.Cycles);
            mappingData = (ASContext.Configuration.EnvironmentalTest.ConnectionMappings);
            continuousReadData = (ASContext.Configuration.EnvironmentalTest.ContinousReadList);

            groups.Add("DID", new List<UCReadOnlyItem>());
            foreach (var ctrl in ASContext.Configuration.Controls.Where(c => c.Group == "DID"))
            {
                foreach (var payload in ctrl.Responses[0].Payloads)
                {
                    var ucItem = new UCReadOnlyItem(ctrl, payload);
                    ucItems.Add(ucItem);

                    //DTC init
                    if (string.IsNullOrEmpty(payload.DTCCode))
                        continue;
                    dtcList[payload.DTCCode] = ctrl;
                }
                groups["DID"] = ucItems;
            }

            //Generate UI
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
            if (FormMain.IsTestRunning)
            {
                cancellationTokenSource.Cancel();
            }
            else //Start Test
            {
                cancellationTokenSource = new CancellationTokenSource();
                StartTest(cancellationTokenSource.Token);
            }

            FormMain.IsTestRunning = !FormMain.IsTestRunning;
            if (FormMain.IsTestRunning)
            {
                ResetTime();
                isActive = true;
                btnStart.Text = "Stop";
                btnStart.ForeColor = Color.Red;
            }
            else
            {
                isActive = false;
                btnStart.Text = "Start";
                btnStart.ForeColor = Color.Green;
            }
        }

        public void StartTest(CancellationToken cancellationToken)
        {
            MonitorUtil.RunTestPeriodically(cancellationToken, MonitorTestType.Environmental);
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

                        ucItem.Visible = titleMatch;
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
        private int totalMessagesReceived = 0;
        private int totalMessagesTransmitted = 0;

        public bool Receive(Service baseService)
        {
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
                
            return false;
        }


        /// <summary>
        /// Handles received IOControlByIdentifierService type of data.
        /// </summary>
        private bool HandleIOControlByIdentifierReceive(IOControlByIdentifierService ioService)
        {
            int loopVal;
            if (!int.TryParse(lblLoopVal.Text, out loopVal) || !cycles.ContainsKey(loopVal))
            {
                return false;
            }

            var cycle = cycles[loopVal];
            UCReadOnlyItem matchedControl = null;
            for (var i = 0; i < ioService.Payloads.Count; i++)
            {
                if (cycle.Functions.SelectMany(p => p.Payloads).Any(x => x == ioService.Payloads[i].PayloadInfo.Name))
                    Helper.WriteCycleMessageToLogFile(ioService.ControlInfo.Name, ioService.Payloads[i].PayloadInfo.Name, Constants.Response, "", "", ioService.Payloads[i].FormattedValue);

                matchedControl = ucItems.FirstOrDefault(c => c.PayloadInfo.Name == ioService.Payloads[i].PayloadInfo.Name);
                if (matchedControl == null)
                    return false;
            }
            matchedControl.ChangeStatus(ioService);
            totalMessagesReceived++;
            UpdateCounters();
            return true;
        }

        /// <summary>
        /// Handles received ReadDTCInformationService type of data.
        /// </summary>
        private void HandleReadDTCInformationReceive(ReadDTCInformationService dtcService)
        {
            foreach (var dtcValue in dtcService.Values)
            {
                if (dtcValue.Mask != 80 || !dtcList.ContainsKey(dtcValue.Code))
                { 
                    continue; 
                }

                var control = dtcList[dtcValue.Code];
                var payload = control.Responses?[0].Payloads.First(p => p.DTCCode == dtcValue.Code);
                
                if (payload == null)
                    continue;
                var uc = ucItems.First(c => c.PayloadInfo.Name == payload.Name);
                uc?.ChangeDtc(dtcValue.Description);
            }
        }

        /// <summary>
        /// Handles received ReadDataByIdenService type of data.
        /// </summary>
        private bool HandleReadDataByIdenService(ReadDataByIdenService readByIdenService)
        {
            int loopVal;
            if (!int.TryParse(lblLoopVal.Text, out loopVal) || !cycles.ContainsKey(loopVal))
            { 
                return false; 
            }

            var cycle = cycles[loopVal];
            UCReadOnlyItem matchedControl = null;

            for (var i = 0; i < readByIdenService.Payloads.Count; i++)
            {
                if (cycle.Functions.SelectMany(p => p.Payloads).Any(x => x == readByIdenService.Payloads[i].PayloadInfo.Name))
                {
                    Helper.WriteCycleMessageToLogFile(readByIdenService.ControlInfo.Name, readByIdenService.Payloads[i].PayloadInfo.Name, Constants.Response, "", "", readByIdenService.Payloads[i].FormattedValue);
                }
                    
                if (mappingData.Any(p => p.Input.Name == readByIdenService.Payloads[i].PayloadInfo.Name)) 
                { 
                    Helper.WriteCycleMessageToLogFile(readByIdenService.ControlInfo.Name, readByIdenService.Payloads[i].PayloadInfo.Name, Constants.Response, "", "", readByIdenService.Payloads[i].FormattedValue); 
                }
                    
                if (continuousReadData.Any(p => p.Name == readByIdenService.Payloads[i].PayloadInfo.Name))
                {
                    Helper.WriteCycleMessageToLogFile(readByIdenService.ControlInfo.Name, readByIdenService.Payloads[i].PayloadInfo.Name, Constants.ContinuousReadResponse, "", "", readByIdenService.Payloads[i].FormattedValue);
                }
                    

                matchedControl = ucItems.FirstOrDefault(c => c.PayloadInfo.Name == readByIdenService.Payloads[i].PayloadInfo.Name);
                if (matchedControl == null)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Updates TX/RX counters on UI
        /// </summary>
        private void UpdateCounters()
        {
            tslTransmitted.GetCurrentParent().Invoke((MethodInvoker)delegate ()
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

        /// <summary>
        /// Handle transmitted data.
        /// </summary>
        /// <param name="sender">Form</param>
        /// <param name="e">Argument</param>
        public bool Sent(short address)
        {
            if (!int.TryParse(lblLoopVal.Text, out int loopVal))
                return false;

            if (!cycles.ContainsKey(loopVal))
                return false;

            var cycle = cycles[loopVal];

            var matchedControls = ucItems.Where(c => c.ControlInfo.Address == address).ToList();
            if (!matchedControls.Any())
                return false;

            foreach (var uc in matchedControls)
            {
                foreach (var payload in cycle.Functions.SelectMany(p => p.Payloads))
                {
                    if (uc.PayloadInfo.Name == payload)
                    {
                        uc.HandleMetrics();
                        break;
                    }
                }
            }
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

        public void DisabledAllSession()
        {
            throw new NotImplementedException();
        }
    }
}
