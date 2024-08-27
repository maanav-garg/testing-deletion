﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Connection.Hardware;
using Connection.Hardware.Can;
using Connection.Hardware.SP;
using AutosarBCM.Properties;
using System.Linq;
using System.Collections.Specialized;
using AutosarBCM.Forms;

namespace AutosarBCM
{
    /// <summary>
    /// Form to display and edit Options.
    /// </summary>
    partial class FormOptions : Form
    {
        #region Variables

        /// <summary>
        /// A dictionary that maps CAN channel names to their corresponding integer values.
        /// </summary>
        private Dictionary<string, int> canChannelDict = new Dictionary<string, int>();

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor.
        /// </summary>
        public FormOptions()
        {
            InitializeComponent();
            Icon = Helper.GetIconFromImage(Resources.gear_16xLG);

            canChannelDict.Add(CSnet.eNETWORK_ID.NETID_HSCAN.ToString(), (int)CSnet.eNETWORK_ID.NETID_HSCAN);
            cmbSerialPortType.DataSource = Enum.GetValues(typeof(SerialPortType));
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Initializes the form.
        /// </summary>
        /// <param name="sender">form</param>
        /// <param name="e">arguments</param>
        private void FormOptions_Load(object sender, EventArgs e)
        {
            treeView.ExpandAll();

            // load fields
            textBoxUserName.Text = Settings.Default.UserName;
            txtTransmitAdress.Text = Settings.Default.TransmitAdress.Substring(2);
            txtReceiveAdress.Text = Settings.Default.ReceiveAdress.Substring(2);
            txtBlockSize.Text = Settings.Default.BlockSize.Substring(2);
            txtStMin.Text = Settings.Default.StMin.Substring(2);
            txtPaddingByte.Text = Settings.Default.PaddingByte.Substring(2);
            cmbSerialPortType.SelectedItem = Settings.Default.SerialPortType;
            txtPort.Text = Settings.Default.SerialPort;
            numBaudRate.Value = Settings.Default.SerialBaudRate;
            numDataBits.Value = Settings.Default.SerialDataBits;
            cmbParity.SelectedIndex = Settings.Default.SerialParity;
            cmbStopBits.SelectedIndex = Settings.Default.SerialStopBits;
            numReadTimeout.Value = Settings.Default.SerialReadTimeout;
            numWriteTimeout.Value = Settings.Default.SerialWriteTimeout;
            numFlushToUI.Value = Settings.Default.FlushToUI;
            numFlushToFile.Value = Settings.Default.FlushToFile;
            numRollingAfter.Value = Settings.Default.RollingAfter;
            txtFilePath.Text = Settings.Default.TraceFilePath;
            tbEMCDataLimit.Text = Settings.Default.EmcDataLimit;

            foreach (var item in Settings.Default.FilterData)
            {
                lbFilterPage.Items.Add(item.ToString());
            }


            if (Settings.Default.IntrepidDevice != null)
            {
                tabCanHardware_cmbBitRate.SelectedItem = Settings.Default.IntrepidDevice.BitRate;
                tabCanHardware_cmbNetworkId.SelectedItem = Settings.Default.IntrepidDevice.NetworkID;
            }
            if (Settings.Default.KvaserDevice != null)
            {
                tabCanHardware_cmbKvaserBitRate.SelectedItem = Settings.Default.KvaserDevice.BitRate;
            }
        }

        /// <summary>
        /// Selects required configuation page according to the selected node.
        /// </summary>
        /// <param name="sender">tree view</param>
        /// <param name="e">argument</param>
        private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            // remove all tab pages, only select one of them
            tabControl.TabPages.Clear();

            if (e.Node.Name == "NodeTool" || (e.Node.Parent == null ? false : e.Node.Parent.Name == "NodeTool"))
            {
                tabControl.TabPages.Add(tabPageProp1);
            }
            else if (e.Node.Name == "NodeComm" || (e.Node.Parent == null ? false : e.Node.Parent.Name == "NodeComm"))
            {
                tabControl.TabPages.Add(tabPageProp2);
                tabControl.TabPages.Add(tabSerialPort);
                tabControl.TabPages.Add(tabCanHardware);
                tabControl.TabPages.Add(tabFilterPage);
                tabControl.TabPages.Add(tabEMCDataLimitation);
            }

            SelectTabPage(e.Node.Name);

            SelectTabControl(e.Node.Name);
            treeView.Select();
        }

        /// <summary>
        /// Selects the tab page within the tab control based on the provided node name.
        /// </summary>
        /// <param name="nodeName">The name of the node to determine the tab page to select.</param>
        private void SelectTabPage(string nodeName)
        {
            switch (nodeName)
            {
                case "NodeTool":
                case "NodeToolGeneral":
                    tabControl.SelectTab(tabPageProp1); break;
                case "NodeComm":
                case "NodeCommGeneral":
                    tabControl.SelectTab(tabPageProp2); break;
                case "NodeCommSerial":
                    tabControl.SelectTab(tabSerialPort); break;
                case "nodeCommCanHardware":
                    tabControl.SelectTab(tabCanHardware); break;
                case "nodeFilter":
                    tabControl.SelectTab(tabFilterPage); break;
                case "nodeEMCDataLimitation":
                    tabControl.SelectTab(tabEMCDataLimitation); break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Selects the tab page within the tab control based on the provided node name.
        /// </summary>
        /// <param name="nodeName">The name of the node to determine the tab page to select.</param>
        private void SelectTabControl(string nodeName)
        {
            switch (nodeName)
            {
                case "NodeTool":
                case "NodeToolGeneral":
                    tabControl.SelectTab(tabPageProp1); break;
                case "NodeComm":
                case "NodeCommGeneral":
                    tabControl.SelectTab(tabPageProp2); break;
                case "NodeCommSerial":
                    tabControl.SelectTab(tabSerialPort); break;
                case "nodeCommCanHardware":
                    tabControl.SelectTab(tabCanHardware); break;
                case "nodeFilter":
                    tabControl.SelectTab(tabFilterPage); break;
                case "nodeEMCDataLimitation":
                    tabControl.SelectTab(tabEMCDataLimitation); break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Handles OK event and saves configurations.
        /// </summary>
        /// <param name="sender">button</param>
        /// <param name="e">argument</param>
        private void buttonOk_Click(object sender, EventArgs e)
        {
            Settings.Default.FilterData = new StringCollection();
            foreach (var item in lbFilterPage.Items)
            {
                Settings.Default.FilterData.Add(item.ToString());
            }

            // save fields
            Settings.Default.UserName = textBoxUserName.Text;
            Settings.Default.TransmitAdress = "0x"+txtTransmitAdress.Text;
            Settings.Default.ReceiveAdress = "0x"+ txtReceiveAdress.Text;
            Settings.Default.BlockSize = "0x" + txtBlockSize.Text;
            Settings.Default.StMin = "0x" + txtStMin.Text;
            Settings.Default.PaddingByte = "0x" + txtPaddingByte.Text;
            Settings.Default.SerialPortType = (SerialPortType)cmbSerialPortType.SelectedItem;
            Settings.Default.SerialPort = txtPort.Text;
            Settings.Default.SerialBaudRate = (int)numBaudRate.Value;
            Settings.Default.SerialDataBits = (int)numDataBits.Value;
            Settings.Default.SerialParity = cmbParity.SelectedIndex;
            Settings.Default.SerialStopBits = cmbStopBits.SelectedIndex;
            Settings.Default.SerialReadTimeout = (int)numReadTimeout.Value;
            Settings.Default.SerialWriteTimeout = (int)numWriteTimeout.Value;
            Settings.Default.FlushToUI = (int)numFlushToUI.Value;
            Settings.Default.FlushToFile = (int)numFlushToFile.Value;
            Settings.Default.RollingAfter = (int)numRollingAfter.Value;
            Settings.Default.TraceFilePath = txtFilePath.Text;
            Settings.Default.EmcDataLimit = tbEMCDataLimit.Text;

            var intrepidDevice = new IntrepidCsCan()
            {
                BitRate = Convert.ToUInt32(string.IsNullOrWhiteSpace(tabCanHardware_cmbBitRate.Text) ? "0" : tabCanHardware_cmbBitRate.Text),
                NetworkID = string.IsNullOrWhiteSpace(tabCanHardware_cmbNetworkId.Text) ? (uint)CSnet.eNETWORK_ID.NETID_DEVICE : Convert.ToUInt32(Enum.Parse(typeof(CSnet.eNETWORK_ID), tabCanHardware_cmbNetworkId.Text))
            };

            var kvaserDevice = new KvaserCan()
            {
                BitRate = Convert.ToUInt32(string.IsNullOrWhiteSpace(tabCanHardware_cmbKvaserBitRate.Text) ? "0" : tabCanHardware_cmbKvaserBitRate.Text)
            };

            Settings.Default.IntrepidDevice = intrepidDevice;
            Settings.Default.KvaserDevice = kvaserDevice;

            Settings.Default.Save();
         

        }

        private void txtTransmitAdress_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Uri.IsHexDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void txtReceiveAdress_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Uri.IsHexDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void txtBlockSize_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Uri.IsHexDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void txtStMin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Uri.IsHexDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void txtPaddingByte_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Uri.IsHexDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        #endregion

        /// <summary>
        /// Event handler for the selection change in the "Device" combo box in the "Can Hardware" tab.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        private void tabCanHardware_cmbDevice_SelectedIndexChanged(object sender, EventArgs e)
        {
            tabCanHardware_grpIntrepid.Visible = false;
            tabCanHardware_grpKvaser.Visible = false;

            if (tabCanHardware_cmbDevice.Text == "Intrepid")
            {
                tabCanHardware_grpIntrepid.Visible = true;

                var hardware = Settings.Default.IntrepidDevice as CanHardware;

                if (hardware == null) { hardware = new IntrepidCsCan(); }

                tabCanHardware_cmbBitRate.SelectedItem = hardware.BitRate.ToString();
                tabCanHardware_cmbNetworkId.SelectedItem = ((CSnet.eNETWORK_ID)hardware.NetworkID).ToString();
            }
            else if (tabCanHardware_cmbDevice.Text == "Kvaser")
            {
                tabCanHardware_grpKvaser.Visible = true;

                var hardware = Settings.Default.KvaserDevice as CanHardware;

                tabCanHardware_cmbKvaserBitRate.SelectedItem = hardware.BitRate.ToString();
            }
        }

        /// <summary>
        /// Event handler for the "Browse" button click in the user interface.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (var dialog = new SaveFileDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                    txtFilePath.Text = dialog.FileName;
            }
        }

        /// <summary>
        /// Handles Load event.
        /// </summary>
        /// <param name="sender">button</param>
        /// <param name="e">argument</param>
        private void loadTsmi_Click(object sender, EventArgs e) { }

        /// <summary>
        /// Handles Export event.
        /// </summary>
        /// <param name="sender">button</param>
        /// <param name="e">argument</param>
        private void exportTsmi_Click(object sender, EventArgs e) { }


        /// <summary>
        /// Handles "Clear" button click event.
        /// </summary>
        /// <param name="sender">button</param>
        /// <param name="e">argument</param>
        private void btnClearFilter_Click(object sender, EventArgs e)
        {
            lbFilterPage.Items.Clear();
        }

        /// <summary>
        /// Handles "Delete" button click event.
        /// </summary>
        /// <param name="sender">button</param>
        /// <param name="e">argument</param>
        private void btnDeleteFilter_Click(object sender, EventArgs e)
        {
            lbFilterPage.Items.Remove(lbFilterPage.SelectedItem);
        }

        /// <summary>
        /// Handles "Add" button click event.
        /// </summary>
        /// <param name="sender">button</param>
        /// <param name="e">argument</param>
        private void btnAddFilter_Click(object sender, EventArgs e)
        {
            string input = tbFilter.Text;

            // Check if the input length is 1 to 3 characters and contains only letters or digits
            if (input.Length >= 1 && input.Length <= 3 && input.All(Uri.IsHexDigit))
            {
                this.lbFilterPage.Items.Add(input);
                tbFilter.Text = string.Empty;  // Clear the TextBox after adding the item
            }
            else
            {
                MessageBox.Show("Please enter a string with 1 to 3 alphanumeric characters.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Handles changing text box event.
        /// </summary>
        /// <param name="sender">button</param>
        /// <param name="e">argument</param>
        private void tbFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow control keys (e.g., backspace)
            if (char.IsControl(e.KeyChar))
            {
                return;
            }

            // Allow only letters and digits, and limit the length to 2 characters
            if (!Uri.IsHexDigit(e.KeyChar) || tbFilter.Text.Length >= 2)
            {
                e.Handled = true;
            }
        }

        private void tbEMCDataLimit_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow control characters like backspace
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Reject the input
            }
        }

        private void chkEnableTrace_CheckedChanged(object sender, EventArgs e)
        {
            GlobalSettings.IsTraceEnabled = chkEnableTrace.Checked;
        }
    }
    
}
