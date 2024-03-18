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

namespace AutosarBCM.Forms.Monitor
{
    /// <summary>
    /// Implements the FormMonitorGenericInput form.
    /// </summary>
    public partial class FormMonitorGenericInput : DockContent, IPeriodicTest
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
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Load page with configuration
        /// </summary>
        /// <param name="config">Monitor config object</param>
        public void LoadConfiguration(AutosarBcmConfiguration config)
        {
            if (monitorConfig != null)
                pnlMonitorInput.Controls.Clear();

            foreach (var group in config.GenericMonitorConfiguration.InputSection.Groups)
            {
                monitorConfig = config;
                pnlMonitorInput.Controls.Add(new Label { Font = new Font(Label.DefaultFont, FontStyle.Bold), Text = group.Name, AutoSize = true, Margin = new Padding(5) });

                var flowPanel = new FlowLayoutPanel { AutoSize = true, Margin = Padding = new Padding(3) };
                flowPanel.Paint += pnlMonitorInput_Paint;

                var items = group.InputItemList.OrderBy(x => x.Name).ToList();
                foreach (var item in items)
                {
                    var ucItem = new UCItem(item,monitorConfig.GenericMonitorConfiguration.InputSection.CommonConfig);
                    ucItem.MessageID = item.MessageIdOrDefault;
                    if (!string.IsNullOrEmpty(item.MessageID))
                    {
                        ucItem.MessageID = item.MessageID;
                    }
                    ucItem.GroupName = group.Name;
                    ucItem.Name = $"uc_{item.Name}_{item.RegisterAddress}";
                    ucItem.Click += UcItem_Click;
                    flowPanel.Controls.Add(ucItem);
                }

                pnlMonitorInput.Controls.Add(flowPanel);
            }
            inputMonitorItems = monitorConfig.GenericMonitorConfiguration.InputSection.Groups.SelectMany(i => i.InputItemList).ToList();
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

            ((UCItem)uc).ChangeStatus(item, response, messageDirection);
            return true;
        }

        /// <summary>
        /// Starts a new test periodically
        /// </summary>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the task</param>
        public void StartTest(CancellationToken cancellationToken)
        {
            MonitorUtil.RunTestPeriodically(monitorConfig, cancellationToken, MonitorTestType.Generic);
        }

        /// <summary>
        /// Gets whether this test is runnable or not.
        /// </summary>
        /// <returns>true if this test is runnable; otherwise, false.</returns>
        public bool CanBeRun()
        {
            return monitorConfig != null;
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
                                ucItem.Visible = ucItem.Name.IndexOf(filter, StringComparison.OrdinalIgnoreCase) >= 0;
                                anyUcItemVisible |= ucItem.Visible;
                            }
                        }
                        flowLabel.Visible = anyUcItemVisible;
                    }
                }
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Checks and formats a response based on SID and NRC values.
        /// </summary>
        /// <param name="SID">The Service Identifier.</param>
        /// <param name="NRC">The Negative Response Code.</param>
        /// <returns>Returns true if the response is valid, otherwise false.</returns>
        private bool CheckResponse(byte SID, byte NRC)
        {
            string response = string.Empty;
            if (sidResponseMessageDict.TryGetValue(SID, out string sidResponse))
            {
                response += sidResponse + " ";

                if (nrcResponseMessageDict.TryGetValue(NRC, out string nrcResponse))
                    response += nrcResponse;
            }

            if (!String.IsNullOrWhiteSpace(response))
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
            UpdateLabelVisibility(uc.Item.ItemType);

            lblItemName.Text = $"{uc.GroupName}-{uc.Item.Name}";
            lblUpperLimit.Text = uc.Item.UpperLimit.ToString();
            lblLowerLimit.Text = uc.Item.LowerLimit.ToString();
            lblCoefficient.Text = uc.Item.Coefficient.ToString();
            lblData.Text = BitConverter.ToString(uc.Item.Data);
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

        /// <summary>
        /// Sets a default status for all digital input monitor items.
        /// </summary>
        /// <param name="status">The status byte to set for each digital item.</param>
        internal void SetDefaultStatus(byte status)
        {
            Enabled = false;
            foreach (var item in inputMonitorItems.Where(x => x.ItemType == "Digital"))
                ((UCItem)FindUIControl(item.Name, item.RegisterAddress)).SetStatus(status, true);
            Enabled = true;
        }

        #endregion
    }
}
