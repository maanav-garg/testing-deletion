using AutosarBCM.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutosarBCM.Core
{
    internal interface IReceiver
    {
        bool Receive(ASResponse response);
    }

    public class ASRequest
    {
        private ServiceInfo ServiceInfo;
        private ControlInfo ControlInfo;

        public byte[] Data { get; set; }
        public Payload Payload { get; set; }

        public ASRequest(ServiceInfo serviceInfo, ControlInfo controlInfo, string data)
        {
            ServiceInfo = serviceInfo;
            ControlInfo = controlInfo;
            Data = data.Split('-').Select(x => Byte.Parse(x, System.Globalization.NumberStyles.HexNumber)).ToArray();
        }

        internal void Execute()
        {
            if (!ServiceInfo.Sessions.Contains(ASContext.CurrentSession.ID))
                return;

            if (!ControlInfo.Services.Contains(ServiceInfo.RequestID))
                return;

            ConnectionUtil.TransmitData(Data);
        }
    }

    public class ASResponse
    {
        public byte[] Data { get; private set; }
        public ServiceInfo ServiceInfo { get; private set; }
        public ControlInfo ControlInfo { get; private set; }
        public List<Payload> Payloads { get; private set; } = new List<Payload>();

        public byte ServiceID => Data[0];
        public ushort ControlAddress => BitConverter.ToUInt16(Data.Skip(1).Take(2).Reverse().ToArray(), 0);

        private ASResponse() { }

        public static ASResponse Parse(byte[] data)
        {
            var response = new ASResponse { Data = data };

            response.ServiceInfo = ASContext.Configuration.GetServiceByResponseID(response.ServiceID);
            response.ControlInfo = ASContext.Configuration.GetControlByAddress(response.ControlAddress);
            response.Payloads = response.ControlInfo.GetPayloads(response.ServiceInfo, data);

            return response;
        }
    }
}
