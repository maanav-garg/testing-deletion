using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutosarBCM
{
    internal class Constants
    {
        #region Logging

        internal const string DefaultEscapeCharacter = "\n\n";
        internal const string MilliVolt = " mV";

        internal const string EnvironmentalStarted = "Environmental Test Started";
        internal const string EnvironmentalFinished = "Environmental Test Finished";
        internal const string ClosingOutputsStarted = "Closing Outputs Started";
        internal const string ClosingOutputsFinished = "Closing Outputs Finished";
        internal const string StartProcessStarted = "Start Process has been Started";
        internal const string StartProcessCompleted = "Start Process has been Completed";
        internal const string ContinousRead = "ContinousRead";
        internal const string MappingRead = "Mapping Read";

        internal const string Opened = "Opened";
        internal const string Sent = "Sent";
        internal const string Closed = "Closed";
        internal const string WiperClosed = "WiperClosed";
        internal const string LoopBackVerified = "Verified";
        internal const string Transmitted = "transmitted";
        internal const string PEPSSentRSSIMesaage = "SentRSSIMesaage";
        internal const string SendDiagData = "SendDiagData";
        internal const string SendADCData = "SendADCData";
        internal const string ReadADCData = "ReadADCData";
        internal const string SendCurrentData = "SendCurrentData";
        internal const string ReadCurrentValue = "ReadCurrentData";
        internal const string ReadDiagData = "ReadDiagData";
        internal const string Unexpected = "UNEXPECTED";
        internal const string Response = "Response";
        internal const string ContinuousReadResponse = "ContinuousReadResponse";
        internal const string MappingMismatch = "MappingMismatch";
        internal const string WiperSet = "WIPER_SET";
        internal const string Enabled = "Enabled";
        internal const string Disabled = "Disabled";
        internal const string Measurement = "Measurement";
        internal const string KeyfobID = "Keyfob ID";
        internal const string Doorlock = "Doorlock";
        internal const string Loopback = "Loopback";
        internal const string LoopbackDataSent = "LOOPBACK_DATA_SENT";
        internal const string FS26IC = "FS26_IC";
        internal const string Off = "OFF";
        internal const string On = "ON";
        internal const string Invalid = "INVALID";
        internal const string OutsideOfLimit = " Outside Of Limit";

        #endregion

        #region Peps

        internal const string PEPS_Get_RSSI_Measurement = "GET_RSSI_Measurement";
        internal const string PEPS_Get_Key_List = "Get_Key_List";
        internal const string PEPS_Immobilizer = "Immobilizer";
        internal const string PEPS_Temperature_Measurement = "Temperature_Measurement";
        internal const string PEPS_Read_Keyfob = "Read_Keyfob";
        internal const string PEPS_Door_Cap_Sensor = "Door_Cap_Sensor";
        internal const string PEPS = "PEPS";

        #endregion

        #region Form Names

        internal const string Form_Main = "FormMain";
        internal const string Form_Uds_Add = "FormUdsAdd";
        internal const string Form_Uds = "FormUds";
        internal const string Form_Transmit_Multi = "FormTransmitMulti";
        internal const string Form_Transmit = "FormTransmit";
        internal const string Form_Trace_Popup = "FormTracePopup";
        internal const string Form_Splash_Screen = "FormSplashScreen";
        internal const string Form_Receive = "FormReceive";
        internal const string Form_Options = "FormOptions";
        internal const string Form_Message_Addition = "FormMessageAddition";
        internal const string Form_About = "FormAbout";
        internal const string Form_Monitor_Env_Input = "FormMonitorEnvInput";
        internal const string Form_Monitor_Env_Output = "FormMonitorEnvOutput";
        internal const string Form_Monitor_Generic_Input = "FormMonitorGenericInput";
        internal const string Form_Monitor_Generic_Output = "FormMonitorGenericOutput";
        internal const string Form_Environmental_Test = "FormEnvironmentalTest";

        #endregion
    }
}
