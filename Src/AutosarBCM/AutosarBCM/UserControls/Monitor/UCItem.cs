using AutosarBCM.Config;
using AutosarBCM.Core;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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
        private ReadDataByIdenService oldValue;

        private InputMonitorItem item;
        private CommonConfig commonConfig;
        private const int ResizeHandleSize = 10;
        private Point lastMousePosition;
        private bool isResizing = false;
        private ToolTip lblNameToolTip;
        private string fullLabelText;

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

            lbResponse.Items.AddRange(controlInfo.GetPayloads(ServiceInfo.ReadDataByIdentifier, null).ToArray());

            InitResizeFeat();
        }


        public UCItem(InputMonitorItem item, CommonConfig commonConfig)
        {
            //this.item = item;
            //this.commonConfig = commonConfig;
        }

        private void InitResizeFeat()
        {

            SetStyle(ControlStyles.ResizeRedraw, true);
            Resize += UCItem_Resize;

            lblName.AutoSize = true;
            fullLabelText = ControlInfo.Name;
            lblName.Text = TruncateText(fullLabelText, 20);
            lblName.AutoEllipsis = true;

            lblName.Margin = new Padding(0, 0, btnRead.Width + 10, 0);

            lblName.MouseHover += LblName_MouseHover;
            lblNameToolTip = new ToolTip();
            // Set the minimum size for the control
            int minWidth = CalculateMinWidth();
            int minHeight = 100;
            MinimumSize = new Size(minWidth, minHeight);
        }

        private void UCItem_Resize(object sender, EventArgs e)
        {
            UpdateLayout();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            ControlPaint.DrawSizeGrip(e.Graphics, BackColor, Width - ResizeHandleSize, Height - ResizeHandleSize, ResizeHandleSize, ResizeHandleSize);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Left && IsInResizeHandle(e.Location))
            {
                isResizing = true;
                lastMousePosition = e.Location;
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            isResizing = false;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (isResizing)
            {
                int deltaX = e.X - lastMousePosition.X;
                int deltaY = e.Y - lastMousePosition.Y;

                Width = Math.Max(Width + deltaX, MinimumSize.Width);
                Height = Math.Max(Height + deltaY, MinimumSize.Height);

                lastMousePosition = e.Location;
                UpdateLayout();
            }
        }

        private void UpdateLayout()
        {
            // Calculate available width for lblName
            int availableWidth = Width - btnRead.Width - 30;

            if (availableWidth > 0)
            {
                lblName.Width = availableWidth;
            }

            if (TextRenderer.MeasureText(fullLabelText, lblName.Font).Width <= availableWidth)
            {
                lblName.Text = fullLabelText;
            }
            else
            {
                lblName.Text = TruncateText(fullLabelText, CalculateMaxCharacters(availableWidth));
            }

            lbResponse.Size = new Size(Math.Max(0, Width - 6), Math.Max(0, Height - 40));
            lbResponse.Location = new Point(3, 40);
        }

        private bool IsInResizeHandle(Point point)
        {
            return point.X >= Width - ResizeHandleSize && point.Y >= Height - ResizeHandleSize;
        }

        private void LblName_MouseHover(object sender, EventArgs e)
        {
            lblNameToolTip.SetToolTip(lblName, fullLabelText);
        }

        private string TruncateText(string text, int maxLength)
        {
            if (text.Length <= maxLength) return text;
            return text.Substring(0, maxLength) + "...";
        }

        private int CalculateMaxCharacters(int availableWidth)
        {
            int averageCharWidth = TextRenderer.MeasureText("A", lblName.Font).Width;
            return Math.Max(1, availableWidth / averageCharWidth);
        }

        private int CalculateMinWidth()
        {
            int lblNameMinWidth = TextRenderer.MeasureText(TruncateText(fullLabelText, 20), lblName.Font).Width;
            int btnReadWidth = btnRead.Width;
            int padding = 40;

            return lblNameMinWidth + btnReadWidth + padding;
        }
        #endregion

        #region Public Methods

        /// <summary>
        /// Change status of the input window regarding to read data from the device.
        /// </summary>
        /// <param name="monitorItem">Monitor item to be updated</param>
        /// <param name="inputResponse">Data comes from device</param>
        public void ChangeStatus(ReadDataByIdenService service)
        {
            lblReceived.BeginInvoke((MethodInvoker)delegate ()
            {
                MessageReceived++;
                lblReceived.Text = MessageReceived.ToString();
            });

            if (oldValue != null)
            {
                bool areEqual = service.Payloads.Count == oldValue.Payloads.Count;

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

            lblStatus.BeginInvoke((MethodInvoker)delegate ()
            {
                lbResponse.Items.Clear();
                lbResponse.Items.AddRange(service.Payloads.ToArray());

            });

        }

        /// <summary>
        /// Handle number of transmitted data.
        /// </summary>
        /// <param name="monitorItem">Monitor item to be updated</param>
        /// <param name="inputResponse">Data comes from device</param>
        internal void HandleMetrics()
        {
            MessageTransmitted++;

            lblTransmitted.BeginInvoke((MethodInvoker)delegate ()
            {
                lblTransmitted.Text = MessageTransmitted.ToString();
            });
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
            oldValue = null;
            lblTransmitted.Text = lblReceived.Text = "0";
            MessageTransmitted = MessageReceived = 0;            
        }

        private void UCItem_Load(object sender, EventArgs e)
        {
            ToolTip ToolTip1 = new ToolTip();
            ToolTip1.SetToolTip(this.btnUCClear, "Clear");
        }
    }

    #endregion
}
