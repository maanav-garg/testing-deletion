using AutosarBCM.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AutosarBCM.Config
{
    public class ServiceInfo
    {
        public byte ID { get; set; }
        public string Name { get; set; }
        public List<byte> Sessions { get; set; }
    }

    public class SessionInfo
    {
        public byte ID { get; set; }
        public string Name { get; set; }
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
    }

    public class PayloadInfo
    {
        public int Index { get; set; }
        public string Name { get; set; }
        public string TypeName { get; set; }
    }

    public class ResponseInfo
    {
        public byte ServiceID { get; set; }
        public List<PayloadInfo> Payloads { get; set; }
    }

    public class ConfigurationInfo
    {
        public List<ServiceInfo> Services { get; set; }
        public List<SessionInfo> Sessions { get; set; }
        public List<ControlInfo> Controls { get; set; }
    }

    public class ASApp
    {
        public static SessionInfo CurrentSession { get; set; }
        public static ConfigurationInfo Configuration { get; set; }

        public ASApp() { }

        internal static ConfigurationInfo ParseConfiguration(string filePath)
        {
            XDocument doc = XDocument.Load(filePath);

            var services = doc.Descendants("Service")
                .Select(s => new ServiceInfo
                {
                    ID = Convert.ToByte(s.Element("ID").Value, 16),
                    Name = s.Element("Name").Value,
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
                                    Index = i + 4,
                                    Name = y.Attribute("name").Value,
                                    TypeName = y.Value
                                }).ToList() : new List<PayloadInfo>(),
                            }).ToList() : new List<ResponseInfo>(),
                }).ToList();

            return Configuration = new ConfigurationInfo
            {
                Services = services,
                Sessions = sessions,
                Controls = controls
            };
        }
    }
}
