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
            serviceInfo = ASContext.Configuration?.Services.Where(x => x.RequestID == (byte)serviceName).FirstOrDefault()
                ?? new ServiceInfo { Name = serviceName.ToString(), RequestID = (byte)serviceName };
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
            var request = new ASRequest(serviceInfo, controlInfo, $"{serviceInfo.RequestID.ToString("X")}-{BitConverter.ToString(BitConverter.GetBytes(controlInfo.Address).Reverse().ToArray())}");
            request.Execute();
        }
    }

    public class IOControlByIdentifierService : Service
    {
        public IOControlByIdentifierService() : base(ServiceName.InputOutputControlByIdentifier) { }

        public void Transmit(ControlInfo controlInfo, byte[] additionalData)
        {
            var address = BitConverter.ToString(BitConverter.GetBytes(controlInfo.Address).Reverse().ToArray());
            var additionalBytes = BitConverter.ToString(additionalData);
            var data = $"{serviceInfo.RequestID.ToString("X")}-{address}-{additionalBytes}";

            var request = new ASRequest(serviceInfo, controlInfo, data);
            request.Execute();
        }
    }

    public class DiagnosticSessionControl : Service
    {
        public DiagnosticSessionControl() : base(ServiceName.DiagnosticSessionControl) { }

        public void Transmit(SessionInfo sessionInfo)
        {
            ConnectionUtil.TransmitData(new byte[] { serviceInfo.RequestID, sessionInfo.ID});
        }
    }

    public class TesterPresent : Service
    {
        public TesterPresent() : base(ServiceName.TesterPresent) { }

        public void Transmit()
        {
            ConnectionUtil.TransmitData(new byte[] { serviceInfo.RequestID, 0});
        }
    }
    public class ECUReset : Service
    {
        public ECUReset() : base(ServiceName.ECUReset) { }

        public void Transmit()
        {
            ConnectionUtil.TransmitData(new byte[] { serviceInfo.RequestID, 0x1 });
        }
    }
}
