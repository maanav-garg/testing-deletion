using AutosarBCM.Enums;
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

        public Service(ServiceName serviceName)
        {
            serviceInfo = ASApp.Configuration.Services.Where(x => x.ID == (byte)serviceName).FirstOrDefault();
        }

        public static void Transmit(ServiceName serviceName, ControlName controlName)
        {
            ASApp.Configuration.Controls.Where(x => x.Name == controlName.ToString()).FirstOrDefault().Transmit(serviceName);
        }
    }

    public class ReadDataByIdenService : Service
    {
        public ReadDataByIdenService() : base(ServiceName.ReadDataByIdentifier) { }

        public void Transmit(ControlInfo controlInfo)
        {
            var request = new ASRequest(serviceInfo, controlInfo, $"03-{serviceInfo.ID.ToString("X")}-{BitConverter.ToString(BitConverter.GetBytes(controlInfo.Address).Reverse().ToArray())}-00-00-00-00");
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
            ConnectionUtil.TransmitData(0x0726, new byte[] { serviceInfo.ID, sessionInfo.ID, 0, 0, 0, 0, 0, 0 });
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
        public byte[] Data { get; private set; }
        public ControlInfo ControlInfo { get; private set; }
        public List<Payload> Payloads { get; private set; } = new List<Payload>();

        public ASResponse(byte[] data)
        {
            Data = data;

            ControlInfo = ASApp.Configuration.Controls.Where(x => x.Address == BitConverter.ToUInt16(data.Skip(2).Take(2).Reverse().ToArray(), 0)).FirstOrDefault();
            foreach (var pInfo in ControlInfo.Responses.Where(a => a.ServiceID == data[1]).First().Payloads)
                Payloads.Add((Payload)Activator.CreateInstance(Type.GetType($"AutosarBCM.Config.{pInfo.TypeName}"), new object[] { pInfo, data }));
        }
    }

    public abstract class Payload
    {
        protected byte[] Data;
        public PayloadInfo PayloadInfo { get; set; }

        public Payload(PayloadInfo payloadInfo, byte[] data)
        {
            PayloadInfo = payloadInfo;
            Data = data;
        }

        public abstract string Print();
    }

    public class DID_Byte_Enable_Disable : Payload
    {
        public DID_Byte_Enable_Disable(PayloadInfo payloadInfo, byte[] data) : base(payloadInfo, data) { }

        public override string Print()
        {
            var result = string.Empty;
            if (this.Data[PayloadInfo.Index] == 0) result = "Disable";
            else if (this.Data[PayloadInfo.Index] == 1) result = "Enable";

            return $"{PayloadInfo.Name,-40}{result}";
        }
    }

    public class DID_Byte_Activate_Inactivate : Payload
    {
        public DID_Byte_Activate_Inactivate(PayloadInfo payloadInfo, byte[] data) : base(payloadInfo, data) { }

        public override string Print()
        {
            var result = string.Empty;
            if (Data[PayloadInfo.Index] == 0) result = "Null - No Control Active";
            else if (Data[PayloadInfo.Index] == 1) result = "Activate";
            else if (Data[PayloadInfo.Index] == 2) result = "Inactivate";

            return $"{PayloadInfo.Name,-40}{result}";
        }
    }

    public class DID_Bytes_High_Low : Payload
    {
        public DID_Bytes_High_Low(PayloadInfo payloadInfo, byte[] data) : base(payloadInfo, data) { }

        public override string Print()
        {
            var result = string.Empty;
            if (Data[PayloadInfo.Index] == 0) result = "Low";
            else if (Data[PayloadInfo.Index] == 1) result = "High";

            return $"{PayloadInfo.Name,-40}{result}";
        }
    }

    public class DID_DE00_0 : Payload
    {
        public DID_DE00_0(PayloadInfo payloadInfo, byte[] data) : base(payloadInfo, data) { }

        public override string Print()
        {
            var result = string.Empty;
            if (this.Data[PayloadInfo.Index] == 0) result = "AMT";
            else if (this.Data[PayloadInfo.Index] == 1) result = "Manual Transmission";

            return $"{PayloadInfo.Name,-40}{result}";
        }
    }

    public class DID_DE00_4 : Payload
    {
        public DID_DE00_4(PayloadInfo payloadInfo, byte[] data) : base(payloadInfo, data) { }

        public override string Print()
        {
            var result = string.Empty;
            if (this.Data[PayloadInfo.Index] == 0) result = "Euro 3";
            else if (this.Data[PayloadInfo.Index] == 1) result = "Euro 5";
            else if (this.Data[PayloadInfo.Index] == 2) result = "Euro 6";
            else if (this.Data[PayloadInfo.Index] == 3) result = "Euro 7";

            return $"{PayloadInfo.Name,-40}{result}";
        }
    }


    public class DID_Byte_Present_notPresent : Payload
    {
        public DID_Byte_Present_notPresent(PayloadInfo payloadInfo, byte[] data) : base(payloadInfo, data) { }

        public override string Print()
        {
            var result = string.Empty;
            if (this.Data[PayloadInfo.Index] == 0) result = "Not Present";
            else if (this.Data[PayloadInfo.Index] == 1) result = "Present";

            return $"{PayloadInfo.Name,-40}{result}";
        }
    }

    public class OnOffState : Payload
    {
        public OnOffState(PayloadInfo payloadInfo, byte[] data) : base(payloadInfo, data) { }

        public override string Print()
        {
            var result = string.Empty;
            if (Data[PayloadInfo.Index] == 0) result = "On";
            else if (Data[PayloadInfo.Index] == 1) result = "Off";

            return $"{PayloadInfo.Name,-40}{result}";
        }
    }

    public class HexDump_1Byte : Payload
    {
        public HexDump_1Byte(PayloadInfo payloadInfo, byte[] data) : base(payloadInfo, data) { }

        public override string Print()
        {
            return $"{PayloadInfo.Name,-40}: {Data[PayloadInfo.Index]}";
        }
    }
    public class HexDump_2Bytes : Payload
    {
        public HexDump_2Bytes(PayloadInfo payloadInfo, byte[] data) : base(payloadInfo, data) { }

        public override string Print()
        {
            return $"{PayloadInfo.Name,-40}: {BitConverter.ToUInt16(Data, PayloadInfo.Index)}";
        }
    }
    public class HexDump_16Bytes : Payload
    {
        public HexDump_16Bytes(PayloadInfo payloadInfo, byte[] data) : base(payloadInfo, data) { }

        public override string Print()
        {
            return $"{PayloadInfo.Name,-40}: {BitConverter.ToUInt16(Data, PayloadInfo.Index)}";
        }
    }


    public class Unsigned_1Byte : Payload
    {
        public Unsigned_1Byte(PayloadInfo payloadInfo, byte[] data) : base(payloadInfo, data) { }

        public override string Print()
        {
            return $"{PayloadInfo.Name,-40}: {Data[PayloadInfo.Index]}";
        }
    }
    public class DID_Lamp_Status : Payload
    {
        public DID_Lamp_Status(PayloadInfo payloadInfo, byte[] data) : base(payloadInfo, data) { }
        public override string Print()
        {
            var result = string.Empty;
            if (this.Data[PayloadInfo.Index] == 0) result = "Null - Not Configured for LED Outage Detection";
            else if (this.Data[PayloadInfo.Index] == 1) result = "Lamp On";
            else if (this.Data[PayloadInfo.Index] == 2) result = "Lamp Off";

            return $"{PayloadInfo.Name,-40}{result}";
        }
    }
    public class DID_Byte_Lamp_Control : Payload
    {
        public DID_Byte_Lamp_Control(PayloadInfo payloadInfo, byte[] data) : base(payloadInfo, data) { }
        public override string Print()
        {
            var result = string.Empty;
            if (this.Data[PayloadInfo.Index] == 0) result = "Null - No Control Active";
            else if (this.Data[PayloadInfo.Index] == 1) result = "Active";
            else if (this.Data[PayloadInfo.Index] == 2) result = "Inactive";
            else if (this.Data[PayloadInfo.Index] == 3) result = "Flash";

            return $"{PayloadInfo.Name,-40}{result}";
        }
    }
    public class DID_4253_0 : Payload
    {
        public DID_4253_0(PayloadInfo payloadInfo, byte[] data) : base(payloadInfo, data) { }
        public override string Print()
        {
            var result = string.Empty;
            if (this.Data[PayloadInfo.Index] == 0) result = "Off - No Request";
            else if (this.Data[PayloadInfo.Index] == 1) result = "Left Turn Request";
            else if (this.Data[PayloadInfo.Index] == 2) result = "Right Turn Request";
            else if (this.Data[PayloadInfo.Index] == 3) result = "Fault";

            return $"{PayloadInfo.Name,-40}{result}";
        }
    }
    public class DID_F166_0 : Payload
    {
        public DID_F166_0(PayloadInfo payloadInfo, byte[] data) : base(payloadInfo, data) { }
        public override string Print()
        {
            return $"{PayloadInfo.Name,-40}{BitConverter.ToInt32(Data, PayloadInfo.Index)}";
        }
    }
    public class DID_42A0_0 : Payload
    {
        public DID_42A0_0(PayloadInfo payloadInfo, byte[] data) : base(payloadInfo, data) { }
        public override string Print()
        {
            var result = string.Empty;
            if (this.Data[PayloadInfo.Index] == 1) result = "Active";
            else if (this.Data[PayloadInfo.Index] == 2) result = "Inactive";
            else if (this.Data[PayloadInfo.Index] == 3) result = "Null - No Control Active";
            else if (this.Data[PayloadInfo.Index] == 4) result = "On";
            else if (this.Data[PayloadInfo.Index] == 5) result = "Off";
            else if (this.Data[PayloadInfo.Index] == 6) result = "Daytime Running Lamp";

            return $"{PayloadInfo.Name,-40}{result}";
        }
    }
    public class DID_40E8_0 : Payload
    {
        public DID_40E8_0(PayloadInfo payloadInfo, byte[] data) : base(payloadInfo, data) { }
        public override string Print()
        {
            var result = string.Empty;
            if (this.Data[PayloadInfo.Index] == 1) result = "Active";
            else if (this.Data[PayloadInfo.Index] == 2) result = "Inactive";
            else if (this.Data[PayloadInfo.Index] == 3) result = "VREG - The Right Front Low Beam Circuit Is Pulse Width Modulating For Voltage Regulation";
            else if (this.Data[PayloadInfo.Index] == 4) result = "Daytime Running Light (DRL) - The Right Front Low Beam Circuit Is Pulse Width Modulating For DRL";

            return $"{PayloadInfo.Name,-40}{result}";
        }
    }

    public class DID_40E7_0 : Payload
    {
        public DID_40E7_0(PayloadInfo payloadInfo, byte[] data) : base(payloadInfo, data) { }
        public override string Print()
        {
            var result = string.Empty;
            if (this.Data[PayloadInfo.Index] == 1) result = "Active";
            else if (this.Data[PayloadInfo.Index] == 2) result = "Inactive";
            else if (this.Data[PayloadInfo.Index] == 3) result = "VREG - The Left Front Low Beam Circuit Is Pulse Width Modulating For Voltage Regulation";
            else if (this.Data[PayloadInfo.Index] == 4) result = "Daytime Running Light (DRL) - The Left Front Low Beam Circuit Is Pulse Width Modulating For DRL";

            return $"{PayloadInfo.Name,-40}{result}";
        }
    }
    public class DID_4255_Daytime_Running_Light : Payload
    {
        public DID_4255_Daytime_Running_Light(PayloadInfo payloadInfo, byte[] data) : base(payloadInfo, data) { }
        public override string Print()
        {
            var result = string.Empty;
            if (this.Data[PayloadInfo.Index] == 0) result = "Null";
            else if (this.Data[PayloadInfo.Index] == 1) result = "On / Set On";
            else if (this.Data[PayloadInfo.Index] == 2) result = "Off / Set Off";

            return $"{PayloadInfo.Name,-40}{result}";
        }
    }
    public class DID_41EF_0 : Payload
    {
        public DID_41EF_0(PayloadInfo payloadInfo, byte[] data) : base(payloadInfo, data) { }
        public override string Print()
        {
            var result = string.Empty;
            if (this.Data[PayloadInfo.Index] == 0) result = "Null - No Control Active";
            else if (this.Data[PayloadInfo.Index] == 1) result = "Active";
            else if (this.Data[PayloadInfo.Index] == 2) result = "Inactive";

            return $"{PayloadInfo.Name,-40}{result}";
        }
    }
    public class Internal_Version : Payload
    {
        public Internal_Version(PayloadInfo payloadInfo, byte[] data) : base(payloadInfo, data) { }
        public override string Print()
        {
            return $"{PayloadInfo.Name,-40}{BitConverter.ToInt32(Data, PayloadInfo.Index)}";
        }
    }
    public class DID_41F1_0 : Payload
    {
        public DID_41F1_0(PayloadInfo payloadInfo, byte[] data) : base(payloadInfo, data) { }
        public override string Print()
        {
            var result = string.Empty;
            if (this.Data[PayloadInfo.Index] == 0) result = "Null - No Control Active";
            else if (this.Data[PayloadInfo.Index] == 1) result = "Active";
            else if (this.Data[PayloadInfo.Index] == 2) result = "Inactive";

            return $"{PayloadInfo.Name,-40}{result}";
        }
    }
    public class DID_41EC_0 : Payload
    {
        public DID_41EC_0(PayloadInfo payloadInfo, byte[] data) : base(payloadInfo, data) { }
        public override string Print()
        {
            var result = string.Empty;
            if (this.Data[PayloadInfo.Index] == 0) result = "Null - No Control Active";
            else if (this.Data[PayloadInfo.Index] == 1) result = "Active";
            else if (this.Data[PayloadInfo.Index] == 2) result = "Inactive";
            else if (this.Data[PayloadInfo.Index] == 3) result = "Ramping";
            else if (this.Data[PayloadInfo.Index] == 4) result = "Flash";

            return $"{PayloadInfo.Name,-40}{result}";
        }
    }
    public class DID_41F3_0 : Payload
    {
        public DID_41F3_0(PayloadInfo payloadInfo, byte[] data) : base(payloadInfo, data) { }
        public override string Print()
        {
            var result = string.Empty;
            if (this.Data[PayloadInfo.Index] == 0) result = "Null - No Control Active";
            else if (this.Data[PayloadInfo.Index] == 1) result = "Active";
            else if (this.Data[PayloadInfo.Index] == 2) result = "Inactive";
            else if (this.Data[PayloadInfo.Index] == 3) result = "Ramping";
            else if (this.Data[PayloadInfo.Index] == 4) result = "Flash";

            return $"{PayloadInfo.Name,-40}{result}";
        }
    }
    public class DID_Byte_Kmph : Payload
    {
        public DID_Byte_Kmph(PayloadInfo payloadInfo, byte[] data) : base(payloadInfo, data) { }

        public override string Print()
        {
            var result = string.Empty;
            if (this.Data[PayloadInfo.Index] == 0x05) result = "5 kmph";
            else if (this.Data[PayloadInfo.Index] == 0x0A) result = "10 kmph";
            else if (this.Data[PayloadInfo.Index] == 0x0F) result = "15 kmph";
            else if (this.Data[PayloadInfo.Index] == 0x14) result = "20 kmph";
            else if (this.Data[PayloadInfo.Index] == 0x19) result = "25 kmph";
            else if (this.Data[PayloadInfo.Index] == 0x1E) result = "30 kmph";
            else if (this.Data[PayloadInfo.Index] == 0x23) result = "35 kmph";
            else if (this.Data[PayloadInfo.Index] == 0x28) result = "40 kmph";
            else if (this.Data[PayloadInfo.Index] == 0x2D) result = "45 kmph";
            else if (this.Data[PayloadInfo.Index] == 0x32) result = "50 kmph";
            else if (this.Data[PayloadInfo.Index] == 0xFF) result = "Not used";

            return $"{PayloadInfo.Name,-40}{result}";
        }
    }
    public class DID_DE02_7 : Payload
    {
        public DID_DE02_7(PayloadInfo payloadInfo, byte[] data) : base(payloadInfo, data) { }
        public override string Print()
        {
            var result = string.Empty;
            if (this.Data[PayloadInfo.Index] == 0) result = "Non-Construciton Vehicle";
            else if (this.Data[PayloadInfo.Index] == 1) result = "Construction Vehicle";

            return $"{PayloadInfo.Name,-40}{result}";
        }
    }
    public class DID_DE03_0 : Payload
    {
        public DID_DE03_0(PayloadInfo payloadInfo, byte[] data) : base(payloadInfo, data) { }
        public override string Print()
        {
            var result = string.Empty;
            if (this.Data[PayloadInfo.Index] == 0) result = "H625";
            else if (this.Data[PayloadInfo.Index] == 1) result = "H476";
            else if (this.Data[PayloadInfo.Index] == 2) result = "H566";

            return $"{PayloadInfo.Name,-40}{result}";
        }
    }
    public class DID_DE04_7 : Payload
    {
        public DID_DE04_7(PayloadInfo payloadInfo, byte[] data) : base(payloadInfo, data) { }
        public override string Print()
        {
            var result = string.Empty;
            if (this.Data[PayloadInfo.Index] == 0) result = "ABS";
            else if (this.Data[PayloadInfo.Index] == 1) result = "EBS";

            return $"{PayloadInfo.Name,-40}{result}";
        }
    }
    public class DID_DE06_1 : Payload
    {
        public DID_DE06_1(PayloadInfo payloadInfo, byte[] data) : base(payloadInfo, data) { }
        public override string Print()
        {
            var result = string.Empty;
            if (this.Data[PayloadInfo.Index] == 0X00) result = "No Flash";
            else if (this.Data[PayloadInfo.Index] == 0x05) result = "5 Time Flash";

            return $"{PayloadInfo.Name,-40}{result}";
        }
    }
    public class DID_DE06_3 : Payload
    {
        public DID_DE06_3(PayloadInfo payloadInfo, byte[] data) : base(payloadInfo, data) { }
        public override string Print()
        {
            var result = string.Empty;
            if (this.Data[PayloadInfo.Index] == 0) result = "Not Changed";
            else if (this.Data[PayloadInfo.Index] == 1) result = "Changed";

            return $"{PayloadInfo.Name,-40}{result}";
        }
    }
    public class DID_DE08_7 : Payload
    {
        public DID_DE08_7(PayloadInfo payloadInfo, byte[] data) : base(payloadInfo, data) { }

        public override string Print()
        {
            var result = string.Empty;
            if (this.Data[PayloadInfo.Index] == 0x00) result = "Tractor";
            else if (this.Data[PayloadInfo.Index] == 0x01) result = "Mixer";
            else if (this.Data[PayloadInfo.Index] == 0x02) result = "Tipper";
            else if (this.Data[PayloadInfo.Index] == 0x03) result = "Road";
            else if (this.Data[PayloadInfo.Index] == 0x04) result = "Full Trailer";
            else if (this.Data[PayloadInfo.Index] == 0x05) result = "Concrete Pump";

            return $"{PayloadInfo.Name,-40}{result}";
        }
    }
    public class DID_DE0B_1 : Payload
    {
        public DID_DE0B_1(PayloadInfo payloadInfo, byte[] data) : base(payloadInfo, data) { }

        public override string Print()
        {
            return $"{Data[PayloadInfo.Index]} %";
        }
    }
    public class DID_DE01_3 : Payload
    {
        public DID_DE01_3(PayloadInfo payloadInfo, byte[] data) : base(payloadInfo, data) { }

        public override string Print()
        {
            var result = string.Empty;
            if (this.Data[PayloadInfo.Index] == 0) result = "9 Liter";
            else if (this.Data[PayloadInfo.Index] == 1) result = "13 Liter";

            return $"{PayloadInfo.Name,-40}{result}";
        }
    }

    public class DID_DE01_4 : Payload
    {
        public DID_DE01_4(PayloadInfo payloadInfo, byte[] data) : base(payloadInfo, data) { }

        public override string Print()
        {
            var result = string.Empty;
            if (this.Data[PayloadInfo.Index] == 0) result = "Disable";
            else if (this.Data[PayloadInfo.Index] == 1) result = "Dry Type";
            else if (this.Data[PayloadInfo.Index] == 2) result = "Wet Type";
            else if (this.Data[PayloadInfo.Index] == 3) result = "Dry&Wet Type";

            return $"{PayloadInfo.Name,-40}{result}";
        }
    }
    //public class Unsigned_1Byte : Payload
    //{
    //    public Unsigned_1Byte(PayloadInfo payloadInfo, byte[] data) : base(payloadInfo, data) { }

    //    public override string Print()
    //    {
    //        var result = string.Empty;
    //        byte[] singleByteArray = new byte[] { Data[PayloadInfo.Index] };
    //        result = BitConverter.ToString(singleByteArray);
    //        return $"{PayloadInfo.Name,-40}{result}";
    //    }
    //}
    public class DID_PWM : Payload
    {
        public DID_PWM(PayloadInfo payloadInfo, byte[] data) : base(payloadInfo, data) { }
        public override string Print()
        {
            var result = string.Empty;
            return $"{BitConverter.ToSingle(Data, 0)} %";
        }
    }
    public class DID_Byte_On_Off : Payload
    {
        public DID_Byte_On_Off(PayloadInfo payloadInfo, byte[] data) : base(payloadInfo, data) { }
        public override string Print()
        {
            var result = string.Empty;
            if (this.Data[PayloadInfo.Index] == 0) result = "Off";
            else if (this.Data[PayloadInfo.Index] == 1) result = "On";

            return $"{PayloadInfo.Name,-40}{result}";
        }
    }


    internal interface IReceiver
    {
        bool Receive(ASResponse response);
    }
}

namespace AutosarBCM.Enums
{
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
        DiagnosticSessionControl = 0x10,
        ReadDataByIdentifier = 0x22,
        InputOutputControlByIdentifier = 0x2F,
        WriteDataByIdentifier = 0x2E,
    }

    public enum ControlName
    {
        DEOO,
        DEO1,
        DE02,
        DE03,
        DE04,
        VIN,
        DE06,
        DE07,
        DE08,
        DE09,
        DE0B,
        DE0C,
        DE0D,
        Spare_DID_2,
        Spare_DID_3,
        DE10,
        DE11,
        DE12,
        DE13,
        DE14,
        DE15,
        Total_Distance_SS_Axle_Lifted,
        Total_Distance_TA_Axle_Lifted,
        Total_Distance_SS_TA_Axle_Lifted,
        Spare_DID_7,
        DE20,
        DE21,
        Spare_DID_4,
        Keyfob_Battery_Status,
        DTC_Configuration,
        DE25,
        DE26,
        DE27,
        DE28,
        DE3A,
        DE3B,
        DE3C, 
        DE3D,
        ACCutOffSupply,
        AirHornActivateSupply,
        Ajar_On_Off_Control,
        All_Doors_Lock_and_Ajar_Input_Signal,
        All_Doors_Lock_and_Ajar_Output_Signal,
        Ambient_Light_LED_Power_Supply,
        Battery_Saver_System_Output_Signals,
        BedAreaLightingSupply,
        Blower_Control_Relay_Supply,
        Blower_Switch,
        BrakeLightRelaySupply,
        CabinTiltValveSupply,
        Cruise_control_switches,
        Daytime_Running_Light_Output,
        Differential_Control_Input,
        DifferentialLockValve1Supply,
        DomeLightSupply,
        DoorLockIndicatorSupply,
        Driver_Power_Window_Motor,
        EngineBrakeSupply,
        FrontParkingLightSupply,
        Hall_Sensor_Supply,
        Hazard_Warning_Switch,
        Headlamp_High_Beam_Output_Control,
        HeatedMirrorSupply,
        HeatedWindshieldSupply,
        Horn_Output_Control,
        Horn_Switch,
        Input_Switches,
        InterLockValveSupply,
        Interior12VLightDimmingSupply,
        InteriorLightDimmingSupply,
        Key_Switch_System_Input_Signal,
        LED_Outputs,
        LIN_PWS_and_HLS_Switch,
        Left_Front_Fog_Lamps_Output,
        Left_Front_Low_Beam_Output_Contro,
        Left_Front_Low_Beam_Output,
        Left_Front_Turn_Lamp_Outage_Feedback,
        Left_Rear_Turn_Lamp_LED_Outage_Feedback,
        Left_Rear_Turn_Signal_Lamp_Control,
        LowLinerFrontSideLiftedValveSupply,
        Main_ECU_Voltage_Supply,
        Map_Lamp_PWM_Supply,
        NOS_Message_Database,
        PTO_Switches,
        PTO_Valve_Supply,
        PWM_StartStopIllimunation12VLED,
        Passenger_Power_Window_Motor,
        Power_Mirror_Control,
        Power_Window_Switch_Input,
        Rear_Fog_Lamps_Output,
        Rear_Park_Lamps_Output,
        ReverseGearSupply,
        Right_Front_Fog_Lamps_Output,
        Right_Front_Low_Beam_Output_Control,
        Right_Front_Low_Beam_Output,
        Right_Front_Turn_Lamp_Outage_Feedback,
        Right_Rear_Turn_Lamp_LED_Outage_Feedback,
        Right_Rear_Turn_Signal_Lamp_Control,
        Spare_Analog_Input,
        Spare_Digital_Outputs,
        Spare_PWM_Inputs,
        Spare_Switch,
        Start_Stop_Switches,
        SteeringColumnSwitchIlluminationSupply,
        StepLightsSupply,
        Stowage_Box_Reminder,
        Sunroof_Motor,
        Sunroof_Switch,
        TCCM_Input,
        TrailerTagAxleLifting1Supply,
        Turn_Indicator_Switch_Input,
        Vehicle_Battery_Voltage,
        WCInhibit,
        Washer_Pump_Front_Relay,
        WaterPumpSupply,
        Windscreen_Wipers_System_Output_Signal,
        WiperParkPositionSwitch
    }
}
