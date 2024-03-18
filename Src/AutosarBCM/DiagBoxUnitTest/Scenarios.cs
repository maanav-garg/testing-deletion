using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Connection.Hardware;
using Connection.Hardware.SP;
using System.IO.Ports;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Text;
using System.Globalization;

namespace DiagBoxUnitTest
{
    [TestClass]
    public class Scenarios
    {
        #region Variables

        private CanHardware hardware;
        private TaskCompletionSource<CanFrameEventArgs> tcs;
        private static uint arbId = 0x07E0;
        private static string responseArbId = "07E808";

        #endregion

        #region Configuration

        [TestInitialize]
        public void Setup()
        {
            hardware = CreateHardware();
            InitHardware(hardware);

            hardware.FrameRead += Hardware_FrameRead;
            tcs = new TaskCompletionSource<CanFrameEventArgs>();
        }

        [TestCleanup]
        public void TearDown()
        {
            hardware.FrameRead -= Hardware_FrameRead;
            SetupSecurityAccess();
            hardware.Transmit(arbId, GetBytes("02-11-03-00-00-00-00-00")); // ECU Reset
            Thread.Sleep(100);
            hardware.Disconnect();
        }
        public void SetupManufacturingSession()
        {
            hardware.FrameRead -= Hardware_FrameRead;
            hardware.Transmit(arbId, GetBytes("02-10-61-00-00-00-00-00")); // Manufacturing Session
            Thread.Sleep(100);
            hardware.FrameRead += Hardware_FrameRead;
        }
        public void SetupSecurityAccess()
        {
            hardware.FrameRead -= Hardware_FrameRead;
            hardware.Transmit(arbId, GetBytes("04-27-01-00-00-00-00-00")); // Seed Request
            Thread.Sleep(100);
            hardware.Transmit(arbId, GetBytes("04-27-02-00-00-00-00-00")); // Send Key
            Thread.Sleep(100);
            hardware.FrameRead += Hardware_FrameRead;
        }

        #endregion

        #region Basic Functions
        private static CanHardware CreateHardware()
        {
            var hardwareList = HardwareHelper.ScanDevices(HardwareHelper.DeviceType.Can);
            if (hardwareList.Count == 0)
                Console.WriteLine("No device found");
            return (CanHardware)hardwareList.FirstOrDefault();

        }
        private static void InitHardware(CanHardware hardware)
        {
            hardware.Connect();
        }
        private void Hardware_FrameRead(object sender, CanFrameEventArgs e)
        {
            tcs.SetResult(e);
        }
        static string HexToString(string hexString)
        {
            return hexString.Replace("-", "");
        }

        static byte[] GetBytes(string value)
        {
            return value.Split('-').Select(x => byte.Parse(x, NumberStyles.HexNumber)).ToArray();
        }
        #endregion

        #region Basic Scenarios

        #region WriteDataByIdentifier

        /*
            After transmitting the UDS_TEST_PARAM data : 05 2E 12 34 00 07 00 00 with the WriteDataByIdentifier
            When we read with ReadDataByIdentifier we get the result : 05 62 12 34 00 07 00 00
        */

        [TestMethod]
        public async Task TestWriteDataByIdentifier()
        {
            SetupManufacturingSession();
            SetupSecurityAccess();

            hardware.FrameRead -= Hardware_FrameRead;

            hardware.Transmit(arbId, GetBytes("05-2E-12-34-00-07-00-00")); // Write to UDS_TEST_PARAM
            Thread.Sleep(100);

            hardware.FrameRead += Hardware_FrameRead;

            hardware.Transmit(arbId, GetBytes("03-22-12-34-00-00-00-00")); // ReadDataByIdentifier

            if (await Task.WhenAny(tcs.Task, Task.Delay(10000)) == tcs.Task)
            {
                var frameEventArgs = await tcs.Task;

                var responsePrefix = responseArbId + "0562123400070000";

                var result = responseArbId + HexToString(BitConverter.ToString(frameEventArgs.Data));

                Assert.AreEqual(responsePrefix, result, "A negative response was received");
            }
            else
            {
                Assert.Fail("Data reception did not occur within the specified time frame.");
            }
        }
        #endregion

        #region InputOutputControllerByIdentifier

        /*
            DID:0x1234 for IOCBI command, write 0x6161 to  UDS_TEST_PARAM.
            You can check it with RDBI command after set IOCBI.
        */

        [TestMethod]
        public async Task TestInputOutputControllerByIdentifier()
        {
            SetupManufacturingSession();
            SetupSecurityAccess();

            hardware.FrameRead -= Hardware_FrameRead;
            hardware.Transmit(arbId, GetBytes("03-2F-12-34-00-00-00-00")); // InputOutputControllerByIdentifier
            Thread.Sleep(100);
            hardware.FrameRead += Hardware_FrameRead;

            hardware.Transmit(arbId, GetBytes("03-22-12-34-00-00-00-00"));

            if (await Task.WhenAny(tcs.Task, Task.Delay(10000)) == tcs.Task)
            {
                var frameEventArgs = await tcs.Task;

                var responsePrefix = responseArbId + "0562123461610000";

                var result = responseArbId + HexToString(BitConverter.ToString(frameEventArgs.Data));

                Assert.AreEqual(responsePrefix, result, "A negative response was received");
            }
            else
            {
                Assert.Fail("Data reception did not occur within the specified time frame.");
            }
        }

        #endregion

        #endregion

        #region Negative Response Control

        #region Security Access

        /*
            First Seed Request(0x01) and than Send Key(0x02)
            If sequence is not correct, you will get NRC 0x24
            For the NRC, we first transmit the Send Key(0x02).
        */

        [TestMethod]
        public async Task NegativeResponseControlSecurityAccess()
        {
            hardware.Transmit(arbId, GetBytes("04-27-02-00-00-00-00-00"));

            if (await Task.WhenAny(tcs.Task, Task.Delay(10000)) == tcs.Task)
            {
                var frameEventArgs = await tcs.Task;

                var responsePrefix = responseArbId + "037F272400000000";

                var result = responseArbId + HexToString(BitConverter.ToString(frameEventArgs.Data));

                Assert.AreEqual(responsePrefix, result, "A negative response was not received");
            }
            else
            {
                Assert.Fail("Data reception did not occur within the specified time frame.");
            }
        }
        #endregion

        #region Out of Session

        /*
            If session is not applicable you will get NRC=0x7F
        */

        [TestMethod]
        public async Task NegativeResponseControlOutOfSession()
        {
            hardware.Transmit(arbId, GetBytes("03-22-12-34-00-00-00-00"));

            if (await Task.WhenAny(tcs.Task, Task.Delay(10000)) == tcs.Task)
            {
                var frameEventArgs = await tcs.Task;

                var responsePrefix = responseArbId + "037F227F00000000";

                var result = responseArbId + HexToString(BitConverter.ToString(frameEventArgs.Data));

                Assert.AreEqual(responsePrefix, result, "A negative response was not received");
            }
            else
            {
                Assert.Fail("Data reception did not occur within the specified time frame.");
            }
        }
        #endregion

        #region Invalid Security Access

        /*
            If you do not have security acces level ok, you will get NRC=0x33 
        */

        [TestMethod]
        public async Task NegativeResponseControlInvalidSecurityAccess()
        {
            SetupManufacturingSession();

            hardware.Transmit(arbId, GetBytes("03-22-12-34-00-00-00-00"));

            if (await Task.WhenAny(tcs.Task, Task.Delay(10000)) == tcs.Task)
            {
                var frameEventArgs = await tcs.Task;

                var responsePrefix = responseArbId + "037F223300000000";

                var result = responseArbId + HexToString(BitConverter.ToString(frameEventArgs.Data));

                Assert.AreEqual(responsePrefix, result, "A negative response was not received");
            }
            else
            {
                Assert.Fail("Data reception did not occur within the specified time frame.");
            }
        }
        #endregion

        #region Unknown DID

        /*
            If you write unknown DID(such as 0x1111) for the command you will get NRC = 0x12
        */

        [TestMethod]
        public async Task NegativeResponseControlUnknownDID()
        {
            SetupManufacturingSession();
            SetupSecurityAccess();

            hardware.Transmit(arbId, GetBytes("03-2F-11-11-00-00-00-00"));

            if (await Task.WhenAny(tcs.Task, Task.Delay(10000)) == tcs.Task)
            {
                var frameEventArgs = await tcs.Task;

                var responsePrefix = responseArbId + "037F2F1200000000";

                var result = responseArbId + HexToString(BitConverter.ToString(frameEventArgs.Data));

                Assert.AreEqual(responsePrefix, result, "A negative response was not received");
            }
            else
            {
                Assert.Fail("Data reception did not occur within the specified time frame.");
            }
        }
        #endregion

        #region Unknown SID

        /*
            For unknown SID you will get NRC=0x11
        */

        [TestMethod]
        public async Task NegativeResponseControlUnknownSID()
        {
            SetupManufacturingSession();
            SetupSecurityAccess();

            hardware.Transmit(arbId, GetBytes("00-00-00-00-00-00-00-00"));

            if (await Task.WhenAny(tcs.Task, Task.Delay(10000)) == tcs.Task)
            {
                var frameEventArgs = await tcs.Task;

                var responsePrefix = responseArbId + "037F001100000000";

                var result = responseArbId + HexToString(BitConverter.ToString(frameEventArgs.Data));

                Assert.AreEqual(responsePrefix, result, "A negative response was not received");
            }
            else
            {
                Assert.Fail("Data reception did not occur within the specified time frame.");
            }
        }
        #endregion

        #region Unknown DataRecord

        /*
            For unknown dataRecord you will get NRC = 0x31
            In example PWM set to 150(hex 0x96) which is not applicable

        */

        [TestMethod]
        public async Task NegativeResponseControlUnknownDataRecord()
        {
            SetupManufacturingSession();
            SetupSecurityAccess();

            hardware.Transmit(arbId, GetBytes("05-2E-61-0B-00-96-00-00"));

            if (await Task.WhenAny(tcs.Task, Task.Delay(10000)) == tcs.Task)
            {
                var frameEventArgs = await tcs.Task;

                var responsePrefix = responseArbId + "037F2E3100000000";

                var result = responseArbId + HexToString(BitConverter.ToString(frameEventArgs.Data));

                Assert.AreEqual(responsePrefix, result, "A negative response was not received");
            }
            else
            {
                Assert.Fail("Data reception did not occur within the specified time frame.");
            }
        }
        #endregion

        #region Wrong Message Format

        /*
            If format of message wrong you will get NRC=0x13
            Example: For WDBI first byte must be 05h if you use another value you will get NRC 0x13
        */

        [TestMethod]
        public async Task NegativeResponseControlWrongFormat()
        {
            SetupManufacturingSession();
            SetupSecurityAccess();

            hardware.Transmit(arbId, GetBytes("04-2E-61-0B-00-46-00-00"));

            if (await Task.WhenAny(tcs.Task, Task.Delay(10000)) == tcs.Task)
            {
                var frameEventArgs = await tcs.Task;

                var responsePrefix = responseArbId + "037F2E1300000000";

                var result = responseArbId + HexToString(BitConverter.ToString(frameEventArgs.Data));

                Assert.AreEqual(responsePrefix, result, "A negative response was not received");
            }
            else
            {
                Assert.Fail("Data reception did not occur within the specified time frame.");
            }
        }
        #endregion

        #endregion
    }
}
