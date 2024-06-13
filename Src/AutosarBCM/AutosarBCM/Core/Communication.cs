using AutosarBCM.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutosarBCM.Core
{
    internal interface IReceiver
    {
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
    public class ToBeTransmittedList
    {
        public ushort Address { get; set; }
        public byte[] ByteValue { get; set; }
        public bool isMaskedValue { get; set; }
        public ToBeTransmittedList(ushort stringValue, byte[] byteVal, bool isMaskedVal)
        {
            Address = stringValue;
            ByteValue = byteVal;
            isMaskedValue = isMaskedVal;
        }
    }
    public class MainList
    {
        public ushort Address { get; set; }
        public byte[] ByteValue { get; set; }
        public bool isMaskedValue { get; set; }

        public MainList(ushort stringValue, byte[] byteVal, bool isMaskedVal)
        {
            Address = stringValue;
            ByteValue = byteVal;
            isMaskedValue = isMaskedVal;
        }
    }
    public class ASResponse
    {
        public byte[] Data { get; private set; }
        public bool IsPositiveRx { get; set; } = false;

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
            else if (Data[0] == ServiceInfo.ReadDTCInformation.ResponseID)
            {
                IsPositiveRx = true;
                return ReadDTCInformationService.Receive(this);
            }
            else if (Data[0] == ServiceInfo.ReadDataByIdentifier.RequestID
                || Data[0] == ServiceInfo.ReadDataByIdentifier.ResponseID)
            {
                if (Data[0] == ServiceInfo.ReadDataByIdentifier.ResponseID)
                    IsPositiveRx= true;
                return ReadDataByIdenService.Receive(this);
            }
            else if (Data[0] == ServiceInfo.DiagnosticSessionControl.ResponseID)
            { 
                IsPositiveRx = true;
                return DiagnosticSessionControl.Receive(this);
            }
            return null;
        }
    }
}
