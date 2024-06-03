using AutosarBCM.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutosarBCM.UserControls.Monitor
{
    public partial class UCDTCCard : UserControl
    {
        public PayloadInfo PayloadInfo { get; }

        public UCDTCCard(PayloadInfo payloadInfo)
        {
            InitializeComponent();

            PayloadInfo = payloadInfo;
            lblName.Text = payloadInfo.Name;
        }

        internal void ChangeStatus(DTCValue dtcValue)
        {
            lbxValues.Invoke((Action)(() =>
            {
                lbxValues.Items.Add($"{dtcValue.Description,-30}{dtcValue.Mask}");
            }));
        }
    }
}
