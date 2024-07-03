using AutosarBCM.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutosarBCM.Core
{
    internal interface IReceiver
    {
        bool Sent(short address);
        bool Receive(Service service);
    }

    internal interface IIOControlByIdenReceiver : IReceiver { }
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
            Data = data?.Split('-').Select(x => Byte.Parse(x, System.Globalization.NumberStyles.HexNumber)).ToArray();
        }

        public ASRequest(ServiceInfo serviceInfo, byte[] data)
        {
            ServiceInfo = serviceInfo;
            Data = data;
        }

        internal void Execute()
        {
            if (!ServiceInfo.Sessions.Contains(ASContext.CurrentSession.ID))
                return;

            if (!ControlInfo?.Services.Contains(ServiceInfo.RequestID) ?? false)
                return;

            ConnectionUtil.TransmitData(Data);
        }
    }
    
    public class ASResponse
    {
        public byte[] Data { get; private set; }
        public bool IsPositiveRx { get; set; } = false;
        public string NegativeResponseCode { get; set; } = string.Empty;

        public ASResponse(byte[] data)
        {
            Data = data;
        }

        public Service Parse()
        {
            if (Data[0] == ServiceInfo.TesterPresent.ResponseID)
            {
                IsPositiveRx = true;
                return TesterPresent.Receive(this);
            }
            else if (Data[0] == ServiceInfo.InputOutputControlByIdentifier.ResponseID)
            {
                IsPositiveRx = true;
                return IOControlByIdentifierService.Receive(this);
            }
            else if (Data[0] == ServiceInfo.ReadDTCInformation.ResponseID)
            {
                IsPositiveRx = true;
                return ReadDTCInformationService.Receive(this);
            }
            else if (Data[0] == ServiceInfo.ClearDTCInformation.ResponseID)
            {
                IsPositiveRx = true;
                return ClearDTCInformation.Receive(this);
            }
            else if (Data[0] == ServiceInfo.ReadDataByIdentifier.ResponseID)
            {
                IsPositiveRx = true;
                return ReadDataByIdenService.Receive(this);
            }
            else if (Data[0] == ServiceInfo.DiagnosticSessionControl.ResponseID)
            {
                IsPositiveRx = true;
                return DiagnosticSessionControl.Receive(this);
            }
            else if (Data[0] == ServiceInfo.NegativeResponse.ResponseID)
            {
                if (Enum.IsDefined(typeof(NRCDescription), Data[2]))
                    NegativeResponseCode = ((NRCDescription)Data[2]).ToString();
                else
                    NegativeResponseCode = "Undefined";
                return NegativeResponse.Receive(this);
            }
            return null;
        }
    }

}
