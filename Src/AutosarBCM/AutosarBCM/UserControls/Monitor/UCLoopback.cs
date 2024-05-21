using AutosarBCM.Config;
using System;

namespace AutosarBCM.UserControls.Monitor
{
    /// <summary>
    /// Represents a user control for performing loopback tests and displaying their results.
    /// </summary>
    public partial class UCLoopback : OutputUserControl
    {
        #region Variables

        /// <summary>
        /// Gets or sets the associated OutputMonitorItem for this control.
        /// </summary>
        private OutputMonitorItem item;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the UCLoopback class.
        /// </summary>
        /// <param name="item">The OutputMonitorItem associated with this control.</param>
        public UCLoopback(OutputMonitorItem item)
        {
            InitializeComponent();
            this.item = item;

            this.lblLine1.Text = item.Loopback.Pair1Name;
            this.lblLine2.Text = item.Loopback.Pair2Name;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Overrides the ChangeStatus method to handle responses and update the control's UI accordingly.
        /// </summary>
        /// <param name="outputResponse">The response received from the device.</param>
        public override void ChangeStatus(Response outputResponse)
        {
            this.Invoke(new Action(() =>
            {
                if (outputResponse.RegisterGroup == (short)Output_ReadGroup.LOOPBACK)
                {
                    if (outputResponse.RegisterAddress == item.Loopback.Pair1Data[4] || outputResponse.RegisterAddress == item.Loopback.Pair2Data[4])
                        lblResponse.Text = "OK";
                    else
                        lblResponse.Text = "NOK";
                }
                else if (outputResponse.RegisterGroup == (short)Output_ReadGroup.LOOPBACK_RESULT)
                {
                    if (outputResponse.RegisterAddress == item.Loopback.Pair1Data[4])
                        lblPair1Response.Text = ((Loopback_Response)outputResponse.ResponseData).ToString();
                    else if (outputResponse.RegisterAddress == item.Loopback.Pair2Data[4])
                        lblPair2Response.Text = ((Loopback_Response)outputResponse.ResponseData).ToString();
                }
                FormMain.TestClickCounter--;
            }));
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Handles the Click event of the Pair1 button, initiating a loopback test for Pair 1.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void btnPair1_Click(object sender, EventArgs e)
        {
            if (!ConnectionUtil.CheckConnection())
                return;

            FormMain.TestClickCounter++;
            lblResponse.Text = "0";
            //new UdsMessage() { Id = item.MessageIdOrDefault, Data = item.Loopback.Pair1Data }.Transmit();
            this.InvokeOnClick(this, new EventArgs());
        }

        /// <summary>
        /// Handles the Click event of the Pair2 button, initiating a loopback test for Pair 2.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void btnPair2_Click(object sender, EventArgs e)
        {
            if (!ConnectionUtil.CheckConnection())
                return;

            FormMain.TestClickCounter++;
            lblResponse.Text = "0";
            //new UdsMessage() { Id = item.MessageIdOrDefault, Data = item.Loopback.Pair2Data }.Transmit();
            this.InvokeOnClick(this, new EventArgs());
        }

        /// <summary>
        /// Handles the Click event of the Read button, initiating a verification loopback test.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void btnRead_Click(object sender, EventArgs e)
        {
            if (!ConnectionUtil.CheckConnection())
                return;

            lblPair1Response.Text = "0";
            lblPair2Response.Text = "0";

            FormMain.TestClickCounter++;
            //new UdsMessage() { Id = item.MessageIdOrDefault, Data = item.Loopback.Verification }.Transmit();
            this.InvokeOnClick(this, new EventArgs());
        }

        /// <summary>
        /// Handles the Click event of the Line1 label, triggering a click event for the control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void lblLine1_Click(object sender, EventArgs e)
        {
            this.InvokeOnClick(this, new EventArgs());
        }

        /// <summary>
        /// Handles the Click event of the Line2 label, triggering a click event for the control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void lblLine2_Click(object sender, EventArgs e)
        {
            this.InvokeOnClick(this, new EventArgs());
        }

        #endregion
    }
}
