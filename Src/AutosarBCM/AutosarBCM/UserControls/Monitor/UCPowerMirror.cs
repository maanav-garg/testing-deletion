using System;
using System.Drawing;
using System.Globalization;
using System.Threading;
using System.Timers;
using AutosarBCM.Config;

namespace AutosarBCM.UserControls.Monitor
{
    /// <summary>
    /// This class represents a user control used for controlling the power mirror.
    /// </summary>
    public partial class UCPowerMirror : OutputUserControl
    {
        #region Variables

        /// <summary>
        /// Enumerates the possible directions of the power mirror.
        /// </summary>
        private enum MirrorDirection
        {
            Left,
            Right,
            Up,
            Down
        }

        /// <summary>
        /// The monitor item associated with the power mirror.
        /// </summary>
        private OutputMonitorItem monitorItem;

        /// <summary>
        /// Timer used for reverting the power mirror state.
        /// </summary>
        private System.Timers.Timer revertTimer = new System.Timers.Timer();

        /// <summary>
        /// Indicates whether the revert timer is enabled.
        /// </summary>
        private bool timerEnabled = false;

        /// <summary>
        /// The direction of the power mirror movement.
        /// </summary>
        private MirrorDirection mirrorDirection;

        /// <summary>
        /// The limit for the number of revert trials.
        /// </summary>
        private int revertTrialLimit = 0;

        /// <summary>
        /// The current count of revert trials.
        /// </summary>
        private int revertTrialCount = 0;

        /// <summary>
        /// The time interval for reverting the power mirror state.
        /// </summary>
        private int revertTime = 0;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="item">OutputMonitorItem Item</param>
        /// <param name="revertTime">Revert time</param>
        public UCPowerMirror(OutputMonitorItem item, int revertTime, int revertTrial)
        {
            InitializeComponent();
            
            lblName.Text = item.Name;
            monitorItem = item;

            numRevertTime.Value = revertTime;
            revertTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            if (numRevertTime.Value > 0)
                revertTimer.Interval = this.revertTime = revertTime;
            revertTrialLimit = revertTrial;
        }

        #endregion

        #region Public

        /// <summary>
        /// Handles the change in status based on the given output response.
        /// </summary>
        /// <param name="outputResponse">The output response containing status information.</param>
        public override void ChangeStatus(Response outputResponse)
        {
            this.Invoke(new Action(() =>
            {
                if (outputResponse.RegisterGroup == (short)Output_PowerMirror.SetOpen)
                {
                    if (outputResponse.RegisterAddress % 4 == 1) //Up
                    {
                        btnPowerMirrorUp.BackColor = Color.Green;
                        btnPowerMirrorDown.BackColor = btnPowerMirrorRight.BackColor = btnPowerMirrorLeft.BackColor = Color.Transparent;
                    }
                    else if (outputResponse.RegisterAddress % 4 == 2) //Down
                    {
                        btnPowerMirrorDown.BackColor = Color.Green;
                        btnPowerMirrorUp.BackColor = btnPowerMirrorRight.BackColor = btnPowerMirrorLeft.BackColor = Color.Transparent;
                    }
                    else if (outputResponse.RegisterAddress % 4 == 3) //Left
                    {
                        btnPowerMirrorLeft.BackColor = Color.Green;
                        btnPowerMirrorRight.BackColor = btnPowerMirrorUp.BackColor = btnPowerMirrorDown.BackColor = Color.Transparent;
                    }
                    else if (outputResponse.RegisterAddress % 4 == 0) //Right
                    {
                        btnPowerMirrorRight.BackColor = Color.Green;
                        btnPowerMirrorLeft.BackColor = btnPowerMirrorUp.BackColor = btnPowerMirrorDown.BackColor = Color.Transparent;
                    }
                    if (revertTime > 0)
                    {
                        btnPowerMirrorDown.Enabled = btnPowerMirrorLeft.Enabled = btnPowerMirrorRight.Enabled = btnPowerMirrorUp.Enabled = false;
                        timerEnabled = true;
                        revertTimer.Start();
                    }
                }
                else if (outputResponse.RegisterGroup == (short)Output_PowerMirror.SetClose)
                {
                    if (outputResponse.RegisterAddress % 4 == 1) //Up
                        btnPowerMirrorUp.BackColor = Color.Transparent;
                    else if (outputResponse.RegisterAddress % 4 == 2) //Down
                        btnPowerMirrorDown.BackColor = Color.Transparent;
                    else if (outputResponse.RegisterAddress % 4 == 3) //Left
                        btnPowerMirrorLeft.BackColor = Color.Transparent;
                    else if (outputResponse.RegisterAddress % 4 == 0) //Right
                        btnPowerMirrorRight.BackColor = Color.Transparent;
                    btnPowerMirrorDown.Enabled = btnPowerMirrorLeft.Enabled = btnPowerMirrorRight.Enabled = btnPowerMirrorUp.Enabled = true;
                    timerEnabled = false;
                    revertTrialCount = 0;
                }
                else if (outputResponse.RegisterGroup == (short)Output_ReadGroup.BBT_BTS)
                {
                    if (outputResponse.RegisterAddress % 3 == 2) //Up
                        lblReadPowerMirrorUp.Text = SetResponseMessage(outputResponse.ResponseData);
                    else if (outputResponse.RegisterAddress % 3 == 0) //Down-left
                        lblReadPowerMirrorLeft.Text = SetResponseMessage(outputResponse.ResponseData);
                    else if (outputResponse.RegisterAddress % 3 == 1) //Right
                        lblReadPowerMirrorRight.Text = SetResponseMessage(outputResponse.ResponseData);
                }
                FormMain.TestClickCounter--;
            }));            
        }

        #endregion

        #region Private

        /// <summary>
        /// Handles the click event of the lblName label control and triggers a click event for the current user control.
        /// </summary>
        /// <param name="sender">The sender object that triggered the event.</param>
        /// <param name="e">The event arguments.</param>
        private void lblName_Click(object sender, EventArgs e)
        {
            this.InvokeOnClick(this, new EventArgs());
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

            switch (mirrorDirection)
            {
                case MirrorDirection.Left:
                    TransmitOpenCloseData(btnPowerMirrorLeft.BackColor, monitorItem.PowerMirror.SetOpenLeft, monitorItem.PowerMirror.SetCloseLeft);
                    break;
                case MirrorDirection.Right:
                    TransmitOpenCloseData(btnPowerMirrorRight.BackColor, monitorItem.PowerMirror.SetOpenRight, monitorItem.PowerMirror.SetCloseRight);
                    break;
                case MirrorDirection.Up:
                    TransmitOpenCloseData(btnPowerMirrorUp.BackColor, monitorItem.PowerMirror.SetOpenUp, monitorItem.PowerMirror.SetCloseUp);
                    break;
                case MirrorDirection.Down:
                    TransmitOpenCloseData(btnPowerMirrorDown.BackColor, monitorItem.PowerMirror.SetOpenDown, monitorItem.PowerMirror.SetCloseDown);
                    break;
                default:
                    break;
            }
            Thread.Sleep(revertTime);
            if (timerEnabled && revertTrialCount < revertTrialLimit)
            {
                revertTrialCount++;
                Invoke(new Action(() => { btnPowerMirrorDown.Enabled = btnPowerMirrorLeft.Enabled = btnPowerMirrorRight.Enabled = btnPowerMirrorUp.Enabled = false; }));
                revertTimer.Start();
            }
            else
                Invoke(new Action(() => { btnPowerMirrorDown.Enabled = btnPowerMirrorLeft.Enabled = btnPowerMirrorRight.Enabled = btnPowerMirrorUp.Enabled = true; }));
        }

        /// <summary>
        /// Converts a BBT_BTS_ReadResponse value to its string representation.
        /// </summary>
        /// <param name="responseData">The BBT_BTS_ReadResponse value to convert.</param>
        /// <returns>The string representation of the BBT_BTS_ReadResponse value.</returns>
        private string SetResponseMessage(short responseData)
        {
            return ((BBT_BTS_ReadResponse)responseData).ToString();
        }

        /// <summary>
        /// Handles the button click event for reading power mirror in the up direction.
        /// </summary>
        /// <param name="sender">The button that triggered the event.</param>
        /// <param name="e">The event arguments.</param>
        private void btnReadUp_Click(object sender, EventArgs e)
        {
            if (!ConnectionUtil.CheckConnection())
                return;

            lblReadPowerMirrorUp.Text = "-";
            FormMain.TestClickCounter++;
            this.InvokeOnClick(this, new EventArgs());
            ConnectionUtil.TransmitData(uint.Parse(MessageID, NumberStyles.HexNumber), monitorItem.PowerMirror.ReadUp);
        }

        /// <summary>
        /// Handles the button click event for reading power mirror in the left direction.
        /// </summary>
        /// <param name="sender">The button that triggered the event.</param>
        /// <param name="e">The event arguments.</param>
        private void btnReadLeft_Click(object sender, EventArgs e)
        {
            if (!ConnectionUtil.CheckConnection())
                return;

            lblReadPowerMirrorLeft.Text = "-";
            FormMain.TestClickCounter++;
            this.InvokeOnClick(this, new EventArgs());
            ConnectionUtil.TransmitData(uint.Parse(MessageID, NumberStyles.HexNumber), monitorItem.PowerMirror.ReadLeftDown);
        }

        /// <summary>
        /// Handles the button click event for reading power mirror in the right direction.
        /// </summary>
        /// <param name="sender">The button that triggered the event.</param>
        /// <param name="e">The event arguments.</param>
        private void btnReadRight_Click(object sender, EventArgs e)
        {
            if (!ConnectionUtil.CheckConnection())
                return;

            lblReadPowerMirrorRight.Text = "-";
            FormMain.TestClickCounter++;
            this.InvokeOnClick(this, new EventArgs());
            ConnectionUtil.TransmitData(uint.Parse(MessageID, NumberStyles.HexNumber), monitorItem.PowerMirror.ReadRight);
        }

        /// <summary>
        /// Handles the button click event for controlling power mirror in the up direction.
        /// </summary>
        /// <param name="sender">The button that triggered the event.</param>
        /// <param name="e">The event arguments.</param>
        private void btnPowerMirrorUp_Click(object sender, EventArgs e)
        {
            mirrorDirection = MirrorDirection.Up;
            TransmitOpenCloseData(btnPowerMirrorUp.BackColor, monitorItem.PowerMirror.SetOpenUp, monitorItem.PowerMirror.SetCloseUp);
        }

        /// <summary>
        /// Handles the button click event for controlling power mirror in the right direction.
        /// </summary>
        /// <param name="sender">The button that triggered the event.</param>
        /// <param name="e">The event arguments.</param>
        private void btnPowerMirrorRight_Click(object sender, EventArgs e)
        {
            mirrorDirection = MirrorDirection.Right;
            TransmitOpenCloseData(btnPowerMirrorRight.BackColor, monitorItem.PowerMirror.SetOpenRight, monitorItem.PowerMirror.SetCloseRight);
        }

        /// <summary>
        /// Handles the button click event for controlling power mirror in the down direction.
        /// </summary>
        /// <param name="sender">The button that triggered the event.</param>
        /// <param name="e">The event arguments.</param>
        private void btnPowerMirrorDown_Click(object sender, EventArgs e)
        {
            mirrorDirection = MirrorDirection.Down;
            TransmitOpenCloseData(btnPowerMirrorDown.BackColor, monitorItem.PowerMirror.SetOpenDown, monitorItem.PowerMirror.SetCloseDown);
        }

        /// <summary>
        /// Handles the button click event for controlling power mirror in the left direction.
        /// </summary>
        /// <param name="sender">The button that triggered the event.</param>
        /// <param name="e">The event arguments.</param>
        private void btnPowerMirrorLeft_Click(object sender, EventArgs e)
        {
            mirrorDirection = MirrorDirection.Left;
            TransmitOpenCloseData(btnPowerMirrorLeft.BackColor, monitorItem.PowerMirror.SetOpenLeft, monitorItem.PowerMirror.SetCloseLeft);
        }

        /// <summary>
        /// Transmits open or close data based on the button color and state.
        /// </summary>
        /// <param name="btnColor">The color of the button.</param>
        /// <param name="openData">The data to transmit when opening.</param>
        /// <param name="closeData">The data to transmit when closing.</param>
        /// <param name="refColor">The reference color to check the button color against.</param>
        private void TransmitOpenCloseData(Color btnColor, byte[] openData, byte[] closeData, Color? refColor = null)
        {
            if (!ConnectionUtil.CheckConnection())
                return;

            FormMain.TestClickCounter++;
            this.InvokeOnClick(this, new EventArgs());
            bool state = refColor.HasValue ? btnColor == refColor : true;

            if (btnColor == Color.Transparent && state)
            {
                ConnectionUtil.TransmitData(uint.Parse(MessageID, NumberStyles.HexNumber), openData);
            }
            else if(state)
            {
                ConnectionUtil.TransmitData(uint.Parse(MessageID, NumberStyles.HexNumber), closeData);
            }
        }

        /// <summary>
        /// Handles the ValueChanged event of the numRevertTime control.
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
