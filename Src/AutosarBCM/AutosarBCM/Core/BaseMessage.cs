using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Xml.Serialization;

namespace AutosarBCM.Core
{
    /// <summary>
    /// Specifies the currently supported transmission protocols.
    /// </summary>
    public enum TransmitProtocol
    {
        /// <summary>
        /// Specifies that the transmission protocol is CAN.
        /// </summary>
        Can,
        /// <summary>
        /// Specifies that the transmission protocol is UDS.
        /// </summary>
        Uds
    }

    /// <summary>
    /// An abstract class that contains common properties and methods of a CAN/UDS message.
    /// </summary>
    public abstract class BaseMessage
    {
        #region Variables

        /// <summary>
        /// Represents a constant for single messages.
        /// </summary>
        private const string singleMessage = "Single-Message";

        /// <summary>
        /// Represents a constant for multi-messages.
        /// </summary>
        private const string multiMessages = "Multi-Messages";

        /// <summary>
        /// Represents a constant for manual messages.
        /// </summary>
        private const string manual = "Manual";

        /// <summary>
        /// Represents a constant for periodic messages.
        /// </summary>
        private const string periodic = "Periodic";

        /// <summary>
        /// A dictionary for mapping SID byte values to SIDDescription enum values.
        /// </summary>
        private Dictionary<byte, string> sidResponseMessageDict = Enum.GetValues(typeof(SIDDescription)).Cast<SIDDescription>().ToDictionary(t => (byte)t, t => t.ToString());

        /// <summary>
        /// A dictionary for mapping NRC byte values to NRCDescription enum values.
        /// </summary>
        private Dictionary<byte, string> nrcResponseMessageDict = Enum.GetValues(typeof(NRCDescription)).Cast<NRCDescription>().ToDictionary(t => (byte)t, t => t.ToString());

        /// <summary>
        /// Gets or sets the Id of the message.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the data of the message.
        /// </summary>
        [XmlElement(DataType = "hexBinary")]
        public byte[] Data { get; set; }

        /// <summary>
        /// Gets or sets the delay between each message while transmitting multiple messages.
        /// </summary>
        public int CycleTime { get; set; }

        /// <summary>
        /// Gets or sets the cycle count value.
        /// </summary>
        public int CycleCount { get; set; }

        /// <summary>
        /// Gets or sets the amount of time for which the message is delayed.
        /// </summary>
        public int DelayTime { get; set; }

        /// <summary>
        /// Gets or sets the number of responses received from the ECU with the same message Id and data.
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// Gets or sets the type of the trigger (Manual, Periodic)
        /// </summary>
        public string Trigger { get; set; }

        /// <summary>
        /// Gets or sets a user-defined text that is attached to the message.
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Gets or sets the timestamp at which the message was transmitted or received.
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Gets or sets the response message associated with the received message.
        /// </summary>
        public string Response { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the length of the message data; For multi-messages type: it's the number of sub-messages defined in the parent message.
        /// </summary>
        public int Length { get; set; }

        /// <summary>
        /// Gets the data of the message represented in a hexadecimal format.
        /// </summary>
        public string DataString { get => Multi ? multiMessages : String.Join(" ", Data.Select(b => b.ToString("X2"))); }

        /// <summary>
        /// Gets or sets whether the message is a single message or a multi-messages type.
        /// </summary>
        public bool Multi { get; set; }

        /// <summary>
        /// Gets or sets the list of sub-messages when the message is a multi-messages type.
        /// </summary>
        public List<BaseMessage> SubMessages { get; set; }

        /// <summary>
        /// Used to stop the process when a cancellation request is sent during the transmission.
        /// </summary>
        private CancellationTokenSource _cancellationTokenSource;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the BaseMessage class with default values.
        /// </summary>
        protected internal BaseMessage()
        {
            Trigger = manual;
            Timestamp = DateTime.Now;
            Data = new byte[8];
        }

        /// <summary>
        /// Initializes a new instance of the BaseMessage class with a specific message id and data.
        /// </summary>
        /// <param name="id">The id of the new message instance.</param>
        /// <param name="data">The data of the new message instance.</param>
        protected internal BaseMessage(string id, string data) : this()
        {
            this.Id = id;
            this.SetData(data);
        }

        /// <summary>
        /// Initializes a new instance of the BaseMessage class; optionally sets the type as multi-messages.
        /// </summary>
        /// <param name="multi">true for multi-messages type; the default is false.</param>
        protected internal BaseMessage(bool multi = false) : this()
        {
            Multi = multi;
            if (multi) this.SubMessages = new List<BaseMessage>();
        }

        /// <summary>
        /// Creates a new instance with all values.
        /// </summary>
        /// <returns>A new instance that is a copy of this object.</returns>
        internal BaseMessage(string id, string data,int cycleTime, int cycleCount, int delayTime, int count, string trigger, string comment, bool multi) : this()
        { 
            Id = id;
            SetData(data);
            CycleTime = cycleTime;
            CycleCount = cycleCount;
            DelayTime = delayTime;
            Count = count;
            Trigger = trigger;
            Comment = comment;
            Multi = multi;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates a new instance that is a copy of the current object. 
        /// If 'copyMode' is true, the new instance's 'Comment' property is prefixed with "Copy of" and 'Count' property is set to 0 for non-sub-messages.
        /// For sub-messages, 'Count' is retained from the original object regardless of the 'copyMode' value.
        /// </summary>
        /// <param name="copyMode">Indicates whether the copy is being made for a copy operation. Default is true.</param>
        /// <returns>A new instance that is a copy of this object.</returns>
        public BaseMessage Clone(bool copyMode = true, bool isSubMessage = false)
        {
            var copy = this.MemberwiseClone() as BaseMessage;

            copy.Comment = copyMode ? "Copy of " + this.Comment : this.Comment;
            copy.Count = copyMode || isSubMessage ? 0 : this.Count;

            if (SubMessages == null)
            {
                return copy;
            }
            copy.SubMessages = new List<BaseMessage>();
            this.SubMessages.ForEach(x => copy.SubMessages.Add(x.Clone(false, true)));
            return copy;
        }

        /// <summary>
        /// Provides a convenient method for configuring message data through a hexadecimal string.
        /// </summary>
        /// <param name="data">The hexadecimal string that contains the new data</param>
        public void SetData(string data)
        {
            if (data == null)
                this.Data = new byte[8];
            else if (data.Contains(" "))
                this.Data = data.Split(' ').Select(x => byte.Parse(x)).ToArray();
            else
                this.Data = Enumerable.Range(0, data.Length / 2).Select(x => Convert.ToByte(data.Substring(x * 2, 2), 16)).ToArray();
        }

        /// <summary>
        /// Increases the number of received messages by a specified number.
        /// </summary>
        /// <param name="count">The number of messages received; default value: 1</param>
        public void IncreaseCounter(int count = 1)
        {
            this.Count += count;
        }

        /// <summary>
        /// Transmits the message to the ECU.
        /// </summary>
        public void Transmit()
        {
            if (Multi)
            {
                SubMessages.ForEach(x => x.TransmitMessage());
                IncreaseCounter();
            }
                
            else TransmitMessage();
        }

        /// <summary>
        /// Checks whether the message can be transmitted or not.
        /// </summary>
        /// <returns>true if the message can be transmitted; otherwise, false.</returns>
        public bool CheckForTransmit()
        {
            return !String.IsNullOrWhiteSpace(Id) && !String.IsNullOrWhiteSpace(DataString);
        }

        /// <summary>
        /// Sets the response message based on the received data.
        /// </summary>
        public void SetResponse()
        {
            Response = String.Empty;
            if (Data[1] == Convert.ToByte("7F", 16))
            {
                if (sidResponseMessageDict.TryGetValue(Data[2], out string sidResponse))
                    Response += sidResponse + " ";
                if (nrcResponseMessageDict.TryGetValue(Data[3], out string nrcResponse))
                    Response += nrcResponse;

                if (string.IsNullOrWhiteSpace(Response))
                    Response = "Unexpected Negative Response!";
            }
            else
            {
                Response = "Success.";
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Transmits the message to the ECU based on the type of the trigger.
        /// </summary>
        private void TransmitMessage()
        {
            int millisecondsDelay = DelayTime != 0 ? DelayTime : 10;

            if (Trigger == periodic)
            {
                _cancellationTokenSource = new CancellationTokenSource();
                for (var i = 0; i < CycleCount; i++)
                {
                    if (_cancellationTokenSource.Token.IsCancellationRequested)
                        break;

                    TransmitInternal(CycleTime);
                }
            }
            else
                TransmitInternal(millisecondsDelay);
        }
        /// <summary>
        /// Allows to transmitted messages if trigger is periodic.
        /// </summary>
        public void StopPeriodicMessage()
        {
            if (Trigger == periodic && _cancellationTokenSource != null)
            {
                _cancellationTokenSource.Cancel();
            }
        }

        /// <summary>
        /// Transmits the message to the ECU with a specified delay.
        /// </summary>
        /// <param name="millisecondsDelay">The number of milliseconds for which the thread is suspended.</param>
        private void TransmitInternal(int? millisecondsDelay)
        {
            if (millisecondsDelay.HasValue)
                System.Threading.Thread.Sleep(millisecondsDelay.Value);

            ConnectionUtil.TransmitData(uint.Parse(Id, NumberStyles.HexNumber), Data);
          
            IncreaseCounter();
        }

        #endregion
    }

    /// <summary>
    /// Implements a BaseMessage class that uses the CAN protocol.
    /// </summary>
    public class CanMessage : BaseMessage
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the CanMessage class with default values.
        /// </summary>                                  
        public CanMessage() : base()
        { }

        /// <summary>
        /// Initializes a new instance of the CanMessage class with a specific message id and data.
        /// </summary>
        /// <param name="id">The id of the new message instance.</param>
        /// <param name="data">The data of the new message instance.</param>
        public CanMessage(string id, string data) : base(id, data)
        { }

        /// <summary>
        /// Initializes a new instance of the CanMessage class; optionally sets the type as multi-messages.
        /// </summary>
        /// <param name="multi">true for multi-messages type; the default is false.</param>
        public CanMessage(bool multi = false) : base(multi)
        { }

        /// <summary>
        /// Initializes a new instance of the CanMessage class with the specified parameters.
        /// </summary>
        /// <param name="id">The identifier of the message.</param>
        /// <param name="data">The data associated with the message.</param>
        /// <param name="cycleTime">The cycle time of the message.</param>
        /// <param name="cycleCount">The cycle count of the message.</param>
        /// <param name="delayTime">The delay time of the message.</param>
        /// <param name="count">The count of the message.</param>
        /// <param name="trigger">The trigger of the message.</param>
        /// <param name="comment">The comment associated with the message.</param>
        /// <param name="multi">A flag indicating whether the message is multi.</param>
        public CanMessage(string id, string data, int cycleTime, int cycleCount, int delayTime, int count, string trigger, string comment,bool multi) : base(id,data,cycleTime,cycleCount,delayTime,count,trigger,comment, multi) { }

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates a new instance of the CanMessage class based on the provided values.
        /// </summary>
        /// <param name="values">An array of values containing message properties.</param>
        /// <returns>A new CanMessage instance initialized with the specified values.</returns>
        public static CanMessage SetCanMessage(string[] values)
        {
            CanMessage msg = new CanMessage(values[0], values[1],
                Convert.ToInt32(string.IsNullOrWhiteSpace(values[2]) ? "0" : values[2]),
                Convert.ToInt32(string.IsNullOrWhiteSpace(values[3]) ? "0" : values[3]),
                Convert.ToInt32(string.IsNullOrWhiteSpace(values[4]) ? "0" : values[4]),
                Convert.ToInt32(string.IsNullOrWhiteSpace(values[5]) ? "0" : values[5]),
                values[6], values[7],
                bool.Parse(values[8]));
            
            msg.Length = values[1].Split(new char[] { ' ' }).Length;
            return msg;
        }

        #endregion
    }

    /// <summary>
    /// Implements a BaseMessage class that uses the Uds protocol.
    /// </summary>
    public class UdsMessage : BaseMessage
    {
        #region Variables

        /// <summary>
        /// Gets or sets the name of the Uds message service.
        /// </summary>
        public string ServiceName { get; set; }
        /// <summary>
        /// Gets or sets the sub-function's name of the Uds message service.
        /// </summary>
        public string SubFunction { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the UdsMessage class with default values.
        /// </summary>
        public UdsMessage() : base()
        { }

        /// <summary>
        /// Initializes a new instance of the UdsMessage class with a specific message id and data.
        /// </summary>
        /// <param name="id">The id of the new message instance.</param>
        /// <param name="data">The data of the new message instance.</param>
        public UdsMessage(string id, string data) : base(id, data)
        { }

        /// <summary>
        /// Initializes a new instance of the UdsMessage class; optionally sets the type as multi-messages.
        /// </summary>
        /// <param name="multi">true for multi-messages type; the default is false.</param>
        public UdsMessage(bool multi = false) : base(multi)
        { }

        /// <summary>
        /// Creates a new instance of the UdsMessage class with additional service-related properties.
        /// </summary>
        /// <param name="id">The message ID.</param>
        /// <param name="data">The message data.</param>
        /// <param name="cycleTime">The cycle time for periodic messages.</param>
        /// <param name="cycleCount">The cycle count for periodic messages.</param>
        /// <param name="delayTime">The delay time for message transmission.</param>
        /// <param name="count">The message count for periodic messages.</param>
        /// <param name="trigger">The trigger condition for message transmission.</param>
        /// <param name="comment">A comment or description of the message.</param>
        /// <param name="multi">Specifies if the message is multi-frame.</param>
        /// <param name="serviceName">The service name associated with the message.</param>
        /// <param name="subFunction">The sub-function associated with the message.</param>
        private UdsMessage(string id, string data, int cycleTime, int cycleCount, int delayTime, int count, string trigger, string comment,bool multi, string serviceName, string subFunction) : base(id, data, cycleTime, cycleCount, delayTime, count, trigger, comment, multi)
        {
            ServiceName = serviceName;
            SubFunction = subFunction;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates a new instance of the UdsMessage class from an array of values.
        /// </summary>
        /// <param name="values">An array of values representing the UdsMessage properties.</param>
        /// <returns>A new UdsMessage instance initialized with the provided values.</returns>
        public static UdsMessage SetUdsMessage(string[] values)
        {
           return new UdsMessage(values[0], values[1],
                Convert.ToInt32(string.IsNullOrWhiteSpace(values[2]) ? "0" : values[2]),
                Convert.ToInt32(string.IsNullOrWhiteSpace(values[3]) ? "0" : values[3]),
                Convert.ToInt32(string.IsNullOrWhiteSpace(values[4]) ? "0" : values[4]),
                Convert.ToInt32(string.IsNullOrWhiteSpace(values[5]) ? "0" : values[5]),
                values[6], values[7], bool.Parse(values[8]), values[9], values[10]);
        }

        #endregion
    }
}
