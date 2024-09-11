using AutosarBCM.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

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
        private bool hasDIDBitsOnOff = false;
        private bool isDoorLock = false;

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
            EnvironmentalTest.CurrentEnvironment = ASContext.Configuration?.EnvironmentalTest.Environments.First().Name;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Handles the click event for the btnStart to check the output controls
        /// </summary>
        /// <param name="sender">A reference to the btnStart instance.</param>
        /// <param name="e">A reference to the event's arguments</param>
        private void btnStart_Click(object sender, EventArgs e)
        {
            FormMain mainForm = Application.OpenForms.OfType<FormMain>().FirstOrDefault();


            Task.Run(async () =>
            {
                FormMain.ControlChecker = true;
                Helper.SendExtendedDiagSession();
                mainForm.UpdateSessionLabel();
                await Task.Delay(1000);
            });
            Start();
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
                        hasDIDBitsOnOff = cInf.Responses.SelectMany(r => r.Payloads).Any(p => p.TypeName == "DID_Bits_On_Off");
                        isDoorLock = cInf.Address.Equals(0xC151);
                        if (hasDIDBitsOnOff || isDoorLock)
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

                        if (item.Address == 0xC151) //(hasDIDBitsOnOff || isDoorLock)
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
            }
        }

        public void LogDataToDGVFromBytes(byte[] data, ushort address)
        {
            var control = ASContext.Configuration.Controls.FirstOrDefault(x => x.Address == address);
            var payloads = control.Responses.SelectMany(r => r.Payloads).Where(p => p.TypeName != "HexDump_1Byte").ToList();
            byte response = data[4];
            byte upperNibble = (byte)((response & 0xF0) >> 4);
            int[] bitArray = new int[4];
            for (int i = 0; i < 4; i++)
            {
                bitArray[i] = (upperNibble >> (3 - i)) & 1;
            }
            foreach (DataGridViewRow row in dgvOutput.Rows)
            {
                if (row.Cells[0] is DataGridViewCheckBoxCell chkCell)
                {
                    bool isSelected = chkCell.Value is true;
                    var controlInfo = (ControlInfo)row.Tag;
                    if (ciDict.TryGetValue(controlInfo, out (List<string>, bool) dictContent))
                    {
                        for (int i = 0; i < payloads.Count; i++)
                        {
                            var pl = payloads[i];
                            if (controlInfo.Address == control.Address)
                            {
                                bool bitValue = bitArray[i] == 1;
                                if (isSelected)
                                {
                                    if (rdoHorizontal.Checked)
                                    {
                                        row.Cells[dictContent.Item2 ? 4 : 3].Value = bitValue ? "On" : "Off";
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public void LogDataToDGVFromService(Service srvData)
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
                            {

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
                        //dgvInput.Rows[dgvInput.Rows.Add(true, control.Name, response.PayloadName)].Tag = control;
                        var result = ASContext.Configuration.GetPayloadInfoByType(response.PayloadTypeName);

                        foreach (var ctrlRes in result.Values.Where(v => v.IsOpen == true || v.IsClose == true))
                        {
                            dgvInput.Rows[dgvInput.Rows.Add(true, control.Name, response.PayloadName)].Tag = control;
                            break;
                        }
                        if (response.PayloadTypeName == "DID_PWM")
                        {
                            dgvInput.Rows[dgvInput.Rows.Add(true, control.Name, response.PayloadName)].Tag = control;
                        }
                    }

                }
            }
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
        }

        private void FormControlChecker_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormMain.ControlChecker = false;
        }

        #endregion

        #region Internal Methods

        #endregion

        #region Public Methods

        #endregion

    }
}