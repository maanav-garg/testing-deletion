using AutosarBCM.Config;
using AutosarBCM.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AutosarBCM
{
    /// <summary>
    /// Represents a UI form that can run tests periodically.
    /// </summary>
    public interface IPeriodicTest
    {
        /// <summary>
        /// Starts a new test periodically
        /// </summary>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the task</param>
        void StartTest(CancellationToken cancellationToken);
        /// <summary>
        /// Gets whether this test is runnable or not.
        /// </summary>
        /// <returns>true if this test is runnable; otherwise, false.</returns>
        bool CanBeRun();

        /// <summary>
        /// Change status of monitor item based on data
        /// </summary>
        /// <param name="receivedData">Data received from device</param>
        bool ChangeStatus(byte[] receivedData, MessageDirection messageDirection);


        /// <summary>
        /// Filter items to show
        /// </summary>
        void FilterUCItems(string filter);

        /// <summary>
        /// Filter items to show by sessions
        /// </summary>
        void SessionFiltering();
    }

    /// <summary>
    /// Represents a UI form that can run tests for one time.
    /// </summary>
    public interface IClickTest
    {
        /// <summary>
        /// Change status of monitor item based on data
        /// </summary>
        bool ChangeStatus(byte[] receivedData, MessageDirection messageDirection);

        /// <summary>
        /// Gets whether this test is runnable or not.
        /// </summary>
        /// <returns>true if this test is runnable; otherwise, false.</returns>
        bool CanBeRun();

        /// <summary>
        /// Filter items to show
        /// </summary>
        void FilterUCItems(string filter);
    }

    /// <summary>
    /// Util class of monitor feature
    /// </summary>
    internal class MonitorUtil
    {
        #region Properties

        /// <summary>
        /// Static event triggered to report progress in environmental monitoring.
        /// </summary>
        public static event Action<EnvironmentalEventArgs> EnvMonitorProgress;

        /// <summary>
        /// Static property to store the start time of environmental monitoring.
        /// </summary>
        private static DateTime StartTime { get; set; }

        ///// <summary>
        ///// UDS messages used in environmental monitoring.
        ///// </summary>
        //private static Dictionary<string, UdsMessage> RssiDictionary = new Dictionary<string, UdsMessage>()
        //{
        //    { Constants.PEPS_Get_RSSI_Measurement, null},
        //    { Constants.PEPS_Immobilizer,null},
        //    { Constants.PEPS_Read_Keyfob,null},
        //    { Constants.PEPS_Get_Key_List, null},
        //    { Constants.PEPS_Temperature_Measurement, null}
        //};

        /// <summary>
        /// Array of bytes representing key list used in environmental monitoring.
        /// </summary>
        private static byte[] KeyList;

        /// <summary>
        /// Token for test running state
        /// </summary>
        private static CancellationToken cancellationToken;

        /// <summary>
        /// Token for test running state
        /// </summary>
        private static AutosarBcmConfiguration monitorConfig;
        #endregion

        #region Public Methods

        /// <summary>
        /// Runs a test in a separate thread periodically.
        /// </summary>
        /// <param name="monitorConfig">A reference to the MonitorConfiguration instance used in the test</param>
        /// <param name="_cancellationToken">A cancellation token that can be used to cancel the task</param>
        internal static void RunTestPeriodically(CancellationToken _cancellationToken, MonitorTestType monitorTestType)
        {

            //cancellationToken = _cancellationToken;
            ////monitorConfig = _monitorConfig;
            ////foreach (var config in monitorConfig.GenericMonitorConfiguration.InputSection.Groups.SelectMany(x => x.InputItemList))
            ////    Program.MainForm.AppendTrace($"{config.Name}: (-1)");

            //Task.Run(() =>
            //{
            //    try
            //    {
            //        if (monitorTestType == MonitorTestType.Generic)
            //        {
            //            var inputMonitorItems = monitorConfig.GenericMonitorConfiguration.InputSection.Groups.SelectMany(i => i.InputItemList).ToList();
            //            var txInterval = monitorConfig.GenericMonitorConfiguration.InputSection.CommonConfig.TxInterval;
            //            var readInterval = monitorConfig.GenericMonitorConfiguration.InputSection.CommonConfig.ReadInterval;

            //            while (!cancellationToken.IsCancellationRequested)
            //            {
            //                foreach (var item in inputMonitorItems)
            //                    item.Transmit(txInterval);

            //                ThreadSleep(readInterval);
            //            }
            //        }
            //        else if (monitorTestType == MonitorTestType.Environmental)
            //        {
            //            //GetRssiMessage(monitorConfig);

            //            StartTime = DateTime.Now;
            //            var cycles = ASContext.Configuration.EnvironmentalTest.Cycles;
            //            //var cycles = monitorConfig.EnvironmentalMonitorConfiguration.OutputSection.Cycles;
            //            var inputItems = monitorConfig.GenericMonitorConfiguration.InputSection.Groups.SelectMany(x => x.InputItemList);
            //            var startCycleIndex = monitorConfig.EnvironmentalMonitorConfiguration.OutputSection.CommonConfig.StartCycleIndex;
            //            var endCycleIndex = monitorConfig.EnvironmentalMonitorConfiguration.OutputSection.CommonConfig.EndCycleIndex;
            //            var dictMapping = new Dictionary<string, InputMonitorItem>();
            //            var continousReadList = new List<InputMonitorItem>();

            //            foreach (var cycle in cycles)
            //                cycle.Items = ASContext.Configuration.Controls
            //                .Where(it => cycle.Functions.Any(c => it.Name.Contains(c) && !string.IsNullOrEmpty(c))).ToList();

            //            var cycleDict = GetCycleDict(cycles);

            //            foreach (var mapping in monitorConfig.EnvironmentalMonitorConfiguration.MappingSection.ConnectionMappings)
            //            {
            //                var inputItem = inputItems.Where(x => x.Name.Equals(mapping.InputName)).FirstOrDefault();
            //                dictMapping.Add(mapping.OutputName, inputItem);
            //            }

            //            foreach (var funcName in monitorConfig.EnvironmentalMonitorConfiguration.MappingSection.ContinousReadList)
            //            {
            //                var inputItem = inputItems.Where(x => x.Name.Equals(funcName)).FirstOrDefault();
            //                continousReadList.Add(inputItem);
            //            }

            //            StartEnvironmentalTest(monitorConfig, cycleDict, startCycleIndex, endCycleIndex, dictMapping, continousReadList);

            //            ThreadSleep(250);

            //            StopEnvironmentalTest(monitorConfig, dictMapping);
            //        }
            //    }
            //    finally
            //    {
            //    }
            //}, cancellationToken);
        }

        /// <summary>
        /// Processes newly received messages and updates the key list if specific conditions are met.
        /// </summary>
        /// <param name="data">The byte array containing the received message data.</param>
        public static void NewMessageReceived(byte[] data)
        {
            if (KeyList != null)
            {
                if(Helper.ByteArrayToString(data.Skip(5).Take(4).ToArray()) == Helper.ByteArrayToString(KeyList))
                    return;
            }

            if (data[3] == 0xEF && data[4] == 0x02)
            {
                KeyList = data.Skip(5).Take(4).ToArray();

                var rssiMesurement = monitorConfig.GenericMonitorConfiguration.OutputSection.Groups.Where(x => x.Name == Constants.PEPS).FirstOrDefault().OutputItemList.Where(x => x.Name == Constants.PEPS_Get_RSSI_Measurement).FirstOrDefault();
                if (rssiMesurement == null)
                    return;
                KeyList.CopyTo(rssiMesurement.PEPSData, 3);
                //RssiDictionary[Constants.PEPS_Get_RSSI_Measurement] = new UdsMessage { Id = rssiMesurement.MessageIdOrDefault, Data = rssiMesurement.PEPSData };
            }
        }

        #endregion

        #region Private Methods

        private static void StartEnvironmentalTest(AutosarBcmConfiguration monitorConfig, Dictionary<int, Core.Cycle> cycleDict, int startCycleIndex, int endCycleIndex, Dictionary<string, InputMonitorItem> dictMapping, List<InputMonitorItem> continousReadList)
        {
            //var cycleIndex = 0;
            //var reboots = 0;

            //List<OutputMonitorItem> softContinuousDiagList = new List<OutputMonitorItem>();

            //for (int i = 0; i < startCycleIndex; i++)
            //{
            //    if (!cycleDict.ContainsKey(i))
            //        continue;
            //    var list = cycleDict[i].OpenItems.Where(it => it.ReadDiagData.Length > 0).ToList();
            //    softContinuousDiagList.AddRange(list.Where(it => !softContinuousDiagList.Contains(it)));
            //}

            //var groupedSoftContinuousDiagList = Helper.GroupList(softContinuousDiagList, (endCycleIndex - startCycleIndex + 1) > softContinuousDiagList.Count ? softContinuousDiagList.Count : (endCycleIndex - startCycleIndex + 1));

            //var timer = new MMTimer(monitorConfig.EnvironmentalMonitorConfiguration.OutputSection.CommonConfig.CycleTime, 0, MMTimer.EventType.Repeating, () => TickHandler(groupedSoftContinuousDiagList, monitorConfig, cycleDict, startCycleIndex, endCycleIndex, dictMapping, continousReadList, ref cycleIndex, ref reboots));

            //Helper.WriteCycleMessageToLogFile(string.Empty, string.Empty, string.Empty, Constants.EnvironmentalStarted, Constants.DefaultEscapeCharacter);
            //timer.Start();
            //while (!cancellationToken.IsCancellationRequested)
            //    ThreadSleep(250);
            //timer.Stop();
            //Helper.WriteCycleMessageToLogFile(string.Empty, string.Empty, string.Empty, Constants.EnvironmentalFinished, Constants.DefaultEscapeCharacter);
        }

        private static void StopEnvironmentalTest(AutosarBcmConfiguration monitorConfig, Dictionary<string, InputMonitorItem> dictMapping)
        {
            //Program.MainForm.ChangeTabControlStatus(false);
            //FormMain.IsTestRunning = true;
            //Helper.WriteCycleMessageToLogFile(string.Empty, string.Empty, string.Empty, Constants.ClosingOutputsStarted, Constants.DefaultEscapeCharacter);
            //var closeCyle = new Cycle { CloseItems = monitorConfig.GenericMonitorConfiguration.OutputSection.Groups.SelectMany(x => x.OutputItemList).ToList() };

            //for (int i = 0; i < 3; i++)
            //    StopCycle(monitorConfig, closeCyle, dictMapping, true);

            //Helper.WriteCycleMessageToLogFile(string.Empty, string.Empty, string.Empty, Constants.ClosingOutputsFinished, Constants.DefaultEscapeCharacter);
            //FormMain.IsTestRunning = false;
            //Program.MainForm.ChangeTabControlStatus(true);
        }

        /// <summary>
        /// Creates a dictionary which includes cycle index and cycle.
        /// </summary>
        /// <param name="cycles">List of cycles.</param>
        private static Dictionary<int,Core.Cycle> GetCycleDict(List<Core.Cycle> cycles)
        {
            var cycleDict = new Dictionary<int, Core.Cycle>();    
            foreach(var cycle in cycles)
            {
                if (cycleDict.ContainsKey(cycle.OpenAt))
                    cycleDict[cycle.OpenAt].OpenItems.AddRange(cycle.Items);
                else
                {
                    cycleDict.Add(cycle.OpenAt, new Core.Cycle(cycle));
                    cycleDict[cycle.OpenAt].OpenItems.AddRange(cycle.Items);
                }

                if (cycle.CloseAt == -1)
                    continue;

                if (cycleDict.ContainsKey(cycle.CloseAt))
                    cycleDict[cycle.CloseAt].CloseItems.AddRange(cycle.Items);
                else
                {
                    cycleDict.Add(cycle.CloseAt, new Core.Cycle(cycle));
                    cycleDict[cycle.CloseAt].CloseItems.AddRange(cycle.Items);
                }
            }

            return cycleDict;
        }

        /// <summary>
        /// Handles the tick event in the monitoring cycle, managing start and stop events, and progress updates.
        /// </summary>
        /// <param name="monitorConfig">Configuration for the monitor.</param>
        /// <param name="cycleDict">Dictionary of cycles in the monitoring process.</param>
        /// <param name="dictMapping">Mapping dictionary for input monitor items.</param>
        /// <param name="continousReadList">List of items for continuous reading.</param>
        /// <param name="cycleIndex">Reference to the current cycle index.</param>
        /// <param name="reboots">Reference to the count of reboots.</param>
        private static void TickHandler(List<List<OutputMonitorItem>> softContinuousDiagList, AutosarBcmConfiguration monitorConfig, Dictionary<int, Core.Cycle> cycleDict, int startCycleIndex, int endCycleIndex,Dictionary<string, InputMonitorItem> dictMapping, List<InputMonitorItem> continousReadList, ref int cycleIndex, ref int reboots)
        {
            //if (cancellationToken.IsCancellationRequested)
            //    return;

            //var txInterval = monitorConfig.EnvironmentalMonitorConfiguration.OutputSection.CommonConfig.TxInterval;

            //if (cycleIndex == 0 && reboots == 0)
            //    Helper.WriteCycleMessageToLogFile(string.Empty, string.Empty, string.Empty, Constants.StartProcessStarted, Constants.DefaultEscapeCharacter);

            //Helper.WriteCycleMessageToLogFile(string.Empty, string.Empty, string.Empty, $"Loop {cycleIndex + 1} Started at Cycle {reboots + 1}", "\n");

            //if (cycleDict.TryGetValue(cycleIndex + 1, out Cycle cycle))
            //{
            //    StopCycle(monitorConfig, cycle, dictMapping);
            //    StartCycle(monitorConfig, cycle, dictMapping);
            //}

            //if (cycleIndex == startCycleIndex - 1 && reboots == 0)
            //    Helper.WriteCycleMessageToLogFile(string.Empty, string.Empty, string.Empty, Constants.StartProcessCompleted, Constants.DefaultEscapeCharacter);

            //OnEnvMonitorProgress(reboots, cycleIndex);

            //if (cycleIndex >= startCycleIndex - 1)
            //{
            //    if(cycleIndex % 4 == 0)
            //    {
            //        foreach (var item in continousReadList)
            //        {
            //            item.Transmit(txInterval, Constants.ContinousRead);
            //        }
            //    }

            //    if(softContinuousDiagList.Count > 0)
            //    {
            //        var softDiagList = softContinuousDiagList[(cycleIndex + 1 - startCycleIndex) % softContinuousDiagList.Count];
            //        foreach (var softDiag in softDiagList)
            //            softDiag.Transmit(softDiag.ReadDiagData, softDiag.MessageIdOrDefault, txInterval, Constants.SendDiagData);
            //    }
            //}                

            //Helper.WriteCycleMessageToLogFile(string.Empty, string.Empty, string.Empty, $"Loop {cycleIndex + 1} finished at Cycle {reboots + 1}", "\n");

            //if (cycleIndex >= endCycleIndex - 1) 
            //{ 
            //    Interlocked.Exchange(ref cycleIndex, startCycleIndex -1); reboots++; 
            //}
            //else 
            //    Interlocked.Increment(ref cycleIndex);
        }

        /// <summary>
        /// Starts a monitoring cycle, opening items and initiating transmissions.
        /// </summary>
        /// <param name="monitorConfig">Configuration for the monitor.</param>
        /// <param name="cycle">The cycle to start.</param>
        /// <param name="dictMapping">Mapping dictionary for input monitor items.</param>
        private static void StartCycle(AutosarBcmConfiguration monitorConfig, Core.Cycle cycle, Dictionary<string, InputMonitorItem> dictMapping)
        {
            //var txInterval = monitorConfig.EnvironmentalMonitorConfiguration.OutputSection.CommonConfig.TxInterval;
            //var pwmDuty = monitorConfig.EnvironmentalMonitorConfiguration.OutputSection.CommonConfig.PWMDutyOpenValue;
            //var pwmFreq = monitorConfig.EnvironmentalMonitorConfiguration.OutputSection.CommonConfig.PWMFreqOpenValue;
            //foreach (var ouputItem in cycle.OpenItems.OfType<OutputMonitorItem>())
            //{
            //    if (cancellationToken.IsCancellationRequested)
            //        return;

            //    InputMonitorItem inputItem;
            //    if (dictMapping.TryGetValue(ouputItem.Name, out inputItem))
            //    {
            //        if (Program.MappingStateDict.TryGetValue(inputItem.Name, out var errorLogDetect))
            //        {
            //            if (errorLogDetect.ChcekIsError())
            //                Helper.WriteErrorMessageToLogFile(inputItem.Name, inputItem.ItemType, Constants.MappingMismatch, "", "", $"Mapping Output: {string.Format("{0} = {1}", Program.MappingStateDict.GetMatch(inputItem.Name).Item1, errorLogDetect.OutputResponse)} mismatched with Input: {string.Format("{0} = {1}", Program.MappingStateDict.GetMatch(inputItem.Name).Item2, errorLogDetect.InputResponse)}");

            //            Program.MappingStateDict.Remove(inputItem.Name);
            //        }
            //        Program.MappingStateDict.Add(ouputItem.Name, inputItem.Name, new ErrorLogDetectObject().UpdateOutputResponse(MappingOperation.Open ,MappingState.OutputSent, MappingResponse.NOC));
            //    }

            //    ouputItem.Open(pwmDuty,pwmFreq);
            //    ThreadSleep(txInterval);
            //    ReadDiagAdcCurrent(ouputItem, txInterval);

            //    if(inputItem != null)
            //    {
            //        if (Program.MappingStateDict.TryGetValue(inputItem.Name, out var errorLogDetect))
            //            Program.MappingStateDict.UpdateValue(inputItem.Name, errorLogDetect.UpdateInputResponse(MappingState.InputSent, MappingResponse.NOC));

            //        inputItem.Transmit(txInterval, Constants.MappingRead);
            //    }

            //    if (ouputItem.ItemType == Constants.PEPS)
            //    {
            //        //if(RssiDictionary.TryGetValue(ouputItem.Name, out var udsMessage))
            //        //{
            //        //    if(udsMessage != null)
            //        //    {
            //        //        udsMessage.Transmit();
            //        //        Helper.WriteCycleMessageToLogFile(ouputItem.Name, ouputItem.ItemType, Constants.PEPS);
            //        //    }
            //        //}
            //    }
            //}
        }

        /// <summary>
        /// Stops a monitoring cycle, closing items and finishing transmissions.
        /// </summary>
        /// <param name="monitorConfig">Configuration for the monitor.</param>
        /// <param name="cycle">The cycle to stop.</param>
        /// <param name="dictMapping">Mapping dictionary for input monitor items.</param>
        private static void StopCycle(AutosarBcmConfiguration monitorConfig, Core.Cycle cycle, Dictionary<string, InputMonitorItem> dictMapping,bool isTestClosing = false)
        {
            //var txInterval = monitorConfig.EnvironmentalMonitorConfiguration.OutputSection.CommonConfig.TxInterval;
            //txInterval = isTestClosing ? txInterval*2 : txInterval;

            //var pwmDuty = monitorConfig.EnvironmentalMonitorConfiguration.OutputSection.CommonConfig.PWMDutyCloseValue;
            //var pwmFreq = monitorConfig.EnvironmentalMonitorConfiguration.OutputSection.CommonConfig.PWMFreqCloseValue;

            //foreach (var ouputItem in cycle.CloseItems.OfType<OutputMonitorItem>())
            //{
            //    InputMonitorItem inputItem;
                
            //    if (dictMapping.TryGetValue(ouputItem.Name, out inputItem))
            //    {
            //        if (Program.MappingStateDict.TryGetValue(inputItem.Name, out var errorLogDetect))
            //        {
            //            if (errorLogDetect.ChcekIsError())
            //                Helper.WriteErrorMessageToLogFile(inputItem.Name, inputItem.ItemType, Constants.MappingMismatch, "", "", $"Mapping Output: {string.Format("{0} = {1}", Program.MappingStateDict.GetMatch(inputItem.Name).Item1, errorLogDetect.OutputResponse)} mismatched with Input: {string.Format("{0} = {1}", Program.MappingStateDict.GetMatch(inputItem.Name).Item2, errorLogDetect.InputResponse)}");

            //            Program.MappingStateDict.Remove(inputItem.Name);
            //        }
            //        Program.MappingStateDict.Add(ouputItem.Name, inputItem.Name, new ErrorLogDetectObject().UpdateOutputResponse(MappingOperation.Close, MappingState.OutputSent, MappingResponse.NOC));
            //    }

            //    ouputItem.Close(pwmDuty, pwmFreq);
            //    ThreadSleep(txInterval);
            //    ReadDiagAdcCurrent(ouputItem, txInterval);

            //    if (inputItem != null)
            //    {
            //        if (Program.MappingStateDict.TryGetValue(inputItem.Name, out var errorLogDetect))
            //            Program.MappingStateDict.UpdateValue(inputItem.Name, errorLogDetect.UpdateInputResponse(MappingState.InputSent, MappingResponse.NOC));

            //        inputItem.Transmit(txInterval, Constants.MappingRead);
            //    }
            //}
        }

        /// <summary>
        /// Reads diagnostic and ADC data for a given output monitor item.
        /// </summary>
        /// <param name="item">The output monitor item to read data from.</param>
        /// <param name="sleepTime">The sleep time between data reads.</param>
        private static void ReadDiagAdcCurrent(OutputMonitorItem item,int sleepTime)
        {
            if (item.ReadDiagData?.Length > 0)
            {
                item.Transmit(item.ReadDiagData, item.MessageIdOrDefault, sleepTime, Constants.SendDiagData);
            }
            //Read adc if current not exists
            if (item.ReadCurrentData?.Length > 0)
            {
                item.Transmit(item.ReadCurrentData, item.MessageIdOrDefault, sleepTime, Constants.SendCurrentData);
            }
            else if (item.ReadADCData?.Length > 0)
            {
                item.Transmit(item.ReadADCData, item.MessageIdOrDefault, sleepTime, Constants.SendADCData);
            }
        }

        /// <summary>
        /// Invokes the environmental monitor progress event with updated information.
        /// </summary>
        /// <param name="reboots">The current reboot count.</param>
        /// <param name="cycleIndex">The current cycle index.</param>
        private static void OnEnvMonitorProgress(int reboots, int cycleIndex)
        {
            EnvMonitorProgress?.Invoke(new EnvironmentalEventArgs
            {
                ElapsedTime = DateTime.Now - StartTime,
                Loop = cycleIndex,
                Reboots = reboots
            });
        }

        /// <summary>
        /// Retrieves and prepares the RSSI message for transmission based on monitor configuration.
        /// </summary>
        /// <param name="monitorConfig">Configuration for the monitor.</param>
        /// <returns>A prepared UdsMessage for RSSI measurement.</returns>
        private static void GetRssiMessage(AutosarBcmConfiguration monitorConfig)
        {
            var pepsGroup = monitorConfig.GenericMonitorConfiguration.OutputSection.Groups.Where(x => x.Name == Constants.PEPS).FirstOrDefault();
            if (pepsGroup == null) 
                return;

            var getKeyList = pepsGroup.OutputItemList.Where(x => x.Name == Constants.PEPS_Get_Key_List).FirstOrDefault();
            if (getKeyList == null)
                return;
            //RssiDictionary[Constants.PEPS_Get_Key_List] = new UdsMessage { Id = getKeyList.MessageIdOrDefault, Data = getKeyList.PEPSData };

            //var immobilizer = pepsGroup.OutputItemList.Where(x => x.Name == Constants.PEPS_Immobilizer).FirstOrDefault();
            //if (immobilizer != null)
            //    RssiDictionary[Constants.PEPS_Immobilizer] = new UdsMessage { Id = immobilizer.MessageIdOrDefault, Data = immobilizer.PEPSData };

            //var readKeyfob = pepsGroup.OutputItemList.Where(x => x.Name == Constants.PEPS_Read_Keyfob).FirstOrDefault();
            //if (readKeyfob != null)
            //    RssiDictionary[Constants.PEPS_Read_Keyfob] = new UdsMessage { Id = readKeyfob.MessageIdOrDefault, Data = readKeyfob.PEPSData };

            //var temperature = pepsGroup.OutputItemList.Where(x => x.Name == Constants.PEPS_Temperature_Measurement).FirstOrDefault();
            //if (temperature != null)
            //    RssiDictionary[Constants.PEPS_Temperature_Measurement] = new UdsMessage { Id = temperature.MessageIdOrDefault, Data = temperature.PEPSData };

            //var rssiMesurement = pepsGroup.OutputItemList.Where(x => x.Name == Constants.PEPS_Get_RSSI_Measurement).FirstOrDefault();
            //if (rssiMesurement == null) 
            //    return;

            //RssiDictionary[Constants.PEPS_Get_Key_List].Transmit();

            Thread.Sleep(5000);
            if (KeyList == null) 
                return;

            //var data = rssiMesurement.PEPSData;
            //KeyList.CopyTo(data, 3);
            //RssiDictionary[Constants.PEPS_Get_RSSI_Measurement] = new UdsMessage { Id = rssiMesurement.MessageIdOrDefault, Data = data };
        }

        /// <summary>
        /// Pauses the thread for a specified duration.
        /// </summary>
        /// <param name="threadSleep">The time in milliseconds for the thread to sleep.</param>
        private static void ThreadSleep(int threadSleep)
        {
            if (threadSleep > 0)
                Thread.Sleep(threadSleep);
        }

        #endregion
    }

    /// <summary>
    /// Custom event arguments class for environmental events, providing details like elapsed time, loop count, and reboot count.
    /// </summary>
    public class EnvironmentalEventArgs : EventArgs
    {
        #region Properties

        /// <summary>
        /// Represents the elapsed time for a particular process or operation.
        /// </summary>
        public TimeSpan ElapsedTime { get; set; }

        /// <summary>
        /// Indicates the current loop or iteration number in a process.
        /// </summary>
        public int Loop { get; set; }

        /// <summary>
        /// Counts the number of reboots or restarts that have occurred during a process.
        /// </summary>
        public int Reboots { get; set; }

        #endregion
    }
}
