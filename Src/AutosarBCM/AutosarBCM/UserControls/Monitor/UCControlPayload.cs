using AutosarBCM.Core;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AutosarBCM.UserControls.Monitor
{
    public partial class UCControlPayload : UserControl
    {
        #region Variables

        private PayloadInfo payloadInfo;

        public bool IsSelected
        {
            get { return chkSelected.Checked; }
        }
        public byte[] SelectedValue 
        {
            get 
            {
                if (cmbValue.Visible)
                    return (cmbValue.SelectedItem as PayloadValue).Value;
                else if (txtPwm.Visible)
                    return AddPwmValueIfValid();
                else
                    return TextBoxesToArray();
            }
        }

        #endregion

        #region Constructor
        public UCControlPayload(PayloadInfo payloadInfo, bool isControlMaskActive)
        {
            InitializeComponent();
            this.payloadInfo = payloadInfo;
            lblName.Text = payloadInfo.Name;
            chkSelected.Visible = isControlMaskActive;
            LoadControl();
        }

        #endregion

        #region Public Methods

        public void SetDefaultValue()
        {
            chkSelected.Checked = false;
            cmbValue.SelectedIndex = cmbValue.SelectedIndex != -1 ? 0 : -1;
            txtPwm.Text = "00000";
            txtDataByte1.Text = txtDataByte2.Text = txtDataByte3.Text = txtDataByte4.Text = "00";
        }

        #endregion

        #region Private Methods

        private void LoadControl()
        {
            var info = ASContext.Configuration.GetPayloadInfoByType(payloadInfo.TypeName);
    
            if (info.Values?.Count > 0)
            {
                cmbValue.Visible = true;
                cmbValue.DataSource = info.Values;
                cmbValue.DisplayMember = "FormattedValue";
            }
            else if (info.TypeName == "DID_PWM")
            {
                txtPwm.Visible = true;
                txtPwm.TextChanged += new EventHandler(PWMTextBox_TextChanged);
            }
            else 
            {
                pnlHexBytes.Visible = true;
                if (info.Length == 1)
                    txtDataByte2.Visible = txtDataByte3.Visible = txtDataByte4.Visible = false;
                else if (info.Length == 2)
                    txtDataByte3.Visible = txtDataByte4.Visible = false;
            }
            
         
            
        }
        private void PWMTextBox_TextChanged(object sender, EventArgs e)
        {
            int value;
            if (int.TryParse(txtPwm.Text, out value) && value > 10000)
            {
                txtPwm.Text = "10000"; 
                txtPwm.SelectionStart = txtPwm.Text.Length;
            }
        }
        /// <summary>
        /// Parses the values of the message data from each TextBox and returns the result as an array of bytes.
        /// </summary>
        /// <returns>The data of the message as a byte array.</returns>
        private byte[] TextBoxesToArray()
        {
            var data = new List<byte>();
            data.Add(byte.Parse(txtDataByte1.Text, System.Globalization.NumberStyles.AllowHexSpecifier));
            if(txtDataByte2.Visible)
                data.Add(byte.Parse(txtDataByte2.Text, System.Globalization.NumberStyles.AllowHexSpecifier));
            if (txtDataByte3.Visible)
                data.Add(byte.Parse(txtDataByte3.Text, System.Globalization.NumberStyles.AllowHexSpecifier));
            if (txtDataByte4.Visible)
                data.Add(byte.Parse(txtDataByte4.Text, System.Globalization.NumberStyles.AllowHexSpecifier));
            return data.ToArray();
        }

        /// <summary>
        /// An event handler to the hex textbox TextChanged event.
        /// </summary>
        /// <param name="sender">A reference to the textbox instance.</param>
        /// <param name="e">A reference to the TextChanged event's arguments.</param>
        private void txtDataByte_TextChanged(object sender, EventArgs e)
        {
            var txt = sender as TextBox;
            if (txt.Text.Length == 2)
            {
                txt.Focus();
                txt.SelectAll();
            }
        }

        /// <summary>
        /// An event handler to the txtDataByte's Click event.
        /// </summary>
        /// <param name="sender">A reference to the txtDataByte instance.</param>
        /// <param name="eventArgs">A reference to the Click event's arguments.</param>
        private void TextBoxOnClick(object sender, EventArgs eventArgs)
        {
            var textBox = (TextBox)sender;
            textBox.SelectAll();
            textBox.Focus();
        }

        /// <summary>
        /// An event handler to the txtDataByte's KeyPress event.
        /// </summary>
        /// <param name="sender">A reference to the txtDataByte instance.</param>
        /// <param name="keyEventArgs">A reference to the KeyPress event's arguments.</param>
        private void TextBoxKeyPress(object sender, KeyPressEventArgs keyEventArgs)
        {
            keyEventArgs.Handled = !Helper.IsHexadecimal(keyEventArgs.KeyChar);
        }

        private byte[] AddPwmValueIfValid()
        {
            var bytes = new List<byte>();
            
            if (txtPwm.Text != "00000" && int.TryParse(txtPwm.Text, out int pwmValue))
            {
                byte[] pwmBytes = BitConverter.GetBytes((ushort)pwmValue);
                if (BitConverter.IsLittleEndian)
                {
                    Array.Reverse(pwmBytes);
                }
                bytes.AddRange(pwmBytes);
            }
            else
            {
                bytes.AddRange(new byte[2]);
            }

            return bytes.ToArray();
        }
        private byte[] TrimLeadingZeros(byte[] bytes)
        {
            int startIndex = Array.FindIndex(bytes, b => b != 0x00);

            if (startIndex == -1)
            {
                return new byte[0];
            }
            byte[] trimmedBytes = new byte[bytes.Length - startIndex];
            Array.Copy(bytes, startIndex, trimmedBytes, 0, bytes.Length - startIndex);

            return trimmedBytes;
        }

        #endregion


    }
}
