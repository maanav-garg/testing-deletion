using Connection.Hardware;
using Connection.Hardware.SP;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO.Ports;
using System.Threading;
using System.Threading.Tasks;
using static Connection.Hardware.HardwareHelper;

namespace DiagBoxUnitTest
{
    /// <summary>
    /// Represents a set of unit tests for SerialPort devices.
    /// </summary>
    //[TestClass]
    public class TestsSerial
    {
        /// <summary>
        /// Represents an instance of the SerialPortHardware for communication.
        /// </summary>
        private static SerialPortHardware hardware;

        /// <summary>
        /// TaskCompletionSource for handling asynchronous events.
        /// </summary>
        private static TaskCompletionSource<SerialPortEventArgs> tcs;

        /// <summary>
        /// Initializes the test class with necessary setup.
        /// </summary>
        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            hardware = HardwareHelper.ScanDevices(DeviceType.SerialPort)[0] as SerialPortHardware;
            hardware.FrameRead += Hardware_FrameRead;
            hardware.Connect();
            Initialize(hardware);

            hardware.Transmit(0x7E0, new byte[] { 0x02, 0x10, 0x61, 0x00, 0x00, 0x00, 0x00, 0x00 }); Thread.Sleep(1000); // Manufacturing Session 
            hardware.Transmit(0x7E0, new byte[] { 0x04, 0x27, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00 }); Thread.Sleep(1000); // Seed Request
            hardware.Transmit(0x7E0, new byte[] { 0x04, 0x27, 0x02, 0x00, 0x00, 0x00, 0x00, 0x00 }); Thread.Sleep(1000); // Send Key

            tcs = new TaskCompletionSource<SerialPortEventArgs>();
        }

        /// <summary>
        /// Test method for executing unit tests.
        /// </summary>
        [TestMethod]
        [DynamicData(nameof(TestData.Cases), dynamicDataDeclaringType: typeof(TestData))]
        public async Task Test(string name, byte[] request, byte[] response)
        {
            tcs = new TaskCompletionSource<SerialPortEventArgs>();
            hardware.Transmit(0x07E0, request);

            if (await Task.WhenAny(tcs.Task, Task.Delay(300)) == tcs.Task)
            {
                string result = BitConverter.ToString((await tcs.Task).Data).Substring(0, 14);
                Assert.AreEqual(BitConverter.ToString(response), result, $"{name} Test Failed");
            }
            else
            {
                Assert.Fail($"{name} Data reception did not occur within the specified time frame.");
            }
        }

        /// <summary>
        /// Cleans up the test class after all tests have been executed.
        /// </summary>
        [ClassCleanup]
        public static void TearDown()
        {
            hardware.FrameRead -= Hardware_FrameRead;
            hardware.Transmit(0x07E0, new byte[] { 0x02, 0x11, 0x03, 0x00, 0x00, 0x00, 0x00, 0x00 }); // ECU Reset
            Thread.Sleep(10);
            hardware.Disconnect();
        }

        /// <summary>
        /// Initializes the Serial Port hardware settings.
        /// </summary>
        /// <param name="hardware">The SerialPortHardware instance to be initialized.</param>
        private static void Initialize(SerialPortHardware hardware)
        {
            hardware.BaudRate = 115200;
            hardware.Parity = Parity.None;
            hardware.DataBits = 8;
            hardware.StopBits = StopBits.One;
            // hardware.Port = currentPort;
            hardware.ReadTimeout = 1000;
            hardware.WriteTimeout = 1000;

            hardware.Transmit("FFFFFFFFFE06");
            hardware.Transmit("FFFFFFFFFB01");
        }

        /// <summary>
        /// Event handler for Hardware_FrameRead. Sets the TaskCompletionSource with the received data.
        /// </summary>
        /// <param name="sender">A reference to the Hardware instance.</param>
        /// <param name="e">A reference to the arguments of the FrameRead event.</param>
        private static void Hardware_FrameRead(object sender, SerialPortEventArgs e)
        {
            tcs?.SetResult(e);
        }
    }
}
