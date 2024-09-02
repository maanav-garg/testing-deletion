using AutosarBCM.Core;
using AutosarBCM.Forms.Monitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

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
        void SessionControlManagement(bool isActive);
    }

    /// <summary>
    /// Represents a UI form that can run tests for one time.
    /// </summary>
    public interface IClickTest
    {
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
        /// <summary>
        /// Static property to store the start time of range group.
        /// </summary>
        private static string RangeStartTime { get; set; }
        /// <summary>
        /// Static property to store cycle loop value.
        /// </summary>
        private static int reboots = 0;
        /// <summary>
        /// Static property to store cycle range value.
        /// </summary>
        private static int cycleRange = 0;

        /// <summary>
        /// Token for test running state
        /// </summary>
        private static CancellationToken cancellationToken;
        private static MMTimer timer;
        #endregion

        #region Public Methods

        /// <summary>
        /// Runs a test in a separate thread periodically.
        /// </summary>
        /// <param name="monitorConfig">A reference to the MonitorConfiguration instance used in the test</param>
        /// <param name="_cancellationToken">A cancellation token that can be used to cancel the task</param>
        internal static void RunTestPeriodically(CancellationToken _cancellationToken, MonitorTestType monitorTestType)
        {

            cancellationToken = _cancellationToken;
            ////monitorConfig = _monitorConfig;
            ////foreach (var config in monitorConfig.GenericMonitorConfiguration.InputSection.Groups.SelectMany(x => x.InputItemList))
            ////    Program.MainForm.AppendTrace($"{config.Name}: (-1)");

            Task.Run(() =>
            {
                try
                {
                    if (monitorTestType == MonitorTestType.Generic)
                    {
                        var genericMonitorItems = ASContext.Configuration.Controls.Where(c => c.Group == "DID" && c.Services.Contains((byte)SIDDescription.SID_READ_DATA_BY_IDENTIFIER));
                        ASContext.Configuration.Settings.TryGetValue("TxInterval", out string txInterval);
                        ASContext.Configuration.Settings.TryGetValue("ReadInterval", out string readInterval);

                        while (!cancellationToken.IsCancellationRequested)
                        {
                            foreach (var item in genericMonitorItems)
                            {

                                bool defaultSessionMatch = item.Services.Any(service => ASContext.CurrentSession.AvailableServices.Contains(service));
                                bool activeExceptionMatch = item.SessionActiveException.Any(exception => exception == ASContext.CurrentSession.ID);
                                bool inactiveExceptionMatch = item.SessionInactiveException.Any(exception => exception == ASContext.CurrentSession.ID);

                                if (((defaultSessionMatch || activeExceptionMatch) && !inactiveExceptionMatch))
                                {
                                    ThreadSleep(int.Parse(txInterval));
                                    item.Transmit(ServiceInfo.ReadDataByIdentifier);
                                }
                            }
                            ThreadSleep(int.Parse(readInterval));
                        }
                    }
                    else if (monitorTestType == MonitorTestType.Environmental)
                    {
                        StartTime = DateTime.Now;
                        var cycles = ASContext.Configuration.EnvironmentalTest.Environments.First(x => x.Name == EnvironmentalTest.CurrentEnvironment).Cycles;
                        var controlItems = ASContext.Configuration.Controls.Where(c => c.Group == "DID");
                        var startCycleIndex = ASContext.Configuration.EnvironmentalTest.Environments.First(x => x.Name == EnvironmentalTest.CurrentEnvironment).EnvironmentalConfig.StartCycleIndex;
                        var endCycleIndex = ASContext.Configuration.EnvironmentalTest.Environments.First(x => x.Name == EnvironmentalTest.CurrentEnvironment).EnvironmentalConfig.EndCycleIndex;
                        cycleRange = ASContext.Configuration.EnvironmentalTest.Environments.First(e => e.Name == EnvironmentalTest.CurrentEnvironment).EnvironmentalConfig.CycleRange;
                        //var dictMapping = new Dictionary<string, InputMonitorItem>();
                        var dictMapping = new Dictionary<string, (Mapping, ControlInfo)>();
                        //var continousReadList = new List<InputMonitorItem>();
                        Dictionary<ControlInfo, string> continousReadList = new Dictionary<ControlInfo, string>();
                        Helper.InitializeUnopenedPayloadLogFile();
                        FormEnvironmentalTest.OpenedControlList.Clear();
                        FormEnvironmentalTest.UnopenedControlList.Clear();

                        var cycleDict = GetCycleDict(cycles);

                        //foreach (var mapping in monitorConfig.EnvironmentalMonitorConfiguration.MappingSection.ConnectionMappings)
                        //{
                        //    var inputItem = inputItems.Where(x => x.Name.Equals(mapping.InputName)).FirstOrDefault();
                        //    dictMapping.Add(mapping.OutputName, inputItem);
                        //}

                        foreach (var mapping in ASContext.Configuration.EnvironmentalTest.ConnectionMappings)
                        {
                            var controlItem = controlItems.Where(x => x.Name.Equals(mapping.Input.Control)).FirstOrDefault();
                            dictMapping.Add(mapping.Output.Name, (mapping, controlItem));
                        }

                        foreach (var funcName in ASContext.Configuration.EnvironmentalTest.Environments.First(x => x.Name == EnvironmentalTest.CurrentEnvironment).ContinousReadList)
                        {

                            var controlItem = controlItems.Where(x => x.Name.Equals(funcName.Control)).FirstOrDefault();
                            if (controlItem != null)
                                continousReadList.Add(controlItem, funcName.Name);

                        }
                        //continousReadList = continousReadList.Distinct;

                        //TODO to be checked
                        //foreach (var func in ASContext.Configuration.EnvironmentalTest.ContinousReadList)
                        //{
                        //    var controlItem = controlItems.Where(x => x.Name.Equals(func.Parent)).FirstOrDefault();
                        //    if (controlItem == null) continue;
                        //    var payloadInfo = controlItem.Responses.FirstOrDefault().Payloads.FirstOrDefault(p => p.Name == func.Name);
                        //    if (payloadInfo == null) continue;
                        //    continousReadList.Add(payloadInfo.Name, controlItem);
                        //}

                        StartEnvironmentalTest(cycleDict, startCycleIndex, endCycleIndex, dictMapping, continousReadList);

                        ThreadSleep(250);

                        StopEnvironmentalTest(cycleDict, dictMapping);
                    }
                }
                finally
                {
                }
            }, cancellationToken);
        }

        /// <summary>
        /// Processes newly received messages and updates the key list if specific conditions are met.
        /// </summary>
        /// <param name="data">The byte array containing the received message data.</param>
        public static void NewMessageReceived(byte[] data)
        {
            //if (KeyList != null)
            //{
            //    if(Helper.ByteArrayToString(data.Skip(5).Take(4).ToArray()) == Helper.ByteArrayToString(KeyList))
            //        return;
            //}

            //if (data[3] == 0xEF && data[4] == 0x02)
            //{
            //    KeyList = data.Skip(5).Take(4).ToArray();

            //    var rssiMesurement = monitorConfig.GenericMonitorConfiguration.OutputSection.Groups.Where(x => x.Name == Constants.PEPS).FirstOrDefault().OutputItemList.Where(x => x.Name == Constants.PEPS_Get_RSSI_Measurement).FirstOrDefault();
            //    if (rssiMesurement == null)
            //        return;
            //    KeyList.CopyTo(rssiMesurement.PEPSData, 3);
            //    //RssiDictionary[Constants.PEPS_Get_RSSI_Measurement] = new UdsMessage { Id = rssiMesurement.MessageIdOrDefault, Data = rssiMesurement.PEPSData };
            //}
        }

        #endregion

        #region Private Methods

        private static void StartEnvironmentalTest(Dictionary<int, Cycle> cycleDict, int startCycleIndex, int endCycleIndex, Dictionary<string, (Mapping, ControlInfo)> dictMapping, Dictionary<ControlInfo, string> continousReadList)
        {
            var cycleIndex = 0;
            FormEnvironmentalTest formEnvTest = (FormEnvironmentalTest)Application.OpenForms[Constants.Form_Environmental_Test];
            formEnvTest.SetCounter(1, 1);
            timer = new MMTimer(0, MMTimer.EventType.OneTime, () => TickHandler(cycleDict, startCycleIndex, endCycleIndex, dictMapping, continousReadList, ref cycleIndex));            Helper.WriteErrorMessageToLogFile(string.Empty, string.Empty, string.Empty, "Imported File: " + FormMain.fileName, Constants.DefaultEscapeCharacter);
            Helper.WriteCycleMessageToLogFile(string.Empty, string.Empty, string.Empty, Constants.EnvironmentalStarted, Constants.DefaultEscapeCharacter);
            timer.Next(ASContext.Configuration.EnvironmentalTest.Environments.First(x => x.Name == EnvironmentalTest.CurrentEnvironment).EnvironmentalConfig.CycleTime);
            while (!cancellationToken.IsCancellationRequested)
                ThreadSleep(250);
            timer.Stop();

        }

        private static void StopEnvironmentalTest(Dictionary<int, Cycle> cycleDict, Dictionary<string, (Mapping, ControlInfo)> dictMapping)
        {
            Helper.WriteCycleMessageToLogFile(string.Empty, string.Empty, string.Empty, Constants.EnvironmentalFinished, Constants.DefaultEscapeCharacter);
            Helper.WriteCycleMessageToLogFile(string.Empty, string.Empty, string.Empty, Constants.ClosingOutputsStarted, Constants.DefaultEscapeCharacter);
            var txInterval = ASContext.Configuration.EnvironmentalTest.Environments.First(x => x.Name == EnvironmentalTest.CurrentEnvironment).EnvironmentalConfig.TxInterval;
            txInterval = false ? txInterval * 2 : txInterval;

            List<Function> functions = new List<Function>();
            foreach (var function in cycleDict.SelectMany(c => c.Value.CloseItems))
            {
                if (function.Scenario != null)
                {
                    var scenario = ASContext.Configuration.EnvironmentalTest.Environments.First(x => x.Name == EnvironmentalTest.CurrentEnvironment).Scenarios.Where(s => s.Name == function.Scenario).FirstOrDefault();
                    var cInfo = ASContext.Configuration.GetControlByAddress(scenario.Address);
                    var payloads = scenario.OpenPayloads.Union(scenario.ClosePayloads).ToList();

                    var ctrl = functions.Where(f => f.ControlInfo.Name == cInfo.Name).FirstOrDefault();
                    if (ctrl == null)
                        functions.Add(new Function { ControlInfo = cInfo, Payloads = payloads });
                    else
                        ctrl.Payloads.AddRange(payloads);
                }
                else
                {
                    var ctrl = functions.Where(f => f.Control == function.Control).FirstOrDefault();
                    if (ctrl == null)
                    {
                        var func = new Function { Control = function.Control, ControlInfo = function.ControlInfo, Payloads = function.Payloads.ToList() };
                        functions.Add(func);
                    }
                    else
                    {
                        ctrl.Payloads.AddRange(function.Payloads);
                    }
                }
            }

            foreach (var function in functions)
            {
                if (function?.ControlInfo == null)
                    continue;
                var hasDIDBitsOnOff = function.ControlInfo.Responses.SelectMany(r => r.Payloads).Any(p => p.TypeName == "DID_Bits_On_Off");
                if (hasDIDBitsOnOff)
                {
                    function.ControlInfo.SwitchForBits(function.Payloads.Distinct().ToList(), false);
                }
                else
                {
                    function.ControlInfo.Switch(function.Payloads.Distinct().ToList(), false);
                }
                Thread.Sleep(txInterval);
            }
            ProcessUnopenedPayloads();
            
            Helper.WriteCycleMessageToLogFile(string.Empty, string.Empty, string.Empty, Constants.ClosingOutputsFinished, Constants.DefaultEscapeCharacter);
            
            FormMain.IsTestRunning = !FormMain.IsTestRunning;
            FormEnvironmentalTest formEnvTest = (FormEnvironmentalTest)Application.OpenForms[Constants.Form_Environmental_Test];
            formEnvTest.SetStartBtnVisual();
        }

        /// <summary>
        /// Creates a dictionary which includes cycle index and cycle.
        /// </summary>
        /// <param name="cycles">List of cycles.</param>
        public static Dictionary<int, Cycle> GetCycleDict(List<Cycle> cycles)
        {
            var cycleDict = new Dictionary<int, Cycle>();
            foreach (var cycle in cycles)
            {
                if (cycleDict.ContainsKey(cycle.OpenAt))
                {
                    cycleDict[cycle.OpenAt].OpenItems.UnionWith(cycle.Functions);
                }
                else
                {
                    cycleDict.Add(cycle.OpenAt, new Cycle(cycle));
                    cycleDict[cycle.OpenAt].OpenItems.UnionWith(cycle.Functions);
                }

                if (cycle.CloseAt == -1)
                    continue;

                if (cycleDict.ContainsKey(cycle.CloseAt))
                {
                    cycleDict[cycle.CloseAt].CloseItems.UnionWith(cycle.Functions);
                }
                else
                {
                    cycleDict.Add(cycle.CloseAt, new Cycle(cycle));
                    cycleDict[cycle.CloseAt].CloseItems.UnionWith(cycle.Functions);
                }
            }

            return cycleDict;
        }

        /// <summary>
        /// Handles the tick event in the monitoring cycle, managing start and stop events, and progress updates.
        /// </summary>
        /// <param name="cycleDict">Dictionary of cycles in the monitoring process.</param>
        /// <param name="dictMapping">Mapping dictionary for input monitor items.</param>
        /// <param name="continousReadList">List of items for continuous reading.</param>
        /// <param name="cycleIndex">Reference to the current cycle index.</param>
        private static void TickHandler(Dictionary<int, Cycle> cycleDict, int startCycleIndex, int endCycleIndex, Dictionary<string, (Mapping, ControlInfo)> dictMapping, Dictionary<ControlInfo, string> continousReadList, ref int cycleIndex)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            if (cancellationToken.IsCancellationRequested)
                return;

            var txInterval = ASContext.Configuration.EnvironmentalTest.Environments.First(x => x.Name == EnvironmentalTest.CurrentEnvironment).EnvironmentalConfig.TxInterval;
            if (cycleIndex == 0 && reboots == 0)
            {
                Helper.WriteCycleMessageToLogFile(string.Empty, string.Empty, string.Empty, Constants.StartProcessStarted, Constants.DefaultEscapeCharacter);
                RangeStartTime = DateTime.Now.ToString("dd-MM-yyyy_HH:mm:ss");
            }

            Helper.WriteCycleMessageToLogFile(string.Empty, string.Empty, string.Empty, $"Loop {cycleIndex + 1} Started at Cycle {reboots + 1}", "\n");

            FormEnvironmentalTest formEnvTest = (FormEnvironmentalTest)Application.OpenForms[Constants.Form_Environmental_Test];
            formEnvTest?.SetCounter(reboots + 1, cycleIndex + 1);


            if (cycleIndex == startCycleIndex - 1 && reboots == 0)
                Helper.WriteCycleMessageToLogFile(string.Empty, string.Empty, string.Empty, Constants.StartProcessCompleted, Constants.DefaultEscapeCharacter);

            if (cycleDict.TryGetValue(cycleIndex + 1, out Cycle cycle))
            {
                if (!(cycleIndex + 1 == 16 && reboots + 1 == 1))
                    StopCycle(cycle, dictMapping);
                StartCycle(cycle, dictMapping);
            }

            if (cycleIndex >= startCycleIndex - 1)
            {
                if (cycleIndex % 4 == 0)
                {
                    Helper.SendExtendedDiagSession();
                    ThreadSleep(txInterval);
                    //new ReadDTCInformationService().Transmit();
                    foreach (var item in continousReadList.Keys)
                    {
                        ThreadSleep(txInterval);
                        Helper.WriteCycleMessageToLogFile(item.Name, continousReadList[item], Constants.ContinousRead);
                        Helper.AddMessageMappingDict(item.Name, continousReadList[item], Constants.ContinousRead);
                        item.Transmit(ServiceInfo.ReadDataByIdentifier);
                        
                        
                    }
                    //TODO to be added again
                    //ThreadSleep(txInterval);
                    //new ClearDTCInformation().Transmit();
                }
            }

            Helper.WriteCycleMessageToLogFile(string.Empty, string.Empty, string.Empty, $"Loop {cycleIndex + 1} finished at Cycle {reboots + 1}", "\n");

            if (cycleIndex >= endCycleIndex - 1)
            {
                Interlocked.Exchange(ref cycleIndex, startCycleIndex - 1);
                reboots++;
                Helper.SendExtendedDiagSession();
                
                if (reboots % cycleRange == 0)
                {
                    ProcessUnopenedPayloads();
                    RangeStartTime = DateTime.Now.ToString("dd-MM-yyyy_HH:mm:ss");
                }   
            }
            else
                Interlocked.Increment(ref cycleIndex);

            stopwatch.Stop();
            var elapsed = ASContext.Configuration.EnvironmentalTest.Environments.First(x => x.Name == EnvironmentalTest.CurrentEnvironment).EnvironmentalConfig.CycleTime - stopwatch.ElapsedMilliseconds;
            if (elapsed <= 0)
                TickHandler(cycleDict, startCycleIndex, endCycleIndex, dictMapping, continousReadList, ref cycleIndex);
            else
                timer.Next((int)elapsed);
        }

        /// <summary>
        /// Starts a monitoring cycle, opening items and initiating transmissions.
        /// </summary>
        /// <param name="monitorConfig">Configuration for the monitor.</param>
        /// <param name="cycle">The cycle to start.</param>
        /// <param name="dictMapping">Mapping dictionary for input monitor items.</param>
        private static void StartCycle(Cycle cycle, Dictionary<string, (Mapping, ControlInfo)> dictMapping)
        {
            var txInterval = ASContext.Configuration.EnvironmentalTest.Environments.First(x => x.Name == EnvironmentalTest.CurrentEnvironment).EnvironmentalConfig.TxInterval;
            var pwmDuty = ASContext.Configuration.EnvironmentalTest.Environments.First(x => x.Name == EnvironmentalTest.CurrentEnvironment).EnvironmentalConfig.PWMDutyOpenValue;
            var pwmFreq = ASContext.Configuration.EnvironmentalTest.Environments.First(x => x.Name == EnvironmentalTest.CurrentEnvironment).EnvironmentalConfig.PWMFreqOpenValue;
            var mapControls = new Dictionary<ControlInfo, string>();
            foreach (var function in cycle.OpenItems)
            {
                if (cancellationToken.IsCancellationRequested)
                    return;

                (Mapping, ControlInfo) mappedItem = (null, null);
                foreach (var payload in function.Payloads)
                {
                    if (dictMapping.TryGetValue(payload, out mappedItem))
                    {
                        if (Program.MappingStateDict.TryGetValue(mappedItem.Item1.Input.Name, out var errorLogDetect))
                        {
                            if (errorLogDetect.CheckIsError())
                                Helper.WriteErrorMessageToLogFile(mappedItem.Item1.Input.Control, $"O: {mappedItem.Item1.Output.Control} ({mappedItem.Item1.Output.Name}) - I: {mappedItem.Item1.Input.Control} ({mappedItem.Item1.Input.Name})", Constants.MappingMismatch, "", "", $"Mapping Output: {string.Format("{0} = {1}", Program.MappingStateDict.GetMatch(mappedItem.Item1.Input.Name).Item1, errorLogDetect.OutputResponse)} mismatched with Input: {string.Format("{0} = {1}", Program.MappingStateDict.GetMatch(mappedItem.Item1.Input.Name).Item2, errorLogDetect.InputResponse)}");

                            Program.MappingStateDict.Remove(mappedItem.Item1.Input.Name);
                        }
                        Program.MappingStateDict.Add(payload, mappedItem.Item1.Input.Name, new ErrorLogDetectObject().UpdateOutputResponse(MappingOperation.Open, MappingState.OutputSent, MappingResponse.NOC));

                        if (mappedItem.Item2 != null)
                        {
                            var inputName = ASContext.Configuration.EnvironmentalTest.ConnectionMappings.First(m => m.Output.Name == payload).Input.Name;
                            if (mapControls.Any(c => c.Key.Name == mappedItem.Item1.Input.Control))
                                continue;
                            mapControls.Add(mappedItem.Item2, inputName);
                            Helper.WriteCycleMessageToLogFile(mappedItem.Item1.Input.Control, inputName, (Constants.MappingRead));
                        }
                    }
                }

                if(function.Control != null && !function.Control.Contains(Constants.DummyControl))
                {
                    var hasDIDBitsOnOff = function.ControlInfo.Responses.SelectMany(r => r.Payloads).Any(p => p.TypeName == "DID_Bits_On_Off");
                    if (hasDIDBitsOnOff)
                    {
                        function.ControlInfo.SwitchForBits(function.Payloads, true);
                    }
                    else
                    {
                        function.ControlInfo.Switch(function.Payloads, true);
                    }

                    CloseSensitiveControls(function.ControlInfo, function.Payloads);
                }

                if (function.Scenario != null)
                {
                    var scenario = ASContext.Configuration.EnvironmentalTest.Environments.First(x => x.Name == EnvironmentalTest.CurrentEnvironment).Scenarios.Where(s => s.Name == function.Scenario).FirstOrDefault();
                    if (scenario == null)
                        continue;

                    var controlInfo = ASContext.Configuration.GetControlByAddress(scenario.Address);
                    var payloads = scenario.OpenPayloads.Select(a => (a, true)).Union(scenario.ClosePayloads.Select(a => (a, false))).ToDictionary(x => x.a, x => x.Item2);

                    var hasDIDBitsOnOff = controlInfo.Responses.SelectMany(r => r.Payloads).Any(p => p.TypeName == "DID_Bits_On_Off");
                    if (hasDIDBitsOnOff)
                    {
                        controlInfo.SwitchForBits(payloads);
                    }
                    else
                    {
                        controlInfo.Switch(payloads);
                    }

                    CloseSensitiveControls(controlInfo, scenario.OpenPayloads);
                }

                ThreadSleep(txInterval);

                if (mappedItem.Item2 != null)
                {
                    if (Program.MappingStateDict.TryGetValue(mappedItem.Item1.Input.Control, out var errorLogDetect))
                        Program.MappingStateDict.UpdateValue(mappedItem.Item1.Input.Control, errorLogDetect.UpdateInputResponse(MappingState.InputSent, MappingResponse.NOC));
                }
            }
            foreach (var mapControl in mapControls)
            {
                Helper.AddMessageMappingDict(mapControl.Key.Name, mapControl.Value, Constants.MappingRead);
                mapControl.Key.Transmit(ServiceInfo.ReadDataByIdentifier);
                ThreadSleep(txInterval);
            }
        }

        private static void CloseSensitiveControls(ControlInfo controlInfo, List<string> payloads)
        {
            var sensitivePayloads = ASContext.Configuration.EnvironmentalTest.Environments.First(x => x.Name == EnvironmentalTest.CurrentEnvironment).SensitiveControls.Where(f => f.Control == controlInfo.Name).FirstOrDefault()?.Payloads.Intersect(payloads).ToList();
            if (sensitivePayloads?.Count > 0)
                Task.Delay(ASContext.Configuration.EnvironmentalTest.Environments.First(x => x.Name == EnvironmentalTest.CurrentEnvironment).EnvironmentalConfig.SensitiveCtrlDuration).ContinueWith((_) =>
                {
                    var hasDIDBitsOnOff = controlInfo.Responses.SelectMany(r => r.Payloads).Any(p => p.TypeName == "DID_Bits_On_Off");
                    if (hasDIDBitsOnOff)
                    {
                        controlInfo.SwitchForBits(sensitivePayloads, false);
                    }
                    else
                    {
                        controlInfo.Switch(sensitivePayloads, false);
                    }
                });
        }

        /// <summary>
        /// Stops a monitoring cycle, closing items and finishing transmissions.
        /// </summary>
        /// <param name="monitorConfig">Configuration for the monitor.</param>
        /// <param name="cycle">The cycle to stop.</param>
        /// <param name="dictMapping">Mapping dictionary for input monitor items.</param>
        private static void StopCycle(Cycle cycle, Dictionary<string, (Mapping, ControlInfo)> dictMapping, bool isTestClosing = false)
        {
            var txInterval = ASContext.Configuration.EnvironmentalTest.Environments.First(x => x.Name == EnvironmentalTest.CurrentEnvironment).EnvironmentalConfig.TxInterval;
            txInterval = isTestClosing ? txInterval * 2 : txInterval;

            var pwmDuty = ASContext.Configuration.EnvironmentalTest.Environments.First(x => x.Name == EnvironmentalTest.CurrentEnvironment).EnvironmentalConfig.PWMDutyCloseValue;
            var pwmFreq = ASContext.Configuration.EnvironmentalTest.Environments.First(x => x.Name == EnvironmentalTest.CurrentEnvironment).EnvironmentalConfig.PWMFreqCloseValue;
            var mapControls = new Dictionary<ControlInfo, string>();

            foreach (var function in cycle.CloseItems)
            {
                (Mapping, ControlInfo) mappedItem = (null, null);
                foreach (var payload in function.Payloads)
                {
                    if (dictMapping.TryGetValue(payload, out mappedItem))
                    {
                        if (Program.MappingStateDict.TryGetValue(mappedItem.Item1.Input.Name, out var errorLogDetect))
                        {
                            if (errorLogDetect.CheckIsError())
                                Helper.WriteErrorMessageToLogFile(mappedItem.Item1.Input.Control, $"O: {mappedItem.Item1.Output.Control} ({mappedItem.Item1.Output.Name}) - I: {mappedItem.Item1.Input.Control} ({mappedItem.Item1.Input.Name})", Constants.MappingMismatch, "", "", $"Mapping Output: {string.Format("{0} = {1}", Program.MappingStateDict.GetMatch(mappedItem.Item1.Input.Name).Item1, errorLogDetect.OutputResponse)} mismatched with Input: {string.Format("{0} = {1}", Program.MappingStateDict.GetMatch(mappedItem.Item1.Input.Name).Item2, errorLogDetect.InputResponse)}");

                            Program.MappingStateDict.Remove(mappedItem.Item1.Input.Name);
                        }
                        Program.MappingStateDict.Add(payload, mappedItem.Item1.Input.Name, new ErrorLogDetectObject().UpdateOutputResponse(MappingOperation.Close, MappingState.OutputSent, MappingResponse.NOC));

                        if (mappedItem.Item2 != null)
                        {
                            var inputName = ASContext.Configuration.EnvironmentalTest.ConnectionMappings.First(m => m.Output.Name == payload).Input.Name;
                            if (mapControls.Any(c => c.Key.Name == mappedItem.Item1.Input.Control))
                                continue;
                            mapControls.Add(mappedItem.Item2, inputName);
                            Helper.WriteCycleMessageToLogFile(mappedItem.Item1.Input.Control, inputName, (Constants.MappingRead));
                        }
                    }
                }

                if (function.Control != null && !function.Control.Contains(Constants.DummyControl))
                {
                    var nonSensitivePayloads = function.Payloads.Except(ASContext.Configuration.EnvironmentalTest.Environments.First(x => x.Name == EnvironmentalTest.CurrentEnvironment).SensitiveControls.Where(f => f.Control == function.ControlInfo.Name).FirstOrDefault()?.Payloads ?? new List<string>()).ToList();
                    if (nonSensitivePayloads?.Count == 0)
                        continue;

                    var hasDIDBitsOnOff = function.ControlInfo.Responses.SelectMany(r => r.Payloads).Any(p => p.TypeName == "DID_Bits_On_Off");
                    if (hasDIDBitsOnOff)
                    {
                        function.ControlInfo.SwitchForBits(nonSensitivePayloads, false);
                    }
                    else
                    {
                        function.ControlInfo.Switch(nonSensitivePayloads, false);
                    }
                }

                if (function.Scenario != null)
                {
                    var scenario = ASContext.Configuration.EnvironmentalTest.Environments.First(x => x.Name == EnvironmentalTest.CurrentEnvironment).Scenarios.Where(s => s.Name == function.Scenario).FirstOrDefault();
                    if (scenario == null)
                        continue;

                    var controlInfo = ASContext.Configuration.GetControlByAddress(scenario.Address);
                    var payloads = scenario.OpenPayloads.Union(scenario.ClosePayloads).ToDictionary(x => x, x => false);

                    var hasDIDBitsOnOff = controlInfo.Responses.SelectMany(r => r.Payloads).Any(p => p.TypeName == "DID_Bits_On_Off");
                    if (hasDIDBitsOnOff)
                    {
                        controlInfo.SwitchForBits(payloads);
                    }
                    else
                    {
                        controlInfo.Switch(payloads);
                    }
                }

                ThreadSleep(txInterval);

                if (mappedItem.Item2 != null)
                {
                    if (Program.MappingStateDict.TryGetValue(mappedItem.Item1.Input.Control, out var errorLogDetect))
                        Program.MappingStateDict.UpdateValue(mappedItem.Item1.Input.Control, errorLogDetect.UpdateInputResponse(MappingState.InputSent, MappingResponse.NOC));
                }
                foreach (var mapControl in mapControls)
                {
                    Helper.AddMessageMappingDict(mapControl.Key.Name, mapControl.Value, Constants.MappingRead);
                    mapControl.Key.Transmit(ServiceInfo.ReadDataByIdentifier);
                    ThreadSleep(txInterval);
                }
            }
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

        /// <summary>
        /// Processes and logs the unopened payloads, then clears the unopened payload list.
        /// </summary>
        private static void ProcessUnopenedPayloads()
        {
            var groupStartCount = ((reboots - 1) / cycleRange) * cycleRange;
            Helper.WriteUnopenedPayloadsToLogFile(FormEnvironmentalTest.UnopenedControlList, groupStartCount, reboots, cycleRange, RangeStartTime, DateTime.Now.ToString("dd-MM-yyyy_HH:mm:ss"));
            FormEnvironmentalTest.UnopenedControlList.Clear();
            FormEnvironmentalTest.OpenedControlList.Clear();
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
