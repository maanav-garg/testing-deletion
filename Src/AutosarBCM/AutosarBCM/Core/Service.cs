using AutosarBCM.Config;
using AutosarBCM.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutosarBCM.Core
{
    public class Service
    {
        protected ServiceInfo serviceInfo;

        public Service(ServiceName serviceName)
        {
            serviceInfo = ASContext.Configuration.Services.Where(x => x.RequestID == (byte)serviceName).FirstOrDefault();
        }

        public static void Transmit(ServiceName serviceName, ControlName controlName)
        {
            ASContext.Configuration.Controls.Where(x => x.Name == controlName.ToString()).FirstOrDefault().Transmit(serviceName);
        }
    }

    public class ReadDataByIdenService : Service
    {
        public ReadDataByIdenService() : base(ServiceName.ReadDataByIdentifier) { }

        public void Transmit(ControlInfo controlInfo)
        {
            var request = new ASRequest(serviceInfo, controlInfo, $"03-{serviceInfo.RequestID.ToString("X")}-{BitConverter.ToString(BitConverter.GetBytes(controlInfo.Address).Reverse().ToArray())}-00-00-00-00");
            request.Execute();
        }
    }

    public class IOCtrlByIdenService : Service
    {
        public IOCtrlByIdenService() : base(ServiceName.InputOutputControlByIdentifier) { }

        public void Transmit(ControlInfo controlInfo)
        {
            throw new NotImplementedException();
        }
    }

    public class DiagnosticSessionControl : Service
    {
        public DiagnosticSessionControl() : base(ServiceName.DiagnosticSessionControl) { }

        public void Transmit(SessionInfo sessionInfo)
        {
            ConnectionUtil.TransmitData(0x0726, new byte[] { serviceInfo.RequestID, sessionInfo.ID, 0, 0, 0, 0, 0, 0 });
        }
    }
}
