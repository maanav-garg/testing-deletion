using AutosarBCM.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutosarBCM.Core
{
    internal interface IReceiver
    {
        bool Receive(Service service);
    }

    internal interface IReadDataByIdenReceiver : IReceiver { }
    internal interface IDTCReceiver : IReceiver { }

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

        public ASResponse(byte[] data)
        {
            Data = data;
        }

        public Service Parse()
        {
            if (Data[0] == 0x7E) return TesterPresent.Receive(this);
            else if (Data[0] == 0x59) return ReadDTCInformationService.Receive(this);
            else if (Data[0] == 0x62) return ReadDataByIdenService.Receive(this);

            return null;
        }
    }
}
