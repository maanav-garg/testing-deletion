using AutosarBCM.Core;
using AutosarBCM.UserControls.Monitor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
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
            pnlMonitor.SuspendLayout();
            pnlMonitor.Controls.Clear();
            ucItems = new List<UCDTCCard>();

            foreach (var cInfo in ASContext.Configuration.Controls.Where(c=> c.Group == "DID"))
                foreach (var pInfo in cInfo.Responses.Where(r => r.ServiceID == 0x62).FirstOrDefault()?.Payloads)
                {
                    var ucItem = new UCDTCCard(cInfo, pInfo);
                    ucItems.Add(ucItem);
                    pnlMonitor.Controls.Add(ucItem);
                }

            pnlMonitor.ResumeLayout();
        }

        public bool Receive(Service baseService)
        {
            if(baseService is ReadDTCInformationService service)
            {
                if (service != null)
                {
                    foreach (var dtcValue in service.Values)
                        foreach (var ucItem in ucItems)
                            if (dtcValue.Code == ucItem.PayloadInfo.DTCCode)
                            {
                                ucItem.ChangeStatus(dtcValue);
                            }
                    return true;
                }
                return false;
            }
            else if(baseService is ClearDTCInformation clearDTCService)
            {
                if (clearDTCService != null)
                {
                    foreach(var ucItem in ucItems)
                    {
                        ucItem.ClearDTCData();
                    }
                    return true;
                }
            }
            return false;
        }

        public bool Sent(short address)
        {
            return false;
            //throw new NotImplementedException();
        }

        internal void FilterItems(string text)
        {
            pnlMonitor.SuspendLayout();
            foreach (var item in pnlMonitor.Controls.OfType<UCDTCCard>())
                item.Visible = $"{item.ControlInfo.Name} {item.PayloadInfo.Name} {item.GetDTCValues()}".IndexOf(text, StringComparison.OrdinalIgnoreCase) >= 0;
            pnlMonitor.ResumeLayout();
        }

        internal void Session_Changed()
        {
            pnlMonitor.Enabled = ServiceInfo.ReadDTCInformation.Sessions.Contains(ASContext.CurrentSession.ID);
        }

        private void btnReadDTC_Click(object sender, EventArgs e)
        {
            if (!ConnectionUtil.CheckConnection())
                return;

            LoadConfiguration();
            new ReadDTCInformationService().Transmit();
        }
        
        private void btnClearDTC_Click(object sender, EventArgs e)
        {
            if (!ConnectionUtil.CheckConnection())
                return;

            new ClearDTCInformation().Transmit();
        }

        public bool Sent(short address)
        {
            return true;
            //throw new NotImplementedException();
        }
    }
}
