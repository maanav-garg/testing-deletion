using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DiagBox.UserControls.Monitor
{
    public partial class OutputItem : UserControl
    {
        public MonitorItem MonitorItem;
        public string GroupName { get; set; }

        public OutputItem(MonitorItem monitorItem)
        {
            InitializeComponent();

            MonitorItem = monitorItem;
            lblName.Text = monitorItem.Name;
        }

        private void OutputItem_DoubleClick(object sender, EventArgs e)
        {
            ConnectionUtil.TransmitData(0, MonitorItem.Data);
            if (numTimeout.Value > 0)
            {
                Thread.Sleep((int)numTimeout.Value);
                ConnectionUtil.TransmitData(0, new byte[0]);
            }
        }

        internal void UpdateState(byte[] receivedData)
        {

        }

        private void lblName_Click(object sender, EventArgs e)
        {
            this.InvokeOnClick(this, new EventArgs());
        }
    }
}
