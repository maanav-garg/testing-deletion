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
    public partial class UCReadOnlyItem : UserControl
    {
        #region Variables

        /// <summary>
        /// Represents a control item associated with this control.
        /// </summary>
        public ControlInfo ControlInfo { get; set; }

        public PayloadInfo PayloadInfo { get; set; }

        /// <summary>
        /// Represents the current value as a tuple containing text and color.
        /// </summary>
        private Tuple<string, Color> currentValue;

        private float MessagesReceived;
        private float MessagesTransmitted;

        public string CurrentDtcDescription { get; set;}

        /// <summary>
        /// Gets or sets the previous (old) value of the input item for IO Control service.
        /// </summary>
        private IOControlByIdentifierService oldValue;
        /// <summary>
        /// Gets or sets the previous (old) value of the input item for Write service.
        /// </summary>
        private WriteDataByIdentifierService oldValueForWriteService;
        
        #endregion

        #region Constructor

        public UCReadOnlyItem(ControlInfo controlInfo, PayloadInfo payloadInfo)
        {
            InitializeComponent();
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
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Change status of the input window regarding to read data from the device.
        /// </summary>
        /// <param name="monitorItem">Monitor item to be updated</param>
        /// <param name="inputResponse">Data comes from device</param>
        public void ChangeStatus(IOControlByIdentifierService service)
        {
            HandleMapping(service);

            if (Program.FormEnvironmentalTest.chkDisableUi.Checked)
                return;

            if (lblReceived.InvokeRequired)
            {
                lblReceived.BeginInvoke((MethodInvoker)delegate ()
                {
                    MessagesReceived++;
                    lblReceived.Text = MessagesReceived.ToString();
                    Calculate();
                });
            }
            else
            {
                MessagesReceived++;
                lblReceived.Text = MessagesReceived.ToString();
                Calculate();
            }

            if (oldValue != null)
            {
                var areEqual = service.Payloads.Count == oldValue.Payloads.Count;

                if (areEqual)
                {
                    for (int i = 0; i < service.Payloads.Count; i++)
                    {
                        if (service.Payloads[i].FormattedValue != oldValue.Payloads[i].FormattedValue ||
                            service.Payloads[i].PayloadInfo.Name != oldValue.Payloads[i].PayloadInfo.Name)
                        {
                            areEqual = false;
                            break;
                        }
                    }
                }

                if (areEqual)
                    return;
            }

            oldValue = service;

            lblWriteStatus.BeginInvoke((MethodInvoker)delegate ()
            {
                if (service.Payloads[0].PayloadInfo.TypeName == "DID_PWM")
                {
                    var payload = (service.Payloads.FirstOrDefault(x => x.PayloadInfo.Name == PayloadInfo.Name)).FormattedValue;

                    string hexValue = payload.Replace("-", "");
                    string decimalValue = (Convert.ToInt32(hexValue, 16)).ToString();
                    lblWriteStatus.Text = decimalValue;
                }
                else
                {
                    var payload = service.Payloads.FirstOrDefault(x => x.PayloadInfo.Name == PayloadInfo.Name);
                    lblWriteStatus.Text = payload?.FormattedValue.ToString();
                }
            });

            lblWriteStatus.BeginInvoke((MethodInvoker)delegate ()
            {
                var payload = service.Payloads.FirstOrDefault(x => x.PayloadInfo.Name == PayloadInfo.Name);
                lblWriteStatus.Text = payload?.FormattedValue.ToString();
            });  
        }

        /// <summary>
        /// Change status of the input window regarding to read data from the device.
        /// </summary>
        /// <param name="monitorItem">Monitor item to be updated</param>
        /// <param name="inputResponse">Data comes from device</param>
        public void ChangeStatus(WriteDataByIdentifierService service)
        {
            //TODO Fix HandleMapping for WriteDataByIdentifierService
            //HandleMapping(service);

            if (Program.FormEnvironmentalTest.chkDisableUi.Checked)
                return;

            if (lblReceived.InvokeRequired)
            {
                lblReceived.BeginInvoke((MethodInvoker)delegate ()
                {
                    MessagesReceived++;
                    lblReceived.Text = MessagesReceived.ToString();
                    Calculate();
                });
            }
            else
            {
                MessagesReceived++;
                lblReceived.Text = MessagesReceived.ToString();
                Calculate();
            }

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

            oldValueForWriteService = service;

            lblWriteStatus.BeginInvoke((MethodInvoker)delegate ()
            {
                if (service.Payloads[0].PayloadInfo.TypeName == "DID_PWM")
                {
                    var payload = (service.Payloads.FirstOrDefault(x => x.PayloadInfo.Name == PayloadInfo.Name)).FormattedValue;
                    if (payload != "")
                    {
                        string hexValue = payload.Replace("-", "");
                        string decimalValue = (Convert.ToInt32(hexValue, 16)).ToString();
                        lblWriteStatus.Text = decimalValue;
                    }
                }

                else
                {
                    var payload = service.Payloads.FirstOrDefault(x => x.PayloadInfo.Name == PayloadInfo.Name);
                    lblWriteStatus.Text = payload?.FormattedValue.ToString();
                }
            });

        }

        private void HandleMapping(IOControlByIdentifierService service)
        {
            var mappingResponse = MappingResponse.OutputError;
            var payload = service.Payloads.FirstOrDefault(x => x.PayloadInfo.Name == PayloadInfo.Name);

            if (payload.PayloadInfo.TypeName == "DID_PWM")
            {
                int value = Convert.ToInt32(payload.FormattedValue.Replace("-", ""), 16);

                if (value == ASContext.Configuration.EnvironmentalTest.Environments.First(x => x.Name == EnvironmentalTest.CurrentEnvironment).EnvironmentalConfig.PWMDutyOpenValue)
                    mappingResponse = MappingResponse.OutputOpen;
                else if (value == ASContext.Configuration.EnvironmentalTest.Environments.First(x => x.Name == EnvironmentalTest.CurrentEnvironment).EnvironmentalConfig.PWMDutyCloseValue)
                    mappingResponse = MappingResponse.OutputClose;
            }
            else
            {
                var value = ASContext.Configuration.GetPayloadInfoByType(payload.PayloadInfo.TypeName).GetPayloadValue(payload.Value);
                if (value != null)
                {
                    if (value.IsOpen) mappingResponse = MappingResponse.OutputOpen;
                    else if (value.IsClose) mappingResponse = MappingResponse.OutputClose;
                }
            }

            if (Program.MappingStateDict.TryGetValue(payload.PayloadInfo.Name, out var errorLogDetect))
                Program.MappingStateDict.UpdateValue(ControlInfo.Name, errorLogDetect.UpdateOutputResponse(errorLogDetect.Operation, MappingState.OutputReceived, mappingResponse));
        }

        /// <summary>
        /// Handle number of transmitted data.
        /// </summary>
        /// <param name="monitorItem">Monitor item to be updated</param>
        /// <param name="inputResponse">Data comes from device</param>
        internal void HandleMetrics()
        {
            MessagesTransmitted++;
            if (lblTransmitted.InvokeRequired)
            {
                lblTransmitted.Invoke((MethodInvoker)delegate ()
                {
                    lblTransmitted.Text = MessagesTransmitted.ToString();
                
                });
            }
            else
            {
                lblTransmitted.Text = MessagesTransmitted.ToString();
            }
            Calculate();
        }

        public void Calculate()
        {
            if (MessagesTransmitted == 0 || MessagesReceived == 0)
            {
                lblDiff.Text = "-";
            }
            else
            {
                if (lblDiff.InvokeRequired)
                {
                    lblDiff.BeginInvoke((MethodInvoker)delegate ()
                    {
                        var x = ControlInfo;
                        double diff = (double)MessagesReceived / MessagesTransmitted;
                        lblDiff.Text = (diff * 100).ToString("F2") + "%";
                        lblDiff.BackColor = diff == 1 ? Color.Green : (diff > 0.9 ? Color.Orange : Color.Red);
                    });
                }
                else
                {
                    var x = ControlInfo;
                    double diff = (double)MessagesReceived / MessagesTransmitted;
                    lblDiff.Text = (diff * 100).ToString("F2") + "%";
                    lblDiff.BackColor = diff == 1 ? Color.Green : (diff > 0.9 ? Color.Orange : Color.Red);
                }
            }
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

        #endregion
    }
}