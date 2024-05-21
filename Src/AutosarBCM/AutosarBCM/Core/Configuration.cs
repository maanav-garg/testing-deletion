using AutosarBCM.Core.Enums;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AutosarBCM.Core
{
    public class SessionInfo
    {
        public byte ID { get; set; }
        public string Name { get; set; }
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
        public List<byte> Services { get; set; }
        public List<ResponseInfo> Responses { get; set; }

        public void Transmit(ServiceName serviceName)
        {
            if (serviceName == ServiceName.ReadDataByIdentifier) new ReadDataByIdenService().Transmit(this);
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
                    Name = s.Element("Name").Value
                })
                .ToList();

            var controls = doc.Descendants("Control")
                .Select(c => new ControlInfo
                {
                    Address = Convert.ToUInt16(c.Element("Address").Value, 16),
                    Name = c.Element("Name").Value,
                    Type = c.Element("Type").Value,
                    Services = c.Element("Services").Value.Split(';').Select(x => byte.Parse(x, System.Globalization.NumberStyles.HexNumber)).ToList(),
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

            return new ConfigurationInfo
            {
                Settings = settings,
                Services = services,
                Sessions = sessions,
                Controls = controls,
                Payloads = payloads,
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
}
