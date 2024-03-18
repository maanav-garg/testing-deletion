using Connection.Hardware;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;
using System.Threading.Tasks;
using static Connection.Hardware.HardwareHelper;

namespace DiagBoxUnitTest
{
    /// <summary>
    /// Represents a set of unit tests for CAN devices.
    /// </summary>
    [TestClass]
    public class TestsCan
    {
        private static CanHardware hardware;
        private static TaskCompletionSource<CanFrameEventArgs> tcs;

        /// <summary>
        /// Initializes the test class by setting up the required hardware and establishing a connection.
        /// </summary>
        /// <param name="context"></param>
        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            hardware = HardwareHelper.ScanDevices(DeviceType.Can)[0] as CanHardware;
            hardware.FrameRead += Hardware_FrameRead;
            hardware.Connect();

            hardware.Transmit(0x7E0, new byte[] { 0x02, 0x10, 0x61, 0x00, 0x00, 0x00, 0x00, 0x00 }); Thread.Sleep(1000); // Manufacturing Session 
            hardware.Transmit(0x7E0, new byte[] { 0x04, 0x27, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00 }); Thread.Sleep(1000); // Seed Request
            hardware.Transmit(0x7E0, new byte[] { 0x04, 0x27, 0x02, 0x00, 0x00, 0x00, 0x00, 0x00 }); Thread.Sleep(1000); // Send Key

            tcs = new TaskCompletionSource<CanFrameEventArgs>();
        }

        /// <summary>
        /// Executes a test for a specific service and validates the response.
        /// </summary>
        /// <param name="name">The name of the test.</param>
        /// <param name="request">The request message for the test.</param>
        /// <param name="response">The expected response message for the test.</param>
        [TestMethod]
        [DynamicData(nameof(TestData.Cases), dynamicDataDeclaringType: typeof(TestData), dynamicDataSourceType: DynamicDataSourceType.Property)]
        public async Task Test(string name, byte[] request, byte[] response)
        {
            tcs = new TaskCompletionSource<CanFrameEventArgs>();
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
        /// Cleans up the test class by transmitting the ECUReset message and disconnecting.
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
        /// Event handler for Hardware_FrameRead. Sets the TaskCompletionSource with the received data.
        /// </summary>
        /// <param name="sender">A reference to the Hardware instance.</param>
        /// <param name="e">A reference to the arguments of the FrameRead event.</param>
        private static void Hardware_FrameRead(object sender, CanFrameEventArgs e)
        {
            tcs?.SetResult(e);
        }
    }
}
