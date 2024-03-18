using AutosarBCM.Config;
using AutosarBCM.UserControls.Monitor;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace AutosarBCM.Forms.Monitor
{
    /// <summary>
    /// Implements the FormMonitorGenericOutput form.
    /// </summary>
    public partial class FormMonitorGenericOutput : DockContent, IClickTest
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
        /// List of read-only output items.
        /// </summary>
        internal List<OutputUserControl> outputItems = new List<OutputUserControl>();

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the FormMonitorGenericOutput class.
        /// </summary>
        public FormMonitorGenericOutput()
        {
            InitializeComponent();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Load page with configuration
        /// </summary>
        /// <param name="config">Monitor config object</param>
        internal void LoadConfiguration(AutosarBcmConfiguration configuration)
        {
            if (monitorConfig != null)
            {
                outputItems.Clear();
                pnlMonitorOutput.Controls.Clear();
            }

            monitorConfig = configuration;

            foreach (var group in configuration.GenericMonitorConfiguration.OutputSection.Groups)
            {
                if (group.OutputItemList.Count == 0)
                    continue;

                pnlMonitorOutput.Controls.Add(new Label { Font = new Font(Label.DefaultFont.FontFamily, 13, FontStyle.Bold), Text = group.Name, AutoSize = true, Margin = new Padding(5) });

                var map = group.OutputItemList.GroupBy(x => x.ItemType).ToDictionary(x => x.Key, x => x.ToList());

                var flowPanel = new FlowLayoutPanel { AutoSize = true, Margin = new Padding(10, 5, 0, 5), FlowDirection = FlowDirection.TopDown };
                foreach (var itemType in map.Keys)
                {
                    if (map[itemType].Count == 0)
                        continue;

                    flowPanel.Controls.Add(new Label { Font = new Font(Label.DefaultFont, FontStyle.Bold), Text = "\t\t" + itemType, AutoSize = true, Margin = new Padding(5,5,5,5) });

                    var subPanel = new FlowLayoutPanel { AutoSize = true, BorderStyle = BorderStyle.FixedSingle, Margin = new Padding(10,5,5,5) };

                    var items = map[itemType].OrderBy(x => x.Name).ToList();
                    foreach (var item in items)
                    {
                        var oItem = CreateOutputUC(item, group);
                        subPanel.Controls.Add(oItem);
                        outputItems.Add(oItem);
                    }

                    flowPanel.Controls.Add(subPanel);
                }
                pnlMonitorOutput.Controls.Add(flowPanel);
            }
        }

        /// <summary>
        /// Change the item status
        /// </summary>
        /// <param name="receivedData">Data comes from device</param>
        public bool ChangeStatus(byte[] receivedData, MessageDirection messageDirection)
        {
            if (messageDirection == MessageDirection.TX || (receivedData[2] == 0x03 && receivedData[3] == 0xef && receivedData[4] == 0x05))
                return false;

            Response response;
            if (receivedData[3] == 0xEF)
                response = new PEPSResponse(receivedData);
            else if (receivedData[2] == (byte)EEProm_ByteGroup.Read_PCI && receivedData[3] == (byte)EEProm_ByteGroup.Read_SID)
                response = EEPromResponse.ReadResponse(receivedData);
            else if (receivedData[2] == (byte)EEProm_ByteGroup.Write_PCI && receivedData[3] == (byte)EEProm_ByteGroup.Write_SID)
                response = EEPromResponse.WriteResponse(receivedData);
            else
                response = new GenericResponse(receivedData,
                monitorConfig.GenericMonitorConfiguration.OutputSection.CommonConfig.InputRegisterGroupOffset,
                monitorConfig.GenericMonitorConfiguration.OutputSection.CommonConfig.InputRegisterGroupLength);

            if (!CheckResponse(response.SID, receivedData[6]))
                return true;

            OutputUserControl uc = outputItems.FirstOrDefault(i => CheckRegistration(i.RegisterDict, response.RegisterGroup, response.RegisterAddress));

            if (uc == null)
                return false;

            uc.ChangeStatus(response);
            return true;
        }

        /// <summary>
        /// Filters the user control items based on the specified filter.
        /// </summary>
        /// <param name="filter">The string used to filter the items.</param>
        public void FilterUCItems(string filter)
        {
            pnlMonitorOutput.SuspendLayout();
            foreach (Control control in pnlMonitorOutput.Controls)
            {
                if (control is Label groupLabel)
                {
                    bool isGroupLabelMatched = groupLabel.Text.IndexOf(filter, StringComparison.OrdinalIgnoreCase) >= 0;

                    int flowPanelIndex = pnlMonitorOutput.Controls.IndexOf(control) + 1;
                    if (flowPanelIndex < pnlMonitorOutput.Controls.Count && pnlMonitorOutput.Controls[flowPanelIndex] is FlowLayoutPanel flowPanel)
                    {
                        bool isAnySubPanelItemVisible = false;

                        for (int i = 0; i < flowPanel.Controls.Count; i += 2) 
                        {
                            var subPanelLabel = flowPanel.Controls[i] as Label;
                            var subPanel = flowPanel.Controls[i + 1] as FlowLayoutPanel;

                            bool isAnyItemVisibleInSubPanel = false;

                            if (subPanel != null)
                            {
                                foreach (var item in subPanel.Controls)
                                {
                                    if (item is OutputUserControl ucItem)
                                    {
                                        bool isItemVisible = isGroupLabelMatched || ucItem.Name.IndexOf(filter, StringComparison.OrdinalIgnoreCase) >= 0;
                                        ucItem.Visible = isItemVisible;
                                        isAnyItemVisibleInSubPanel |= isItemVisible;
                                    }
                                }
                            }

                            if (subPanelLabel != null)
                                subPanelLabel.Visible = isAnyItemVisibleInSubPanel; 

                            isAnySubPanelItemVisible |= isAnyItemVisibleInSubPanel;
                        }

                        flowPanel.Visible = isGroupLabelMatched || isAnySubPanelItemVisible;
                        groupLabel.Visible = flowPanel.Visible;
                    }
                }
            }
            pnlMonitorOutput.ResumeLayout();
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
        /// Sets the keyfob ID for the RSSI measurement in PEPS output items.
        /// </summary>
        /// <param name="keyfobID">The keyfob ID array to be used in RSSI measurement.</param>
        internal void SetKeyListForRSSI(byte[] keyfobID)
        {
            outputItems.OfType<UCPEPSOutput>().Where(x => x.Name == "uc_PEPS_GET_RSSI_Measurement").FirstOrDefault().SetKeyfobID(keyfobID);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Changes the border color of the FlowLayoutPanel groups
        /// </summary>
        /// <param name="sender">A reference to the FlowlayoutPanel instance to be painted.</param>
        /// <param name="e">A reference to the Paint event's arguments.</param>
        private void flowPanel_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, ((FlowLayoutPanel)sender).ClientRectangle, Color.LightGray, ButtonBorderStyle.Solid);
        }

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
                FormMain.TestClickCounter--;
                return false;
            }
            return true;
        }

        /// <summary>
        /// Creates an output user control based on the specified monitor item and group.
        /// </summary>
        /// <param name="item">The output monitor item used to create the control.</param>
        /// <param name="group">The group to which the control will be associated.</param>
        /// <returns>A new instance of an output user control.</returns>
        private OutputUserControl CreateOutputUC(OutputMonitorItem item, Group group)
        {
            var revertTime = item.RevertTime != 0 ? item.RevertTime : (monitorConfig.GenericMonitorConfiguration.OutputSection.CommonConfig?.RevertTime ?? 0);
            var revertLimit = monitorConfig.GenericMonitorConfiguration.OutputSection.CommonConfig?.RevertTrialLimit ?? 0;
            var riskLimit = monitorConfig.GenericMonitorConfiguration.OutputSection.CommonConfig?.DefaultRiskLimit ?? string.Empty;
            var defaultFreq = monitorConfig.GenericMonitorConfiguration.OutputSection.CommonConfig?.DefaultFrequency ?? 0;
            var defaultDuty = monitorConfig.GenericMonitorConfiguration.OutputSection.CommonConfig?.DefaultDuty ?? 0;

            OutputUserControl ucItem;
            if (item.ItemType == "PEPS")
                ucItem = new UCPEPSOutput(item);
            else if (item.ItemType == "Power Mirror")
                ucItem = new UCPowerMirror(item, revertTime, revertLimit);
            else if (item.ItemType == "Loopback")
                ucItem = new UCLoopback(item);
            else if (item.ItemType == "Wiper")
                ucItem = new UCWiper(item);
            else if (item.ItemType == "Power Window" || item.ItemType == "Sunroof")
                ucItem = new UCOpenCloseController(item, revertTime, revertLimit);
            else if (item.ItemType == "DoorControl")
                ucItem = new UCDoorControls(item, riskLimit, revertLimit);
            else if (item.ItemType == "EEProm")
                ucItem = new UCEEProm(item);
            else
            {
                ucItem = new UCDigitalOutputItem(item, revertTime, revertLimit, riskLimit, defaultFreq, defaultDuty);
                ucItem.PWM = monitorConfig.GenericMonitorConfiguration.OutputSection.CommonConfig?.Pwm ?? 0;
            }
            ucItem.Click += UcItem_Click;
            ucItem.MessageID = !string.IsNullOrEmpty(item.MessageID) ? item.MessageID : (monitorConfig.GenericMonitorConfiguration.OutputSection.CommonConfig?.MessageID);
            ucItem.GroupName = group.Name;
            ucItem.RegisterDict = CreateRegisterDict(item);
            ucItem.Name = $"uc_{group.Name}_{item.Name}";
            return ucItem;
        }

        /// <summary>
        /// Updates the status strip data
        /// </summary>
        /// <param name="sender">User control</param>
        /// <param name="e">Event args</param>
        private void UcItem_Click(object sender, EventArgs e)
        {
            if (this.InvokeRequired)
                return;
            ResetVisibility();

            switch (sender)
            {
                case UCDigitalOutputItem ucoutputitem:
                    lblItemName.Text = $"{ucoutputitem.GroupName}-{ucoutputitem.MonitorItem.Name}";
                    lblRevertTime.Text = ucoutputitem.RevertTime.ToString();
                    lblState.Text = ucoutputitem.State;
                    lblAdcData.Text = BitConverter.ToString(ucoutputitem.MonitorItem.ReadADCData);
                    lblCurrentData.Text = BitConverter.ToString(ucoutputitem.MonitorItem.ReadCurrentData);
                    lblDiagData.Text = BitConverter.ToString(ucoutputitem.MonitorItem.ReadDiagData);
                    lblAdcValue.Text = ucoutputitem.ADCValue;
                    lblCurrentValue.Text = ucoutputitem.CurrentData;
                    lblDiagValue.Text = ucoutputitem.DIAGValue;
                    ucoutputitem.Focus();

                    if (ucoutputitem.MonitorItem.ItemType != "Send" && ucoutputitem.MonitorItem.ItemType != "PWM")
                        lblStateHeader.Visible = lblState.Visible = lblRevertTime.Visible = lblRevertTimeHeader.Visible = true;
                    if (ucoutputitem.MonitorItem.ReadADCData?.Length > 0)
                        lblAdcDataHeader.Visible = lblAdcData.Visible = true;
                    if (ucoutputitem.MonitorItem.ReadCurrentData.Length > 0)
                        lblCurrentDataHeader.Visible = lblCurrentData.Visible = true;
                    if (ucoutputitem.ADCValue != "-")
                        lblAdcValueHeader.Visible = lblAdcValue.Visible = true;
                    if (ucoutputitem.MonitorItem.ReadDiagData?.Length > 0)
                        lblDiagDataHeader.Visible = lblDiagData.Visible = true;
                    if (ucoutputitem.DIAGValue != "-")
                        lblDiagValueHeader.Visible = lblDiagValue.Visible = true;
                    if (ucoutputitem.CurrentData != "-")
                        lblCurrentValueHeader.Visible = lblCurrentValue.Visible = true;
                    break;
                case UCDoorControls ucdoorcontrolitem:
                    lblItemName.Text = $"{ucdoorcontrolitem.GroupName}-{ucdoorcontrolitem.Name}";
                    ucdoorcontrolitem.Focus();
                    break;
                case UCLoopback ucloopbackitem:
                    lblItemName.Text = $"{ucloopbackitem.GroupName}-{ucloopbackitem.Name}";
                    ucloopbackitem.Focus();
                    break;
                case UCPEPSOutput ucpepsitem:
                    lblItemName.Text = $"{ucpepsitem.GroupName}-{ucpepsitem.Name}";
                    ucpepsitem.Focus();
                    break;
                case UCPowerMirror ucpowermirroritem:
                    lblItemName.Text = $"{ucpowermirroritem.GroupName}-{ucpowermirroritem.Name}";
                    ucpowermirroritem.Focus();
                    break;
                case UCWiper ucwiperitem:
                    lblItemName.Text = $"{ucwiperitem.GroupName}-{ucwiperitem.Name}";
                    ucwiperitem.Focus();
                    break;
                case UCReadOnlyOutputItem ucreadonlyitem:
                    lblItemName.Text = $"{ucreadonlyitem.GroupName}-{ucreadonlyitem.Name}";
                    ucreadonlyitem.Focus();
                    break;
                case UCOpenCloseController ucopenclosecontroller:
                    lblItemName.Text = $"{ucopenclosecontroller.GroupName}-{ucopenclosecontroller.Name}";
                    ucopenclosecontroller.Focus();
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Resets the visibility of various labels to hidden.
        /// </summary>
        private void ResetVisibility()
        {
            lblRevertTime.Visible = lblRevertTimeHeader.Visible = lblStateHeader.Visible = lblState.Visible = lblAdcData.Visible = lblAdcDataHeader.Visible = lblDiagData.Visible = lblDiagDataHeader.Visible = lblAdcValue.Visible = lblAdcValueHeader.Visible = lblDiagValue.Visible = lblDiagValueHeader.Visible = lblCurrentDataHeader.Visible = lblCurrentData.Visible = lblCurrentValue.Visible = lblCurrentValueHeader.Visible = false;
        }

        /// <summary>
        /// Creates a dictionary mapping register groups to their corresponding data bytes based on an output monitor item.
        /// </summary>
        /// <param name="item">The output monitor item used for creating the dictionary.</param>
        /// <returns>A dictionary of register groups and their data bytes.</returns>
        private Dictionary<short, List<byte>> CreateRegisterDict(OutputMonitorItem item)
        {
            Dictionary<short, List<byte>> dict = new Dictionary<short, List<byte>>();
            var messageId = Helper.StringToByteArray(monitorConfig.GenericMonitorConfiguration.OutputSection.CommonConfig.MessageID);

            var groupOffset = monitorConfig.GenericMonitorConfiguration.OutputSection.CommonConfig.InputRegisterGroupOffset - messageId.Length;
            var groupLength = monitorConfig.GenericMonitorConfiguration.OutputSection.CommonConfig.InputRegisterGroupLength;

            if (item.ReadADCData?.Length > 0)
                AddToDict(dict, (short)(Helper.GetValueOfPrimitive(item.ReadADCData, groupOffset, groupLength)), item.ReadADCData[4]);
            if (item.ReadCurrentData?.Length > 0)
                AddToDict(dict, (short)(Helper.GetValueOfPrimitive(item.ReadCurrentData, groupOffset, groupLength)), item.ReadCurrentData[4]);
            if (item.ReadDiagData?.Length > 0)
                AddToDict(dict, (short)(Helper.GetValueOfPrimitive(item.ReadDiagData, groupOffset, groupLength)), item.ReadDiagData[4]);
            if (item.SetPWMData?.Length > 0)
                AddToDict(dict, (short)(Helper.GetValueOfPrimitive(item.SetPWMData, groupOffset, groupLength)), item.SetPWMData[4]);
            if (item.OpenData?.Length > 0)
                AddToDict(dict, (short)(Helper.GetValueOfPrimitive(item.OpenData, groupOffset, groupLength)), item.OpenData[4]);
            if (item.CloseData?.Length > 0)
                AddToDict(dict, (short)(Helper.GetValueOfPrimitive(item.CloseData, groupOffset, groupLength)), item.CloseData[4]);
            if (item.PEPSData?.Length > 0)
                AddToDict(dict, BitConverter.ToInt16(new byte[] { item.PEPSData[2], 0 }, 0), item.PEPSData[2]);
            if (item.SendData?.Length > 0)
                AddToDict(dict, (short)(Helper.GetValueOfPrimitive(item.SendData, groupOffset, groupLength)), item.SendData[4]);

            if (item.Loopback != null)
            {
                AddToDict(dict, (short)Helper.GetValueOfPrimitive(item.Loopback.Pair1Data, groupOffset, groupLength), item.Loopback.Pair1Data[4]);
                AddToDict(dict, (short)Helper.GetValueOfPrimitive(item.Loopback.Pair2Data, groupOffset, groupLength), item.Loopback.Pair2Data[4]);
                AddToDict(dict, (short)Helper.GetValueOfPrimitive(item.Loopback.Verification, groupOffset, groupLength), item.Loopback.Pair1Data[4]);
                AddToDict(dict, (short)Helper.GetValueOfPrimitive(item.Loopback.Verification, groupOffset, groupLength), item.Loopback.Pair2Data[4]);
            }

            if (item.WiperCase != null)
            {
                AddToDict(dict, (short)Helper.GetValueOfPrimitive(item.WiperCase.StopHigh, groupOffset, groupLength), item.WiperCase.StopLow[4]);
                AddToDict(dict, (short)Helper.GetValueOfPrimitive(item.WiperCase.StopHigh, groupOffset, groupLength), item.WiperCase.StopHigh[4]);
                AddToDict(dict, (short)Helper.GetValueOfPrimitive(item.WiperCase.StopHigh, groupOffset, groupLength), item.WiperCase.HighStop[4]);
                AddToDict(dict, (short)Helper.GetValueOfPrimitive(item.WiperCase.StopHigh, groupOffset, groupLength), item.WiperCase.LowStop[4]);
                AddToDict(dict, (short)Helper.GetValueOfPrimitive(item.WiperCase.StopHigh, groupOffset, groupLength), item.WiperCase.LowHigh[4]);
                AddToDict(dict, (short)Helper.GetValueOfPrimitive(item.WiperCase.StopHigh, groupOffset, groupLength), item.WiperCase.HighLow[4]);
            }

            if (item.PowerMirror != null)
            {
                AddToDict(dict, (short)(Helper.GetValueOfPrimitive(item.PowerMirror.SetOpenUp, groupOffset, groupLength)), item.PowerMirror.SetOpenUp[4]);
                AddToDict(dict, (short)(Helper.GetValueOfPrimitive(item.PowerMirror.SetCloseUp, groupOffset, groupLength)), item.PowerMirror.SetCloseUp[4]);
                AddToDict(dict, (short)(Helper.GetValueOfPrimitive(item.PowerMirror.SetOpenDown, groupOffset, groupLength)), item.PowerMirror.SetOpenDown[4]);
                AddToDict(dict, (short)(Helper.GetValueOfPrimitive(item.PowerMirror.SetCloseDown, groupOffset, groupLength)), item.PowerMirror.SetCloseDown[4]);
                AddToDict(dict, (short)(Helper.GetValueOfPrimitive(item.PowerMirror.SetOpenLeft, groupOffset, groupLength)), item.PowerMirror.SetOpenLeft[4]);
                AddToDict(dict, (short)(Helper.GetValueOfPrimitive(item.PowerMirror.SetCloseLeft, groupOffset, groupLength)), item.PowerMirror.SetCloseLeft[4]);
                AddToDict(dict, (short)(Helper.GetValueOfPrimitive(item.PowerMirror.SetOpenRight, groupOffset, groupLength)), item.PowerMirror.SetOpenRight[4]);
                AddToDict(dict, (short)(Helper.GetValueOfPrimitive(item.PowerMirror.SetCloseRight, groupOffset, groupLength)), item.PowerMirror.SetCloseRight[4]);
                AddToDict(dict, (short)(Helper.GetValueOfPrimitive(item.PowerMirror.ReadUp, groupOffset, groupLength)), item.PowerMirror.ReadUp[4]);
                AddToDict(dict, (short)(Helper.GetValueOfPrimitive(item.PowerMirror.ReadLeftDown, groupOffset, groupLength)), item.PowerMirror.ReadLeftDown[4]);
                AddToDict(dict, (short)(Helper.GetValueOfPrimitive(item.PowerMirror.ReadRight, groupOffset, groupLength)), item.PowerMirror.ReadRight[4]);
            }

            if (item.PowerWindow != null || item.Sunroof != null)
            {
                var openCloseItem = (item.Sunroof != null ? (OpenCloseItem)item.Sunroof : (OpenCloseItem)item.PowerWindow);

                AddToDict(dict, (short)(Helper.GetValueOfPrimitive(openCloseItem.EnableOpenData, groupOffset, groupLength)), openCloseItem.EnableOpenData[4]);
                AddToDict(dict, (short)(Helper.GetValueOfPrimitive(openCloseItem.DisableOpenData, groupOffset, groupLength)), openCloseItem.DisableOpenData[4]);
                AddToDict(dict, (short)(Helper.GetValueOfPrimitive(openCloseItem.EnableCloseData, groupOffset, groupLength)), openCloseItem.EnableCloseData[4]);
                AddToDict(dict, (short)(Helper.GetValueOfPrimitive(openCloseItem.DisableCloseData, groupOffset, groupLength)), openCloseItem.DisableCloseData[4]);
                AddToDict(dict, (short)(Helper.GetValueOfPrimitive(openCloseItem.ReadOpenDiagData, groupOffset, groupLength)), openCloseItem.ReadOpenDiagData[4]);
                AddToDict(dict, (short)(Helper.GetValueOfPrimitive(openCloseItem.ReadCloseDiagData, groupOffset, groupLength)), openCloseItem.ReadCloseDiagData[4]);
            }

            if (item.DoorControl != null)
            {
                AddToDict(dict, (short)(Helper.GetValueOfPrimitive(item.DoorControl.DoorLockEnable, groupOffset, groupLength)), item.DoorControl.DoorLockEnable[4]);
                AddToDict(dict, (short)(Helper.GetValueOfPrimitive(item.DoorControl.DoorLockDisable, groupOffset, groupLength)), item.DoorControl.DoorLockDisable[4]);
                AddToDict(dict, (short)(Helper.GetValueOfPrimitive(item.DoorControl.DoorUnlockDisable, groupOffset, groupLength)), item.DoorControl.DoorUnlockDisable[4]);
                AddToDict(dict, (short)(Helper.GetValueOfPrimitive(item.DoorControl.DoorUnlockEnable, groupOffset, groupLength)), item.DoorControl.DoorUnlockEnable[4]);
                AddToDict(dict, (short)(Helper.GetValueOfPrimitive(item.DoorControl.ReadDoorLockDiag, groupOffset, groupLength)), item.DoorControl.ReadDoorLockDiag[4]);
                AddToDict(dict, (short)(Helper.GetValueOfPrimitive(item.DoorControl.ReadDoorUnlockDiag, groupOffset, groupLength)), item.DoorControl.ReadDoorUnlockDiag[4]);
            }

            if(item.EEProm != null)
            {
                AddToDict(dict, (short)(Helper.GetValueOfPrimitive(item.EEProm.ReadResponse, 0, 2)), 0);
                AddToDict(dict, (short)(Helper.GetValueOfPrimitive(item.EEProm.WriteResponse, 0, 2)), 0);
            }

            return dict;
        }

        /// <summary>
        /// Adds a key-value pair to the dictionary, or appends the value if the key already exists.
        /// </summary>
        /// <param name="dict">The dictionary to add to.</param>
        /// <param name="key">The key to add or append to.</param>
        /// <param name="value">The value to add or append.</param>
        private void AddToDict(Dictionary<short, List<byte>> dict, short key, byte value)
        {
            if (dict.ContainsKey(key))
                dict[key].Add(value);
            else
                dict.Add(key, new List<byte>() { value });
        }

        /// <summary>
        /// Checks if a specific register group and address is registered in the dictionary.
        /// </summary>
        /// <param name="dict">The dictionary containing register group and address data.</param>
        /// <param name="registerGroup">The register group to check.</param>
        /// <param name="registerAddress">The register address to check.</param>
        /// <returns>True if the register group and address are found; otherwise, false.</returns>
        private bool CheckRegistration(Dictionary<short, List<byte>> dict, short registerGroup, byte registerAddress)
        {
            if (dict.TryGetValue(registerGroup, out List<byte> address))
                if (address.Contains(registerAddress))
                    return true;

            return false;
        }

        #endregion
    }
}
