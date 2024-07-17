using AutosarBCM.Config;
using AutosarBCM.Core;
using AutosarBCM.Core.Config;
using AutosarBCM.UserControls.Monitor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.UI;
using System.Windows.Forms;
using System.Xml.Schema;

namespace AutosarBCM
{
    /// <summary>
    /// Represents a form that checks the input/output controls.
    /// </summary>
    public partial class FormControlChecker : Form
    {
        #region Variables
        enum ControlOrder
        {
            Horizontal,
            Vertical
        }


        /// <summary>
        /// A reference to the MonitorConfiguration instance representing the config file.
        /// </summary>
        private AutosarBcmConfiguration Config;
        /// <summary>
        /// Represents a CheckBox control for selecting all outputs.
        /// </summary>
        private CheckBox chkSelectAllOutput;
        /// <summary>
        /// Represents a CheckBox control for selecting all inputs.
        /// </summary>
        private CheckBox chkSelectAllInput;
        /// <summary>
        /// Control order of the execution
        /// </summary>
        private ControlOrder controlOrder;
        public ControlInfo ControlInfo { get; set; }

        private Dictionary<ControlInfo, (List<string>, bool)> ciDict;

        private Dictionary<string, (List<string>, bool)> ciDictBits;

        private Dictionary<ControlInfo, List<string>> ciDict2;



        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the FormControlChecker class.
        /// </summary>
        public FormControlChecker()
        {
            InitializeComponent();
            rdoOrder_CheckedChanged(null, null);
            rdoControl_CheckedChanged(null, null);

        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Handles the click event for the btnImport to load a configuration file.
        /// </summary>
        /// <param name="sender">A reference to the btnImport instance.</param>
        /// <param name="e">A reference to the event's arguments</param>
        private void btnImport_Click(object sender, EventArgs e)
        {
            //LoadConfig();
        }

        /// <summary>
        /// Handles the click event for the btnStart to check the output controls
        /// </summary>
        /// <param name="sender">A reference to the btnStart instance.</param>
        /// <param name="e">A reference to the event's arguments</param>
        private void btnStart_Click(object sender, EventArgs e)
        {
            FormMain mainForm = Application.OpenForms.OfType<FormMain>().FirstOrDefault();
            if (mainForm.tsbSession.Text != "Session: Extended Diagnostic Session")
            {
                Helper.ShowWarningMessageBox("Must be in Extended Diagnostic Session.");
                return;
            }


            Task.Run(async () =>
            {
                GetExtendedDiagSessionFromControlChecker();
                await Task.Delay(1000);
            });
            Start();



            //if (!FormMain.ControlChecker)
            //{
            //    if (!ConnectionUtil.CheckConnection())
            //        return;
            //    if (Config == null)
            //    {
            //        Helper.ShowWarningMessageBox("Please, load the configuration file first.");
            //        return;
            //    }
            //    Task.Run(() => Start());
            //}
            //RefreshUI(!FormMain.ControlChecker);
        }
        private void LoadConfig()
        {
            if (ASContext.Configuration == null)
            {
                Helper.ShowWarningMessageBox("No configuration file is imported. Please import the file first!");
                this.Close();
                return;
            }
            LoadData();
        }

        /// <summary>
        /// Starts the process of checking the input/output controls based on the type of test.
        /// </summary>
        private void Start()
        {
            try
            {
                Thread.Sleep(1000);
                if (rdoOutput.Checked) StartOutputControls();
                else if (rdoInput.Checked) StartInputControls();
                Thread.Sleep(1000);
            }
            finally
            {

            }

        }

        /// <summary>
        /// Checks the input controls.
        /// </summary>
        private void StartInputControls()
        {
            ClearResponse(3, dgvInput);
            int txIntervalCC = Convert.ToInt32(numInterval.Value);
            var payloadList = new List<string>();
            ciDict2 = new Dictionary<ControlInfo, List<string>>();
            foreach (DataGridViewRow row in dgvInput.Rows)
            {
                if (row.Cells[0] is DataGridViewCheckBoxCell checkBoxCell)
                {
                    bool isChecked = checkBoxCell.Value is true;
                    if (isChecked)
                    {
                        var cInf = (ControlInfo)row.Tag;
                        if (ciDict2.Keys.Where(x => x.Address == cInf.Address).Count() == 0)
                        {
                            ciDict2.Add(cInf, (new List<string>()));
                        }
                        ciDict2[cInf] = (new List<string> { row.Cells[2].Value.ToString() });
                    }
                }
            }

            Task.Run(async () =>
            {
                for (var i = 0; i < ciDict2.Keys.Count; i++)
                {
                    var item = ciDict2.Keys.ElementAt(i);
                    if (item.Address == 0xDF5E)
                    {
                        continue;
                    }
                    else
                    {
                        item.Transmit(ServiceInfo.ReadDataByIdentifier);
                        await Task.Delay(txIntervalCC);
                    }
                }

            });
        }

        /// <summary>
        /// Checks the output controls and their corresponding input controls.
        /// </summary>
        private void StartOutputControls()
        {
            ClearResponse(3, dgvOutput);
            //ASContext.Configuration.Settings.TryGetValue("TxIntervalForControlChecker", out string txIntervalValue);
            int txIntervalCC = Convert.ToInt32(numInterval.Value);
            var payloadList = new List<string>();
            ciDict = new Dictionary<ControlInfo, (List<string>, bool)>();
            ciDictBits = new Dictionary<string, (List<string>, bool)>();
            foreach (DataGridViewRow row in dgvOutput.Rows)
            {
                if (row.Cells[0] is DataGridViewCheckBoxCell checkBoxCell)
                {
                    bool isChecked = checkBoxCell.Value is true;
                    if (isChecked)
                    {
                        var cInf = (ControlInfo)row.Tag;
                        var hasDIDBitsOnOff = cInf.Responses.SelectMany(r => r.Payloads).Any(p => p.TypeName == "DID_Bits_On_Off");
                        if (hasDIDBitsOnOff)
                        {
                            ciDictBits.Add(row.Cells[2].Value.ToString(), (new List<string> { row.Cells[2].Value.ToString() }, false));
                        }
                        if (ciDict.Keys.Where(x => x.Address == cInf.Address).Count() == 0)
                        {
                            ciDict.Add(cInf, (new List<string>(), false));
                        }
                        ciDict[cInf] = (new List<string> { row.Cells[2].Value.ToString() }, false);
                    }
                }
            }

            if (rdoHorizontal.Checked)
            {
                Task.Run(async () =>
                {
                    for (var i = 0; i < ciDict.Keys.Count; i++)
                    {
                        var item = ciDict.Keys.ElementAt(i);
                        var hasDIDBitsOnOff = item.Responses.SelectMany(r => r.Payloads).Any(p => p.TypeName == "DID_Bits_On_Off");
                        if (item.Address == 0xDF5E)
                        {
                            continue;
                        }

                        if (hasDIDBitsOnOff)
                        {
                            item.SwitchForBits(ciDictBits.Keys.ToList(), true);
                            await Task.Delay(txIntervalCC);
                            Thread.Sleep(100);
                            ciDict[item] = (ciDict[item].Item1, true);
                            item.SwitchForBits(ciDictBits.Keys.ToList(), false);
                            await Task.Delay(txIntervalCC);
                        }
                        else
                        {
                            item.Switch(ciDict[item].Item1, true);
                            await Task.Delay(txIntervalCC);
                            Thread.Sleep(100);
                            ciDict[item] = (ciDict[item].Item1, true);
                            item.Switch(ciDict[item].Item1, false);
                            await Task.Delay(txIntervalCC);
                        }
                    }

                });

                //LogDataToDGV("Horizontal");
            }
            else if (rdoVertical.Checked)
            {
                Task.Run(async () =>
                {
                    foreach (var item in ciDict)
                    {
                        item.Key.Switch(item.Value.Item1, true);
                        await Task.Delay(txIntervalCC);
                    }

                    Thread.Sleep(100);

                    foreach (var item in ciDict)
                    {
                        ciDict[item.Key] = (item.Value.Item1, true);
                        item.Key.Switch(item.Value.Item1, false);
                        await Task.Delay(txIntervalCC);
                    }
                });
                //LogDataToDGV("Vertical");
            }


        }

        public void LogDataToDGV(Service srvData)
        {
            if (rdoOutput.Checked)
            {
                var service = (IOControlByIdentifierService)srvData;
                foreach (DataGridViewRow row in dgvOutput.Rows)
                {
                    if (row.Cells[0] is DataGridViewCheckBoxCell chkCell)
                    {
                        bool isSelected = chkCell.Value is true;
                        var controlInfo = (ControlInfo)row.Tag;

                        if ((ciDict.TryGetValue(controlInfo, out (List<string>, bool) dictContent)))
                        {
                            foreach (var pl in service.Payloads)
                                if (controlInfo.Address == service.ControlInfo.Address)
                                {
                                    if (isSelected != false)
                                    {
                                        var all_values = ASContext.Configuration.GetPayloadInfoByType(pl.PayloadInfo.TypeName).Values;
                                        var matchingValue = all_values.FirstOrDefault(x => x.Value.SequenceEqual(pl.Value));
                                        if (pl.PayloadInfo.TypeName == "DID_PWM")
                                        {
                                            if (rdoHorizontal.Checked)
                                            {
                                                row.Cells[dictContent.Item2 ? 4 : 3].Value = pl.FormattedValue;
                                            }
                                        }
                                        else
                                        {
                                            if (matchingValue != null)
                                            {
                                                byte[] isOpenValue = matchingValue.IsOpen ? matchingValue.Value : null;
                                                byte[] isCloseValue = matchingValue.IsClose ? matchingValue.Value : null;
                                                if (rdoHorizontal.Checked)
                                                {
                                                    if (!dictContent.Item2)
                                                        row.Cells[3].Value = isCloseValue != null ? BitConverter.ToString(isCloseValue) : matchingValue.FormattedValue;
                                                    else
                                                        row.Cells[4].Value = isOpenValue != null ? BitConverter.ToString(isOpenValue) : matchingValue.FormattedValue;
                                                }
                                            }
                                        }

                                    }
                                }
                        }
                    }
                }
            }
            else
            {
                var service = (ReadDataByIdenService)srvData;

                foreach (DataGridViewRow row in dgvInput.Rows)
                {
                    if (row.Cells[0] is DataGridViewCheckBoxCell chkCell)
                    {
                        bool isSelected = chkCell.Value is true;
                        var controlInfo = (ControlInfo)row.Tag;

                        if ((ciDict2.TryGetValue(controlInfo, out List<string> dictContent)))
                        {
                            foreach (var pl in service.Payloads)
                            {
                                if (controlInfo.Address == service.ControlInfo.Address)
                                {
                                    if (!isSelected)
                                        continue;
                                    row.Cells[3].Value = pl.FormattedValue;
                                }
                            }
                        }
                    }
                }
            }

        }



        //public void LogDataToDGV(byte[] data)
        //{
        //    ASContext.Configuration.Settings.TryGetValue("ReadInterval", out string readIntervalValue);
        //    int readIntervalCC = int.TryParse(readIntervalValue, out int interval) ? interval : 0;
        //    if (data[0] != 0x7F)
        //    {
        //        ushort address = (ushort)((data[1] << 8) | data[2]);
        //        byte[] payloadValues = data.Skip(4).ToArray();

        //        foreach (DataGridViewRow row in dgvOutput.Rows)
        //        {
        //            if (row.Cells[0] is DataGridViewCheckBoxCell chkCell)
        //            {
        //                bool isSelected = chkCell.Value is true;
        //                var controlInfo = (ControlInfo)row.Tag;
        //                if (controlInfo.Address == address)
        //                {
        //                    for (int i = 0; i < payloadValues.Length; i++)
        //                    {
        //                        var payloadInfo = controlInfo.Responses.SelectMany(r => r.Payloads)
        //                                                               .ElementAtOrDefault(i);
        //                        byte[] isOpenValue = payloadInfo.Values?.FirstOrDefault(x => x.IsOpen)?.Value;
        //                        byte[] isCloseValue = payloadInfo.Values?.FirstOrDefault(x => x.IsClose)?.Value;
        //                        if (payloadInfo != null && payloadInfo.Name == row.Cells[2].Value.ToString())
        //                        {
        //                            if (isSelected)
        //                            {
        //                                row.Cells[3].Value = BitConverter.ToString(isOpenValue);
        //                                row.Cells[4].Value = BitConverter.ToString(isCloseValue);
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    else
        //    {
        //        return;
        //    }
        //}
        /// <summary>
        /// Updates the DataGridView values based on the response received.
        /// </summary>
        /// <param name="row">The DataGridViewRow to be updated.</param>
        /// <param name="response">A reference to the response as a byte array.</param>
        /// <returns>true if the values were successfully updated; otherwise, false.</returns>
        private bool HandleInputResponse(DataGridViewRow row, GenericResponse response)
        {
            var input = (InputMonitorItem)row.Tag;
            var id = response.RawData.Skip(4).Take(3);

            if (input.Data?.Skip(2).Take(3).SequenceEqual(id) ?? false) UpdateValue(row, 3, response.FormattedResult(input));
            else return false;

            ApplyInputColors(row, input);
            return true;
        }

        /// <summary>
        /// Updates the DataGridView values based on the response received.
        /// </summary>
        /// <param name="row">The DataGridViewRow to be updated.</param>
        /// <param name="response">A reference to the response as a byte array.</param>
        /// <returns>true if the values were successfully updated; otherwise, false.</returns>
        private bool HandleOutputResponse(DataGridViewRow row, GenericResponse response)
        {
            var cInfo = (ControlInfoCC)row.Tag;

            if (!cInfo.Closing && cInfo.IsOpenResponse(response)) UpdateValue(row, 4, response.FormattedResult());
            else if (!cInfo.Closing && cInfo.IsDiagResponse(response)) UpdateValue(row, 5, UCDigitalOutputItem.GetDigitalReadDiagResponseData(response.RegisterGroup, response.ResponseData));
            else if (!cInfo.Closing && cInfo.IsADCResponse(response)) UpdateValue(row, 6, UCDigitalOutputItem.GetDigitalReadADCResponseData(response.RegisterGroup, response.ResponseData));
            else if (!cInfo.Closing && cInfo.IsCurrentResponse(response)) UpdateValue(row, 7, UCDigitalOutputItem.GetDigitalReadCurrentValueResponseData(response.RegisterGroup, response.ResponseData));
            else if (!cInfo.Closing && cInfo.IsInputResponse(response)) UpdateValue(row, 8, response.FormattedResult(cInfo.CorrespondingInput));

            else if (cInfo.Closing && cInfo.IsCloseResponse(response)) UpdateValue(row, 9, response.FormattedResult());
            else if (cInfo.Closing && cInfo.IsDiagResponse(response)) UpdateValue(row, 10, UCDigitalOutputItem.GetDigitalReadDiagResponseData(response.RegisterGroup, response.ResponseData));
            else if (cInfo.Closing && cInfo.IsADCResponse(response)) UpdateValue(row, 11, UCDigitalOutputItem.GetDigitalReadADCResponseData(response.RegisterGroup, response.ResponseData));
            else if (cInfo.Closing && cInfo.IsCurrentResponse(response)) UpdateValue(row, 12, UCDigitalOutputItem.GetDigitalReadCurrentValueResponseData(response.RegisterGroup, response.ResponseData));
            else if (cInfo.Closing && cInfo.IsInputResponse(response)) UpdateValue(row, 13, response.FormattedResult(cInfo.CorrespondingInput));

            else return false;

            ApplyOutputColors(row, cInfo, response);
            return true;
        }

        /// <summary>
        /// Updates the value in a specific cell and changes the color accordingly.
        /// </summary>
        /// <param name="row">The DataGridViewRow containing the cell to be updated.</param>
        /// <param name="index">The index of the cell to be updated.</param>
        /// <param name="value">The new value to be assigned to the cell.</param>
        private void UpdateValue(DataGridViewRow row, int index, string value)
        {
            Invoke(new Action(() => row.Cells[index].Value = value));
        }

        /// <summary>
        /// Applies colors to specific cells based on specific conditions.
        /// </summary>
        /// <param name="row">The DataGridViewRow containing the cell to be formatted.</param>
        /// <param name="input">A reference to the input as an InputMonitorItem.</param>
        private void ApplyInputColors(DataGridViewRow row, InputMonitorItem input)
        {
            if (input.ItemType == "Digital")
                row.Cells[3].Style.ForeColor = row.Cells[3].Value?.ToString() == "ON" ? Color.Green : Color.Red;
        }

        /// <summary>
        /// Handles the DataGridView cell mouse click event to perform specific actions based on the column index.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A DataGridViewCellMouseEventArgs that contains the event data.</param>
        private void SetSelectAllControls(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                DataGridView dgv = sender as DataGridView;
                dgv.Visible = true;
                Rectangle rect = dgv.GetCellDisplayRectangle(e.ColumnIndex, -1, true);

                if (dgv == dgvOutput)
                {
                    if (chkSelectAllOutput == null)
                    {
                        chkSelectAllOutput = new CheckBox();
                        chkSelectAllOutput.Size = new Size(14, 14);
                        chkSelectAllOutput.Location = new Point(rect.Location.X + (rect.Width - chkSelectAllOutput.Width) / 2, rect.Location.Y + (rect.Height - chkSelectAllOutput.Height) / 2);
                        chkSelectAllOutput.Checked = true;
                        dgv.Controls.Add(chkSelectAllOutput);

                        chkSelectAllOutput.Checked = true;

                        chkSelectAllOutput.Click += (s, args) =>
                        {
                            foreach (DataGridViewRow row in dgv.Rows)
                            {
                                row.Cells[e.ColumnIndex].Value = chkSelectAllOutput.Checked;
                                dgv.ClearSelection();
                                dgv.EndEdit();
                            }
                        };
                    }
                    else
                    {
                        chkSelectAllOutput.Location = new Point(rect.Location.X + (rect.Width - chkSelectAllOutput.Width) / 2 - 1, rect.Location.Y + (rect.Height - chkSelectAllOutput.Height) / 2);
                    }
                }
                else
                {
                    if (chkSelectAllInput == null)
                    {
                        chkSelectAllInput = new CheckBox();
                        chkSelectAllInput.Size = new Size(14, 14);
                        chkSelectAllInput.Location = new Point(rect.Location.X + (rect.Width - chkSelectAllInput.Width) / 2, rect.Location.Y + (rect.Height - chkSelectAllInput.Height) / 2);
                        chkSelectAllInput.Checked = true;
                        dgv.Controls.Add(chkSelectAllInput);

                        chkSelectAllInput.Checked = true;

                        chkSelectAllInput.Click += (s, args) =>
                        {
                            foreach (DataGridViewRow row in dgv.Rows)
                            {
                                row.Cells[e.ColumnIndex].Value = chkSelectAllInput.Checked;
                                dgv.ClearSelection();
                                dgv.EndEdit();
                            }
                        };
                    }
                    else
                    {
                        chkSelectAllInput.Location = new Point(rect.Location.X + (rect.Width - chkSelectAllInput.Width) / 2 - 1, rect.Location.Y + (rect.Height - chkSelectAllInput.Height) / 2);
                    }
                }
                dgv.Visible = dgv == dgvOutput ? rdoOutput.Checked : rdoInput.Checked;
            }
        }

        /// <summary>
        /// Applies colors to specific cells based on specific conditions.
        /// </summary>
        /// <param name="row">The DataGridViewRow containing the cell to be formatted.</param>
        /// <param name="cInfo">A reference to the control information as a ControlInfo.</param>
        /// <param name="response">A reference to the response as a GenericResponse.</param>
        private void ApplyOutputColors(DataGridViewRow row, ControlInfoCC cInfo, GenericResponse response)
        {
            var id = response.RawData.Skip(4).Take(3);

            row.Cells[8].Style.ForeColor = row.Cells[4].Value?.ToString() == "E_OK" && row.Cells[8].Value?.ToString() == "ON" ? Color.Green : Color.Red;
            row.Cells[13].Style.ForeColor = row.Cells[9].Value?.ToString() == "E_OK" && row.Cells[13].Value?.ToString() == "OFF" ? Color.Green : Color.Red;

            if (cInfo.IsDiagResponse(response) && !IsDiagResponseOK(response) && !cInfo.Closing) row.Cells[5].Style.BackColor = Color.IndianRed;
            if (cInfo.IsDiagResponse(response) && !IsDiagResponseOK(response) && cInfo.Closing) row.Cells[10].Style.BackColor = Color.IndianRed;
        }

        /// <summary>
        /// Loads the data from the configuration file and populates the DataGridView controls accordingly.
        /// </summary>
        /// <returns>true if the data was successfully loaded; otherwise, false.</returns>
        private void LoadData()
        {
            if (ASContext.Configuration == null)
            {
                Helper.ShowWarningMessageBox("Please, load the configuration file first.");
                return;
            }

            if (ASContext.Configuration.Settings.TryGetValue("TxIntervalForControlChecker", out string txIntervalValue))
            {

                numInterval.Value = Convert.ToDecimal(txIntervalValue);
            }
            else
            {
                MessageBox.Show("TXIntervalForControlChecker key not found in the settings.");
            }

            if (rdoOutput.Checked)
            {
                SetSelectAllControls(dgvOutput, new DataGridViewCellMouseEventArgs(0, 0, 0, 0, new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0)));
                dgvOutput.Rows.Clear();
                foreach (var control in ASContext.Configuration.Controls.Where(c => c.Services.Contains((byte)SIDDescription.SID_INPUT_OUTPUT_CONTROL_BY_IDENTIFIER) && c.Group == "DID"))
                {
                    foreach (var response in control.Responses.Where(r => r.Payloads != null)
                            .SelectMany(r => r.Payloads, (r, p) => new { ControlName = control.Name, r.ServiceID, PayloadName = p.Name, PayloadTypeName = p.TypeName }))
                    {

                        var result = ASContext.Configuration.GetPayloadInfoByType(response.PayloadTypeName);

                        foreach (var ctrlRes in result.Values.Where(v => v.IsOpen == true || v.IsClose == true))
                        {
                            dgvOutput.Rows[dgvOutput.Rows.Add(true, control.Name, response.PayloadName)].Tag = control;
                            break;
                        }
                        if (response.PayloadTypeName == "DID_PWM")
                        {
                            dgvOutput.Rows[dgvOutput.Rows.Add(true, control.Name, response.PayloadName)].Tag = control;
                        }
                    }
                }

            }
            else if (rdoInput.Checked)
            {
                SetSelectAllControls(dgvInput, new DataGridViewCellMouseEventArgs(0, 0, 0, 0, new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0)));
                dgvInput.Rows.Clear();
                foreach (var control in ASContext.Configuration.Controls.Where(c => c.Services.Contains((byte)SIDDescription.SID_READ_DATA_BY_IDENTIFIER) && c.Group == "DID"))
                {
                    foreach (var response in control.Responses.Where(r => r.Payloads != null && r.Payloads.Any())
                    .SelectMany(r => r.Payloads, (r, p) => new { ControlName = control.Name, r.ServiceID, PayloadName = p.Name, PayloadTypeName = p.TypeName }))
                    {
                        dgvInput.Rows[dgvInput.Rows.Add(true, control.Name, response.PayloadName)].Tag = control;
                    }

                }
            }
        }

        /// <summary>
        /// Refreshes the UI based on the specified condition.
        /// </summary>
        /// <param name="start">Indicates whether the process is starting or stopping.</param>
        private void RefreshUI(bool start)
        {
            Invoke(new Action(() =>
            {
                btnStart.Text = start ? "Stop" : "Start";
                FormMain.ControlChecker = start;
                btnStart.ForeColor = start ? Color.Red : DefaultForeColor;
                btnImport.Enabled = btnSave.Enabled = !start;
                grpType.Enabled = !start;
                grpOrder.Enabled = !start;
            }));
        }

        /// <summary>
        /// Clears the response data in the specified DataGridView starting from the given column index.
        /// </summary>
        /// <param name="startIndex">The starting index of the column to clear.</param>
        /// <param name="dgv">The DataGridView from which to clear the response data.</param>
        private void ClearResponse(int startIndex, DataGridView dgv)
        {
            foreach (DataGridViewRow row in dgv.Rows)
            {
                for (int i = startIndex; i < row.Cells.Count; i++)
                {
                    if ((string)row.Cells[i].Value == "N / A")
                        continue;

                    //if (dgv == dgvOutput)
                    //    ((ControlInfo)row.Tag).Closing = false;

                    row.Cells[i].Value = null;
                    row.Cells[i].Style.BackColor = Color.White;
                }
            }
        }

        /// <summary>
        /// Sets the value of a specific cell to "N/A" and changes its background color accordingly.
        /// </summary>
        /// <param name="row">A reference to the DataGridViewRow containing the cell to be updated.</param>
        /// <param name="index">The index of the cell within the row.</param>
        private static void SetUnavailable(DataGridViewRow row, int index)
        {
            row.Cells[index].Value = "N/A";
            row.Cells[index].Style.BackColor = Color.LightGray;
        }

        /// <summary>
        /// Transmits the specified data with a specified message ID.
        /// </summary>
        /// <param name="messageId">The message ID, that represented as a hexadecimal string.</param>
        /// <param name="data">The data to be transmitted.</param>
        /// <returns>true if the data was successfully transmitted; otherwise false.</returns>
        private bool Transmit(string messageId, byte[] data)
        {
            if (data == null || data.Length == 0) return false;

            ConnectionUtil.TransmitData(uint.Parse(messageId, NumberStyles.HexNumber), data);
            Thread.Sleep((int)numInterval.Value);
            return true;
        }

        /// <summary>
        /// Handles the click event of the btnSave and exports the DataGridView data to a CSV file.
        /// </summary>
        /// <param name="sender">A reference to the btnSave instance.</param>
        /// <param name="e">A reference to the event's arguments.</param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            //    Helper.ExportToCSV(rdoInput.Checked ? dgvInput : dgvOutput);
        }

        /// <summary>
        /// Handles the TextChanged event of the txtFilter control and filters rows based on the entered text.
        /// </summary>
        /// <param name="sender">A reference to the txtFilter instance.</param>
        /// <param name="e">A reference to the event's arguments.</param>
        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            DataGridView dgv = rdoOutput.Checked ? dgvOutput : dgvInput;
            foreach (DataGridViewRow row in dgv.Rows.OfType<DataGridViewRow>().Union(dgv.Rows.OfType<DataGridViewRow>()))
                row.Visible = string.IsNullOrEmpty(txtFilter.Text) || row.Cells.OfType<DataGridViewCell>().Any(x => x.Value?.ToString().ToLower().Contains(txtFilter.Text.ToLower()) ?? false);
        }

        /// <summary>
        /// Checks if the response is OK based on the specified conditions.
        /// </summary>
        /// <param name="response">A reference to the response as a GenericResponse.</param>
        /// <returns>true if the response is OK; otherwise, false.</returns>
        private bool IsDiagResponseOK(GenericResponse response)
        {
            return (response.RegisterGroup == 0x6101 && response.ResponseData == 0)
            || (response.RegisterGroup == 0x6108 && response.ResponseData == 0)
            || (response.RegisterGroup == 0x610C && response.ResponseData == 1)
            || (response.RegisterGroup == 0x610D && response.ResponseData == 1)
            || (response.RegisterGroup == 0x610E && response.ResponseData == 1)
            || (response.RegisterGroup == 0x6110 && response.ResponseData == 1)
            || (response.RegisterGroup == 0x6111 && response.ResponseData == 1)
            || (response.RegisterGroup == 0x6113 && response.ResponseData == 0);
        }

        /// <summary>
        /// Handles the CheckedChanged event of the rdoControl control and changes the visibility of the DataGridView accordingly.
        /// </summary>
        /// <param name="sender">A reference to the rdoControl instance.</param>
        /// <param name="e">A reference to the event's arguments.</param>
        private void rdoControl_CheckedChanged(object sender, EventArgs e)
        {
            dgvInput.Visible = rdoInput.Checked;
            dgvOutput.Visible = rdoOutput.Checked;
            grpOrder.Visible = lblOrderNote.Visible = rdoOutput.Checked;
            LoadData();
        }

        /// <summary>
        /// Handles the DataGridView cell mouse click event to toggle the value of a checkbox cell.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A DataGridViewCellMouseEventArgs that contains the event data.</param>
        private void dgv_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            if (e.ColumnIndex == 0 && e.RowIndex >= 0)
            {
                DataGridViewCell clickedCell = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];
                if (clickedCell is DataGridViewCheckBoxCell checkBoxCell)
                {
                    checkBoxCell.Value = !(bool)checkBoxCell.Value;
                    if ((bool)checkBoxCell.Value == false)
                    {
                        if (rdoInput.Checked)
                            chkSelectAllInput.CheckState = CheckState.Unchecked;
                        else
                            chkSelectAllOutput.CheckState = CheckState.Unchecked;
                    }
                    dgv.EndEdit();
                }
            }
        }

        /// <summary>
        /// Handles the CheckedChanged event of the rdoOrder control and changes the executio accordingly.
        /// </summary>
        /// <param name="sender">A reference to the rdoControl instance.</param>
        /// <param name="e">A reference to the event's arguments.</param>
        private void rdoOrder_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewColumn col in dgvOutput.Columns)
                col.Visible = true;

            lblWaitTime.Enabled = numWaitTime.Enabled = rdoVertical.Checked;
            controlOrder = rdoHorizontal.Checked ? ControlOrder.Horizontal : ControlOrder.Vertical;
            if (rdoHorizontal.Checked)
            {
                lblOrderNote.Text = "Note: Output controls will be opened and closed sequantially.";
            }
            else
            {
                lblOrderNote.Text = "Note: Output will be opened at once. Then closed according to wait time.";


            }
        }
        private void FormControlChecker_Load(object sender, EventArgs e)
        {
            FormMain.ControlChecker = true;
            rdoOutput.Checked = true;
            LoadConfig();
            //btnImport.PerformClick();
        }


        #endregion

        #region Internal Methods

        /// <summary>
        /// Handles the response received and updates the DataGridView rows accordingly.
        /// </summary>
        /// <param name="response">A reference to the response as a byte array.</param>
        internal void HandleResponse(byte[] response)
        {
            var genericResponse = new GenericResponse(response,
                   Config.GenericMonitorConfiguration.InputSection.CommonConfig.InputRegisterGroupOffset,
                   Config.GenericMonitorConfiguration.InputSection.CommonConfig.InputRegisterGroupLength);

            //if (rdoInput.Checked)
            //    foreach (DataGridViewRow row in dgvInput.Rows)
            //        if (HandleInputResponse(row, genericResponse))
            //            break;
            if (rdoOutput.Checked)
                foreach (DataGridViewRow row in dgvOutput.Rows)
                    if (HandleOutputResponse(row, genericResponse))
                        break;
        }

        #endregion

        #region Public Methods
        public void GetExtendedDiagSessionFromControlChecker()
        {
            if (!ConnectionUtil.CheckConnection())
                return;
            FormMain.ControlChecker = true;
            var sessionInfo = (SessionInfo)ASContext.Configuration.Sessions.FirstOrDefault(x => x.Name == "Extended Diagnostic Session");
            ASContext.CurrentSession = sessionInfo;
            new DiagnosticSessionControl().Transmit(sessionInfo);

        }
        public void UpdateUI(Action updateAction)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(updateAction);
            }
            else
            {
                updateAction();
            }
        }
        #endregion

        private void FormControlChecker_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormMain.ControlChecker = false;
        }
    }
}