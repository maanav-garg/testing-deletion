using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using AutosarBCM.Config;

namespace AutosarBCM.UserControls.Monitor
{
    /// <summary>
    /// Represents a user control for displaying EEPROM (Electrically Erasable Programmable Read-Only Memory) information.
    /// </summary>
    public partial class UCEEProm : OutputUserControl
    {
        #region Variables

        /// <summary>
        /// Gets or sets the first data byte represented by txtDataByte1.
        /// </summary>
        private byte data0 { get { return byte.Parse(string.IsNullOrEmpty(txtDataByte1.Text) ? "00" : txtDataByte1.Text.PadLeft(2, '0'), NumberStyles.HexNumber); } }

        /// <summary>
        /// Gets or sets the second data byte represented by txtDataByte2.
        /// </summary>
        private byte data1 { get { return byte.Parse(string.IsNullOrEmpty(txtDataByte2.Text) ? "00" : txtDataByte2.Text.PadLeft(2, '0'), NumberStyles.HexNumber); } }

        /// <summary>
        /// Gets or sets the third data byte represented by txtDataByte3.
        /// </summary>
        private byte data2 { get { return byte.Parse(string.IsNullOrEmpty(txtDataByte3.Text) ? "00" : txtDataByte3.Text.PadLeft(2, '0'), NumberStyles.HexNumber); } }

        /// <summary>
        /// Gets or sets the write address represented by txtWriteAddress.
        /// </summary>
        private byte[] writeAddress { get { return Helper.StringToByteArray(string.IsNullOrEmpty(txtWriteAddress.Text) ? "0000" : txtWriteAddress.Text.PadLeft(4, '0')); } }

        /// <summary>
        /// Gets or sets the read address represented by txtWriteAddress.
        /// </summary>
        private byte[] readAddress { get { return Helper.StringToByteArray(string.IsNullOrEmpty(txtReadAddress.Text) ? "0000" : txtReadAddress.Text.PadLeft(4, '0')); } }

        /// <summary>
        /// Gets the decimal representation of the read address.
        /// </summary>
        private short decimalReadAddress { get { return (short)Helper.GetValueOfPrimitive(readAddress, 0, 2); } }

        /// <summary>
        /// Gets or sets the associated OutputMonitorItem.
        /// </summary>
        private OutputMonitorItem monitorItem { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the UCEEProm class.
        /// </summary>
        /// <param name="monitorItem">The associated OutputMonitorItem.</param>
        public UCEEProm(OutputMonitorItem monitorItem)
        {
            InitializeComponent();
            this.monitorItem = monitorItem;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Handles the change in status based on the provided EEPROM response.
        /// </summary>
        /// <param name="outputResponse">The EEPROM response used to change the status.</param>
        public override void ChangeStatus(Response outputResponse)
        {
            var response = outputResponse as EEPromResponse;
            this.Invoke(new Action(() => { 
                if (response.RegisterGroup == (short)EEProm_ShortGroup.Read)
                {
                    if (response.EEPromAddress == decimalReadAddress)
                    {
                        lblData0.Text = response.ResponseData.ToString("x2").ToUpper();
                        lblData1.Text = response.ResponseData2.ToString("x2").ToUpper();
                        lblData2.Text = response.ResponseData3.ToString("x2").ToUpper();
                    }

                    btnRead.ForeColor = Color.Green;
                }
                else if (response.RegisterGroup == (short)EEProm_ShortGroup.Write)
                {
                    if(response.ResponseData == (byte)EEProm_WriteResponse.E_OK)
                        btnWrite.ForeColor = Color.Green;
                    else if ( response.ResponseData == (byte)EEProm_WriteResponse.E_NOT_OK)
                        btnWrite.ForeColor = Color.Red;
                }
                FormMain.TestClickCounter--;
            }));
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Handles the TextChanged event for txtDataByte1 textbox, allowing focus to txtDataByte2 when the text length reaches 2 characters.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void txtDataByte1_TextChanged(object sender, EventArgs e)
        {
            if (txtDataByte1.Text.Length == 2)
            {
                txtDataByte2.Focus();
                txtDataByte2.SelectAll();
            }
        }

        /// <summary>
        /// Handles the TextChanged event for txtDataByte2 textbox, allowing focus to txtDataByte3 when the text length reaches 2 characters.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void txtDataByte2_TextChanged(object sender, EventArgs e)
        {
            if (txtDataByte2.Text.Length == 2)
            {
                txtDataByte3.Focus();
                txtDataByte3.SelectAll();
            }
        }

        /// <summary>
        /// Handles the Click event for the Write button, validates the address, prepares the data, and transmits it.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void btnWrite_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtWriteAddress.Text))
            {
                Helper.ShowWarningMessageBox("Address is empty!");
                return;
            }

            btnWrite.ForeColor = Color.Black;
            Array.Copy(writeAddress, 0, monitorItem.EEProm.WriteData, 3, 2);
            monitorItem.EEProm.WriteData[5] = data0;
            monitorItem.EEProm.WriteData[6] = data1;
            monitorItem.EEProm.WriteData[7] = data2;

            TransmitData(monitorItem.EEProm.WriteData);
        }

        /// <summary>
        /// Handles the Click event for the Read button, validates the address, prepares the data, and transmits it.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void btnRead_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtReadAddress.Text))
            {
                Helper.ShowWarningMessageBox("Address is empty!");
                return;
            }

            lblData0.Text = "-";
            lblData1.Text = "-";
            lblData2.Text = "-";
            btnRead.ForeColor = Color.Black;
            Array.Copy(readAddress,0, monitorItem.EEProm.ReadData,3,2);

            TransmitData(monitorItem.EEProm.ReadData);
        }

        /// <summary>
        /// Transmits data if a connection is available.
        /// </summary>
        private void TransmitData(byte[] data)
        {
            if (!ConnectionUtil.CheckConnection())
                return;
            FormMain.TestClickCounter++;
            ConnectionUtil.TransmitData(uint.Parse(monitorItem.MessageIdOrDefault, NumberStyles.HexNumber),data);
        }

        /// <summary>
        /// Handles the Leave event for address textboxes, validates the address range, and displays warnings if necessary.
        /// </summary>
        private void Address_Leave(object sender, EventArgs e)
        {
            var txt = sender as TextBox;

            if (int.Parse(txt.Text, NumberStyles.HexNumber) < int.Parse(monitorItem.EEProm.LowerAddressLimit, NumberStyles.HexNumber))
            {
                txt.Text = monitorItem.EEProm.LowerAddressLimit;
                Helper.ShowWarningMessageBox("Address is lower than the limit!");
            }
            else if (int.Parse(txt.Text, NumberStyles.HexNumber) > int.Parse(monitorItem.EEProm.UpperAddressLimit, NumberStyles.HexNumber))
            {
                txt.Text = monitorItem.EEProm.UpperAddressLimit;
                Helper.ShowWarningMessageBox("Address is higher than the limit!");
            }
        }

        /// <summary>
        /// Handles the KeyPress event for data byte textboxes, allowing only hexadecimal input.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void txtDataByte_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(Helper.IsHexadecimal(e.KeyChar));
        }

        #endregion
    }
}
