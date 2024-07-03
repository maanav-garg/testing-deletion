using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutosarBCM
{
    public enum SIDDescription : byte
    {
        SID_DIAGNOSTIC_SESSION_CONTROL = 0x10,
        SID_ECU_RESET = 0x11,
        SID_CLEAR_DIAGNOSTIC_INFORMATION = 0x14,
        SID_READ_DTC_INFORMATION = 0x19,
        SID_READ_DATA_BY_IDENTIFIER_REQ = 0x22,
        SID_READ_MEMORY_BY_ADDRESS = 0x23,
        SID_READ_SCALING_DATA_BY_IDENTIFIER = 0x24,
        SID_SECURITY_ACCESS = 0x27,
        SID_COMMUNICATION_CONTROL = 0x28,
        SID_READ_DATA_BY_PERIODIC_IDENTIFIER = 0x2A,
        SID_DYNAMICALLY_DEFINE_DATA_IDENTIFIER = 0x2C,
        SID_WRITE_DATA_BY_IDENTIFIER = 0x2E,
        SID_INPUT_OUTPUT_CONTROL_BY_IDENTIFIER_REQ = 0x2F,
        SID_ROUTINE_CONTROL = 0x31,
        SID_REQUEST_DOWNLOAD = 0x34,
        SID_REQUEST_UPLOAD = 0x35,
        SID_TRANSFER_DATA = 0x36,
        SID_REQUEST_TRANSFER_EXIT = 0x37,
        SID_WRITE_MEMORY_BY_ADDRESS = 0x3D,
        SID_TESTER_PRESENT = 0x3E,
        SID_READ_DATA_BY_IDENTIFIER_RESP = 0x62,
        SID_INPUT_OUTPUT_CONTROL_BY_IDENTIFIER_RESP = 0x6F,
        SID_NEGATIVE_RESPONSE = 0x7F,
        SID_ACCESS_TIMING_PARAMETER = 0X83,
        SID_SECURED_DATA_TRANSMISSION = 0X84,
        SID_CONTROL_DTC_SETTING = 0x85,
        SID_RESPONSE_ON_EVENT = 0x86
    }


    public enum NRCDescription : byte
    {
        NRC_GENERAL_REJECT = 0x10,
        NRC_SERVICE_NOT_SUPPORTED = 0x11,
        NRC_SUB_FUNCTION_NOT_SUPPORTED = 0x12,
        NRC_INCORRECT_MESSAGE_LENGTH_OR_INVALID_FORMAT = 0x13,
        NRC_RESPONSE_TOO_LONG = 0x14,
        NRC_BUSY_REPEAT_REQUEST = 0x21,
        NRC_CONDITIONS_NOT_CORRECT = 0x22,
        NRC_REQUEST_SEQUENCE_ERROR = 0x24,
        NRC_NO_RESPONSE_FROM_SUBNET_COMPONENT = 0x25,
        NRC_FAILURE_PREVENTS_EXECUTION_OF_REQUESTED_ACTION = 0x26,
        NRC_REQUEST_OUT_OF_RANGE = 0x31,
        NRC_SECURITY_ACCESS_DENIED = 0x33,
        NRC_INVALID_KEY = 0x35,
        NRC_EXCEED_NUMBER_OF_ATTEMPTS = 0x36,
        NRC_REQUIRED_TIME_DELAY_NOT_EXPIRED = 0x37,
        NRC_UPLOAD_DOWNLOAD_NOT_EXCEPTED = 0x70,
        NRC_TRANSFER_DATA_SUSPENDED = 0x71,
        NRC_GENERAL_PROGRAMMING_FAILURE = 0x72,
        NRC_WRONG_BLOCK_SEQUENCE_COUNTER = 0x73,
        NRC_RESPONSE_PENDING = 0x78,
        NRC_SUB_FUNCTION_NOT_SUPPORTED_IN_ACTIVE_SESSION = 0x7E,
        NRC_SERVICE_NOT_SUPPORTED_IN_ACTIVE_SESSION = 0x7F
    }
    public enum Output_OpenCloseGroup : short
    {
        EO_On = 0x6104,
        EO_Off = 0x6105,
        DIO_Open = 0x610A,
        DIO_Close = 0x610B,
        PowerWindow_Open = 0x6146,
        PowerWindow_Close = 0x6147,
        Sunroof_Open = 0x6148,
        Sunroof_Close = 0x6149,
        Washer_Open = 0x6150,
        Washer_Close = 0x6151,
        WaterPump_Open = 0x6152,
        WaterPump_Close = 0x6153
    }
    
    public enum  Output_PwmGroup : short
    {
        PWM_Set = 0x6112,
        XS4200 = 0x6107
    }

    public enum Output_PowerMirror : short
    {
        SetOpen = 0x6142,
        SetClose = 0x6143
    }

    public enum Output_ReadGroup : short
    {
        BBT_BTS = 0x6101,
        SWCH = 0x6102,
        EO = 0x6103,
        ADC = 0x6106, 
        CURRENTVALUE = 0x6198, 
        XS4200 = 0x6108,
        DIO = 0x6109,
        VND5T035LAK = 0x610C,
        HallSensorRightSupply_0_5A = 0x610D,
        HallSensorLeftSupply_0_5A = 0x610E,
        DoorLockRight = 0x610F,
        DoorLockLeft = 0x6110,
        HSD = 0x6111,
        MPQ6528 = 0x6113,
        PWHALL = 0X6114,
        FS26 = 0x6116,
        LOOPBACK = 0x6118,
        LOOPBACK_RESULT = 0x6119,
        VNHD7008AY = 0x6110,
        Wiper = 0x6154
    }

    public enum BBT_BTS_ReadResponse : short
    {
        NORMALOPERATION = 0x0000,
        CURRENTLIMITATION = 0x0001,
        SHORTTOGND = 0x0002,
        OVERTEMPERATURE = 0x0003,
        SHORTTOBATT = 0x0004,
        OPENLOAD = 0x0005,
        INVERSECURRENT = 0x0006,
        UNDERLOAD = 0x0007,
        UNDEFINED = 0x0008,
        DONTCARE = 0x0009,
        ISFAULT = 0x000A
    }

    public enum SWCH_ReadResponse : short
    {
        OFF = 0x0000,
        ON = 0x0001,
        INVALIDPARAM = 0x0002
    }

    public enum EO_ReadResponse : short
    {
        STD_LOW = 0x0000,
        STD_HIGH = 0x0001
    }

    public enum XS4200 : short
    {
        FAULT_NOT_OCCUR = 0x0000,
        FAULT_OVER_CURRENT = 0x0001,
        FAULT_SHORT_CIRCUIT = 0x0002,
        FAULT_OT = 0x0004,
        FAULT_SHORT_BAT = 0x0008,
        FAULT_OPEN_LOAD_OFFSTATE = 0x0010,
        FAULT_OPEN_LOAD_ONSTATE = 0x0020,
        FAULT_OVER_TEMP = 0x0100
    }

    public enum DIO_ReadResponse : short
    {
        STD_LOW = 0x0000,
        STD_HIGH = 0x0001
    }
    
    public enum VND5T035LAK_ReadResponse : short
    {
        STANDBY = 0X0000,
        NORMALOPERATION = 0X0001,
        OVERLOAD = 0X0002,
        OVERTEMP_SHORTTOGRND = 0X0003,
        SHORTTOBAT = 0X0004,
        OPENLOADOFFSTATE = 0X0005,
        CON_NONE = 0X0006,
        CON_ERROR = 0X0007
    }

    public enum HallSensor_ReadResponse : short
    {
        STANDBY = 0x0000,
        NORMALOPERATION = 0x0001,
        OVERLOAD = 0x0002,
        UNDERVOLTAGE = 0x0003,
        OFF_STATE_DIAG = 0x0004,
        NGTVOUTVOL = 0x0005,
        CON_NONE = 0x0006,
        CON_ERROR = 0x0007
    }

    public enum DoorLock_ReadResponse : short
    {
        STANDBY = 0x0000,
        NORMAL = 0x0001,
        SHORTOBAT = 0x0002,
        OPENLOAD = 0x0003,
        VDS_LSA_PROTECT_LATCHOFF = 0x0004,
        VDS_LSB_PROTECT_LATCHOFF = 0x0005,
        HSA_PROTECT_LATCHOFF = 0x0006,
        HSB_PROTECT_LATCHOFF = 0x0007,
        UNNONE = 0x0008,
        CON_ERROR = 0x0009
    }
    
    public enum HSD_ReadResponse : short
    {
        STANDBY = 000000,
        NORMAL = 000001,
        OVERLOAD = 000002,
        UNDERVOLTAGE = 000003,
        OFF_STATE_DIAGNOSTICS = 000004,
        NEGATIVE_OUTPUT_VOLTAGE = 000005,
        SHORT_TO_VBAT = 000006,
        OPEN_LOAD = 000007,
        SHORT_TO_GROUND = 000008
    }

    public enum MPQ6528_ReadResponse : byte
    {
        E_OK = 0x0000,
        E_NOT_OK = 0x0001
    }
    
    public enum PWM_SetResponse : byte
    {
        E_OK = 0x0000,
        E_NOT_OK = 0x0001
    }

    public enum XS4200PWM_SetResponse : byte
    {
        E_OK = 0x0000,
        E_NOT_OK = 0x0001
    }

    public enum DigitalUdsOnCan_OpenCloseValues : byte
    {
        E_OK = 0x0000,
        E_NOT_OK = 0x0001
    }

    public enum PwmTag
    {
        freq,
        duty,
        freq_duty
    }

    public enum PowerWindow_ReadResponse : byte
    {
    }
    public enum Loopback_Response : short
    {
        NOT_RECEIVED = 0x0000,
        RECEIVED_OK = 0x0001,
        RECEIVED_NOK = 0x0002
    }

    public enum RiskLevels : byte
    {
        Low,
        Medium,
        High
    }

    public enum DoorControls : short
    {
        Enable = 0x6144,
        Disable = 0x6145
    }

    public enum VNHD7008AY_Response : short
    {
        STANDBY = 0x0000,
        NORMAL = 0x0001,
        SHORTOBAT = 0x0002,
        OPENLOAD = 0x0003,
        VDS_LSA_PROTECT_LATCHOFF = 0x0004,
        VDS_LSB_PROTECT_LATCHOFF = 0x0005,
        HSA_PROTECT_LATCHOFF = 0x0006,
        HSB_PROTECT_LATCHOFF = 0x0007,
        UNNONE = 0x0008
    }
    
    public enum WIPER_ID
    {
        STOP_LOW = 0x00,
        STOP_HIGH = 0x01,
        LOW_HIGH = 0x02,
        HIGH_LOW = 0x03,
        HIGH_STOP = 0x04,
        LOW_STOP = 0x05
    }

    public enum Set_Group : short
    {
        SET_STATUS = 0x6155
    }

    public enum Status_Response : short
    {
        E_OK = 0x0000,
        E_NOT_OK = 0x0001
    }

    public enum EEProm_ByteGroup : byte
    {
        Write_PCI = 0x05,
        Write_SID = 0x3D,
        Read_PCI = 0x07,
        Read_SID = 0x23
    }
    
    public enum EEProm_ShortGroup : short
    {
        Write = 0x053D,
        Read = 0x0723
    }

    public enum EEProm_WriteResponse : byte
    {
        E_OK = 0x01,
        E_NOT_OK = 0x00,
    }

    public enum MonitorTestType
    {
        Generic,
        Environmental
    }

    public enum MessageDirection
    {
        TX,
        RX
    }

    public enum MappingState
    {
        NOC,
        OutputSent,
        OutputReceived,
        InputSent,
        InputReceived
    }
    
    public enum MappingResponse
    {
        NOC,
        OutputOpen,
        OutputClose,
        OutputError,
        InputOn,
        InputOff,
        InputError
    }

    public enum MappingOperation
    {
        Open,
        Close
    }

    public enum NCK2910_GPIO_PINValues
    {
        DEBUG_P1_RXD = 0x02,
        DEBUG_P2_RXC = 0x03
    }

    public enum NCK2910_GPIO_LevelValues
    {
        Low = 0x02,
        High = 0x03
    }
    
    public enum WiperStatus
    {
        Stop,
        Low,
        High 
    }
}
