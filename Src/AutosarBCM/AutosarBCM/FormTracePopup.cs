using System.Drawing;
using System.Windows.Forms;

namespace AutosarBCM
{
    /// <summary>
    /// Represents a popup form for displaying trace information.
    /// </summary>
    internal partial class FormTracePopup : Form
    {
        #region Constructor

        /// <summary>
        /// Initializes the components of the FormTracePopup.
        /// </summary>
        internal FormTracePopup()
        {
            InitializeComponent();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Appends text to the trace popup with the specified color.
        /// </summary>
        /// <param name="color">The color to be used for the appended text</param>
        /// <param name="text">The text to be appended</param>
        internal void AppendTraceToPopup(Color color,string text)
        {
            txtTrace.SelectionColor = color;
            txtTrace.AppendText(text);
            ScrollToBottom();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Scroll brings the pop up to the most current location
        /// </summary>
        private void ScrollToBottom()
        {
            txtTrace.SelectionStart = txtTrace.TextLength;
            txtTrace.ScrollToCaret();
        }

        /// <summary>
        /// Clears the log panel
        /// </summary>
        /// <param name="sender">Control</param>
        /// <param name="e">Event args</param>
        private void tsbClearLog_Click(object sender, System.EventArgs e)
        {
            txtTrace.Clear();
        }

        #endregion
    }
}
