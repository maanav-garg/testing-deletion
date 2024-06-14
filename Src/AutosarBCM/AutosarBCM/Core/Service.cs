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
        public ASResponse Response { get; set; }

        public ServiceInfo ServiceInfo { get; set; }

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
        public ControlInfo ControlInfo { get; private set; }
        public List<Payload> Payloads { get; private set; }

        public ReadDataByIdenService() : base(ServiceInfo.ReadDataByIdentifier) { }

        public void Transmit(ControlInfo controlInfo)
        {
            new ASRequest(ServiceInfo, controlInfo, $"{ServiceInfo.RequestID.ToString("X")}-{BitConverter.ToString(BitConverter.GetBytes(controlInfo.Address).Reverse().ToArray())}")
                .Execute();
        }

        internal static Service Receive(ASResponse response)
        {
            var service = new ReadDataByIdenService();

            service.ControlInfo = ASContext.Configuration.GetControlByAddress(BitConverter.ToUInt16(response.Data.Skip(1).Take(2).Reverse().ToArray(), 0));
            service.Payloads = service.ControlInfo.GetPayloads(service.ServiceInfo, response.Data);
            service.Response = response;
            return service;
        }
    }

    public class IOControlByIdentifierService : Service
    {
        public ControlInfo ControlInfo { get; private set; }
        public List<Payload> Payloads { get; private set; }

        public IOControlByIdentifierService() : base(ServiceInfo.InputOutputControlByIdentifier) { }

        public void Transmit(ControlInfo controlInfo, byte[] additionalData)
        {
            var address = BitConverter.ToString(BitConverter.GetBytes(controlInfo.Address).Reverse().ToArray());
            var additionalBytes = BitConverter.ToString(additionalData);
            var data = $"{ServiceInfo.RequestID.ToString("X")}-{address}-{additionalBytes}";

            var request = new ASRequest(ServiceInfo, controlInfo, data);
            request.Execute();
        }

        internal static Service Receive(ASResponse response)
        {
            var service = new IOControlByIdentifierService();

            service.ControlInfo = ASContext.Configuration.GetControlByAddress(BitConverter.ToUInt16(response.Data.Skip(1).Take(2).Reverse().ToArray(), 0));

            service.Payloads = service.ControlInfo.GetPayloads(service.ServiceInfo, response.Data);
            service.Response = response;
            return service;
        }
    }

    public class DiagnosticSessionControl : Service
    {
        public DiagnosticSessionControl() : base(ServiceInfo.DiagnosticSessionControl) { }

        public void Transmit(SessionInfo sessionInfo)
        {
            ConnectionUtil.TransmitData(new byte[] { ServiceInfo.RequestID, sessionInfo.ID });
        }

        internal static DiagnosticSessionControl Receive(ASResponse response)
        {
            return new DiagnosticSessionControl()
            {
                Response = response
            };
        }
    }

    public class NegativeResponse : Service
    {
        public NegativeResponse() : base(ServiceInfo.NegativeResponse) { }

        internal static NegativeResponse Receive(ASResponse response)
        {
            return new NegativeResponse()
            {
                Response = response
            };
        }
    }

    public class TesterPresent : Service
    {
        public TesterPresent() : base(ServiceInfo.TesterPresent) { }

        public void Transmit()
        {
            if (ServiceInfo == null) return;
            ConnectionUtil.TransmitData(new byte[] { ServiceInfo.RequestID, 0x80 });
        }

        internal static TesterPresent Receive(ASResponse response)
        {
            return new TesterPresent()
            {
                Response = response
            };
        }
    }

    public class ECUReset : Service
    {
        public ECUReset() : base(ServiceInfo.ECUReset) { }

        public void Transmit()
        {
            if (ServiceInfo == null) return;
            ConnectionUtil.TransmitData(new byte[] { ServiceInfo.RequestID, 0x1 });
        }
    }

    public class ReadDTCInformationService : Service
    {
        public List<DTCValue> Values { get; set; }

        public ReadDTCInformationService() : base(ServiceInfo.ReadDTCInformation) { }

        public void Transmit()
        {
            new ASRequest(ServiceInfo, new byte[] { ServiceInfo.RequestID, 0x02, 0x40 }).Execute();
        }

        public static ReadDTCInformationService Receive(ASResponse response)
        {
            var result = new List<DTCValue>();
            var data = response.Data.Skip(3).ToArray();

            for (var i = 0; i < data.Length; i += 4)
                result.Add(new DTCValue
                {
                    Code = BitConverter.ToString(data.Skip(i).Take(2).ToArray()).Replace("-", ""),
                    FailureType = data[i + 2],
                    Mask = data[i + 3]
                });
            return new ReadDTCInformationService 
            { 
                Values = result,
                Response = response
            };
        }
    }
   
    public class ClearDTCInformation : Service
    {
        public ControlInfo ControlInfo { get; private set; }
        public List<Payload> Payloads { get; private set; }
        
        
        public ClearDTCInformation() : base(ServiceInfo.ClearDTCInformation) { }
        
        public void Transmit()
        {
            if (ServiceInfo == null) return;
            //All DTCs
            ConnectionUtil.TransmitData(new byte[] { ServiceInfo.RequestID, 0xFF, 0xFF, 0xFF });
        }

        public static ClearDTCInformation Receive(ASResponse response)
        {
            var service = new ClearDTCInformation();

            service.Response = response;
            return service;
        }
    }
    
    public class DTCValue
    {
        public string Code { get; set; }
        public byte FailureType { get; set; }
        public byte Mask { get; set; }
        public string Description { get => DTCFailure.GetByValue(FailureType).Description; }
    }
}
