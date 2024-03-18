using AutosarBCM.Config;
using AutosarBCM.Message;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace AutosarBCM.UserControls.Monitor
{
    /// <summary>
    /// Represents a user control for monitoring and controlling a wiper device.
    /// </summary>
    public partial class UCWiper : OutputUserControl
    {
        #region Variables

        /// <summary>
        /// Gets or sets the output monitor item associated with this control.
        /// </summary>
        public OutputMonitorItem Item { get; set; }

        /// <summary>
        /// Gets or sets the current wiper status.
        /// </summary>
        private WiperStatus CurrentStatus { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the UCWiper class.
        /// </summary>
        /// <param name="item">The output monitor item for the wiper control.</param>
        public UCWiper(OutputMonitorItem item)
        {
            InitializeComponent();
            this.Item = item;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Handles the change in status based on the given output response.
        /// </summary>
        /// <param name="outputResponse">The output response containing status information.</param>
        public override void ChangeStatus(Response outputResponse)
        {
            this.Invoke(new Action(() =>
            {
                btnStop.ForeColor = btnLow.ForeColor = btnHigh.ForeColor = Control.DefaultForeColor;

                if (outputResponse.RegisterGroup == (short)Output_ReadGroup.Wiper)
                {
                    if (outputResponse.RegisterAddress == (byte)WIPER_ID.STOP_LOW || outputResponse.RegisterAddress == (byte)WIPER_ID.HIGH_LOW)
                    {
                        btnLow.ForeColor = Color.Green;
                        CurrentStatus = WiperStatus.Low;
                    }
                    else if (outputResponse.RegisterAddress == (byte)WIPER_ID.STOP_HIGH || outputResponse.RegisterAddress == (byte)WIPER_ID.LOW_HIGH)
                    {
                        btnHigh.ForeColor = Color.Green;
                        CurrentStatus = WiperStatus.High;
                    }
                    else if (outputResponse.RegisterAddress == (byte)WIPER_ID.HIGH_STOP || outputResponse.RegisterAddress == (byte)WIPER_ID.LOW_STOP)
                    {
                        btnStop.ForeColor = Color.Green;
                        CurrentStatus = WiperStatus.Stop;
                    }
                }
                else if (outputResponse.RegisterGroup == (short)Output_ReadGroup.XS4200)
                {
                    lblDIAG.Text = ((XS4200)outputResponse.ResponseData).ToString();
                }
                else if (outputResponse.RegisterGroup == (short)Output_ReadGroup.ADC)
                {
                    lblADC.Text = $"{outputResponse.ResponseData} mV";
                }
                else if (outputResponse.RegisterGroup == (short)Output_ReadGroup.CURRENTVALUE)
                {
                    lblCurrent.Text = $"{outputResponse.ResponseData} mA";
                }
                FormMain.TestClickCounter--;
            }));
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Handles the click event of the Stop button to stop the wiper.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        private void btnStop_Click(object sender, EventArgs e)
        {
            if (!ConnectionUtil.CheckConnection()) return;

            FormMain.TestClickCounter++;
            this.InvokeOnClick(this, new EventArgs());
            if (CurrentStatus == WiperStatus.Low)
                new UdsMessage() { Id = Item.MessageIdOrDefault, Data = Item.WiperCase.LowStop }.Transmit();
            else if (CurrentStatus == WiperStatus.High)
                new UdsMessage() { Id = Item.MessageIdOrDefault, Data = Item.WiperCase.HighStop }.Transmit();
        }

        /// <summary>
        /// Handles the click event of the Low button to set the wiper to a low position.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        private void btnLow_Click(object sender, EventArgs e)
        {
            if (!ConnectionUtil.CheckConnection())
                return;
            FormMain.TestClickCounter++;
            this.InvokeOnClick(this, new EventArgs());

            if (CurrentStatus == WiperStatus.Stop)
                new UdsMessage() { Id = Item.MessageIdOrDefault, Data = Item.WiperCase.StopLow }.Transmit();
            else if (CurrentStatus == WiperStatus.High)
                new UdsMessage() { Id = Item.MessageIdOrDefault, Data = Item.WiperCase.HighLow }.Transmit();
        }

        /// <summary>
        /// Handles the click event of the High button to set the wiper to a high position.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        private void btnHigh_Click(object sender, EventArgs e)
        {
            if (!ConnectionUtil.CheckConnection()) 
                return;

            FormMain.TestClickCounter++;
            this.InvokeOnClick(this, new EventArgs());

            if(CurrentStatus == WiperStatus.Stop)
                new UdsMessage() { Id = Item.MessageIdOrDefault, Data = Item.WiperCase.StopHigh }.Transmit();
            else if(CurrentStatus == WiperStatus.Low)
                new UdsMessage() { Id = Item.MessageIdOrDefault, Data = Item.WiperCase.LowHigh }.Transmit();
        }

        /// <summary>
        /// Handles the click event of the ADC Read button to read the ADC value of the wiper.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        private void btnAdcRead_Click(object sender, EventArgs e)
        {
            if (!ConnectionUtil.CheckConnection()) return;
            lblADC.Text = string.Empty;
            FormMain.TestClickCounter++;
            this.InvokeOnClick(this, new EventArgs());
            new UdsMessage { Id = Item.MessageIdOrDefault, Data = Item.ReadADCData }.Transmit();
        }

        /// <summary>
        /// Handles the click event of the Current Value Read button to read the ADC Current value of the wiper.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        private void btnCurrentRead_Click(object sender, EventArgs e)
        {
            if (!ConnectionUtil.CheckConnection()) return;
            lblCurrent.Text = string.Empty;
            FormMain.TestClickCounter++;
            this.InvokeOnClick(this, new EventArgs());
            new UdsMessage { Id = Item.MessageIdOrDefault, Data = Item.ReadCurrentData }.Transmit();
        }

        /// <summary>
        /// Handles the click event of the Diagnostic Read button to read diagnostic data related to the wiper.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        private void btnDiagRead_Click(object sender, EventArgs e)
        {
            if (!ConnectionUtil.CheckConnection()) return;
            lblDIAG.Text = string.Empty;
            FormMain.TestClickCounter++;
            this.InvokeOnClick(this, new EventArgs());
            new UdsMessage { Id = Item.MessageIdOrDefault, Data = Item.ReadDiagData }.Transmit();
        }

        /// <summary>
        /// Handles the mouse click event of the UCWiper control to trigger a click event for the control.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The mouse event arguments.</param>
        private void UCWiper_MouseClick(object sender, MouseEventArgs e)
        {
            this.InvokeOnClick(this, new EventArgs());
        }

        #endregion
    }
}
