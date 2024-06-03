using AutosarBCM.Core;
using AutosarBCM.UserControls.Monitor;
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
using WeifenLuo.WinFormsUI.Docking;

namespace AutosarBCM.Forms.Monitor
{
    public partial class FormDTCPanel : DockContent, IDTCReceiver
    {
        private List<UCDTCCard> ucItems = new List<UCDTCCard>();

        public FormDTCPanel()
        {
            InitializeComponent();
        }

        public void LoadConfiguration()
        {
            pnlMonitor.Controls.Clear();
            ucItems = new List<UCDTCCard>();

            foreach (var pInfo in ASContext.Configuration.Controls.SelectMany(p => p.Responses.Where(r => r.ServiceID == 0x62).FirstOrDefault()?.Payloads))
            {
                var ucItem = new UCDTCCard(pInfo);
                ucItems.Add(ucItem);
                pnlMonitor.Controls.Add(ucItem);
            }
        }

        public bool Receive(Service baseService)
        {
            var service = (ReadDTCInformationService)baseService;

            foreach (var dtcValue in service.Values)
                foreach (var ucItem in ucItems)
                    if (dtcValue.Code == ucItem.PayloadInfo.DTCCode)
                    {
                        ucItem.ChangeStatus(dtcValue);
                        break;
                    }
            return true;
        }
    }
}
