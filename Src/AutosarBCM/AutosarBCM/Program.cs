using System;
using System.Globalization;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using AutosarBCM.Forms.Monitor;
using LicenseHelper;
using log4net;
using log4net.Config;

namespace AutosarBCM
{
    /// <summary>
    /// Static class to initiate the program.
    /// </summary>
    static class Program
    {
        /// <summary>
        /// Unique name for the mutex.
        /// </summary>
        private static Mutex _mutex = new Mutex(true, "\"##||AutosarBCMAppMutex||##\"");
        public static Thread UIThread = Thread.CurrentThread;
        public static FormMain MainForm => (FormMain)Application.OpenForms["FormMain"];
        public static FormControlChecker FormControlChecker => (FormControlChecker)Application.OpenForms["FormControlChecker"];
        public static FormEMCView FormEMCView => (FormEMCView)Application.OpenForms["FormEMCView"];

        public static FormEnvironmentalTest FormEnvironmentalTest => (FormEnvironmentalTest)Application.OpenForms[Constants.Form_Environmental_Test];

        public static MultiKeyDictionary<string, string, ErrorLogDetectObject> MappingStateDict = new MultiKeyDictionary<string, string, ErrorLogDetectObject>();

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (!_mutex.WaitOne(0, true))
            {
                MessageBox.Show("An instance of the application is already running.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;
            #region Init log4net
            XmlConfigurator.Configure();
            var logger = LogManager.GetLogger("");
            logger.Info("-------------");
            logger.Info("AutosarBCM " + Helper.AppVersionFullText);
            #endregion

            //subscribe to unhandled exceptions
            Application.ThreadException += Application_ThreadException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

#if !DEBUG
            var appName = Assembly.GetExecutingAssembly().GetName().Name;
            if (!ValidationHelper.CheckLicense(appName))
                return;
#endif

            FormSplashScreen splashScreen = new FormSplashScreen();
            splashScreen.Show();
            splashScreen.Refresh();

            FormMain formMain = new FormMain();

            splashScreen.Close();

            Application.Run(formMain);

            _mutex.ReleaseMutex();
        }

        private static void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            MainForm.AppendTrace($"Scheduler Exception: {e.Exception.Message}");   
        }

        /// <summary>
        /// Called when unhandled exception is catched
        /// </summary>
        /// <param name="sender">not used</param>
        /// <param name="e">exception details</param>
        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var ex = new ApplicationException("Unhandled exception occurred. [AppDomain]", e.ExceptionObject as Exception);
            Helper.ShowErrorMessageBox(ex);
        }

        /// <summary>
        /// Called when unhandled exception is catched in any thread
        /// </summary>
        /// <param name="sender">not used</param>
        /// <param name="e">exception details</param>
        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            if (e.Exception is ObjectDisposedException)
            {
                Helper.Logger.Error(e);
                return;
            }

            ApplicationException ex = new ApplicationException("Unhandled exception occurred.", e.Exception);
            Helper.ShowErrorMessageBox(ex);
        }
    }

}
