using AutosarBCM.Core;
using AutosarBCM.UserControls.Monitor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutosarBCM.Forms.Monitor
{
    public partial class FormEnvironmentalTest : Form, IPeriodicTest
    {

        #region Variables
        private SortedDictionary<string, List<UCItem>> groups = new SortedDictionary<string, List<UCItem>>();
        private List<UCItem> uCItems = new List<UCItem>();
        /// <summary>
        /// A CancellationTokenSource for managing cancellation of asynchronous operations.
        /// </summary>
        private CancellationTokenSource cancellationTokenSource;

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

            groups.Add("Other", new List<UCItem>());
            foreach (var ctrl in ASContext.Configuration.Controls)
            {
                var ucItem = new UCItem(ctrl);
                uCItems.Add(ucItem);
                //ucItem.Click += UcItem_Click;
                ucItem.Enabled = false;
                string groupName = ctrl?.Group;
                if (!string.IsNullOrEmpty(groupName))
                {
                    if (!groups.ContainsKey(groupName))
                    {
                        groups.Add(groupName, new List<UCItem>());
                    }
                    groups[groupName].Add(ucItem);
                }
                else
                {
                    groups["Other"].Add(ucItem);
                }
            }
            foreach (var group in groups)
            {
                var flowPanelGroup = new FlowLayoutPanel { AutoSize = true, Margin = Padding = new Padding(3) };
                var label = new Label { Text = group.Key, AutoSize = true, Font = new Font(FontFamily.GenericSansSerif, 12, FontStyle.Bold) };
                pnlMonitor.Controls.Add(label);

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

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (FormMain.IsTestRunning)
            {
                cancellationTokenSource.Cancel();
            }
            else //Start Test
            {
                cancellationTokenSource = new CancellationTokenSource();
                //StartTest(cancellationTokenSource.Token);
            }

            FormMain.IsTestRunning = !FormMain.IsTestRunning;
            if (FormMain.IsTestRunning)
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
            //throw new NotImplementedException();
        }

        public void SessionFiltering()
        {
            throw new NotImplementedException();
        }
    }
}
