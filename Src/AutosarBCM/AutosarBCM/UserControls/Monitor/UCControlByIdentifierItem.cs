using AutosarBCM.Core.Enums;
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
    public partial class UCControlByIdentifierItem : UserControl
    {
        #region Variables
        private UCItem ucItem;

        #endregion

        #region Constructor
        public UCControlByIdentifierItem()
        {
            InitializeComponent();
            cmbInputControlParameter.DataSource = Enum.GetValues(typeof(InputControlParameter));
        }

        #endregion

        #region Public Methods
        public void UpdateSidebar(UCItem ucItem)
        {
            pnlControls.Controls.Clear();

            this.ucItem = ucItem;
            lblName.Text = $"{ucItem.ControlInfo.Group}-{ucItem.ControlInfo.Name}";
            lblAddress.Text = "Address: " + BitConverter.ToString(BitConverter.GetBytes(ucItem.ControlInfo.Address).Reverse().ToArray());

            //IOControlByIdentifier Service
            if (!ucItem.ControlInfo.Services.Contains((byte)ServiceName.InputOutputControlByIdentifier))
                return;

            foreach (var payload in ucItem.ControlInfo.Responses[0].Payloads)
            {
                var ucPayload = new UCControlPayload(payload);
                ucPayload.BorderStyle = BorderStyle.FixedSingle;
                ucPayload.Anchor = AnchorStyles.Top| AnchorStyles.Left;
                pnlControls.Controls.Add(ucPayload);
            }
        }

        #endregion

        #region Private Methods
        private void btnSend_Click(object sender, EventArgs e)
        {
            if (!ConnectionUtil.CheckConnection())
                return;

            ucItem.ControlInfo.Transmit(ServiceName.InputOutputControlByIdentifier, PrepareControlData());
        }

        private byte[] PrepareControlData()
        {
            byte controlByte = 0x0;
            var bitIndex = 0;

            var bytes = new List<byte>();
            bytes.Add((byte)InputControlParameter.ShortTermAdjustment);
            foreach (var uc in pnlControls.Controls)
            {
                if (uc is UCControlPayload ucPayload)
                {
                    if (ucPayload.IsSelected)
                    {
                        bytes.Add(ucPayload.SelectedValue);
                        controlByte |= (byte)(1 << (7 - bitIndex));
                    }
                    else
                        bytes.Add(0x0);
                    bitIndex++;
                }
            }
            bytes.Add(controlByte);
            return bytes.ToArray();
        }

        #endregion




    }
}
