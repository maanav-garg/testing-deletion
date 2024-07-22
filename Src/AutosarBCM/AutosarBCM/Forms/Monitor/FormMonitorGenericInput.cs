using AutosarBCM.UserControls.Monitor;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using AutosarBCM.Config;
using AutosarBCM.Core.Config;
using AutosarBCM.Core;
using System.Net;
using AutosarBCM.Properties;
using System.IO;

namespace AutosarBCM.Forms.Monitor
{
    /// <summary>
    /// Implements the FormMonitorGenericInput form.
    /// </summary>
    public partial class FormMonitorGenericInput : DockContent, IPeriodicTest, IReadDataByIdenReceiver
    {
        #region Variables

        /// <summary>
        /// Configuration settings for the monitor.
        /// </summary>
        private AutosarBcmConfiguration monitorConfig;

        /// <summary>
        /// Maps SID codes to response messages.
        /// </summary>
        private Dictionary<byte, string> sidResponseMessageDict = Enum.GetValues(typeof(SIDDescription)).Cast<SIDDescription>().ToDictionary(t => (byte)t, t => t.ToString());

        /// <summary>
        /// Maps NRC codes to response messages.
        /// </summary>
        private Dictionary<byte, string> nrcResponseMessageDict = Enum.GetValues(typeof(NRCDescription)).Cast<NRCDescription>().ToDictionary(t => (byte)t, t => t.ToString());

        /// <summary>
        /// List of read-only input items.
        /// </summary>
        private List<InputMonitorItem> inputMonitorItems = new List<InputMonitorItem>();

        private List<UCItem> uCItems = new List<UCItem>();

        SortedDictionary<string, List<UCItem>> groups = new SortedDictionary<string, List<UCItem>>();

        /// <summary>
        /// A CancellationTokenSource for managing cancellation of asynchronous operations.
        /// </summary>
        private CancellationTokenSource cancellationTokenSource;

        /// <summary>
        /// Determines whether a test is running or not.
        /// </summary>
        internal static bool IsTestRunning = false;

        /// <summary>
        /// Maps item types to visibility states for upper, lower, and coefficient labels.
        /// </summary>
        private readonly Dictionary<string, (bool Upper, bool Lower, bool Coefficient)> visibilitySettings = new Dictionary<string, (bool, bool, bool)>
        {
            { "Digital", (false, false, false) },
            { "Analog", (true, true, true) },
            { "Resistive", (true, true, true) },
            { "Frequency", (true, true, false) }
        };

        /// <summary>
        /// Determines how many output test clicked.
        /// </summary>
        public static int TestClickCounter { get { return testClickCounter; } set { testClickCounter = value < 0 ? 0 : value; } }
        public static int testClickCounter = 0;

        #endregion

        #region Constructor

        /// <summary>
        /// The constructor
        /// </summary>
        public FormMonitorGenericInput()
        {
            InitializeComponent();
            pnlMonitorInput.HorizontalScroll.Maximum = 0;
            pnlMonitorInput.AutoScroll = true;
            typeof(FlowLayoutPanel).InvokeMember("DoubleBuffered", BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
                null, pnlMonitorInput, new object[] { true });

            splitContainer1.Panel2Collapsed = true;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Load page with configuration
        /// </summary>
        /// <param name="config">Monitor config object</param>
        public void LoadConfiguration(ConfigurationInfo config)
        {
            ClearPreviousConfiguration();
            ASContext.Configuration = config;
            pnlMonitorInput.Controls.Clear();
            if (!groups.ContainsKey("Other"))
            {
                groups["Other"] = new List<UCItem>();
            }
            foreach (var ctrl in config.Controls)
            {
                var ucItem = new UCItem(ctrl);
                uCItems.Add(ucItem);
                ucItem.Click += UcItem_Click;
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
                pnlMonitorInput.Controls.Add(label);

                flowPanelGroup.Paint += pnlMonitorInput_Paint;

                foreach (var ucItem in group.Value)
                {
                    flowPanelGroup.Controls.Add(ucItem);
                }

                pnlMonitorInput.Controls.Add(flowPanelGroup);
            }

            splitContainer1.Panel2Collapsed = false;
        }

        /// <summary>
        /// Change the item status
        /// </summary>
        /// <param name="receivedData">Data comes from device</param>
        public bool ChangeStatus(byte[] receivedData, MessageDirection messageDirection)
        {
            var response = new GenericResponse(receivedData, monitorConfig.GenericMonitorConfiguration.InputSection.CommonConfig.InputRegisterGroupOffset, monitorConfig.GenericMonitorConfiguration.InputSection.CommonConfig.InputRegisterGroupLength);

            if (messageDirection == MessageDirection.RX && !CheckResponse(response.SID, receivedData[6]))
                return true;

            InputMonitorItem item = null;

            if (response.RegisterGroup == (short)Output_ReadGroup.FS26)
                item = inputMonitorItems.FirstOrDefault(i => i.RegisterGroup == response.RegisterGroup.ToString("x4"));
            else if (response.RegisterGroup == (short)Set_Group.SET_STATUS)
                item = inputMonitorItems.FirstOrDefault(i => i.RegisterAddress == response.RegisterAddress.ToString("X2"));
            else
                item = inputMonitorItems.FirstOrDefault(i => i.RegisterGroup == response.RegisterGroup.ToString("x4")
                                                    && i.RegisterAddress == response.RegisterAddress.ToString("X2"));

            if (item == null)
                return false;

            var uc = FindUIControl(item.Name, item.RegisterAddress);
            if (uc == null)
                return false;

            //((UCItem)uc).ChangeStatus(item, response, messageDirection);
            return true;
        }

        /// <summary>
        /// Starts a new test periodically
        /// </summary>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the task</param>
        public void StartTest(CancellationToken cancellationToken)
        {
            MonitorUtil.RunTestPeriodically(cancellationToken, MonitorTestType.Generic);
        }
        /// <summary>
        /// Clear Previous ASConfiguration
        /// </summary>
        public void ClearPreviousConfiguration()
        {
            pnlMonitorInput.Controls.Clear();
            groups.Clear();
            uCItems.Clear();
        }

        /// <summary>
        /// Gets whether this test is runnable or not.
        /// </summary>
        /// <returns>true if this test is runnable; otherwise, false.</returns>
        public bool CanBeRun()
        {
            return ASContext.Configuration != null;
        }

        /// <summary>
        /// Filters the user control items based on the specified filter.
        /// </summary>
        /// <param name="filter">The string used to filter the items.</param>
        public void FilterUCItems(string filter)
        {
            foreach (FlowLayoutPanel flowPanel in pnlMonitorInput.Controls.OfType<FlowLayoutPanel>())
            {
                var labelIndex = pnlMonitorInput.Controls.IndexOf(flowPanel) - 1;
                if (labelIndex >= 0 && pnlMonitorInput.Controls[labelIndex] is Label flowLabel)
                {
                    bool isLabelMatched = flowLabel.Text.IndexOf(filter, StringComparison.OrdinalIgnoreCase) >= 0;

                    if (isLabelMatched)
                    {
                        flowLabel.Visible = true;
                        foreach (var uc in flowPanel.Controls)
                        {
                            if (uc is UCItem ucItem)
                            {
                                ucItem.Visible = true;
                            }
                        }
                    }
                    else
                    {
                        bool anyUcItemVisible = false;
                        foreach (var uc in flowPanel.Controls)
                        {
                            if (uc is UCItem ucItem)
                            {
                                bool titleMatch = ucItem.ControlInfo.Name.IndexOf(filter, StringComparison.OrdinalIgnoreCase) >= 0;
                                bool listItemMatch = ucItem.GetListBoxItems().Any(item => item.IndexOf(filter, StringComparison.OrdinalIgnoreCase) >= 0);

                                ucItem.Visible = titleMatch || listItemMatch;
                                anyUcItemVisible |= ucItem.Visible;
                            }
                        }
                        flowLabel.Visible = anyUcItemVisible;
                    }
                }
            }
        }

        /// <summary>
        /// Filters and updates the state of controls based on the current session's available services and exceptions.
        /// </summary>
        public void SessionFiltering()
        {
            foreach (Control control in pnlMonitorInput.Controls)
            {
                if (control is FlowLayoutPanel flowPanel)
                {
                    foreach (UCItem ucItem in flowPanel.Controls.OfType<UCItem>())
                    {
                        bool defaultSessionMatch = ucItem.ControlInfo.Services.Any(service => ASContext.CurrentSession.AvailableServices.Contains(service));
                        bool activeExceptionMatch = ucItem.ControlInfo.SessionActiveException.Any(exception => exception == ASContext.CurrentSession.ID);
                        bool inactiveExceptionMatch = ucItem.ControlInfo.SessionInactiveException.Any(exception => exception == ASContext.CurrentSession.ID);

                        if (ucItem.InvokeRequired)
                        {
                            ucItem.BeginInvoke((MethodInvoker)delegate ()
                            {
                                ucItem.Enabled = (defaultSessionMatch || activeExceptionMatch) && !inactiveExceptionMatch;
                            });
                        }
                        else
                        {
                            ucItem.Enabled = (defaultSessionMatch || activeExceptionMatch) && !inactiveExceptionMatch;
                        }
                    }

                }
            }
        }
        public void DisabledAllSession()
        {
            Helper.ShowWarningMessageBox("Session failed to activate, try again.");
            foreach (Control control in pnlMonitorInput.Controls)
            {
                if (control is FlowLayoutPanel flowPanel)
                {
                    foreach (UCItem ucItem in flowPanel.Controls.OfType<UCItem>())
                    {

                        ucItem.BeginInvoke(new Action(() =>
                        {
                            ucItem.Enabled = false;
                        }));
                    }
                }
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// An event handler to the btnStart's Click event.
        /// </summary>
        /// <param name="sender">A reference to the btnStart instance.</param>
        /// <param name="e">A reference to the Click event's arguments.</param>
        private void btnStartInput_Click(object sender, EventArgs e)
        {
            if (!ConnectionUtil.CheckConnection())
                return;
            if (IsTestRunning)
            {
                cancellationTokenSource.Cancel();
                TestClickCounter = 0;
            }
            else
            {
                if (!CanBeRun())
                {
                    Helper.ShowWarningMessageBox("Please, load the configuration file first.");
                    return;
                }
                cancellationTokenSource = new CancellationTokenSource();
                StartTest(cancellationTokenSource.Token);
            }

            IsTestRunning = !IsTestRunning;
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
        /// Checks and formats a response based on SID and NRC values.
        /// </summary>
        /// <param name="SID">The Service Identifier.</param>
        /// <param name="NRC">The Negative Response Code.</param>
        /// <returns>Returns true if the response is valid, otherwise false.</returns>
        private bool CheckResponse(byte SID, byte NRC)
        {
            var response = string.Empty;
            if (sidResponseMessageDict.TryGetValue(SID, out string sidResponse))
            {
                response += sidResponse + " ";

                if (nrcResponseMessageDict.TryGetValue(NRC, out string nrcResponse))
                    response += nrcResponse;
            }

            if (!string.IsNullOrWhiteSpace(response))
            {
                Program.MainForm.AppendTrace($"{response}");
                return false;
            }
            return true;
        }

        /// <summary>
        /// Updates the status strip data
        /// </summary>
        /// <param name="sender">User control</param>
        /// <param name="e">Event args</param>
        private void UcItem_Click(object sender, EventArgs e)
        {
            var uc = (UCItem)sender;
            uc.Focus();
            //UpdateLabelVisibility(uc.Item.ItemType);

            ucControlByIdentifierItem.UpdateSidebar(uc);
        }

        /// <summary>
        /// Sets label visibility based on the item type, defaulting to visible if not specified.
        /// </summary>
        /// <param name="itemType">Item type to determine label visibility.</param>
        private void UpdateLabelVisibility(string itemType)
        {
            if (visibilitySettings.TryGetValue(itemType, out var settings))
            {
                tsUpperLimitLbl.Visible = settings.Upper;
                lblUpperLimit.Visible = settings.Upper;
                tsLowerLimitLbl.Visible = settings.Lower;
                lblLowerLimit.Visible = settings.Lower;
                tsCoefficientLbl.Visible = settings.Coefficient;
                lblCoefficient.Visible = settings.Coefficient;
            }
            else
            {
                tsUpperLimitLbl.Visible = true;
                lblUpperLimit.Visible = true;
                tsLowerLimitLbl.Visible = true;
                lblLowerLimit.Visible = true;
                tsCoefficientLbl.Visible = true;
                lblCoefficient.Visible = true;
            }
        }

        /// <summary>
        /// Finds a user interface control based on its name and register address.
        /// </summary>
        /// <param name="name">The name of the control to find.</param>
        /// <param name="registerAddress">The register address associated with the control.</param>
        /// <returns>The found control, or null if no control is found.</returns>
        private Control FindUIControl(string name, string registerAddress)
        {
            return pnlMonitorInput.Controls.Find($"uc_{name}_{registerAddress}", true).First();
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

        public bool Receive(Service baseService)
        {
            var service = baseService as ReadDataByIdenService;
            if (service != null)
            {
                foreach (var ucItem in uCItems)
                    if (ucItem.ControlInfo.Address == service.ControlInfo.Address)
                    {
                        if (service.ControlInfo.Name == "Vestel_Internal_Software_Version" && FormMain.isSwVerClicked)
                            break;
                        ucItem.ChangeStatus(service);
                        return true;
                    }
            }
            return false;
        }


        /// <summary>
        /// Handle transmitted data.
        /// </summary>
        public bool Sent(ushort address)
        {
            foreach (var ucItem in uCItems)
                if (ucItem.ControlInfo.Address == address && !FormMain.isSwVerClicked)
                {
                    ucItem.HandleMetrics();
                    return true;
                }
            return false;
        }

        internal void ToggleSidebar()
        {
            splitContainer1.Panel2Collapsed = !splitContainer1.Panel2Collapsed;
        }

        #endregion
    }
}
