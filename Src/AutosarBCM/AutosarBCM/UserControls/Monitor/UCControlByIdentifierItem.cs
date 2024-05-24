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
            this.ucItem = ucItem;
            lblName.Text = $"{ucItem.ControlInfo.Group}-{ucItem.ControlInfo.Name}";
            lblAddress.Text = "Address: " + BitConverter.ToString(BitConverter.GetBytes(ucItem.ControlInfo.Address).Reverse().ToArray());
        }

        #endregion

        #region Private Methods
        private void btnSend_Click(object sender, EventArgs e)
        {
            //TODO to be uncommented
            //if (!ConnectionUtil.CheckConnection())
            //    return;

            ucItem.ControlInfo.Transmit(ServiceName.InputOutputControlByIdentifier, new byte[] { (byte)InputControlParameter.ShortTermAdjustment, 0x2, 0x3 });
        }

        #endregion




    }
}
