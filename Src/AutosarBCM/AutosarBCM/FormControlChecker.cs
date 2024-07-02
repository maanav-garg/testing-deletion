using AutosarBCM.Config;
using AutosarBCM.Core;
using AutosarBCM.Forms.Monitor;
using AutosarBCM.UserControls.Monitor;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
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

            if (!FormMain.ControlChecker)
            {
                if (!ConnectionUtil.CheckConnection())
                    return;
              
                if (Config == null)
                {
                    Helper.ShowWarningMessageBox("Please, load the configuration file first.");
                    return;
                }
              
                Task.Run(() => Start());
            }
            RefreshUI(!FormMain.ControlChecker);
        }
        private void LoadConfig()
        {
            if (ASContext.Configuration == null)
            {
                Helper.ShowWarningMessageBox("No configuration file is imported. Please import the file first!");
                this.Close();
                return;
            }
            LoadData(true);
        }

        /// <summary>
        /// Starts the process of checking the input/output controls based on the type of test.
        /// </summary>
        private void Start()
        {
            try
            {
                Thread.Sleep(1000);
                if (rdoInput.Checked) StartInputControls();
                if (rdoOutput.Checked) StartOutputControls();
            }
            finally
            {
                RefreshUI(false);
            }
        }

        /// <summary>
        /// Checks the input controls.
        /// </summary>
        private void StartInputControls()
        {
            //bool anyDataSelected = false;
            //ClearResponse(3, dgvInput);
            //foreach (DataGridViewRow row in dgvInput.Rows)
            //{
            //    if ((DataGridViewCell)row.Cells[0] is DataGridViewCheckBoxCell toBeTransmittedRow)
            //    {
            //        if ((bool)toBeTransmittedRow.Value == true)
            //        {
            //            anyDataSelected = true;
            //            var input = (InputMonitorItem)row.Tag;
            //            Transmit(input.MessageIdOrDefault, input.Data);

            //            if (!FormMain.ControlChecker) break;
            //        }
            //    }
            //}
            //if (!anyDataSelected)
            //    Helper.ShowWarningMessageBox("No Check Edits");
        }

        /// <summary>
        /// Checks the output controls and their corresponding input controls.
        /// </summary>
        private void StartOutputControls()
        {
            bool isDataSelected = false;
            ClearResponse(4, dgvOutput);
            if (controlOrder == ControlOrder.Horizontal)
            {
                foreach (DataGridViewRow row in dgvOutput.Rows)
                {
                    var openRow = row.Cells[0];

                    if ((DataGridViewCell)row.Cells[0] is DataGridViewCheckBoxCell toBeTransmittedRow)
                    {
                        if (!(bool)toBeTransmittedRow.Value)
                            continue;

                        isDataSelected = true;
                        var cInfo = (ControlInfo)row.Tag;

                        if (!Transmit(cInfo.Output.MessageIdOrDefault, cInfo.Open)) SetUnavailable(row, 4);
                        if (!Transmit(cInfo.Output.MessageIdOrDefault, cInfo.Diag)) SetUnavailable(row, 5);
                        if (!Transmit(cInfo.Output.MessageIdOrDefault, cInfo.ADC)) SetUnavailable(row, 6);
                        if (!Transmit(cInfo.Output.MessageIdOrDefault, cInfo.Current)) SetUnavailable(row, 7);
                        if (!Transmit(cInfo.CorrespondingInput?.MessageIdOrDefault, cInfo.CorrespondingInput?.Data)) SetUnavailable(row, 8);

                        Thread.Sleep(50);
                        cInfo.Closing = true;

                        if (!Transmit(cInfo.Output.MessageIdOrDefault, cInfo.Close)) SetUnavailable(row, 9);
                        if (!Transmit(cInfo.Output.MessageIdOrDefault, cInfo.Diag)) SetUnavailable(row, 10);
                        if (!Transmit(cInfo.Output.MessageIdOrDefault, cInfo.ADC)) SetUnavailable(row, 11);
                        if (!Transmit(cInfo.Output.MessageIdOrDefault, cInfo.Current)) SetUnavailable(row, 12);
                        if (!Transmit(cInfo.CorrespondingInput?.MessageIdOrDefault, cInfo.CorrespondingInput?.Data)) SetUnavailable(row, 13);

                        if (!FormMain.ControlChecker)
                            break;
                    }
                }
                if (!isDataSelected)
                    Helper.ShowWarningMessageBox("No Check Edits");
            }
            else //Vertical
            {
                var selectedControls = new List<(ControlInfo, DataGridViewRow)>();

                foreach (DataGridViewRow row in dgvOutput.Rows)
                {
                    var openRow = row.Cells[0];
                    if (openRow is DataGridViewCheckBoxCell toBeTransmittedRow)
                    {
                        if (!(bool)toBeTransmittedRow.Value)
                            continue;
                        isDataSelected = true;
                        var cInfo = (ControlInfo)row.Tag;

                        if (!Transmit(cInfo.Output.MessageIdOrDefault, cInfo.Open))
                            SetUnavailable(row, 4);
                        if (!Transmit(cInfo.CorrespondingInput?.MessageIdOrDefault, cInfo.CorrespondingInput?.Data))
                            SetUnavailable(row, 8);

                        selectedControls.Add((cInfo, row));

                        if (!FormMain.ControlChecker)
                            break;
                    }
                }

                // Wait for controls
                Thread.Sleep((int)numWaitTime.Value);

                foreach (var control in selectedControls)
                {
                    if (!Transmit(control.Item1.Output.MessageIdOrDefault, control.Item1.Close))
                        SetUnavailable(control.Item2, 9);
                    if (!Transmit(control.Item1.CorrespondingInput?.MessageIdOrDefault, control.Item1.CorrespondingInput?.Data))
                        SetUnavailable(control.Item2, 13);
                }
            }
        }

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
            var cInfo = (ControlInfo)row.Tag;

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
                else
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
                dgv.Visible = dgv == dgvOutput ? rdoOutput.Checked : rdoInput.Checked;
            }
        }

        /// <summary>
        /// Applies colors to specific cells based on specific conditions.
        /// </summary>
        /// <param name="row">The DataGridViewRow containing the cell to be formatted.</param>
        /// <param name="cInfo">A reference to the control information as a ControlInfo.</param>
        /// <param name="response">A reference to the response as a GenericResponse.</param>
        private void ApplyOutputColors(DataGridViewRow row, ControlInfo cInfo, GenericResponse response)
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
        /// <param name="all">Indicates whether to load all the data of input and output controls or just the data for the selected type.</param>
        /// <returns>true if the data was successfully loaded; otherwise, false.</returns>
        private void LoadData(bool all = false)
        {
            if (ASContext.Configuration == null)
            {
                Helper.ShowWarningMessageBox("Please, load the configuration file first.");
                return;
            }

            if (ASContext.Configuration.Settings.TryGetValue("TxInterval", out string txIntervalValue))
            {

                numInterval.Value = Convert.ToDecimal(txIntervalValue);
            }
            else
            {
                MessageBox.Show("TXInterval key not found in the settings.");
            }

            if (all || rdoOutput.Checked)
            {
                SetSelectAllControls(dgvOutput, new DataGridViewCellMouseEventArgs(0, 0, 0, 0, new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0)));
                dgvOutput.Rows.Clear();
                foreach (var control in ASContext.Configuration.Controls.Where(c => c.Services.Contains(0x2F) && c.Group == "DID"))
                {
                    foreach (var response in control.Responses.Where(r => r.Payloads != null && r.Payloads.Any())
                    .SelectMany(r => r.Payloads, (r, p) => new { ControlName = control.Name, r.ServiceID, PayloadName = p.Name, PayloadTypeName = p.TypeName }))
                    {
                        dgvOutput.Rows[dgvOutput.Rows.Add(true, control.Name, response.PayloadName)].Tag = control;
                    }

                }

            }
            //if (all || rdoOutput.Checked)
            //{
            //    SetSelectAllControls(dgvOutput, new DataGridViewCellMouseEventArgs(0, 0, 0, 0, new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0)));
            //    dgvOutput.Rows.Clear();

            //    foreach (var item in ControlHelper.GetControlsExtended(Config))
            //    {
            //        var index = dgvOutput.Rows.Add(true ,item.Name, item.Output.ItemType, item.CorrespondingInput?.Name);
            //        dgvOutput.Rows[index].Tag = item;

            //        if (item.CorrespondingInput == null)
            //        {
            //            dgvOutput.Rows[index].Cells[3].Value = dgvOutput.Rows[index].Cells[8].Value = dgvOutput.Rows[index].Cells[13].Value = "N / A";
            //            dgvOutput.Rows[index].Cells[3].Style.BackColor = dgvOutput.Rows[index].Cells[8].Style.BackColor = dgvOutput.Rows[index].Cells[13].Style.BackColor = Color.LightGray;
            //        }
            //    }
            //}
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

                    if (dgv == dgvOutput)
                        ((ControlInfo)row.Tag).Closing = false;

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
            foreach (DataGridViewRow row in dgvOutput.Rows.OfType<DataGridViewRow>().Union(dgvOutput.Rows.OfType<DataGridViewRow>()))
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
            dgvOutput.Visible = rdoOutput.Checked;
            grpOrder.Visible = lblOrderNote.Visible = rdoOutput.Checked;
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
                dgvOutput.Columns[5].Visible = false;
                dgvOutput.Columns[6].Visible = false;
                dgvOutput.Columns[7].Visible = false;

                dgvOutput.Columns[10].Visible = false;
                dgvOutput.Columns[11].Visible = false;
                dgvOutput.Columns[12].Visible = false;

            }
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

        private void FormControlChecker_Load(object sender, EventArgs e)
        {
            rdoOutput.Checked = true;
            LoadConfig();
            //btnImport.PerformClick();
        }
    }
}