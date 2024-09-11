using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO.Ports;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Connection.Hardware;
using Connection.Hardware.Can;
using Connection.Hardware.SP;
using AutosarBCM.Common;
using AutosarBCM.Properties;
using AutosarBCM.Core;
using Connection.Protocol.Uds;

namespace AutosarBCM
{
    /// <summary>
    /// Implements a bridge between the UI layer and the Connection library.
    /// </summary>
    internal class ConnectionUtil
    {
        #region Properties

        /// <summary>
        /// Represents a string used for storing a list of items.
        /// </summary>
        public string listString = "";
        /// <summary>
        /// A reference to the selected device.
        /// </summary>
        public static IHardware hardware = null;
        private static Iso15765 transportProtocol = null;
        /// <summary>
        /// A textual representation indicating the current state of the connection.
        /// </summary>
        public string ConnectionLogger = "";
        /// <summary>
        /// A reference to the main form.
        /// </summary>
        public static FormMain formMain = (FormMain)Application.OpenForms[Constants.Form_Main];
        /// <summary>
        /// A synchronization object used for locking critical sections of code to ensure thread safety.
        /// </summary>
        private static object lockObj = new object();
        private ushort address;
        private byte session;
        private Dictionary<ushort, string> controlDict = new Dictionary<ushort, string>();
        private static byte channelId = 0;

        #endregion

        #region Public Methods

        /// <summary>
        /// Establishes a connection with the selected device.
        /// </summary>
        /// <returns>true if the connection is successfully established; otherwise, false.</returns>
        public bool BaseConnection()
        {
            var hardwareList = CreateHardwareList();
            if (hardwareList?.Count == 0)
            {
                Helper.ShowWarningMessageBox("No device found!");
                return false;
            }
            hardware = SelectHardware(hardwareList);

            if (hardware == null)
                return false;

            try
            {
                InitHardware(hardware);
                FormMain formMain = (FormMain)Application.OpenForms[Constants.Form_Main];

                formMain.txtTrace.ForeColor = Color.Blue;
                formMain.openConnection.Text = "Stop Connection";
                formMain.openConnection.Image = formMain.imageList1.Images[3];
                ConnectionLogger = "Connection is started";
                formMain.AppendTrace(ConnectionLogger, Color.Green);
                formMain.lblConnection.Text = $"Online - {hardware.Name}";
                formMain.lblConnection.ForeColor = Color.Green;
                if (hardware is CanHardware canHardware)
                {

                    if (canHardware is IntrepidCsCan && (canHardware.BitRate > 0))
                    {
                        canHardware.SetBitRate((int)canHardware.BitRate, (int)canHardware.NetworkID);
                        channelId = (byte)canHardware.NetworkID;
                    }
                    else if (canHardware is KvaserCan && canHardware.BitRate > 0)
                    {
                        canHardware.SetBitRate((int)canHardware.BitRate, 0);
                    }

                    transportProtocol = new Iso15765();
                    transportProtocol.Config.PhysicalAddr.TxId = Convert.ToUInt32(Settings.Default.TransmitAdress, 16);
                    transportProtocol.Config.PhysicalAddr.RxId = Convert.ToUInt32(Settings.Default.ReceiveAdress, 16);
                    transportProtocol.Config.BlockSize = Convert.ToByte(Settings.Default.BlockSize, 16);
                    transportProtocol.Config.PaddingByte = Convert.ToByte(Settings.Default.PaddingByte, 16);
                    transportProtocol.Config.StMin = Convert.ToByte(Settings.Default.StMin, 16);
                    transportProtocol.Hardware = hardware;

                    transportProtocol.MessageReceived += TransportProtocol_MessageReceived;
                    transportProtocol.MessageSent += TransportProtocol_MessageSent;
                    transportProtocol.ReceiveError += TransportProtocol_ReceiveError;



                }
                else if (hardware is SerialPortHardware serialHardware)
                {
                    transportProtocol = new Iso15765();
                    transportProtocol.MessageReceived += TransportProtocol_MessageReceived;
                    transportProtocol.MessageSent += TransportProtocol_MessageSent;
                    transportProtocol.ReceiveError += TransportProtocol_ReceiveError;
                    transportProtocol.Hardware = hardware;

                    //serialHardware.FrameRead += SerialHardware_FrameRead;
                    //serialHardware.FrameWritten += SerialHardware_FrameWritten;
                    //serialHardware.ErrorAccured += SerialHardware_ErrorAccured;
                    if (serialHardware.SerialPortType == SerialPortType.UT146)
                    {
                        serialHardware.Transmit("FFFFFFFFFE06");
                        Thread.Sleep(1);
                        serialHardware.Transmit("FFFFFFFFFB01");
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
        }
        public void LoadControlDictionary()
        {
            if (ASContext.Configuration == null)
                return;
            foreach (var control in ASContext.Configuration.Controls)
            {
                if (!controlDict.ContainsKey(control.Address))
                    controlDict.Add(control.Address, control.Name);
            }

        }

        private void TransportProtocol_ReceiveError(object sender, Connection.Protocol.TransportErrorEventArs e)
        {
            if (FormMain.IsTestRunning)
            {
                Helper.SendExtendedDiagSession();
            }

            AppendTrace(e.Message, DateTime.Now, Color.Red);
        }
        private void TransportProtocol_MessageSent(object sender, Connection.Protocol.TransportEventArgs e)
        {
            // Tester present
            if (e.Data[0] == (byte)SIDDescription.SID_TESTER_PRESENT)
                //ServiceInfo.TesterPresent.RequestID)
                return;

            var txId = transportProtocol.Config.PhysicalAddr.RxId.ToString("X") == "0" ? "72E" : transportProtocol.Config.PhysicalAddr.RxId.ToString("X");
            var txRead = $"Tx {txId} {BitConverter.ToString(e.Data)}";
            var time = new DateTime((long)e.Timestamp);

            if (FormMain.EMCMonitoring || FormMain.ControlChecker)
            {
                AppendTrace(txRead, time, Color.Black);
                return;
            }

            // Handle transmitted data -TX- 
            if (e.Data[0] == (byte)SIDDescription.SID_READ_DATA_BY_IDENTIFIER
                || e.Data[0] == (byte)SIDDescription.SID_INPUT_OUTPUT_CONTROL_BY_IDENTIFIER)
            {
                foreach (var receiver in FormMain.Receivers)
                    if (receiver.Sent(BitConverter.ToUInt16(e.Data.Skip(1).Take(2).Reverse().ToArray(), 0))) ;
            }

            if (!Settings.Default.FilterData.Contains(e.Data[0].ToString("X")))
                AppendTrace(txRead, time, Color.Black);
        }

        private void TransportProtocol_MessageReceived(object sender, Connection.Protocol.TransportEventArgs e)
        {
            var service = new ASResponse(e.Data).Parse();
            var rxId = transportProtocol.Config.PhysicalAddr.RxId.ToString("X") == "0" ? "72E" : transportProtocol.Config.PhysicalAddr.RxId.ToString("X");
            //var rxId = transportProtocol.Config.PhysicalAddr.RxId.ToString("X");
            if (e.Data[0] == (byte)SIDDescription.SID_DIAGNOSTIC_SESSION_CONTROL + 0x40)
            {
                session = (byte)e.Data[1];
            }
            var selectedSession = (SessionInfo)ASContext.Configuration?.Sessions?.FirstOrDefault(x => x.ID == session);
            string sName = selectedSession?.Name;

            if (e.Data[0] == (byte)SIDDescription.SID_READ_DATA_BY_IDENTIFIER + 0x40
            || e.Data[0] == (byte)SIDDescription.SID_INPUT_OUTPUT_CONTROL_BY_IDENTIFIER + 0x40)
            {
                address = BitConverter.ToUInt16(e.Data.Skip(1).Take(2).Reverse().ToArray(), 0);
            }
            else
                address = 0;
            string enumName = "";// service.ServiceInfo != null ? service.ServiceInfo.Name : Enum.GetName(typeof(SIDDescription), (byte)(e.Data[0] - 0x40));
            controlDict.TryGetValue(address, out string cName);
            var value = cName != null ? cName : sName != null ? sName : "";
            var rxRead = "";
            if (e.Data[0] != (byte)SIDDescription.SID_NEGATIVE_RESPONSE)
            {
                rxRead = $"Rx {rxId} {BitConverter.ToString(e.Data)} ({value} | {enumName})";
            }
            else
            {
                rxRead = $"Rx {rxId} {BitConverter.ToString(e.Data)}";
            }
            var time = new DateTime((long)e.Timestamp);
            if (service?.ServiceInfo != null)
            {
                if (service?.ServiceInfo == ServiceInfo.TesterPresent)
                    return;

                if (service?.ServiceInfo == ServiceInfo.NegativeResponse)
                {
                    if (e.Data[1] == (byte)SIDDescription.SID_DIAGNOSTIC_SESSION_CONTROL)
                    {
                        FormMain formMain = (FormMain)Application.OpenForms[Constants.Form_Main];
                        if (formMain.dockMonitor.ActiveDocument is IPeriodicTest formInput)
                            formInput.SessionControlManagement(false);
                    }
                    AppendTrace($"{rxRead} ({service.Response.NegativeResponseCode})", time);
                    return;
                }
                if (FormMain.ControlChecker)
                {
                    AppendTrace(rxRead, time);
                    if (address == 0xC151)
                        SendByteDataToControlChecker(e.Data, address);
                    else
                        SendServiceDataToControlChecker(service);
                    return;
                }
                if (FormMain.EMCMonitoring)
                {
                    AppendTrace(rxRead, time);
                    Program.FormEMCView?.HandleResponse(service);
                    return;
                }

                if (service?.ServiceInfo == ServiceInfo.ReadDataByIdentifier)
                {
                    HandleGeneralMessages(service);
                    foreach (var receiver in FormMain.Receivers.OfType<IReadDataByIdenReceiver>())
                        if (receiver.Receive(service)) continue;
                }
                else if (service?.ServiceInfo == ServiceInfo.InputOutputControlByIdentifier)
                {
                    foreach (var receiver in FormMain.Receivers.OfType<IIOControlByIdenReceiver>())
                        if (receiver.Receive(service)) continue;
                }
                else if (service?.ServiceInfo == ServiceInfo.WriteDataByIdentifier)
                {
                    foreach (var receiver in FormMain.Receivers.OfType<IWriteByIdenReceiver>())
                        if (receiver.Receive(service)) continue;
                }
                else if (service?.ServiceInfo == ServiceInfo.ReadDTCInformation
                        || service?.ServiceInfo == ServiceInfo.ClearDTCInformation)
                {
                    foreach (var receiver in FormMain.Receivers.OfType<IDTCReceiver>())
                        if (receiver.Receive(service)) continue;
                }
                if (service?.ServiceInfo == ServiceInfo.DiagnosticSessionControl)
                {
                    if (!FormMain.ControlChecker)
                    {
                        FormMain formMain = (FormMain)Application.OpenForms[Constants.Form_Main];
                        if (formMain.dockMonitor.ActiveDocument is IPeriodicTest formInput)
                            formInput.SessionFiltering();
                    }
                }

                //var data = Enumerable.Range(0, byteHexText.Length / 2).Select(x => Convert.ToByte(byteHexText.Substring(x * 2, 2), 16)).ToArray();

                ////HandleSWVersion(bytes);

                if (!Settings.Default.FilterData.Contains(e.Data[0].ToString("X")))
                {
                    if (!string.IsNullOrEmpty(service.Response.NegativeResponseCode))
                        AppendTrace($"{rxRead} ({service.Response.NegativeResponseCode})", time);
                    else
                        AppendTrace($"{rxRead}", time);
                    //AppendTraceRx($"{rxRead} ({service.Response.NegativeResponseCode})", time);
                }
                address = 0;
                session = 0x00;

            }
        }

        public void SendServiceDataToControlChecker(Service service)
        {
            if (!(service is ReadDataByIdenService || service is IOControlByIdentifierService))
                return;
            FormControlChecker formChecker = Application.OpenForms[Constants.Form_Control_Checker] as FormControlChecker;
            if (formChecker != null)
            {
                formChecker.LogDataToDGVFromService(service);
            }
        }
        public void SendByteDataToControlChecker(byte[] data, ushort address)
        {
            FormControlChecker formChecker = Application.OpenForms[Constants.Form_Control_Checker] as FormControlChecker;
            if (formChecker != null)
            {
                formChecker.LogDataToDGVFromBytes(data, address);
            }
        }

        /// <summary>
        /// Checks whether the connection has been established or not.
        /// </summary>
        /// <returns>true if the connection is established and in an open state; otherwise, false.</returns>
        public static bool CheckConnection()
        {
            if (ConnectionUtil.hardware == null)
            {
                Helper.ShowWarningMessageBox("No device connected!");
                return false;
            }
            FormMain formMain = (FormMain)Application.OpenForms[Constants.Form_Main];
            if (formMain.openConnection.Text == ("Stop Connection"))
                return true;
            else
            {
                Helper.ShowWarningMessageBox("There is a problem about connection!");
                return false;
            }
        }

        /// <summary>
        /// Checks whether a session has been selected or not.
        /// </summary>
        /// <returns>true if a session is selected; otherwise, false.</returns>
        public static bool CheckSession()
        {
            if (ASContext.CurrentSession == null)
            {
                Helper.ShowWarningMessageBox("No session selected!");
                return false;
            }
            else
                return true;
        }

        /// <summary>
        /// Transmits an array of bytes to a specific device.
        /// </summary>
        /// <param name="canId">The id of the message.</param>
        /// <param name="dataBytes">A byte array represents the data of the message.</param>
        public static void TransmitData(byte[] dataBytes)
        {
            if (Thread.CurrentThread != Program.UIThread)
                TransmitDataInternal(dataBytes);
            else
                Task.Run(() => TransmitDataInternal(dataBytes));
        }

        public static void TransmitData(uint canId, byte[] dataBytes)
        {
            if (Thread.CurrentThread != Program.UIThread)
                TransmitDataInternal(dataBytes, canId);
            else
                Task.Run(() => TransmitDataInternal(dataBytes, canId));
        }

        private static void TransmitDataInternal(byte[] dataBytes, uint? canId = null)
        {
            FormMain formMain = (FormMain)Application.OpenForms[Constants.Form_Main];
            lock (lockObj)
            {
                try
                {
                    if (canId != null)
                        transportProtocol.Config.PhysicalAddr.TxId = (uint)canId;
                    else
                        transportProtocol.Config.PhysicalAddr.TxId = Convert.ToUInt32(Settings.Default.TransmitAdress, 16);
                    if (Settings.Default.DebugLogging)
                        formMain.AppendTrace($"Message Sent: {BitConverter.ToString(dataBytes)}");
                    transportProtocol.SendBytes(dataBytes, channelId);
                }
                catch (Exception ex)
                {
                    formMain.AppendTrace(ex.ToString(), Color.Red);
                }
            }
        }

        /// <summary>
        /// Disconnects the current connection and frees the resources.
        /// </summary>
        public void Disconnect()
        {
            try
            {
                transportProtocol?.Hardware?.Disconnect();
                //hardware?.Disconnect();

                if (transportProtocol != null)
                    transportProtocol.Hardware = null;

                FormMain formMain = (FormMain)Application.OpenForms[Constants.Form_Main];
                if (formMain.InvokeRequired)
                    formMain.Invoke(new Action(() => UpdateStartButton(formMain)));
                else
                    UpdateStartButton(formMain);
            }
            catch (Exception ex)
            {
                AppendTrace(ex.ToString(), DateTime.Now, Color.Red);
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Event handler for the SerialHardware's FrameWritten event.
        /// This event is triggered when a frame is successfully written to the serial port.
        /// </summary>
        /// <param name="sender">A reference to the Hardware instance.</param>
        /// <param name="e">A reference to the FrameWritten event's arguments.</param>
        private void SerialHardware_FrameWritten(object sender, SerialPortEventArgs e)
        {
            ((FormMain)Application.OpenForms[Constants.Form_Main]).SetCounter(1, 0);
            //((FormMain)Application.OpenForms[Constants.Form_Main]).UpdateInputMonitorControls(e.Data, MessageDirection.TX);
            //((FormMain)Application.OpenForms[Constants.Form_Main]).UpdateOutputMonitorControls(e.Data, MessageDirection.TX);
        }

        /// <summary>
        /// Event handler for the SerialHardware's FrameRead event.
        /// This event is triggered when data is read from the serial port.
        /// </summary>
        /// <param name="sender">A reference to the Hardware instance.</param>
        /// <param name="e">A reference to the FrameRead event's arguments.</param>
        private void SerialHardware_FrameRead(object sender, SerialPortEventArgs e)
        {
            if (e.Data.Length % 24 != 0)
                return;

            for (int i = 0; i < e.Data.Length; i += 24)
            {
                var dataArr = e.Data.ToList().Skip(i).Take(24).ToArray();
                var asciiHex = System.Text.Encoding.ASCII.GetString(dataArr).Trim();
                var byteHexText = $"{asciiHex.Substring(0, 4)}{asciiHex.Substring(6)}";

                FormMain formMain = (FormMain)Application.OpenForms[Constants.Form_Main];
                formMain.SetCounter(0, 1);

                if (hardware is SerialPortHardware hwSp && hwSp.SerialPortType == SerialPortType.UT146)
                {
                    var rxRead = "Rx " + uint.Parse(asciiHex.Substring(0, 4), NumberStyles.HexNumber).ToString("X") + " " + asciiHex.Substring(6);
                    var time = DateTime.Now;

                    var data = Enumerable.Range(0, byteHexText.Length / 2).Select(x => Convert.ToByte(byteHexText.Substring(x * 2, 2), 16)).ToArray();

                    //HandleSWVersion(data);

                    AppendTrace(rxRead, time);
                    AppendTraceRx(rxRead, time);
                }
            }
        }

        /// <summary>
        /// Event handler for the SerialHardware's ErrorAccured event.
        /// This event is triggered when an error occurs in the serial port communication.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments containing error information.</param>
        private void SerialHardware_ErrorAccured(object sender, SerialPortErrorEventArgs e)
        {
            if (FormMain.IsTestRunning)
            {
                Helper.SendExtendedDiagSession();
            }
            var time = new DateTime((long)e.Timestamp);
            AppendTrace(e.Message, time, Color.Red);
            if (e.ErrorType == SerialHardware_ErrorType.Disposed)
                Disconnect();
        }

        /// <summary>
        /// Selects a device from a collection of connected devices.
        /// </summary>
        /// <param name="list">A list of currently connected devices.</param>
        /// <returns>A reference to the selected device.</returns>
        private IHardware SelectHardware(List<IHardware> list)
        {
            using (var form = new FormHardwareList(list))
                if (form.ShowDialog() == DialogResult.OK)
                    return (IHardware)list[form.SelectedIndex];
            return null;
        }

        /// <summary>
        /// Checks the received message, increases the counter if it already exists, or adds it as a new row.
        /// </summary>
        /// <param name="text">A string represents the received message.</param>
        /// <param name="time">The timestamp when the message was received.</param>
        /// <param name="color">Specifies the foreground color of the output.</param>
        private void AppendTraceRx(string text, DateTime time, Color? color = null)
        {
            FormMain formMain = (FormMain)Application.OpenForms[Constants.Form_Main];
        }

        /// <summary>
        /// Writes a message with a timestamp to the console; Optionally, specifies the color.
        /// </summary>
        /// <param name="text">The message to be written to the console.</param>
        /// <param name="time">The timestamp of the message.</param>
        /// <param name="color">Specifies the foreground color of the output.</param>
        private void AppendTrace(string text, DateTime time, Color? color = null)
        {
            ((FormMain)Application.OpenForms[Constants.Form_Main]).AppendTrace(text,
                text.Contains("7F") ? Color.Red : color ?? Color.Green);
        }

        /// <summary>
        /// Scans all connected devices and returns a reference to them.
        /// </summary>
        /// <returns>A list of all connected devices.</returns>
        private List<IHardware> CreateHardwareList()
        {
            var hardwareList = HardwareHelper.ScanDevices(HardwareHelper.DeviceType.Can | HardwareHelper.DeviceType.SerialPort);
            SetDefaultSettings(hardwareList);
            if (hardwareList.Count == 0)
                Console.WriteLine("No device found");
            return hardwareList;
        }

        /// <summary>
        /// Sets default settings based on the device type.
        /// </summary>
        /// <param name="hardwareList">A list of all connected devices.</param>
        private void SetDefaultSettings(List<IHardware> hardwareList)
        {
            foreach (var hardware in hardwareList)
            {
                if (hardware is SerialPortHardware serialHardware)
                {
                    serialHardware.Port = serialHardware.Name;
                    serialHardware.SerialPortType = Settings.Default.SerialPortType;
                    serialHardware.BaudRate = Settings.Default.SerialBaudRate;
                    serialHardware.DataBits = Settings.Default.SerialDataBits;
                    serialHardware.Parity = (Parity)Settings.Default.SerialParity;
                    serialHardware.StopBits = (StopBits)Settings.Default.SerialStopBits;
                    serialHardware.ReadTimeout = Settings.Default.SerialReadTimeout;
                    serialHardware.WriteTimeout = Settings.Default.SerialWriteTimeout;
                }
                else if (hardware is IntrepidCsCan intrepidCsCanHardware)
                {
                    if (Settings.Default.IntrepidDevice == null)
                        Settings.Default.IntrepidDevice = new IntrepidCsCan();
                    intrepidCsCanHardware.BitRate = Settings.Default.IntrepidDevice.BitRate;
                    intrepidCsCanHardware.NetworkID = Settings.Default.IntrepidDevice.NetworkID;
                }
                else if (hardware is KvaserCan kvaserCanHardware)
                {
                    if (Settings.Default.KvaserDevice == null)
                        Settings.Default.KvaserDevice = new KvaserCan();
                    else
                        kvaserCanHardware.BitRate = Settings.Default.KvaserDevice.BitRate;
                }
            }
        }

        /// <summary>
        /// Establishes a connection with the specified device.
        /// </summary>
        /// <param name="hardware">The device to which a connection is being established.</param>
        private void InitHardware(IHardware hardware)
        {
            hardware.Connect(false);
        }

        /// <summary>
        /// Update the UI elements related to the connection start button and connection status.
        /// </summary>
        /// <param name="formMain">The main form instance where the UI elements are located.</param>
        private void UpdateStartButton(FormMain formMain)
        {
            formMain.lblConnection.Text = "Offline";
            formMain.lblConnection.ForeColor = Color.Red;
            formMain.openConnection.Text = "Start Connection";
            formMain.openConnection.Image = formMain.imageList1.Images[2];
            ConnectionLogger = "Connection is closed";
            formMain.AppendTrace(ConnectionLogger, Color.Red);
        }

        /// <summary>
        /// checks received message
        /// </summary>
        /// <param name="data">A byte array represents the data of the received message.</param>
        private void HandleGeneralMessages(Service service)
        {
            if (service is ReadDataByIdenService readService)
            {
                if (readService.ControlInfo.Name == "Vestel_Internal_Software_Version")
                {
                    var embeddedSwVersion = readService.Payloads.First().Value;
                    FormMain formMain = (FormMain)Application.OpenForms[Constants.Form_Main];
                    if (formMain.InvokeRequired)
                        formMain.Invoke(new MethodInvoker(() => formMain.SetEmbeddedSoftwareVersion(embeddedSwVersion)));
                    else
                        formMain.SetEmbeddedSoftwareVersion(embeddedSwVersion);

                }
            }
        }

        #endregion
    }
}
