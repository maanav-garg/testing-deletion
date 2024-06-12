using AutosarBCM.Config;
using AutosarBCM.Core;
using AutosarBCM.Forms.Monitor;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Windows.Forms;

namespace AutosarBCM.UserControls.Monitor
{
    /// <summary>
    /// Represents a user control for displaying read-only output information.
    /// </summary>
    public partial class UCReadOnlyItem : UserControl
    {
        #region Variables

        /// <summary>
        /// Represents an output monitor item associated with this control.
        /// </summary>
        public OutputMonitorItem Item;
        /// <summary>
        /// Represents a control item associated with this control.
        /// </summary>
        public Core.ControlInfo ControlInfo { get; set; }
        public PayloadInfo PayloadInfo { get; set; }
        /// <summary>
        /// Gets or sets the group name of the control.
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// Gets or sets a dictionary that maps register values to their corresponding bytes.
        /// </summary>
        public Dictionary<short, byte> RegisterDict { get; set; } = new Dictionary<short, byte>();

        /// <summary>
        /// Gets the status value from the control's label, or "-" if the label is empty.
        /// </summary>
        public string StatusValue { get { return String.IsNullOrEmpty(lblStatus.Text) ? "-" : lblStatus.Text; } }

        /// <summary>
        /// Gets or sets the message ID associated with this control.
        /// </summary>
        public string MessageID { get; set; }

        /// <summary>
        /// The data group used for sending data.
        /// </summary>
        private short sendDataGroup = 0;

        /// <summary>
        /// Represents the current value as a tuple containing text and color.
        /// </summary>
        private Tuple<string, Color> currentValue;

        private float MessagesReceived;
        private float MessagesTransmitted;

        /// <summary>
        /// An array to store RSSI (Received Signal Strength Indicator) measurement values.
        /// </summary>
        private float[] rssiValues = new float[3];

        /// <summary>
        /// Gets or sets the previous (old) value of the input item.
        /// </summary>
        private IOControlByIdentifierService oldValue;
        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the UCReadOnlyOutputItem class with the specified OutputMonitorItem and message ID.
        /// </summary>
        /// <param name="item">The OutputMonitorItem associated with this control.</param>
        /// <param name="messageID">The message ID to be used for communication.</param>
        public UCReadOnlyItem(OutputMonitorItem item,string messageID)
        {
            InitializeComponent();            

            this.Item = item;

            MessageID = item.MessageID = messageID;

            if (item.Name.Length > 23)
                lblName.Text = $"{item.Name.Substring(0, 20)}...";
            else
                lblName.Text = item.Name;

            if (Item.SendData?.Length > 0)
                sendDataGroup = (short)(Helper.GetValueOfPrimitive(Item.SendData, 2, 2));

            if (item.ItemType == "PEPS")
            {
                lblDiff.Visible = false;
                label1.Visible = false;
                lblTransmitted.Visible = false;
            }

            currentValue = new Tuple<string, Color>(lblStatus.Text, lblStatus.ForeColor);
        }

        public UCReadOnlyItem(Core.ControlInfo controlInfo, PayloadInfo payloadInfo)
        {
            InitializeComponent();
            ControlInfo= controlInfo;
            PayloadInfo = payloadInfo;
          
            if (controlInfo.Name.Length > 30)
                lblParent.Text = $"{controlInfo.Name.Substring(0, 27)}...";
            else
                lblParent.Text = controlInfo.Name;

            if (payloadInfo.Name.Length > 30)
                lblName.Text = $"{payloadInfo.Name.Substring(0, 27)}...";
            else
                lblName.Text = payloadInfo.Name;
        }

        #endregion

        #region Public Methods


        /// <summary>
        /// Change status of the input window regarding to read data from the device.
        /// </summary>
        /// <param name="monitorItem">Monitor item to be updated</param>
        /// <param name="inputResponse">Data comes from device</param>
        public void ChangeStatus(IOControlByIdentifierService service)
        {
           
            
            lblReceived.BeginInvoke((MethodInvoker)delegate ()
            {
                MessagesReceived++;
                lblReceived.Text = MessagesReceived.ToString();
            });

            if (oldValue != null)
            {
                bool areEqual = service.Payloads.Count == oldValue.Payloads.Count;

                if (areEqual)
                {
                    for (int i = 0; i < service.Payloads.Count; i++)
                    {
                        if (service.Payloads[i].FormattedValue != oldValue.Payloads[i].FormattedValue ||
                            service.Payloads[i].PayloadInfo.Name != oldValue.Payloads[i].PayloadInfo.Name)
                        {
                            areEqual = false;
                            break;
                        }
                    }
                }

                if (areEqual)
                    return;
            }

            oldValue = service;

            lblStatus.BeginInvoke((MethodInvoker)delegate ()
            {
                if (service.Payloads[0].PayloadInfo.TypeName == "DID_PWM")
                {
                    var payload = (service.Payloads.FirstOrDefault(x => x.PayloadInfo.Name == PayloadInfo.Name)).FormattedValue;

                    string hexValue = payload.Replace("-", "");
                    string decimalValue = (Convert.ToInt32(hexValue, 16)).ToString();
                    lblWriteStatus.Text = decimalValue;
                }
                else
                {
                    var payload = service.Payloads.FirstOrDefault(x => x.PayloadInfo.Name == PayloadInfo.Name);
                    lblWriteStatus.Text = payload?.FormattedValue.ToString();
                }
            });
        }

        /// <summary>
        /// Handle number of transmitted data.
        /// </summary>
        /// <param name="monitorItem">Monitor item to be updated</param>
        /// <param name="inputResponse">Data comes from device</param>
        internal void HandleMetrics()
        {
            MessagesTransmitted++;

            lblTransmitted.BeginInvoke((MethodInvoker)delegate ()
            {
                lblTransmitted.Text = MessagesTransmitted.ToString();
            });
        }

        /// <summary>
        /// Change status of the input window regarding to read data from the device.
        /// </summary>
        /// <param name="monitorItem">Monitor item to be updated</param>
        /// <param name="inputResponse">Data comes from device</param>
        public void ChangeStatus(Response outputResponse, MessageDirection messageDirection)
        {
            UpdateCounters(outputResponse, messageDirection);
            if (messageDirection == MessageDirection.TX) return;

            var oldValue = currentValue;

            if (Item.ItemType == "Digital" || Item.ItemType == "FET")
            {
                currentValue = DigitalCheck(outputResponse);
            }
            else if (Item.ItemType == "PWM")
            {
                currentValue = PwmCheck(outputResponse);
            }
            else if (Item.ItemType == "Power Mirror")
            {
                currentValue = PowerMirrorCheck(outputResponse);
            }
            else if (Item.ItemType == "Loopback")
            {
                currentValue = LoopbackCheck(outputResponse);
            }
            else if (Item.ItemType == "Wiper")
            {
                currentValue = WiperCheck(outputResponse);
            }
            else if (Item.ItemType == "Power Window" ||  Item.ItemType == "Sunroof")
            {
                currentValue = OpenCloseItemCheck(outputResponse);
            }
            else if(outputResponse.RegisterGroup == sendDataGroup)
            {
                currentValue = SendDataCheck(outputResponse);
            }
            else if(Item.ItemType == "DoorControl")
            {
                currentValue = SendDoorControlCheck(outputResponse);
            }
            else if(Item.ItemType == "PEPS")
            {
                currentValue = SendPEPSCheck(outputResponse);
            }

            if (Program.MappingStateDict.TryGetValue(Item.Name, out var errorLogDetect))
                Program.MappingStateDict.UpdateValue(Item.Name, errorLogDetect.UpdateOutputResponse(errorLogDetect.Operation, MappingState.OutputReceived, GetMappingLogState(errorLogDetect.Operation))) ;

            if (oldValue.Item1 != currentValue.Item1 && oldValue.Item2 != currentValue.Item2)
                Program.MainForm.AppendTrace($"{Item.Name}: ({currentValue})");

            else 
                return;

            if (lblStatus.InvokeRequired)
            {
                lblStatus.BeginInvoke((MethodInvoker)delegate ()
                {
                    lblStatus.Text = currentValue.Item1;
                    lblStatus.ForeColor = currentValue.Item2;
                });
            }
            else
            {
                lblStatus.Text = currentValue.Item1;
                lblStatus.ForeColor = currentValue.Item2;
            }
        }

        #endregion

        #region Private Methods

        private MappingResponse GetMappingLogState(MappingOperation operation)
        {
            if (operation == MappingOperation.Open && (currentValue.Item1 == "ON" || currentValue.Item1 == "SET"))
                return MappingResponse.OutputOpen;
            else if (operation == MappingOperation.Close && (currentValue.Item1 == "OFF" || ((Item.PwmTag == "XS4200" && currentValue.Item1 == "ON") || currentValue.Item1 == "SET")))
                return MappingResponse.OutputClose;
            else
                return MappingResponse.OutputError;
        }

        private void UpdateCounters(Response outputResponse, MessageDirection messageDirection)
        {
            if (messageDirection == MessageDirection.TX)
            {
                if (outputResponse.RegisterGroup == (short)Output_ReadGroup.LOOPBACK_RESULT) return;
                if (Item.ItemType != "PEPS")
                    MessagesTransmitted++;
            }
            else
                MessagesReceived++;

            Invoke(new Action(() =>
            {
                lblTransmitted.Text = MessagesTransmitted.ToString();
                lblReceived.Text = MessagesReceived.ToString();

                var diff = MessagesReceived / MessagesTransmitted;
                lblDiff.Text = diff.ToString("P2");
                lblDiff.BackColor = diff == 1 ? Color.Green : (diff > 0.9 ? Color.Orange : Color.Red);
            }));
        }

        /// <summary>
        /// Set ADC return of response
        /// </summary>
        /// <param name="group">Register group</param>
        /// <param name="value">Respone value</param>
        private string GetDigitalReadADCResponseData(short group, short value)
        {
            switch (group)
            {
                case (short)Output_ReadGroup.ADC:
                    return value.ToString() + Constants.MilliVolt;
                case (short)Output_ReadGroup.DIO:
                    return ((DIO_ReadResponse)value).ToString();
                case (short)Output_ReadGroup.PWHALL:
                    return value.ToString();
                default:
                    return string.Empty;
            }
        }

        /// <summary>
        /// Set ADC current value return of response
        /// </summary>
        /// <param name="group">Register group</param>
        /// <param name="value">Respone value</param>
        private string GetDigitalReadCurrentValueResponseData(short group, short value)
        {
            switch (group)
            {
                case (short)Output_ReadGroup.CURRENTVALUE:
                    return value.ToString();
                default:
                    return string.Empty;
            }
        }

        /// <summary>
        /// Set Diag return of response
        /// </summary>
        /// <param name="group">Register group</param>
        /// <param name="value">Respone value</param>
        private string GetDigitalReadDiagResponseData(short group, short value)
        {
            switch (group)
            {
                case (short)Output_ReadGroup.BBT_BTS:
                    return ((BBT_BTS_ReadResponse)value).ToString();
                case (short)Output_ReadGroup.SWCH:
                    return ((SWCH_ReadResponse)value).ToString();
                case (short)Output_ReadGroup.EO:
                    return ((EO_ReadResponse)value).ToString();
                case (short)Output_ReadGroup.VND5T035LAK:
                    return ((VND5T035LAK_ReadResponse)value).ToString();
                case (short)Output_ReadGroup.HallSensorLeftSupply_0_5A:
                case (short)Output_ReadGroup.HallSensorRightSupply_0_5A:
                    return ((HallSensor_ReadResponse)value).ToString();
                case (short)Output_ReadGroup.DoorLockLeft:
                case (short)Output_ReadGroup.DoorLockRight:
                    return ((DoorLock_ReadResponse)value).ToString();
                case (short)Output_ReadGroup.HSD:
                    return ((HSD_ReadResponse)value).ToString();
                case (short)Output_ReadGroup.MPQ6528:
                    return ((MPQ6528_ReadResponse)value).ToString();
                case (short)Output_ReadGroup.XS4200:
                    return ((XS4200)value).ToString();
                default:
                    return string.Empty;
            }
        }

        /// <summary>
        /// Checks the response for ADC or Diagnostics data, writes log messages, and returns the current status.
        /// </summary>
        /// <param name="outputResponse">The response received from the device.</param>
        /// <returns>
        /// A tuple containing the current status text and color.
        /// </returns>
        private Tuple<string, Color> CheckAdcDiagCurrentResponse(Response outputResponse)
        {
            string response = null;
            string function = string.Empty;
            if (Item.ReadADCData?.Length > 0)
            {
                response = GetDigitalReadADCResponseData(outputResponse.RegisterGroup, outputResponse.ResponseData);
                if (!string.IsNullOrEmpty(response))
                    Helper.WriteCycleMessageToLogFile(Item.Name, Item.ItemType, Constants.ReadADCData, "", "", response);
                else
                    function = Constants.ReadADCData;

            }
            if (Item.ReadCurrentData?.Length > 0)
            {
                response = GetDigitalReadCurrentValueResponseData(outputResponse.RegisterGroup, outputResponse.ResponseData);
                if (!string.IsNullOrEmpty(response))
                    Helper.WriteCycleMessageToLogFile(Item.Name, Item.ItemType, Constants.ReadCurrentValue, "", "", response);
                else
                    function = Constants.ReadCurrentValue;

            }
            if (Item.ReadDiagData?.Length > 0 && string.IsNullOrEmpty(response))
            {
                response = GetDigitalReadDiagResponseData(outputResponse.RegisterGroup, outputResponse.ResponseData);
                if (!string.IsNullOrEmpty(response))
                    Helper.WriteCycleMessageToLogFile(Item.Name, Item.ItemType, Constants.ReadDiagData, "", "", response);
                else 
                    function = Constants.ReadDiagData;
            }
            
            if (string.IsNullOrEmpty(response))
            {
                Helper.WriteCycleMessageToLogFile(Item.Name, Item.ItemType, function, "", "", Constants.Unexpected);
                Helper.WriteErrorMessageToLogFile(Item.Name, Item.ItemType, function, "", "", Constants.Unexpected);
            }

            return new Tuple<string, Color>(lblStatus.Text, lblStatus.ForeColor);
        }

        /// <summary>
        /// Digital check for the received data
        /// </summary>
        /// <param name="monitorItem">Monitor item to be updated</param>
        private Tuple<string, Color> DigitalCheck(Response outputResponse)
        {
            if (outputResponse.RegisterGroup == (short)Output_OpenCloseGroup.EO_On || outputResponse.RegisterGroup == (short)Output_OpenCloseGroup.DIO_Open)
            {
                if (outputResponse.ResponseData == (byte)DigitalUdsOnCan_OpenCloseValues.E_OK)
                {
                    Helper.WriteCycleMessageToLogFile(Item.Name, Item.ItemType, Constants.Response, "", "", DigitalUdsOnCan_OpenCloseValues.E_OK.ToString());
                    return new Tuple<string, Color>("ON", Color.Green);
                }

                else if (outputResponse.ResponseData == (byte)DigitalUdsOnCan_OpenCloseValues.E_NOT_OK)
                {
                    Program.MainForm.AppendTrace($"EO Command failed for {lblName.Text} operation");

                    Helper.WriteErrorMessageToLogFile(Item.Name, Item.ItemType, Constants.Response, "", "", DigitalUdsOnCan_OpenCloseValues.E_NOT_OK.ToString());
                    Helper.WriteCycleMessageToLogFile(Item.Name, Item.ItemType, Constants.Response, "", "", DigitalUdsOnCan_OpenCloseValues.E_NOT_OK.ToString());

                    return new Tuple<string, Color>("NOK", Color.Black);
                }

            } else if (outputResponse.RegisterGroup == (short)Output_OpenCloseGroup.EO_Off || outputResponse.RegisterGroup == (short)Output_OpenCloseGroup.DIO_Close)
            {
                if (outputResponse.ResponseData == (byte)DigitalUdsOnCan_OpenCloseValues.E_OK)
                {
                    Helper.WriteCycleMessageToLogFile(Item.Name, Item.ItemType, Constants.Response, "", "", DigitalUdsOnCan_OpenCloseValues.E_OK.ToString());
                    return new Tuple<string, Color>("OFF", Color.Red);
                }

                else if (outputResponse.ResponseData == (byte)DigitalUdsOnCan_OpenCloseValues.E_NOT_OK)
                {
                    Helper.WriteErrorMessageToLogFile(Item.Name, Item.ItemType, Constants.Response, "", "", DigitalUdsOnCan_OpenCloseValues.E_NOT_OK.ToString());
                    Helper.WriteCycleMessageToLogFile(Item.Name, Item.ItemType, Constants.Response, "", "", DigitalUdsOnCan_OpenCloseValues.E_NOT_OK.ToString());

                    Program.MainForm.AppendTrace($"EO Command failed for {lblName.Text} operation");

                    return new Tuple<string, Color>("NOK", Color.Black);
                }
            } else if (outputResponse.RegisterGroup == (short)Output_PwmGroup.XS4200)
            {
                if (outputResponse.ResponseData == (byte)XS4200PWM_SetResponse.E_OK)
                {
                    Helper.WriteCycleMessageToLogFile(Item.Name, Item.ItemType, Constants.Response, "", "", DigitalUdsOnCan_OpenCloseValues.E_OK.ToString());
                    return new Tuple<string, Color>("ON", Color.Green);
                }

                else if (outputResponse.ResponseData == (byte)XS4200PWM_SetResponse.E_NOT_OK)
                {
                    Program.MainForm.AppendTrace($"EO Command failed for {lblName.Text} operation");

                    Helper.WriteErrorMessageToLogFile(Item.Name, Item.ItemType, Constants.Response, "", "", DigitalUdsOnCan_OpenCloseValues.E_NOT_OK.ToString());
                    Helper.WriteCycleMessageToLogFile(Item.Name, Item.ItemType, Constants.Response, "", "", DigitalUdsOnCan_OpenCloseValues.E_NOT_OK.ToString());

                    return new Tuple<string, Color>("NOK", Color.Black);
                }
            }
            else if (outputResponse.RegisterGroup == (short)Output_OpenCloseGroup.Washer_Open) 
            {
                Helper.WriteCycleMessageToLogFile(Item.Name, Item.ItemType, Constants.Response, "", "", Constants.Enabled);
                return new Tuple<string, Color>("ON", Color.Green);
            }
            else if (outputResponse.RegisterGroup == (short)Output_OpenCloseGroup.Washer_Close)
            {
                Helper.WriteCycleMessageToLogFile(Item.Name, Item.ItemType, Constants.Response, "", "", Constants.Disabled);
                return new Tuple<string, Color>("OFF", Color.Red);
            }
            else if (outputResponse.RegisterGroup == (short)Output_OpenCloseGroup.WaterPump_Open)
            {
                Helper.WriteCycleMessageToLogFile(Item.Name, Item.ItemType, Constants.Response, "", "", Constants.Enabled);
                return new Tuple<string, Color>("ON", Color.Green);
            }
            else if (outputResponse.RegisterGroup == (short)Output_OpenCloseGroup.WaterPump_Close)
            {
                Helper.WriteCycleMessageToLogFile(Item.Name, Item.ItemType, Constants.Response, "", "", Constants.Disabled);
                return new Tuple<string, Color>("OFF", Color.Red);
            }

            return CheckAdcDiagCurrentResponse(outputResponse);
        }

        /// <summary>
        /// Analog upper and lower limit check for the received data
        /// </summary>
        /// <param name="monitorItem">Monitor item to be updated</param>
        private Tuple<string, Color> PwmCheck(Response outputResponse)
        {
            if ((outputResponse.RegisterGroup == (short)Output_PwmGroup.PWM_Set && outputResponse.ResponseData == (byte)PWM_SetResponse.E_OK) 
                || (outputResponse.RegisterGroup == (short)Output_PwmGroup.XS4200 && outputResponse.ResponseData == (byte)XS4200PWM_SetResponse.E_OK))
            {
                Helper.WriteCycleMessageToLogFile(Item.Name, Item.ItemType, Constants.Response, "", "", PWM_SetResponse.E_OK.ToString());
                return new Tuple<string, Color>("SET", Color.Green);
            }
            else if (outputResponse.RegisterGroup == (short)Output_PwmGroup.PWM_Set && outputResponse.ResponseData == (byte)PWM_SetResponse.E_NOT_OK
                || (outputResponse.RegisterGroup == (short)Output_PwmGroup.XS4200 && outputResponse.ResponseData == (byte)XS4200PWM_SetResponse.E_NOT_OK))
            {
                Program.MainForm.AppendTrace($"Set PWM Command failed for '{lblName.Text}'");

                Helper.WriteCycleMessageToLogFile(Item.Name, Item.ItemType, Constants.Response, "", "", PWM_SetResponse.E_NOT_OK.ToString());
                Helper.WriteErrorMessageToLogFile(Item.Name, Item.ItemType, Constants.Response, "", "", PWM_SetResponse.E_NOT_OK.ToString());

                return new Tuple<string, Color>("NOT SET", Color.Red);
            }

            return CheckAdcDiagCurrentResponse(outputResponse);
        }

        /// <summary>
        /// Checks the response for Power Mirror operation, writes log messages, and returns the current status.
        /// </summary>
        /// <param name="outputResponse">The response received from the device.</param>
        /// <returns>
        /// A tuple containing the current status text and color.
        /// </returns>
        private Tuple<string, Color> PowerMirrorCheck(Response outputResponse)
        {
            if (outputResponse.RegisterGroup == (short)Output_PowerMirror.SetOpen)
            {
                Helper.WriteCycleMessageToLogFile(Item.Name, Item.ItemType, Constants.Response, "", "", Constants.Enabled);
                return new Tuple<string, Color>("ON", Color.Green);
            }
            if (outputResponse.RegisterGroup == (short)Output_PowerMirror.SetClose)
            {
                Helper.WriteCycleMessageToLogFile(Item.Name, Item.ItemType, Constants.Response, "", "", Constants.Disabled);
                return new Tuple<string, Color>("OFF", Color.Red);
            }

            return CheckAdcDiagCurrentResponse(outputResponse);
        }

        /// <summary>
        /// Checks the response for Loopback operation, writes log messages, and returns the current status.
        /// </summary>
        /// <param name="outputResponse">The response received from the device.</param>
        /// <returns>
        /// A tuple containing the current status text and color.
        /// </returns>
        private Tuple<string, Color> LoopbackCheck(Response outputResponse)
        {
            if (outputResponse.RegisterGroup == (short)Output_ReadGroup.LOOPBACK)
            {
                MessagesTransmitted++;
                Item.SendLoopbackVerification();
                Helper.WriteCycleMessageToLogFile(Item.Name, Item.ItemType, Constants.Loopback, "", "", Constants.LoopbackDataSent);
            }
            if (outputResponse.RegisterGroup == (short)Output_ReadGroup.LOOPBACK_RESULT)
            {
                Helper.WriteCycleMessageToLogFile(Item.Name, Item.ItemType, Constants.Loopback, "", "", ((Loopback_Response)outputResponse.ResponseData).ToString());
                return new Tuple<string, Color>(((Loopback_Response)outputResponse.ResponseData).ToString(), Color.Black);
            }

            return new Tuple<string, Color>(lblStatus.Text, lblStatus.ForeColor);
        }

        /// <summary>
        /// Checks the response for Open/Close operation, writes log messages, and returns the current status.
        /// </summary>
        /// <param name="outputResponse">The response received from the device.</param>
        /// <returns>
        /// A tuple containing the current status text and color.
        /// </returns>
        private Tuple<string, Color> OpenCloseItemCheck(Response outputResponse)
        {
            if (outputResponse.RegisterGroup == (short)Output_OpenCloseGroup.Sunroof_Open || outputResponse.RegisterGroup == (short)Output_OpenCloseGroup.PowerWindow_Open)
            {
                Helper.WriteCycleMessageToLogFile(Item.Name, Item.ItemType, Constants.Response, "", "", Constants.Enabled);
                return new Tuple<string, Color>("ON", Color.Green);
            }
            if (outputResponse.RegisterGroup == (short)Output_OpenCloseGroup.Sunroof_Close || outputResponse.RegisterGroup == (short)Output_OpenCloseGroup.PowerWindow_Close)
            {
                Helper.WriteCycleMessageToLogFile(Item.Name, Item.ItemType, Constants.Response, "", "", Constants.Disabled);
                return new Tuple<string, Color>("OFF", Color.Red);
            }

            return CheckAdcDiagCurrentResponse(outputResponse);
        }

        /// <summary>
        /// Checks the response for sending data, writes log messages, and returns the current status.
        /// </summary>
        /// <param name="outputResponse">The response received from the device.</param>
        /// <returns>
        /// A tuple containing the current status text and color.
        /// </returns>
        private Tuple<string, Color> SendDataCheck(Response outputResponse)
        {
            if (outputResponse.RegisterGroup == sendDataGroup)
            {
                Helper.WriteCycleMessageToLogFile(Item.Name, Item.ItemType, Constants.Loopback, "", "", Constants.Response);
                return new Tuple<string, Color>("Sent", Color.Green);
            }

            return new Tuple<string, Color>(lblStatus.Text, lblStatus.ForeColor);
        }

        /// <summary>
        /// Checks the response for Wiper operation, writes log messages, and returns the current status.
        /// </summary>
        /// <param name="outputResponse">The response received from the device.</param>
        /// <returns>
        /// A tuple containing the current status text and color.
        /// </returns>
        private Tuple<string, Color> WiperCheck(Response outputResponse)
        {
            if (outputResponse.RegisterGroup == (short)Output_ReadGroup.Wiper
                && (
                    outputResponse.RegisterAddress == (byte)WIPER_ID.STOP_LOW
                    || outputResponse.RegisterAddress == (byte)WIPER_ID.STOP_HIGH
                    || outputResponse.RegisterAddress == (byte)WIPER_ID.LOW_STOP
                    || outputResponse.RegisterAddress == (byte)WIPER_ID.HIGH_STOP
                ))
            {
                Helper.WriteCycleMessageToLogFile(Item.Name, Item.ItemType, Constants.Response, "", "", Constants.WiperSet);
                return new Tuple<string, Color>("SET", Color.Green);
            }

            return CheckAdcDiagCurrentResponse(outputResponse);
        }

        /// <summary>
        /// Checks the response for sending Door Control commands, writes log messages, and returns the current status.
        /// </summary>
        /// <param name="outputResponse">The response received from the device.</param>
        /// <returns>
        /// A tuple containing the current status text and color.
        /// </returns>
        private Tuple<string, Color> SendDoorControlCheck(Response outputResponse)
        {
            if (outputResponse.RegisterGroup == (short)DoorControls.Enable)
            {
                Helper.WriteCycleMessageToLogFile(Item.Name, Item.ItemType, Constants.Doorlock, "", "", Constants.Enabled);
                return new Tuple<string, Color>("ON", Color.Green);
            }else if (outputResponse.RegisterGroup == (short)DoorControls.Disable)
            {
                Helper.WriteCycleMessageToLogFile(Item.Name, Item.ItemType, Constants.Doorlock, "", "", Constants.Disabled);
                return new Tuple<string, Color>("OFF", Color.Red);
            }

            return CheckAdcDiagCurrentResponse(outputResponse);
        }
        
        /// <summary>
        /// Checks the response for sending PEPS commands, writes log messages, and returns the current status.
        /// </summary>
        /// <param name="outputResponse">The response received from the device.</param>
        /// <returns>
        /// A tuple containing the current status text and color.
        /// </returns>
        private Tuple<string, Color> SendPEPSCheck(Response outputResponse)
        {
            var pepsResponse = outputResponse as PEPSResponse;
            if (pepsResponse.RegisterAddress == 3)
            {
                if (pepsResponse.RawData[9] == 0)
                {
                    rssiValues[0] = pepsResponse.ResponseData32 / 1000;
                    Helper.WriteCycleMessageToLogFile(Item.Name, Item.ItemType, Constants.Measurement, "", "", "X:"+rssiValues[0]);
                }
                else if (pepsResponse.RawData[9] == 1)
                {
                    rssiValues[1] = pepsResponse.ResponseData32 / 1000;
                    Helper.WriteCycleMessageToLogFile(Item.Name, Item.ItemType, Constants.Measurement, "", "", "Y:" + rssiValues[1]);
                }
                else if (pepsResponse.RawData[9] == 2)
                {
                    rssiValues[2] = pepsResponse.ResponseData32 / 1000;
                    Helper.WriteCycleMessageToLogFile(Item.Name, Item.ItemType, Constants.Measurement, "", "", "Z:" + rssiValues[2]);
                    Helper.WriteCycleMessageToLogFile(Item.Name, Item.ItemType, Constants.Measurement, "", "", "SQSUM:" + Math.Sqrt(Math.Pow(rssiValues[0], 2) + Math.Pow(rssiValues[1], 2) + Math.Pow(rssiValues[2], 2)));
                }

                var message = $"X:{rssiValues[0]} - Y:{rssiValues[1]} - Z:{rssiValues[2]} - SQSUM:{Math.Sqrt(Math.Pow(rssiValues[0], 2) + Math.Pow(rssiValues[1], 2) + Math.Pow(rssiValues[2], 2))}";

                return new Tuple<string, Color>(message,Color.Green);
            }else if (pepsResponse.RegisterAddress == 2)
            {
                var message = Helper.ByteArrayToString(pepsResponse.RawData.Skip(5).Take(4).ToArray());

                Helper.WriteCycleMessageToLogFile(Item.Name, Item.ItemType, Constants.KeyfobID, "", "", message); 
                MonitorUtil.NewMessageReceived(outputResponse.RawData);
                return new Tuple<string, Color>(message, Color.Green);
            }else if (pepsResponse.RegisterAddress == 1)
            {
                var message = string.Empty;
                if (pepsResponse.RawData[9] == 0)
                {
                    message = Helper.ByteArrayToString(pepsResponse.RawData.Skip(5).Take(4).ToArray());
                }
                else if (pepsResponse.RawData[9] == 1)
                {
                    message = $"Identifier: {GetButtonIdentifier(pepsResponse.RawData[5])}, " +
                        $"Receive: {((float)((short)Helper.GetValueOfPrimitive(pepsResponse.RawData, 6, 2)) / 10)} s," +
                        $" Battery Level: {((pepsResponse.RawData[8] == 0x01) ? "OK" : "NOK")}";
                }
                Helper.WriteCycleMessageToLogFile(Item.Name, Item.ItemType, Constants.PEPS_Read_Keyfob, "", "", message);
                return new Tuple<string, Color>(message, Color.Green);
            }
            else if (pepsResponse.RegisterAddress == 4)
            {
                var message = Helper.ByteArrayToString(pepsResponse.RawData.Skip(5).Take(4).ToArray());

                Helper.WriteCycleMessageToLogFile(Item.Name, Item.ItemType, Constants.PEPS_Immobilizer, "", "", message);

                return new Tuple<string, Color>(message, Color.Green);
            }
            else if (pepsResponse.RegisterAddress == 5)
            {
                if (pepsResponse.RawData[2] == 0x03 && pepsResponse.RawData[3] == 0xEF && pepsResponse.RawData[4] == 0x05)
                {
                    var message = string.Empty;
                    if (pepsResponse.RawData[5] == 0x01)
                        message = "WUP detected at DoorCapSensor1";
                    if (pepsResponse.RawData[5] == 0x02)
                        message = "WUP detected at DoorCapSensor2";

                    Helper.WriteCycleMessageToLogFile(Item.Name, Item.ItemType, Constants.PEPS_Door_Cap_Sensor, "", "", message);
                    return new Tuple<string, Color>(message, Color.Green);
                }
            }
            else if (pepsResponse.RegisterAddress == 6)
            {
                var message = ((float)(pepsResponse.ResponseData2) / 1000).ToString() + " C";

                Helper.WriteCycleMessageToLogFile(Item.Name, Item.ItemType, Constants.PEPS_Temperature_Measurement, "", "", message);
                return new Tuple<string, Color>(message, Color.Green);
            }

            return new Tuple<string, Color>(lblStatus.Text, lblStatus.ForeColor);
        }

        /// <summary>
        /// Label click event
        /// </summary>
        /// <param name="sender">label</param>
        /// <param name="e">Event args</param>
        private void lblName_Click(object sender, EventArgs e)
        {
            this.InvokeOnClick(this, new EventArgs());
        }

        /// <summary>
        /// Gets the button identifier based on the provided value.
        /// </summary>
        /// <param name="value">The value to determine the button identifier.</param>
        /// <returns>The button identifier string.</returns>
        private string GetButtonIdentifier(byte value)
        {
            if (value == 1) return "Button 1";
            else if (value == 2) return "Button 2";
            else if (value == 4) return "Button 3";
            return string.Empty;
        }
        #endregion

        private void lblWriteStatus_Click(object sender, EventArgs e)
        {

        }
    }
}