using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using static AutosarBCM.Core.ControlInfo;

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

        public static ServiceInfo DiagnosticSessionControl { get => ASContext.Configuration?.GetServiceByRequestID(0x10); }
        public static ServiceInfo ReadDataByIdentifier { get => ASContext.Configuration?.GetServiceByRequestID(0x22); }
        public static ServiceInfo IOCtrlByIdentifier { get => ASContext.Configuration?.GetServiceByRequestID(0x2F); }
        public static ServiceInfo WriteDataByIdentifier { get => ASContext.Configuration?.GetServiceByRequestID(0x2E); }
        public static ServiceInfo TesterPresent { get => ASContext.Configuration?.GetServiceByRequestID(0x3E); }
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

        public void Transmit(ServiceInfo serviceInfo)
        {
            if (serviceInfo.RequestID == 0x22) new ReadDataByIdenService().Transmit(this);
        }

        internal List<Payload> GetPayloads(ServiceInfo serviceInfo, byte[] data)
        {
            var payloads = new List<Payload>();
            var responseIndex = serviceInfo.ResponseIndex;

            foreach (var pInfo in Responses.Where(a => a.ServiceID == serviceInfo.ResponseID).First()?.Payloads)
            {
                var pDef = ASContext.Configuration.GetPayloadInfoByType(pInfo.TypeName);
                if (pDef == null) continue;
                var value = data?.Skip(responseIndex).Take(pDef.Length).ToArray();

                payloads.Add(InitializeType(pInfo, value));
                responseIndex += pDef?.Length ?? 0;

                if (pInfo.Bits.Count > 0) payloads.AddRange(pInfo.Bits.Select((a, i) => InitializeType(a, value, i)));
            }
            return payloads.ToList();
        }

        private static Payload InitializeType(PayloadInfo payloadInfo, byte[] value, int? index = null)
        {
            return ((Payload)Activator.CreateInstance(System.Type.GetType($"AutosarBCM.Core.{payloadInfo.TypeName}"))).Parse(payloadInfo, value, index);
        }
    }

    public class PayloadInfo
    {
        public string Name { get; set; }
        public string NamePadded { get => IsBit ? $"    {Name}" : Name; }
        public string TypeName { get; set; }
        public int Length { get; set; }
        public bool IsBit { get; internal set; }
        public List<PayloadValue> Values { get; set; }
        public List<PayloadInfo> Bits { get; set; }

        internal PayloadValue GetPayloadValue(byte[] value)
        {
            return Values?.FirstOrDefault(v => v.Value.SequenceEqual(value));
        }
    }

    public class PayloadValue
    {
        public string ValueString { get; internal set; }
        public string Color { get; set; }
        public string FormattedValue { get; set; }
        public byte[] Value { get => Enumerable.Range(0, ValueString.Length).Where(x => x % 2 == 0).Select(y => Convert.ToByte(ValueString.Substring(y, 2), 16)).ToArray(); }
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
                                    TypeName = y.Attribute("typeName").Value,
                                    Bits = y.Elements("Payload").Select(z => new PayloadInfo
                                    {
                                        Name = z.Attribute("name").Value,
                                        TypeName = z.Attribute("typeName").Value,
                                        IsBit = true,
                                    }).ToList()
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
                            ValueString = x.Attribute("value").Value,
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

        internal ServiceInfo GetServiceByResponseID(byte serviceID)
        {
            return Services.Where(x => x.ResponseID == serviceID).FirstOrDefault();
        }

        internal ServiceInfo GetServiceByRequestID(byte serviceID)
        {
            return Services.Where(x => x.RequestID == serviceID).FirstOrDefault();
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
