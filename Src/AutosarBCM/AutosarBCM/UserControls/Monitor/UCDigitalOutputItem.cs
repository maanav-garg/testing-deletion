using System;
using System.Drawing;
using System.Globalization;
using System.Timers;
using System.Windows.Forms;
using AutosarBCM.Config;

namespace AutosarBCM.UserControls.Monitor
{
    /// <summary>
    /// Represents a user control for displaying and controlling a digital output item.
    /// </summary>
    public partial class UCDigitalOutputItem : OutputUserControl
    {
        #region Variables

        /// <summary>
        /// Represents a digital output monitor item.
        /// </summary>
        public OutputMonitorItem MonitorItem;

        /// <summary>
        /// Gets the current state of the digital output item.
        /// </summary>
        public string State { get { return btnSwitch.Text; } }

        /// <summary>
        /// Gets the current ADC (Analog-to-Digital Converter) value of the digital output item.
        /// Returns a dash ("-") if the value is not available or empty.
        /// </summary>
        public string ADCValue { get { return String.IsNullOrEmpty(lblADC.Text) ? "-" : lblADC.Text; } }

        /// <summary>
        /// Gets the current DIAG (Diagnostic) value of the digital output item.
        /// Returns a dash ("-") if the value is not available or empty.
        /// </summary>
        public string DIAGValue { get { return String.IsNullOrEmpty(lblDIAG.Text) ? "-" : lblDIAG.Text; } }
        /// <summary>
        /// Gets the current value of the digital output item.
        /// Returns a dash ("-") if the value is not available or empty.
        /// </summary>
        public string CurrentData { get { return String.IsNullOrEmpty(lblCurrent.Text) ? "-" : lblCurrent.Text; } }

        /// <summary>
        /// Timer used for reverting the state of the digital output item.
        /// </summary>
        private System.Timers.Timer revertTimer = new System.Timers.Timer();

        /// <summary>
        /// Indicates whether the timer has worked.
        /// </summary>
        private bool timerWorked = false;

        /// <summary>
        /// The current trial count.
        /// </summary>
        private int trialCount = 0;

        /// <summary>
        /// The limit for the number of trials.
        /// </summary>
        private int trialLimit = 0;

        /// <summary>
        /// Byte array for XS4200 open data.
        /// </summary>
        private byte[] XS4200_OpenData = new byte[8];

        /// <summary>
        /// Byte array for XS4200 close data.
        /// </summary>
        private byte[] XS4200_CloseData = new byte[8];

        /// <summary>
        /// The group to send data.
        /// </summary>
        private short sendDataGroup = 0;

        /// <summary>
        /// The risk limit for the digital output item.
        /// </summary>
        private RiskLevels riskLimit = default;

        /// <summary>
        /// The current risk level of the digital output item.
        /// </summary>
        private RiskLevels riskLevel = default;

        /// <summary>
        /// Indicates whether the risk has been accepted for the digital output item.
        /// </summary>
        private bool riskAccepted = false;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the UCDigitalOutputItem class with specified parameters.
        /// </summary>
        /// <param name="monitorItem">The output monitor item associated with this control.</param>
        /// <param name="revertTime">The time (in milliseconds) before reverting the state.</param>
        /// <param name="revertLimit">The limit for the number of trials before reverting.</param>
        /// <param name="riskLimit">The risk limit for the digital output item.</param>
        /// <param name="defaultFreq">The default frequency value for PWM (Pulse Width Modulation).</param>
        /// <param name="defaultDuty">The default duty cycle value for PWM.</param>
        public UCDigitalOutputItem(OutputMonitorItem monitorItem, int revertTime, int revertLimit,string riskLimit, int defaultFreq, int defaultDuty)
        {
            InitializeComponent();

            this.MonitorItem = monitorItem;
            lblName.Text = monitorItem.Name;
            numTimeout.Value = RevertTime = revertTime;
            trialLimit = revertLimit;

            nudDuty.Value = defaultDuty > 0 ? defaultDuty : nudDuty.Value;
            nudFreq.Value = defaultFreq > 0 ? defaultFreq : nudFreq.Value;

            revertTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            if (RevertTime > 0)
                revertTimer.Interval = RevertTime;

            grpADC.MouseClick += lblName_Click;
            grpDIAG.MouseClick += lblName_Click;
            grpCurrent.MouseClick += lblName_Click;

            grpADC.Enabled = monitorItem.ReadADCData?.Length > 0;
            grpCurrent.Enabled = monitorItem.ReadCurrentData?.Length > 0;
            grpDIAG.Enabled = monitorItem.ReadDiagData?.Length > 0;
            if(!(pnlRead.Visible = grpADC.Enabled || grpDIAG.Enabled))
                this.Height -= pnlRead.Height;

            btnSwitch.Visible = numTimeout.Visible = lblRevertTime.Visible = (monitorItem.OpenData?.Length > 0 && monitorItem.CloseData?.Length > 0) || monitorItem.PwmTag == "XS4200";

            if (btnSwitch.Visible && numTimeout.Visible && lblRevertTime.Visible)
                SetItemName(23);
            else
            {
                SetItemName(40);
                lblName.Width = pnlDigital.Width;
            }

            if (monitorItem.ItemType != "PWM" || monitorItem.SetPWMData?.Length == 0)
            {
                pnlPWM.Visible = false;
                Height -= pnlPWM.Height;
            }
            else
            {
                grpDuty.Enabled = monitorItem.PwmTag == PwmTag.duty.ToString() || monitorItem.PwmTag == PwmTag.freq_duty.ToString();
                grpFrequency.Enabled = monitorItem.PwmTag == PwmTag.freq.ToString() || monitorItem.PwmTag == PwmTag.freq_duty.ToString();
            }

            if(monitorItem.PwmTag == "XS4200")
            {
                MonitorItem.SetPWMData[MonitorItem.SetPWMData.Length - 1] = 100;
                Array.Copy(MonitorItem.SetPWMData, XS4200_OpenData, MonitorItem.SetPWMData.Length);

                MonitorItem.SetPWMData[MonitorItem.SetPWMData.Length - 1] = 0;
                Array.Copy(MonitorItem.SetPWMData, XS4200_CloseData, MonitorItem.SetPWMData.Length);
            }

            if (!String.IsNullOrEmpty(riskLimit))
                this.riskLimit = Helper.GetEnumValue<RiskLevels>(riskLimit);
            if (!String.IsNullOrEmpty(monitorItem.RiskLevel))
                this.riskLevel = Helper.GetEnumValue<RiskLevels>(monitorItem.RiskLevel);

            pcbMediumRisk.Visible = riskLevel == RiskLevels.Medium;
            pcbHighRisk.Visible = riskLevel == RiskLevels.High;

            pnlDigital.Visible = !(pnlSend.Visible = monitorItem.SendData?.Length > 0);
            Height = !pnlSend.Visible ? Height - pnlDigital.Height : Height - pnlDigital.Height;

            if (monitorItem.SendData?.Length > 0)
                sendDataGroup = (short)(Helper.GetValueOfPrimitive(MonitorItem.SendData, 2, 2));
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Change labels regarding response values
        /// </summary>
        /// <param name="outputResponse">Response data</param>
        public override void ChangeStatus(Response response)
        {
            var outputResponse = (GenericResponse)response;

            var checkOpenCloseData = new Tuple<string, Color>(btnSwitch.Text, btnSwitch.ForeColor);
            var checkReadDiagData = lblDIAG.Text;
            var checkReadAdcData = lblADC.Text;
            var checkReadCurrentData = lblCurrent.Text;
            var checkPwmResponse = btnSetPwm.ForeColor;
            var checkSendResponse = btnSend.ForeColor;

            if (MonitorItem.ItemType == "Digital" || MonitorItem.ItemType == "FET")
                checkOpenCloseData = DigitalOpenCloseCheck(outputResponse);
            else if (MonitorItem.ItemType == "PWM")
                checkPwmResponse = PwmSetResponseCheck(outputResponse);

            if (grpADC.Enabled)
                checkReadAdcData = GetDigitalReadADCResponseData(outputResponse.RegisterGroup, outputResponse.ResponseData, lblADC.Text);
            if (grpCurrent.Enabled)
                checkReadCurrentData = GetDigitalReadCurrentValueResponseData(outputResponse.RegisterGroup, outputResponse.ResponseData, lblCurrent.Text);
            if (grpDIAG.Enabled)
                checkReadDiagData = GetDigitalReadDiagResponseData(outputResponse.RegisterGroup, outputResponse.ResponseData, lblDIAG.Text);
            if (pnlSend.Visible && MonitorItem.SendData?.Length > 0)
                checkSendResponse = GetSendDataResponseCheck(outputResponse);

            if (btnSwitch.InvokeRequired)
            {
                btnSwitch.BeginInvoke((MethodInvoker)delegate ()
                {
                    btnSwitch.Text = checkOpenCloseData.Item1;
                    btnSwitch.ForeColor = checkOpenCloseData.Item2;
                    lblADC.Text = checkReadAdcData;
                    lblCurrent.Text = checkReadCurrentData;
                    lblDIAG.Text = checkReadDiagData;
                    btnSetPwm.ForeColor = checkPwmResponse;
                    btnSend.ForeColor = checkSendResponse;
                });
            }
            else
            {
                btnSwitch.Text = checkOpenCloseData.Item1;
                btnSwitch.ForeColor = checkOpenCloseData.Item2;
                lblADC.Text = checkReadAdcData;
                lblCurrent.Text = checkReadCurrentData;
                lblDIAG.Text = checkReadDiagData;
                btnSetPwm.ForeColor = checkPwmResponse;
                btnSend.ForeColor = checkSendResponse;
            }
            FormMain.TestClickCounter--;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Sets the name label for the digital output item, truncating it if necessary.
        /// </summary>
        /// <param name="length">The maximum length of the name label.</param>
        private void SetItemName(int length)
        {
            if (MonitorItem.Name.Length > length)
                lblSendName.Text = lblName.Text = $"{MonitorItem.Name.Substring(0, length - 3)}...";
            else
                lblSendName.Text = lblName.Text = MonitorItem.Name;
        }

        /// <summary>
        /// Set Response Data to On/Off button
        /// </summary>
        /// <param name="outputResponse">Response data</param>
        private Tuple<string, Color> DigitalOpenCloseCheck(GenericResponse outputResponse)
        {
            if ((outputResponse.RegisterGroup == (short)Output_OpenCloseGroup.EO_On || outputResponse.RegisterGroup == (short)Output_OpenCloseGroup.EO_Off)
                || outputResponse.RegisterGroup == (short)Output_OpenCloseGroup.DIO_Open || outputResponse.RegisterGroup == (short)Output_OpenCloseGroup.DIO_Close)
            {
                if (outputResponse.ResponseData == (byte)DigitalUdsOnCan_OpenCloseValues.E_OK)
                {
                    return GetSwitchButtonState();
                }

                else if (outputResponse.ResponseData == (byte)DigitalUdsOnCan_OpenCloseValues.E_NOT_OK)
                {
                    Program.MainForm.AppendTrace($"EO Command failed for {btnSwitch.Text} operation");

                    CheckTimer();
                }
            }
            else if (outputResponse.RegisterGroup == (short)Output_PwmGroup.XS4200)
            {
                if (outputResponse.ResponseData == (byte)XS4200PWM_SetResponse.E_OK)
                {
                    return GetSwitchButtonState();
                }
                else if (outputResponse.ResponseData == (byte)XS4200PWM_SetResponse.E_NOT_OK)
                {
                    Program.MainForm.AppendTrace($"XS4200 PWM Command failed for {btnSwitch.Text} operation");

                    CheckTimer();
                }
            }
            else if (outputResponse.RegisterGroup == (short)Output_OpenCloseGroup.Washer_Open || outputResponse.RegisterGroup == (short)Output_OpenCloseGroup.Washer_Close)
            {
                return GetSwitchButtonState();
            }
            else if (outputResponse.RegisterGroup == (short)Output_OpenCloseGroup.WaterPump_Open || outputResponse.RegisterGroup == (short)Output_OpenCloseGroup.WaterPump_Close)
            {
                return GetSwitchButtonState();
            }

            numTimeout.BeginInvoke((MethodInvoker)delegate () { numTimeout.Value = RevertTime; });

            return new Tuple<string, Color>(btnSwitch.Text, btnSwitch.ForeColor);
        }

        /// <summary>
        /// Checks the timer status and starts it if necessary based on the trial count and timer state.
        /// </summary>
        private void CheckTimer()
        {
            if (trialCount < trialLimit && timerWorked)
            {
                btnSwitch.Invoke(new Action(() => { btnSwitch.Enabled = false; }));
                revertTimer.Start();
                trialCount++;
            }
            else
            {
                trialCount = 0;
                timerWorked = false;
            }
        }

        /// <summary>
        /// Gets the state of the switch button and returns it as a tuple containing the new state and color.
        /// </summary>
        /// <returns>A tuple containing the new state and color of the switch button.</returns>
        private Tuple<string, Color> GetSwitchButtonState()
        {
            if (numTimeout.Value > 0 && !timerWorked)
            {
                btnSwitch.Invoke(new Action(() => { btnSwitch.Enabled = false; }));
                revertTimer.Start();
            }
            else if (numTimeout.Value > 0 && timerWorked)
            {
                trialCount = 0;
                timerWorked = false;
            }

            if (btnSwitch.Text == "ON")
                return new Tuple<string, Color>("OFF", Color.Red);

            else if (btnSwitch.Text == "OFF")
                return new Tuple<string, Color>("ON", Color.Green);

            return new Tuple<string, Color>(btnSwitch.Text, btnSwitch.ForeColor);
        }

        /// <summary>
        /// Checks the response of setting PWM (Pulse Width Modulation) and returns the corresponding color.
        /// </summary>
        /// <param name="outputResponse">The response from setting PWM.</param>
        /// <returns>The color to represent the response status.</returns>
        private Color PwmSetResponseCheck(GenericResponse outputResponse)
        {
            if ((outputResponse.RegisterGroup == (short)Output_PwmGroup.PWM_Set && outputResponse.ResponseData == (byte)PWM_SetResponse.E_OK) || outputResponse.RegisterGroup == (short)Output_PwmGroup.XS4200)
                return Color.Green;
            else if (outputResponse.RegisterGroup == (short)Output_PwmGroup.PWM_Set && outputResponse.ResponseData == (byte)PWM_SetResponse.E_NOT_OK)
            {
                Program.MainForm.AppendTrace($"Set PWM Command failed for '{MonitorItem.Name}'");

                return Color.Red;
            }

            return btnSetPwm.ForeColor;
        }

        /// <summary>
        /// Checks the response of sending data and returns the corresponding color.
        /// </summary>
        /// <param name="outputResponse">The response from sending data.</param>
        /// <returns>The color to represent the response status.</returns>
        private Color GetSendDataResponseCheck(GenericResponse outputResponse)
        {
            if (outputResponse.RegisterGroup == sendDataGroup)
                return Color.Green;

            return btnSetPwm.ForeColor;
        }

        /// <summary>
        /// Set Diag return of response
        /// </summary>
        /// <param name="group">Register group</param>
        /// <param name="value">Respone value</param>
        internal static string GetDigitalReadDiagResponseData(short group, short value, string defaultValue = "")
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
                    return defaultValue;
            }
        }

        /// <summary>
        /// Set ADC return of response
        /// </summary>
        /// <param name="group">Register group</param>
        /// <param name="value">Respone value</param>
        internal static string GetDigitalReadADCResponseData(short group, short value, string defaultValue = "")
        {
            switch (group)
            {
                case (short)Output_ReadGroup.ADC:
                    return value.ToString() + " mV";
                case (short)Output_ReadGroup.DIO:
                    return ((DIO_ReadResponse)value).ToString();
                case (short)Output_ReadGroup.PWHALL:
                    return value.ToString();
                default:
                    return defaultValue;
            }
        }

        /// <summary>
        /// Set Current Value return of response
        /// </summary>
        /// <param name="group">Register group</param>
        /// <param name="value">Respone value</param>
        internal static string GetDigitalReadCurrentValueResponseData(short group, short value, string defaultValue = "")
        {
            switch (group)
            {
                case (short)Output_ReadGroup.CURRENTVALUE:
                    return value.ToString() + " mA";
                default:
                    return defaultValue;
            }
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
        /// On/Off button click event
        /// </summary>
        /// <param name="sender">label</param>
        /// <param name="e">Event args</param>
        private void btnSwitch_Click(object sender, EventArgs e)
        {
            if (!ConnectionUtil.CheckConnection())
                return;

            FormMain.TestClickCounter++;
            TransmitOpenCloseData();
            this.InvokeOnClick(this, new EventArgs());
        }

        /// <summary>
        /// To send Open or Close messages
        /// </summary>
        private void TransmitOpenCloseData()
        {
            if (btnSwitch.Text == "ON")
            {
                if (MonitorItem.PwmTag == "XS4200")
                    ConnectionUtil.TransmitData(uint.Parse(MonitorItem.MessageIdOrDefault, NumberStyles.HexNumber), XS4200_CloseData);
                else
                    ConnectionUtil.TransmitData(uint.Parse(MonitorItem.MessageIdOrDefault, NumberStyles.HexNumber), MonitorItem.CloseData);
            }
            else if (btnSwitch.Text == "OFF")
            {
                if (MonitorItem.PwmTag == "XS4200")
                    ConnectionUtil.TransmitData(uint.Parse(MonitorItem.MessageIdOrDefault, NumberStyles.HexNumber), XS4200_OpenData);
                else
                    ConnectionUtil.TransmitData(uint.Parse(MonitorItem.MessageIdOrDefault, NumberStyles.HexNumber), MonitorItem.OpenData);
            }
        }

        /// <summary>
        /// Triggred function of revertTimer
        /// </summary>
        /// <param name="source">timer</param>
        /// <param name="e">Elapsed Event args</param>
        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            revertTimer.Stop();

            timerWorked = true;

            FormMain.TestClickCounter++;
            TransmitOpenCloseData();

            btnSwitch.Invoke(new Action(() => { btnSwitch.Enabled = true; }));
        }

        /// <summary>
        /// Reverttime change event
        /// </summary>
        /// <param name="sender">label</param>
        /// <param name="e">Event args</param>
        private void numTimeout_ValueChanged(object sender, EventArgs e)
        {
            if (riskLevel > riskLimit && !riskAccepted)
            {
                if (!(pcbAccepted.Visible = riskAccepted = Helper.ShowConfirmationMessageBox($"Changing this value is risky!")))
                {
                    numTimeout.ValueChanged -= numTimeout_ValueChanged;
                    numTimeout.Value = RevertTime;
                    numTimeout.ValueChanged += numTimeout_ValueChanged;
                    return;
                }
            }

            if (numTimeout.Value > 0)
                revertTimer.Interval = RevertTime = (int)numTimeout.Value;
            else
                RevertTime = (int)numTimeout.Value;
        }

        /// <summary>
        /// Read adc button click event
        /// </summary>
        /// <param name="sender">Button</param>
        /// <param name="e">Event args</param>
        private void btnAdcRead_Click(object sender, EventArgs e)
        {
            if (!ConnectionUtil.CheckConnection())
                return;

            lblADC.Text = string.Empty;

            FormMain.TestClickCounter++;
            ConnectionUtil.TransmitData(uint.Parse(MonitorItem.MessageIdOrDefault, NumberStyles.HexNumber), MonitorItem.ReadADCData);
            this.InvokeOnClick(this, new EventArgs());
        }

        /// <summary>
        /// Read current data button click event
        /// </summary>
        /// <param name="sender">Button</param>
        /// <param name="e">Event args</param>
        private void btnCurrentRead_Click(object sender, EventArgs e)
        {
            if (!ConnectionUtil.CheckConnection())
                return;

            lblCurrent.Text = string.Empty;

            FormMain.TestClickCounter++;
            ConnectionUtil.TransmitData(uint.Parse(MonitorItem.MessageIdOrDefault, NumberStyles.HexNumber), MonitorItem.ReadCurrentData);
            this.InvokeOnClick(this, new EventArgs());
        }

        /// <summary>
        /// Read diag button click event
        /// </summary>
        /// <param name="sender">Button</param>
        /// <param name="e">Event args</param>
        private void btnDiagRead_Click(object sender, EventArgs e)
        {
            if (!ConnectionUtil.CheckConnection())
                return;

            lblDIAG.Text = string.Empty;

            FormMain.TestClickCounter++;
            ConnectionUtil.TransmitData(uint.Parse(MonitorItem.MessageIdOrDefault, NumberStyles.HexNumber), MonitorItem.ReadDiagData);
            this.InvokeOnClick(this, new EventArgs());
        }

        /// <summary>
        /// Set pwm button click event
        /// </summary>
        /// <param name="sender">Button</param>
        /// <param name="e">Event args</param>
        private void btnSetPwm_Click(object sender, EventArgs e)
        {
            if (!ConnectionUtil.CheckConnection())
                return;


            btnSetPwm.ForeColor = Color.Black;

            if (grpFrequency.Enabled)
            {
                var freqVal = Helper.StringToByteArray(Convert.ToInt16(nudFreq.Value).ToString("X4"));
                Array.Copy(freqVal, 0, MonitorItem.SetPWMData, 5, freqVal.Length);
            }

            if (grpDuty.Enabled)
            {
                var dutyVal = Convert.ToByte(nudDuty.Value);
                MonitorItem.SetPWMData[MonitorItem.SetPWMData.Length - 1] = dutyVal;
            }

            FormMain.TestClickCounter++;
            ConnectionUtil.TransmitData(uint.Parse(MonitorItem.MessageIdOrDefault, NumberStyles.HexNumber), MonitorItem.SetPWMData);
            this.InvokeOnClick(this, new EventArgs());
        }

        /// <summary>
        /// Handles the Click event when the "Send" button is clicked to transmit data.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void btnSend_Click(object sender, EventArgs e)
        {
            if (!ConnectionUtil.CheckConnection())
                return;


            btnSend.ForeColor = Color.Black;

            FormMain.TestClickCounter++;
            ConnectionUtil.TransmitData(uint.Parse(MonitorItem.MessageIdOrDefault, NumberStyles.HexNumber), MonitorItem.SendData);
            this.InvokeOnClick(this, new EventArgs());
        }

        /// <summary>
        /// Displays a tooltip with "High Risk" when the mouse hovers over the "High Risk" icon.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void pcbHighRisk_MouseHover(object sender, EventArgs e)
        {
            ToolTip tt = new ToolTip();
            tt.SetToolTip(this.pcbHighRisk, "High Risk");
        }

        /// <summary>
        /// Displays a tooltip with "Medium Risk" when the mouse hovers over the "Medium Risk" icon.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void pcbMediumRisk_MouseHover(object sender, EventArgs e)
        {
            ToolTip tt = new ToolTip();
            tt.SetToolTip(this.pcbMediumRisk, "Medium Risk");
        }

        /// <summary>
        /// Displays a tooltip with "Risk Accepted" when the mouse hovers over the "Risk Accepted" icon.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void pcbAccepted_MouseHover(object sender, EventArgs e)
        {
            ToolTip tt = new ToolTip();
            tt.SetToolTip(this.pcbAccepted, "Risk Accepted");
        }

        /// <summary>
        /// Displays a tooltip with the DIAG (Diagnostic) value when the mouse hovers over the DIAG label.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void lblDIAG_MouseHover(object sender, EventArgs e)
        {
            ToolTip tt = new ToolTip();
            tt.SetToolTip(this.lblDIAG, this.lblDIAG.Text);
        }

        /// <summary>
        /// Displays a tooltip with the ADC (Analog-to-Digital Converter) value when the mouse hovers over the ADC label.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void lblADC_MouseHover(object sender, EventArgs e)
        {
            ToolTip tt = new ToolTip();
            tt.SetToolTip(this.lblADC, lblADC.Text);
        }

        /// <summary>
        /// Displays a tooltip with the Current value when the mouse hovers over the Current label.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void lblCurrent_MouseHover(object sender, EventArgs e)
        {
            ToolTip tt = new ToolTip();
            tt.SetToolTip(this.lblCurrent, lblCurrent.Text);
        }

        /// <summary>
        /// Handles the Click event when the name label is clicked to trigger a click event for the control.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void lblSendName_Click(object sender, EventArgs e)
        {
            this.InvokeOnClick(this, new EventArgs());
        }

        /// <summary>
        /// Event handler called when the "btnMax" button is clicked.
        /// Sets the Duty value to 100.
        /// </summary>
        /// <param name="sender">The object that initiated the event</param>
        /// <param name="e">Event arguments</param>
        private void btnMax_Click(object sender, EventArgs e)
        {
            nudDuty.Value = 100;
        }

        /// <summary>
        /// Event handler called when the "btnMin" button is clicked.
        /// Sets the Duty value to 0.
        /// </summary>
        /// <param name="sender">The object that initiated the event</param>
        /// <param name="e">Event arguments</param>
        private void btnMin_Click(object sender, EventArgs e)
        {
            nudDuty.Value = 0;
        }

        #endregion
    }
}
