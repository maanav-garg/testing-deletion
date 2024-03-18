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
    /// Implements the FormMonitorEnvInput form.
    /// </summary>
    public partial class FormMonitorEnvInput : DockContent, IPeriodicTest
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
        /// Visibility settings for different item types.
        /// </summary>
        private readonly Dictionary<string, (bool Upper, bool Lower, bool Coefficient)> visibilitySettings = new Dictionary<string, (bool, bool, bool)>
        {
            { "Digital", (false, false, false) },
            { "Analog", (true, true, true) },
            { "Resistive", (true, true, true) },
            { "Frequency", (true, true, false) }
        };

        private Dictionary<string, (Control uc, InputMonitorItem item)> pnlMonitorUserControlDict = new Dictionary<string, (Control uc, InputMonitorItem item)>();
        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the FormMonitorEnvInput class.
        /// </summary>
        public FormMonitorEnvInput()
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
        /// Loads the page with the specified MonitorConfiguration
        /// </summary>
        /// <param name="configuration">A reference to the MonitorConfiguration instance to be used</param>
        internal void LoadConfiguration(AutosarBcmConfiguration configuration)
        {
            if (monitorConfig != null)
            {
                pnlMonitorInput.Controls.Clear();
                pnlMonitorUserControlDict.Clear();
            }

            monitorConfig = configuration;

            var defaultMessageId = configuration.GenericMonitorConfiguration.InputSection.CommonConfig?.MessageID;

            foreach (var group in configuration.GenericMonitorConfiguration.InputSection.Groups)
            {
                pnlMonitorInput.Controls.Add(new Label { Font = new Font(Label.DefaultFont, FontStyle.Bold), Text = group.Name, AutoSize = true, Margin = new Padding(5) });

                var flowPanel = new FlowLayoutPanel { AutoSize = true, Margin = Padding = new Padding(3) };
                flowPanel.Paint += pnlMonitorInput_Paint;

                var inputItems = group.InputItemList.OrderBy(x => x.Name).ToList();
                foreach (var item in inputItems)
                {
                    var ucItem = new UCItem(item, monitorConfig.GenericMonitorConfiguration.InputSection.CommonConfig);
                    ucItem.MessageID = defaultMessageId;
                    if (!string.IsNullOrEmpty(item.MessageID))
                    {
                        ucItem.MessageID = item.MessageID;
                    }
                    ucItem.GroupName = group.Name;
                    ucItem.Name = $"uc_{item.Name}_{item.RegisterAddress}";
                    ucItem.Click += UcItem_Click;
                    flowPanel.Controls.Add(ucItem);

                    pnlMonitorUserControlDict.Add($"{item.RegisterGroup}{item.RegisterAddress}", (ucItem, item));
                }

                pnlMonitorInput.Controls.Add(flowPanel);
            }
        }

        /// <summary>
        /// Starts a new test periodically
        /// </summary>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the task</param>
        public void StartTest(CancellationToken cancellationToken)
        {
            MonitorUtil.RunTestPeriodically(monitorConfig, cancellationToken, MonitorTestType.Environmental);
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

        /// <summary>
        /// Change the item status
        /// </summary>
        /// <param name="receivedData">Data comes from device</param>
        public bool ChangeStatus(byte[] receivedData, MessageDirection messageDirection)
        {
            var response = new GenericResponse(receivedData, monitorConfig.GenericMonitorConfiguration.InputSection.CommonConfig.InputRegisterGroupOffset, monitorConfig.GenericMonitorConfiguration.InputSection.CommonConfig.InputRegisterGroupLength);

            if (messageDirection == MessageDirection.RX)
            {
                var isSuccess = CheckResponse(response.SID, receivedData[6]);
                if (!String.IsNullOrWhiteSpace(isSuccess))
                {
                    ((FormMain)Application.OpenForms[Constants.Form_Main]).AppendTrace($"{isSuccess}");
                    return true;
                }
            }

            if (pnlMonitorUserControlDict.TryGetValue($"{response.RegisterGroup:X4}{response.RegisterAddress:X2}", out (Control uc, InputMonitorItem item) value))
            {
                if (value.uc == null)
                {
                    ((FormMain)Application.OpenForms[Constants.Form_Main]).AppendTrace($"Controller has not contains that address!");
                    return false;
                }

                ((UCItem)value.uc).ChangeStatus(value.item, response, messageDirection);
                return true;
            }
                
            return false;            
        }

        /// <summary>
        /// Adds a new output monitor item to a specified group.
        /// </summary>
        /// <param name="outputMonitorItem">The output monitor item to add.</param>
        /// <param name="group">The group to which the item will be added.</param>
        public void AddNewOutputMonitorItem(OutputMonitorItem outputMonitorItem, Group group)
        {
            var outputItems = monitorConfig.GenericMonitorConfiguration.OutputSection.Groups.FirstOrDefault(g => g.Name == group.Name);
            outputItems.OutputItemList.Add(outputMonitorItem);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Checks and formats a response based on SID and NRC values.
        /// </summary>
        /// <param name="SID">The Service Identifier.</param>
        /// <param name="NRC">The Negative Response Code.</param>
        /// <returns>A formatted response string or null if no response is found.</returns>
        private string CheckResponse(byte SID, byte NRC)
        {
            string response = string.Empty;
            if (sidResponseMessageDict.TryGetValue(SID, out string sidResponse))
            {
                response += sidResponse + " ";

                if (nrcResponseMessageDict.TryGetValue(NRC, out string nrcResponse))
                    response += nrcResponse;
            }

            return String.IsNullOrWhiteSpace(response) ? null : response;
        }

        /// <summary>
        /// Updates the data displayed in the status bar
        /// </summary>
        /// <param name="sender">A reference to the UcItem instance</param>
        /// <param name="e">A reference to the Click event's arguments</param>
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
                tsUpperLimitLbl.Visible = lblUpperLimit.Visible = settings.Upper;
                tsLowerLimitLbl.Visible = lblLowerLimit.Visible = settings.Lower;
                tsCoefficientLbl.Visible = lblCoefficient.Visible = settings.Coefficient;
            }
            else
            {
                tsUpperLimitLbl.Visible = lblUpperLimit.Visible = tsLowerLimitLbl.Visible = lblLowerLimit.Visible = tsCoefficientLbl.Visible = lblCoefficient.Visible = true;
            }
        }

        /// <summary>
        /// Changes the border color of the FlowLayoutPanel groups
        /// </summary>
        /// <param name="sender">A reference to the FlowlayoutPanel instance to be painted.</param>
        /// <param name="e">A reference to the Paint event's arguments.</param>
        private void pnlMonitorInput_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, ((FlowLayoutPanel)sender).ClientRectangle, Color.LightGray, ButtonBorderStyle.Solid);
        }

        #endregion
    }
}