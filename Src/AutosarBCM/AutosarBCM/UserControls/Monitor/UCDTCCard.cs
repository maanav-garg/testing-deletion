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
        public Core.ControlInfo ControlInfo { get; }
        public PayloadInfo PayloadInfo { get; }

        public UCDTCCard(Core.ControlInfo controlInfo, PayloadInfo payloadInfo)
        {
            InitializeComponent();

            ControlInfo = controlInfo;
            PayloadInfo = payloadInfo;

            lblCtrlName.Text = controlInfo.Name;
            lblSubCtrlName.Text = payloadInfo.Name;
        }

        internal void ChangeStatus(DTCValue dtcValue)
        {
            lbxValues.Invoke((Action)(() =>
            {
                lbxValues.Items.Add($"{dtcValue.Description,-30}{dtcValue.Mask}");
            }));
        }

        public string GetDTCValues()
        {
            return string.Join(" ", lbxValues.Items.Cast<string>());
        }

        internal void ClearDTCData()
        {
            lbxValues.Invoke((Action)(() =>
            {
                lbxValues.Items.Clear();
            }));
        }
    }
}
