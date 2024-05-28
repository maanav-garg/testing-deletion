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
    public partial class UCControlPayload : UserControl
    {
        #region Variables

        private PayloadInfo payloadInfo;

        public bool IsSelected
        {
            get { return chkSelected.Checked; }
        }
        public byte[] SelectedValue 
        {
            get { return (cmbValue.SelectedItem as PayloadValue).Value; }
        }

        #endregion

        #region Constructor
        public UCControlPayload(PayloadInfo payloadInfo)
        {
            InitializeComponent();
            this.payloadInfo = payloadInfo;
            lblName.Text = payloadInfo.Name;
            FillValues();
        }

        #endregion

        #region Public Methods

        #endregion

        #region Private Methods

        private void FillValues()
        {
            var info = ASContext.Configuration.GetPayloadInfoByType(payloadInfo.TypeName);
            cmbValue.DataSource = info.Values;
            cmbValue.DisplayMember = "FormattedValue";
        }

        #endregion


    }
}
