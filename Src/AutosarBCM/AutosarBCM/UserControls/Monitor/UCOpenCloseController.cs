using System;
using System.Drawing;
using System.Globalization;
using System.Threading;
using System.Timers;
using System.Windows.Forms;
using AutosarBCM.Config;

namespace AutosarBCM.UserControls.Monitor
{
    /// <summary>
    /// Represents a user control for controlling open/close operations and displaying their status.
    /// </summary>
    public partial class UCOpenCloseController : OutputUserControl
    {
        #region Variables

        /// <summary>
        /// Represents a class that handles open and close operations and manages their status.
        /// </summary>
        private OutputMonitorItem Item;

        /// <summary>
        /// Represents an object containing information about open and close operations.
        /// </summary>
        private OpenCloseItem openCloseItem;

        /// <summary>
        /// Represents the direction of the operation (Open or Close).
        /// </summary>
        private enum Direction
        {
            Open,
            Close
        }

        /// <summary>
        /// Represents a timer for handling revert operations.
        /// </summary>
        private System.Timers.Timer revertTimer = new System.Timers.Timer();

        /// <summary>
        /// Represents a flag indicating whether the timer is enabled.
        /// </summary>
        private bool timerEnabled = false;

        /// <summary>
        /// Represents the direction of the operation (Open or Close).
        /// </summary>
        private Direction direction;

        /// <summary>
        /// Represents the limit for the number of revert trials.
        /// </summary>
        private int revertTrialLimit = 0;

        /// <summary>
        /// Represents the current count of revert trials.
        /// </summary>
        private int revertTrialCount = 0;

        /// <summary>
        /// Represents the time duration for revert operations.
        /// </summary>
        private int revertTime = 0;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="UCOpenCloseController"/> class.
        /// </summary>
        /// <param name="item">The <see cref="OutputMonitorItem"/> associated with the control.</param>
        /// <param name="revertTime">The time duration for revert operations.</param>
        /// <param name="revertTrial">The limit for the number of revert trials.</param>
        public UCOpenCloseController(OutputMonitorItem item, int revertTime, int revertTrial)
        {
            InitializeComponent();

            lblName.Text = item.Name;

            Item = item;

            if (Item.ItemType == "Power Window")
            {
                openCloseItem = Item.PowerWindow as OpenCloseItem;
                lblOpen.Text = "Up Diag";
                lblClose.Text = "Down Diag";
            }
            else if (Item.ItemType == "Sunroof")
                openCloseItem = Item.Sunroof;

            numRevertTime.Value = revertTime;
            revertTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            if (numRevertTime.Value > 0)
                revertTimer.Interval = revertTime;
            revertTrialLimit = revertTrial;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Handles the change in status based on the given output response.
        /// </summary>
        /// <param name="outputResponse">The output response containing status information.</param>
        public override void ChangeStatus (Response outputResponse)
        {
            this.Invoke(new Action(() => {
                if (outputResponse.RegisterGroup == (short)Output_OpenCloseGroup.PowerWindow_Open || outputResponse.RegisterGroup == (short)Output_OpenCloseGroup.Sunroof_Open)
                {
                    if (outputResponse.RegisterAddress % 2 == 0) // Up/Open
                    {
                        btnUp.BackColor = Color.Green;
                        btnDown.BackColor = Color.Transparent;
                    }
                    else if (outputResponse.RegisterAddress % 2 == 1) // Down/Close
                    {
                        btnDown.BackColor = Color.Green;
                        btnUp.BackColor = Color.Transparent;
                    }

                    if(revertTime > 0)
                    {
                        btnUp.Enabled = btnDown.Enabled = false;
                        timerEnabled = true;
                        revertTimer.Start();
                    }
                }
                else if (outputResponse.RegisterGroup == (short)Output_OpenCloseGroup.PowerWindow_Close || outputResponse.RegisterGroup == (short)Output_OpenCloseGroup.Sunroof_Close)
                {
                    if (outputResponse.RegisterAddress % 2 == 0) // Up/Open
                        btnUp.BackColor = Color.Transparent;
                    else if (outputResponse.RegisterAddress % 2 == 1) // Down/Close
                        btnDown.BackColor = Color.Transparent;
                    timerEnabled = false;

                    btnUp.Enabled = btnDown.Enabled = true;
                    revertTrialCount = 0;
                }

                if (outputResponse.RegisterGroup == (short)Output_ReadGroup.BBT_BTS)
                {
                    if (outputResponse.RegisterAddress == openCloseItem.ReadOpenDiagData[4])
                        lblOpenDiag.Text = ((BBT_BTS_ReadResponse)outputResponse.ResponseData).ToString();
                    else if (outputResponse.RegisterAddress == openCloseItem.ReadCloseDiagData[4])
                        lblCloseDiag.Text = ((BBT_BTS_ReadResponse)outputResponse.ResponseData).ToString();
                }
                else if (outputResponse.RegisterGroup == (short)Output_ReadGroup.MPQ6528)
                {
                    if (outputResponse.RegisterAddress == openCloseItem.ReadOpenDiagData[4])
                        lblOpenDiag.Text = ((MPQ6528_ReadResponse)outputResponse.ResponseData).ToString();
                    else if (outputResponse.RegisterAddress == openCloseItem.ReadCloseDiagData[4])
                        lblCloseDiag.Text = ((MPQ6528_ReadResponse)outputResponse.ResponseData).ToString();
                }
                if (outputResponse.RegisterGroup == (short)Output_ReadGroup.ADC)
                {
                    lblAdc.Text = (outputResponse.ResponseData.ToString()) + " mV";
                }
                if (outputResponse.RegisterGroup == (short)Output_ReadGroup.CURRENTVALUE)
                {
                    lblCurrent.Text = (outputResponse.ResponseData.ToString()) + " mA";
                }
                FormMain.TestClickCounter--;
            }));           
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Handles the Click event for the control's name label and invokes the Click event for the entire control.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void lblName_Click(object sender, EventArgs e)
        {
            this.InvokeOnClick(this, new EventArgs());
        }

        /// <summary>
        /// Handles the Click event for the "Up" button, sets the direction to open, and transmits the corresponding data.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void btnUp_Click(object sender, EventArgs e)
        {
            direction = Direction.Open;
            TransmitData(openCloseItem.EnableOpenData, openCloseItem.DisableOpenData, btnUp);
            this.InvokeOnClick(this, new EventArgs());
        }

        /// <summary>
        /// Handles the Click event for the "Down" button, sets the direction to close, and transmits the corresponding data.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void btnDown_Click(object sender, EventArgs e)
        {
            direction = Direction.Close;
            TransmitData(openCloseItem.EnableCloseData, openCloseItem.DisableCloseData, btnDown);
            this.InvokeOnClick(this, new EventArgs());
        }

        /// <summary>
        /// Handles the Click event for the "Read ADC" button and transmits the corresponding data if available.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void btnReadAdc_Click(object sender, EventArgs e)
        {
            lblAdc.Text = "-";
            if(Item.ReadADCData?.Length > 0)
                TransmitData(Item.ReadADCData);
            this.InvokeOnClick(this, new EventArgs());
        }

        /// <summary>
        /// Handles the Click event for the "Read Current" button and transmits the corresponding data if available.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void btnReadCurrent_Click(object sender, EventArgs e)
        {
            lblCurrent.Text = "-";
            if (Item.ReadCurrentData?.Length > 0)
                TransmitData(Item.ReadCurrentData);
            this.InvokeOnClick(this, new EventArgs());
        }

        /// <summary>
        /// Handles the Click event for the "Read Open Diag" button and transmits the corresponding data.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void btnReadOpenDiag_Click(object sender, EventArgs e)
        {
            lblOpenDiag.Text = "-";
            TransmitData(openCloseItem.ReadOpenDiagData);
            this.InvokeOnClick(this, new EventArgs());
        }

        /// <summary>
        /// Handles the Click event for the "Read Close Diag" button and transmits the corresponding data.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void btnReadCloseDiag_Click(object sender, EventArgs e)
        {
            lblCloseDiag.Text= "-";
            TransmitData(openCloseItem.ReadCloseDiagData);
            this.InvokeOnClick(this, new EventArgs());
        }

        /// <summary>
        /// Transmits data based on the state of the button, either open or close data.
        /// </summary>
        /// <param name="openData">The data to transmit when opening.</param>
        /// <param name="closeData">The data to transmit when closing.</param>
        /// <param name="btn">The button indicating the state.</param>
        private void TransmitData(byte[] openData, byte[] closeData, Button btn)
        {
            if(btn.BackColor == Color.Transparent)
                TransmitData(openData);
            else
                TransmitData(closeData);
        }

        /// <summary>
        /// Transmits the specified data over the connection.
        /// </summary>
        /// <param name="readData">The data to transmit.</param>
        private void TransmitData(byte[] readData)
        {
            if (!ConnectionUtil.CheckConnection())
                return;

            FormMain.TestClickCounter++;

            ConnectionUtil.TransmitData(uint.Parse(MessageID, NumberStyles.HexNumber), readData);
        }
        
        /// <summary>
        /// Triggered function of revertTimer
        /// </summary>
        /// <param name="source">timer</param>
        /// <param name="e">Elapsed Event args</param>
        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            revertTimer.Stop();

            FormMain.TestClickCounter++;

            switch (direction)
            {
                case Direction.Open:
                    TransmitData(openCloseItem.DisableOpenData);
                    break;
                case Direction.Close:
                    TransmitData(openCloseItem.DisableCloseData);
                    break;                
                default:
                    break;
            }
            Thread.Sleep(revertTime);
            if (timerEnabled && revertTrialCount < revertTrialLimit)
            {
                revertTrialCount++;
                Invoke(new Action(() => { btnDown.Enabled = btnUp.Enabled = false; }));
                revertTimer.Start();
            }
            else
                Invoke(new Action(() => { btnDown.Enabled = btnUp.Enabled = true; }));
        }

        /// <summary>
        /// Handles the event when the revert time value is changed. Updates the revert timer interval accordingly.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void numRevertTime_ValueChanged(object sender, EventArgs e)
        {
            if (numRevertTime.Value > 0)
                revertTimer.Interval = revertTime = (int)numRevertTime.Value;
            else
                revertTime = (int)numRevertTime.Value;
        }

        #endregion
    }
}
