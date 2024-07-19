using System;
using System.Runtime.InteropServices;

namespace AutosarBCM
{
    /// <summary>
    /// Provides a high-precision timer using the multimedia timer interface.
    /// </summary>
    public class MMTimer
    {
        #region Variables

        /// <summary>
        /// The resolution of the timer in milliseconds.
        /// </summary>
        private readonly int resolution;

        /// <summary>
        /// The type of the event: One-time or Repeating.
        /// </summary>
        private readonly EventType eventType;

        /// <summary>
        /// The event handler to be invoked on timer events.
        /// </summary>
        private readonly TimerEventHandler handler;

        /// <summary>
        /// The identifier for the timer.
        /// </summary>
        private uint timer;

        /// <summary>
        /// Imports the timeSetEvent function from the WinMM.dll.
        /// </summary>
        [DllImport("WinMM.dll", SetLastError = true)]
        public static extern uint timeSetEvent(int delay, int resolution, TimerEventHandler handler, ref int userCtx, int eventType);

        /// <summary>
        /// Imports the timeKillEvent function from the WinMM.dll.
        /// </summary>
        [DllImport("WinMM.dll", SetLastError = true)]
        public static extern uint timeKillEvent(uint timerId);

        /// <summary>
        /// Delegate for handling timer events.
        /// </summary>
        public delegate void TimerEventHandler(uint timerId, uint msg, ref int userData, int rsv1, int rsv2);

        /// <summary>
        /// Enumeration to define the type of timer event.
        /// </summary>
        public enum EventType
        {
            OneTime = 0,
            Repeating = 1
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the MMTimer class.
        /// </summary>
        /// <param name="delay">The delay in milliseconds.</param>
        /// <param name="resolution">The resolution in milliseconds.</param>
        /// <param name="eventType">The type of the event (One-time or Repeating).</param>
        /// <param name="action">The action to execute on timer event.</param>
        public MMTimer(int resolution, EventType eventType, Action action)
        {
            this.resolution = resolution;
            this.eventType = eventType;

            handler = (uint id, uint msg, ref int userData, int rsv1, int rsv2) => action();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Starts the timer.
        /// </summary>
        public void Next(int delay)
        {
            var userData = 0;
            timer = timeSetEvent(delay, resolution, handler, ref userData, (int)eventType);
        }

        /// <summary>
        /// Stops the timer.
        /// </summary>
        public void Stop()
        {
            timeKillEvent(timer);
        }

        #endregion
    }
}
