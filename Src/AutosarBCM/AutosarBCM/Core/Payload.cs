using System;
using System.Linq;

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

            if (value != null)
                if (value.Any(x => x > 0)) SetFormattedValue();
                else
                {
                    Value = new byte[] { 0x0, 0x0 };
                    SetFormattedValue();
                }

            return this;
        }

        internal static Type GetConcreteType(PayloadInfo payloadInfo)
        {
            if (payloadInfo.Format == "hex") return typeof(HexPayload);
            else if (payloadInfo.Format == "uint16") return typeof(UInt16Payload);
            else if (payloadInfo.Format == "uint16-hex") return typeof(UInt16HexPayload);
            return typeof(DefaultPayload);
        }

        protected abstract void SetFormattedValue();
    }

    public class DefaultPayload : Payload
    {
        protected override void SetFormattedValue()
        {
            var payloadValue = ASContext.Configuration.GetPayloadInfoByType(PayloadInfo.TypeName).GetPayloadValue(Value);
            FormattedValue = payloadValue?.FormattedValue;
            Color = payloadValue?.Color;
        }
    }

    public class HexPayload : Payload
    {
        protected override void SetFormattedValue() => FormattedValue = BitConverter.ToString(Value);
    }

    public class UInt16Payload : Payload
    {
        protected override void SetFormattedValue() => FormattedValue = BitConverter.ToUInt16(Value.Reverse().ToArray(), 0).ToString();
    }

    public class UInt16HexPayload : Payload
    {
        protected override void SetFormattedValue() => FormattedValue = Convert.ToUInt32(Value.FirstOrDefault().ToString("X2"), 16).ToString();
    }
}
