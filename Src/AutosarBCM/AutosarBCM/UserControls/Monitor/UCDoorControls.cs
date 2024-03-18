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
    /// Represents a user control for door controls, including locking and unlocking.
    /// </summary>
    public partial class UCDoorControls : OutputUserControl
    {
        #region Variables

        /// <summary>
        /// The output monitor item associated with this control.
        /// </summary>
        OutputMonitorItem monitorItem;

        /// <summary>
        /// Time in milliseconds before locking reverts.
        /// </summary>
        private int LockRevertTime = 0;

        /// <summary>
        /// Time in milliseconds before unlocking reverts.
        /// </summary>
        private int UnlockRevertTime = 0;

        /// <summary>
        /// Timer for locking reversion.
        /// </summary>
        private System.Timers.Timer lockRevertTimer = new System.Timers.Timer();

        /// <summary>
        /// Indicates whether the locking reversion timer is enabled.
        /// </summary>
        private bool lockRevertTimerEnabled = false;

        /// <summary>
        /// Timer for unlocking reversion.
        /// </summary>
        private System.Timers.Timer unlockRevertTimer = new System.Timers.Timer();

        /// <summary>
        /// Indicates whether the unlocking reversion timer is enabled.
        /// </summary>
        private bool unlockRevertTimerEnabled = false;

        /// <summary>
        /// The risk level associated with this control.
        /// </summary>
        private RiskLevels riskLevel = default;

        /// <summary>
        /// The risk limit for this control.
        /// </summary>
        private RiskLevels riskLimit = default;

        /// <summary>
        /// Indicates whether the risk has been accepted for this control.
        /// </summary>
        private bool riskAccepted = false;

        /// <summary>
        /// The limit for the number of revert trials.
        /// </summary>
        private int revertTrialLimit = 0;

        /// <summary>
        /// The current trial count for locking reversion.
        /// </summary>
        private int lockRevertTrialCount = 0;

        /// <summary>
        /// The current trial count for unlocking reversion.
        /// </summary>
        private int unlockRevertTrialCount = 0;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the UCDoorControls class with the specified parameters.
        /// </summary>
        /// <param name="item">The OutputMonitorItem associated with this control.</param>
        /// <param name="riskLimit">The risk limit for this control.</param>
        /// <param name="revertTrial">The limit for the number of revert trials.</param>
        public UCDoorControls(OutputMonitorItem item,string riskLimit, int revertTrial)
        {
            InitializeComponent();
            monitorItem = item;

            numRevertTimeLock.Value = LockRevertTime = item.DoorControl.DoorLockRevertTime;
            numRevertTimeUnlock.Value = UnlockRevertTime = item.DoorControl.DoorUnlockRevertTime;

            lockRevertTimer.Elapsed += LockRevertTimer_Elapsed;
            unlockRevertTimer.Elapsed += UnlockRevertTimer_Elapsed;

            lblName.Text = item.Name;

            if(!String.IsNullOrEmpty(item.RiskLevel))
                riskLevel = Helper.GetEnumValue<RiskLevels>(item.RiskLevel);
            if(!String.IsNullOrEmpty(riskLimit))
                this.riskLimit = Helper.GetEnumValue<RiskLevels>(riskLimit);

            pcbMediumRisk.Visible = riskLevel == RiskLevels.Medium;
            pcbHighRisk.Visible = riskLevel == RiskLevels.High;

            revertTrialLimit = revertTrial;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Changes the status of the door control based on the given response.
        /// This method is overridden from the base class and should implement the logic for changing the status.
        /// </summary>
        /// <param name="outputResponse">The response used to change the status.</param>
        public override void ChangeStatus(Response outputResponse)
        {
            this.Invoke(new Action(() =>
            {
                if (outputResponse.RegisterGroup == (short)DoorControls.Enable)
                {
                    if (outputResponse.RegisterAddress % 2 == 0) //lock
                    {
                        btnLock.BackColor = Color.DarkBlue;
                        lockRevertTimerEnabled = true; btnUnlock.Enabled = false;
                        if (numRevertTimeLock.Value > 0)
                        {
                            btnLock.Enabled = false;
                            lockRevertTimer.Start();
                        }
                    }
                    else //unlock
                    {
                        btnUnlock.BackColor = Color.DarkBlue;
                        lockRevertTimerEnabled = true; btnLock.Enabled = false;
                        if (numRevertTimeUnlock.Value > 0)
                        {
                            btnUnlock.Enabled = false;
                            unlockRevertTimer.Start();
                        }
                    }
                }
                else if (outputResponse.RegisterGroup == (short)DoorControls.Disable)
                {
                    if (outputResponse.RegisterAddress % 2 == 0) //lock
                    {
                        lockRevertTimerEnabled = false; btnUnlock.Enabled = true;
                        btnLock.BackColor = Color.Transparent;
                        lockRevertTrialCount = 0;
                    }
                    else //unlock
                    {
                        unlockRevertTimerEnabled = false; btnLock.Enabled = true;
                        btnUnlock.BackColor = Color.Transparent;
                        unlockRevertTrialCount = 0;
                    }
                }
                else if ((outputResponse.RegisterGroup == (short)Output_ReadGroup.VNHD7008AY))
                {
                    if (outputResponse.RegisterAddress == monitorItem.DoorControl.ReadDoorLockDiag[4])
                        lblLockDiag.Text = ((VNHD7008AY_Response)outputResponse.ResponseData).ToString();

                    else if (outputResponse.RegisterAddress == monitorItem.DoorControl.ReadDoorUnlockDiag[4])
                        lblUnlockDiag.Text = ((VNHD7008AY_Response)outputResponse.ResponseData).ToString();
                }
                FormMain.TestClickCounter--;
            }));            
        }

        #endregion

        #region Private Methods

        #region Timer Elapsed Funcs

        /// <summary>
        /// Handles the Elapsed event of the LockRevertTimer to revert the lock status.
        /// </summary>
        /// <param name="source">The source of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void LockRevertTimer_Elapsed(object source, ElapsedEventArgs e)
        {
            StartRevertTimer(btnLock,monitorItem.DoorControl.DoorLockDisable, LockRevertTime,ref lockRevertTimerEnabled,ref lockRevertTrialCount, lockRevertTimer);
        }

        /// <summary>
        /// Handles the Elapsed event of the UnlockRevertTimer to revert the unlock status.
        /// </summary>
        /// <param name="source">The source of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void UnlockRevertTimer_Elapsed(object source, ElapsedEventArgs e)
        {
            StartRevertTimer(btnUnlock, monitorItem.DoorControl.DoorUnlockDisable, UnlockRevertTime,ref unlockRevertTimerEnabled,ref unlockRevertTrialCount, unlockRevertTimer);
        }

        /// <summary>
        /// Starts a reversion timer for a button control based on the specified parameters.
        /// </summary>
        /// <param name="btn">The button control associated with the timer.</param>
        /// <param name="data">The data to transmit for reversion.</param>
        /// <param name="revertTime">The time in milliseconds before reversion.</param>
        /// <param name="isEnable">A reference to a boolean indicating whether the timer is enabled.</param>
        /// <param name="counter">A reference to an integer counter for trials.</param>
        /// <param name="timer">The timer to start.</param>
        private void StartRevertTimer(Button btn, byte[] data,int revertTime,ref bool isEnable,ref int counter, System.Timers.Timer timer)
        {
            timer.Stop();

            TransmitMessage(data);

            Thread.Sleep(revertTime);
            if (isEnable && counter < revertTrialLimit)
            {
                counter++;
                btn.Invoke(new Action(() => { btn.Enabled = false; }));
                timer.Start();
            }
            else
                btn.Invoke(new Action(() => { btn.Enabled = true; }));
        }

        #endregion

        #region Click Events

        /// <summary>
        /// Handles the Click event of the btnLock control to lock the door or unlock it based on its current state.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void btnLock_Click(object sender, EventArgs e)
        {
            if (btnLock.BackColor == Color.Transparent)
                TransmitMessage(monitorItem.DoorControl.DoorLockEnable);
            else
                TransmitMessage(monitorItem.DoorControl.DoorLockDisable);
            this.InvokeOnClick(this, new EventArgs());
        }

        /// <summary>
        /// Handles the Click event of the btnUnlock control to unlock the door or lock it based on its current state.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void btnUnlock_Click(object sender, EventArgs e)
        {
            btnLock.Focus();
            if (btnUnlock.BackColor == Color.Transparent)
                TransmitMessage(monitorItem.DoorControl.DoorUnlockEnable);
            else
                TransmitMessage(monitorItem.DoorControl.DoorUnlockDisable);
            this.InvokeOnClick(this, new EventArgs());
        }

        /// <summary>
        /// Handles the Click event of the btnLockDiagRead control to read the door lock diagnostic data.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void btnLockDiagRead_Click(object sender, EventArgs e)
        {
            lblLockDiag.Text = "-";
            TransmitMessage(monitorItem.DoorControl.ReadDoorLockDiag);
            this.InvokeOnClick(this, new EventArgs());
        }

        /// <summary>
        /// Handles the Click event of the btnUnlockDiagRead control to read the door unlock diagnostic data.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void btnUnlockDiagRead_Click(object sender, EventArgs e)
        {
            lblUnlockDiag.Text = "-";
            TransmitMessage(monitorItem.DoorControl.ReadDoorUnlockDiag);
            this.InvokeOnClick(this, new EventArgs());
        }

        /// <summary>
        /// Transmits the specified message data if a connection is established.
        /// </summary>
        /// <param name="data">The data to transmit.</param>
        private void TransmitMessage(byte[] data)
        {
            if(!ConnectionUtil.CheckConnection())
                return;

            FormMain.TestClickCounter++;
            ConnectionUtil.TransmitData(uint.Parse(MessageID, NumberStyles.HexNumber), data);
        }

        /// <summary>
        /// Handles the Click event of the lblName control to invoke the click event of this control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void lblName_Click(object sender, EventArgs e)
        {
            this.InvokeOnClick(this, new EventArgs());
        }

        #endregion

        #region Numeric Up Down

        /// <summary>
        /// Handles the MouseHover event of the pcbHighRisk control to display a tooltip for high risk.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void pcbHighRisk_MouseHover(object sender, EventArgs e)
        {
            ToolTip tt = new ToolTip();
            tt.SetToolTip(this.pcbHighRisk, "High Risk");
        }

        /// <summary>
        /// Handles the MouseHover event of the pcbMediumRisk control to display a tooltip for medium risk.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void pcbMediumRisk_MouseHover(object sender, EventArgs e)
        {
            ToolTip tt = new ToolTip();
            tt.SetToolTip(this.pcbMediumRisk, "Medium Risk");
        }

        /// <summary>
        /// Handles the MouseHover event of the pcbAccepted control to display a tooltip for risk acceptance.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void pcbAccepted_MouseHover(object sender, EventArgs e)
        {
            ToolTip tt = new ToolTip();
            tt.SetToolTip(this.pcbAccepted, "Risk Accepted");
        }

        /// <summary>
        /// Handles the ValueChanged event of the numRevertTimeLock control to update the lock reversion time.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void numRevertTimeLock_ValueChanged(object sender, EventArgs e)
        {
            if (CheckIsRisky(LockRevertTime, numRevertTimeLock, numRevertTimeLock_ValueChanged))
                return;

                if (numRevertTimeLock.Value > 0)
                    lockRevertTimer.Interval = UnlockRevertTime = (int)numRevertTimeLock.Value;
                else
                    UnlockRevertTime = (int)numRevertTimeLock.Value;
        }

        /// <summary>
        /// Handles the ValueChanged event of the numRevertTimeUnlock control to update the unlock reversion time.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void numRevertTimeUnlock_ValueChanged(object sender, EventArgs e)
        {
            if (CheckIsRisky(UnlockRevertTime, numRevertTimeUnlock, numRevertTimeUnlock_ValueChanged))
                return;

                if (numRevertTimeUnlock.Value > 0)
                    unlockRevertTimer.Interval = UnlockRevertTime = (int)numRevertTimeUnlock.Value;
                else
                    UnlockRevertTime = (int)numRevertTimeUnlock.Value;
        }

        /// <summary>
        /// Checks if changing the revert time is risky and handles it accordingly.
        /// </summary>
        /// <param name="revertTime">The current revert time.</param>
        /// <param name="numRevert">The numeric up-down control for the revert time.</param>
        /// <param name="handler">The event handler for ValueChanged event.</param>
        /// <returns>True if the change is considered risky and is not accepted; otherwise, false.</returns>
        private bool CheckIsRisky(int revertTime, NumericUpDown numRevert, EventHandler handler)
        {
            if (riskLevel > riskLimit && !riskAccepted)
            {
                if (!(pcbAccepted.Visible = riskAccepted = Helper.ShowConfirmationMessageBox($"Changing this value is risky!")))
                {
                    numRevert.ValueChanged -= handler;
                    numRevert.Value = revertTime;
                    numRevert.ValueChanged += handler;
                    return true;
                }
            }
            return false;
        }

        #endregion

        #endregion
    }
}
