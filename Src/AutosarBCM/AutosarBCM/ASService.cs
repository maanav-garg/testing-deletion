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
            if (Data[PayloadInfo.Index] == 0) result = "Activate";
            else if (Data[PayloadInfo.Index] == 1) result = "Inactivate";
            else if (Data[PayloadInfo.Index] == 2) result = "Null - No Control Active";

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

    public class HexDump : Payload
    {
        public HexDump(PayloadInfo payloadInfo, byte[] data) : base(payloadInfo, data) { }

        public override string Print()
        {
            return $"{PayloadInfo.Name,-40}: {Data[PayloadInfo.Index]}";
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
        DE02,
        DE03,
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
