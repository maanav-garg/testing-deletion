namespace AutosarBCM
{
    public enum SIDDescription : byte
    {
        SID_DIAGNOSTIC_SESSION_CONTROL = 0x10,
        SID_ECU_RESET = 0x11,
        SID_CLEAR_DIAGNOSTIC_INFORMATION = 0x14,
        SID_READ_DTC_INFORMATION = 0x19,
        SID_READ_DATA_BY_IDENTIFIER = 0x22,
        SID_READ_MEMORY_BY_ADDRESS = 0x23,
        SID_READ_SCALING_DATA_BY_IDENTIFIER = 0x24,
        SID_SECURITY_ACCESS = 0x27,
        SID_COMMUNICATION_CONTROL = 0x28,
        SID_READ_DATA_BY_PERIODIC_IDENTIFIER = 0x2A,
        SID_DYNAMICALLY_DEFINE_DATA_IDENTIFIER = 0x2C,
        SID_WRITE_DATA_BY_IDENTIFIER = 0x2E,
        SID_INPUT_OUTPUT_CONTROL_BY_IDENTIFIER = 0x2F,
        SID_ROUTINE_CONTROL = 0x31,
        SID_REQUEST_DOWNLOAD = 0x34,
        SID_REQUEST_UPLOAD = 0x35,
        SID_TRANSFER_DATA = 0x36,
        SID_REQUEST_TRANSFER_EXIT = 0x37,
        SID_WRITE_MEMORY_BY_ADDRESS = 0x3D,
        SID_TESTER_PRESENT = 0x3E,
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
}
