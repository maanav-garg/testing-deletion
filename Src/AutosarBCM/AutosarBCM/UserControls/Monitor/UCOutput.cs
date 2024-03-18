using System.Collections.Generic;
using System.Windows.Forms;
using AutosarBCM.Config;

namespace AutosarBCM.UserControls.Monitor
{
    /// <summary>
    /// Represents a user control for output items, providing common properties and functionalities.
    /// </summary>
    public class OutputUserControl : UserControl
    {
        #region Variables

        /// <summary>
        /// Gets or sets the group name associated with the control.
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// Gets or sets the message ID associated with the control.
        /// </summary>
        public string MessageID { get; set; }

        /// <summary>
        /// Gets or sets the dictionary mapping register groups to their corresponding data bytes.
        /// </summary>
        public Dictionary<short, List<byte>> RegisterDict { get; set; } = new Dictionary<short, List<byte>>();

        /// <summary>
        /// Gets or sets the PWM (Pulse Width Modulation) value associated with the control.
        /// </summary>
        public int PWM { get; set; }

        /// <summary>
        /// Gets or sets the revert time for the control.
        /// </summary>
        public int RevertTime { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Changes the status of the output based on the given response.
        /// This is a virtual method and should be overridden in derived classes.
        /// </summary>
        /// <param name="outputResponse">The response used to change the status.</param>
        public virtual void ChangeStatus(Response outputResponse) { }

        #endregion

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // OutputUserControl
            // 
            this.Name = "OutputUserControl";
            this.ResumeLayout(false);

        }
    }
}
