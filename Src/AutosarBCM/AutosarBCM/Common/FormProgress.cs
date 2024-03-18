using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutosarBCM.Common
{
    /// <summary>
    /// Form that displays progress of a background operation.
    /// </summary>
    partial class FormProgress : Form
    {
        /// <summary>
        /// Returns the result as the type used by BackgroundWorker.
        /// (It is avoided to introduce yet another abstraction to store the same data.)
        /// </summary>
        private RunWorkerCompletedEventArgs mRunWorkerCompletedEventArgs;

        /// <summary>
        /// argument of DoWork operation
        /// </summary>
        private object mDoWorkArgument;

        /// <summary>
        /// Duration that the form waits before closing, after completing the work.
        /// It might be used to allow user see the result.
        /// </summary>
        public int DelayInMilliseconds { get; set; }

        /// <summary>
        /// Default constructor.
        /// Text description is "Please wait". progress and cancellation is not supported.
        /// </summary>
        public FormProgress()
            : this(Properties.Resources.Msg_P_PleaseWait, false, false)
        {
        }

        /// <summary>
        /// Task description can be changed by this constructor.
        /// </summary>
        /// <param name="taskDescription">Window title</param>
        /// <param name="workerReportsProgress">true to make background worker support progress</param>
        /// <param name="workerSupportsCancellation">true to make background worker support cancellation</param>
        public FormProgress(string taskDescription, bool workerReportsProgress, bool workerSupportsCancellation)
        {
            InitializeComponent();

            labelTaskDescription.Text = taskDescription;
            labelOperationDescription.Text = string.Empty;
            backgroundWorker.WorkerReportsProgress = workerReportsProgress;
            backgroundWorker.WorkerSupportsCancellation = workerSupportsCancellation;
        }

        /// <summary>
        /// Shows the form.
        /// </summary>
        /// <param name="doWork">the eventhandler that will be run by background worker</param>
        /// <param name="argument">argument to the background worker</param>
        /// <returns>DoWork result</returns>
        public RunWorkerCompletedEventArgs Run(DoWorkEventHandler doWork, object argument = null)
        {
            backgroundWorker.DoWork += doWork;
            mDoWorkArgument = argument;
            this.ShowDialog();

            return mRunWorkerCompletedEventArgs;
        }

        /// <summary>
        /// Runs backgroundworker "DoWork" thread.
        /// </summary>
        /// <param name="e">not used</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // show cancel button only if worker supports cancellation
            buttonCancel.Enabled = backgroundWorker.WorkerSupportsCancellation;

            // Set progress bar style
            if (!backgroundWorker.WorkerReportsProgress)
                progressBar.Style = ProgressBarStyle.Marquee;

            backgroundWorker.RunWorkerAsync(mDoWorkArgument);
        }

        /// <summary>
        /// Updates progress bar and displayed text.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
            labelOperationDescription.Text = (string)e.UserState;
        }

        /// <summary>
        /// Closes the form according to the DoWork result.
        /// </summary>
        /// <param name="sender">backgroundWorker</param>
        /// <param name="e">DoWork result</param>
        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            mRunWorkerCompletedEventArgs = e;

            if (e.Error == null)
            {
                // Note: It's an error to read the Result when Error is not null.
                ChangeCancelButtonToCloseButton();
                AutoCloseForm(DelayInMilliseconds);
            }
            else
            {
                this.Close();
            }
        }

        /// <summary>
        /// Closes the form according to the delay. If delay is less than 1ms, it closes the form immediately.
        /// </summary>
        /// <param name="delayInMilliseconds">delay</param>
        private void AutoCloseForm(int delayInMilliseconds)
        {
            // use a timer only if the Interval is valid. (>= 1)
            if (delayInMilliseconds < 1)
            {
                this.Close();
                return;
            }

            var closeTimer = new Timer();
            closeTimer.Interval = delayInMilliseconds;
            closeTimer.Tick += (sender, args) =>
            {
                this.Close();
                closeTimer.Dispose();
            };
            closeTimer.Start();
        }

        /// <summary>
        /// Changes cancel to close
        /// </summary>
        private void ChangeCancelButtonToCloseButton()
        {
            buttonCancel.Text = Properties.Resources.Close;
            buttonCancel.Enabled = true;
            buttonCancel.Click += (sender, args) => this.Close();
        }

        /// <summary>
        /// Starts cancellation of background worker
        /// </summary>
        /// <param name="sender">button</param>
        /// <param name="e">not used</param>
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            if (!backgroundWorker.WorkerSupportsCancellation)
            {
                return;
            }

            backgroundWorker.CancelAsync();
            buttonCancel.Visible = false;   // prevent user pressing "cancel" button more.
        }
    }
}
