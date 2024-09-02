using AutosarBCM.Core.Config;
using System;
using System.Linq;

namespace AutosarBCM.Core
{
    internal interface IReceiver
    {
        bool Sent(ushort address);
        bool Receive(Service service);
    }

    internal interface IIOControlByIdenReceiver : IReceiver { }
    internal interface IReadDataByIdenReceiver : IReceiver { }
    internal interface IDTCReceiver : IReceiver { }
    internal interface IWriteByIdenReceiver : IReceiver { }

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
            if (Data[0] == (byte)SIDDescription.SID_TESTER_PRESENT + 0x40)
            {
                IsPositiveRx = true;
                return TesterPresent.Receive(this);
            }
            else if (Data[0] == (byte)SIDDescription.SID_INPUT_OUTPUT_CONTROL_BY_IDENTIFIER + 0x40)
            {
                IsPositiveRx = true;
                return IOControlByIdentifierService.Receive(this);
            }
            else if (Data[0] == (byte)SIDDescription.SID_WRITE_DATA_BY_IDENTIFIER + 0x40)
            {
                IsPositiveRx = true;
                return WriteDataByIdentifierService.Receive(this);
            }
            else if (Data[0] == (byte)SIDDescription.SID_READ_DTC_INFORMATION + 0x40)
            {
                IsPositiveRx = true;
                return ReadDTCInformationService.Receive(this);
            }

            else if (Data[0] == (byte)SIDDescription.SID_READ_DATA_BY_IDENTIFIER + 0x40)
            {
                IsPositiveRx = true;
                return ReadDataByIdenService.Receive(this);
            }
            else if (Data[0] == (byte)SIDDescription.SID_DIAGNOSTIC_SESSION_CONTROL + 0x40)
            {
                IsPositiveRx = true;
                return DiagnosticSessionControl.Receive(this);
            }
            else if (Data[0] == (byte)SIDDescription.SID_NEGATIVE_RESPONSE)
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
