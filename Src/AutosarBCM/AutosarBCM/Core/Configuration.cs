using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;
using AutosarBCM.UserControls.Monitor;
using System.Threading.Tasks;
using AutosarBCM.Core.Config;
using AutosarBCM.Properties;
using System.Diagnostics;
using System.IO;
using System.Xml;
using System.Windows.Forms;
using System.Web.UI;
using System.Reflection;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Linq.Expressions;
using System.Security.Cryptography;

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
        public static ServiceInfo InputOutputControlByIdentifier { get => ASContext.Configuration?.GetServiceByRequestID(0x2F); }
        public static ServiceInfo WriteDataByIdentifier { get => ASContext.Configuration?.GetServiceByRequestID(0x2E); }
        public static ServiceInfo TesterPresent { get => ASContext.Configuration?.GetServiceByRequestID(0x3E); }
        public static ServiceInfo ECUReset { get => ASContext.Configuration?.GetServiceByRequestID(0x11); }
        public static ServiceInfo ReadDTCInformation { get => ASContext.Configuration?.GetServiceByRequestID(0x19); }
        public static ServiceInfo NegativeResponse { get => ASContext.Configuration?.GetServiceByResponseID(0x7F); }
        public static ServiceInfo ClearDTCInformation { get => ASContext.Configuration?.GetServiceByRequestID(0x14); }
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

        public void Transmit(ServiceInfo serviceInfo, byte[] data = null)
        {
            if (serviceInfo == ServiceInfo.ReadDataByIdentifier)
                new ReadDataByIdenService().Transmit(this);
            else if (serviceInfo == ServiceInfo.InputOutputControlByIdentifier)
                new IOControlByIdentifierService().Transmit(this, data);
            else if (serviceInfo == ServiceInfo.WriteDataByIdentifier)
                new WriteDataByIdentifierService().Transmit(this, data);

        }

        public void SwitchForBits(List<string> payloads, bool isOpen)
        {
            SwitchForBits(payloads.ToDictionary(x => x, x => isOpen));
        }

        public void SwitchForBits(Dictionary<string, bool> payloads)
        {
            var bitIndex = 0;
            byte bits = 0x0;
            byte controlByte = 0x0;
            var bytes = new List<byte>();
            bytes.Add((byte)InputControlParameter.ShortTermAdjustment);

            foreach (var payload in Responses?[0].Payloads)
            {
                payloads.TryGetValue(payload.Name, out bool isOpen);

                if (payload.TypeName != "DID_Bits_On_Off")
                {
                    continue;
                }
                else
                {
                    if (payloads.ContainsKey(payload.Name))
                    {
                        controlByte |= (byte)(1 << (7 - bitIndex));
                        if (isOpen)
                        {
                            bits |= (byte)(1 << (7 - bitIndex));
                        }
                    }
                    bitIndex++;
                }
            }
            //Set the values to high, control mask to low bits
            var resultByte = (byte)((bits) & 0xFF | ((controlByte) & 0xFF) >> 4);
            bytes.Add(resultByte);
            Transmit(ServiceInfo.InputOutputControlByIdentifier, bytes.ToArray());
        }

        public void Switch(List<string> payloads, bool isOpen)
        {
            Switch(payloads.ToDictionary(x => x, x => isOpen));
        }

        public void Switch(Dictionary<string, bool> payloads)
        {
            byte controlByte = 0x0;
            var bitIndex = 0;

            var bytes = new List<byte>();
            var isControlMaskActive = false;

            if (Services.IndexOf((byte)SIDDescription.SID_WRITE_DATA_BY_IDENTIFIER) == -1)
            {
                bytes.Add((byte)InputControlParameter.ShortTermAdjustment);
                isControlMaskActive = Responses[0].Payloads.Count > 1;
            }

            foreach (var payload in Responses?[0].Payloads)
            {
                payloads.TryGetValue(payload.Name, out bool isOpen);

                if (!payloads.ContainsKey(payload.Name))
                {
                    if (payload.TypeName == "DID_PWM")
                    {
                        bytes.Add(0x0);
                        bytes.Add(0x0);
                    }
                    else
                    {
                        bytes.Add(0x0);
                    }
                }
                else //Payload match
                {
                    controlByte |= (byte)(1 << (7 - bitIndex));

                    var resultPayload = ASContext.Configuration.GetPayloadInfoByType(payload.TypeName);
                    if (resultPayload == null) break;

                    //Check if control has enum
                    if (resultPayload.Values?.Count > 0)
                    {
                        var data = new List<byte>();

                        if (isOpen)
                            data = resultPayload.Values.FirstOrDefault(v => v.IsOpen).Value.ToList();
                        else
                            data = resultPayload.Values.FirstOrDefault(v => v.IsClose).Value.ToList();

                        bytes.AddRange(data);

                    }
                    else
                    {
                        if (resultPayload.TypeName == "DID_PWM")
                        {
                            byte[] pwmBytes;
                            if (isOpen)
                                pwmBytes = BitConverter.GetBytes((ushort)ASContext.Configuration.EnvironmentalTest.Environments.First(x => x.Name == EnvironmentalTest.CurrentEnvironment).EnvironmentalConfig.PWMDutyOpenValue).Reverse().ToArray();
                            else
                                pwmBytes = BitConverter.GetBytes((ushort)ASContext.Configuration.EnvironmentalTest.Environments.First(x => x.Name == EnvironmentalTest.CurrentEnvironment).EnvironmentalConfig.PWMDutyCloseValue).Reverse().ToArray();

                            //Array.Reverse(pwmBytes);
                            bytes.AddRange(pwmBytes);
                        }
                        else
                        {
                            for (int i = 0; i < resultPayload.Length; i++)
                            {
                                if (isOpen)
                                    bytes.Add(0x1);
                                else
                                    bytes.Add(0x0);
                            }
                        }

                    }
                    Console.WriteLine($" Send Control Name: {Name} -- Send Val: {payload.Name} -- {(isOpen ? Constants.Opened : Constants.Closed)}");
                    Helper.WriteCycleMessageToLogFile(Name, payload.Name, (isOpen ? Constants.Opened : Constants.Closed));

                }

                bitIndex++;
            }
            if (isControlMaskActive)
                bytes.Add(controlByte);

            if (Services.IndexOf((byte)SIDDescription.SID_WRITE_DATA_BY_IDENTIFIER) == -1)
                //Console.WriteLine($"DID {Name} {(isOpen ? "opened" : "closed")}");
                Transmit(ServiceInfo.InputOutputControlByIdentifier, bytes.ToArray());
            else
                Transmit(ServiceInfo.WriteDataByIdentifier, bytes.ToArray());

        }

        internal List<Payload> GetPayloads(ServiceInfo serviceInfo, byte[] data)
        {
            var payloads = new List<Payload>();
            var responseIndex = serviceInfo.ResponseIndex;

            foreach (var pInfo in Responses.First()?.Payloads)
            {
                var pDef = ASContext.Configuration.GetPayloadInfoByType(pInfo.TypeName);
                if (pDef == null) continue;
                var value = data?.Skip(responseIndex).Take(pDef.Length).ToArray();

                payloads.Add(InitializeType(pInfo, value));
                responseIndex += pDef?.Length ?? 0;

                if (pInfo.Bits?.Count > 0) payloads.AddRange(pInfo.Bits.Select((a, i) => InitializeType(a, value, i)));
            }
            return payloads.ToList();
        }

        private static Payload InitializeType(PayloadInfo payloadInfo, byte[] value, int? index = null)
        {
            if (FormMain.isMdxFile)
            {
                return ((Payload)Activator.CreateInstance(System.Type.GetType($"AutosarBCM.Core.Config.MDX_Payload"))).Parse(payloadInfo, value, index);
            }
            else if (FormMain.isOdxFile)
            {
                return ((Payload)Activator.CreateInstance(System.Type.GetType($"AutosarBCM.Core.Config.ODX_Payload"))).Parse(payloadInfo, value, index);
            }
            else
            {
                return ((Payload)Activator.CreateInstance(System.Type.GetType($"AutosarBCM.Core.Config.{payloadInfo.TypeName}"))).Parse(payloadInfo, value, index);
            }

        }
    }

    public class PayloadInfo
    {
        public string Name { get; set; }
        public string NamePadded { get => IsBit ? $"    {Name}" : Name; }
        public string TypeName { get; set; }
        public int Length { get; set; }
        public bool IsBit { get; internal set; }
        public string DTCCode { get; set; }
        public List<PayloadValue> Values { get; set; }
        public List<PayloadInfo> Bits { get; set; }

        internal PayloadValue GetPayloadValue(byte[] value)
        {
            return Values?.FirstOrDefault(v => v.Value.SequenceEqual(value)) ?? new PayloadValue { FormattedValue = $"U/I {BitConverter.ToString(value)}" };
        }
    }

    public class PayloadValue
    {
        public string ValueString { get; internal set; }
        public string Color { get; set; }
        public string FormattedValue { get; set; }
        public bool IsClose { get; set; }
        public bool IsOpen { get; set; }
        public byte[] Value { get => Enumerable.Range(0, ValueString.Length).Where(x => x % 2 == 0).Select(y => Convert.ToByte(ValueString.Substring(y, 2), 16)).ToArray(); }

    }

    public class ResponseInfo
    {
        public byte ServiceID { get; set; }
        public List<PayloadInfo> Payloads { get; set; }
    }

    public class DTCFailure
    {
        public byte Value { get; set; }
        public string Description { get; set; }

        public static DTCFailure GetByValue(byte value)
        {
            return ASContext.Configuration.DTCFailureTypes.Where(a => a.Value == value).FirstOrDefault();
        }
    }

    public class ConfigurationInfo
    {
        public Dictionary<string, string> Settings { get; set; }
        public List<ServiceInfo> Services { get; set; }
        public List<SessionInfo> Sessions { get; set; }
        public List<ControlInfo> Controls { get; set; }
        public List<PayloadInfo> Payloads { get; set; }
        public List<DTCFailure> DTCFailureTypes { get; set; }
        public EnvironmentalTest EnvironmentalTest { get; set; }


        internal static ConfigurationInfo ParseODX(string filePath)
        {

            XmlDocument odxDoc = new XmlDocument();
            odxDoc.Load(filePath);

            List<string> reqNrespIDs = new List<string>();
            List<string> usedReqIDs = new List<string>();   
            List<string> typeNames = new List<string>();
            List<string> usedReqList = new List<string>();
            List<string> reqList = new List<string>();
            List<string> newReqList = new List<string>();
            List<string> idPaylNcontN = new List<string>();
            List<string> seshList = new List<string>();
            Dictionary<string, string> functIdNgroup = new Dictionary<string, string>();
            Dictionary<string, string> dtcCodeNtext = new Dictionary<string, string>(); 
            Dictionary<string, string> textNsdgDict = new Dictionary<string, string>();
            Dictionary<string, string> dopDict = new Dictionary<string, string>();
            List<ServiceInfo> services = new List<ServiceInfo>();
            List<PayloadInfo> payloadInfos =  new List<PayloadInfo>();
            List<DTCFailure> dTCFailures = new List<DTCFailure>();
            List<ControlInfo> controls = new List<ControlInfo>();
            List<SessionInfo> sessions = new List<SessionInfo>();
            XmlNodeList basEVariants = odxDoc.GetElementsByTagName("BASE-VARIANT");
            foreach (XmlNode basEVariant in basEVariants)
            {
                XmlNodeList functClassesVariants = basEVariant.SelectNodes("FUNCT-CLASSS");
                foreach(XmlNode functClassesVariant in functClassesVariants)
                {
                    XmlNodeList functClassVariants = functClassesVariant.SelectNodes("FUNCT-CLASS");
                    foreach(XmlNode functClassVariant in functClassVariants)
                    {
                        functIdNgroup.Add(functClassVariant.Attributes.Item(0).Value, functClassVariant.SelectSingleNode("LONG-NAME").InnerText);
                    }
                }
            }

            XmlNodeList dopVariants = odxDoc.GetElementsByTagName("DATA-OBJECT-PROP");
            foreach (XmlNode dopVariant in dopVariants)
            {
                XmlNode dop_sNameAttribute = dopVariant.SelectSingleNode("SHORT-NAME");
                if (dop_sNameAttribute != null && dop_sNameAttribute.InnerText == "DID_D100_1")
                {
                    XmlNodeList compuScales = dopVariant.SelectSingleNode("COMPU-METHOD").SelectSingleNode("COMPU-INTERNAL-TO-PHYS").SelectSingleNode("COMPU-SCALES").SelectNodes("COMPU-SCALE");
                    foreach (XmlNode compuScale in compuScales)
                    {
                        string seshNum = "0" + compuScale.SelectSingleNode("LOWER-LIMIT").InnerText;
                        string seshName = compuScale.SelectSingleNode("COMPU-CONST").SelectSingleNode("VT").InnerText;
                        SessionInfo session = new SessionInfo();
                        session.Name = seshName;
                        session.ID = Convert.ToByte(seshNum, 16);
                        if (seshNum != "02")
                        {
                            session.AvailableServices = new List<byte>
                            {
                                Convert.ToByte(SIDDescription.SID_READ_DATA_BY_IDENTIFIER)
                            };
                        }
                        sessions.Add(session);
                    }
                }
                else if (dop_sNameAttribute != null && dop_sNameAttribute.InnerText == "FTB")
                {
                    XmlNodeList compuScales = dopVariant.SelectSingleNode("COMPU-METHOD").SelectSingleNode("COMPU-INTERNAL-TO-PHYS").SelectSingleNode("COMPU-SCALES").SelectNodes("COMPU-SCALE");
                    foreach (XmlNode compuScale in compuScales)
                    {
                        DTCFailure failure =  new DTCFailure();
                        failure.Value = Convert.ToByte(compuScale.SelectSingleNode("LOWER-LIMIT").InnerText);
                        failure.Description = compuScale.SelectSingleNode("COMPU-CONST").InnerText;
                        dTCFailures.Add(failure);
                    }
                }
            }
            
            XmlNodeList ecuVariants = odxDoc.GetElementsByTagName("ECU-VARIANT");
            foreach (XmlNode ecuVariant in ecuVariants)
            {
                XmlNodeList diagDataDicts = ecuVariant.SelectNodes("DIAG-DATA-DICTIONARY-SPEC");
                foreach (XmlNode diagDataDict in diagDataDicts)
                {
                    XmlNodeList dtcDopVariants = diagDataDict.SelectNodes("DTC-DOPS");
                    foreach (XmlNode dtcDopVariant in dtcDopVariants)
                    {
                        XmlNodeList dtcDops = dtcDopVariant.SelectNodes("DTC-DOP");
                        foreach(XmlNode dtcDop in dtcDops)
                        {
                            XmlNodeList dtcVariants = dtcDop.SelectNodes("DTCS");
                            foreach(XmlNode dtcVariant in dtcVariants)
                            {
                                XmlNodeList dtcs = dtcVariant.SelectNodes("DTC");
                                foreach(XmlNode dtc in dtcs)
                                {
                                    string[] textArr = dtc.SelectSingleNode("TEXT").InnerText.Split(' ');
                                    string tempStr = "";
                                    for (int i = 3; i < dtc.SelectSingleNode("SHORT-NAME").InnerText.Length - 2; i++)
                                    {
                                        tempStr += dtc.SelectSingleNode("SHORT-NAME").InnerText[i];
                                    }
                                    string descN = "";
                                    if (!dtcCodeNtext.ContainsKey(tempStr))
                                        dtcCodeNtext.Add(tempStr, textArr[0]);
                                    XmlNodeList sdgVariants = dtc.SelectNodes("SDGS");
                                    foreach(XmlNode sdgVariant in sdgVariants)
                                    {
                                        XmlNodeList sdgs = sdgVariant.SelectNodes("SDG");
                                        foreach(XmlNode sdg in sdgs)
                                        {
                                            descN = sdg.InnerText;
                                        }
                                    }
                                    if (!textNsdgDict.ContainsKey(textArr[0]))
                                        textNsdgDict.Add(textArr[0],descN);
                                        
                                }
                            }
                        }
                    }
                }

                XmlNodeList diagComms = ecuVariant.SelectNodes("DIAG-COMMS");
                foreach (XmlNode diagComm in diagComms)
                {
                    XmlNodeList ecuVar_diagServices = diagComm.SelectNodes("DIAG-SERVICE");
                    foreach (XmlNode ecuVar_diagService in ecuVar_diagServices)
                    {
                        var c = new ControlInfo();
                        XmlNode diagServ = ecuVar_diagService.SelectSingleNode("SHORT-NAME");
                        if (diagServ != null)
                        {

                            string[] tempArr = diagServ.InnerText.Split('_');
                            if (tempArr[tempArr.Length - 1] == "Read")
                            {
                                string[] sNameArr = tempArr.Take(tempArr.Length - 1).ToArray();
                                string sName = "";
                                foreach (string word in sNameArr)
                                {
                                    sName += word + " ";
                                }
                                sName = sName.TrimEnd(' ');
                                c.Name = sName;
                                //TODO type i fixle
                                c.Type = "Not found yet";

                                string groupId = ecuVar_diagService.SelectSingleNode("FUNCT-CLASS-REFS").SelectSingleNode("FUNCT-CLASS-REF").Attributes.Item(0).Value;
                                string groupName = functIdNgroup.Values.ElementAt(functIdNgroup.Keys.ToList().IndexOf(groupId));
                                switch (groupName)
                                {
                                    case "ECU Configuration DIDs":
                                        c.Group = "ECU-DID";
                                        break;
                                    case "DIDs":
                                        c.Group = "DID";
                                        break;
                                    default:
                                        c.Group = "DID";
                                        break;
                                }
                                controls.Add(c);
                            }
                            else if (tempArr[tempArr.Length - 1] == "Adjustment" && tempArr[tempArr.Length - 2] == "Term" && tempArr[tempArr.Length - 3] == "Short")
                            {
                                string[] sNameArr = tempArr.Take(tempArr.Length - 3).ToArray();
                                string sName = "";
                                foreach (string word in sNameArr)
                                {
                                    sName += word + " ";
                                }
                                sName = sName.TrimEnd(' ');
                                bool contains = false;
                                foreach (ControlInfo con in controls)
                                {
                                    if (con.Name == sName)
                                    {
                                        contains = true;
                                    }
                                }
                                if (contains = false)
                                {
                                    c.Name = sName;
                                    controls.Add(c);
                                }
                            }
                        }
                    }
                }
                XmlNodeList reqsVariants = ecuVariant.SelectNodes("REQUESTS");
                foreach (XmlNode reqsVariant in reqsVariants)
                {
                    XmlNodeList reqVariants = reqsVariant.SelectNodes("REQUEST");
                    foreach (XmlNode reqVariant in reqVariants)
                    {
                        XmlNode rqName = reqVariant.SelectSingleNode("SHORT-NAME");
                        if (rqName != null)
                        {
                            XmlNodeList paramsElems = reqVariant.SelectNodes("PARAMS");
                            foreach (XmlNode paramsElem in paramsElems)
                            {
                                XmlNodeList paramElems = paramsElem.SelectNodes("PARAM");
                                foreach (XmlNode paramElem in paramElems)
                                {
                                    if (paramElem.Attributes.Item(0).Value == "SERVICE-ID")
                                    {
                                        XmlNode paramElemNext = paramElem.NextSibling;
                                        if (paramElemNext != null)
                                        {
                                            XmlNode paramName = paramElemNext.SelectSingleNode("SHORT-NAME");
                                            if (paramName.InnerText == "Identifier")
                                            {
                                                XmlNode codedVal = paramElem.SelectSingleNode("CODED-VALUE");
                                                if (codedVal != null)
                                                {
                                                    foreach (ControlInfo cont in controls)
                                                    {
                                                        string[] tempArr = rqName.InnerText.Split('_');
                                                        string rName = "";
                                                        int k = 1;
                                                        if (tempArr[tempArr.Length - 1] == "Adjustment")
                                                            k = 3;
                                                        for (int i = 1; i < tempArr.Length - k; i++)
                                                        {
                                                            rName += tempArr[i] + " ";
                                                        }
                                                        rName = rName.TrimEnd(' ');
                                                        if (rName.Equals(cont.Name))
                                                        {
                                                            reqNrespIDs.Add(codedVal.InnerText+"+"+cont.Name);
                                                            if (cont.Services == null)
                                                                cont.Services = new List<byte>(new byte[] { Convert.ToByte(codedVal.InnerText), });
                                                            else
                                                                cont.Services.Add(Convert.ToByte(codedVal.InnerText));
                                                        }
                                                    }
                                                }
                                            }
                                            else if (paramName.InnerText == "Diagnostic_session")
                                            {
                                                seshList.Add(paramElem.SelectSingleNode("SHORT-NAME").InnerText.Split('_')[1] + "+" + Convert.ToByte(paramElem.SelectSingleNode("CODED-VALUE").InnerText));
                                            }
                                        }
                                    }
                                    else if (paramElem.Attributes.Item(0).Value == "DATA-ID")
                                    {
                                        XmlNode paramName = paramElem.SelectSingleNode("SHORT-NAME");
                                        if (paramName.InnerText == "Identifier")
                                        {
                                            XmlNode codedVal = paramElem.SelectSingleNode("CODED-VALUE");
                                            if (codedVal != null)
                                            {
                                                //TODO:isim uyusumu durumunda coded valueyu cekip controlinfo icerisinden doldurma
                                                foreach (ControlInfo cont in controls)
                                                {
                                                    string[] tempArr = rqName.InnerText.Split('_');
                                                    string rName = "";
                                                    int k = 1;
                                                    if (tempArr[tempArr.Length - 1] == "Adjustment")
                                                        k = 3;
                                                    for (int i = 1; i < tempArr.Length - k; i++)
                                                    {
                                                        rName += tempArr[i] + " ";
                                                    }
                                                    rName = rName.TrimEnd(' ');
                                                    if (rName.Equals(cont.Name))
                                                    {
                                                        cont.Address = Convert.ToUInt16(codedVal.InnerText);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else if (paramElem.Attributes.Item(0).Value == "DATA")
                                    {
                                        foreach(XmlNode dopRef in paramElem.SelectNodes("DOP-REF"))
                                        {
                                            string idRef = dopRef.Attributes.Item(0).Value;
                                            if (paramElem.SelectSingleNode("DESC") != null)
                                            {
                                                string descName = paramElem.SelectSingleNode("DESC").InnerText.Replace("\n",string.Empty).Trim().Replace("\t", " "); ;
                                                if (descName.Contains("C4.24"))
                                                    Console.Write("");
                                                reqList.Add(idRef + "+" + descName);
                                            }
                                        }
                                    }
                                }
                            }

                        }
                    }
                }
                XmlNodeList reqRespVars = odxDoc.GetElementsByTagName("REQUESTS");
                foreach (XmlNode reqRespVar in reqRespVars)
                {
                    XmlNodeList reqResps = reqRespVar.SelectNodes("REQUEST");
                    foreach (XmlNode reqResp in reqResps)
                    {
                        if (reqResp.LastChild.FirstChild.NextSibling != null)
                        {
                            if (reqResp.LastChild.FirstChild.NextSibling.Attributes.Item(0).Value == "SUBFUNCTION" && reqResp.LastChild.FirstChild.NextSibling.SelectSingleNode("SHORT-NAME").InnerText == "Diagnostic_session")
                            {
                                if(!seshList.Contains(reqResp.SelectSingleNode("SHORT-NAME").InnerText.Split('_')[1] + "+" + Convert.ToByte((reqResp.LastChild.FirstChild.SelectSingleNode("CODED-VALUE").InnerText))))
                                    seshList.Add(reqResp.SelectSingleNode("SHORT-NAME").InnerText.Split('_')[1] + "+" + Convert.ToByte((reqResp.LastChild.FirstChild.SelectSingleNode("CODED-VALUE").InnerText)));

                            }
                        }
                    }
                }
                XmlNodeList posRespVars = odxDoc.GetElementsByTagName("POS-RESPONSES");
                foreach (XmlNode posRespVar in posRespVars)
                {
                    XmlNodeList posResps = posRespVar.SelectNodes("POS-RESPONSE");
                    foreach(XmlNode posResp in posResps)
                    {
                        if (posResp.LastChild.FirstChild.NextSibling != null)
                        {
                            if (posResp.LastChild.FirstChild.NextSibling.Attributes.Item(0).Value == "SUBFUNCTION" && posResp.LastChild.FirstChild.NextSibling.SelectSingleNode("SHORT-NAME").InnerText == "Diagnostic_session")
                            {
                                string element = seshList.ElementAt(0);

                                if (posResp.SelectSingleNode("SHORT-NAME").InnerText.Split('_')[1] == element.Split('+')[0])
                                {
                                    ServiceInfo service = new ServiceInfo();
                                    service.ResponseID = Convert.ToByte(posResp.LastChild.FirstChild.SelectSingleNode("CODED-VALUE").InnerText);
                                    service.RequestID = Convert.ToByte(element.Split('+')[1]);
                                    service.Name = "DiagnosticSessionControl";
                                    services.Add(service);
                                }
                            }
                        }
                    }
                }
                XmlNodeList ecuVars = odxDoc.GetElementsByTagName("ECU-VARIANT");
                foreach (XmlNode ecuVar in ecuVars)
                {
                    XmlNodeList diagDataDictSpecs = ecuVar.SelectNodes("DIAG-DATA-DICTIONARY-SPEC");
                    foreach (XmlNode diagDataDictSpec in diagDataDictSpecs)
                    {
                        XmlNodeList structureVariants = diagDataDictSpec.SelectNodes("STRUCTURES");
                        foreach (XmlNode structureVariant in structureVariants)
                        {
                            XmlNodeList structures = structureVariant.SelectNodes("STRUCTURE");
                            foreach (XmlNode structure in structures)
                            {
                                if(structure.SelectSingleNode("LONG-NAME") != null)
                                {
                                    string contName = structure.SelectSingleNode("LONG-NAME").InnerText;
                                    XmlNodeList paramVariants = structure.SelectNodes("PARAMS");
                                    foreach (XmlNode paramVariant in paramVariants)
                                    {
                                        XmlNodeList paramSts = paramVariant.SelectNodes("PARAM");
                                        foreach (XmlNode paramSt in paramSts)
                                        {
                                            if (paramSt.SelectSingleNode("DESC") != null)
                                            {
                                                string payloadDesc = paramSt.SelectSingleNode("DESC").InnerText;
                                                PayloadInfo payload = new PayloadInfo();
                                                payload.Name = payloadDesc.Trim().Replace("\t", " ");
                                                if (!reqList.Contains(paramSt.SelectSingleNode("DOP-REF").Attributes.Item(0).Value + "+" + payload.Name))
                                                    idPaylNcontN.Add(paramSt.SelectSingleNode("DOP-REF").Attributes.Item(0).Value + "+" + payload.Name+"+"+contName);// + "+" + control.Name);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                XmlNodeList baseVars = odxDoc.GetElementsByTagName("BASE-VARIANT");
                foreach (XmlNode baseVar in baseVars)
                {
                    XmlNodeList diagDicts = baseVar.SelectNodes("DIAG-DATA-DICTIONARY-SPEC");
                    XmlNode matchingDop = null;
                    foreach (XmlNode diagDict in diagDicts)
                    {
                        XmlNodeList dtcDops = diagDict.SelectNodes("DATA-OBJECT-PROPS");
                        foreach (XmlNode dtcDop in dtcDops)
                        {
                            XmlNodeList dopDops = dtcDop.SelectNodes("DATA-OBJECT-PROP");
                            foreach (XmlNode dopDop in dopDops)
                            {
                                bool isReqAdded = false;
                                foreach (string element in reqList)
                                {
                                    //sikinti burada
                                    string dopSName = dopDop.SelectSingleNode("SHORT-NAME").InnerText;
                                    string newElm = element + "+" + dopSName;
                                    if (element.Contains("C4.24") && dopSName == "DID_Byte_Present_notPresent")
                                        Console.Write("");

                                    if (dopDop.Attributes.Item(0).Value == element.Split('+')[0] && !newReqList.Contains(newElm))
                                    {
                                        /*matchingDop = dopDop;
                                        XmlNodeList dops = dopDop.SelectNodes("DATA-OBJECT-PROP");
                                        string dopSName = matchingDop.SelectSingleNode("SHORT-NAME").InnerText;*/
                                        //XmlNodeList compScas = null;
                                        /*if (matchingDop.SelectSingleNode("COMPU-METHOD").SelectSingleNode("COMPU-INTERNAL-TO-PHYS") != null)
                                            compScas = matchingDop.SelectSingleNode("COMPU-METHOD").SelectSingleNode("COMPU-INTERNAL-TO-PHYS").SelectSingleNode("COMPU-SCALES").SelectNodes("COMPU-SCALE");*/
                                        //if (!dopDict.ContainsKey(element.Split('+')[0]))
                                        //{
                                        if (!dopDict.ContainsKey(element.Split('+')[0]))
                                            dopDict.Add(element.Split('+')[0], dopSName); 
                                        //string newElm = element + "+" + dopSName;
                                        if(!newReqList.Contains(newElm))
                                            newReqList.Add(newElm);
                                        isReqAdded = true;
                                    }
                                    if (isReqAdded)
                                        break;
                                }

                                /*foreach (string element in idPaylNcontN)
                                {
                                    if (dopDop.Attributes.Item(0).Value == element.Split('+')[0])
                                    {
                                        matchingDop = dopDop;
                                        XmlNodeList dops = dopDop.SelectNodes("DATA-OBJECT-PROP");
                                        string dopSName = matchingDop.SelectSingleNode("SHORT-NAME").InnerText;
                                        XmlNodeList compScas = null;
                                        if (matchingDop.SelectSingleNode("COMPU-METHOD").SelectSingleNode("COMPU-INTERNAL-TO-PHYS") != null)
                                            compScas = matchingDop.SelectSingleNode("COMPU-METHOD").SelectSingleNode("COMPU-INTERNAL-TO-PHYS").SelectSingleNode("COMPU-SCALES").SelectNodes("COMPU-SCALE");
                                        if (!dopDict.ContainsKey(element.Split('+')[0]))
                                            dopDict.Add(element.Split('+')[0], dopSName);
                                        string newElm = element.Split('+')[0] + "+" + element.Split('+')[1] + "+" + dopSName;
                                        if (!newReqList.Contains(newElm)) { 
                                            newReqList.Add(newElm);
                                            idPaylNcontN.Remove(newElm);
                                        }
                                    }
                                }*/
                            }
                        }
                    }
                }
                XmlNodeList posRespsVariants = ecuVariant.SelectNodes("POS-RESPONSES");
                foreach (XmlNode posRespsVariant in posRespsVariants)
                {
                    XmlNodeList posRespVariants = posRespsVariant.SelectNodes("POS-RESPONSE");
                    foreach (XmlNode respVariant in posRespVariants)
                    {
                        
                        List<ResponseInfo> responses = new List<ResponseInfo>();
                        XmlNode respName = respVariant.SelectSingleNode("SHORT-NAME");
                        foreach (ControlInfo control in controls)
                        {
                            ResponseInfo response = new ResponseInfo();
                            string[] sNameArr = respName.InnerText.Split('_');
                            string sName = "";
                            int k = 1;
                            if (sNameArr[sNameArr.Length - 1] == "Adjustment")
                                k = 3;
                            for (int i = 1; i < sNameArr.Length - k; i++)
                            {
                                sName += sNameArr[i] + " ";
                            }
                            sName = sName.TrimEnd(' ');

                            if (sName.Equals(control.Name.Trim()))
                            {
                                XmlNodeList paramsVars = respVariant.SelectNodes("PARAMS");
                                foreach (XmlNode paramsVar in paramsVars)
                                {
                                    foreach (XmlNode paramVar in paramsVar.SelectNodes("PARAM"))
                                    {
                                        /*if (respName.InnerText == "PR_DE09_Read" && control.Name.Contains("DE09"))
                                            Console.Write("");*/
                                        //DEXX didlerinin cekilebildigi clause
                                        if ((respName.InnerText.Contains("Read") && respVariant.NextSibling.SelectSingleNode("SHORT-NAME").InnerText.Contains("Write")) && paramVar.Attributes.Item(0).Value == "DATA" && paramsVar.FirstChild.NextSibling.Attributes.Item(0).Value == "ID" && paramsVar.FirstChild.NextSibling.Attributes.Item(0).Value == "ID") //&& paramVar.SelectSingleNode("DESC") != null)
                                        {
                                            //Payload ismi requestlerde yoksa pos responselardan bakma

                                            XmlNode payloadDesc = paramVar.SelectSingleNode("SHORT-NAME");
                                            PayloadInfo payload = new PayloadInfo();
                                            payload.Name = payloadDesc.InnerText.Trim().Replace("_", " ");
                                            //burada csvdeki isimlendirmelerin containl
                                            if (payload.Name == "C4.24 MainRightMirrorUpCtrl")
                                                Console.Write("");

                                            //if (reqList.Contains(paramVar.SelectSingleNode("DOP-REF").Attributes.Item(0).Value + "+" + payload.Name))
                                            reqList.Add(paramVar.SelectSingleNode("DOP-REF").Attributes.Item(0).Value + "+" + payload.Name);
                                            idPaylNcontN.Add(paramVar.SelectSingleNode("DOP-REF").Attributes.Item(0).Value + "+" + payload.Name + "+" + control.Name);
                                            foreach (XmlNode baseVar in baseVars)
                                            {
                                                XmlNodeList diagDicts = baseVar.SelectNodes("DIAG-DATA-DICTIONARY-SPEC");
                                                XmlNode matchingDop = null;
                                                foreach (XmlNode diagDict in diagDicts)
                                                {
                                                    XmlNodeList dtcDops = diagDict.SelectNodes("DATA-OBJECT-PROPS");
                                                    foreach (XmlNode dtcDop in dtcDops)
                                                    {
                                                        XmlNodeList dopDops = dtcDop.SelectNodes("DATA-OBJECT-PROP");
                                                        foreach (XmlNode dopDop in dopDops)
                                                        {
                                                            foreach (string element in reqList)
                                                            {
                                                                if (dopDop.Attributes.Item(0).Value == element.Split('+')[0])
                                                                {
                                                                    matchingDop = dopDop;
                                                                    XmlNodeList dops = dopDop.SelectNodes("DATA-OBJECT-PROP");
                                                                    string dopSName = matchingDop.SelectSingleNode("SHORT-NAME").InnerText;

                                                                    if (!dopDict.ContainsKey(element.Split('+')[0]))
                                                                        dopDict.Add(element.Split('+')[0], dopSName);
                                                                    string newElm = element + "+" + dopSName;
                                                                    if (!newReqList.Contains(newElm))
                                                                        newReqList.Add(newElm);
                                                                }
                                                            }

                                                            foreach (string element in idPaylNcontN)
                                                            {
                                                                if (element.Contains("DE09"))
                                                                    Console.Write("");
                                                                if (dopDop.Attributes.Item(0).Value == element.Split('+')[0])
                                                                {
                                                                    matchingDop = dopDop;
                                                                    XmlNodeList dops = dopDop.SelectNodes("DATA-OBJECT-PROP");
                                                                    string dopSName = matchingDop.SelectSingleNode("SHORT-NAME").InnerText;
                                                                    if (!dopDict.ContainsKey(element.Split('+')[0]))
                                                                        dopDict.Add(element.Split('+')[0], dopSName);
                                                                    string newElm = element.Split('+')[0] + "+" + element.Split('+')[1] + "+" + dopSName;
                                                                    if (!newReqList.Contains(newElm))
                                                                    {
                                                                        newReqList.Add(newElm);
                                                                        //idPaylNcontN.Remove(newElm);
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                            PayloadValue payloadVal = new PayloadValue();
                                            if (payloadDesc != null)
                                            {
                                                XmlNodeList baseVariants = odxDoc.GetElementsByTagName("BASE-VARIANT");
                                                foreach (XmlNode baseVariant in baseVariants)
                                                {
                                                    XmlNodeList diagDataVars = baseVariant.SelectNodes("DIAG-DATA-DICTIONARY-SPEC");
                                                    foreach (XmlNode diagDataVar in diagDataVars)
                                                    {
                                                        /*XmlNodeList dtcDops = diagDataVar.SelectNodes("DTC-DOPS");
                                                        foreach (XmlNode dtcDop in dtcDops)
                                                        {
                                                            XmlNodeList dtcDopVariants = dtcDop.SelectNodes("DTC-DOP");
                                                            foreach (XmlNode dtcDopVariant in dtcDopVariants)
                                                            {
                                                                XmlNodeList dtcsNodes = dtcDopVariant.SelectNodes("DTCS");
                                                                foreach (XmlNode dtcsNode in dtcsNodes)
                                                                {
                                                                    XmlNodeList dtcVariants = dtcsNode.SelectNodes("DTC");
                                                                    foreach (XmlNode dtcVariant in dtcVariants)
                                                                    {
                                                                        bool isDtcSet = false;
                                                                        XmlNode dtcDop_Text = dtcVariant.SelectSingleNode("TEXT");
                                                                        string[] textArr = dtcDop_Text.InnerText.Split(' ');
                                                                        //if (payload.Name.Contains("HornActivateSwitch") && textNsdgDict.ContainsKey("HornActivateSwitch"))
                                                                        //newReqList.Add(dtcVariant.SelectSingleNode("DOP-REF").Attributes.Item(0).Value+"+");
                                                                        string payloadValName = payload.Name;
                                                                        //Burasi 
                                                                        if (payload.Name != "")
                                                                        {
                                                                            if (control.Name == "DE09" && textArr[0].Contains("DE09"))
                                                                                Console.Write("");
                                                                            if (textArr[0] == control.Name || textArr[0] == payload.Name || payload.Name.Contains(textArr[0]) || payload.Name.Contains(textArr[0].Replace("Output", "")))//textArr[0] == payload.Name.Substring(0,4)+"Activate"+payload.Name.Substring(4) )
                                                                            {
                                                                                XmlNode dtcDop_sNameNode = dtcVariant.SelectSingleNode("SHORT-NAME");
                                                                                if (dtcDop_sNameNode != null)
                                                                                {
                                                                                    string tempStr = "";
                                                                                    for (int i = 3; i < dtcDop_sNameNode.InnerText.Length - 2; i++)
                                                                                    {
                                                                                        tempStr += dtcDop_sNameNode.InnerText[i];
                                                                                    }
                                                                                    payload.DTCCode = tempStr;
                                                                                    isDtcSet = true;
                                                                                }
                                                                            }
                                                                        }
                                                                        if (isDtcSet)
                                                                            break;
                                                                    }
                                                                }
                                                            }
                                                        }*/
                                                        if (respName.InnerText == "PR_DE09_Read")
                                                            Console.Write("");
                                                        XmlNodeList dopsVar = diagDataVar.SelectNodes("DATA-OBJECT-PROPS");
                                                        foreach (XmlNode dopVar in dopsVar)
                                                        {
                                                            XmlNodeList dops = dopVar.SelectNodes("DATA-OBJECT-PROP");
                                                            foreach (XmlNode dop in dops)
                                                            {
                                                                if (dopDict.Keys.Contains(dop.Attributes.Item(0).Value))
                                                                {
                                                                    PayloadInfo tempPayload = new PayloadInfo();
                                                                    string dopID = dopDict.Keys.ElementAt(dopDict.Keys.ToList().IndexOf(dop.Attributes.Item(0).Value));
                                                                    string typName = dop.SelectSingleNode("SHORT-NAME").InnerText;
                                                                    if (typName == "HexDump_1Byte")
                                                                        Console.Write("");
                                                                    string bitLenStr = dop.SelectSingleNode("DIAG-CODED-TYPE").SelectSingleNode("BIT-LENGTH").InnerText;
                                                                    int byteLen = int.Parse(bitLenStr) / 8;
                                                                    payload.Length = byteLen;
                                                                    payload.TypeName = typName;
                                                                    foreach (string descN in newReqList)
                                                                    {

                                                                        //dictten text arr 0 ve descn ile girme
                                                                        if (reqList.Contains(descN.Split('+')[0] + "+" + descN.Split('+')[1]) && /*!usedReqList.Contains(descN) &&*/ (descN.Split('+')[2] == payload.TypeName && (descN.Split('+')[1].Equals(payload.Name)))) // || textNsdgDict.ContainsValue(payload.Name)))
                                                                        {
                                                                            if (payload.Name == "C4.24 MainRightMirrorUpCtrl")
                                                                                Console.Write("");
                                                                            if (!usedReqList.Contains(descN))
                                                                                usedReqList.Add(descN);
                                                                            if (dop.SelectSingleNode("COMPU-METHOD").SelectSingleNode("COMPU-INTERNAL-TO-PHYS") != null)
                                                                            {
                                                                                XmlNodeList valNodes = dop.SelectSingleNode("COMPU-METHOD").SelectSingleNode("COMPU-INTERNAL-TO-PHYS").SelectSingleNode("COMPU-SCALES").SelectNodes("COMPU-SCALE");
                                                                                foreach (XmlNode valNode in valNodes)
                                                                                {
                                                                                    PayloadValue tempPayVal = new PayloadValue();
                                                                                    if (valNode.SelectSingleNode("COMPU-CONST") != null)
                                                                                    {
                                                                                        payloadVal.FormattedValue = valNode.SelectSingleNode("COMPU-CONST").SelectSingleNode("VT").InnerText;
                                                                                        payloadVal.ValueString = (Convert.ToByte(valNode.SelectSingleNode("LOWER-LIMIT").InnerText).ToString("X2"));
                                                                                        tempPayVal.ValueString = payloadVal.ValueString;
                                                                                        tempPayVal.FormattedValue = payloadVal.FormattedValue;
                                                                                        if (payload.Values != null)
                                                                                            payload.Values.Add(tempPayVal);

                                                                                        else
                                                                                        {
                                                                                            payload.Values = new List<PayloadValue>
                                                                                    {
                                                                                        tempPayVal
                                                                                    };
                                                                                        }
                                                                                    }
                                                                                }
                                                                            }
                                                                            /*if (payload.Name.Contains("Horn"))
                                                                                Console.Write("");
                                                                            if ((textNsdgDict.ContainsValue(payload.Name) && control.Name.Replace("Ctrl", "") == textNsdgDict.Keys.ElementAt(textNsdgDict.Values.ToList().IndexOf(payload.Name))) || (textNsdgDict.ContainsValue(payload.Name) && control.Name.Replace(" Warning ", "") == textNsdgDict.Keys.ElementAt(textNsdgDict.Values.ToList().IndexOf(payload.Name))) || textNsdgDict.ContainsValue(payload.Name) && control.Name.Replace(" ", "Activate") == textNsdgDict.Keys.ElementAt(textNsdgDict.Values.ToList().IndexOf(payload.Name)))
                                                                            {
                                                                                if (control.Name.Contains("Hazard Warning"))
                                                                                {
                                                                                    Console.Write("");
                                                                                }
                                                                                XmlNodeList valNodes = dop.SelectSingleNode("COMPU-METHOD").SelectSingleNode("COMPU-INTERNAL-TO-PHYS").SelectSingleNode("COMPU-SCALES").SelectNodes("COMPU-SCALE");
                                                                                foreach (XmlNode valNode in valNodes)
                                                                                {
                                                                                    PayloadValue tempPayVal = new PayloadValue();
                                                                                    if (valNode.SelectSingleNode("COMPU-CONST") != null)
                                                                                    {
                                                                                        payloadVal.FormattedValue = valNode.SelectSingleNode("COMPU-CONST").SelectSingleNode("VT").InnerText;
                                                                                        payloadVal.ValueString = (Convert.ToByte(valNode.SelectSingleNode("LOWER-LIMIT").InnerText).ToString("X2"));
                                                                                        tempPayVal.ValueString = payloadVal.ValueString;
                                                                                        tempPayVal.FormattedValue = payloadVal.FormattedValue;
                                                                                        if (payload.Values != null)
                                                                                            payload.Values.Add(tempPayVal);

                                                                                        else
                                                                                        {
                                                                                            payload.Values = new List<PayloadValue>
                                                                                            {
                                                                                                tempPayVal
                                                                                            };
                                                                                        }
                                                                                    }
                                                                                }
                                                                            }*/
                                                                            else
                                                                            {
                                                                                PayloadValue tempPayVal = new PayloadValue();
                                                                                payload.Values = new List<PayloadValue>
                                                                        {
                                                                            tempPayVal
                                                                        };
                                                                            }
                                                                            tempPayload.TypeName = payload.TypeName;
                                                                            tempPayload.Length = payload.Length;
                                                                            tempPayload.Values = payload.Values;
                                                                            //if (tempPayload.Values != null)
                                                                            //{ }
                                                                            PayloadInfo tempInfo = new PayloadInfo();
                                                                            tempInfo.TypeName = tempPayload.TypeName;
                                                                            tempInfo.Length = tempPayload.Length;
                                                                            tempInfo.Values = tempPayload.Values;
                                                                            //if(payload.TypeName == tempPayload.TypeName
                                                                            if (!typeNames.Contains(tempInfo.TypeName))
                                                                            {
                                                                                payloadInfos.Add(tempInfo);
                                                                                typeNames.Add(tempInfo.TypeName);
                                                                            }
                                                                            //payloadlardaki ayni typenamede olanlari tekrar ekliyor bunu cikarmam lazim
                                                                            if (tempPayload != null)
                                                                            {
                                                                                if (response.Payloads != null)
                                                                                    response.Payloads.Add(tempPayload);
                                                                                else
                                                                                {
                                                                                    response.Payloads = new List<PayloadInfo>
                                                                            {
                                                                                tempPayload
                                                                            };
                                                                                }
                                                                            }

                                                                            tempPayload.DTCCode = payload.DTCCode;
                                                                            tempPayload.Name = payload.Name;
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        else if (paramVar.Attributes.Item(0).Value == "SERVICE-ID")
                                        {
                                            XmlNode serviceId = paramVar.SelectSingleNode("CODED-VALUE");
                                            response.ServiceID = Convert.ToByte(serviceId.InnerText);
                                            foreach (string element in reqNrespIDs)
                                            {
                                                if (element.Split('+')[1] == control.Name && !usedReqIDs.Contains(element.Split('+')[0]))
                                                {
                                                    ServiceInfo service = new ServiceInfo();
                                                    service.ResponseID = Convert.ToByte(serviceId.InnerText);
                                                    service.RequestID = Convert.ToByte(element.Split('+')[0]);
                                                    if (service.RequestID == 0x22)
                                                    {
                                                        service.Name = "ReadDataByIdentifier";
                                                    }
                                                    else if (service.RequestID == 0x2f)
                                                    {
                                                        service.Name = "InputOutputControlByIdentifier";
                                                    }
                                                    else if (service.RequestID == 0x2e)
                                                    {
                                                        service.Name = "WriteDataByIdentifier";
                                                    }
                                                    services.Add(service);
                                                    usedReqIDs.Add(element.Split('+')[0]);
                                                }
                                            }
                                            //dene bakalim req ve responselardan ayni birkac adet olacak mi
                                            if (response != null)
                                            {
                                                if (response.Payloads == null)
                                                {
                                                    response.Payloads = new List<PayloadInfo>();
                                                }
                                                responses.Add(response);
                                                if (control.Responses == null)
                                                    control.Responses = responses;
                                                else
                                                    control.Responses.Add(response);
                                            }
                                        }
                                        else if (paramVar.Attributes.Item(0).Value == "DATA-ID")
                                        {
                                            XmlNode payloadEl = paramVar.SelectSingleNode("CODED-VALUE");
                                        }
                                        //Structure ise
                                        //TODO BURAYA BAK LA
                                        else if (paramVar.Attributes.Item(0).Value == "DATA" && paramVar.SelectSingleNode("DESC") == null)
                                        {


                                            if (control.Name == "All Doors Lock and Ajar Input Signal")
                                                Console.Write("");
                                            string payLN = "";
                                            for (int i = 0; i < paramVar.SelectSingleNode("SHORT-NAME").InnerText.Split('_').Length; i++)
                                            {
                                                payLN += paramVar.SelectSingleNode("SHORT-NAME").InnerText.Split('_')[i] + " ";
                                            }
                                            payLN = payLN.TrimEnd();
                                            if (payLN == "C4.24 MainRightMirrorUpCtrl")
                                                Console.Write("");
                                            string tempTrialPayLN = "";
                                            if (paramVar.SelectSingleNode("SHORT-NAME").InnerText.Split('_').Length > 2)
                                            {
                                                for (int i = 0; i < paramVar.SelectSingleNode("SHORT-NAME").InnerText.Split('_').Length; i++)
                                                {
                                                    if(i!=2)
                                                        tempTrialPayLN += paramVar.SelectSingleNode("SHORT-NAME").InnerText.Split('_')[i] + " ";
                                                }
                                                tempTrialPayLN = tempTrialPayLN.TrimEnd();
                                            }
                                            //remote key rolling countrer specific clause
                                            if (control.Name == tempTrialPayLN)
                                            {
                                                Console.WriteLine(control.Name);
                                                string payloadDesc = paramVar.SelectSingleNode("LONG-NAME").InnerText;
                                                PayloadInfo payload = new PayloadInfo();
                                                payload.Name = payloadDesc;
                                                //burada csvdeki isimlendirmelerin containl
                                                if (!reqList.Contains(paramVar.SelectSingleNode("DOP-REF").Attributes.Item(0).Value + "+" + payload.Name))
                                                {
                                                    reqList.Add(paramVar.SelectSingleNode("DOP-REF").Attributes.Item(0).Value + "+" + payload.Name);
                                                    idPaylNcontN.Add(paramVar.SelectSingleNode("DOP-REF").Attributes.Item(0).Value + "+" + payload.Name + "+" + control.Name);

                                                }
                                                foreach (XmlNode baseVar in baseVars)
                                                {
                                                    XmlNodeList diagDicts = baseVar.SelectNodes("DIAG-DATA-DICTIONARY-SPEC");
                                                    XmlNode matchingDop = null;
                                                    foreach (XmlNode diagDict in diagDicts)
                                                    {
                                                        XmlNodeList dtcDops = diagDict.SelectNodes("DATA-OBJECT-PROPS");
                                                        foreach (XmlNode dtcDop in dtcDops)
                                                        {
                                                            XmlNodeList dopDops = dtcDop.SelectNodes("DATA-OBJECT-PROP");
                                                            foreach (XmlNode dopDop in dopDops)
                                                            {
                                                                foreach (string element in reqList)
                                                                {
                                                                    if (dopDop.Attributes.Item(0).Value == element.Split('+')[0])
                                                                    {
                                                                        matchingDop = dopDop;
                                                                        XmlNodeList dops = dopDop.SelectNodes("DATA-OBJECT-PROP");
                                                                        string dopSName = matchingDop.SelectSingleNode("SHORT-NAME").InnerText;
                                                                        if (!dopDict.ContainsKey(element.Split('+')[0]))
                                                                            dopDict.Add(element.Split('+')[0], dopSName);
                                                                        string newElm = element + "+" + dopSName;
                                                                        if (!newReqList.Contains(newElm))
                                                                            newReqList.Add(newElm);
                                                                    }
                                                                }

                                                                foreach (string element in idPaylNcontN)
                                                                {
                                                                    if (dopDop.Attributes.Item(0).Value == element.Split('+')[0])
                                                                    {
                                                                        matchingDop = dopDop;
                                                                        XmlNodeList dops = dopDop.SelectNodes("DATA-OBJECT-PROP");
                                                                        string dopSName = matchingDop.SelectSingleNode("SHORT-NAME").InnerText;
                                                                        if (!dopDict.ContainsKey(element.Split('+')[0]))
                                                                            dopDict.Add(element.Split('+')[0], dopSName);
                                                                        string newElm = element.Split('+')[0] + "+" + element.Split('+')[1] + "+" + dopSName;
                                                                        if (!newReqList.Contains(newElm))
                                                                        {
                                                                            newReqList.Add(newElm);
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                                PayloadValue payloadVal = new PayloadValue();
                                                if (payloadDesc != null)
                                                {
                                                    XmlNodeList baseVariants = odxDoc.GetElementsByTagName("BASE-VARIANT");
                                                    foreach (XmlNode baseVariant in baseVariants)
                                                    {
                                                        XmlNodeList diagDataVars = baseVariant.SelectNodes("DIAG-DATA-DICTIONARY-SPEC");
                                                        foreach (XmlNode diagDataVar in diagDataVars)
                                                        {
                                                            XmlNodeList dtcDops = diagDataVar.SelectNodes("DTC-DOPS");
                                                            foreach (XmlNode dtcDop in dtcDops)
                                                            {
                                                                XmlNodeList dtcDopVariants = dtcDop.SelectNodes("DTC-DOP");
                                                                foreach (XmlNode dtcDopVariant in dtcDopVariants)
                                                                {
                                                                    XmlNodeList dtcsNodes = dtcDopVariant.SelectNodes("DTCS");
                                                                    foreach (XmlNode dtcsNode in dtcsNodes)
                                                                    {
                                                                        XmlNodeList dtcVariants = dtcsNode.SelectNodes("DTC");
                                                                        foreach (XmlNode dtcVariant in dtcVariants)
                                                                        {
                                                                            bool isDtcSet = false;
                                                                            XmlNode dtcDop_Text = dtcVariant.SelectSingleNode("TEXT");
                                                                            string[] textArr = dtcDop_Text.InnerText.Split(' ');
                                                                            //if (payload.Name.Contains("HornActivateSwitch") && textNsdgDict.ContainsKey("HornActivateSwitch"))
                                                                            //newReqList.Add(dtcVariant.SelectSingleNode("DOP-REF").Attributes.Item(0).Value+"+");
                                                                            string payloadValName = payload.Name;
                                                                            //Burasi 
                                                                            if (payload.Name != "")
                                                                            {
                                                                                if (textArr[0] == control.Name || textArr[0] == payload.Name || payload.Name.Contains(textArr[0]) || payload.Name.Contains(textArr[0].Replace("Output", "")))//textArr[0] == payload.Name.Substring(0,4)+"Activate"+payload.Name.Substring(4) )
                                                                                {
                                                                                    XmlNode dtcDop_sNameNode = dtcVariant.SelectSingleNode("SHORT-NAME");
                                                                                    if (dtcDop_sNameNode != null)
                                                                                    {
                                                                                        string tempStr = "";
                                                                                        for (int i = 3; i < dtcDop_sNameNode.InnerText.Length - 2; i++)
                                                                                        {
                                                                                            tempStr += dtcDop_sNameNode.InnerText[i];
                                                                                        }
                                                                                        payload.DTCCode = tempStr;
                                                                                        isDtcSet = true;
                                                                                    }
                                                                                }
                                                                            }
                                                                            if (isDtcSet)
                                                                                break;
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                            XmlNodeList dopsVar = diagDataVar.SelectNodes("DATA-OBJECT-PROPS");
                                                            foreach (XmlNode dopVar in dopsVar)
                                                            {
                                                                XmlNodeList dops = dopVar.SelectNodes("DATA-OBJECT-PROP");
                                                                foreach (XmlNode dop in dops)
                                                                {
                                                                    if (dopDict.Keys.Contains(dop.Attributes.Item(0).Value))
                                                                    {
                                                                        PayloadInfo tempPayload = new PayloadInfo();
                                                                        string dopID = dopDict.Keys.ElementAt(dopDict.Keys.ToList().IndexOf(dop.Attributes.Item(0).Value));
                                                                        string typName = dop.SelectSingleNode("SHORT-NAME").InnerText;
                                                                        string bitLenStr = dop.SelectSingleNode("DIAG-CODED-TYPE").SelectSingleNode("BIT-LENGTH").InnerText;
                                                                        int byteLen = int.Parse(bitLenStr) / 8;
                                                                        payload.Length = byteLen;
                                                                        payload.TypeName = typName;
                                                                        foreach (string descN in newReqList)
                                                                        {
                                                                            //dictten text arr 0 ve descn ile girme
                                                                            if (!usedReqList.Contains(descN) && (descN.Split('+')[2] == payload.TypeName && (descN.Split('+')[1].Equals(payload.Name)))) // || textNsdgDict.ContainsValue(payload.Name)))
                                                                            {
                                                                                if (!usedReqList.Contains(descN))
                                                                                    usedReqList.Add(descN);
                                                                                if (dop.SelectSingleNode("COMPU-METHOD").SelectSingleNode("COMPU-INTERNAL-TO-PHYS") != null)
                                                                                {
                                                                                    XmlNodeList valNodes = dop.SelectSingleNode("COMPU-METHOD").SelectSingleNode("COMPU-INTERNAL-TO-PHYS").SelectSingleNode("COMPU-SCALES").SelectNodes("COMPU-SCALE");
                                                                                    foreach (XmlNode valNode in valNodes)
                                                                                    {
                                                                                        PayloadValue tempPayVal = new PayloadValue();
                                                                                        if (valNode.SelectSingleNode("COMPU-CONST") != null)
                                                                                        {
                                                                                            payloadVal.FormattedValue = valNode.SelectSingleNode("COMPU-CONST").SelectSingleNode("VT").InnerText;
                                                                                            payloadVal.ValueString = (Convert.ToByte(valNode.SelectSingleNode("LOWER-LIMIT").InnerText).ToString("X2"));
                                                                                            tempPayVal.ValueString = payloadVal.ValueString;
                                                                                            tempPayVal.FormattedValue = payloadVal.FormattedValue;
                                                                                            if (payload.Values != null)
                                                                                                payload.Values.Add(tempPayVal);

                                                                                            else
                                                                                            {
                                                                                                payload.Values = new List<PayloadValue>
                                                                                            {
                                                                                                tempPayVal
                                                                                            };
                                                                                            }
                                                                                        }
                                                                                    }
                                                                                }
                                                                                /*if (payload.Name.Contains("Horn"))
                                                                                    Console.Write("");
                                                                                if ((textNsdgDict.ContainsValue(payload.Name) && control.Name.Replace("Ctrl", "") == textNsdgDict.Keys.ElementAt(textNsdgDict.Values.ToList().IndexOf(payload.Name))) || (textNsdgDict.ContainsValue(payload.Name) && control.Name.Replace(" Warning ", "") == textNsdgDict.Keys.ElementAt(textNsdgDict.Values.ToList().IndexOf(payload.Name))) || textNsdgDict.ContainsValue(payload.Name) && control.Name.Replace(" ", "Activate") == textNsdgDict.Keys.ElementAt(textNsdgDict.Values.ToList().IndexOf(payload.Name)))
                                                                                {
                                                                                    if (control.Name.Contains("Hazard Warning"))
                                                                                    {
                                                                                        Console.Write("");
                                                                                    }
                                                                                    XmlNodeList valNodes = dop.SelectSingleNode("COMPU-METHOD").SelectSingleNode("COMPU-INTERNAL-TO-PHYS").SelectSingleNode("COMPU-SCALES").SelectNodes("COMPU-SCALE");
                                                                                    foreach (XmlNode valNode in valNodes)
                                                                                    {
                                                                                        PayloadValue tempPayVal = new PayloadValue();
                                                                                        if (valNode.SelectSingleNode("COMPU-CONST") != null)
                                                                                        {
                                                                                            payloadVal.FormattedValue = valNode.SelectSingleNode("COMPU-CONST").SelectSingleNode("VT").InnerText;
                                                                                            payloadVal.ValueString = (Convert.ToByte(valNode.SelectSingleNode("LOWER-LIMIT").InnerText).ToString("X2"));
                                                                                            tempPayVal.ValueString = payloadVal.ValueString;
                                                                                            tempPayVal.FormattedValue = payloadVal.FormattedValue;
                                                                                            if (payload.Values != null)
                                                                                                payload.Values.Add(tempPayVal);

                                                                                            else
                                                                                            {
                                                                                                payload.Values = new List<PayloadValue>
                                                                                                {
                                                                                                    tempPayVal
                                                                                                };
                                                                                            }
                                                                                        }
                                                                                    }
                                                                                }*/
                                                                                else
                                                                                {
                                                                                    PayloadValue tempPayVal = new PayloadValue();
                                                                                    payload.Values = new List<PayloadValue>
                                                                                    {
                                                                                    tempPayVal
                                                                                    };
                                                                                }
                                                                                tempPayload.TypeName = payload.TypeName;
                                                                                tempPayload.Length = payload.Length;
                                                                                tempPayload.Values = payload.Values;
                                                                                //if (tempPayload.Values != null)
                                                                                //{ }
                                                                                PayloadInfo tempInfo = new PayloadInfo();
                                                                                tempInfo.TypeName = tempPayload.TypeName;
                                                                                tempInfo.Length = tempPayload.Length;
                                                                                tempInfo.Values = tempPayload.Values;
                                                                                //if(payload.TypeName == tempPayload.TypeName
                                                                                if (!typeNames.Contains(tempInfo.TypeName))
                                                                                {
                                                                                    payloadInfos.Add(tempInfo);
                                                                                    typeNames.Add(tempInfo.TypeName);
                                                                                }
                                                                                //payloadlardaki ayni typenamede olanlari tekrar ekliyor bunu cikarmam lazim
                                                                                if (tempPayload != null)
                                                                                {
                                                                                    if (response.Payloads != null)
                                                                                        response.Payloads.Add(tempPayload);
                                                                                    else
                                                                                    {
                                                                                        response.Payloads = new List<PayloadInfo>
                                                                                    {
                                                                                        tempPayload
                                                                                    };
                                                                                    }
                                                                                }

                                                                                tempPayload.DTCCode = payload.DTCCode;
                                                                                tempPayload.Name = payload.Name;
                                                                            }
                                                                        }
                                                                    }
                                                                    //else if()
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                            else if (control.Name == payLN)
                                            {

                                                Console.WriteLine(control.Name);
                                                string payloadDesc = paramVar.SelectSingleNode("LONG-NAME").InnerText;
                                                PayloadInfo payload = new PayloadInfo();
                                                payload.Name = payloadDesc;
                                                //burada csvdeki isimlendirmelerin containl
                                                if (!reqList.Contains(paramVar.SelectSingleNode("DOP-REF").Attributes.Item(0).Value + "+" + payload.Name))
                                                {
                                                    reqList.Add(paramVar.SelectSingleNode("DOP-REF").Attributes.Item(0).Value + "+" + payload.Name);
                                                    idPaylNcontN.Add(paramVar.SelectSingleNode("DOP-REF").Attributes.Item(0).Value + "+" + payload.Name + "+" + control.Name);

                                                }
                                                foreach (XmlNode baseVar in baseVars)
                                                {
                                                    XmlNodeList diagDicts = baseVar.SelectNodes("DIAG-DATA-DICTIONARY-SPEC");
                                                    XmlNode matchingDop = null;
                                                    foreach (XmlNode diagDict in diagDicts)
                                                    {
                                                        XmlNodeList dtcDops = diagDict.SelectNodes("DATA-OBJECT-PROPS");
                                                        foreach (XmlNode dtcDop in dtcDops)
                                                        {
                                                            XmlNodeList dopDops = dtcDop.SelectNodes("DATA-OBJECT-PROP");
                                                            foreach (XmlNode dopDop in dopDops)
                                                            {
                                                                foreach (string element in reqList)
                                                                {
                                                                    if (dopDop.Attributes.Item(0).Value == element.Split('+')[0])
                                                                    {
                                                                        matchingDop = dopDop;
                                                                        XmlNodeList dops = dopDop.SelectNodes("DATA-OBJECT-PROP");
                                                                        string dopSName = matchingDop.SelectSingleNode("SHORT-NAME").InnerText;
                                                                        if (!dopDict.ContainsKey(element.Split('+')[0]))
                                                                            dopDict.Add(element.Split('+')[0], dopSName);
                                                                        string newElm = element + "+" + dopSName;
                                                                        if (!newReqList.Contains(newElm))
                                                                            newReqList.Add(newElm);
                                                                    }
                                                                }

                                                                foreach (string element in idPaylNcontN)
                                                                {
                                                                    if (dopDop.Attributes.Item(0).Value == element.Split('+')[0])
                                                                    {
                                                                        matchingDop = dopDop;
                                                                        XmlNodeList dops = dopDop.SelectNodes("DATA-OBJECT-PROP");
                                                                        string dopSName = matchingDop.SelectSingleNode("SHORT-NAME").InnerText;
                                                                        if (!dopDict.ContainsKey(element.Split('+')[0]))
                                                                            dopDict.Add(element.Split('+')[0], dopSName);
                                                                        string newElm = element.Split('+')[0] + "+" + element.Split('+')[1] + "+" + dopSName;
                                                                        if (!newReqList.Contains(newElm))
                                                                        {
                                                                            newReqList.Add(newElm);
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                                PayloadValue payloadVal = new PayloadValue();
                                                if (payloadDesc != null)
                                                {
                                                    XmlNodeList baseVariants = odxDoc.GetElementsByTagName("BASE-VARIANT");
                                                    foreach (XmlNode baseVariant in baseVariants)
                                                    {
                                                        XmlNodeList diagDataVars = baseVariant.SelectNodes("DIAG-DATA-DICTIONARY-SPEC");
                                                        foreach (XmlNode diagDataVar in diagDataVars)
                                                        {
                                                            XmlNodeList dtcDops = diagDataVar.SelectNodes("DTC-DOPS");
                                                            foreach (XmlNode dtcDop in dtcDops)
                                                            {
                                                                XmlNodeList dtcDopVariants = dtcDop.SelectNodes("DTC-DOP");
                                                                foreach (XmlNode dtcDopVariant in dtcDopVariants)
                                                                {
                                                                    XmlNodeList dtcsNodes = dtcDopVariant.SelectNodes("DTCS");
                                                                    foreach (XmlNode dtcsNode in dtcsNodes)
                                                                    {
                                                                        XmlNodeList dtcVariants = dtcsNode.SelectNodes("DTC");
                                                                        foreach (XmlNode dtcVariant in dtcVariants)
                                                                        {
                                                                            bool isDtcSet = false;
                                                                            XmlNode dtcDop_Text = dtcVariant.SelectSingleNode("TEXT");
                                                                            string[] textArr = dtcDop_Text.InnerText.Split(' ');
                                                                            //if (payload.Name.Contains("HornActivateSwitch") && textNsdgDict.ContainsKey("HornActivateSwitch"))
                                                                            //newReqList.Add(dtcVariant.SelectSingleNode("DOP-REF").Attributes.Item(0).Value+"+");
                                                                            string payloadValName = payload.Name;
                                                                            //Burasi 
                                                                            if (payload.Name != "")
                                                                            {
                                                                                if (textArr[0] == control.Name || textArr[0] == payload.Name || payload.Name.Contains(textArr[0]) || payload.Name.Contains(textArr[0].Replace("Output", "")))//textArr[0] == payload.Name.Substring(0,4)+"Activate"+payload.Name.Substring(4) )
                                                                                {
                                                                                    XmlNode dtcDop_sNameNode = dtcVariant.SelectSingleNode("SHORT-NAME");
                                                                                    if (dtcDop_sNameNode != null)
                                                                                    {
                                                                                        string tempStr = "";
                                                                                        for (int i = 3; i < dtcDop_sNameNode.InnerText.Length - 2; i++)
                                                                                        {
                                                                                            tempStr += dtcDop_sNameNode.InnerText[i];
                                                                                        }
                                                                                        payload.DTCCode = tempStr;
                                                                                        isDtcSet = true;
                                                                                    }
                                                                                }
                                                                            }
                                                                            if (isDtcSet)
                                                                                break;
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                            XmlNodeList dopsVar = diagDataVar.SelectNodes("DATA-OBJECT-PROPS");
                                                            foreach (XmlNode dopVar in dopsVar)
                                                            {
                                                                XmlNodeList dops = dopVar.SelectNodes("DATA-OBJECT-PROP");
                                                                foreach (XmlNode dop in dops)
                                                                {
                                                                    if (dopDict.Keys.Contains(dop.Attributes.Item(0).Value))
                                                                    {
                                                                        PayloadInfo tempPayload = new PayloadInfo();
                                                                        string dopID = dopDict.Keys.ElementAt(dopDict.Keys.ToList().IndexOf(dop.Attributes.Item(0).Value));
                                                                        string typName = dop.SelectSingleNode("SHORT-NAME").InnerText;
                                                                        string bitLenStr = dop.SelectSingleNode("DIAG-CODED-TYPE").SelectSingleNode("BIT-LENGTH").InnerText;
                                                                        int byteLen = int.Parse(bitLenStr) / 8;
                                                                        payload.Length = byteLen;
                                                                        payload.TypeName = typName;
                                                                        foreach (string descN in newReqList)
                                                                        {
                                                                            //dictten text arr 0 ve descn ile girme
                                                                            if (!usedReqList.Contains(descN) && (descN.Split('+')[2] == payload.TypeName && (descN.Split('+')[1].Equals(payload.Name)))) // || textNsdgDict.ContainsValue(payload.Name)))
                                                                            {
                                                                                if (!usedReqList.Contains(descN))
                                                                                    usedReqList.Add(descN);
                                                                                if (dop.SelectSingleNode("COMPU-METHOD").SelectSingleNode("COMPU-INTERNAL-TO-PHYS") != null)
                                                                                {
                                                                                    XmlNodeList valNodes = dop.SelectSingleNode("COMPU-METHOD").SelectSingleNode("COMPU-INTERNAL-TO-PHYS").SelectSingleNode("COMPU-SCALES").SelectNodes("COMPU-SCALE");
                                                                                    foreach (XmlNode valNode in valNodes)
                                                                                    {
                                                                                        PayloadValue tempPayVal = new PayloadValue();
                                                                                        if (valNode.SelectSingleNode("COMPU-CONST") != null)
                                                                                        {
                                                                                            payloadVal.FormattedValue = valNode.SelectSingleNode("COMPU-CONST").SelectSingleNode("VT").InnerText;
                                                                                            payloadVal.ValueString = (Convert.ToByte(valNode.SelectSingleNode("LOWER-LIMIT").InnerText).ToString("X2"));
                                                                                            tempPayVal.ValueString = payloadVal.ValueString;
                                                                                            tempPayVal.FormattedValue = payloadVal.FormattedValue;
                                                                                            if (payload.Values != null)
                                                                                                payload.Values.Add(tempPayVal);

                                                                                            else
                                                                                            {
                                                                                                payload.Values = new List<PayloadValue>
                                                                                            {
                                                                                                tempPayVal
                                                                                            };
                                                                                            }
                                                                                        }
                                                                                    }
                                                                                }
                                                                                /*if (payload.Name.Contains("Horn"))
                                                                                    Console.Write("");
                                                                                if ((textNsdgDict.ContainsValue(payload.Name) && control.Name.Replace("Ctrl", "") == textNsdgDict.Keys.ElementAt(textNsdgDict.Values.ToList().IndexOf(payload.Name))) || (textNsdgDict.ContainsValue(payload.Name) && control.Name.Replace(" Warning ", "") == textNsdgDict.Keys.ElementAt(textNsdgDict.Values.ToList().IndexOf(payload.Name))) || textNsdgDict.ContainsValue(payload.Name) && control.Name.Replace(" ", "Activate") == textNsdgDict.Keys.ElementAt(textNsdgDict.Values.ToList().IndexOf(payload.Name)))
                                                                                {
                                                                                    if (control.Name.Contains("Hazard Warning"))
                                                                                    {
                                                                                        Console.Write("");
                                                                                    }
                                                                                    XmlNodeList valNodes = dop.SelectSingleNode("COMPU-METHOD").SelectSingleNode("COMPU-INTERNAL-TO-PHYS").SelectSingleNode("COMPU-SCALES").SelectNodes("COMPU-SCALE");
                                                                                    foreach (XmlNode valNode in valNodes)
                                                                                    {
                                                                                        PayloadValue tempPayVal = new PayloadValue();
                                                                                        if (valNode.SelectSingleNode("COMPU-CONST") != null)
                                                                                        {
                                                                                            payloadVal.FormattedValue = valNode.SelectSingleNode("COMPU-CONST").SelectSingleNode("VT").InnerText;
                                                                                            payloadVal.ValueString = (Convert.ToByte(valNode.SelectSingleNode("LOWER-LIMIT").InnerText).ToString("X2"));
                                                                                            tempPayVal.ValueString = payloadVal.ValueString;
                                                                                            tempPayVal.FormattedValue = payloadVal.FormattedValue;
                                                                                            if (payload.Values != null)
                                                                                                payload.Values.Add(tempPayVal);

                                                                                            else
                                                                                            {
                                                                                                payload.Values = new List<PayloadValue>
                                                                                                {
                                                                                                    tempPayVal
                                                                                                };
                                                                                            }
                                                                                        }
                                                                                    }
                                                                                }*/
                                                                                else
                                                                                {
                                                                                    PayloadValue tempPayVal = new PayloadValue();
                                                                                    payload.Values = new List<PayloadValue>
                                                                                    {
                                                                                    tempPayVal
                                                                                    };
                                                                                }
                                                                                tempPayload.TypeName = payload.TypeName;
                                                                                tempPayload.Length = payload.Length;
                                                                                tempPayload.Values = payload.Values;
                                                                                //if (tempPayload.Values != null)
                                                                                //{ }
                                                                                PayloadInfo tempInfo = new PayloadInfo();
                                                                                tempInfo.TypeName = tempPayload.TypeName;
                                                                                tempInfo.Length = tempPayload.Length;
                                                                                tempInfo.Values = tempPayload.Values;
                                                                                //if(payload.TypeName == tempPayload.TypeName
                                                                                if (!typeNames.Contains(tempInfo.TypeName))
                                                                                {
                                                                                    payloadInfos.Add(tempInfo);
                                                                                    typeNames.Add(tempInfo.TypeName);
                                                                                }
                                                                                //payloadlardaki ayni typenamede olanlari tekrar ekliyor bunu cikarmam lazim
                                                                                if (tempPayload != null)
                                                                                {
                                                                                    if (response.Payloads != null)
                                                                                        response.Payloads.Add(tempPayload);
                                                                                    else
                                                                                    {
                                                                                        response.Payloads = new List<PayloadInfo>
                                                                                    {
                                                                                        tempPayload
                                                                                    };
                                                                                    }
                                                                                }

                                                                                tempPayload.DTCCode = payload.DTCCode;
                                                                                tempPayload.Name = payload.Name;
                                                                            }
                                                                        }
                                                                    }
                                                                    //else if()
                                                                }
                                                            }
                                                        }
                                                    }
                                                }

                                            }
                                            //Special cases for QFs
                                            else if (paramVar.SelectSingleNode("SHORT-NAME").InnerText.Split('_')[paramVar.SelectSingleNode("SHORT-NAME").InnerText.Split('_').Length - 1] == "STRUCTURE")
                                            {
                                                if (control.Name.Contains("QF"))
                                                    Console.Write("");
                                                if (control.Name == "All Doors Lock and Ajar Input Signal")
                                                    Console.Write("");
                                                Console.WriteLine(control.Name);
                                                string payloadDesc = paramVar.SelectSingleNode("LONG-NAME").InnerText;
                                                PayloadInfo payload = new PayloadInfo();
                                                payload.Name = payloadDesc;
                                                //burada csvdeki isimlendirmelerin containl
                                                if (!reqList.Contains(paramVar.SelectSingleNode("DOP-REF").Attributes.Item(0).Value + "+" + payload.Name))
                                                {
                                                    reqList.Add(paramVar.SelectSingleNode("DOP-REF").Attributes.Item(0).Value + "+" + payload.Name);
                                                    idPaylNcontN.Add(paramVar.SelectSingleNode("DOP-REF").Attributes.Item(0).Value + "+" + payload.Name + "+" + control.Name);

                                                }
                                                foreach (XmlNode baseVar in baseVars)
                                                {
                                                    XmlNodeList diagDicts = baseVar.SelectNodes("DIAG-DATA-DICTIONARY-SPEC");
                                                    XmlNode matchingDop = null;
                                                    foreach (XmlNode diagDict in diagDicts)
                                                    {
                                                        XmlNodeList dtcDops = diagDict.SelectNodes("DATA-OBJECT-PROPS");
                                                        foreach (XmlNode dtcDop in dtcDops)
                                                        {
                                                            XmlNodeList dopDops = dtcDop.SelectNodes("DATA-OBJECT-PROP");
                                                            foreach (XmlNode dopDop in dopDops)
                                                            {
                                                                foreach (string element in reqList)
                                                                {
                                                                    if (dopDop.Attributes.Item(0).Value == element.Split('+')[0] )
                                                                    {
                                                                        matchingDop = dopDop;
                                                                        XmlNodeList dops = dopDop.SelectNodes("DATA-OBJECT-PROP");
                                                                        string dopSName = matchingDop.SelectSingleNode("SHORT-NAME").InnerText;
                                                                        if (!dopDict.ContainsKey(element.Split('+')[0]))
                                                                            dopDict.Add(element.Split('+')[0], dopSName);
                                                                        string newElm = element + "+" + dopSName;
                                                                        if (!newReqList.Contains(newElm))
                                                                            newReqList.Add(newElm);
                                                                    }
                                                                }

                                                                foreach (string element in idPaylNcontN)
                                                                {
                                                                    if (dopDop.Attributes.Item(0).Value == element.Split('+')[0])
                                                                    {
                                                                        matchingDop = dopDop;
                                                                        XmlNodeList dops = dopDop.SelectNodes("DATA-OBJECT-PROP");
                                                                        string dopSName = matchingDop.SelectSingleNode("SHORT-NAME").InnerText;
                                                                        if (!dopDict.ContainsKey(element.Split('+')[0]))
                                                                            dopDict.Add(element.Split('+')[0], dopSName);
                                                                        string newElm = element.Split('+')[0] + "+" + element.Split('+')[1] + "+" + dopSName;
                                                                        if (!newReqList.Contains(newElm))
                                                                        {
                                                                            newReqList.Add(newElm);
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                                PayloadValue payloadVal = new PayloadValue();
                                                if (payloadDesc != null)
                                                {
                                                    XmlNodeList baseVariants = odxDoc.GetElementsByTagName("BASE-VARIANT");
                                                    foreach (XmlNode baseVariant in baseVariants)
                                                    {
                                                        XmlNodeList diagDataVars = baseVariant.SelectNodes("DIAG-DATA-DICTIONARY-SPEC");
                                                        foreach (XmlNode diagDataVar in diagDataVars)
                                                        {
                                                            XmlNodeList dtcDops = diagDataVar.SelectNodes("DTC-DOPS");
                                                            foreach (XmlNode dtcDop in dtcDops)
                                                            {
                                                                XmlNodeList dtcDopVariants = dtcDop.SelectNodes("DTC-DOP");
                                                                foreach (XmlNode dtcDopVariant in dtcDopVariants)
                                                                {
                                                                    XmlNodeList dtcsNodes = dtcDopVariant.SelectNodes("DTCS");
                                                                    foreach (XmlNode dtcsNode in dtcsNodes)
                                                                    {
                                                                        XmlNodeList dtcVariants = dtcsNode.SelectNodes("DTC");
                                                                        foreach (XmlNode dtcVariant in dtcVariants)
                                                                        {
                                                                            bool isDtcSet = false;
                                                                            XmlNode dtcDop_Text = dtcVariant.SelectSingleNode("TEXT");
                                                                            string[] textArr = dtcDop_Text.InnerText.Split(' ');
                                                                            //if (payload.Name.Contains("HornActivateSwitch") && textNsdgDict.ContainsKey("HornActivateSwitch"))
                                                                            //newReqList.Add(dtcVariant.SelectSingleNode("DOP-REF").Attributes.Item(0).Value+"+");
                                                                            string payloadValName = payload.Name;
                                                                            //Burasi 
                                                                            if (payload.Name != "")
                                                                            {
                                                                                if (textArr[0] == control.Name || textArr[0] == payload.Name || payload.Name.Contains(textArr[0]) || payload.Name.Contains(textArr[0].Replace("Output", "")))//textArr[0] == payload.Name.Substring(0,4)+"Activate"+payload.Name.Substring(4) )
                                                                                {
                                                                                    XmlNode dtcDop_sNameNode = dtcVariant.SelectSingleNode("SHORT-NAME");
                                                                                    if (dtcDop_sNameNode != null)
                                                                                    {
                                                                                        string tempStr = "";
                                                                                        for (int i = 3; i < dtcDop_sNameNode.InnerText.Length - 2; i++)
                                                                                        {
                                                                                            tempStr += dtcDop_sNameNode.InnerText[i];
                                                                                        }
                                                                                        payload.DTCCode = tempStr;
                                                                                        isDtcSet = true;
                                                                                    }
                                                                                }
                                                                            }
                                                                            if (isDtcSet)
                                                                                break;
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                            XmlNodeList dopsVar = diagDataVar.SelectNodes("DATA-OBJECT-PROPS");
                                                            foreach (XmlNode dopVar in dopsVar)
                                                            {
                                                                XmlNodeList dops = dopVar.SelectNodes("DATA-OBJECT-PROP");
                                                                foreach (XmlNode dop in dops)
                                                                {
                                                                    if (dopDict.Keys.Contains(dop.Attributes.Item(0).Value))
                                                                    {
                                                                        PayloadInfo tempPayload = new PayloadInfo();
                                                                        string dopID = dopDict.Keys.ElementAt(dopDict.Keys.ToList().IndexOf(dop.Attributes.Item(0).Value));
                                                                        string typName = dop.SelectSingleNode("SHORT-NAME").InnerText;
                                                                        string bitLenStr = dop.SelectSingleNode("DIAG-CODED-TYPE").SelectSingleNode("BIT-LENGTH").InnerText;
                                                                        int byteLen = int.Parse(bitLenStr) / 8;
                                                                        payload.Length = byteLen;
                                                                        payload.TypeName = typName;
                                                                        foreach (string descN in newReqList)
                                                                        {
                                                                            //dictten text arr 0 ve descn ile girme
                                                                            if (!usedReqList.Contains(descN) && (descN.Split('+')[2] == payload.TypeName && (descN.Split('+')[1].Equals(payload.Name)))) // || textNsdgDict.ContainsValue(payload.Name)))
                                                                            {
                                                                                if (!usedReqList.Contains(descN))
                                                                                    usedReqList.Add(descN);
                                                                                if (dop.SelectSingleNode("COMPU-METHOD").SelectSingleNode("COMPU-INTERNAL-TO-PHYS") != null)
                                                                                {
                                                                                    XmlNodeList valNodes = dop.SelectSingleNode("COMPU-METHOD").SelectSingleNode("COMPU-INTERNAL-TO-PHYS").SelectSingleNode("COMPU-SCALES").SelectNodes("COMPU-SCALE");
                                                                                    foreach (XmlNode valNode in valNodes)
                                                                                    {
                                                                                        PayloadValue tempPayVal = new PayloadValue();
                                                                                        if (valNode.SelectSingleNode("COMPU-CONST") != null)
                                                                                        {
                                                                                            payloadVal.FormattedValue = valNode.SelectSingleNode("COMPU-CONST").SelectSingleNode("VT").InnerText;
                                                                                            payloadVal.ValueString = (Convert.ToByte(valNode.SelectSingleNode("LOWER-LIMIT").InnerText).ToString("X2"));
                                                                                            tempPayVal.ValueString = payloadVal.ValueString;
                                                                                            tempPayVal.FormattedValue = payloadVal.FormattedValue;
                                                                                            if (payload.Values != null)
                                                                                                payload.Values.Add(tempPayVal);

                                                                                            else
                                                                                            {
                                                                                                payload.Values = new List<PayloadValue>
                                                                                            {
                                                                                                tempPayVal
                                                                                            };
                                                                                            }
                                                                                        }
                                                                                    }
                                                                                }
                                                                                /*if (payload.Name.Contains("Horn"))
                                                                                    Console.Write("");
                                                                                if ((textNsdgDict.ContainsValue(payload.Name) && control.Name.Replace("Ctrl", "") == textNsdgDict.Keys.ElementAt(textNsdgDict.Values.ToList().IndexOf(payload.Name))) || (textNsdgDict.ContainsValue(payload.Name) && control.Name.Replace(" Warning ", "") == textNsdgDict.Keys.ElementAt(textNsdgDict.Values.ToList().IndexOf(payload.Name))) || textNsdgDict.ContainsValue(payload.Name) && control.Name.Replace(" ", "Activate") == textNsdgDict.Keys.ElementAt(textNsdgDict.Values.ToList().IndexOf(payload.Name)))
                                                                                {
                                                                                    if (control.Name.Contains("Hazard Warning"))
                                                                                    {
                                                                                        Console.Write("");
                                                                                    }
                                                                                    XmlNodeList valNodes = dop.SelectSingleNode("COMPU-METHOD").SelectSingleNode("COMPU-INTERNAL-TO-PHYS").SelectSingleNode("COMPU-SCALES").SelectNodes("COMPU-SCALE");
                                                                                    foreach (XmlNode valNode in valNodes)
                                                                                    {
                                                                                        PayloadValue tempPayVal = new PayloadValue();
                                                                                        if (valNode.SelectSingleNode("COMPU-CONST") != null)
                                                                                        {
                                                                                            payloadVal.FormattedValue = valNode.SelectSingleNode("COMPU-CONST").SelectSingleNode("VT").InnerText;
                                                                                            payloadVal.ValueString = (Convert.ToByte(valNode.SelectSingleNode("LOWER-LIMIT").InnerText).ToString("X2"));
                                                                                            tempPayVal.ValueString = payloadVal.ValueString;
                                                                                            tempPayVal.FormattedValue = payloadVal.FormattedValue;
                                                                                            if (payload.Values != null)
                                                                                                payload.Values.Add(tempPayVal);

                                                                                            else
                                                                                            {
                                                                                                payload.Values = new List<PayloadValue>
                                                                                                {
                                                                                                    tempPayVal
                                                                                                };
                                                                                            }
                                                                                        }
                                                                                    }
                                                                                }*/
                                                                                else
                                                                                {
                                                                                    PayloadValue tempPayVal = new PayloadValue();
                                                                                    payload.Values = new List<PayloadValue>
                                                                                    {
                                                                                    tempPayVal
                                                                                    };
                                                                                }
                                                                                tempPayload.TypeName = payload.TypeName;
                                                                                tempPayload.Length = payload.Length;
                                                                                tempPayload.Values = payload.Values;
                                                                                //if (tempPayload.Values != null)
                                                                                //{ }
                                                                                PayloadInfo tempInfo = new PayloadInfo();
                                                                                tempInfo.TypeName = tempPayload.TypeName;
                                                                                tempInfo.Length = tempPayload.Length;
                                                                                tempInfo.Values = tempPayload.Values;
                                                                                //if(payload.TypeName == tempPayload.TypeName
                                                                                if (!typeNames.Contains(tempInfo.TypeName))
                                                                                {
                                                                                    payloadInfos.Add(tempInfo);
                                                                                    typeNames.Add(tempInfo.TypeName);
                                                                                }
                                                                                //payloadlardaki ayni typenamede olanlari tekrar ekliyor bunu cikarmam lazim
                                                                                if (tempPayload != null)
                                                                                {
                                                                                    if (response.Payloads != null)
                                                                                        response.Payloads.Add(tempPayload);
                                                                                    else
                                                                                    {
                                                                                        response.Payloads = new List<PayloadInfo>
                                                                                    {
                                                                                        tempPayload
                                                                                    };
                                                                                    }
                                                                                }

                                                                                tempPayload.DTCCode = payload.DTCCode;
                                                                                tempPayload.Name = payload.Name;
                                                                            }
                                                                        }
                                                                    }
                                                                    //else if()
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                List<string> tempList = new List<string>();
                                                foreach (string listEl in idPaylNcontN)
                                                {
                                                    if (listEl.Contains("Field"))
                                                        Console.Write("");
                                                    if (listEl.Split('+')[2] == control.Name || control.Name.Contains(listEl.Split('+')[2]))
                                                        tempList.Add(listEl);
                                                }
                                                foreach (string listEl in tempList)
                                                {
                                                    string payloadDesc = listEl.Split('+')[1];
                                                    Console.WriteLine(control.Name);
                                                    if (listEl.Split('+')[2] == "All Doors Lock and Ajar Output Signal Status")
                                                        Console.WriteLine(control.Name);
                                                    string dopId = paramVar.SelectSingleNode("DOP-REF").Attributes.Item(0).Value;
                                                    //foreach o list donecek 
                                                    PayloadInfo payload = new PayloadInfo();
                                                    payload.Name = payloadDesc.Trim().Replace("\t", " ");
                                                    if (payload.Name == "C4.24 MainRightMirrorUpCtrl")
                                                        Console.Write("");
                                                    PayloadValue payloadVal = new PayloadValue();
                                                    if (payloadDesc != null)
                                                    {
                                                        XmlNodeList baseVariants = odxDoc.GetElementsByTagName("BASE-VARIANT");
                                                        foreach (XmlNode baseVariant in baseVariants)
                                                        {
                                                            XmlNodeList diagDataVars = baseVariant.SelectNodes("DIAG-DATA-DICTIONARY-SPEC");
                                                            foreach (XmlNode diagDataVar in diagDataVars)
                                                            {
                                                                XmlNodeList dtcDops = diagDataVar.SelectNodes("DTC-DOPS");
                                                                foreach (XmlNode dtcDop in dtcDops)
                                                                {
                                                                    XmlNodeList dtcDopVariants = dtcDop.SelectNodes("DTC-DOP");
                                                                    foreach (XmlNode dtcDopVariant in dtcDopVariants)
                                                                    {
                                                                        XmlNodeList dtcsNodes = dtcDopVariant.SelectNodes("DTCS");
                                                                        foreach (XmlNode dtcsNode in dtcsNodes)
                                                                        {
                                                                            XmlNodeList dtcVariants = dtcsNode.SelectNodes("DTC");
                                                                            foreach (XmlNode dtcVariant in dtcVariants)
                                                                            {
                                                                                bool isDtcSet = false;
                                                                                XmlNode dtcDop_Text = dtcVariant.SelectSingleNode("TEXT");
                                                                                string[] textArr = dtcDop_Text.InnerText.Split(' ');
                                                                                string payloadValName = payload.Name;
                                                                                //Burasi 
                                                                                if (payload.Name != "" && payload.Name != null)
                                                                                {
                                                                                    if (textArr[0] == control.Name || textArr[0] == payload.Name || payload.Name.Contains(textArr[0]) || payload.Name.Contains(textArr[0].Replace("Output", "")))//textArr[0] == payload.Name.Substring(0,4)+"Activate"+payload.Name.Substring(4) )
                                                                                    {
                                                                                        XmlNode dtcDop_sNameNode = dtcVariant.SelectSingleNode("SHORT-NAME");
                                                                                        if (dtcDop_sNameNode != null)
                                                                                        {
                                                                                            string tempStr = "";
                                                                                            for (int i = 3; i < dtcDop_sNameNode.InnerText.Length - 2; i++)
                                                                                                tempStr += dtcDop_sNameNode.InnerText[i];
                                                                                            payload.DTCCode = tempStr;
                                                                                            isDtcSet = true;
                                                                                        }
                                                                                    }
                                                                                }
                                                                                if (isDtcSet)
                                                                                    break;
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                                XmlNodeList dopsVar = diagDataVar.SelectNodes("DATA-OBJECT-PROPS");
                                                                foreach (XmlNode dopVar in dopsVar)
                                                                {
                                                                    XmlNodeList dops = dopVar.SelectNodes("DATA-OBJECT-PROP");
                                                                    foreach (XmlNode dop in dops)
                                                                    {
                                                                        if (dopDict.Keys.Contains(dop.Attributes.Item(0).Value))
                                                                        {
                                                                            PayloadInfo tempPayload = new PayloadInfo();
                                                                            string dopID = dopDict.Keys.ElementAt(dopDict.Keys.ToList().IndexOf(dop.Attributes.Item(0).Value));
                                                                            string typName = dop.SelectSingleNode("SHORT-NAME").InnerText;
                                                                            string bitLenStr = dop.SelectSingleNode("DIAG-CODED-TYPE").SelectSingleNode("BIT-LENGTH").InnerText;
                                                                            int byteLen = int.Parse(bitLenStr) / 8;
                                                                            payload.Length = byteLen;
                                                                            payload.TypeName = typName;
                                                                            if (listEl.Split('+')[2] == "All Doors Lock and Ajar Output Signal Status")
                                                                                Console.WriteLine(listEl.Split('+')[2]);
                                                                            foreach (string descN in newReqList)
                                                                            {
                                                                                //dictten text arr 0 ve descn ile girme
                                                                                if (!usedReqList.Contains(descN) && (descN.Split('+')[2] == payload.TypeName && (descN.Split('+')[1].Equals(payload.Name)))) //|| textNsdgDict.ContainsValue(payload.Name)))
                                                                                {
                                                                                    if (!usedReqList.Contains(descN) || control.Name == listEl.Split('+')[2])
                                                                                        usedReqList.Add(descN);
                                                                                    if (dop.SelectSingleNode("COMPU-METHOD").SelectSingleNode("COMPU-INTERNAL-TO-PHYS") != null)
                                                                                    {
                                                                                        XmlNodeList valNodes = dop.SelectSingleNode("COMPU-METHOD").SelectSingleNode("COMPU-INTERNAL-TO-PHYS").SelectSingleNode("COMPU-SCALES").SelectNodes("COMPU-SCALE");
                                                                                        foreach (XmlNode valNode in valNodes)
                                                                                        {
                                                                                            PayloadValue tempPayVal = new PayloadValue();
                                                                                            if (valNode.SelectSingleNode("COMPU-CONST") != null)
                                                                                            {
                                                                                                payloadVal.FormattedValue = valNode.SelectSingleNode("COMPU-CONST").SelectSingleNode("VT").InnerText;
                                                                                                payloadVal.ValueString = (Convert.ToByte(valNode.SelectSingleNode("LOWER-LIMIT").InnerText).ToString("X2"));
                                                                                                tempPayVal.ValueString = payloadVal.ValueString;
                                                                                                tempPayVal.FormattedValue = payloadVal.FormattedValue;
                                                                                                if (payload.Values != null)
                                                                                                    payload.Values.Add(tempPayVal);

                                                                                                else
                                                                                                {
                                                                                                    payload.Values = new List<PayloadValue>
                                                                                                    {
                                                                                                        tempPayVal
                                                                                                    };
                                                                                                }
                                                                                            }
                                                                                        }
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        PayloadValue tempPayVal = new PayloadValue();
                                                                                        payload.Values = new List<PayloadValue>
                                                                                        {
                                                                                            tempPayVal
                                                                                        };
                                                                                    }
                                                                                    tempPayload.TypeName = payload.TypeName;
                                                                                    tempPayload.Length = payload.Length;
                                                                                    tempPayload.Values = payload.Values;
                                                                                    //if (tempPayload.Values != null)
                                                                                    //{ }
                                                                                    PayloadInfo tempInfo = new PayloadInfo();
                                                                                    tempInfo.TypeName = tempPayload.TypeName;
                                                                                    tempInfo.Length = tempPayload.Length;
                                                                                    tempInfo.Values = tempPayload.Values;
                                                                                    //if(payload.TypeName == tempPayload.TypeName
                                                                                    if (!typeNames.Contains(tempInfo.TypeName))
                                                                                    {
                                                                                        payloadInfos.Add(tempInfo);
                                                                                        typeNames.Add(tempInfo.TypeName);
                                                                                    }
                                                                                    //payloadlardaki ayni typenamede olanlari tekrar ekliyor bunu cikarmam lazim
                                                                                    if (tempPayload != null)
                                                                                    {
                                                                                        if (response.Payloads != null)
                                                                                            response.Payloads.Add(tempPayload);
                                                                                        else
                                                                                        {
                                                                                            response.Payloads = new List<PayloadInfo>
                                                                                            {
                                                                                                tempPayload
                                                                                            };
                                                                                        }
                                                                                    }

                                                                                    tempPayload.DTCCode = payload.DTCCode;
                                                                                    tempPayload.Name = payload.Name;
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        //Payload ismi requestlerde yoksa pos responselardan bakma
                                        else if (paramVar.Attributes.Item(0).Value == "DATA" && paramVar.SelectSingleNode("DESC") != null)
                                        {
                                            XmlNode payloadDesc = paramVar.SelectSingleNode("DESC");
                                            PayloadInfo payload = new PayloadInfo();
                                            payload.Name = payloadDesc.InnerText.Trim().Replace("\t", " ");
                                            //burada csvdeki isimlendirmelerin containl
                                            if (payload.Name == "C4.24 MainRightMirrorUpCtrl")
                                                Console.Write("");
                                            if (!reqList.Contains(paramVar.SelectSingleNode("DOP-REF").Attributes.Item(0).Value + "+" + payload.Name))
                                            { 
                                                reqList.Add(paramVar.SelectSingleNode("DOP-REF").Attributes.Item(0).Value + "+" + payload.Name);

                                                idPaylNcontN.Add(paramVar.SelectSingleNode("DOP-REF").Attributes.Item(0).Value + "+" + payload.Name + "+" + control.Name);
                                            }

                                            foreach (XmlNode baseVar in baseVars)
                                            {
                                                XmlNodeList diagDicts = baseVar.SelectNodes("DIAG-DATA-DICTIONARY-SPEC");
                                                XmlNode matchingDop = null;
                                                foreach (XmlNode diagDict in diagDicts)
                                                {
                                                    XmlNodeList dtcDops = diagDict.SelectNodes("DATA-OBJECT-PROPS");
                                                    foreach (XmlNode dtcDop in dtcDops)
                                                    {
                                                        XmlNodeList dopDops = dtcDop.SelectNodes("DATA-OBJECT-PROP");
                                                        foreach (XmlNode dopDop in dopDops)
                                                        {
                                                            foreach (string element in reqList)
                                                            {
                                                                if (dopDop.Attributes.Item(0).Value == element.Split('+')[0])
                                                                {
                                                                    matchingDop = dopDop;
                                                                    XmlNodeList dops = dopDop.SelectNodes("DATA-OBJECT-PROP");
                                                                    string dopSName = matchingDop.SelectSingleNode("SHORT-NAME").InnerText;

                                                                    if (!dopDict.ContainsKey(element.Split('+')[0]))
                                                                        dopDict.Add(element.Split('+')[0], dopSName);
                                                                    string newElm = element + "+" + dopSName;
                                                                    if (!newReqList.Contains(newElm))
                                                                        newReqList.Add(newElm);
                                                                }
                                                            }

                                                            foreach (string element in idPaylNcontN)
                                                            {
                                                                if (dopDop.Attributes.Item(0).Value == element.Split('+')[0])
                                                                {
                                                                    if (element.Contains("Vestel Internal Software"))
                                                                        Console.Write("");
                                                                    matchingDop = dopDop;
                                                                    XmlNodeList dops = dopDop.SelectNodes("DATA-OBJECT-PROP");
                                                                    string dopSName = matchingDop.SelectSingleNode("SHORT-NAME").InnerText;
                                                                    if (!dopDict.ContainsKey(element.Split('+')[0]))
                                                                        dopDict.Add(element.Split('+')[0], dopSName);
                                                                    string newElm = element.Split('+')[0] + "+" + element.Split('+')[1] + "+" + dopSName;
                                                                    if (!newReqList.Contains(newElm))
                                                                    {
                                                                        newReqList.Add(newElm);
                                                                        idPaylNcontN.Remove(newElm);
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                            PayloadValue payloadVal = new PayloadValue();
                                            if (payloadDesc != null)
                                            {
                                                XmlNodeList baseVariants = odxDoc.GetElementsByTagName("BASE-VARIANT");
                                                foreach (XmlNode baseVariant in baseVariants)
                                                {
                                                    XmlNodeList diagDataVars = baseVariant.SelectNodes("DIAG-DATA-DICTIONARY-SPEC");
                                                    foreach (XmlNode diagDataVar in diagDataVars)
                                                    {
                                                        XmlNodeList dtcDops = diagDataVar.SelectNodes("DTC-DOPS");
                                                        foreach (XmlNode dtcDop in dtcDops)
                                                        {
                                                            XmlNodeList dtcDopVariants = dtcDop.SelectNodes("DTC-DOP");
                                                            foreach (XmlNode dtcDopVariant in dtcDopVariants)
                                                            {
                                                                XmlNodeList dtcsNodes = dtcDopVariant.SelectNodes("DTCS");
                                                                foreach (XmlNode dtcsNode in dtcsNodes)
                                                                {
                                                                    XmlNodeList dtcVariants = dtcsNode.SelectNodes("DTC");
                                                                    foreach (XmlNode dtcVariant in dtcVariants)
                                                                    {
                                                                        bool isDtcSet = false;
                                                                        XmlNode dtcDop_Text = dtcVariant.SelectSingleNode("TEXT");
                                                                        string[] textArr = dtcDop_Text.InnerText.Split(' ');
                                                                        //if (payload.Name.Contains("HornActivateSwitch") && textNsdgDict.ContainsKey("HornActivateSwitch"))
                                                                        //newReqList.Add(dtcVariant.SelectSingleNode("DOP-REF").Attributes.Item(0).Value+"+");
                                                                        string payloadValName = payload.Name;
                                                                        //Burasi 
                                                                        if (payload.Name != "")
                                                                        {
                                                                            if (textArr[0] == control.Name || textArr[0] == payload.Name || payload.Name.Contains(textArr[0]) || payload.Name.Contains(textArr[0].Replace("Output", "")))//textArr[0] == payload.Name.Substring(0,4)+"Activate"+payload.Name.Substring(4) )
                                                                            {
                                                                                XmlNode dtcDop_sNameNode = dtcVariant.SelectSingleNode("SHORT-NAME");
                                                                                if (dtcDop_sNameNode != null)
                                                                                {
                                                                                    string tempStr = "";
                                                                                    for (int i = 3; i < dtcDop_sNameNode.InnerText.Length - 2; i++)
                                                                                    {
                                                                                        tempStr += dtcDop_sNameNode.InnerText[i];
                                                                                    }
                                                                                    payload.DTCCode = tempStr;
                                                                                    isDtcSet = true;
                                                                                }
                                                                            }
                                                                        }
                                                                        if (isDtcSet)
                                                                            break;
                                                                    }
                                                                }
                                                            }
                                                        }
                                                        XmlNodeList dopsVar = diagDataVar.SelectNodes("DATA-OBJECT-PROPS");
                                                        foreach (XmlNode dopVar in dopsVar)
                                                        {
                                                            XmlNodeList dops = dopVar.SelectNodes("DATA-OBJECT-PROP");
                                                            foreach (XmlNode dop in dops)
                                                            {
                                                                if (dopDict.Keys.Contains(dop.Attributes.Item(0).Value))
                                                                {
                                                                    PayloadInfo tempPayload = new PayloadInfo();
                                                                    string dopID = dopDict.Keys.ElementAt(dopDict.Keys.ToList().IndexOf(dop.Attributes.Item(0).Value));
                                                                    string typName = dop.SelectSingleNode("SHORT-NAME").InnerText;
                                                                    string bitLenStr = dop.SelectSingleNode("DIAG-CODED-TYPE").SelectSingleNode("BIT-LENGTH").InnerText;
                                                                    int byteLen = int.Parse(bitLenStr) / 8;
                                                                    payload.Length = byteLen;
                                                                    payload.TypeName = typName;
                                                                    foreach (string descN in newReqList)
                                                                    {
                                                                        if (descN.Contains("Vestel Internal S"))
                                                                            Console.Write("");
                                                                        //dictten text arr 0 ve descn ile girme
                                                                        if (reqList.Contains(descN.Split('+')[0]+"+"+descN.Split('+')[1])&&!usedReqList.Contains(descN) && (descN.Split('+')[2] == payload.TypeName && (descN.Split('+')[1].Equals(payload.Name)))) // || textNsdgDict.ContainsValue(payload.Name)))
                                                                        {
                                                                            if (payload.Name == "C4.24 MainRightMirrorUpCtrl")
                                                                                Console.Write("");
                                                                            if (!usedReqList.Contains(descN))
                                                                                 usedReqList.Add(descN);
                                                                            if (dop.SelectSingleNode("COMPU-METHOD").SelectSingleNode("COMPU-INTERNAL-TO-PHYS") != null)
                                                                            {
                                                                                XmlNodeList valNodes = dop.SelectSingleNode("COMPU-METHOD").SelectSingleNode("COMPU-INTERNAL-TO-PHYS").SelectSingleNode("COMPU-SCALES").SelectNodes("COMPU-SCALE");
                                                                                foreach (XmlNode valNode in valNodes)
                                                                                {
                                                                                    PayloadValue tempPayVal = new PayloadValue();
                                                                                    if (valNode.SelectSingleNode("COMPU-CONST") != null)
                                                                                    {
                                                                                        payloadVal.FormattedValue = valNode.SelectSingleNode("COMPU-CONST").SelectSingleNode("VT").InnerText;
                                                                                        payloadVal.ValueString = (Convert.ToByte(valNode.SelectSingleNode("LOWER-LIMIT").InnerText).ToString("X2"));
                                                                                        tempPayVal.ValueString = payloadVal.ValueString;
                                                                                        tempPayVal.FormattedValue = payloadVal.FormattedValue;
                                                                                        if (payload.Values != null)
                                                                                            payload.Values.Add(tempPayVal);

                                                                                        else
                                                                                        {
                                                                                            payload.Values = new List<PayloadValue>
                                                                                            {
                                                                                                tempPayVal
                                                                                            };
                                                                                        }
                                                                                    }
                                                                                }
                                                                            }
                                                                            /*if (payload.Name.Contains("Horn"))
                                                                                Console.Write("");
                                                                            if ((textNsdgDict.ContainsValue(payload.Name) && control.Name.Replace("Ctrl", "") == textNsdgDict.Keys.ElementAt(textNsdgDict.Values.ToList().IndexOf(payload.Name))) || (textNsdgDict.ContainsValue(payload.Name) && control.Name.Replace(" Warning ", "") == textNsdgDict.Keys.ElementAt(textNsdgDict.Values.ToList().IndexOf(payload.Name))) || textNsdgDict.ContainsValue(payload.Name) && control.Name.Replace(" ", "Activate") == textNsdgDict.Keys.ElementAt(textNsdgDict.Values.ToList().IndexOf(payload.Name)))
                                                                            {
                                                                                if (control.Name.Contains("Hazard Warning"))
                                                                                {
                                                                                    Console.Write("");
                                                                                }
                                                                                XmlNodeList valNodes = dop.SelectSingleNode("COMPU-METHOD").SelectSingleNode("COMPU-INTERNAL-TO-PHYS").SelectSingleNode("COMPU-SCALES").SelectNodes("COMPU-SCALE");
                                                                                foreach (XmlNode valNode in valNodes)
                                                                                {
                                                                                    PayloadValue tempPayVal = new PayloadValue();
                                                                                    if (valNode.SelectSingleNode("COMPU-CONST") != null)
                                                                                    {
                                                                                        payloadVal.FormattedValue = valNode.SelectSingleNode("COMPU-CONST").SelectSingleNode("VT").InnerText;
                                                                                        payloadVal.ValueString = (Convert.ToByte(valNode.SelectSingleNode("LOWER-LIMIT").InnerText).ToString("X2"));
                                                                                        tempPayVal.ValueString = payloadVal.ValueString;
                                                                                        tempPayVal.FormattedValue = payloadVal.FormattedValue;
                                                                                        if (payload.Values != null)
                                                                                            payload.Values.Add(tempPayVal);

                                                                                        else
                                                                                        {
                                                                                            payload.Values = new List<PayloadValue>
                                                                                            {
                                                                                                tempPayVal
                                                                                            };
                                                                                        }
                                                                                    }
                                                                                }
                                                                            }*/
                                                                            else
                                                                            {
                                                                                PayloadValue tempPayVal = new PayloadValue();
                                                                                payload.Values = new List<PayloadValue>
                                                                                {
                                                                                    tempPayVal
                                                                                };
                                                                            }
                                                                            tempPayload.TypeName = payload.TypeName;
                                                                            tempPayload.Length = payload.Length;
                                                                            tempPayload.Values = payload.Values;
                                                                            //if (tempPayload.Values != null)
                                                                            //{ }
                                                                            PayloadInfo tempInfo = new PayloadInfo();
                                                                            tempInfo.TypeName = tempPayload.TypeName;
                                                                            tempInfo.Length = tempPayload.Length;
                                                                            tempInfo.Values = tempPayload.Values;
                                                                            //if(payload.TypeName == tempPayload.TypeName
                                                                            if (!typeNames.Contains(tempInfo.TypeName))
                                                                            {
                                                                                payloadInfos.Add(tempInfo);
                                                                                typeNames.Add(tempInfo.TypeName);
                                                                            }
                                                                            //payloadlardaki ayni typenamede olanlari tekrar ekliyor bunu cikarmam lazim
                                                                            if (tempPayload != null)
                                                                            {
                                                                                if (response.Payloads != null)
                                                                                    response.Payloads.Add(tempPayload);
                                                                                else
                                                                                {
                                                                                    response.Payloads = new List<PayloadInfo>
                                                                                    {
                                                                                        tempPayload
                                                                                    };
                                                                                }
                                                                            }

                                                                            tempPayload.DTCCode = payload.DTCCode;
                                                                            tempPayload.Name = payload.Name;
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }









                //XmlNodeList negRespsVariants = ecuVariant.SelectNodes("NEG-RESPONSES");
                //foreach (XmlNode negRespsVariant in negRespsVariants)
                //{
                //    XmlNodeList negRespVariants = negRespsVariant.SelectNodes("NEG-RESPONSE");
                //    foreach (XmlNode respVariant in negRespVariants)
                //    {
                //        List<ResponseInfo> responses = new List<ResponseInfo>();
                //        XmlNode respName = respVariant.SelectSingleNode("SHORT-NAME");
                //        foreach (ControlInfo control in controls)
                //        {
                //            string[] sNameArr = respName.InnerText.Split('_');
                //            string sName = "";
                //            for (int i = 1; i < sNameArr.Length - 1; i++)
                //            {
                //                sName += sNameArr[i] + " ";
                //            }
                //            sName = sName.TrimEnd(' ');
                //            if (sName.Equals(control.Name))
                //            {
                //                XmlNodeList paramsVars = respVariant.SelectNodes("PARAMS");
                //                foreach (XmlNode paramsVar in paramsVars)
                //                {
                //                    foreach (XmlNode paramVar in paramsVar.SelectNodes("PARAM"))
                //                    {
                //                        if (paramVar.Attributes.Item(0).Value == "SERVICE-ID")
                //                        {
                //                            XmlNode serviceId = paramVar.SelectSingleNode("CODED-VALUE");
                //                            ResponseInfo response = new ResponseInfo();
                //                            response.ServiceID = Convert.ToByte(serviceId.InnerText);
                //                            responses.Add(response);
                //                            if(control.Responses == null)
                //                            {
                //                                control.Responses = responses;
                //                            }
                //                            else
                //                            {
                //                                control.Responses.Add(response);
                //                            }
                //                        }

                //                    }
                //                }
                //            }
                //        }
                //    }
                //}
            }
            //XmlNodeList baseVariants = odxDoc.GetElementsByTagName("BASE-VARIANT");
            //foreach(XmlNode baseVariant in baseVariants)
            //{
            //    XmlNodeList diagDataVars = baseVariant.SelectNodes("DIAG-DATA-DICTIONARY-SPEC");
            //    foreach (XmlNode diagDataVar in diagDataVars)
            //    {
            //        XmlNodeList dtcDops = diagDataVar.SelectNodes("DTC-DOPS");
            //        foreach (XmlNode dtcDop in dtcDops)
            //        {
            //            XmlNodeList dtcDopVariants = dtcDop.SelectNodes("DTC-DOP");
            //            foreach (XmlNode dtcDopVariant in dtcDopVariants)
            //            {
            //                XmlNodeList dtcsNodes = dtcDopVariant.SelectNodes("DTCS");
            //                foreach (XmlNode dtcsNode in dtcsNodes)
            //                {
            //                    XmlNodeList dtcVariants = dtcsNode.SelectNodes("DTC");
            //                    foreach (XmlNode dtcVariant in dtcVariants)
            //                    {
            //                        XmlNode dtcDop_Text = dtcVariant.SelectSingleNode("TEXT");
            //                        string[] textArr = dtcDop_Text.InnerText.Split(' ');          
            //                        foreach (ControlInfo cont in controls)
            //                        {
            //                            if (textArr[0] == cont.Name) {
            //                                XmlNode dtcDop_sNameNode = dtcVariant.SelectSingleNode("SHORT-NAME");
            //                                if (dtcDop_sNameNode != null)
            //                                {
            //                                    string tempStr = "";
            //                                    for (int i = 3; i < dtcDop_sNameNode.InnerText.Length-2; i++)
            //                                    {
            //                                        tempStr += dtcDop_sNameNode.InnerText[i];
            //                                    }                      
            //                                }
            //                            }
            //                        }
            //                    }
            //                }
            //                XmlNode diagCodeTypeNode = dtcDopVariant.SelectSingleNode("DIAG-CODED-TYPE");
            //                if (diagCodeTypeNode != null)
            //                {
            //                    XmlAttributeCollection attributes = diagCodeTypeNode.Attributes;
            //                    Console.WriteLine("DIAG-CODED-TYPE: ");
            //                    foreach (XmlAttribute attribute in attributes)
            //                    {
            //                        Console.WriteLine($"{attribute.Name}: {attribute.Value}");
            //                    }

            //                    Console.WriteLine("BIT-LENGTH: " + diagCodeTypeNode.InnerText);
            //                }

            //                XmlNode physTypeNode = dtcDopVariant.SelectSingleNode("PHYSICAL-TYPE");
            //                if (physTypeNode != null)
            //                {
            //                    XmlAttributeCollection attributes = physTypeNode.Attributes;
            //                    Console.WriteLine("PHYSICAL-TYPE: ");
            //                    foreach (XmlAttribute attribute in attributes)
            //                    {
            //                        Console.WriteLine($"{attribute.Name}: {attribute.Value}");
            //                    }
            //                }

            //                XmlNode compuNode = dtcDopVariant.SelectSingleNode("COMPU-METHOD");
            //                if (compuNode != null)
            //                {
            //                    Console.WriteLine("CATEGORY: " + compuNode.InnerText);
            //                }
            //                //XmlNodeList dtcsNodes = dtcDopVariant.SelectNodes("DTCS");
            //                foreach (XmlNode dtcsNode in dtcsNodes)
            //                {
            //                    XmlNodeList dtcVariants = dtcsNode.SelectNodes("DTC");
            //                    foreach (XmlNode dtcVariant in dtcVariants)
            //                    {
            //                        XmlNode sNameNode = dtcVariant.SelectSingleNode("SHORT-NAME");
            //                        XmlNode tCodeNode = dtcVariant.SelectSingleNode("TROUBLE-CODE");
            //                        XmlNode dispTCodeNode = dtcVariant.SelectSingleNode("DISPLAY-TROUBLE-CODE");
            //                        XmlNode textNode = dtcVariant.SelectSingleNode("TEXT");
            //                        XmlNodeList sdgsVariants = dtcVariant.SelectNodes("SDGS");
            //                        foreach (XmlNode sdgsVariant in sdgsVariants)
            //                        {
            //                            XmlNodeList sdgVariants = sdgsVariant.SelectNodes("SDG");
            //                            foreach (XmlNode sdgVariant in sdgVariants)
            //                            {
            //                                XmlNodeList sdVariants = sdgVariant.SelectNodes("SDG-CAPTION");
            //                                foreach (XmlNode sdVariant in sdVariants)
            //                                {
            //                                    XmlNode sd_sNameNode = sdVariant.SelectSingleNode("SHORT-NAME");
            //                                    if (sd_sNameNode != null)
            //                                    {
            //                                        Console.WriteLine("SHORT-NAME: " + sd_sNameNode.InnerText);
            //                                    }

            //                                    XmlNode sd_lNameNode = sdVariant.SelectSingleNode("LONG-NAME");
            //                                    if (sd_lNameNode != null)
            //                                    {
            //                                        Console.WriteLine("LONG-NAME: " + sd_lNameNode.InnerText);
            //                                    }

            //                                    XmlNode descNode = sdVariant.SelectSingleNode("DESC");
            //                                    if (descNode != null)
            //                                    {
            //                                        Console.WriteLine("DESC: " + descNode.InnerText);
            //                                    }
            //                                }

            //                                XmlNode sdNode = sdgVariant.SelectSingleNode("SD");
            //                                if (sdNode != null)
            //                                {
            //                                    Console.WriteLine("SD: " + sdNode.InnerText);
            //                                }
            //                            }
            //                        }
            //                    }
            //                }
            //            }

            //        }
            //    }
            //}
            

            return new ConfigurationInfo
            {
                Services = services,
                Sessions = sessions,
                Controls = controls,
                Payloads = payloadInfos,
                DTCFailureTypes = dTCFailures,
                //EnvironmentalTest = null
            };
        }


        internal static ConfigurationInfo Parse(string filePath)
        {
            XDocument doc = XDocument.Load(filePath);

            if (FormMain.isMdxFile)
            {
                var sessions = doc.Descendants("PROTOCOL").Descendants("APPLICATION_LAYER").Descendants("SESSIONS_SUPPORTED").Descendants("SESSION")
                .Select(s => new SessionInfo
                {
                    ID = Convert.ToByte(s.Element("NUMBER").Value, 16),
                    Name = s.Element("NAME").Value,
                    AvailableServices = new List<byte> { 0x22 },
                })
                .ToList();

                var services = doc.Descendants("PROTOCOL").Descendants("APPLICATION_LAYER").Descendants("SERVICES_SUPPORTED").Descendants("SERVICE")
                    .Select(s => new ServiceInfo
                    {
                        RequestID = s.Element("NUMBER") != null ? Convert.ToByte(s.Element("NUMBER").Value, 16) : (byte)0,
                        ResponseID = s.Element("NUMBER") != null ? (byte)(Convert.ToByte(s.Element("NUMBER").Value, 16) + 0x40) : (byte)0,
                        Name = s.Element("NAME").Value,
                        ResponseIndex = s.Element("NUMBER") != null
                            ? s.Element("NUMBER").Value == "0x22"
                                ? 3
                                : s.Element("NUMBER").Value == "0x2f"
                                    ? 4
                                    : 0
                            : 0,
                        Sessions = s.Attribute("SESSION_REFS") != null
                                      ? s.Attribute("SESSION_REFS").Value
                                            .Split(' ')
                                            .Select(refValue => byte.Parse(refValue.Substring(refValue.LastIndexOf('_') + 1)))
                                            .ToList()
                                      : new List<byte>()
                    })
                    .ToList();

                var payloads = new List<PayloadInfo> { };
                var counter = 0;


                var controls = doc.Descendants("ECU_DATA").Descendants("DATA_IDENTIFIERS").Descendants("DID")
                    .Select(c => new ControlInfo
                    {
                        Address = Convert.ToUInt16(c.Element("NUMBER").Value, 16),
                        Name = c.Element("NAME").Value,
                        //Type = c.Element("Type").Value,
                        Group = c.Attribute("ID").Value.Contains("did_DE") ? "ECU_DID" : "DID",

                        Services = c.Descendants("ACCESS_PARAMETERS")
                                    .Descendants()
                                    .Where(x => x.Attribute("SERVICE_REFS") != null)
                                    .SelectMany(x => x.Attribute("SERVICE_REFS").Value.Split(' ')
                                                    .Select(refValue => byte.Parse(refValue.Substring(refValue.Length - 2), System.Globalization.NumberStyles.HexNumber)))
                                    .ToList(),

                        SessionActiveException = new List<byte>(),
                        SessionInactiveException = new List<byte>(),

                        Responses = new List<ResponseInfo>
                        {
                        new ResponseInfo
                        {
                              ServiceID = 0x62,
                              Payloads = c.Elements("SUB_FIELD")?.Select(x =>
                                {
                                    var least_sig_bit = double.Parse(x.Element("LEAST_SIG_BIT")?.Value);
                                    var most_sig_bit = double.Parse(x.Element("MOST_SIG_BIT")?.Value);
                                    var length = 1;
                                    if(least_sig_bit != most_sig_bit)
                                    {
                                        length = (int)Math.Round(((most_sig_bit + 1) - least_sig_bit)/8);
                                    }
                                    var descriptionValue = x.Element("DESCRIPTION")?.Value;
                                    string[] parts = descriptionValue?.Split(new[] { ' ', '\t', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                                    var descriptionPart = parts != null && parts.Length > 1 ? parts[1].Trim() : null;

                                    var dtcCode = doc.Descendants("ECU_DATA")
                                                     .Descendants("DIAGNOSTIC_TROUBLE_CODES")
                                                     .Descendants("DTC")
                                                     .Where(dtc => dtc.Element("DESCRIPTION")?.Value == descriptionPart)
                                                     .Select(dtc => dtc.Element("NUMBER")?.Value)
                                                     .FirstOrDefault();

                                    var dataType = x.Element("DATA_DEFINITION").Element("DATA_TYPE").Value;
                                    List<PayloadValue> values;

                                    if (dataType == "enumerated")
                                    {
                                        values = x.Element("DATA_DEFINITION")
                                          .Element("ENUMERATED_PARAMETERS")
                                          .Elements("ENUM_MEMBER")
                                          .Select(z =>
                                          {
                                              string valueString = z.Element("ENUM_VALUE").Value;
                                              byte byteValue = Convert.ToByte(valueString, 16);
                                              string formattedValueString = byteValue.ToString("X2");

                                              return new PayloadValue
                                              {
                                                  ValueString = formattedValueString,
                                                  FormattedValue = z.Element("DESCRIPTION").Value,
                                              };
                                          })
                                          .ToList();
                                    }
                                    else if (dataType == "bytes")
                                    {
                                        values = new List<PayloadValue>
                                        {
                                            new PayloadValue
                                            {
                                                ValueString = "0",
                                                FormattedValue = "bytes"
                                            }
                                        };
                                    }
                                    else if (dataType == "unsigned")
                                    {
                                        values = new List<PayloadValue>
                                        {
                                            new PayloadValue
                                            {
                                                ValueString = "0",
                                                FormattedValue = "unsigned"
                                            }
                                        };
                                    }
                                    else
                                    {
                                        values = new List<PayloadValue>();
                                    }

                                    string existingTypeName = null;

                                    bool valueExists = payloads.Any(p => p.Length == length && p.Values.Select(v => v.ValueString).SequenceEqual(values.Select(v => v.ValueString)));

                                    if (!valueExists)
                                    {
                                        counter += 1;
                                        payloads.Add(new PayloadInfo
                                        {
                                            Length = length,
                                            TypeName = "TypeName" + counter,
                                            Values = values
                                        });
                                    }
                                    else
                                    {
                                        existingTypeName = payloads.First(p => p.Values.Select(v => v.ValueString).SequenceEqual(values.Select(v => v.ValueString))).TypeName;
                                    }


                                    return new PayloadInfo
                                    {
                                        Name = descriptionValue,
                                        DTCCode = dtcCode,
                                        TypeName = existingTypeName ?? "TypeName" + counter
                                    };


                                }).ToList() ?? new List<PayloadInfo>()
                        }
                        }

                    }).ToList();

                var dtcFailureTypes = doc.Descendants("ECU_DATA").Descendants("DIAGNOSTIC_TROUBLE_CODES").Descendants("DTC_FAILURE_TYPES_SUPPORTED").Descendants("DTC_FAILURE_TYPE")
                    .Select(t => new DTCFailure
                    {
                        Value = Convert.ToByte(t.Element("NUMBER").Value, 16),
                        Description = t.Element("DESCRIPTION")?.Value,
                    })
                    .ToList();

                Console.WriteLine(payloads);

                return new ConfigurationInfo
                {
                    Sessions = sessions,
                    Services = services,
                    Controls = controls,
                    Payloads = payloads,
                    DTCFailureTypes = dtcFailureTypes
                };

            }
            else
            {
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
                                        DTCCode = y.Attribute("dtcCode")?.Value,
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
                                FormattedValue = x.Value,
                                IsClose = x.Attribute("isClose")?.Value == "true",
                                IsOpen = x.Attribute("isOpen")?.Value == "true",
                            }).ToList(),
                    })
                    .ToList();

                var dtcFailureTypes = doc.Descendants("DTCFailureTypes").Descendants("Type")
                    .Select(t => new DTCFailure
                    {
                        Value = Convert.ToByte(t.Attribute("value").Value, 16),
                        Description = t.Value,
                    })
                    .ToList();


                #region Environmental Test

                var environmentalTest = doc.Descendants("EnvironmentalTest")
                    .Select(t => new EnvironmentalTest
                    {
                        ConnectionMappings = t.Element("ConnectionMappings").Elements("Mapping")
                            .Select(m => new Mapping
                            {
                                Input = m.Elements("Input")
                                    .Select(f => new Function
                                    {
                                        Name = f.Value,
                                        Control = f.Attribute("parent")?.Value ?? null,
                                    }).First(),
                                Output = m.Elements("Output")
                                    .Select(f => new Function
                                    {
                                        Name = f.Value,
                                        Control = f.Attribute("parent")?.Value ?? null,
                                    }).First(),
                            }).ToList(),
                        Environments = t.Element("Environments").Elements("Environment")
                            .Select(e => new Environment
                            {
                                Name = e.Element("Name").Value,
                                EnvironmentalConfig = e.Descendants("EnvironmentalConfig")
                                    .Select(c => new EnvironmentalConfig
                                    {
                                        CycleTime = int.Parse(c.Element("CycleTime").Value),
                                        TxInterval = int.Parse(c.Element("TxInterval").Value),
                                        StartCycleIndex = int.Parse(c.Element("StartCycleIndex").Value),
                                        EndCycleIndex = int.Parse(c.Element("EndCycleIndex").Value),
                                        PWMDutyOpenValue = short.Parse(c.Element("PWMDutyOpenValue").Value),
                                        PWMDutyCloseValue = short.Parse(c.Element("PWMDutyCloseValue").Value),
                                        PWMFreqOpenValue = byte.Parse(c.Element("PWMFreqOpenValue").Value),
                                        PWMFreqCloseValue = byte.Parse(c.Element("PWMFreqCloseValue").Value),
                                    }).First(),
                                Cycles = e.Element("Cycles").Elements("Cycle")
                                    .Select(c => new Cycle
                                    {
                                        Name = c.Element("Name").Value,
                                        OpenAt = int.Parse(c.Element("OpenAt").Value),
                                        CloseAt = int.Parse(c.Element("CloseAt").Value),
                                        Functions = c.Element("Functions").Elements("Function")
                                            .Select(f => new Function
                                            {
                                                Control = f.Attribute("control")?.Value,
                                                ControlInfo = controls.FirstOrDefault(x => x.Name == f.Attribute("control")?.Value),
                                                Scenario = f.Attribute("scenario")?.Value,
                                                Payloads = f.Elements("Payload").Select(x => x.Value).ToList()
                                            }).ToList(),
                                    }).ToList(),
                                ContinousReadList = e.Element("ContinousReadList").Elements("Func")
                                     .Select(f => new Function
                                     {
                                         Name = f.Value,
                                         Control = f.Attribute("parent")?.Value ?? null,
                                     }).ToList(),

                                SensitiveControls = e.Element("SensitiveControls").Elements("Function")
                                    .Select(f => new Function
                                    {
                                        Control = f.Attribute("control")?.Value,
                                        Payloads = f.Elements("Payload").Select(x => x.Value).ToList()
                                    }).ToList(),
                                Scenarios = e.Element("Scenarios").Elements("Scenario")
                                    .Select(s => new Scenario
                                    {
                                        Address = Convert.ToUInt16(s.Element("Address").Value, 16),
                                        Name = s.Element("Name").Value,
                                        OpenPayloads = s.Element("OpenPayloads").Elements("Payload").Select(x => x.Value).ToList(),
                                        ClosePayloads = s.Element("ClosePayloads").Elements("Payload").Select(x => x.Value).ToList(),
                                    }).ToList(),
                            }).ToList(),
                    }).First();

                #endregion


                return new ConfigurationInfo
                {
                    Settings = settings,
                    Services = services,
                    Sessions = sessions,
                    Controls = controls,
                    Payloads = payloads,
                    DTCFailureTypes = dtcFailureTypes,
                    EnvironmentalTest = environmentalTest
                };
            }

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
            return Payloads?.FirstOrDefault(x => x.TypeName == typeName);
        }
    }

    public class ASContext
    {
        public static SessionInfo CurrentSession { get; set; }
        public static ConfigurationInfo Configuration { get; set; }

        public ASContext(string configFile)
        {
            if (configFile != null)
            {
                if(FormMain.isOdxFile)
                    Configuration = ConfigurationInfo.ParseODX(configFile);
                else
                    Configuration = ConfigurationInfo.Parse(configFile);
            }
        }
    }

    public class EnvironmentalTest
    {
        /// <summary>
        /// Name of current selected environment
        /// </summary>
        public static string CurrentEnvironment { get; set; }

        /// <summary>
        /// Gets or sets a list of connection mappings.
        /// </summary>
        public List<Mapping> ConnectionMappings { get; set; }
        /// <summary>
        /// Gets or sets the environments
        /// </summary>
        public List<Environment> Environments { get; set; }
    }

    public class Environment
    {
        /// <summary>
        /// Name of environment
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the environment configs
        /// </summary>
        public EnvironmentalConfig EnvironmentalConfig { get; set; }
        /// Gets or sets a list of continuous read functions.
        /// </summary>
        public List<Function> ContinousReadList { get; set; }
        /// <summary>
        /// Gets or sets the test cycles
        /// </summary>
        public List<Cycle> Cycles { get; set; }
        /// <summary>
        /// Gets or sets a list of sensitive controls
        /// </summary>
        public List<Function> SensitiveControls { get; set; }
        /// <summary>
        /// Gets or sets a list of scenarios
        /// </summary>
        public List<Scenario> Scenarios { get; set; }
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
        public Function Output { get; set; }
        /// <summary>
        /// Relevant Input Name
        /// </summary>
        public Function Input { get; set; }

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
        public short PWMDutyOpenValue { get; set; }
        /// <summary>
        /// Gets or sets the message of PWM Close Duty value
        /// </summary>
        public short PWMDutyCloseValue { get; set; }
        /// <summary>
        /// Gets or sets the message of PWM Open Frequency value
        /// </summary>
        public short PWMFreqOpenValue { get; set; }
        /// <summary>
        /// Gets or sets the message of PWM Close Frequency value
        /// </summary>
        public short PWMFreqCloseValue { get; set; }
        /// <summary>
        /// Gets or sets the duration of sensitive control
        /// </summary>
        public int SensitiveCtrlDuration { get; set; }

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
        public List<Function> Functions { get; set; }

        public HashSet<Function> CloseItems { get; set; } = new HashSet<Function>();
        public HashSet<Function> OpenItems { get; set; } = new HashSet<Function>();

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

    public class Function
    {
        public string Name { get; set; }
        public string Control { get; set; }
        public string Scenario { get; set; }
        public ControlInfo ControlInfo { get; set; }
        public List<string> Payloads { get; set; }
    }

    /// <summary>
    /// Represents a test scenario containing a list of open and close payloads
    /// </summary>
    public class Scenario
    {
        /// <summary>
        /// Gets or sets the address of the relevant control
        /// </summary>
        public ushort Address { get; set; }
        /// <summary>
        /// Gets or sets the name of the payload relevant to the scenario
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the list of open payloads
        /// </summary>
        public List<string> OpenPayloads { get; set; }
        /// <summary>
        /// Gets or sets the list of close payloads
        /// </summary>
        public List<string> ClosePayloads { get; set; }
    }
}
