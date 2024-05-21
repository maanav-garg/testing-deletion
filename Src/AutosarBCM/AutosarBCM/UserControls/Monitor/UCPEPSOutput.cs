using System;
using System.Drawing;
using System.Globalization;
using System.Linq;
using AutosarBCM.Config;
using AutosarBCM.Forms.Monitor;

namespace AutosarBCM.UserControls.Monitor
{
    /// <summary>
    /// Represents a User Control for displaying PEPS (Passive Entry Passive Start) output information.
    /// </summary>
    public partial class UCPEPSOutput : OutputUserControl
    {
        #region Variables

        /// <summary>
        /// The OutputMonitorItem associated with this user control.
        /// </summary>
        private OutputMonitorItem item;

        /// <summary>
        /// The string representation of "Read_Keyfob" used to determine the control's visibility.
        /// </summary>
        private string readKeyfob = "Read_Keyfob";

        /// <summary>
        /// The string representation of "GET_RSSI_Measurement" used to determine the control's visibility.
        /// </summary>
        private string rssiMeasurement = "GET_RSSI_Measurement";

        /// <summary>
        /// The string representation of "Immobilizer" used to determine the control's visibility.
        /// </summary>
        private string immobilizier = "Immobilizer";

        /// <summary>
        /// The string representation of "Door_Cap_Sensor" used to determine the control's visibility.
        /// </summary>
        private string doorCapSensor = "Door_Cap_Sensor";

        /// <summary>
        /// The string representation of "Door_Cap_Sensor" used to determine the control's visibility.
        /// </summary>
        private string tempMeasurement = "Temperature_Measurement";
            
        /// <summary>
        /// The string representation of "Door_Cap_Sensor" used to determine the control's visibility.
        /// </summary>
        private string NCK2910_GPIO = "NCK2910_GPIO_Output_Pins";

        /// <summary>
        /// An array to store RSSI (Received Signal Strength Indicator) measurement values.
        /// </summary>
        private float[] rssiValues = new float[3];
        /// <summary>
        /// Holds the number of cap sensor signals received.
        /// </summary>
        private int capSensor1Counter = 0;
        /// <summary>
        /// Holds the number of cap sensor signals received.
        /// </summary>
        private int capSensor2Counter = 0;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the UCPEPSOutput class with the specified OutputMonitorItem.
        /// </summary>
        /// <param name="item">The OutputMonitorItem associated with this user control.</param>
        public UCPEPSOutput(OutputMonitorItem item)
        {
            InitializeComponent();
            this.item = item;

            lblName.Text = item.Name;            

            grpMain.Visible = item.Name == readKeyfob || item.Name == immobilizier || item.Name == Constants.PEPS_Get_Key_List || item.Name == tempMeasurement;
            grpRSSI.Visible = item.Name == rssiMeasurement;
            grpDoorCap.Visible = item.Name == doorCapSensor;
            grpNCK2910.Visible = item.Name == NCK2910_GPIO;
            lblBatteryLevelName.Visible = lblBatteryLevelValue.Visible = lblKeyfobName.Visible = lblKeyfobId.Visible = lblButtonIden.Visible = lblButtonIdenValue.Visible = item.Name == readKeyfob;
            lblButtonIden.Visible = lblButtonIdenValue.Visible = item.Name == tempMeasurement;
            lblButtonIden.Text = item.Name == tempMeasurement ? "Temperature:" : lblBatteryLevelName.Text;
            lblValue2.Visible = item.Name != readKeyfob;
            lblCapSensor1Wup.Visible = lblCapSensor1WupCount.Visible = lblCapSensor2Wup.Visible = lblCapSensor2WupCount.Visible = item.Name == doorCapSensor;
            lblValue1.Visible = lblValue2.Visible = item.Name != tempMeasurement;
            cmbLevel.DataSource = Enum.GetValues(typeof(NCK2910_GPIO_LevelValues));
            cmbPIN.DataSource = Enum.GetValues(typeof(NCK2910_GPIO_PINValues));
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Handles the response change for the PEPS control.
        /// </summary>
        /// <param name="response">The response object containing PEPS data.</param>
        public override void ChangeStatus(Response response)
        {
            var pepsResponse = (PEPSResponse)response;
            lblValue1.Invoke(new Action(() =>
            {
                if (pepsResponse.RegisterAddress == 3)
                {
                    if (pepsResponse.RawData[9] == 0)
                        rssiValues[0] = pepsResponse.ResponseData32 / 1000;
                    else if (response.RawData[9] == 1)
                        rssiValues[1] = pepsResponse.ResponseData32 / 1000;
                    else if (response.RawData[9] == 2)
                        rssiValues[2] = pepsResponse.ResponseData32 / 1000;

                    lblX.Text = $"{rssiValues[0]} mV";
                    lblY.Text = $"{rssiValues[1]} mV";
                    lblZ.Text = $"{rssiValues[2]} mV";
                    lblSQSUM.Text = Math.Sqrt(Math.Pow(rssiValues[0], 2) + Math.Pow(rssiValues[1], 2) + Math.Pow(rssiValues[2], 2)).ToString();
                }
                else if (response.RegisterAddress == 1)
                {
                    if (response.RawData[9] == 0)
                    {
                        ((FormMonitorGenericOutput)this.ParentForm).SetKeyListForRSSI(response.RawData.Skip(5).Take(4).ToArray());
                        lblKeyfobId.Text = Helper.ByteArrayToString(response.RawData.Skip(5).Take(4).ToArray()).ToUpper();
                    }
                    else if (response.RawData[9] == 1)
                    {
                        lblButtonIdenValue.Text = GetButtonIdentifier(response.RawData[5]);
                        lblValue1.Text = ((float)((short)Helper.GetValueOfPrimitive(response.RawData, 6, 2)) / 10).ToString() + " s";
                        lblBatteryLevelValue.Text = (response.RawData[8] == 0x01) ? "OK" : "NOK";
                    }
                }
                else if (response.RegisterAddress == 2 || response.RegisterAddress == 4)
                {
                    lblValue1.Text = pepsResponse.ResponseData.ToString("X2");
                    lblValue2.Text = pepsResponse.ResponseData2.ToString("X2");

                    if (response.RegisterAddress == 2)
                        ((FormMonitorGenericOutput)this.ParentForm).SetKeyListForRSSI(response.RawData.Skip(5).Take(4).ToArray());
                }
                else if (response.RegisterAddress == 6)
                {
                    lblButtonIdenValue.Text = ((float)(response.ResponseData2) / 1000).ToString();
                }
                else if (response.RegisterAddress == 7)
                {
                    lblA.Text = response.RawData[5].ToString("X2");
                    lblB.Text = response.RawData[6].ToString("X2");
                    lblC.Text = response.RawData[7].ToString("X2");
                    lblD.Text = response.RawData[8].ToString("X2");
                }
                
                if (response.RegisterAddress == 5)
                {
                    if (response.RawData[2] == 0x03 && response.RawData[3] == 0xEF && response.RawData[4] == 0x05)
                    {
                        if (response.RawData[5] == 0x01)
                            capSensor1Counter++;
                        if (response.RawData[5] == 0x02)
                            capSensor2Counter++;

                        lblCapSensor1WupCount.Text = capSensor1Counter.ToString();
                        lblCapSensor2WupCount.Text = capSensor2Counter.ToString();
                    }
                }
                else
                {
                    FormMain.TestClickCounter--;
                }
            }));
        }

        /// <summary>
        /// Sets the Keyfob ID values in the PEPS control.
        /// </summary>
        /// <param name="keyfobID">The array containing Keyfob ID values.</param>
        internal void SetKeyfobID(byte[] keyfobID)
        {
            txtKeyfobID1.Text = keyfobID[0].ToString("X2");
            txtKeyfobID2.Text = keyfobID[1].ToString("X2");
            txtKeyfobID3.Text = keyfobID[2].ToString("X2");
            txtKeyfobID4.Text = keyfobID[3].ToString("X2");
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Handles the button click event for reading RSSI data.
        /// </summary>
        /// <param name="sender">The button that triggered the event.</param>
        /// <param name="e">The event arguments.</param>
        private void btnRSSI_Click(object sender, EventArgs e)
        {
            if (!ConnectionUtil.CheckConnection())
                return;

            var keyfobID = GetKeyfobID();
            if (keyfobID == null)
            {
                Helper.ShowWarningMessageBox("Please set the keyfob Id first.");
                return;
            }

            FormMain.TestClickCounter += 3;

            var data = item.PEPSData;
            keyfobID.CopyTo(data, 3);
            //new UdsMessage { Id = item.MessageIdOrDefault, Data = data }.Transmit();
            this.InvokeOnClick(this, new EventArgs());
        }

        /// <summary>
        /// Handles the button click event for reading PEPS data.
        /// </summary>
        /// <param name="sender">The button that triggered the event.</param>
        /// <param name="e">The event arguments.</param>
        private void btnRead_Click(object sender, EventArgs e)
        {
            if (!ConnectionUtil.CheckConnection())
                return;

            if (item.Name == tempMeasurement)
            {
                lblButtonIdenValue.Text = "-";
            }

            if (item.Name == readKeyfob)
            {
                FormMain.TestClickCounter += 3;
                lblButtonIdenValue.Text = "-";
                lblKeyfobId.Text = "-";
                lblBatteryLevelValue.Text = "-";
                lblValue1.Text = "0";
            }
            else
                FormMain.TestClickCounter++;

            //new UdsMessage { Id = item.MessageIdOrDefault, Data = item.PEPSData }.Transmit();
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

        /// <summary>
        /// Gets the Keyfob ID from the text boxes.
        /// </summary>
        /// <returns>The Keyfob ID as a byte array, or null if parsing fails.</returns>
        private byte[] GetKeyfobID()
        {
            if (byte.TryParse(txtKeyfobID1.Text, NumberStyles.HexNumber, null, out byte value1) &&
                byte.TryParse(txtKeyfobID2.Text, NumberStyles.HexNumber, null, out byte value2) &&
                byte.TryParse(txtKeyfobID3.Text, NumberStyles.HexNumber, null, out byte value3) &&
                byte.TryParse(txtKeyfobID4.Text, NumberStyles.HexNumber, null, out byte value4))
                return new byte[] { value1, value2, value3, value4 };
            return null;
        }

        /// <summary>
        /// Handles the click event for the control's name label.
        /// </summary>
        /// <param name="sender">The label that triggered the event.</param>
        /// <param name="e">The event arguments.</param>
        private void lblName_Click(object sender, EventArgs e)
        {
            this.InvokeOnClick(this, new EventArgs());
        }

        private void btnNCK2910_Click(object sender, EventArgs e)
        {
            this.InvokeOnClick(this, new EventArgs());

            if (!ConnectionUtil.CheckConnection())
                return;

            lblA.Text = lblB.Text = lblC.Text = lblD.Text = "-";

            item.PEPSData[5] = (byte)((NCK2910_GPIO_PINValues)cmbPIN.SelectedItem);
            item.PEPSData[6] = (byte)((NCK2910_GPIO_LevelValues)cmbPIN.SelectedItem);

            FormMain.TestClickCounter++;
            //new UdsMessage { Id = item.MessageIdOrDefault, Data = item.PEPSData }.Transmit();

        }
        #endregion

    }
}
