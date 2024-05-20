using AutosarBCM.Config;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutosarBCM.Core
{
    public abstract class Payload
    {
        protected byte[] Data;
        public byte Value { get; protected set; }
        public string FormattedValue { get; protected set; }
        public string Color { get; private set; }

        public PayloadInfo PayloadInfo { get; set; }

        public Payload(PayloadInfo payloadInfo, byte[] data)
        {
            PayloadInfo = payloadInfo;
            Value = data[payloadInfo.Index];
            Data = data;
            SetValue();
        }

        public void SetValue()
        {
            Print();
            SetColor();
        }

        private void SetColor()
        {
            Color = ASContext.Configuration.GetPayloadInfoByType(PayloadInfo.TypeName)?.GetColor(Value);
        }

        public abstract string Print();
    }

    public class DID_Byte_Enable_Disable : Payload
    {
        public DID_Byte_Enable_Disable(PayloadInfo payloadInfo, byte[] data) : base(payloadInfo, data) { }

        public override string Print()
        {
            if (this.Data[PayloadInfo.Index] == 0) FormattedValue = "Disable";
            else if (this.Data[PayloadInfo.Index] == 1) FormattedValue = "Enable";

            return $"{PayloadInfo.Name,-30}{FormattedValue}";
        }
    }

    public class DID_Byte_Activate_Inactivate : Payload
    {
        public DID_Byte_Activate_Inactivate(PayloadInfo payloadInfo, byte[] data) : base(payloadInfo, data) { }

        public override string Print()
        {
            if (Data[PayloadInfo.Index] == 0) FormattedValue = "Null - No Control Active";
            else if (Data[PayloadInfo.Index] == 1) FormattedValue = "Activate";
            else if (Data[PayloadInfo.Index] == 2) FormattedValue = "Inactivate";

            return $"{PayloadInfo.Name,-30}{FormattedValue}";
        }
    }

    public class DID_Bytes_High_Low : Payload
    {
        public DID_Bytes_High_Low(PayloadInfo payloadInfo, byte[] data) : base(payloadInfo, data) { }

        public override string Print()
        {
            if (Data[PayloadInfo.Index] == 0) FormattedValue = "Low";
            else if (Data[PayloadInfo.Index] == 1) FormattedValue = "High";

            return $"{PayloadInfo.Name,-30}{FormattedValue}";
        }
    }

    public class DID_DE00_0 : Payload
    {
        public DID_DE00_0(PayloadInfo payloadInfo, byte[] data) : base(payloadInfo, data) { }

        public override string Print()
        {
            if (this.Data[PayloadInfo.Index] == 0) FormattedValue = "AMT";
            else if (this.Data[PayloadInfo.Index] == 1) FormattedValue = "Manual Transmission";

            return $"{PayloadInfo.Name,-30}{FormattedValue}";
        }
    }

    public class DID_DE00_4 : Payload
    {
        public DID_DE00_4(PayloadInfo payloadInfo, byte[] data) : base(payloadInfo, data) { }

        public override string Print()
        {
            if (this.Data[PayloadInfo.Index] == 0) FormattedValue = "Euro 3";
            else if (this.Data[PayloadInfo.Index] == 1) FormattedValue = "Euro 5";
            else if (this.Data[PayloadInfo.Index] == 2) FormattedValue = "Euro 6";
            else if (this.Data[PayloadInfo.Index] == 3) FormattedValue = "Euro 7";

            return $"{PayloadInfo.Name,-30}{FormattedValue}";
        }
    }

    public class DID_Byte_Present_notPresent : Payload
    {
        public DID_Byte_Present_notPresent(PayloadInfo payloadInfo, byte[] data) : base(payloadInfo, data) { }

        public override string Print()
        {
            if (this.Data[PayloadInfo.Index] == 0) FormattedValue = "Not Present";
            else if (this.Data[PayloadInfo.Index] == 1) FormattedValue = "Present";

            return $"{PayloadInfo.Name,-30}{FormattedValue}";
        }
    }

    public class OnOffState : Payload
    {
        public OnOffState(PayloadInfo payloadInfo, byte[] data) : base(payloadInfo, data) { }

        public override string Print()
        {
            if (Data[PayloadInfo.Index] == 0) FormattedValue = "On";
            else if (Data[PayloadInfo.Index] == 1) FormattedValue = "Off";

            return $"{PayloadInfo.Name,-30}{FormattedValue}";
        }
    }



    //public class HexDump : Payload
    //{
    //    public HexDump(PayloadInfo payloadInfo, byte[] data) : base(payloadInfo, data) { }

    //    public override string Print()
    //    {
    //        return $"{PayloadInfo.Name,-40}: {Data[PayloadInfo.Index]}";
    //    }
    //}
    public class Unsigned_1Byte : Payload
    {
        public Unsigned_1Byte(PayloadInfo payloadInfo, byte[] data) : base(payloadInfo, data) { }

        public override string Print()
        {
            return $"{PayloadInfo.Name,-40}: {Data[PayloadInfo.Index]}";
        }
    }
    public class DID_Lamp_Status : Payload
    {
        public DID_Lamp_Status(PayloadInfo payloadInfo, byte[] data) : base(payloadInfo, data) { }
        public override string Print()
        {
            if (this.Data[PayloadInfo.Index] == 0) FormattedValue = "Null - Not Configured for LED Outage Detection";
            else if (this.Data[PayloadInfo.Index] == 1) FormattedValue = "Lamp On";
            else if (this.Data[PayloadInfo.Index] == 2) FormattedValue = "Lamp Off";

            return $"{PayloadInfo.Name,-30}{FormattedValue}";
        }
    }
    public class DID_Byte_Lamp_Control : Payload
    {
        public DID_Byte_Lamp_Control(PayloadInfo payloadInfo, byte[] data) : base(payloadInfo, data) { }
        public override string Print()
        {
            if (this.Data[PayloadInfo.Index] == 0) FormattedValue = "Null - No Control Active";
            else if (this.Data[PayloadInfo.Index] == 1) FormattedValue = "Active";
            else if (this.Data[PayloadInfo.Index] == 2) FormattedValue = "Inactive";
            else if (this.Data[PayloadInfo.Index] == 3) FormattedValue = "Flash";

            return $"{PayloadInfo.Name,-30}{FormattedValue}";
        }
    }
    public class DID_4253_0 : Payload
    {
        public DID_4253_0(PayloadInfo payloadInfo, byte[] data) : base(payloadInfo, data) { }
        public override string Print()
        {
            if (this.Data[PayloadInfo.Index] == 0) FormattedValue = "Off - No Request";
            else if (this.Data[PayloadInfo.Index] == 1) FormattedValue = "Left Turn Request";
            else if (this.Data[PayloadInfo.Index] == 2) FormattedValue = "Right Turn Request";
            else if (this.Data[PayloadInfo.Index] == 3) FormattedValue = "Fault";

            return $"{PayloadInfo.Name,-30}{FormattedValue}";
        }
    }
    public class DID_F166_0 : Payload
    {
        public DID_F166_0(PayloadInfo payloadInfo, byte[] data) : base(payloadInfo, data) { }
        public override string Print()
        {
            return $"{PayloadInfo.Name,-30}{BitConverter.ToInt32(Data, PayloadInfo.Index)}";
        }
    }
    public class DID_42A0_0 : Payload
    {
        public DID_42A0_0(PayloadInfo payloadInfo, byte[] data) : base(payloadInfo, data) { }
        public override string Print()
        {
            if (this.Data[PayloadInfo.Index] == 1) FormattedValue = "Active";
            else if (this.Data[PayloadInfo.Index] == 2) FormattedValue = "Inactive";
            else if (this.Data[PayloadInfo.Index] == 3) FormattedValue = "Null - No Control Active";
            else if (this.Data[PayloadInfo.Index] == 4) FormattedValue = "On";
            else if (this.Data[PayloadInfo.Index] == 5) FormattedValue = "Off";
            else if (this.Data[PayloadInfo.Index] == 6) FormattedValue = "Daytime Running Lamp";

            return $"{PayloadInfo.Name,-30}{FormattedValue}";
        }
    }
    public class DID_40E8_0 : Payload
    {
        public DID_40E8_0(PayloadInfo payloadInfo, byte[] data) : base(payloadInfo, data) { }
        public override string Print()
        {
            if (this.Data[PayloadInfo.Index] == 1) FormattedValue = "Active";
            else if (this.Data[PayloadInfo.Index] == 2) FormattedValue = "Inactive";
            else if (this.Data[PayloadInfo.Index] == 3) FormattedValue = "VREG - The Right Front Low Beam Circuit Is Pulse Width Modulating For Voltage Regulation";
            else if (this.Data[PayloadInfo.Index] == 4) FormattedValue = "Daytime Running Light (DRL) - The Right Front Low Beam Circuit Is Pulse Width Modulating For DRL";

            return $"{PayloadInfo.Name,-30}{FormattedValue}";
        }
    }

    public class DID_40E7_0 : Payload
    {
        public DID_40E7_0(PayloadInfo payloadInfo, byte[] data) : base(payloadInfo, data) { }
        public override string Print()
        {
            if (this.Data[PayloadInfo.Index] == 1) FormattedValue = "Active";
            else if (this.Data[PayloadInfo.Index] == 2) FormattedValue = "Inactive";
            else if (this.Data[PayloadInfo.Index] == 3) FormattedValue = "VREG - The Left Front Low Beam Circuit Is Pulse Width Modulating For Voltage Regulation";
            else if (this.Data[PayloadInfo.Index] == 4) FormattedValue = "Daytime Running Light (DRL) - The Left Front Low Beam Circuit Is Pulse Width Modulating For DRL";

            return $"{PayloadInfo.Name,-30}{FormattedValue}";
        }
    }
    public class DID_4255_Daytime_Running_Light : Payload
    {
        public DID_4255_Daytime_Running_Light(PayloadInfo payloadInfo, byte[] data) : base(payloadInfo, data) { }
        public override string Print()
        {
            if (this.Data[PayloadInfo.Index] == 0) FormattedValue = "Null";
            else if (this.Data[PayloadInfo.Index] == 1) FormattedValue = "On / Set On";
            else if (this.Data[PayloadInfo.Index] == 2) FormattedValue = "Off / Set Off";

            return $"{PayloadInfo.Name,-30}{FormattedValue}";
        }
    }
    public class DID_41EF_0 : Payload
    {
        public DID_41EF_0(PayloadInfo payloadInfo, byte[] data) : base(payloadInfo, data) { }
        public override string Print()
        {
            if (this.Data[PayloadInfo.Index] == 0) FormattedValue = "Null - No Control Active";
            else if (this.Data[PayloadInfo.Index] == 1) FormattedValue = "Active";
            else if (this.Data[PayloadInfo.Index] == 2) FormattedValue = "Inactive";

            return $"{PayloadInfo.Name,-30}{FormattedValue}";
        }
    }
    public class Internal_Version : Payload
    {
        public Internal_Version(PayloadInfo payloadInfo, byte[] data) : base(payloadInfo, data) { }
        public override string Print()
        {
            return $"{PayloadInfo.Name,-30}{BitConverter.ToInt32(Data, PayloadInfo.Index)}";
        }
    }
    public class DID_41F1_0 : Payload
    {
        public DID_41F1_0(PayloadInfo payloadInfo, byte[] data) : base(payloadInfo, data) { }
        public override string Print()
        {
            if (this.Data[PayloadInfo.Index] == 0) FormattedValue = "Null - No Control Active";
            else if (this.Data[PayloadInfo.Index] == 1) FormattedValue = "Active";
            else if (this.Data[PayloadInfo.Index] == 2) FormattedValue = "Inactive";

            return $"{PayloadInfo.Name,-30}{FormattedValue}";
        }
    }
    public class DID_41EC_0 : Payload
    {
        public DID_41EC_0(PayloadInfo payloadInfo, byte[] data) : base(payloadInfo, data) { }
        public override string Print()
        {
            if (this.Data[PayloadInfo.Index] == 0) FormattedValue = "Null - No Control Active";
            else if (this.Data[PayloadInfo.Index] == 1) FormattedValue = "Active";
            else if (this.Data[PayloadInfo.Index] == 2) FormattedValue = "Inactive";
            else if (this.Data[PayloadInfo.Index] == 3) FormattedValue = "Ramping";
            else if (this.Data[PayloadInfo.Index] == 4) FormattedValue = "Flash";

            return $"{PayloadInfo.Name,-30}{FormattedValue}";
        }
    }
    public class DID_41F3_0 : Payload
    {
        public DID_41F3_0(PayloadInfo payloadInfo, byte[] data) : base(payloadInfo, data) { }
        public override string Print()
        {
            if (this.Data[PayloadInfo.Index] == 0) FormattedValue = "Null - No Control Active";
            else if (this.Data[PayloadInfo.Index] == 1) FormattedValue = "Active";
            else if (this.Data[PayloadInfo.Index] == 2) FormattedValue = "Inactive";
            else if (this.Data[PayloadInfo.Index] == 3) FormattedValue = "Ramping";
            else if (this.Data[PayloadInfo.Index] == 4) FormattedValue = "Flash";

            return $"{PayloadInfo.Name,-30}{FormattedValue}";
        }
    }
    public class DID_DE02_7 : Payload
    {
        public DID_DE02_7(PayloadInfo payloadInfo, byte[] data) : base(payloadInfo, data) { }
        public override string Print()
        {
            if (this.Data[PayloadInfo.Index] == 0) FormattedValue = "Non-Construciton Vehicle";
            else if (this.Data[PayloadInfo.Index] == 1) FormattedValue = "Construction Vehicle";

            return $"{PayloadInfo.Name,-30}{FormattedValue}";
        }
    }
    public class DID_DE03_0 : Payload
    {
        public DID_DE03_0(PayloadInfo payloadInfo, byte[] data) : base(payloadInfo, data) { }
        public override string Print()
        {
            if (this.Data[PayloadInfo.Index] == 0) FormattedValue = "H625";
            else if (this.Data[PayloadInfo.Index] == 1) FormattedValue = "H476";
            else if (this.Data[PayloadInfo.Index] == 2) FormattedValue = "H566";

            return $"{PayloadInfo.Name,-30}{FormattedValue}";
        }
    }
    public class DID_DE01_3 : Payload
    {
        public DID_DE01_3(PayloadInfo payloadInfo, byte[] data) : base(payloadInfo, data) { }

        public override string Print()
        {
            if (this.Data[PayloadInfo.Index] == 0) FormattedValue = "9 Liter";
            else if (this.Data[PayloadInfo.Index] == 1) FormattedValue = "13 Liter";

            return $"{PayloadInfo.Name,-30}{FormattedValue}";
        }
    }

    public class DID_DE01_4 : Payload
    {
        public DID_DE01_4(PayloadInfo payloadInfo, byte[] data) : base(payloadInfo, data) { }

        public override string Print()
        {
            if (this.Data[PayloadInfo.Index] == 0) FormattedValue = "Disable";
            else if (this.Data[PayloadInfo.Index] == 1) FormattedValue = "Dry Type";
            else if (this.Data[PayloadInfo.Index] == 2) FormattedValue = "Wet Type";
            else if (this.Data[PayloadInfo.Index] == 3) FormattedValue = "Dry&Wet Type";

            return $"{PayloadInfo.Name,-30}{FormattedValue}";
        }
    }

    public class DID_PWM : Payload
    {
        public DID_PWM(PayloadInfo payloadInfo, byte[] data) : base(payloadInfo, data) { }
        public override string Print()
        {
            return $"{BitConverter.ToSingle(Data, 0)} %";
        }
    }
    public class DID_Byte_On_Off : Payload
    {
        public DID_Byte_On_Off(PayloadInfo payloadInfo, byte[] data) : base(payloadInfo, data) { }
        public override string Print()
        {
            if (this.Data[PayloadInfo.Index] == 0) FormattedValue = "Off";
            else if (this.Data[PayloadInfo.Index] == 1) FormattedValue = "On";

            return $"{PayloadInfo.Name,-30}{FormattedValue}";
        }
    }
}
