using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AutosarBCM.Config;
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
        public Core.ControlInfo emcTimeControl { get; set; }
        /// <summary>
        /// Keeps the relation with dtc and it's parent control
        /// </summary>
        private Dictionary<string, Core.ControlInfo> dtcList = new Dictionary<string, Core.ControlInfo>();
        /// <summary>
        /// Keeps the values of DID payloads. It will be used to determine the changed data
        /// </summary>
        private Dictionary<string, string> payloadValueList = new Dictionary<string, string>();
        /// <summary>
        /// Keeps the values of DTC values. It will be used to determine the changed data
        /// </summary>
        private Dictionary<string, Dictionary<string, byte>> dtcValueList = new Dictionary<string, Dictionary<string, byte>>();

        List<DataGridViewRow> excelData = new List<DataGridViewRow>();

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
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Initializes the data structures
        /// </summary>
        private void InitializeLists()
        {
            foreach (var control in ASContext.Configuration.Controls)
            {
                foreach (var payload in control.Responses?[0].Payloads)
                {
                    payloadValueList[payload.Name] = string.Empty;
                    if (string.IsNullOrEmpty(payload.DTCCode))
                        continue;
                    dtcList[payload.DTCCode] = control;
                    dtcValueList[payload.DTCCode] = new Dictionary<string, byte>();
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
            foreach (DataGridViewRow row in dgvData.Rows.OfType<DataGridViewRow>())
                row.Visible = string.IsNullOrEmpty(txtFilter.Text) || row.Cells.OfType<DataGridViewCell>().Any(x => x.Value?.ToString().ToLower().Contains(txtFilter.Text.ToLower()) ?? false);
        }

        /// <summary>
        /// Handles the Click event of the btnStart control and starts or stops the monitoring process.
        /// </summary>
        /// <param name="sender">A reference to the btnStart instance.</param>
        /// <param name="e">A reference to the event's arguments.</param>
        private void btnStart_Click(object sender, EventArgs e)
        {
            if (!ConnectionUtil.CheckConnection())
                return;

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
                                ThreadSleep(int.Parse(txInterval));
                                control.Transmit(ServiceInfo.ReadDataByIdentifier);
                            }
                            ThreadSleep(int.Parse(txInterval));
                            new ReadDTCInformationService().Transmit();
                            Thread.Sleep(int.Parse(dtcWaitingTime));
                        }
                    }
                    else
                    {
                        FormMain.EMCMonitoring = false;
                        timer?.Stop();
                    }
                    btnStart.Text = start ? "Stop" : "Start";
                    btnStart.ForeColor = start ? Color.Red : DefaultForeColor;
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
        private bool AddDataRow(Core.ControlInfo control, Core.PayloadInfo payload, string controlValue, string dtcValue)
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
                        AddDataRow(readService.ControlInfo, payload.PayloadInfo, payload.FormattedValue, "");
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
                        //Add default value
                        if (!dtcValueList[dtcValue.Code].ContainsKey(dtcValue.Description))
                            dtcValueList[dtcValue.Code].Add(dtcValue.Description, 0);

                        if (dtcValueList[dtcValue.Code][dtcValue.Description] != dtcValue.Mask)
                        {
                            AddDataRow(control, payload, "", dtcValue.Description);
                            dtcValueList[dtcValue.Code][dtcValue.Description] = dtcValue.Mask;
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
            lblElapsedTime.Text = payload.FormattedValue;
            //if (Config.CommonConfig.EMCLifecycle.Skip(2).Take(2).SequenceEqual(response.RawData.Skip(4).Take(2)))
            //{
            //    lblElapsedTime.Text = TimeSpan.FromMinutes(BitConverter.ToInt16(response.RawData, 8)).ToString(@"hh\:mm\:ss\.fff");
            //    return true;
            //}
            return false;
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
    }
}
