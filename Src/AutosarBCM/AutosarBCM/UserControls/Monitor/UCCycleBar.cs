using System;
using System.Drawing;
using System.Windows.Forms;

namespace AutosarBCM.UserControls.Monitor
{
    /// <summary>
    /// Represents a user control for displaying cycling status and information.
    /// </summary>
    public partial class UCCycleBar : UserControl
    {
        #region Variables

        /// <summary>
        /// The color used to represent the cycling status.
        /// </summary>
        private Color color = Color.Red;

        /// <summary>
        /// Gets or sets a value indicating whether the cycling is currently running.
        /// </summary>
        public bool Running
        {
            get { return running; }
            set
            {
                running = value;
                color = value ? Color.LimeGreen : Color.Red;
                Invalidate();
            }
        }
        private bool running;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the UCCycleBar class.
        /// </summary>
        public UCCycleBar()
        {
            InitializeComponent();

            this.Paint += UCCycleBar_Paint;
            MonitorUtil.EnvMonitorProgress += MonitorUtil_EnvMonitorProgress;
        }

        #endregion

        #region Public Methods

        #endregion

        #region Private Methods

        /// <summary>
        /// Handles the environmental monitoring progress event and updates the displayed information.
        /// </summary>
        /// <param name="args">The event arguments containing monitoring information.</param>
        private void MonitorUtil_EnvMonitorProgress(EnvironmentalEventArgs args)
        {
            if (this.InvokeRequired)
                this.Invoke(new Action(() =>
                {
                    lblTimeSpent.Text = args.ElapsedTime.ToString("hh\\:mm\\:ss");
                    lblLoop.Text = args.Loop.ToString();
                    lblReboots.Text = args.Reboots.ToString();
                }));
        }

        /// <summary>
        /// Handles the paint event of the UCCycleBar control and updates the cycling status display.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void UCCycleBar_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillEllipse(new SolidBrush(color), new RectangleF(15, 7, 10, 10));
        }

        #endregion
    }
}
