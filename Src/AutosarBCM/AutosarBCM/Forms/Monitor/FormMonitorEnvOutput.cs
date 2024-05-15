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
    /// Implements the FormMonitorEnvOutput form.
    /// </summary>
    public partial class FormMonitorEnvOutput : DockContent, IClickTest
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
        private List<UCReadOnlyOutputItem> outputItems = new List<UCReadOnlyOutputItem>();
        
        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the FormMonitorEnvOutput class.
        /// </summary>
        public FormMonitorEnvOutput()
        {
            InitializeComponent();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Change the UI controls based on the response data
        /// </summary>
        /// <param name="receivedData">Response RX data</param>
        public bool ChangeStatus(byte[] receivedData, MessageDirection messageDirection)
        {
            Response response;
            if (receivedData[3] == 0xEF)
                response = new PEPSResponse(receivedData);
            else 
                response = new GenericResponse(receivedData,
                monitorConfig.GenericMonitorConfiguration.OutputSection.CommonConfig.InputRegisterGroupOffset,
                monitorConfig.GenericMonitorConfiguration.OutputSection.CommonConfig.InputRegisterGroupLength);

            if (messageDirection == MessageDirection.RX)
            {
                var isSuccess = CheckResponse(response.SID, receivedData[6]);
                if (!String.IsNullOrWhiteSpace(isSuccess))
                {
                    ((FormMain)Application.OpenForms[Constants.Form_Main]).AppendTrace($"{isSuccess}");
                    return true;
                }
            }

            var uc = outputItems.FirstOrDefault(i => CheckRegistration(i.RegisterDict, response.RegisterGroup, response.RegisterAddress));

            if (uc == null)
                return false;

            uc.ChangeStatus(response, messageDirection);
            return true;
        }

        /// <summary>
        /// Dynamically generates UI controls
        /// </summary>
        /// <param name="configuration">Monitor configuration object</param>
        internal void LoadConfiguration(AutosarBcmConfiguration configuration)
        {
            if (monitorConfig != null)
            {
                outputItems.Clear();
                pnlMonitorOutput.Controls.Clear();
            }

            monitorConfig = configuration;
            var defaultMessageId = configuration.GenericMonitorConfiguration.OutputSection.CommonConfig?.MessageID;

            foreach (var group in configuration.GenericMonitorConfiguration.OutputSection.Groups)
            {
                if (group.OutputItemList.Count == 0)
                    continue;

                pnlMonitorOutput.Controls.Add(new Label { Font = new Font(Label.DefaultFont.FontFamily, 13, FontStyle.Bold), Text = group.Name, AutoSize = true, Margin = new Padding(5) });

                var flowPanel = new FlowLayoutPanel { AutoSize = true, Margin = new Padding(10, 5, 0, 5) };
                flowPanel.Paint += pnlMonitorInput_Paint;

                var outputItems = group.OutputItemList.OrderBy(i => i.Name).ToList();
                for (int i = 0; i < outputItems.Count; i++)
                {
                    var item = outputItems[i];
                    if (item.ItemType == "Loopback")
                        flowPanel.Controls.AddRange(CreateOutputItemsFromLoopBack(item, group).ToArray());

                    else if (item.ItemType == "Power Mirror")
                        flowPanel.Controls.AddRange(CreateOutputItemsFromPowerMirror(item, group).ToArray());
                        
                    else if (item.ItemType == "Wiper")
                        flowPanel.Controls.AddRange(CreateOutputItemsFromWiper(item, group).ToArray());

                    else if (item.ItemType == "Sunroof" || item.ItemType == "Power Window")
                        flowPanel.Controls.AddRange(CreateOutputItemsFromOpenCloseItem(item,group).ToArray());

                    else if (item.ItemType == "DoorControl")
                        flowPanel.Controls.AddRange(CreateOutputItemsFromDoorControlsItem(item, group).ToArray());

                    else
                        flowPanel.Controls.Add(CreateOutputItem(item, group));
                }

                pnlMonitorOutput.Controls.Add(flowPanel);
            }
            outputItems.AddRange(pnlMonitorOutput.Controls.OfType<FlowLayoutPanel>().ToList().SelectMany(sl => sl.Controls.OfType<UCReadOnlyOutputItem>()));
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
            pnlMonitorOutput.SuspendLayout();

            foreach (FlowLayoutPanel flowPanel in pnlMonitorOutput.Controls.OfType<FlowLayoutPanel>())
            {
                bool isAnyControlVisible = false;

                foreach (var uc in flowPanel.Controls)
                {
                    if (uc is UCReadOnlyOutputItem ucItem)
                    {
                        bool isVisible = ucItem.Name.IndexOf(filter, StringComparison.OrdinalIgnoreCase) >= 0;
                        ucItem.Visible = isVisible;
                        isAnyControlVisible |= isVisible;
                    }
                }

                flowPanel.Visible = isAnyControlVisible;
                var labelIndex = pnlMonitorOutput.Controls.IndexOf(flowPanel) - 1;
                if (labelIndex >= 0)
                {
                    pnlMonitorOutput.Controls[labelIndex].Visible = isAnyControlVisible;
                }
            }

            pnlMonitorOutput.ResumeLayout();
        }


        #endregion

        #region Private Methods

        /// <summary>
        /// Creates output items for wiper functionality from a given item and group.
        /// </summary>
        /// <param name="item">The output monitor item containing wiper data.</param>
        /// <param name="group">The group to add the output items to.</param>
        /// <returns>List of read-only output items created for wiper functionality.</returns>
        private List<UCReadOnlyOutputItem> CreateOutputItemsFromWiper(OutputMonitorItem item, Group group)
        {
            if (item.WiperCase == null)
                return new List<UCReadOnlyOutputItem>();

            var items = new List<UCReadOnlyOutputItem>
            {
                CreateOutputItem(new OutputMonitorItem()
                {
                    ItemType = item.ItemType,
                    Name = $"{item.Name}_Stop_2_Low",
                    MessageID = item.MessageID,
                    OpenData = item.WiperCase.StopLow
                }, group),
                CreateOutputItem(new OutputMonitorItem()
                {
                    ItemType = item.ItemType,
                    Name = $"{item.Name}_Stop_2_High",
                    MessageID = item.MessageID,
                    OpenData = item.WiperCase.StopHigh
                        }, group),
                CreateOutputItem(new OutputMonitorItem()
                {
                    ItemType = item.ItemType,
                    Name = $"{item.Name}_Low_2_Stop",
                    MessageID = item.MessageID,
                    OpenData = item.WiperCase.LowStop
                }, group),
                CreateOutputItem(new OutputMonitorItem()
                {
                    ItemType = item.ItemType,
                    Name = $"{item.Name}_High_2_Stop",
                    MessageID = item.MessageID,
                    OpenData = item.WiperCase.HighStop
                }, group)
                    };

            foreach (var it in items)
                ((FormMonitorEnvInput)Application.OpenForms[Constants.Form_Monitor_Env_Input]).AddNewOutputMonitorItem(it.Item, group);

            return items;
        }

        /// <summary>
        /// Creates output items for door control functionality from a given item and group.
        /// </summary>
        /// <param name="item">The output monitor item containing door control data.</param>
        /// <param name="group">The group to add the output items to.</param>
        /// <returns>List of read-only output items created for door control functionality.</returns>
        private List<UCReadOnlyOutputItem> CreateOutputItemsFromDoorControlsItem(OutputMonitorItem item, Group group)
        {
            if(item.DoorControl == null)
                return new List<UCReadOnlyOutputItem>();
				
            var items = new List<UCReadOnlyOutputItem>
            {
                CreateOutputItem(new OutputMonitorItem()
                {
					ItemType = item.ItemType,
                    Name = item.Name + "_LOCK",
                    MessageID = item.MessageID,
                    OpenData = item.DoorControl.DoorLockEnable,
                    CloseData = item.DoorControl.DoorLockDisable,
                    ReadDiagData = item.DoorControl.ReadDoorLockDiag
                }, group),
                CreateOutputItem(new OutputMonitorItem()
                {
                    ItemType = item.ItemType,
                    Name = item.Name + "_UNLOCK",
                    MessageID = item.MessageID,
                    OpenData = item.DoorControl.DoorUnlockEnable,
                    CloseData= item.DoorControl.DoorUnlockDisable,
                    ReadDiagData = item.DoorControl.ReadDoorUnlockDiag
                }, group),
            };
			
			      foreach (var it in items)
                ((FormMonitorEnvInput)Application.OpenForms[Constants.Form_Monitor_Env_Input]).AddNewOutputMonitorItem(it.Item, group);

            return items;
        }

        /// <summary>
        /// Creates output items for open/close functionality from a given item and group.
        /// </summary>
        /// <param name="item">The output monitor item containing open/close data.</param>
        /// <param name="group">The group to add the output items to.</param>
        /// <returns>List of read-only output items created for open/close functionality.</returns>
        private List<UCReadOnlyOutputItem> CreateOutputItemsFromOpenCloseItem(OutputMonitorItem item, Group group)
        {
            var openCloseItem = GetOpenCloseItem(item);

            if (openCloseItem == null)
                return new List<UCReadOnlyOutputItem>();

            var items = new List<UCReadOnlyOutputItem>
            {
                CreateOutputItem(new OutputMonitorItem()
                {
                    ItemType = item.ItemType,
                    Name = item.Name + (item.PowerWindow != null ? "_UP" : "_OPEN"),
                    MessageID = item.MessageID,
                    OpenData = openCloseItem.EnableOpenData,
                    CloseData = openCloseItem.DisableOpenData,
                    ReadDiagData = openCloseItem.ReadOpenDiagData
                }, group),
                CreateOutputItem(new OutputMonitorItem()
                {
                    ItemType = item.ItemType,
                    Name = item.Name + (item.PowerWindow != null ? "_DOWN" : "_CLOSE"),
                    MessageID = item.MessageID,
                    OpenData = openCloseItem.EnableCloseData,
                    CloseData= openCloseItem.DisableCloseData,
                    ReadDiagData = openCloseItem.ReadCloseDiagData
                }, group),
            };

            foreach (var it in items)
                ((FormMonitorEnvInput)Application.OpenForms[Constants.Form_Monitor_Env_Input]).AddNewOutputMonitorItem(it.Item, group);

            return items;
        }

        /// <summary>
        /// Gets the open/close item details from the provided output monitor item.
        /// </summary>
        /// <param name="item">The output monitor item.</param>
        /// <returns>The open/close item details, if available; otherwise, null.</returns>
        private OpenCloseItem GetOpenCloseItem(OutputMonitorItem item)
        {
            if(item.Sunroof != null)
                return item.Sunroof;
            else if (item.PowerWindow != null)
                return item.PowerWindow;
            else return null;
        }

        /// <summary>
        /// Creates output items for loopback functionality from a given item and group.
        /// </summary>
        /// <param name="item">The output monitor item containing loopback data.</param>
        /// <param name="group">The group to add the output items to.</param>
        /// <returns>List of read-only output items created for loopback functionality.</returns>
        private List<UCReadOnlyOutputItem> CreateOutputItemsFromLoopBack(OutputMonitorItem item, Group group)
        {
            if (item.Loopback.Pair2Data == null)
                return new List<UCReadOnlyOutputItem>();

            var items = new List<UCReadOnlyOutputItem>
            {
                CreateOutputItem(new OutputMonitorItem()
                {
                    ItemType = item.ItemType,
                    Name = item.Loopback.Pair1Name,
                    MessageID = item.MessageID,
                    Loopback = new Loopback
                    {
                        Pair1Data = item.Loopback.Pair1Data,
                        Pair1Name = item.Loopback.Pair1Name,
                        Verification = item.Loopback.Verification,
                    }
                }, group),
                CreateOutputItem(new OutputMonitorItem()
                {
                    ItemType = item.ItemType,
                    Name = item.Loopback.Pair2Name,
                    MessageID = item.MessageID,
                    Loopback = new Loopback
                    {
                        Pair1Data = item.Loopback.Pair2Data,
                        Pair1Name = item.Loopback.Pair2Name,
                        Verification = item.Loopback.Verification,
                    }
                }, group),
            };

            foreach (var it in items)
                ((FormMonitorEnvInput)Application.OpenForms[Constants.Form_Monitor_Env_Input]).AddNewOutputMonitorItem(it.Item, group);

            return items;
        }

        /// <summary>
        /// Creates output items for power mirror controls from a given item and group.
        /// </summary>
        /// <param name="item">The output monitor item containing power mirror data.</param>
        /// <param name="group">The group to add the output items to.</param>
        /// <returns>List of read-only output items created for power mirror controls.</returns>
        private List<UCReadOnlyOutputItem> CreateOutputItemsFromPowerMirror(OutputMonitorItem item, Group group)
        {
            if(item.PowerMirror == null)
                return new List<UCReadOnlyOutputItem>();

            var items = new List<UCReadOnlyOutputItem>
            {
                CreateOutputItem(new OutputMonitorItem()
                {
                    ItemType = item.ItemType,
                    Name = item.Name + "_Up",
                    MessageID = item.MessageID,
                    OpenData = item.PowerMirror.SetOpenUp,
                    CloseData = item.PowerMirror.SetCloseUp,
                    ReadDiagData = item.PowerMirror.ReadUp
                }, group),
                CreateOutputItem(new OutputMonitorItem()
                {
                    ItemType = item.ItemType,
                    Name = item.Name + "_Down",
                    MessageID = item.MessageID,
                    OpenData = item.PowerMirror.SetOpenDown,
                    CloseData = item.PowerMirror.SetCloseDown,
                    ReadDiagData = item.PowerMirror.ReadLeftDown
                }, group),
                CreateOutputItem(new OutputMonitorItem()
                {
                    ItemType = item.ItemType,
                    Name = item.Name + "_Left",
                    MessageID = item.MessageID,
                    OpenData = item.PowerMirror.SetOpenLeft,
                    CloseData = item.PowerMirror.SetCloseLeft,
                    ReadDiagData = item.PowerMirror.ReadLeftDown
                }, group),
                CreateOutputItem(new OutputMonitorItem()
                {
                    ItemType = item.ItemType,
                    Name = item.Name + "_Right",
                    MessageID = item.MessageID,
                    OpenData = item.PowerMirror.SetOpenRight,
                    CloseData = item.PowerMirror.SetCloseRight,
                    ReadDiagData = item.PowerMirror.ReadRight
                }, group)
            };

            foreach (var it in items)
                ((FormMonitorEnvInput)Application.OpenForms[Constants.Form_Monitor_Env_Input]).AddNewOutputMonitorItem(it.Item,group);

            return items;
        }

        /// <summary>
        /// Creates the User Control object
        /// </summary>
        /// <param name="item">Monitor Item</param>
        /// <param name="group">Item Group</param>
        /// <returns>User control</returns>
        private UCReadOnlyOutputItem CreateOutputItem(OutputMonitorItem item, Group group)
        {
            var ucItem = new UCReadOnlyOutputItem(item, !string.IsNullOrEmpty(item.MessageID) ? item.MessageID : (monitorConfig.GenericMonitorConfiguration.OutputSection.CommonConfig?.MessageID));

            ucItem.GroupName = group.Name;
            ucItem.RegisterDict = CreateRegisterDict(item);
            ucItem.Name = $"uc_{group.Name}_{item.Name}";
            ucItem.Click += UcItem_Click;

            return ucItem;
        }

        /// <summary>
        /// User control click event to show the details in status section
        /// </summary>
        /// <param name="sender">User control</param>
        /// <param name="e">Event args</param>
        private void UcItem_Click(object sender, EventArgs e)
        {
            var ucOutputItem = (UCReadOnlyOutputItem)sender;
            ucOutputItem.Focus();
            lblItemName.Text = $"{ucOutputItem.Item.Name}";
            lblData.Text = ucOutputItem.StatusValue;

            if (ucOutputItem.StatusValue != "-")
                lblDataHeader.Visible = lblData.Visible = true;
            else lblDataHeader.Visible = lblData.Visible = false;
        }

        /// <summary>
        /// Checks the TX response message
        /// </summary>
        /// <param name="SID">SID of CAN message</param>
        /// <param name="NRC">Negative response code</param>
        /// <returns>Resonse status</returns>
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
        /// Creates a dictionary to handle the output register data
        /// </summary>
        /// <param name="item">Monitor item</param>
        /// <returns>Dictionary object</returns>
        private Dictionary<short, byte> CreateRegisterDict(OutputMonitorItem item)
        {
            Dictionary<short, byte> dict = new Dictionary<short, byte>();
            var messageId = Helper.StringToByteArray(monitorConfig.GenericMonitorConfiguration.OutputSection.CommonConfig.MessageID);

            var groupOffset = monitorConfig.GenericMonitorConfiguration.OutputSection.CommonConfig.InputRegisterGroupOffset - messageId.Length;
            var groupLength = monitorConfig.GenericMonitorConfiguration.OutputSection.CommonConfig.InputRegisterGroupLength;

            if (item.SetPWMData?.Length > 0)
                dict.Add((short)(Helper.GetValueOfPrimitive(item.SetPWMData, groupOffset, groupLength)), item.SetPWMData[4]);
            if (item.ReadDiagData?.Length > 0)
                dict.Add((short)(Helper.GetValueOfPrimitive(item.ReadDiagData, groupOffset, groupLength)), item.ReadDiagData[4]);
            if (item.ReadADCData?.Length > 0)
                dict.Add((short)(Helper.GetValueOfPrimitive(item.ReadADCData, groupOffset, groupLength)), item.ReadADCData[4]);
            if (item.ReadCurrentData?.Length > 0)
                dict.Add((short)(Helper.GetValueOfPrimitive(item.ReadCurrentData, groupOffset, groupLength)), item.ReadCurrentData[4]);
            if (item.OpenData?.Length > 0)
                dict.Add((short)(Helper.GetValueOfPrimitive(item.OpenData, groupOffset, groupLength)), item.OpenData[4]);
            if (item.CloseData?.Length > 0)
                dict.Add((short)(Helper.GetValueOfPrimitive(item.CloseData, groupOffset, groupLength)), item.CloseData[4]);
            if (item.SendData?.Length > 0)
                dict.Add((short)(Helper.GetValueOfPrimitive(item.SendData, groupOffset, groupLength)), item.SendData[4]);
            if (item.PEPSData?.Length > 0)
                dict.Add(BitConverter.ToInt16(new byte[] { item.PEPSData[2], 0 }, 0), item.PEPSData[2]);

            if (item.ItemType == "Loopback")
            {
                dict.Add((short)(Helper.GetValueOfPrimitive(item.Loopback.Pair1Data, groupOffset, groupLength)), item.Loopback.Pair1Data[4]);
                dict.Add((short)(Helper.GetValueOfPrimitive(item.Loopback.Verification, groupOffset, groupLength)), item.Loopback.Pair1Data[4]);
            }

            return dict;
        }

        /// <summary>
        /// Checks the registration
        /// </summary>
        /// <param name="dict">Dictionary</param>
        /// <param name="registerGroup">Register Group</param>
        /// <param name="registerAddress">Register Address</param>
        /// <returns>true if dict has value</returns>
        private bool CheckRegistration(Dictionary<short, byte> dict, short registerGroup, byte registerAddress)
        {
            if (dict.TryGetValue(registerGroup, out byte address))
                if (address == registerAddress)
                    return true;

            return false;
        }

        /// <summary>
        /// Changes the border color of the FlowLayoutPanel groups
        /// </summary>
        /// <param name="sender">A reference to the FlowlayoutPanel instance to be painted.</param>
        /// <param name="e">A reference to the Paint event's arguments.</param>
        private void pnlMonitorInput_Paint(object sender, PaintEventArgs e)
        {
            FlowLayoutPanel panel = sender as FlowLayoutPanel;
            if (panel != null && panel.Visible)
            {
                ControlPaint.DrawBorder(e.Graphics, panel.ClientRectangle, Color.LightGray, ButtonBorderStyle.Solid);
            }
        }

        #endregion

        private void pnlMonitorOutput_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
