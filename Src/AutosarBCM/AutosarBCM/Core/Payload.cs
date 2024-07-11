using AutosarBCM.Core.Config;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutosarBCM.Core.Config
{
    public abstract class Payload
    {
        public byte[] Value { get; protected set; }

        public string FormattedValue { get; protected set; }
        public string Color { get; protected set; }

        public PayloadInfo PayloadInfo { get; set; }

        public Payload() { }

        public Payload Parse(PayloadInfo payloadInfo, byte[] value, int? index = null)
        {
            PayloadInfo = payloadInfo;
            Value = value;

            if (value != null && payloadInfo.IsBit)
                Value = new byte[] { (byte)((value[0] & (1 << index)) == 0 ? 0 : 1) };

            if (value != null) SetFormattedValue();
            return this;
        }

        protected virtual void SetFormattedValue()
        {
            var payloadValue = ASContext.Configuration.GetPayloadInfoByType(PayloadInfo.TypeName).GetPayloadValue(Value);
            FormattedValue = payloadValue?.FormattedValue;
            Color = payloadValue?.Color;
        }
    }

    public class DID_Byte_Enable_Disable : Payload { }
    public class DID_Byte_Activate_Inactivate : Payload { }
    public class DID_Bytes_High_Low : Payload { }
    public class DID_DE00_0 : Payload { }
    public class DID_DE00_4 : Payload { }
    public class DID_Byte_Present_notPresent : Payload { }
    public class OnOffState : Payload { }
    public class DID_Lamp_Status : Payload { }
    public class DID_Byte_Lamp_Control : Payload { }
    public class DID_4253_0 : Payload { }
    public class DID_42A0_0 : Payload { }
    public class DID_40E8_0 : Payload { }
    public class DID_40E7_0 : Payload { }
    public class DID_4255_Daytime_Running_Light : Payload { }
    public class DID_41EF_0 : Payload { }
    public class DID_41F1_0 : Payload { }
    public class DID_41EC_0 : Payload { }
    public class DID_41F3_0 : Payload { }
    public class DID_DE02_7 : Payload { }
    public class DID_DE03_0 : Payload { }
    public class DID_DE01_3 : Payload { }
    public class DID_DE01_4 : Payload { }
    public class DID_DE08_7 : Payload { }
    public class DID_Byte_Kmph : Payload { }
    public class DID_C257_0 : Payload { }
    public class DID_Byte_On_Off : Payload { }
    public class DID_Bits_On_Off : Payload { }

    public class HexDump_1Byte : Payload
    {
        protected override void SetFormattedValue() => FormattedValue = BitConverter.ToString(Value);
    }
    public class HexDump_2Bytes : Payload
    {
        protected override void SetFormattedValue() => FormattedValue = BitConverter.ToString(Value);
    }

    public class HexDump_4Bytes : Payload
    {
        protected override void SetFormattedValue() => FormattedValue = BitConverter.ToString(Value);
    }
    public class HexDump_16Bytes : Payload
    {
        protected override void SetFormattedValue() => FormattedValue = BitConverter.ToString(Value);
    }

    public class Unsigned_1Byte : Payload
    {
        protected override void SetFormattedValue() => FormattedValue = BitConverter.ToString(Value);
    }

    public class DID_F166_0 : Payload
    {
        protected override void SetFormattedValue() => FormattedValue = BitConverter.ToString(Value);
    }

    public class Internal_Version : Payload
    {
        protected override void SetFormattedValue() => FormattedValue = BitConverter.ToString(Value);
    }

    public class DID_PWM : Payload
    {
        protected override void SetFormattedValue() => FormattedValue = BitConverter.ToString(Value);
    }
    public class DID_DE26 : Payload
    {
        protected override void SetFormattedValue() => FormattedValue = BitConverter.ToString(Value);
    }
    public class DID_DE04_7 : Payload
    {
        protected override void SetFormattedValue() => FormattedValue = BitConverter.ToString(Value);
    }
    public class DID_DE06_1 : Payload
    {
        protected override void SetFormattedValue() => FormattedValue = BitConverter.ToString(Value);
    }
    public class DID_DE06_3 : Payload
    {
        protected override void SetFormattedValue() => FormattedValue = BitConverter.ToString(Value);
    }
    public class DID_DE0B_1 : Payload
    {
        protected override void SetFormattedValue() => FormattedValue = BitConverter.ToString(Value);
    }
    public class Unsigned_2Bytes : Payload
    {
        protected override void SetFormattedValue() => FormattedValue = BitConverter.ToString(Value);
    }
}
