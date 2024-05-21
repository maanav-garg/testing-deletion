using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutosarBCM.Core.Enums
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
        TesterPresent = 0x3E
    }

    public enum ControlName
    {
        DEOO,
        DEO1,
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
