using AutosarBCM.Properties;
using AutosarBCM.UserControls.Monitor;
using Connection.Hardware;
using Connection.Hardware.Can;
using Connection.Hardware.SP;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Windows.Forms;

namespace AutosarBCM.Common
{
    /// <summary>
    /// Hardware list window
    /// </summary>
    public partial class FormHardwareList : Form
    {
        #region Variables

        /// <summary>
        /// Gets the index of the selected item.
        /// </summary>
        public int SelectedIndex { get; private set; }
        private string SelectedItem { get; set; }
        public SortedDictionary<string, Tuple<int, string>> AdditionalHardware { get; private set; }


        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the FormHardwareList class.
        /// </summary>
        /// <param name="list">A list of all connected devices.</param>
        public FormHardwareList(List<IHardware> list)
        {
            InitializeComponent();

            SelectedIndex = -1;
            cmbDevices.ValueMember = "Name";
            cmbDevices.DisplayMember = "Name";
            cmbDevices.DataSource = list;
            cmbSerialPortType.DataSource = Enum.GetValues(typeof(SerialPortType));
            AdditionalHardware = new SortedDictionary<string, Tuple<int, string>>();

            cmbDevices_SelectedIndexChanged(null, null);
            this.KeyDown += new KeyEventHandler(PopupForm_KeyDown);
            this.KeyPreview = true;
        }

        private void chkMultiChannelList_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            var item = chkMultiChannelList.Items[e.Index].ToString();

            if (e.NewValue == CheckState.Checked)
            {
                txtTransmitAdress.Visible = grpIntrepidCanMultiChannel_cmbBitRate.Visible = label11.Visible = label4.Visible = txtMultiChannelSelectedName.Visible = label7.Visible = true;
                if (AdditionalHardware != null && AdditionalHardware.TryGetValue(item, out var tuple))
                {
                    if (!string.IsNullOrEmpty(tuple.Item2))
                        txtTransmitAdress.Text = tuple.Item2;
                    else
                        txtTransmitAdress.Text = String.Empty;

                    if (tuple.Item1 > 0)
                        grpIntrepidCanMultiChannel_cmbBitRate.SelectedValue = tuple.Item1;
                    else
                        grpIntrepidCanMultiChannel_cmbBitRate.SelectedIndex = 0;
                }
                else
                {
                    grpIntrepidCanMultiChannel_cmbBitRate.SelectedValue = Settings.Default.IntrepidDevice.BitRate;
                    txtTransmitAdress.Text = Settings.Default.TransmitAdress.Substring(2);
                    AddChannelToDictionary(null, null);
                }
            }
            else
            {
                if (AdditionalHardware.ContainsKey(item))
                    AdditionalHardware.Remove(item);
                txtTransmitAdress.Visible = grpIntrepidCanMultiChannel_cmbBitRate.Visible = label11.Visible = label4.Visible = false;
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// An event handler to the PopupForm's KeyDown event.
        /// </summary>
        /// <param name="sender">A reference to the PopupForm instance.</param>
        /// <param name="e">A reference to the KeyDown event's arguments.</param>
        private void PopupForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Dispose();
            }
            else if (e.KeyCode == Keys.Enter)
            {
                btnConnect_Click(sender, e);
            }
        }

        /// <summary>
        /// An event handler to the btnConnect's Click event.
        /// </summary>
        /// <param name="sender">A reference to the btnConnect instance.</param>
        /// <param name="e">A reference to the Click event's arguments.</param>
        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (cmbDevices.SelectedIndex != -1)
            {
                SelectedIndex = cmbDevices.SelectedIndex;

                if (cmbDevices.SelectedItem is SerialPortHardware serialHardware)
                {
                    serialHardware.SerialPortType = (SerialPortType)cmbSerialPortType.SelectedItem;
                    serialHardware.Port = txtPort.Text;
                    serialHardware.BaudRate = (int)numBaudRate.Value;
                    serialHardware.DataBits = (int)numDataBits.Value;
                    serialHardware.Parity = (Parity)cmbParity.SelectedIndex;
                    serialHardware.StopBits = (StopBits)cmbStopBits.SelectedIndex;
                    serialHardware.ReadTimeout = (int)numReadTimeout.Value;
                    serialHardware.WriteTimeout = (int)numWriteTimeout.Value;
                }
                else if (cmbDevices.SelectedItem is IntrepidCsCan intrepidCsCan)
                {
                    intrepidCsCan.BitRate = Convert.ToUInt32(string.IsNullOrWhiteSpace(grpIntrepidCanProperties_cmbBitRate.Text) ? "0" : grpIntrepidCanProperties_cmbBitRate.Text);
                    intrepidCsCan.NetworkID = string.IsNullOrWhiteSpace(grpIntrepidCanProperties_cmbNetworkId.Text) ? (uint)CSnet.eNETWORK_ID.NETID_DEVICE : Convert.ToByte(Enum.Parse(typeof(CSnet.eNETWORK_ID), grpIntrepidCanProperties_cmbNetworkId.Text));
                }
                else if (cmbDevices.SelectedItem is KvaserCan kvaserCan)
                {
                    kvaserCan.BitRate = Convert.ToUInt32(grpKvaserCanProperties_cmbBitRate.SelectedItem);
                }
                DialogResult = DialogResult.OK;
            }
        }

        /// <summary>
        /// An event handler to the cmbDevices's SelectedIndexChanged event.
        /// </summary>
        /// <param name="sender">A reference to the cmbDevices instance.</param>
        /// <param name="e">A reference to the SelectedIndexChanged event's arguments.</param>
        private void cmbDevices_SelectedIndexChanged(object sender, EventArgs e)
        {
            var hardware = cmbDevices.SelectedItem as IHardware;
            txtName.Text = hardware.Name;
            txtDescription.Text = hardware.HardwareDetails;

            grpIntrepidCanProperties.Visible = grpKvaserCanProperties.Visible = grpVectorCanProperties.Visible = grpSerialProperties.Visible = false;
            if (hardware is IntrepidCsCan intepidHardware)
            {
                grpIntrepidCanProperties.Visible = true;
                grpIntrepidCanProperties_cmbBitRate.SelectedItem = intepidHardware.BitRate.ToString();
                grpIntrepidCanProperties_cmbNetworkId.SelectedItem = ((CSnet.eNETWORK_ID)intepidHardware.NetworkID).ToString();
            }
            else if(hardware is VectorCan vectorHardware) {
                grpVectorCanProperties.Visible = true;
            }
            else if(hardware is KvaserCan kvaserHardware) {
                grpKvaserCanProperties.Visible = true;
                grpKvaserCanProperties_cmbBitRate.SelectedItem = kvaserHardware.BitRate.ToString();
            }
            else if (hardware is SerialPortHardware serialHardware)
            {
                grpSerialProperties.Visible = true;
                cmbSerialPortType.SelectedItem = serialHardware.SerialPortType;
                txtPort.Text = serialHardware.Port;
                numBaudRate.Value = serialHardware.BaudRate;
                numDataBits.Value = serialHardware.DataBits;
                cmbParity.SelectedIndex = (int)serialHardware.Parity;
                cmbStopBits.SelectedIndex = (int)serialHardware.StopBits;
                numReadTimeout.Value = serialHardware.ReadTimeout;
                numWriteTimeout.Value = serialHardware.WriteTimeout;
            }
            this.Height = 150 + pnlProperties.Height;
        }

        #endregion

        private bool isProgrammaticCheck = false;
        private void chkIntrepidMultiChanel_CheckedChanged(object sender, EventArgs e)
        {
            if (isProgrammaticCheck)
                return;

            if (grpIntrepidCanProperties_cmbNetworkId.SelectedItem == null)
            {
                isProgrammaticCheck = true;
                Helper.ShowWarningMessageBox("Please select a network first.");
                chkIntrepidMultiChanel.Checked = false;
                isProgrammaticCheck = false;
                return;
            }

            if (chkIntrepidMultiChanel.Checked)
            {
                object[] itemsArray = new object[grpIntrepidCanProperties_cmbNetworkId.Items.Count];
                grpIntrepidCanProperties_cmbNetworkId.Items.CopyTo(itemsArray, 0);
                chkMultiChannelList.Items.AddRange(itemsArray);
                chkMultiChannelList.Items.Remove(grpIntrepidCanProperties_cmbNetworkId.SelectedItem);
            }

            grpIntrepidCanMultiChannel.Visible = chkIntrepidMultiChanel.Checked;
        }

        private void chkMultiChannelList_MouseClick(object sender, MouseEventArgs e)
        {
            CheckedListBox listBox = sender as CheckedListBox;

            if (listBox != null && listBox.SelectedIndex != ListBox.NoMatches)
            {
                if (listBox.GetItemChecked(listBox.SelectedIndex))
                    txtTransmitAdress.Visible = grpIntrepidCanMultiChannel_cmbBitRate.Visible = label11.Visible = label4.Visible = true;
                else
                    txtTransmitAdress.Visible = grpIntrepidCanMultiChannel_cmbBitRate.Visible = label11.Visible = label4.Visible = false;

                txtMultiChannelSelectedName.Visible = label7.Visible = true;
                SelectedItem = (string)listBox.SelectedItem;
                txtMultiChannelSelectedName.Text = SelectedItem;
            }
        }

        private void AddChannelToDictionary(object sender, EventArgs e)
        {
            if (int.TryParse(grpIntrepidCanMultiChannel_cmbBitRate.SelectedItem?.ToString(), out int bitRate) && !string.IsNullOrEmpty(txtTransmitAdress.Text))
                AdditionalHardware[SelectedItem] = new Tuple<int, string>(bitRate, txtTransmitAdress.Text);
        }


        private void txtTransmitAdress_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Uri.IsHexDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
