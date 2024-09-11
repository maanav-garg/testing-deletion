using System;

namespace AutosarBCM.Config
{
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
