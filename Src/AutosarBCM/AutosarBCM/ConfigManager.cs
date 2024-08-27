using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Xml.Serialization;

namespace AutosarBCM.Config
{
    /// <summary>
    /// Manages the configuration settings for the monitoring system.
    /// </summary>
    internal class MonitorConfigManager
    {
        #region Properties

        /// <summary>
        /// Represents the file path of the configuration file used by the MonitorConfigManager.
        /// </summary>
        private static string ConfigFile;

        /// <summary>
        /// Instance
        /// </summary>
        private static MonitorConfigManager instance;

        /// <summary>
        /// Configuration variable
        /// </summary>
        private AutosarBcmConfiguration configuration;

        /// <summary>
        /// CIToolkitConfiguration configuration
        /// </summary>
        public AutosarBcmConfiguration Configuration => configuration;

        #endregion

        #region Constructor

        /// <summary>
        /// The constructor
        /// </summary>
        protected MonitorConfigManager()
        {
            configuration = GetConfigData(ConfigFile);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Create instance of the class
        /// </summary>
        /// <returns></returns>
        public static MonitorConfigManager GetConfig(string filePath)
        {
            ConfigFile = filePath;
            instance = new MonitorConfigManager();
            return instance;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Gets the Monitor configuration details from xml file
        /// </summary>
        /// <param name="confFile">Configuration file path</param>
        /// <returns>Configuration</returns>
        private static AutosarBcmConfiguration GetConfigData(string confFile)
        {
            try
            {
                using (var fs = new FileStream(confFile, FileMode.Open))
                {
                    var xs = new XmlSerializer(typeof(AutosarBcmConfiguration));
                    var config = xs.Deserialize(fs) as AutosarBcmConfiguration;
                    return config;
                }
            }
            catch (Exception ex)
            {
                Helper.ShowErrorMessageBox(ex);
                return null;
            }
        }

        #endregion
    }

    /// <summary>
    /// Root configuration class
    /// </summary>
    public class AutosarBcmConfiguration 
    {
        #region Properties

        /// <summary>
        /// Gets or sets the common configuration settings for the application.
        /// </summary>
        public CommonConfig CommonConfig { get; set; }
        /// <summary>
        /// Gets or sets the generic monitor configuration
        /// </summary>
        public GenericMonitorConfiguration GenericMonitorConfiguration { get; set; }
        /// <summary>
        /// Gets or sets the environmental monitor configuration
        /// </summary>
        public EnvironmentalMonitorConfiguration EnvironmentalMonitorConfiguration { get; set; }

        #endregion
    }

    /// <summary>
    /// Environmental monitor configuration class
    /// </summary>
    public class EnvironmentalMonitorConfiguration
    {
        #region Properties

        /// <summary>
        /// Gets or sets the mapping section
        /// </summary>
        public MappingSection MappingSection { get; set; }

        /// <summary>
        /// Gets or sets the input section
        /// </summary>
        public Section InputSection { get; set; }

        /// <summary>
        /// Gets or sets the output section
        /// </summary>
        public Section OutputSection { get; set; }

        #endregion
    }

    /// <summary>
    /// Generic monitor configuration class
    /// </summary>
    public class GenericMonitorConfiguration
    {
        #region Properties

        /// <summary>
        /// Gets or sets the input section
        /// </summary>
        public Section InputSection { get; set; }

        /// <summary>
        /// Gets or sets the output section
        /// </summary>
        public Section OutputSection { get; set; }

        #endregion
    }

    /// <summary>
    /// Section class
    /// </summary>
    public class Section
    {
        #region Properties

        /// <summary>
        /// Gets or sets the common configuration settings for the application.
        /// </summary>
        public CommonConfig CommonConfig { get; set; }

        /// <summary>
        /// Gets or sets the groups
        /// </summary>
        public List<Group> Groups { get; set; }

        /// <summary>
        /// Gets or sets the test cycles
        /// </summary>
        public List<Cycle> Cycles { get; set; }

        /// <summary>
        /// The constructor
        /// </summary>
        public Section()
        {
            Groups = new List<Group>();
            Cycles = new List<Cycle>();
        }

        #endregion
    }

    /// <summary>
    /// Monitor group class
    /// </summary>
    public class Group
    {
        #region Properties

        /// <summary>
        /// Group name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// List of subgroups
        /// </summary>
        [XmlArrayItem(ElementName = "Group")]
        public List<Group> SubGroups { get; set; }
        /// <summary>
        /// List of monitor input items
        /// </summary>
        [XmlArrayItem(ElementName = "Item")]
        public List<InputMonitorItem> InputItemList { get; set; }
        /// <summary>
        /// List of monitor output items
        /// </summary>
        [XmlArrayItem(ElementName = "Item")]
        public List<OutputMonitorItem> OutputItemList { get; set; }

        #endregion
    }

    /// <summary>
    /// Base monitor item class
    /// </summary>
    public class MonitorItem
    {
        #region Properties

        /// <summary>
        /// Gets or sets the message id
        /// </summary>
        public string MessageID { get; set; }

        /// <summary>
        /// Gets or sets the name of the message.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the lower limit of the received data.
        /// </summary>
        public int LowerLimit { get; set; }
        /// <summary>
        /// Gets or sets the upper limit of the received data.
        /// </summary>
        public int UpperLimit { get; set; }
        /// <summary>
        /// Gets or sets the coefficient value for the resistive message.
        /// </summary>
        public double Coefficient { get; set; }
        /// <summary>
        /// Type of the monitor item
        /// </summary>
        public string ItemType { get; set; }

        /// <summary>
        /// Transmits the message with the specified common message ID.
        /// </summary>
        /// <param name="commonMessageID">The common message ID to transmit.</param>
        /// <param name="pause">Optional. The pause duration in milliseconds before transmitting the message.</param>
        /// <param name="logMessage">Optional. The log message to record when transmitting the message.</param>
        public virtual void Transmit(int? pause = null,string logMessage = "transmitted") { }

        #endregion
    }

    /// <summary>
    /// Configuration of monitor item
    /// </summary>
    public class InputMonitorItem : MonitorItem
    {
        #region Properties

        /// <summary>
        /// Gets the message ID or uses the default message ID from the common configuration if it's not set.
        /// </summary>
        public string MessageIdOrDefault => string.IsNullOrWhiteSpace(MessageID) ? FormMain.Configuration.GenericMonitorConfiguration.InputSection.CommonConfig.MessageID : MessageID;
        /// <summary>
        /// Gets or sets the register group of item.
        /// </summary>
        public string RegisterGroup { get; set; }
        /// <summary>
        /// Gets or sets the register address of item.
        /// </summary>
        public string RegisterAddress { get; set; }
        /// <summary>
        /// Gets or sets the data of the message.
        /// </summary>
        [XmlElement(DataType = "hexBinary")]
        public byte[] Data { get; set; }
        /// <summary>
        /// Gets or sets the length of the data.
        /// </summary>
        public int DataLength { get; set; }
        /// <summary>
        /// Gets or sets the offset of the data.
        /// </summary>
        public int DataOffset { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Transmits the data
        /// </summary>
        /// <param name="commonMessageID"></param>
        /// <param name="pause"></param>
        public override void Transmit(int? pause = null, string logMessage = Constants.Transmitted)
        {
            ConnectionUtil.TransmitData(uint.Parse(MessageIdOrDefault, NumberStyles.HexNumber), Data);

            Helper.WriteCycleMessageToLogFile(Name, ItemType, logMessage);

            if (pause.HasValue)
                Thread.Sleep(pause.Value);
        }

        #endregion
    }

    /// <summary>
    /// Output monitor item class
    /// </summary>
    public class OutputMonitorItem : MonitorItem
    {
        #region Properties

        public string MessageIdOrDefault => string.IsNullOrWhiteSpace(MessageID) ? FormMain.Configuration.GenericMonitorConfiguration.OutputSection.CommonConfig.MessageID : MessageID;
        /// <summary>
        /// Risk level of digital item
        /// </summary>
        [XmlAttribute("riskLevel")]
        public string RiskLevel { get; set; }
        /// <summary>
        /// Type of PWM set values
        /// </summary>
        [XmlAttribute("pwmTag")]
        public string PwmTag { get; set; }
        /// <summary>
        /// Revert time for the output
        /// </summary>
        public int RevertTime { get; set; }
        /// <summary>
        /// Gets or sets the data of the message.
        /// </summary>
        [XmlElement(DataType = "hexBinary")]
        public byte[] ReadADCData { get; set; }
        /// <summary>
        /// Gets or sets the data of the message.
        /// </summary>
        [XmlElement(DataType = "hexBinary")]
        public byte[] ReadCurrentData { get; set; }
        /// <summary>
        /// Gets or sets the data of the message.
        /// </summary>
        [XmlElement(DataType = "hexBinary")]
        public byte[] ReadDiagData { get; set; }
        /// <summary>
        /// Gets or sets the data of the message.
        /// </summary>
        [XmlElement(DataType = "hexBinary")]
        public byte[] SetPWMData { get; set; }
        /// <summary>
        /// Gets or sets the data of the message.
        /// </summary>
        [XmlElement(DataType = "hexBinary")]
        public byte[] OpenData { get; set; }
        /// <summary>
        /// Gets or sets the data of the message.
        /// </summary>
        [XmlElement(DataType = "hexBinary")]
        public byte[] CloseData { get; set; }
        /// <summary>
        /// Gets or sets the data of the message.
        /// </summary>
        [XmlElement(DataType = "hexBinary")]
        public byte[] SendData { get; set; }
        /// <summary>
        /// Gets or sets the data of the message.
        /// </summary>
        [XmlElement(DataType = "hexBinary")]
        public byte[] PEPSData { get; set; }
        /// <summary>
        /// Gets or sets the PowerMirror instance.
        /// </summary>
        public PowerMirror PowerMirror { get; set; }
        /// <summary>
        /// Gets or sets the Loopback instance.
        /// </summary>
        public Loopback Loopback { get; set; }
        /// <summary>
        /// Gets or sets the WiperCase instance.
        /// </summary>
        public WiperCase WiperCase { get; set; }
        /// <summary>
        /// Gets or sets the Sunroof instance.
        /// </summary>
        public Sunroof Sunroof { get; set; }
        /// <summary>
        /// Gets or sets the PowerWindow instance.
        /// </summary>
        public PowerWindow PowerWindow { get; set; }
        /// <summary>
        /// Gets or sets the DoorControl instance.
        /// </summary>
        public DoorControl DoorControl { get; set; }

        /// <summary>
        /// Gets or sets the EEProm instance.
        /// </summary>
        public EEProm EEProm { get; set; }
        /// <summary>
        /// Gets or sets the SentMessage instance.
        /// </summary>
        public SentMessage SentMessage { get; set; }
        #endregion

        #region Methods

        /// <summary>
        /// Sends open command for digital outputs
        /// </summary>
        /// <param name="pwmDuty">The byte duty data for PWM Open.</param>
        /// <param name="pwmFreq">The short freq data for PWM Open.</param>
        public void Open(byte pwmDuty,short pwmFreq)
        {
            if (OpenData?.Length > 0)
            {
                ConnectionUtil.TransmitData(uint.Parse(MessageID, NumberStyles.HexNumber), OpenData);
                Helper.WriteCycleMessageToLogFile(Name, ItemType, Constants.Opened);
            }
            else if (SetPWMData?.Length > 0)
            {
                byte[] pwmMsg = GetPWMMessage(pwmDuty, pwmFreq);
                ConnectionUtil.TransmitData(uint.Parse(MessageID, NumberStyles.HexNumber), pwmMsg);
                Helper.WriteCycleMessageToLogFile(Name, ItemType, Constants.Opened);
            }
            else if (Loopback != null)
            {
                ConnectionUtil.TransmitData(uint.Parse(MessageID, NumberStyles.HexNumber), Loopback.Pair1Data);
                Helper.WriteCycleMessageToLogFile(Name, ItemType, Constants.Sent);
            }
            else if (SendData?.Length > 0) 
            {
                ConnectionUtil.TransmitData(uint.Parse(MessageID, NumberStyles.HexNumber), SendData);
                Helper.WriteCycleMessageToLogFile(Name, ItemType, Constants.Sent);
            }
        }

        /// <summary>
        /// Sends close command for digital outputs
        /// </summary>
        /// <param name="pwmDuty">The byte duty data for PWM Close.</param>
        /// <param name="pwmFreq">The short freq data for PWM Close.</param>
        public void Close(byte pwmDuty, short pwmFreq)
        {
            if (CloseData?.Length > 0)
            {
                ConnectionUtil.TransmitData(uint.Parse(MessageID, NumberStyles.HexNumber), CloseData);
                Helper.WriteCycleMessageToLogFile(Name, ItemType, Constants.Closed);
            }
            else if (SetPWMData?.Length > 0)
            {
                byte[] pwmMsg = GetPWMMessage(pwmDuty, pwmFreq);
                ConnectionUtil.TransmitData(uint.Parse(MessageID, NumberStyles.HexNumber), pwmMsg);
                Helper.WriteCycleMessageToLogFile(Name, ItemType, Constants.Closed);
            }if (ItemType == "Wiper" && Name.EndsWith("2_Stop"))
            {
                ConnectionUtil.TransmitData(uint.Parse(MessageID, NumberStyles.HexNumber), OpenData);
                Helper.WriteCycleMessageToLogFile(Name, ItemType, Constants.WiperClosed);
            }
        }

        internal byte[] GetPWMMessage(byte pwmDuty, short pwmFreq)
        {
            if (SetPWMData == null || SetPWMData.Length == 0) return SetPWMData;

            var msg = new byte[SetPWMData.Length];
            Array.Copy(SetPWMData, msg, msg.Length);
            if (PwmTag == "freq_duty")
            {
                var freq = BitConverter.GetBytes(pwmFreq);
                msg[5] = freq[1];
                msg[6] = freq[0];
            }
            msg[msg.Length - 1] = pwmDuty;
            return msg;
        }

        /// <summary>
        /// Sends a loopback verification message and logs the action.
        /// </summary>
        public void SendLoopbackVerification()
        {
            ConnectionUtil.TransmitData(uint.Parse(MessageID, NumberStyles.HexNumber), Loopback.Verification);
            Helper.WriteCycleMessageToLogFile(Name,ItemType, Constants.LoopBackVerified);
        }

        /// <summary>
        /// Transmits data and logs the action.
        /// </summary>
        /// <param name="data">The data to transmit.</param>
        /// <param name="commonMessageID">Optional common message ID.</param>
        /// <param name="pause">Optional pause duration in milliseconds.</param>
        /// <param name="logMessage">Optional log message.</param>
        public void Transmit(byte[] data, string commonMessageID = "", int? pause = null, string logMessage = Constants.Transmitted)
        {
            var messageId = string.IsNullOrEmpty(MessageID) ? commonMessageID : MessageID;
            ConnectionUtil.TransmitData(uint.Parse(messageId, NumberStyles.HexNumber), data);

            Helper.WriteCycleMessageToLogFile(Name, ItemType, logMessage);

            if (pause.HasValue)
                Thread.Sleep(pause.Value);
        }

        #endregion
    }

    /// <summary>
    /// Common config class
    /// </summary>
    public class CommonConfig
    {
        #region Properties

        public int Pwm { get; set; }
        /// <summary>
        /// Gets or sets the common message id
        /// </summary>
        public string MessageID { get; set; }
        /// <summary>
        /// Gets or sets the time interval for generic test
        /// </summary>
        public int ReadInterval { get; set; }
        /// <summary>
        /// Gets or sets the time interval for generic test
        /// </summary>
        public int CycleTime { get; set; }
        /// <summary>
        /// gets or sets cycle time for environmental tests
        /// </summary>
        public int TxInterval { get; set; }
        /// <summary>
        /// Revert trail count for generix outputs
        /// </summary>
        public int RevertTrialLimit { get; set; }
        /// <summary>
        /// Gets or sets timer tick delay for environmental tests
        /// </summary>
        public int RevertTime { get; set; }
        /// <summary>
        /// Gets or sets the register group offset
        /// </summary>
        public int InputRegisterGroupOffset { get; set; }
        /// <summary>
        /// Gets or sets the register group length
        /// </summary>
        public int InputRegisterGroupLength { get; set; }
        /// <summary>
        /// Gets or sets the coefficient
        /// </summary>
        public double InputCoefficient { get; set; }
        /// <summary>
        /// Gets or sets the lower limit of the received data.
        /// </summary>
        public int InputLowerLimit { get; set; }
        /// <summary>
        /// Gets or sets the upper limit of the received data.
        /// </summary>
        public int InputUpperLimit { get; set; }
        /// <summary>
        /// Sets default value of duty
        /// </summary>
        public int DefaultDuty { get; set; }
        /// <summary>
        /// Sets default value of frequency
        /// </summary>
        public int DefaultFrequency { get; set; }
        /// <summary>
        /// Sets the limit of warinig Messages
        /// </summary>
        public string DefaultRiskLimit { get; set; }
        /// <summary>
        /// Gets or sets the message of SetStatus service
        /// </summary>
        [XmlElement(DataType = "hexBinary")]
        public byte[] SetStatus { get; set; }
        /// <summary>
        /// Gets or sets the start index of the cycles
        /// </summary>
        public int StartCycleIndex { get; set; }
        /// <summary>
        /// Gets or sets the end index of the cycles
        /// </summary>
        public int EndCycleIndex { get; set; }
        /// Gets or sets the message of PWM Open Duty value
        /// </summary>
        public byte PWMDutyOpenValue { get; set; }
        /// <summary>
        /// Gets or sets the message of PWM Close Duty value
        /// </summary>
        public byte PWMDutyCloseValue { get; set; }
        /// <summary>
        /// Gets or sets the message of PWM Open Frequency value
        /// </summary>
        public short PWMFreqOpenValue { get; set; }
        /// <summary>
        /// Gets or sets the message of PWM Close Frequency value
        /// </summary>
        public short PWMFreqCloseValue { get; set; }
        /// <summary>
        /// Gets or sets the message of PWM Close Frequency value
        /// </summary>
        public string TesterPresentMessage { get; set; }
        /// <summary>
        /// Gets or sets the message of EMCLifecycle
        /// </summary>
        [XmlElement(DataType = "hexBinary")]
        public byte[] EMCLifecycle { get; set; }

        #endregion
    }
    //public class Controls
    //{
    //    public string Name { get; set; }

    //    public string Address { get; set; }
    //    public string Group { get; set; }
    //    public string Services { get; set; }
    //}


        /// <summary>
        /// Tx Response class
        /// </summary>
        public class Response
    {
        #region Properties

        /// <summary>
        /// Gets or sets the parsed message id
        /// </summary>
        public short MessageId { get; set; }
        /// <summary>
        /// Gets or sets the data length of the parsed messsage
        /// </summary>
        public byte DataLength { get; set; }
        /// <summary>
        /// Gets or sets the PCI of the parsed messsage
        /// </summary>
        public byte PCI { get; set; }
        /// <summary>
        /// Gets or sets the SID of the parsed messsage
        /// </summary>
        public byte SID { get; set; }
        /// <summary>
        /// Gets or sets the register group of the parsed messsage
        /// </summary>
        public short RegisterGroup { get; set; }
        /// <summary>
        /// Gets or sets the register address of the parsed messsage
        /// </summary>
        public byte RegisterAddress { get; set; }
        /// <summary>
        /// Gets or sets the raw data of the parsed messsage
        /// </summary>
        public short ResponseData { get; set; }
        /// <summary>
        /// Gets or sets the raw data
        /// </summary>
        public short ResponseData2 { get; set; }
        /// <summary>
        /// Gets or sets the raw data
        /// </summary>
        public short ResponseData3 { get; set; }
        /// <summary>
        /// Gets or sets the raw data
        /// </summary>
        public float ResponseData32 { get; set; }
        /// <summary>
        /// Gets or sets the raw data
        /// </summary>
        public byte[] RawData { get; set; }

        #endregion
    }

    /// <summary>
    /// Represents a generic response received from a device.
    /// </summary>
    public class GenericResponse : Response
    {
        #region Constructor

        /// <summary>
        /// The constructor
        /// </summary>
        /// <param name="receivedData">Rx data</param>
        /// <param name="offset">Data read offset</param>
        /// <param name="length">Data read length</param>
        public GenericResponse(byte[] receivedData, int offset, int length)
        {
            if (receivedData.Length == 8)
                receivedData = receivedData.Prepend((byte)0).Prepend((byte)0).ToArray();

            MessageId = (short)Helper.GetValueOfPrimitive(receivedData, 0, 2);
            //DataLength = (byte)Helper.GetValueOfPrimitive(receivedData, ? , 1),
            PCI = (byte)Helper.GetValueOfPrimitive(receivedData, 2, 1);
            SID = (byte)Helper.GetValueOfPrimitive(receivedData, 3, 1);
            RegisterGroup = (short)Helper.GetValueOfPrimitive(receivedData, offset, length);
            RegisterAddress = (byte)Helper.GetValueOfPrimitive(receivedData, 6, 1);
            ResponseData = (short)Helper.GetValueOfPrimitive(receivedData, 7, 2);
            RawData = receivedData;
        }

        /// <summary>
        /// Returns a formatted result of the response based on the group of the register.
        /// </summary>
        /// <param name="input">The input monitor item associated with the response.</param>
        /// <returns>A human-readable string representing the response.</returns>
        public string FormattedResult(InputMonitorItem input = null)
        {
            if (RegisterGroup == 0x6102)
            {
                if (ResponseData == 0) return "OFF";
                else if (ResponseData == 1) return "ON";
                else if (ResponseData == 2) return "NOK";
            }
            else if (RegisterGroup == 0x6103)
            {
                if (ResponseData == 0) return "STD_LOW";
                else if (ResponseData == 1) return "STD_HIGH";
            }
            else if (RegisterGroup == 0x6106)
                return $"{ResponseData * input.Coefficient}";
            else if (RegisterGroup == 0x6104 || RegisterGroup == 0x6105)
            {
                if (ResponseData == 0) return "E_OK";
                else if (ResponseData == 1) return "E_NOT_OK";
            }
            else if (RegisterGroup == 0x6107 || RegisterGroup == 0x6112)
            {
                if (ResponseData == 0) return "E_OK";
                else if (ResponseData == 1) return "E_NOT_OK";
            }
            else if (RegisterGroup == 0x6114)
                return ResponseData.ToString();
            else if (RegisterGroup == 0x6116)
                return $"0x{RawData[6]:X} - {ResponseData}";

            return "UNIDENTIFIED";
        }

        #endregion
    }

    /// <summary>
    /// Represents a response received from a Passive Entry Passive Start (PEPS) device.
    /// </summary>
    public class PEPSResponse : Response
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the PEPSResponse class with the received data.
        /// </summary>
        /// <param name="receivedData">The byte array containing the received data.</param>
        public PEPSResponse(byte[] receivedData)
        {
            MessageId = BitConverter.ToInt16(receivedData, 0);
            PCI = receivedData[2];
            SID = receivedData[3];
            RegisterGroup = BitConverter.ToInt16(new byte[] { receivedData[4], 0 }, 0);
            RegisterAddress = receivedData[4];

            ResponseData = BitConverter.ToInt16(receivedData.Skip(5).Take(2).Reverse().ToArray(), 0);
            ResponseData2 = BitConverter.ToInt16(receivedData.Skip(7).Take(2).Reverse().ToArray(), 0);
            ResponseData32 = BitConverter.ToSingle(receivedData.Skip(5).Take(4).Reverse().ToArray(), 0);

            RawData = receivedData;
        }

        #endregion
    }

    /// <summary>
    /// Represents a response received from an Electrically Erasable Programmable Read-Only Memory (EEPROM) device.
    /// </summary>
    public class EEPromResponse : Response
    {
        #region Properties

        /// <summary>
        /// Gets or sets the EEPROM address associated with the response.
        /// </summary>
        public short EEPromAddress { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new EEPromResponse object from the received data.
        /// </summary>
        /// <param name="receivedData">The byte array containing the received data.</param>
        /// <returns>A new EEPromResponse object.</returns>
        public static EEPromResponse ReadResponse(byte[] receivedData)
        {
            return new EEPromResponse {
                MessageId = (short)Helper.GetValueOfPrimitive(receivedData, 0, 2),
                RegisterGroup = (short)Helper.GetValueOfPrimitive(receivedData, 2, 2),
                EEPromAddress = (short)Helper.GetValueOfPrimitive(receivedData, 5, 2),
                ResponseData = (byte)Helper.GetValueOfPrimitive(receivedData, 7, 1),
                ResponseData2 = (byte)Helper.GetValueOfPrimitive(receivedData, 8, 1),
                ResponseData3 = (byte)Helper.GetValueOfPrimitive(receivedData, 9, 1),
                RegisterAddress = 0
            };
        }

        #endregion

        #region Methods

        /// <summary>
        /// Creates an EEPROM write response object based on the received data.
        /// </summary>
        /// <param name="receivedData">The received data containing the response information.</param>
        /// <returns>An EEPROM write response object.</returns>
        public static EEPromResponse WriteResponse(byte[] receivedData)
        {
            return new EEPromResponse {
                MessageId = (short)Helper.GetValueOfPrimitive(receivedData, 0, 2),
                RegisterGroup = (short)Helper.GetValueOfPrimitive(receivedData, 2, 2),
                ResponseData = (byte)Helper.GetValueOfPrimitive(receivedData, 6, 1),
                RegisterAddress = 0
            };
        }

        #endregion
    }

    /// <summary>
    /// Test Cycle
    /// </summary>
    public class Cycle 
    {
        public Cycle() { }
        public Cycle(Cycle cycle)
        {
            Name = cycle.Name;
            OpenAt = cycle.OpenAt;
            CloseAt = cycle.CloseAt;
            Functions = cycle.Functions;
        }
        #region Properties

        /// <summary>
        /// Name of cycle
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Cycle number of outputs to be opened
        /// </summary>
        public int OpenAt { get; set; }
        /// <summary>
        /// Cycle number of outputs to be closed
        /// </summary>
        public int CloseAt { get; set; }
        /// <summary>
        /// List of functions
        /// </summary>
        [XmlArrayItem("FuncName")]
        public List<string> Functions { get; set; }
        /// <summary>
        /// List of items
        /// </summary>
        public List<OutputMonitorItem> Items { get; set; }
        public List<OutputMonitorItem> CloseItems { get; set; } = new List<OutputMonitorItem>();
        public List<OutputMonitorItem> OpenItems { get; set; } = new List<OutputMonitorItem>();
        #endregion
    }

    /// <summary>
    /// Represents the control data for a power mirror, which is used to control the mirror's movement.
    /// </summary>
    public class PowerMirror
    {
        #region Properties

        /// <summary>
        /// Represents the data to set the mirror in the "open" position for moving it upwards.
        /// </summary>
        [XmlElement(DataType = "hexBinary")]
        public byte[] SetOpenUp { get; set; }
        /// <summary>
        /// Represents the data to set the mirror in the "open" position for moving it downwards.
        /// </summary>
        [XmlElement(DataType = "hexBinary")]
        public byte[] SetOpenDown { get; set; }
        /// <summary>
        /// Represents the data to set the mirror in the "open" position for moving it to the left.
        /// </summary>
        [XmlElement(DataType = "hexBinary")]
        public byte[] SetOpenLeft { get; set; }
        /// <summary>
        /// Represents the data to set the mirror in the "open" position for moving it to the right.
        /// </summary>
        [XmlElement(DataType = "hexBinary")]
        public byte[] SetOpenRight { get; set; }
        /// <summary>
        /// Represents the data to set the mirror in the "close" position for moving it upwards.
        /// </summary>
        [XmlElement(DataType = "hexBinary")]
        public byte[] SetCloseUp { get; set; }
        /// <summary>
        /// Represents the data to set the mirror in the "close" position for moving it downwards.
        /// </summary>
        [XmlElement(DataType = "hexBinary")]
        public byte[] SetCloseDown { get; set; }
        /// <summary>
        /// Represents the data to set the mirror in the "close" position for moving it to the left.
        /// </summary>
        [XmlElement(DataType = "hexBinary")]
        public byte[] SetCloseLeft { get; set; }
        /// <summary>
        /// Represents the data to set the mirror in the "close" position for moving it to the right.
        /// </summary>
        [XmlElement(DataType = "hexBinary")]
        public byte[] SetCloseRight { get; set; }
        /// <summary>
        /// Represents the data to read the mirror's position when moving it upwards.
        /// </summary>
        [XmlElement(DataType = "hexBinary")]
        public byte[] ReadUp { get; set; }
        /// <summary>
        /// Represents the data to read the mirror's position when moving it to the left or downwards.
        /// </summary>
        [XmlElement(DataType = "hexBinary")]
        public byte[] ReadLeftDown { get; set; }
        /// <summary>
        /// Represents the data to read the mirror's position when moving it to the right.
        /// </summary>
        [XmlElement(DataType = "hexBinary")]
        public byte[] ReadRight { get; set; }

        #endregion
    }

    /// <summary>
    /// Represents loopback control data used for testing and verification purposes.
    /// </summary>
    public class Loopback
    {
        #region Properties

        /// <summary>
        /// Gets or sets the name of the first pair of loopback data.
        /// </summary>
        public string Pair1Name { get; set; }
        /// <summary>
        /// Gets or sets the binary data for the first pair of loopback data.
        /// </summary>
        [XmlElement(DataType = "hexBinary")]
        public byte[] Pair1Data { get; set; }
        /// <summary>
        /// Gets or sets the name of the second pair of loopback data.
        /// </summary>
        public string Pair2Name { get; set; }
        /// <summary>
        /// Gets or sets the binary data for the second pair of loopback data.
        /// </summary>
        [XmlElement(DataType = "hexBinary")]
        public byte[] Pair2Data { get; set; }
        /// <summary>
        /// Gets or sets the binary data used for loopback verification.
        /// </summary>
        [XmlElement(DataType = "hexBinary")]
        public byte[] Verification { get; set; }

        #endregion
    }

    /// <summary>
    /// Represents a section in the configuration file that contains mapping information.
    /// </summary>
    public class MappingSection
    {
        #region Properties

        /// <summary>
        /// Gets or sets a list of connection mappings.
        /// </summary>
        public List<Mapping> ConnectionMappings { get; set; }
        /// <summary>
        /// Gets or sets a list of continuous read functions.
        /// </summary>
        [XmlArrayItem("Func")]
        public List<string> ContinousReadList { get; set; }

        #endregion
    }

    /// <summary>
    /// Mapping output-input class
    /// </summary>
    public class Mapping
    {
        #region Properties

        /// <summary>
        /// Output name
        /// </summary>
        public string OutputName { get; set; }
        /// <summary>
        /// Relevant Input Name
        /// </summary>
        public string InputName { get; set; }

        #endregion
    }

    /// <summary>
    /// Represents a set of commands for controlling the wiper.
    /// </summary>
    public class WiperCase
    {
        #region Properties

        /// <summary>
        /// Gets or sets the command to stop the wiper at a low position.
        /// </summary>
        [XmlElement(DataType = "hexBinary")]
        public byte[] StopLow { get; set; }

        /// <summary>
        /// Gets or sets the command to stop the wiper at a high position.
        /// </summary>
        [XmlElement(DataType = "hexBinary")]
        public byte[] StopHigh { get; set; }

        /// <summary>
        /// Gets or sets the command to set the wiper to a stop position.
        /// </summary>
        [XmlElement(DataType = "hexBinary")]
        public byte[] HighStop { get; set; }

        /// <summary>
        /// Gets or sets the command to set the wiper to a stop position.
        /// </summary>
        [XmlElement(DataType = "hexBinary")]
        public byte[] LowStop { get; set; }

        /// <summary>
        /// Gets or sets the command to set the wiper to a high position.
        /// </summary>
        [XmlElement(DataType = "hexBinary")]
        public byte[] LowHigh { get; set; }

        /// <summary>
        /// Gets or sets the command to set the wiper to a low position.
        /// </summary>
        [XmlElement(DataType = "hexBinary")]
        public byte[] HighLow { get; set; }

        #endregion
    }

    /// <summary>
    /// Represents a set of commands for controlling an open-close item (e.g., sunroof, power window).
    /// </summary>
    public class OpenCloseItem
    {
        #region Properties

        /// <summary>
        /// Gets or sets the command to enable opening the item.
        /// </summary>
        [XmlElement(DataType = "hexBinary")]
        public byte[] EnableOpenData { get; set; }

        /// <summary>
        /// Gets or sets the command to disable opening the item.
        /// </summary>
        [XmlElement(DataType = "hexBinary")]
        public byte[] DisableOpenData { get; set; }

        /// <summary>
        /// Gets or sets the command to enable closing the item.
        /// </summary>
        [XmlElement(DataType = "hexBinary")]
        public byte[] EnableCloseData { get; set; }

        /// <summary>
        /// Gets or sets the command to disable closing the item.
        /// </summary>
        [XmlElement(DataType = "hexBinary")]
        public byte[] DisableCloseData { get; set; }

        /// <summary>
        /// Gets or sets the command to read diagnostic data when the item is open.
        /// </summary>
        [XmlElement(DataType = "hexBinary")]
        public byte[] ReadOpenDiagData { get; set; }

        /// <summary>
        /// Gets or sets the command to read diagnostic data when the item is closed.
        /// </summary>
        [XmlElement(DataType = "hexBinary")]
        public byte[] ReadCloseDiagData { get; set; }

        #endregion
    }

    /// <summary>
    /// Represents a set of commands for controlling a power window.
    /// </summary>
    public class PowerWindow : OpenCloseItem
    {
        #region Properties

        /// <summary>
        /// Gets or sets the command to enable moving the window upwards.
        /// </summary>
        [XmlElement(DataType = "hexBinary")]
        public byte[] EnableUpData { get { return EnableOpenData; } set { EnableOpenData = value; } }

        /// <summary>
        /// Gets or sets the command to disable moving the window upwards.
        /// </summary>
        [XmlElement(DataType = "hexBinary")]
        public byte[] DisableUpData { get { return DisableOpenData; } set { DisableOpenData = value; } }

        /// <summary>
        /// Gets or sets the command to enable moving the window downwards.
        /// </summary>
        [XmlElement(DataType = "hexBinary")]
        public byte[] EnableDownData { get { return EnableCloseData; } set { EnableCloseData = value; } }

        /// <summary>
        /// Gets or sets the command to disable moving the window downwards.
        /// </summary>
        [XmlElement(DataType = "hexBinary")]
        public byte[] DisableDownData { get { return DisableCloseData; } set { DisableCloseData = value; } }

        /// <summary>
        /// Gets or sets the command to read diagnostic data when the window is moving up.
        /// </summary>
        [XmlElement(DataType = "hexBinary")]
        public byte[] ReadUpDiagData { get { return ReadOpenDiagData; } set { ReadOpenDiagData = value; } }

        /// <summary>
        /// Gets or sets the command to read diagnostic data when the window is moving down.
        /// </summary>
        [XmlElement(DataType = "hexBinary")]
        public byte[] ReadDownDiagData { get { return ReadCloseDiagData; } set { ReadCloseDiagData = value; } }

        #endregion
    }

    /// <summary>
    /// Represents a set of commands for controlling a sunroof.
    /// </summary>
    public class Sunroof : OpenCloseItem { }

    /// <summary>
    /// Represents a set of commands for controlling door locking and unlocking.
    /// </summary>
    public class DoorControl
    {
        #region Properties

        /// <summary>
        /// Gets or sets the diagnostic data for reading the door lock status.
        /// </summary>
        [XmlElement(DataType = "hexBinary")]
        public byte[] ReadDoorLockDiag { get; set; }

        /// <summary>
        /// Gets or sets the command to enable door locking.
        /// </summary>
        [XmlElement(DataType = "hexBinary")]
        public byte[] DoorLockEnable { get; set; }

        /// <summary>
        /// Gets or sets the time (in milliseconds) to revert the door locking state.
        /// </summary>
        public int DoorLockRevertTime { get; set; }

        /// <summary>
        /// Gets or sets the command to disable door locking.
        /// </summary>
        [XmlElement(DataType = "hexBinary")]
        public byte[] DoorLockDisable { get; set; }

        /// <summary>
        /// Gets or sets the diagnostic data for reading the door unlock status.
        /// </summary>
        [XmlElement(DataType = "hexBinary")]
        public byte[] ReadDoorUnlockDiag { get; set; }

        /// <summary>
        /// Gets or sets the command to enable door unlocking.
        /// </summary>
        [XmlElement(DataType = "hexBinary")]
        public byte[] DoorUnlockEnable { get; set; }

        /// <summary>
        /// Gets or sets the time (in milliseconds) to revert the door unlocking state.
        /// </summary>
        public int DoorUnlockRevertTime { get; set; }

        /// <summary>
        /// Gets or sets the command to disable door unlocking.
        /// </summary>
        [XmlElement(DataType = "hexBinary")]
        public byte[] DoorUnlockDisable { get; set; }

        #endregion
    }

    /// <summary>
    /// This class represents EEPROM data and is used for performing EEPROM-related operations.
    /// </summary>
    public class EEProm
    {
        #region Properties

        /// <summary>
        /// Gets or sets the data for writing to the EEPROM.
        /// </summary>
        [XmlElement(DataType = "hexBinary")]
        public byte[] WriteData { get; set; }

        /// <summary>
        /// Gets or sets the data for reading from the EEPROM.
        /// </summary>
        [XmlElement(DataType = "hexBinary")]
        public byte[] ReadData { get; set; }

        /// <summary>
        /// Gets or sets the response data after reading from the EEPROM.
        /// </summary>
        [XmlElement(DataType = "hexBinary")]
        public byte[] ReadResponse { get; set; }

        /// <summary>
        /// Gets or sets the response data after writing to the EEPROM.
        /// </summary>
        [XmlElement(DataType = "hexBinary")]
        public byte[] WriteResponse { get; set; }

        /// <summary>
        /// Gets or sets the upper address limit for EEPROM operations.
        /// </summary>
        public string UpperAddressLimit { get; set; }

        /// <summary>
        /// Gets or sets the lower address limit for EEPROM operations.
        /// </summary>
        public string LowerAddressLimit { get; set; }

        #endregion
    }

    /// <summary>
    /// This class represents sent data and is used for performing message log and counter operations.
    /// </summary>
    public class SentMessage
    {
        public string Id { get; set; }
        public string itemType { get; set; }
        public string itemName { get; set; }
        public string operation { get; set; }
        public DateTime timestamp { get; set; }
    }
}
