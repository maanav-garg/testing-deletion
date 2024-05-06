using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AutosarBCM.Config
{
    public class Service
    {
        protected ServiceInfo serviceInfo;

        public Service() { }

        public static void Transmit(ServiceName serviceName, ControlName controlName)
        {
            ASApp.Configuration.Controls.Where(x => x.Name == controlName.ToString()).FirstOrDefault().Transmit(serviceName);
        }
    }

    public class ReadDataByIdenService : Service
    {
        public ReadDataByIdenService()
        {
            serviceInfo = ASApp.Configuration.Services.Where(x => x.ID == (byte)ServiceName.ReadDataByIdentifier).FirstOrDefault();
        }

        public void Transmit(ControlInfo controlInfo)
        {
            var request = new ASRequest(serviceInfo, controlInfo, $"03-{serviceInfo.ID}-{BitConverter.ToString(BitConverter.GetBytes(controlInfo.Address).Reverse().ToArray())}-00-00-00-00");
            request.Execute();
        }
    }

    public class IOCtrlByIdenService : Service
    {
        public IOCtrlByIdenService()
        {
            serviceInfo = ASApp.Configuration.Services.Where(x => x.ID == (byte)ServiceName.InputOutputControlByIdentifier).FirstOrDefault();
        }

        public void Transmit(ControlInfo controlInfo)
        {
            throw new NotImplementedException();
        }
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
            if (!ServiceInfo.Sessions.Contains(ASApp.CurrentSession.ID))
                return;

            if (!ControlInfo.Services.Contains(ServiceInfo.ID))
                return;

            ConnectionUtil.TransmitData(0x0726, Data);
        }
    }

    public class ASResponse
    {
        public byte[] Data { get; set; }
        public List<Payload> Payloads { get; set; } = new List<Payload>();

        public ASResponse(byte[] data)
        {
            Data = data;

            var controlInfo = ASApp.Configuration.Controls.Where(x => x.Address == BitConverter.ToUInt16(data.Skip(2).Take(2).Reverse().ToArray(), 0)).FirstOrDefault();
            foreach (var pInfo in controlInfo.Responses.Where(a => a.ServiceID == data[1]).First().Payloads)
                Payloads.Add((Payload)Activator.CreateInstance(Type.GetType($"AutosarBCM.Config.{pInfo.Value}"), new object[] { pInfo.Key, data }));
        }
    }

    public abstract class Payload
    {
        protected byte[] Data;
        public string Name { get; set; }

        public Payload(string name, byte[] data)
        {
            Name = name;
            Data = data;
        }

        public abstract string Print();
    }

    public class PL_DID_Byte_Activate_Inactivate : Payload
    {
        public PL_DID_Byte_Activate_Inactivate(string name, byte[] data) : base(name, data) { }

        public override string Print()
        {
            if (this.Data[0] == 0) return "Activate";
            else if (this.Data[0] == 1) return "Inactivate";
            else if (this.Data[0] == 2) return "Null - No Control Active";
            return string.Empty;
        }
    }

    public class PL_OnOffState : Payload
    {
        public PL_OnOffState(string name, byte[] data) : base(name, data) { }

        public override string Print()
        {
            if (this.Data[0] == 0) return "On";
            else if (this.Data[0] == 1) return "Off";
            return string.Empty;
        }
    }

    public class PL_HexDump : Payload
    {
        public PL_HexDump(string name, byte[] data) : base(name, data) { }

        public override string Print()
        {
            return new StringBuilder()
                .AppendLine($"Door Unlock Relay - Passenger's Side: {(DID_Bits_On_Off)Data[4]}")
                .AppendLine($"Door Unlock Relay - Driver's Side: {(DID_Bits_On_Off)Data[5]}")
                .AppendLine($"Passanger Door Lock Relay: {(DID_Bits_On_Off)Data[6]}")
                .AppendLine($"Driver Door Lock Relay: {(DID_Bits_On_Off)Data[7]}")
                .ToString();
        }
    }

    public enum DID_Byte_Activate_Inactivate : byte
    {
        Activate = 0,
        Inactivate = 1,
        NoControlActive = 2
    }

    public enum DID_Bytes_High_Low : byte
    {
        High = 0,
        Low = 1
    }

    public enum OnOffState : byte
    {
        On = 0,
        Off = 1
    }

    public enum DID_42C0_0 : byte
    {
        Null = 0,
        Active = 1,
        Inactive = 2,
        DRL = 3
    }

    public enum DID_42C1_0 : byte
    {
        Null = 0,
        Active = 1,
        Inactive = 2,
        DRL = 3
    }

    public enum DID_Bits_On_Off : byte
    {
        On = 0,
        Off = 1
    }

    public enum DID_Byte_Present_notPresent : byte
    {
        NotPresent = 0,
        Present = 1
    }

    public enum ServiceName : byte
    {
        ReadDataByIdentifier = 0x22,
        InputOutputControlByIdentifier = 0x2F,
    }

    public enum ControlName
    {
        ACCutOffSupply,
        AirHornActivateSupply,
        Ajar_On_Off_Control,
        All_Doors_Lock_and_Ajar_Input_Signal,
        All_Doors_Lock_and_Ajar_Output_Signal,
        Ambient_Light_LED_Power_Supply,
        Battery_Saver_System_Output_Signals,
        BedAreaLightingSupply
    }
}
