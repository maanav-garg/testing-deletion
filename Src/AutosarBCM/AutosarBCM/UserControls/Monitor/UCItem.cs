using AutosarBCM.Config;
using AutosarBCM.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace AutosarBCM.UserControls.Monitor
{
    /// <summary>
    /// Represents a user control for displaying and interacting with input items.
    /// </summary>
    public partial class UCItem : UserControl
    {
        #region Variables

        public ASResponse Response { get; }

        /// <summary>
        /// Gets or sets the associated InputMonitorItem for this control.
        /// </summary>
        public InputMonitorItem Item;

        /// <summary>
        /// Gets or sets the group name associated with the control.
        /// </summary>
        public string MessageID { get; set; }

        public Core.ControlInfo ControlInfo { get; set; }

        /// <summary>
        /// Gets or sets the group name associated with the control.
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// Gets or sets a boolean indicating whether the input is being logged.
        /// </summary>
        public bool IsLogged = false;

        /// <summary>
        /// Gets or sets a number of transmitted message and received message.
        /// </summary>
        public float MessageTransmitted = 0;
        public float MessageReceived = 0;

        /// <summary>
        /// Gets or sets the previous (old) value of the input item.
        /// </summary>
        private ASResponse oldValue;

        private InputMonitorItem item;
        private CommonConfig commonConfig;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the UCItem class.
        /// </summary>
        /// <param name="item">The InputMonitorItem associated with this control.</param>
        /// <param name="commonConfig">The CommonConfig object used for configuration (optional).</param>
        //public UCItem(InputMonitorItem item, CommonConfig commonConfig = null)
        public UCItem(Core.ControlInfo controlInfo)
        {
            InitializeComponent();

            ControlInfo = controlInfo;
            if (controlInfo.Name.Length > 23)
                lblName.Text = $"{controlInfo.Name.Substring(0, 20)}...";
            else
                lblName.Text = controlInfo.Name;

            lbResponse.Items.AddRange(controlInfo.GetPayloads(ServiceInfo.ReadDataByIdentifier, null).ToArray());
        }

        public UCItem(InputMonitorItem item, CommonConfig commonConfig)
        {
            //this.item = item;
            //this.commonConfig = commonConfig;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Change status of the input window regarding to read data from the device.
        /// </summary>
        /// <param name="response">Data comes from device</param>
        public void ChangeStatus(ASResponse response)
        {
            // Check if the message is transmitted successfully (0x22 value received)
            bool isTransmitted = response.ServiceID.Equals((byte)SIDDescription.SID_READ_DATA_BY_IDENTIFIER);

            
            if (isTransmitted)
            {
                // Increment messageTransmitted value
                MessageTransmitted++;

                // Update the UI with the new messageTransmitted value
                lblTransmitted.BeginInvoke((MethodInvoker)delegate ()
                {
                    lblTransmitted.Text = MessageTransmitted.ToString();
                });
            }
            else
            {
                lblReceived.BeginInvoke((MethodInvoker)delegate ()
                {
                    MessageReceived++;
                    lblReceived.Text = MessageReceived.ToString();
                });

                if (oldValue != null)
                {
                    bool areEqual = response.Payloads.Count == oldValue.Payloads.Count;

                    if (areEqual)
                    {
                        for (int i = 0; i < response.Payloads.Count; i++)
                        {
                            if (response.Payloads[i].FormattedValue != oldValue.Payloads[i].FormattedValue ||
                                response.Payloads[i].PayloadInfo.Name != oldValue.Payloads[i].PayloadInfo.Name)
                            {
                                areEqual = false;
                                break;
                            }
                        }
                    }

                    if (areEqual)
                        return;
                }

                oldValue = response;

                lblStatus.BeginInvoke((MethodInvoker)delegate ()
                {
                    lbResponse.Items.Clear();
                    lbResponse.Items.AddRange(response.Payloads.ToArray());
                });
            }
             
            
        }

        /// <summary>
        /// Retrieves the items from listBox1 as an enumerable collection of strings.
        /// </summary>
        /// <returns>An IEnumerable of strings containing the items in listBox1.</returns>
        public IEnumerable<string> GetListBoxItems()
        {
            foreach (var item in lbResponse.Items)
            {
                if (item is PayloadInfo payloadInfo)
                {
                    yield return $"{payloadInfo.Name} {payloadInfo.TypeName}";
                }
                else if (item is Payload payload)
                {
                    yield return $"{payload.PayloadInfo.Name} {payload.PayloadInfo.TypeName} {payload.FormattedValue}";
                }
            }
        }

        #endregion

        #region Private Methods

        //private void UpdateCounters(MessageDirection messageDirection)
        //{
        //    if (messageDirection == MessageDirection.TX)
        //        MessagesTransmitted++;
        //    else
        //        MessagesReceived++;

        //    Invoke(new Action(() =>
        //    {
        //        lblTransmitted.Text = MessagesTransmitted.ToString();
        //        lblReceived.Text = MessagesReceived.ToString();

        //        var diff = MessagesReceived / MessagesTransmitted;
        //        lblDiff.Text = diff.ToString("P2");
        //        lblDiff.BackColor = diff == 1 ? Color.Green : (diff > 0.9 ? Color.Orange : Color.Red);
        //    }));
        //}

        /// <summary>
        /// Label click event
        /// </summary>
        /// <param name="sender">label</param>
        /// <param name="e">Event args</param>
        private void lblName_Click(object sender, EventArgs e)
        {
            this.InvokeOnClick(this, new EventArgs());
        }

        internal void ChangeStatus(InputMonitorItem item, GenericResponse response, MessageDirection messageDirection)
        {
            throw new NotImplementedException();
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            if (!ConnectionUtil.CheckConnection())
                return;

            ControlInfo.Transmit(ServiceInfo.ReadDataByIdentifier);
        }

        private void lbResponse_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            e.DrawFocusRectangle();

            if (e.Index < 0) return;

            var item = lbResponse.Items[e.Index] as Payload;
            e.Graphics.DrawString($"{item.PayloadInfo.NamePadded,-30} {item.FormattedValue}", e.Font, new SolidBrush(Color.FromName(item.Color ?? DefaultForeColor.Name)), e.Bounds);
        }

        private void btnUCClear_Click(object sender, EventArgs e)
        {
            lbResponse.Items.Clear();
            lbResponse.Items.AddRange(ControlInfo.GetPayloads(ServiceInfo.ReadDataByIdentifier, null).ToArray());

        }
    }

    #endregion
}