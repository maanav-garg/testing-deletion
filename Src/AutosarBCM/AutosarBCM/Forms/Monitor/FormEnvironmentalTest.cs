﻿using AutosarBCM.Core;
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
using System.Windows.Forms;

namespace AutosarBCM.Forms.Monitor
{
    public partial class FormEnvironmentalTest : Form, IPeriodicTest, IIOControlByIdenReceiver
    {

        #region Variables
        private SortedDictionary<string, List<UCReadOnlyItem>> groups = new SortedDictionary<string, List<UCReadOnlyItem>>();
        private List<UCReadOnlyItem> ucItems = new List<UCReadOnlyItem>();
        /// <summary>
        /// A CancellationTokenSource for managing cancellation of asynchronous operations.
        /// </summary>
        private CancellationTokenSource cancellationTokenSource;
        int timeCs, timeSec, timeMin, timeHour;
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

            groups.Add("DID", new List<UCReadOnlyItem>());
            foreach (var ctrl in ASContext.Configuration.Controls.Where(c => c.Group == "DID"))
            {
                foreach (var payload in ctrl.Responses[0].Payloads)
                {
                    var ucItem = new UCReadOnlyItem(ctrl, payload);
                    ucItems.Add(ucItem);
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
            timeCs = 0;
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
            var service = (IOControlByIdentifierService)baseService;
            if (service == null)
            {
                return false;
            }
            else
            {
                var items = groups["DID"];
                var matchedControls = items.Where(c => c.ControlInfo.Name == service.ControlInfo.Name);
                if (matchedControls == null)
                    return false;
                totalMessagesReceived++;
                foreach (var uc in matchedControls)
                {
                    uc.ChangeStatus(service);
                }
                UpdateCounters();
                return true;
            }

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
            foreach (var ucItem in ucItems)
                if (ucItem.ControlInfo.Address == address)
                {
                    ucItem.HandleMetrics();
                    totalMessagesTransmitted++;
                    return true;
                }
            return false;

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
                timeCs++;
                if (timeCs >= 100)
                {
                    timeSec++;
                    timeCs = 0;

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
            lblCs.Text = String.Format("{0:00}", timeCs);
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
