using AutosarBCM.Config;
using AutosarBCM.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace AutosarBCM.Core
{
    public class SessionInfo
    {
        public byte ID { get; set; }
        public string Name { get; set; }
        public List<byte> AvailableServices { get; set; }
    }

    public class ServiceInfo
    {
        public byte RequestID { get; set; }
        public byte ResponseID { get; set; }
        public string Name { get; set; }
        public int ResponseIndex { get; set; }
        public List<byte> Sessions { get; set; }
    }

    public class ControlInfo
    {
        public ushort Address { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Group { get; set; }
        public List<byte> Services { get; set; }
        public List<byte> SessionActiveException { get; set; }
        public List<byte> SessionInactiveException { get; set; }
        public List<ResponseInfo> Responses { get; set; }

        public void Transmit(ServiceName serviceName, byte[] data = null)
        {
            if (serviceName == ServiceName.ReadDataByIdentifier) 
                new ReadDataByIdenService().Transmit(this);
            else if (serviceName == ServiceName.InputOutputControlByIdentifier) 
                new IOControlByIdentifierService().Transmit(this, data);
        }

        internal IEnumerable<PayloadInfo> GetPayloads(byte serviceID)
        {
            return Responses.Where(a => a.ServiceID == serviceID).First()?.Payloads;
        }
    }

    public class PayloadInfo
    {
        public int Index { get; set; }
        public string Name { get; set; }
        public string TypeName { get; set; }
        public int Length { get; set; }
        public List<PayloadValue> Values { get; set; }

        internal PayloadValue GetPayloadValue(byte value)
        {
            return Values?.FirstOrDefault(v => v.Value == value);
        }
    }

    public class PayloadValue
    {
        public byte Value { get; set; }
        public string Color { get; set; }
        public string FormattedValue { get; set; }
    }

    public class ResponseInfo
    {
        public byte ServiceID { get; set; }
        public List<PayloadInfo> Payloads { get; set; }
    }

    public class ConfigurationInfo
    {
        public Dictionary<string, string> Settings { get; set; }
        public List<ServiceInfo> Services { get; set; }
        public List<SessionInfo> Sessions { get; set; }
        public List<ControlInfo> Controls { get; set; }
        public List<PayloadInfo> Payloads { get; set; }
        public EnvironmentalTest EnvironmentalTest { get; set; }

        internal static ConfigurationInfo Parse(string filePath)
        {
            XDocument doc = XDocument.Load(filePath);

            var settings = doc.Descendants("Settings").Descendants("Entry")
                .ToDictionary(
                    k => k.Attribute("key").Value,
                    v => v.Attribute("value").Value);

            var services = doc.Descendants("Service")
                .Select(s => new ServiceInfo
                {
                    RequestID = s.Attribute("requestID") != null ? Convert.ToByte(s.Attribute("requestID").Value, 16) : (byte)0,
                    ResponseID = s.Attribute("responseID") != null ? Convert.ToByte(s.Attribute("responseID").Value, 16) : (byte)0,
                    Name = s.Element("Name").Value,
                    ResponseIndex = s.Element("ResponseIndex") != null ? int.Parse(s.Element("ResponseIndex").Value) : 0,
                    Sessions = s.Element("Sessions") != null ? s.Element("Sessions").Value.Split(';').Select(byte.Parse).ToList() : new List<byte>()
                })
                .ToList();

            var sessions = doc.Descendants("Sessions").Descendants("Session")
                .Select(s => new SessionInfo
                {
                    ID = Convert.ToByte(s.Element("ID").Value, 16),
                    Name = s.Element("Name").Value,
                    AvailableServices = s.Element("AvailableServices").Value != "" ? s.Element("AvailableServices").Value.Split(';').Select(x => byte.Parse(x, System.Globalization.NumberStyles.HexNumber)).ToList() : new List<byte>(),
                })
                .ToList();

            var controls = doc.Descendants("Control")
                .Select(c => new ControlInfo
                {
                    Address = Convert.ToUInt16(c.Element("Address").Value, 16),
                    Name = c.Element("Name").Value,
                    Type = c.Element("Type").Value,
                    Group = c.Element("Group")?.Value,
                    Services = c.Element("Services").Value.Split(';').Select(x => byte.Parse(x, System.Globalization.NumberStyles.HexNumber)).ToList(),
                    SessionActiveException = c.Element("SessionActiveException") != null && c.Element("SessionActiveException").Value != "" ? c.Element("SessionActiveException").Value.Split(';').Select(x => byte.Parse(x, System.Globalization.NumberStyles.HexNumber)).ToList() : new List<byte>(),
                    SessionInactiveException = c.Element("SessionInactiveException") != null && c.Element("SessionInactiveException").Value != "" ? c.Element("SessionInactiveException").Value.Split(';').Select(x => byte.Parse(x, System.Globalization.NumberStyles.HexNumber)).ToList() : new List<byte>(),

                    Responses = c.Element("Responses") != null ?
                        c.Element("Responses").Elements("Response").Select(x =>
                            new ResponseInfo
                            {
                                ServiceID = Convert.ToByte(x.Attribute("serviceId").Value, 16),
                                Payloads = x.Elements("Payload") != null ? x.Elements("Payload").Select((y, i) => new PayloadInfo
                                {
                                    Name = y.Attribute("name").Value,
                                    TypeName = y.Value
                                }).ToList() : new List<PayloadInfo>(),
                            }).ToList() : new List<ResponseInfo>(),
                }).ToList();

            var payloads = doc.Descendants("Payloads").Descendants("Payload")
                .Select(s => new PayloadInfo
                {
                    Length = int.Parse(s.Attribute("length").Value),
                    TypeName = s.Attribute("typeName").Value,
                    Values = s.Elements("Value")
                        .Select(x => new PayloadValue
                        {
                            Value = Convert.ToByte(x.Attribute("value").Value, 16),
                            Color = x.Attribute("color")?.Value ?? null,
                            FormattedValue = x.Value
                        }).ToList(),
                })
                .ToList();


            #region Environmental Test
            
            var environmentalTest = doc.Descendants("EnvironmentalTest")
                .Select(t => new EnvironmentalTest
                {
                    EnvironmentalConfig = t.Descendants("EnvironmentalConfig")
                        .Select(c => new EnvironmentalConfig
                        {
                            CycleTime = int.Parse(c.Element("CycleTime").Value),
                            TxInterval = int.Parse(c.Element("TxInterval").Value),
                            StartCycleIndex = int.Parse(c.Element("StartCycleIndex").Value),
                            EndCycleIndex = int.Parse(c.Element("EndCycleIndex").Value),
                            PWMDutyOpenValue = byte.Parse(c.Element("PWMDutyOpenValue").Value),
                            PWMDutyCloseValue = byte.Parse(c.Element("PWMDutyCloseValue").Value),
                            PWMFreqOpenValue = byte.Parse(c.Element("PWMFreqOpenValue").Value),
                            PWMFreqCloseValue = byte.Parse(c.Element("PWMFreqCloseValue").Value),
                        }).First(),
                    ConnectionMappings= t.Element("ConnectionMappings").Elements("Mapping")
                        .Select(m => new Mapping
                        {
                            InputName= m.Element("InputName").Value,
                            OutputName= m.Element("OutputName").Value,
                        }).ToList(),
                    ContinousReadList = t.Element("ContinousReadList").Elements("Func")
                        .Select(f => f.Value).ToList(),
                    Cycles = t.Element("Cycles").Elements("Cycle")
                        .Select(c => new Cycle { 
                            Name= c.Element("Name").Value,
                            OpenAt = int.Parse(c.Element("OpenAt").Value),
                            CloseAt = int.Parse(c.Element("CloseAt").Value),
                            Functions = c.Element("Functions").Elements("FuncName")
                                .Select(f => f.Value).ToList(),
                        }).ToList()
                }).First();

            #endregion

            return new ConfigurationInfo
            {
                Settings = settings,
                Services = services,
                Sessions = sessions,
                Controls = controls,
                Payloads = payloads,
                EnvironmentalTest = environmentalTest
            };
        }

        internal ServiceInfo GetServiceByID(byte serviceID)
        {
            return Services.Where(x => x.ResponseID == serviceID).FirstOrDefault();
        }

        internal ControlInfo GetControlByAddress(ushort controlAddress)
        {
            return Controls.Where(x => x.Address == controlAddress).FirstOrDefault();
        }

        internal PayloadInfo GetPayloadInfoByType(string typeName)
        {
            return Payloads.FirstOrDefault(x => x.TypeName == typeName);
        }
    }

    public class ASContext
    {
        public static SessionInfo CurrentSession { get; set; }
        public static ConfigurationInfo Configuration { get; set; }

        public ASContext(string configFile)
        {
            if (configFile != null)
                Configuration = ConfigurationInfo.Parse(configFile);
        }
    }

    public class EnvironmentalTest
    {
        public EnvironmentalConfig EnvironmentalConfig { get; set; }
        /// <summary>
        /// Gets or sets a list of connection mappings.
        /// </summary>
        public List<Mapping> ConnectionMappings { get; set; }

        /// <summary>
        /// Gets or sets a list of continuous read functions.
        /// </summary>
        [XmlArrayItem("Item")]
        public List<string> ContinousReadList { get; set; }
        /// <summary>
        /// Gets or sets the test cycles
        /// </summary>
        public List<Cycle> Cycles { get; set; }
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
    /// Common config class
    /// </summary>
    public class EnvironmentalConfig
    {
        #region Properties

        /// <summary>
        /// Gets or sets the time interval for generic test
        /// </summary>
        public int CycleTime { get; set; }
        /// <summary>
        /// gets or sets cycle time for environmental tests
        /// </summary>
        public int TxInterval { get; set; }
        /// <summary>
        /// Gets or sets the start index of the cycles
        /// </summary>
        public int StartCycleIndex { get; set; }
        /// <summary>
        /// Gets or sets the end index of the cycles
        /// </summary>
        public int EndCycleIndex { get; set; }
        ///
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

        #endregion
    }

    /// <summary>
    /// Test Cycle
    /// </summary>
    public class Cycle
    {
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
        public List<ControlInfo> Items { get; set; }
        public List<ControlInfo> CloseItems { get; set; } = new List<ControlInfo>();
        public List<ControlInfo> OpenItems { get; set; } = new List<ControlInfo>();
        #endregion

        public Cycle() { }
        public Cycle(Cycle cycle)
        {
            Name = cycle.Name;
            OpenAt = cycle.OpenAt;
            CloseAt = cycle.CloseAt;
            Functions = cycle.Functions;
        }
    }

}
