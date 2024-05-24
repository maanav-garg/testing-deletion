using AutosarBCM.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutosarBCM.Core
{
    public class Service
    {
        protected ServiceInfo ServiceInfo;

        public Service(ServiceInfo serviceInfo)
        {
            ServiceInfo = serviceInfo;
        }

        public static void Transmit(ServiceInfo serviceInfo, ushort controlAddress)
        {
            ASContext.Configuration.Controls.Where(x => x.Address == controlAddress).FirstOrDefault()?.Transmit(serviceInfo);
        }
    }

    public class ReadDataByIdenService : Service
    {
        public ReadDataByIdenService() : base(ServiceInfo.ReadDataByIdentifier) { }

        public void Transmit(ControlInfo controlInfo)
        {
            new ASRequest(ServiceInfo, controlInfo, $"{ServiceInfo.RequestID.ToString("X")}-{BitConverter.ToString(BitConverter.GetBytes(controlInfo.Address).Reverse().ToArray())}")
                .Execute();
        }
    }

    public class IOCtrlByIdenService : Service
    {
        public IOCtrlByIdenService() : base(ServiceInfo.IOCtrlByIdentifier) { }

        public void Transmit(ControlInfo controlInfo)
        {
            throw new NotImplementedException();
        }
    }

    public class DiagnosticSessionControl : Service
    {
        public DiagnosticSessionControl() : base(ServiceInfo.DiagnosticSessionControl) { }

        public void Transmit(SessionInfo sessionInfo)
        {
            ConnectionUtil.TransmitData(new byte[] { ServiceInfo.RequestID, sessionInfo.ID });
        }
    }

    public class TesterPresent : Service
    {
        public TesterPresent() : base(ServiceInfo.TesterPresent) { }

        public void Transmit()
        {
            if (ServiceInfo == null) return;
            ConnectionUtil.TransmitData(new byte[] { ServiceInfo.RequestID, 0 });
        }
    }
}
