using AutosarBCM.Config;
using AutosarBCM.Message;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace AutosarBCM.UserControls.Monitor
{
    /// <summary>
    /// Represents a user control for displaying and interacting with input items.
    /// </summary>
    public partial class UCItem : UserControl
    {
        #region Variables

        /// <summary>
        /// Gets or sets the associated InputMonitorItem for this control.
        /// </summary>
        public InputMonitorItem Item;

        /// <summary>
        /// Gets or sets the group name associated with the control.
        /// </summary>
        public string MessageID { get; set; }

        /// <summary>
        /// Gets or sets the group name associated with the control.
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// Gets or sets the current value of the input item.
        /// </summary>
        private double currentValue = -1;

        /// <summary>
        /// Gets or sets a boolean indicating whether the input is being logged.
        /// </summary>
        public bool IsLogged = false;

        /// <summary>
        /// Gets or sets the previous (old) value of the input item.
        /// </summary>
        private double oldValue = -1;
        private float MessagesReceived;
        private float MessagesTransmitted;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the UCItem class.
        /// </summary>
        /// <param name="item">The InputMonitorItem associated with this control.</param>
        /// <param name="commonConfig">The CommonConfig object used for configuration (optional).</param>
        public UCItem(InputMonitorItem item, CommonConfig commonConfig = null)
        {
            InitializeComponent();
            Item = item;
            if (item.Name.Length > 23)
                lblName.Text = $"{item.Name.Substring(0, 20)}...";
            else
                lblName.Text = item.Name;

            if (commonConfig != null)
            {
                Item.Coefficient = item.Coefficient > 0 ? item.Coefficient : commonConfig.InputCoefficient;
                Item.LowerLimit = item.LowerLimit > 0 ? item.LowerLimit : commonConfig.InputLowerLimit;
                Item.UpperLimit = item.UpperLimit > 0 ? item.UpperLimit : commonConfig.InputUpperLimit;
            }
            else
            {
                Item.Coefficient = item.Coefficient;
                Item.LowerLimit = item.LowerLimit;
                Item.UpperLimit = item.UpperLimit;
            }
            grpStatus.Visible = item.ItemType == "Digital" && Item.RegisterGroup == ((short)Output_ReadGroup.SWCH).ToString("X2")
                && Program.MainForm.MonitorTestType == MonitorTestType.Generic;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Change status of the input window regarding to read data from the device.
        /// </summary>
        /// <param name="monitorItem">Monitor item to be updated</param>
        /// <param name="inputResponse">Data comes from device</param>
        public void ChangeStatus(InputMonitorItem monitorItem, GenericResponse inputResponse, MessageDirection messageDirection)
        {
            UpdateCounters(messageDirection);
            if (messageDirection == MessageDirection.TX) return;

            oldValue = currentValue;

            if (lblStatus.InvokeRequired)
            {
                lblStatus.BeginInvoke((MethodInvoker)delegate ()
                {
                    UpdateStatus(monitorItem, inputResponse);
                    TraceLogDecision(monitorItem, oldValue);
                });
            }
            else
            {
                UpdateStatus(monitorItem, inputResponse);
                TraceLogDecision(monitorItem, oldValue);
            }            
        }

        #endregion

        #region Private Methods

        private void UpdateCounters(MessageDirection messageDirection)
        {
            if (messageDirection == MessageDirection.TX)
                MessagesTransmitted++;
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
        /// Digital check for the received data
        /// </summary>
        /// <param name="inputResponse">Response value</param>
        private void DigitalCheck(GenericResponse inputResponse)
        {
            if (inputResponse.RegisterGroup == (short)Set_Group.SET_STATUS)
            {
                if (inputResponse.ResponseData == (short)Status_Response.E_OK)
                    btnSetStatus.ForeColor = Color.Green;
                else
                    btnSetStatus.ForeColor = Color.Red;
                return;
            }

            if (inputResponse.RegisterGroup == 0x6116)
            {
                Helper.WriteCycleMessageToLogFile(Item.Name, Item.ItemType, Constants.Response, "", "", Constants.FS26IC);
                if (oldValue == inputResponse.ResponseData)
                    return;
                lblStatus.Text = $"0x{inputResponse.RawData[6]:X} - {inputResponse.ResponseData}";
            }
            else if (inputResponse.ResponseData == 0)
            {
                Helper.WriteCycleMessageToLogFile(Item.Name, Item.ItemType, Constants.Response, "", "", Constants.Off);

                CheckMappingDict(MappingResponse.InputOff);

                if (oldValue == inputResponse.ResponseData)
                    return;
                lblStatus.Text = Constants.Off;
                lblStatus.ForeColor = Color.Red; 
            }
            else if (inputResponse.ResponseData == 1)
            {
                Helper.WriteCycleMessageToLogFile(Item.Name, Item.ItemType, Constants.Response, "", "", Constants.On);

                CheckMappingDict(MappingResponse.InputOn);

                if (oldValue == inputResponse.ResponseData)
                    return;
                lblStatus.Text = Constants.On;
                lblStatus.ForeColor = Color.Green;
            }
            else if (inputResponse.ResponseData == 2)
            {
                Helper.WriteCycleMessageToLogFile(Item.Name, Item.ItemType, Constants.Response, "", "", Constants.Invalid);
                Helper.WriteErrorMessageToLogFile(Item.Name, Item.ItemType, Constants.Response, "", "", Constants.Invalid);

                CheckMappingDict(MappingResponse.InputError);

                if (oldValue == inputResponse.ResponseData)
                    return;
                lblStatus.Text = "NOK";
                lblStatus.ForeColor = Color.Black;
            }
            else
            {
                Helper.WriteCycleMessageToLogFile(Item.Name, Item.ItemType, Constants.Response, "", "", Constants.Unexpected);
                Helper.WriteErrorMessageToLogFile(Item.Name, Item.ItemType, Constants.Response, "", "", Constants.Unexpected);
                if (oldValue == inputResponse.ResponseData)
                    return;
                return;
            }
            currentValue = inputResponse.ResponseData;
        }

        /// <summary>
        /// Analog upper and lower limit check for the received data
        /// </summary>
        /// <param name="inputResponse">Response value</param>
        private void AnalogCheck(GenericResponse inputResponse)
        {
            var converted = inputResponse.ResponseData * Item.Coefficient;
            currentValue = converted;

            if (converted < Item.UpperLimit && converted > Item.LowerLimit)
            {
                Helper.WriteCycleMessageToLogFile(Item.Name, Item.ItemType, Constants.Response, "", "", converted.ToString());
                if (oldValue == currentValue)
                    return;
                lblStatus.ForeColor = Color.Green;
            }
            else
            {
                Helper.WriteCycleMessageToLogFile(Item.Name, Item.ItemType, Constants.Response, "", "", converted.ToString() + Constants.OutsideOfLimit);
                Helper.WriteErrorMessageToLogFile(Item.Name, Item.ItemType, Constants.Response, "", "", converted.ToString() + Constants.OutsideOfLimit);
                if (oldValue == currentValue)
                    return;
                lblStatus.ForeColor = Color.Red;
            }
            lblStatus.Text = converted.ToString();
        }

        /// <summary>
        /// Resistive upper and lower limit check for the received data
        /// </summary>
        /// <param name="inputResponse">Response value</param>
        private void ResistiveCheck(GenericResponse inputResponse)
        {
            var converted = inputResponse.ResponseData * Item.Coefficient;
            currentValue = converted;

            if (converted < Item.UpperLimit && converted > Item.LowerLimit)
            {
                Helper.WriteCycleMessageToLogFile(Item.Name, Item.ItemType, Constants.Response, "", "", converted.ToString());
                if (oldValue == currentValue)
                    return;
                lblStatus.ForeColor = Color.Green;
            }
            else
            {
                Helper.WriteCycleMessageToLogFile(Item.Name, Item.ItemType, Constants.Response, "", "", converted.ToString() + Constants.OutsideOfLimit);
                Helper.WriteErrorMessageToLogFile(Item.Name, Item.ItemType, Constants.Response, "", "", converted.ToString() + Constants.OutsideOfLimit);
                if (oldValue == currentValue)
                    return;
                lblStatus.ForeColor = Color.Red;
            }
            lblStatus.Text = converted.ToString();
        }

        /// <summary>
        /// Reads the Frequency value from the received bytes.
        /// </summary>
        /// <param name="monitorItem">A reference to the InputMonitorItem instance</param>
        /// <param name="inputResponse">Response value</param>
        private void FrequencyCheck(InputMonitorItem monitorItem, GenericResponse inputResponse)
        {
            currentValue = inputResponse.ResponseData;

            if (inputResponse.ResponseData < monitorItem.UpperLimit && inputResponse.ResponseData > monitorItem.LowerLimit)
            {
                Helper.WriteCycleMessageToLogFile(Item.Name, Item.ItemType, Constants.Response, "", "", currentValue.ToString());
                if (oldValue == inputResponse.ResponseData)
                    return;
                lblStatus.ForeColor = Color.Green;
            }
            else
            {
                Helper.WriteCycleMessageToLogFile(Item.Name, Item.ItemType, Constants.Response, "", "", currentValue.ToString() + Constants.OutsideOfLimit);
                Helper.WriteErrorMessageToLogFile(Item.Name, Item.ItemType, Constants.Response, "", "", currentValue.ToString() + Constants.OutsideOfLimit);
                if (oldValue == inputResponse.ResponseData)
                    return;
                lblStatus.ForeColor = Color.Red;
            }
            lblStatus.Text = inputResponse.ResponseData.ToString();
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
        /// Logs the current value of a monitor item if it has changed from its old value and meets certain conditions.
        /// If the new value is different from the old one and is within defined limits, it logs the value.
        /// This logging occurs only if it hasn't been logged before.
        /// </summary>
        /// <param name="monitorItem">The monitor item being checked.</param>
        /// <param name="oldValue">The previous value of the monitor item for comparison.</param>
        private void TraceLogDecision(InputMonitorItem monitorItem, double oldValue)
        {
            if (oldValue != currentValue && IsWithinLimits(currentValue) && !IsLogged)
            {
                Program.MainForm.AppendTrace($"{monitorItem.Name}: ({lblStatus.Text})");
                IsLogged = true;
            }
            else if (oldValue != currentValue && !IsWithinLimits(currentValue) && IsLogged)
            {
                Program.MainForm.AppendTrace($"{monitorItem.Name}: ({lblStatus.Text})");
                IsLogged = true;
            }
            else if (oldValue != currentValue && !IsWithinLimits(currentValue) && !IsLogged)
            {
                Program.MainForm.AppendTrace($"{monitorItem.Name}: ({lblStatus.Text})");
                IsLogged = true;
            }

        }

        /// <summary>
        /// Checks if a given value is within the specified lower and upper limits of an item.
        /// </summary>
        /// <param name="value">The value to check against the limits.</param>
        /// <returns>Returns true if the value is within limits; otherwise, false.</returns>
        private bool IsWithinLimits(double value)
        {
            return value >= Item.LowerLimit && value <= Item.UpperLimit;
        }

        /// <summary>
        /// Updates the status label and processes the monitor item based on its type.
        /// It checks the type of the monitor item and calls the corresponding method to handle
        /// the specific type of input response received from the device.
        /// </summary>
        /// <param name="monitorItem">The monitor item whose status is to be updated.</param>
        /// <param name="inputResponse">The response received from the device to be processed according to the monitor item's type.</param>
        private void UpdateStatus(InputMonitorItem monitorItem, GenericResponse inputResponse)
        {
            //TODO to be checked
            //lblStatus.Text = Constants.Off;
            //lblStatus.ForeColor = Color.Red;

            if (monitorItem.ItemType == "Digital")
            {
                DigitalCheck(inputResponse);
            }
            else if (monitorItem.ItemType == "Analog")
            {
                AnalogCheck(inputResponse);
            }
            else if (monitorItem.ItemType == "Resistive")
            {
                ResistiveCheck(inputResponse);
            }
            else if (monitorItem.ItemType == "Frequency")
            {
                FrequencyCheck(monitorItem, inputResponse);
            }
        }

        /// <summary>
        /// Handles the Click event of the "Set Status" button. Sets the status value based on user input.
        /// </summary>
        /// <param name="sender">The button that triggered the event.</param>
        /// <param name="e">The event arguments.</param>
        private void btnSetStatus_Click(object sender, EventArgs e)
        {
            ((Button)sender).ForeColor = Button.DefaultForeColor;
            SetStatus((byte)txtStatus.Value, false);
        }

        /// <summary>
        /// Sets the status for the input item.
        /// </summary>
        /// <param name="status">The status value to set.</param>
        /// <param name="reset">A flag indicating whether to reset the status value.</param>
        internal void SetStatus(byte status, bool reset)
        {
            if (!ConnectionUtil.CheckConnection()) return;
            if (FormMain.IsTestRunning)
            {
                Helper.ShowWarningMessageBox("There is an already running test. Please stop it first");
                return;
            }
            if (Item.RegisterGroup != ((short)Output_ReadGroup.SWCH).ToString("X2")) return;

            if (reset)
                txtStatus.Value = status;

            FormMain.TestClickCounter++;
            var data = FormMain.Configuration.GenericMonitorConfiguration.InputSection.CommonConfig.SetStatus;
            data[4] = byte.Parse(Item.RegisterAddress, System.Globalization.NumberStyles.HexNumber);
            data[6] = status;

            new UdsMessage { Id = Item.MessageIdOrDefault, Data = data }.Transmit();
        }
        /// <summary>
        /// Check the status for the mapping input item.
        /// </summary>
        /// <param name="response">The status value to set.</param>
        private void CheckMappingDict(MappingResponse response)
        {
            if (Program.MappingStateDict.TryGetValue(Item.Name, out var errorLogDetect))
            {
                Program.MappingStateDict.UpdateValue(Item.Name, errorLogDetect.UpdateInputResponse(MappingState.InputReceived, response));

                if (errorLogDetect.ChcekIsError())
                    Helper.WriteErrorMessageToLogFile(Item.Name, Item.ItemType, Constants.MappingMismatch, "", "", $"Mapping Output: {string.Format("{0} = {1}", Program.MappingStateDict.GetMatch(Item.Name).Item1, errorLogDetect.OutputResponse)} mismatched with Input: {string.Format("{0} = {1}", Program.MappingStateDict.GetMatch(Item.Name).Item2, errorLogDetect.InputResponse)}");

                Program.MappingStateDict.Remove(Item.Name);
            }
        }

        #endregion
    }
}