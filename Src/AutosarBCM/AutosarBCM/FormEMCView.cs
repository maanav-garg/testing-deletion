using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AutosarBCM.Core.Config;
using AutosarBCM.UserControls.Monitor;
using AutosarBCM;
using AutosarBCM.Core;
using System.Threading;
using System.Web.UI;
using AutosarBCM.Properties;

namespace AutosarBCM
{
    /// <summary>
    /// Represents a form that displays the EMC data in a grid.
    /// </summary>
    public partial class FormEMCView : Form
    {
        #region Variables

        /// <summary>
        /// A reference to the timer that updates the time elapsed
        /// </summary>
        private System.Windows.Forms.Timer timer;
        public ControlInfo emcTimeControl { get; set; }
        /// <summary>
        /// Keeps the relation with dtc and it's parent control
        /// </summary>
        private Dictionary<string, ControlInfo> dtcList = new Dictionary<string, ControlInfo>();
        /// <summary>
        /// Keeps the values of DID payloads. It will be used to determine the changed data
        /// </summary>
        private Dictionary<string, string> payloadValueList = new Dictionary<string, string>();

        List<DataGridViewRow> excelData = new List<DataGridViewRow>();
        private List<UCEmcReadOnlyItem> ucItems = new List<UCEmcReadOnlyItem>();
        private Dictionary<string, List<UCEmcReadOnlyItem>> groups = new Dictionary<string, List<UCEmcReadOnlyItem>>();

        private int emcDataLimit;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the FormEMCView class.
        /// </summary>
        public FormEMCView()
        {
            InitializeComponent();
            emcDataLimit = int.Parse(Settings.Default.EmcDataLimit);
            if (ASContext.Configuration == null)
            {
                Helper.ShowWarningMessageBox("Please, load the configuration file first.");
                return;
            }
            InitializeLists();
            InitializeCards();
        }



        #endregion

        #region Private Methods

        /// <summary>
        /// Create cards
        /// </summary>
        private void InitializeCards()
        {
            pnlCardLayout.Controls.Clear();
            //TODO XML EMC layouttan çekilecek
            foreach (var group in ASContext.Configuration.Layouts)
            {
                foreach (var item in group.Layouts)
                {
                    var ctrl = ASContext.Configuration.Controls.First(x => x.Name == item.Control);

                    if (ctrl == null)
                        continue;
                    var payload = ctrl.Responses[0].Payloads.First(x => x.Name == item.Name);
                    if (payload == null)
                        continue;

                    var ucItem = new UCEmcReadOnlyItem(ctrl, payload, item, group.BackgroundColor, group.TextColor);
                    ucItems.Add(ucItem);
                    if (!string.IsNullOrEmpty(payload.DTCCode))
                        dtcList[payload.DTCCode] = ctrl;

                    //if (!groups.ContainsKey("Other"))
                    //{
                    //    groups["Other"] = new List<UCEmcReadOnlyItem>();
                    //}
                    if (!string.IsNullOrEmpty(group.Name))
                    {
                        if (!groups.ContainsKey(group.Name))
                        {
                            groups.Add(group.Name, new List<UCEmcReadOnlyItem>());
                        }
                        groups[group.Name].Add(ucItem);
                    }
                    else
                    {
                        groups["Other"].Add(ucItem);
                    }
                }

            }
            foreach (var group in groups)
            {
                var flowPanelGroup = new FlowLayoutPanel { AutoSize = true, Margin = Padding = new Padding(3, 3, 3, 40) };
                var label = new Label { Text = group.Key, AutoSize = true, Font = new Font(FontFamily.GenericSansSerif, 12, FontStyle.Bold) };
                pnlCardLayout.Controls.Add(label);

                flowPanelGroup.Paint += pnlMonitorInput_Paint;

                foreach (var ucItem in group.Value)
                {
                    flowPanelGroup.Controls.Add(ucItem);
                }

                pnlCardLayout.Controls.Add(flowPanelGroup);
            }
        }

        /// <summary>
        /// Changing border color of the flowpanel groups
        /// </summary>
        /// <param name="sender">Flowlayoutpanel to be painted.</param>
        /// <param name="e">PaintEventArgs of the sender.</param>
        private void pnlMonitorInput_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, ((FlowLayoutPanel)sender).ClientRectangle, Color.LightGray, ButtonBorderStyle.Solid);
        }

        /// <summary>
        /// Initializes the data structures
        /// </summary>
        private void InitializeLists()
        {
            foreach (var control in ASContext.Configuration.Controls)
            {
                foreach (var payload in control.Responses?[0].Payloads)
                {
                    foreach (var bitPayload in payload.Bits)
                    {
                        payloadValueList[bitPayload.Name] = string.Empty;
                    }
                    payloadValueList[payload.Name] = string.Empty;
                    if (string.IsNullOrEmpty(payload.DTCCode))
                        continue;
                    dtcList[payload.DTCCode] = control;
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the btnSave control and exports the data to a CSV file.
        /// </summary>
        /// <param name="sender">A reference to the btnSave instance.</param> 
        /// <param name="e">A reference to the event's arguments.</param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            Helper.ExportToCSV(dgvData.Columns, excelData);
        }

        /// <summary>
        /// Handles the TextChanged event of the txtFilter control and filters rows based on the entered text.
        /// </summary>
        /// <param name="sender">A reference to the txtFilter instance.</param>
        /// <param name="e">A reference to the event's arguments.</param>
        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
                FilterUCEMCItems(txtFilter.Text);
            else
            {
                foreach (DataGridViewRow row in dgvData.Rows.OfType<DataGridViewRow>())
                    row.Visible = string.IsNullOrEmpty(txtFilter.Text) || row.Cells.OfType<DataGridViewCell>().Any(x => x.Value?.ToString().ToLower().Contains(txtFilter.Text.ToLower()) ?? false);
            }
            pnlCardLayout.Refresh();
        }
        public void FilterUCEMCItems(string filter)
        {
            pnlCardLayout.SuspendLayout();
            foreach (FlowLayoutPanel flowPanel in pnlCardLayout.Controls.OfType<FlowLayoutPanel>())
            {
                flowPanel.SuspendLayout();
                var labelIndex = pnlCardLayout.Controls.IndexOf(flowPanel) - 1;
                if (labelIndex >= 0 && pnlCardLayout.Controls[labelIndex] is Label flowLabel)
                {
                    bool isLabelMatched = flowLabel.Text.IndexOf(filter, StringComparison.OrdinalIgnoreCase) >= 0;

                    if (isLabelMatched)
                    {
                        flowLabel.Visible = true;
                        foreach (var uc in flowPanel.Controls)
                        {
                            if (uc is UCEmcReadOnlyItem ucItem)
                            {
                                ucItem.Visible = true;
                            }
                        }
                    }
                    else
                    {
                        bool anyUcItemVisible = false;
                        foreach (var uc in flowPanel.Controls)
                        {
                            if (uc is UCEmcReadOnlyItem ucItem)
                            {
                                bool titleMatch = ucItem.PayloadInfo.Name.IndexOf(filter, StringComparison.OrdinalIgnoreCase) >= 0
                            || ucItem.ControlInfo.Name.IndexOf(filter, StringComparison.OrdinalIgnoreCase) >= 0;
                                ucItem.Visible = titleMatch;
                                anyUcItemVisible |= ucItem.Visible;
                            }
                        }
                        flowLabel.Visible = anyUcItemVisible;

                    }
                    flowPanel.ResumeLayout();
                }
                pnlCardLayout.ResumeLayout();
            }
            //pnlCardLayout.SuspendLayout();
            //foreach (FlowLayoutPanel flowPanel in pnlCardLayout.Controls.OfType<FlowLayoutPanel>())
            //{
            //    flowPanel.SuspendLayout();
            //    foreach (var uc in flowPanel.Controls)
            //    {
            //        if (uc is UCEmcReadOnlyItem ucItem)
            //        {
            //            bool titleMatch = ucItem.PayloadInfo.Name.IndexOf(filter, StringComparison.OrdinalIgnoreCase) >= 0
            //                || ucItem.ControlInfo.Name.IndexOf(filter, StringComparison.OrdinalIgnoreCase) >= 0;
            //            ucItem.Visible = titleMatch;

            //        }
            //    }
            //    flowPanel.ResumeLayout();
            //}
            //pnlCardLayout.ResumeLayout();
        }

        /// <summary>
        /// Handles the Click event of the btnStart control and starts or stops the monitoring process.
        /// </summary>
        /// <param name="sender">A reference to the btnStart instance.</param>
        /// <param name="e">A reference to the event's arguments.</param>
        private void btnStart_Click(object sender, EventArgs e)
        {
            FormMain mainForm = Application.OpenForms.OfType<FormMain>().FirstOrDefault();
            Task.Run(async () =>
            {
                Helper.SendDefaultSession();
                mainForm.UpdateSessionLabel();

                await Task.Delay(1000);
            });

            Start(!FormMain.EMCMonitoring);
        }

        /// <summary>
        /// Starts or stops the monitoring process.
        /// </summary>
        /// <param name="start">true if the process is starting; otherwise, false.</param>
        private void Start(bool start)
        {
            FormMain.EMCMonitoring = start;
            btnStart.Text = start ? "Stop" : "Start";
            btnStart.ForeColor = start ? Color.Red : DefaultForeColor;
            //TODO düzeltilecek
            emcTimeControl = ASContext.Configuration.Controls.FirstOrDefault(c => c.Name == "Emc_Swc_LifeCycle");
            Task.Run(() =>
            {
                try
                {
                    if (start)
                    {
                        timer = new System.Windows.Forms.Timer() { Interval = 60000 };
                        timer.Tick += (s, e) => { emcTimeControl.Transmit(ServiceInfo.ReadDataByIdentifier); };
                        timer.Start();

                        var controls = ASContext.Configuration.Controls.Where(c => c.Group == "DID" && c.Services.Contains(0x22));
                        ASContext.Configuration.Settings.TryGetValue("TxInterval", out string txInterval);
                        ASContext.Configuration.Settings.TryGetValue("DTCWaitingTime", out string dtcWaitingTime);

                        while (FormMain.EMCMonitoring)
                        {
                            foreach (var control in controls)
                            {
                                if (!start)
                                    break;
                                ThreadSleep(int.Parse(txInterval));
                                control.Transmit(ServiceInfo.ReadDataByIdentifier);
                            }
                            ThreadSleep(int.Parse(txInterval));
                            if (!start)
                                break;
                            new ReadDTCInformationService().Transmit();
                            Thread.Sleep(int.Parse(dtcWaitingTime));
                            new ClearDTCInformation().Transmit();
                        }
                    }
                    else
                    {
                        FormMain.EMCMonitoring = false;
                        timer?.Stop();
                    }
                    if (btnStart.InvokeRequired)
                    {
                        btnStart.BeginInvoke((MethodInvoker)delegate ()
                        {
                            btnStart.Text = start ? "Stop" : "Start";
                            btnStart.ForeColor = start ? Color.Red : DefaultForeColor;
                        });
                    }
                    else
                    {
                        btnStart.Text = start ? "Stop" : "Start";
                        btnStart.ForeColor = start ? Color.Red : DefaultForeColor;
                    }
                }
                finally
                {
                }
            });
        }

        /// <summary>
        /// Pauses the thread for a specified duration.
        /// </summary>
        /// <param name="threadSleep">The time in milliseconds for the thread to sleep.</param>
        private static void ThreadSleep(int threadSleep)
        {
            if (threadSleep > 0)
                Thread.Sleep(threadSleep);
        }

        /// <summary>
        /// Adds a new row to the DataGridView based on the specified parameters.
        /// </summary>
        /// <param name="control">A reference to the ControlInfo instance.</param>
        /// <param name="payload">A reference to the PayloadInfo instance.</param>
        /// <param name="controlValue">The Control value if applicable.</param>
        /// <param name="dtcValue">The DTC value if applicable.</param>
        /// <returns>true if the row was added successfully; otherwise, false.</returns>
        private bool AddDataRow(ControlInfo control, PayloadInfo payload, string controlValue, string dtcValue)
        {
            Invoke(new Action(() =>
            {
                dgvData.FirstDisplayedScrollingRowIndex = dgvData.Rows.Add(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), control?.Name, payload?.Name, controlValue, dtcValue);
                excelData.Add(dgvData.Rows[dgvData.Rows.GetLastRow(DataGridViewElementStates.None)]);
                if (dgvData.Rows.Count >= emcDataLimit)
                    dgvData.Rows.Clear();
            }));
            return true;
        }

        private void AddDataCardView(string name, string value, string dTCValue)
        {
            ucItems.Where(a => a.PayloadInfo.Name == name).FirstOrDefault()?.ChangeStatus(value, dTCValue);
        }

        /// <summary>
        /// Handles the FormClosing event of the FormEMCView control.
        /// </summary>
        /// <param name="sender">A reference to the FormEMCView instance.</param>
        /// <param name="e">A reference to the event's arguments.</param>
        private void FormEMCView_FormClosing(object sender, FormClosingEventArgs e)
        {
            Start(false);
        }

        /// <summary>
        /// Handles the response received for DID and adds a new row to the DataGridView based on the response.
        /// </summary>
        /// <param name="service">A reference to the response as a byte array.</param>
        private void HandleDidReadResponse(Service service)
        {
            if (service is ReadDataByIdenService readService && readService.Response.IsPositiveRx)
            {
                if (readService.ControlInfo.Address == emcTimeControl?.Address)
                {
                    IsInformativeRX(readService.Payloads.FirstOrDefault());
                }

                foreach (var payload in readService.Payloads)
                {
                    //Check for changed data
                    if (payloadValueList[payload.PayloadInfo.Name] != payload.FormattedValue)
                    {
                        tabControl1.Invoke(new Action(() =>
                        {
                            AddDataCardView(payload.PayloadInfo.Name, payload.FormattedValue, null);
                            AddDataRow(readService.ControlInfo, payload.PayloadInfo, payload.FormattedValue, "");
                        }));

                        payloadValueList[payload.PayloadInfo.Name] = payload.FormattedValue;
                    }
                }
            }
        }

        /// <summary>
        /// Handles the response received for DTC and adds a new row to the DataGridView based on the response.
        /// </summary>
        /// <param name="service">A reference to the response as a byte array.</param>
        private void HandleDtcResponse(Service service)
        {
            if (service is ReadDTCInformationService dtcService && dtcService.Response.IsPositiveRx)
            {
                foreach (var dtcValue in dtcService.Values)
                {
                    if (!dtcList.ContainsKey(dtcValue.Code))
                        continue;
                    var control = dtcList[dtcValue.Code];
                    var payload = control.Responses?[0].Payloads.First(p => p.DTCCode == dtcValue.Code);
                    if (payload == null)
                        continue;
                    //Check for changed data
                    try
                    {
                        if (dtcValue.Mask == 0x0B)
                        {
                            AddDataCardView(payload.Name, "", dtcValue.Description);
                            AddDataRow(control, payload, "", dtcValue.Description);
                        }
                    }
                    catch (Exception ex)
                    {

                    }

                }
            }
        }

        /// <summary>
        /// Determines whether the specified response is an informative RX message
        /// </summary>
        /// <param name="payload">A reference to the payload to be checked</param>
        /// <returns>True if the response is an informative RX message; otherwise, false</returns>
        private bool IsInformativeRX(Payload payload)
        {
            //TODO to be checked
            Invoke(new Action(() =>
            {
                var hour = payload.Value[payload.Value.Length - 2];
                var minute = payload.Value[payload.Value.Length - 1];

                int hourInt = (int)hour;
                int minuteInt = (int)minute;

                string timeFormatted = $"{hourInt:D2}:{minuteInt:D2}";

                lblElapsedTime.Text = timeFormatted;
            }));
            return false;
        }

        /// <summary>
        /// Enables the EMC function.
        /// </summary>
        private void activeFunctionEnable_Click(object sender, EventArgs e)
        {
            if (!SendEmcControl("EMC_FunctionEnable", true))
                return;
            inactiveFunctionEnable.Checked = !(activeFunctionEnable.Checked = true);
            functionEnableDropDownButton.Image = Resources.pass;
        }

        /// <summary>
        /// Disables the EMC function.
        /// </summary>
        private void inactiveFunctionEnable_Click(object sender, EventArgs e)
        {
            if (!SendEmcControl("EMC_FunctionEnable", false))
                return;
            inactiveFunctionEnable.Checked = !(activeFunctionEnable.Checked = false);
            functionEnableDropDownButton.Image = Resources.reset;
        }

        /// <summary>
        /// Enables the PEPS function control.
        /// </summary>
        private void activePepsFunction_Click(object sender, EventArgs e)
        {
            if (!SendEmcControl("EMC_PEPSFunctionControl", true))
                return;
            inactivePepsFunction.Checked = !(activePepsFunction.Checked = true);
            pepsFunctionControlDropDownButton.Image = Resources.pass;
        }

        /// <summary>
        /// Disables the PEPS function control.
        /// </summary>
        private void inactivePepsFunction_Click(object sender, EventArgs e)
        {
            if (!SendEmcControl("EMC_PEPSFunctionControl", false))
                return;
            inactivePepsFunction.Checked = !(activePepsFunction.Checked = false);
            pepsFunctionControlDropDownButton.Image = Resources.reset;
        }

        /// <summary>
        /// Enables the low battery voltage protection.
        /// </summary>
        private void activeLowBatteryVoltage_Click(object sender, EventArgs e)
        {
            if (!SendEmcControl("EMC_LowBatteryVoltageProtectionEnable", true))
                return;
            inactiveLowBatteryVoltage.Checked = !(activeLowBatteryVoltage.Checked = true);
            lowBatteryProtectionDropDownButton.Image = Resources.pass;
        }

        /// <summary>
        /// Disables the low battery voltage protection.
        /// </summary>
        private void inactiveLowBatteryVoltage_Click(object sender, EventArgs e)
        {
            if (!SendEmcControl("EMC_LowBatteryVoltageProtectionEnable", false))
                return;

            inactiveLowBatteryVoltage.Checked = !(activeLowBatteryVoltage.Checked = false);
            lowBatteryProtectionDropDownButton.Image = Resources.reset;
        }

        /// <summary>
        /// Sends control data to the EMC.
        /// </summary>
        /// <param name="controlName">The name of the control to be transmitted.</param>
        /// <param name="isActive">A boolean value indicating whether the control should be active or not.</param>
        private bool SendEmcControl(string controlName, bool isActive)
        {
            if (!ConnectionUtil.CheckConnection() || !ConnectionUtil.CheckSession())
                return false;

            var emcItem = ASContext.Configuration.Controls.FirstOrDefault(c => c.Name == controlName);
            if (emcItem == null)
                return false;
            else
            {
                emcItem.Transmit(ServiceInfo.WriteDataByIdentifier, new byte[] { isActive ? (byte)1 : (byte)0 });
                return true;
            }
        }


        #endregion

        #region Public Methods

        /// <summary>
        /// Handles the response received and adds a new row to the DataGridView based on the response.
        /// </summary>
        /// <param name="service">A reference to the response as a byte array.</param>
        internal void HandleResponse(Service service)
        {
            if (!(service is ReadDataByIdenService || service is ReadDTCInformationService))
                return;


            HandleDidReadResponse(service);
            HandleDtcResponse(service);


        }

        #endregion

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            tabControl1.SelectedIndex = e.TabPageIndex;
        }

        private void FormEMCView_Load(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 0;
        }
    }
}
