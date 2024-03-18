using System;
using System.Threading;
using System.Windows.Forms;

namespace AutosarBCM
{
    /// <summary>
    /// This form presents the application version and branding information while the main application is loading.
    /// </summary>
    public partial class FormSplashScreen : Form
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the FormSplashScreen class.
        /// </summary>
        public FormSplashScreen()
        {
            InitializeComponent();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Handles the Load event of the FormSplashScreen control.
        /// It sets up the application information label and centers it within the splash screen.
        /// </summary>
        /// <param name="sender">form</param>
        /// <param name="e">argument</param>
        private void FormSplashScreen_Load(object sender, EventArgs e)
        {
            lblAppInfo.AutoSize = true;
            lblAppInfo.Text = Helper.AppVersionText;
            lblAppInfo.Left = (this.ClientSize.Width - lblAppInfo.Width) / 2;

        }

        /// <summary>
        /// Handles the FormClosing event of the FormSplashScreen control.
        /// Introduces a delay when the splash screen is closing. 
        /// </summary>
        /// <param name="sender">form</param>
        /// <param name="e">argument</param>
        private void FormSplashScreen_FormClosing(object sender, FormClosingEventArgs e)
        {
            Thread.Sleep(2000);
        }

        #endregion
    }
}
