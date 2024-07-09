using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO.Ports;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Connection.Hardware;
using Connection.Hardware.Can;
using Connection.Hardware.SP;
using AutosarBCM.Common;
using AutosarBCM.Properties;
using AutosarBCM.Core.Config;
using AutosarBCM.Core;
using Connection.Protocol.Uds;
using AutosarBCM.Config;
using System.Collections.Specialized;
using System.Runtime.InteropServices;

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
                    //canHardware.FrameRead += Hardware_FrameRead;
                    //canHardware.FrameWritten += Hardware_FrameWritten;
                    //canHardware.CanError += Hardware_CanError;

                    if (canHardware is IntrepidCsCan && (canHardware.BitRate > 0))
                    {
                        canHardware.SetBitRate((int)canHardware.BitRate, (int)canHardware.NetworkID);
                    }
                    else if (canHardware is IntrepidCsCan && canHardware.NetworkID != null)
                    {
                        canHardware.NetworkID = canHardware.NetworkID;
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
                    serialHardware.FrameRead += SerialHardware_FrameRead;
                    serialHardware.FrameWritten += SerialHardware_FrameWritten;
                    serialHardware.ErrorAccured += SerialHardware_ErrorAccured;
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

        private void TransportProtocol_ReceiveError(object sender, Connection.Protocol.TransportErrorEventArs e)
        {
            Helper.ShowErrorMessageBox(e.Message);
            //if (e. == CanHardware_ErrorStatus.Disconnect)
            //    Disconnect();
        }
        private void TransportProtocol_MessageSent(object sender, Connection.Protocol.TransportEventArgs e)
        {

            // Tester present
            if (e.Data[0] == ServiceInfo.TesterPresent.RequestID)
                return;

            // Handle transmitted data -TX-
            if (e.Data[0] == ServiceInfo.ReadDataByIdentifier.RequestID
                || e.Data[0] == ServiceInfo.InputOutputControlByIdentifier.RequestID)
            {
                foreach (var receiver in FormMain.Receivers)
                    if (receiver.Sent(BitConverter.ToInt16(e.Data.Skip(1).Take(2).Reverse().ToArray(), 0))) ;
            }


            var txId = transportProtocol.Config.PhysicalAddr.RxId.ToString("X");
            var txRead = $"Tx {txId} {BitConverter.ToString(e.Data)}";
            var time = new DateTime((long)e.Timestamp);

            if (!Settings.Default.FilterData.Contains(e.Data[0].ToString("X")))
                AppendTrace(txRead, time, Color.Black);
        }

        private void TransportProtocol_MessageReceived(object sender, Connection.Protocol.TransportEventArgs e)
        {
            var service = new ASResponse(e.Data).Parse();

            var rxId = transportProtocol.Config.PhysicalAddr.RxId.ToString("X");

            var rxRead = $"Rx {rxId} {BitConverter.ToString(e.Data)}";
            var time = new DateTime((long)e.Timestamp);

            if (service?.ServiceInfo == ServiceInfo.TesterPresent)
                return;

            if (service?.ServiceInfo == ServiceInfo.NegativeResponse)
            {
                if (e.Data[1] == (byte)SIDDescription.SID_DIAGNOSTIC_SESSION_CONTROL)
                {
                    FormMain formMain = (FormMain)Application.OpenForms[Constants.Form_Main];
                    if (formMain.dockMonitor.ActiveDocument is IPeriodicTest formInput)
                        formInput.DisabledAllSession();
                }

                AppendTrace($"{rxRead} ({service.Response.NegativeResponseCode})", time);
                return;
            }

            if (FormMain.ControlChecker)
            {
                AppendTrace(rxRead, time);
                SendDataToControlChecker(service);
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
                foreach (var receiver in FormMain.Receivers.OfType<IReadDataByIdenReceiver>())
                    if (receiver.Receive(service)) continue;
            }
            else if (service?.ServiceInfo == ServiceInfo.InputOutputControlByIdentifier)
            {
                foreach (var receiver in FormMain.Receivers.OfType<IIOControlByIdenReceiver>())
                    if (receiver.Receive(service)) continue ;
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

            ////HandleGeneralMessages(bytes);

            if (!Settings.Default.FilterData.Contains(e.Data[0].ToString("X")))
            {
                AppendTrace(rxRead, time);
                AppendTraceRx(rxRead, time);
            }


        }
        public void SendDataToControlChecker(Service service)
        {
            if (!(service is ReadDataByIdenService || service is IOControlByIdentifierService))
                return;
            FormControlChecker formChecker = Application.OpenForms[Constants.Form_Control_Checker] as FormControlChecker;
            if (formChecker != null)
            {
                formChecker.LogDataToDGV(service);
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
            MessageBox.Show("HW Layer not used");
        }

        private static void TransmitDataInternal(byte[] dataBytes)
        {
            FormMain formMain = (FormMain)Application.OpenForms[Constants.Form_Main];
            lock (lockObj)
            {
                try
                {
                    transportProtocol.SendBytes(dataBytes);
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

                    //if (FormMain.EMCMonitoring)
                    //{
                    //    AppendTrace(rxRead, time);
                    //    Program.FormEMCView?.HandleResponse(data);
                    //    return;
                    //}

                    //if (FormMain.ControlChecker)
                    //{
                    //    AppendTrace(rxRead, time);
                    //    Program.FormControlChecker.HandleResponse(data);
                    //    continue;
                    //}
                    //if (formMain.UpdateOutputMonitorControls(data, MessageDirection.RX))
                    //{
                    //    AppendTrace(rxRead, time);
                    //    continue;
                    //}
                    //if (formMain.UpdateInputMonitorControls(data, MessageDirection.RX))
                    //{
                    //    if (!FormMain.IsTestRunning) AppendTrace(rxRead, time);
                    //    return;
                    //}

                    HandleGeneralMessages(data);

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
            Helper.ShowErrorMessageBox(e.Message);
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

            //if (formReceive.dgvReceives.InvokeRequired)
            //{
            //    formReceive.dgvReceives.Invoke(new Action(delegate ()
            //    {
            //        var receivePattern = @"(?:Tx|Rx)\s?(\w+)\s?(.*)";
            //        RegexOptions options = RegexOptions.Singleline;
            //        var matchedItem = Regex.Matches(text, receivePattern, options)[0];
            //        var messageID = matchedItem.Groups[1].Value.ToString();
            //        var dataBytes = matchedItem.Groups[2].Value.ToString().Replace("-", " ");
            //        if (!dataBytes.Contains(" "))
            //        {
            //            dataBytes = String.Join(" ", Enumerable.Range(0, dataBytes.Length / 2).Select(x => dataBytes.Substring(x * 2, 2)));
            //        }

            //        var messageCheck = formReceive.dgvReceives.Rows.Cast<DataGridViewRow>().FirstOrDefault(r => r.Cells[0].Value.ToString() == messageID && r.Cells[3].Value.ToString() == dataBytes);

            //        if (messageCheck != null && messageID != "0")
            //        {
            //            var rowIndex = messageCheck.Index;
            //            ((CanMessage)messageCheck.DataBoundItem).SetData(dataBytes);
            //            ((CanMessage)messageCheck.DataBoundItem).IncreaseCounter();
            //            ((CanMessage)messageCheck.DataBoundItem).SetResponse();
            //            formReceive.ResetAndApplyFilterWithSelectionRestore();
            //            formReceive.dgvReceives.Rows[rowIndex].DefaultCellStyle.ForeColor = formReceive.dgvReceives.Rows[rowIndex].DefaultCellStyle.SelectionForeColor = dataBytes.Contains("7F") ? Color.Red : Color.Green;
            //        }
            //        else if (!messageID.Equals("0"))
            //        {
            //            formReceive.AddMessage(new CanMessage(messageID, dataBytes) { Count = 1, Timestamp = time });
            //            var rowIndex = formReceive.dgvReceives.Rows.Count - 1;
            //            ((CanMessage)formReceive.dgvReceives.Rows[rowIndex].DataBoundItem).SetResponse();
            //            formReceive.dgvReceives.Rows[rowIndex].DefaultCellStyle.ForeColor = formReceive.dgvReceives.Rows[rowIndex].DefaultCellStyle.SelectionForeColor = dataBytes.Contains("7F") ? Color.Red : Color.Green;
            //        }
            //    }));
            //}
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
        /// An event handler to the Hardware's FrameWritten event.
        /// Writes the message id and data to the console.
        /// </summary>
        /// <param name="sender">A reference to the Hardware instance.</param>
        /// <param name="e">A reference to the FrameWritten event's arguments.</param>
        private void Hardware_FrameWritten(object sender, CanFrameEventArgs e)
        {
            ((FormMain)Application.OpenForms[Constants.Form_Main]).SetCounter(1, 0);
            ((FormMain)Application.OpenForms[Constants.Form_Main]).UpdateInputMonitorControls(e.Data, MessageDirection.TX);
            ((FormMain)Application.OpenForms[Constants.Form_Main]).UpdateOutputMonitorControls(e.Data, MessageDirection.TX);

            //if (FormMain.IsTestRunning && FormMain.TestClickCounter == 0)
            //    return;

            string txWritten = "Tx " + e.CanId.ToString("X") + " " + BitConverter.ToString(e.Data);
            var time = new DateTime(e.Timestamp);
            AppendTrace(txWritten, time, Color.Black);
        }

        /// <summary>
        /// Event handler for handling CAN bus errors.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments containing error information.</param>
        private void Hardware_CanError(object sender, CanErrorEventArgs e)
        {
            Helper.ShowErrorMessageBox(e.ErrorMessage);
            if (e.Status == CanHardware_ErrorStatus.Disconnect)
                Disconnect();
        }

        /// <summary>
        /// An event handler to the Hardware's FrameRead event.
        /// Writes the message id and data to the console.
        /// </summary>
        /// <param name="sender">A reference to the Hardware instance.</param>
        /// <param name="e">A reference to the FrameRead event's arguments.</param>
        internal void Hardware_FrameRead(object sender, CanFrameEventArgs e)
        {
            ////if (e.Data.Length % 8 != 0)
            ////    return;

            //// TODO: Refactor Data Check
            //// When connecting to the NeoVI Fire3 Device, it causes an error because we get long responses.
            //if (e.Data.Length > 8)
            //    return;

            //FormMain formMain = (FormMain)Application.OpenForms[Constants.Form_Main];

            //// TODO: Refactor Data Check
            //// Replace hardcoded byte value checks with a more flexible method, such as a configuration object or dictionary.
            //// DoorCap signals that we only receive Rx are not included in the success average.
            //if (e.Data[0] != 0x03 || e.Data[1] != 0xef || e.Data[2] != 0x05)
            //    formMain.SetCounter(0, 1);
            //var bytes = new byte[10];
            //var messageId = Helper.StringToByteArray(e.CanId.ToString("X4"));

            ////If the read data isn't structured
            //if (e.Data.Length < 8)
            //    Array.Copy(new byte[2] {0 , 0}, bytes, 2);
            //else
            //    Array.Copy(messageId, bytes, messageId.Length);

            //Array.Copy(e.Data, 0, bytes, 2, e.Data.Length);

            var rxRead = "Rx " + e.CanId.ToString("X") + " " + BitConverter.ToString(e.Data);
            var time = new DateTime(e.Timestamp);

            ////if (FormMain.ControlChecker)
            ////{
            ////    AppendTrace(rxRead, time);
            ////    Program.FormControlChecker?.HandleResponse(bytes);
            ////    return;
            ////}

            ////if (formMain.UpdateOutputMonitorControls(bytes, MessageDirection.RX))
            ////{
            ////    AppendTrace(rxRead, time);
            ////    return;
            ////}
            ////if (formMain.UpdateInputMonitorControls(bytes, MessageDirection.RX))
            ////{
            ////    if (!FormMain.IsTestRunning) AppendTrace(rxRead, time);
            ////    return;
            ////}

            ////HandleGeneralMessages(bytes);

            AppendTrace(rxRead, time);
            AppendTraceRx(rxRead, time);
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
            hardware.Connect();
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
        private void HandleGeneralMessages(byte[] data)
        {
            if (data[2] == 0x06 && data[3] == 0x6F && data[4] == 0x61 && data[5] == 0x99)
            {
                var EmbSwVersion = data.Skip(6).Take(4).ToArray();
                FormMain formMain = (FormMain)Application.OpenForms[Constants.Form_Main];
                if (formMain.InvokeRequired)
                    formMain.Invoke(new MethodInvoker(() => formMain.SetEmbeddedSoftwareVersion(EmbSwVersion)));
                else
                    formMain.SetEmbeddedSoftwareVersion(EmbSwVersion);

            }
        }

        #endregion
    }
}
