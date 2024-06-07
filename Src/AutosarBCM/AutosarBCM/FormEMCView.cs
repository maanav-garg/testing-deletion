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

namespace AutosarBCM
{
    /// <summary>
    /// Represents a form that displays the EMC data in a grid.
    /// </summary>
    public partial class FormEMCView : Form
    {
        #region Variables

        /// <summary>
        /// A reference to the MonitorConfiguration instance representing the config file.
        /// </summary>
        private AutosarBcmConfiguration Config;

        /// <summary>
        /// A list of all controls from the config file
        /// </summary>
        private List<ControlInfo> ControlList;

        /// <summary>
        /// A reference to the timer that updates the time elapsed
        /// </summary>
        //private Timer timer;
        public Core.ControlInfo ControlInfo { get; set; }

        private ASContext ASContext;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the FormEMCView class.
        /// </summary>
        public FormEMCView()
        {
            InitializeComponent();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Handles the Click event of the btnSave control and exports the data to a CSV file.
        /// </summary>
        /// <param name="sender">A reference to the btnSave instance.</param> 
        /// <param name="e">A reference to the event's arguments.</param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            Helper.ExportToCSV(dgvData);
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
            Task.Run(() =>
            {
                try
                {

                    if (start)
                    {

                        //if (!ConnectionUtil.CheckConnection())
                        //   return;

                        //if (Config == null)
                        //{ Helper.ShowWarningMessageBox("Please, load the configuration file first."); return; }

                        //timer = new Timer() { Interval = 60000 };
                        //timer.Tick += (s, e) => { new UdsMessage() { Id = Config.CommonConfig.MessageID, Data = Config.CommonConfig.EMCLifecycle }.Transmit(); };
                        //timer.Start();

                        


                        var controls = ASContext.Configuration.Controls.Where(c => c.Group == "DID" && c.Services.Contains(0x22));
                        ASContext.Configuration.Settings.TryGetValue("TxInterval", out string txInterval);

                        foreach (var control in controls)
                        {
                            ThreadSleep(int.Parse(txInterval));
                            control.Transmit(ServiceInfo.ReadDataByIdentifier);
                        }

                        

                    }
                    else
                        FormMain.EMCMonitoring = start;
                    btnStart.Text = start ? "Stop" : "Start";
                    btnStart.ForeColor = start ? Color.Red : DefaultForeColor;
                    //timer?.Stop();

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
        /// <param name="item">A reference to the ControlInfo instance.</param>
        /// <param name="response">A reference to the GenericResponse instance.</param>
        /// <param name="diagValue">The diagnostic value if applicable.</param>
        /// <param name="adcValue">The ADC value if applicable.</param>
        /// <returns>true if the row was added successfully; otherwise, false.</returns>
        private bool AddDataRow(ControlInfo item, GenericResponse response, string diagValue, string adcValue, string currentValue)
        {
            Invoke(new Action(() =>
            {
                dgvData.FirstDisplayedScrollingRowIndex = dgvData.Rows.Add(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), BitConverter.ToString(response.RawData), item?.Name, item?.Output.ItemType, diagValue, adcValue, currentValue);
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
        /// Handles the Click event of the btnImport control and imports the data from a CSV file.
        /// </summary>
        /// <param name="sender">A reference to the btnImport instance.</param>
        /// <param name="e">A reference to the event's arguments.</param>
        private void btnImport_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog() { Filter = "Xml|*.xml" })
                if (dialog.ShowDialog() == DialogResult.OK)
                    Config = MonitorConfigManager.GetConfig(dialog.FileName).Configuration;
        }

        #endregion

        #region Internal Methods

        /// <summary>
        /// Handles the response received and adds a new row to the DataGridView based on the response.
        /// </summary>
        /// <param name="responseArray">A reference to the response as a byte array.</param>
        /// <returns>true if the row was added successfully; otherwise, false.</returns>
        internal void HandleResponse(ReadDataByIdenService response)
        {
            Invoke(new Action(() =>
            {
                foreach (var payload in response.Payloads)
                {
                    dgvData.Rows.Add("", "", response.ControlInfo.Name, payload.PayloadInfo.Name, payload.FormattedValue, "");
                }

            }));
        }

        /// <summary>
        /// Determines whether the specified response is an informative RX message
        /// </summary>
        /// <param name="response">A reference to the response to be checked</param>
        /// <returns>True if the response is an informative RX message; otherwise, false</returns>
        private bool IsInformativeRX(GenericResponse response)
        {
            if (Config.CommonConfig.EMCLifecycle.Skip(2).Take(2).SequenceEqual(response.RawData.Skip(4).Take(2)))
            {
                lblElapsedTime.Text = TimeSpan.FromMinutes(BitConverter.ToInt16(response.RawData, 8)).ToString(@"hh\:mm\:ss\.fff");
                return true;
            }
            return false;
        }

        #endregion
    }
}
