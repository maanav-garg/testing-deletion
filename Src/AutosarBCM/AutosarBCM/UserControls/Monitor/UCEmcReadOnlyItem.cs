using AutosarBCM.Core.Config;
using AutosarBCM.Config;
using AutosarBCM.Core;
using AutosarBCM.Forms.Monitor;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Windows.Forms;

namespace AutosarBCM.UserControls.Monitor
{
    /// <summary>
    /// Represents a user control for displaying read-only output information.
    /// </summary>
    public partial class UCEmcReadOnlyItem : UserControl
    {
        #region Variables
        /// <summary>
        /// Represents a control item associated with this control.
        /// </summary>
        public ControlInfo ControlInfo { get; set; }
        public PayloadInfo PayloadInfo { get; set; }
        /// <summary>
        /// Gets or sets the group name of the control.
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// Gets or sets a dictionary that maps register values to their corresponding bytes.
        /// </summary>
        public Dictionary<short, byte> RegisterDict { get; set; } = new Dictionary<short, byte>();

        /// <summary>
        /// Gets the status value from the control's label, or "-" if the label is empty.
        /// </summary>
        public string StatusValue { get { return String.IsNullOrEmpty(lblDtcStatus.Text) ? "-" : lblDtcStatus.Text; } }

        /// <summary>
        /// Gets or sets the message ID associated with this control.
        /// </summary>
        public string MessageID { get; set; }

        /// <summary>
        /// The data group used for sending data.
        /// </summary>
        private short sendDataGroup = 0;

        /// <summary>
        /// Represents the current value as a tuple containing text and color.
        /// </summary>
        private Tuple<string, Color> currentValue;

        public string CurrentDtcDescription { get; set; }


        /// <summary>
        /// Gets or sets the previous (old) value of the input item for IO Control service.
        /// </summary>
        private IOControlByIdentifierService oldValue;
        /// <summary>
        /// Gets or sets the previous (old) value of the input item for Write service.
        /// </summary>
        private WriteDataByIdentifierService oldValueForWriteService;
        private List<Label> _labels;

        #endregion

        #region Constructor

        public UCEmcReadOnlyItem(ControlInfo controlInfo, PayloadInfo payloadInfo, Layout layout, string bgColor, string txColor)
        {
            InitializeComponent();
            InitializeLabels();
            ControlInfo = controlInfo;
            PayloadInfo = payloadInfo;

            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(this.lblParent, controlInfo.Name);

            if (controlInfo.Name.Length > 14)
            {
                lblParent.Text = $"{controlInfo.Name.Substring(0, 12)}...";
            }
            else
            {
                lblParent.Text = controlInfo.Name;
            }
            toolTip.SetToolTip(this.lblName, payloadInfo.Name);

            if (payloadInfo.Name.Length > 30)
                lblName.Text = $"{payloadInfo.Name.Substring(0, 27)}...";
            else
                lblName.Text = payloadInfo.Name;
            if (payloadInfo.Name.Contains("C1.1_") || payloadInfo.Name.Contains("C1.4_") || payloadInfo.Name.Contains("C1.7_") || payloadInfo.Name.Contains("C1.10_"))
                this.BackColor = Color.FromName("LightSteelBlue");
            else
                this.BackColor = Color.FromName(bgColor);
            if (payloadInfo.Name.Contains("C4.18_") || payloadInfo.Name.Contains("C4.34_") || payloadInfo.Name.Contains("C5.41_") || payloadInfo.Name.Contains("C4.17_"))
                SetLabelsColor(Color.FromName("Green"));
            else
                SetLabelsColor(Color.FromName(txColor));
            lblFunctionFeature.Text = layout.FunctionFeature;
            lblLoadFeature.Text = layout.LoadFeature;

        }

        #endregion

        #region Public Methods

        public void SetDefaultValue()
        {
            this.Invoke(new Action(() => { 
            lblWriteStatus.Text = lblDtcStatus.Text = lblLastStatusTime.Text = lblLastDtcTime.Text = "-";
            }));
        }
        public void SetLabelsColor(Color color)
        {
            if (_labels != null)
            {
                foreach (var label in _labels)
                {
                    label.ForeColor = color;
                }
            }
        }

        internal void ChangeStatus(string value, string dTCValue)
        {
            this.Invoke(new Action(() =>
            {
                if (dTCValue == null)
                {
                    lblLastStatusTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    lblWriteStatus.Text = value;
                }
                else
                {
                    lblDtcStatus.Text = dTCValue;
                    lblLastDtcTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                }

            }));



        }

        /// <summary>
        /// Change status of the input window regarding to read data from the device.
        /// </summary>
        /// <param name="monitorItem">Monitor item to be updated</param>
        /// <param name="inputResponse">Data comes from device</param>
        public void ChangeStatusForWriteService(ReadDataByIdenService service)
        {

            if (oldValueForWriteService != null)
            {
                var areEqual = service.Payloads.Count == oldValueForWriteService.Payloads.Count;

                if (areEqual)
                {
                    for (int i = 0; i < service.Payloads.Count; i++)
                    {
                        if (service.Payloads[i].FormattedValue != oldValueForWriteService.Payloads[i].FormattedValue ||
                            service.Payloads[i].PayloadInfo.Name != oldValueForWriteService.Payloads[i].PayloadInfo.Name)
                        {
                            areEqual = false;
                            break;
                        }
                    }
                }

                if (areEqual)
                    return;
            }

            lblWriteStatus.BeginInvoke((MethodInvoker)delegate ()
            {
                var payload = service.Payloads.FirstOrDefault(x => x.PayloadInfo.Name == PayloadInfo.Name);
                lblWriteStatus.Text = payload?.FormattedValue.ToString();
            });

        }

        private ToolTip toolTipDtc = new ToolTip();

        /// <summary>
        /// Change DTC of the input window regarding to read data from the device.
        /// </summary>
        public void ChangeDtc(string dtc)
        {
            if (lblDtcStatus.InvokeRequired)
            {
                lblDtcStatus.Invoke((MethodInvoker)delegate ()
                {
                    string displayText = dtc.Length > 20 ? dtc.Substring(0, 20) + "..." : dtc;
                    lblDtcStatus.Text = displayText;
                    toolTipDtc.SetToolTip(lblDtcStatus, dtc);
                });
            }
            else
            {
                string displayText = dtc.Length > 20 ? dtc.Substring(0, 20) + "..." : dtc;
                lblDtcStatus.Text = displayText;
                toolTipDtc.SetToolTip(lblDtcStatus, dtc);
            }
            CurrentDtcDescription = dtc;
        }


        #endregion

        #region Private Methods

        /// <summary>
        /// Label click event
        /// </summary>
        /// <param name="sender">label</param>
        /// <param name="e">Event args</param>
        private void lblName_Click(object sender, EventArgs e)
        {
            this.InvokeOnClick(this, new EventArgs());
        }
        private void InitializeLabels()
        {
            _labels = new List<Label>
            {
                lblName,
                lblDtcStatus,
                lblWriteStatus,
                lblFunctionFeature,
                lblLoadFeature,
                lblLastDtcTime,
                lblLastDtcTimeText,
                lblLastStatusTime,
                lblLastStatusTimeText,
                lblParent,
                label3,
                label4
            };
        }

        /// <summary>
        /// Gets the button identifier based on the provided value.
        /// </summary>
        /// <param name="value">The value to determine the button identifier.</param>
        /// <returns>The button identifier string.</returns>
        private string GetButtonIdentifier(byte value)
        {
            if (value == 1) return "Button 1";
            else if (value == 2) return "Button 2";
            else if (value == 4) return "Button 3";
            return string.Empty;
        }
        #endregion
    }
}