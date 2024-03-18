using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiagBoxUnitTest
{
    /// <summary>
    /// Represents a function in Unit Test.
    /// </summary>
    public class Function
    {
        /// <summary>
        /// Gets or sets the address of the function.
        /// </summary>
        public byte Address { get; set; }

        /// <summary>
        /// Gets or sets the name of the function.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the group to which the function belongs.
        /// </summary>
        public string Group { get; set; }
    }

    /// <summary>
    /// Represents a service in Unit test.
    /// </summary>
    public class Service
    {
        /// <summary>
        /// Gets or sets the name of the service.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the request message format for the service.
        /// </summary>
        public string Request { get; set; }

        /// <summary>
        /// Gets or sets the response message format for the service.
        /// </summary>
        public string Response { get; set; }

        /// <summary>
        /// Gets or sets the list of functions associated with the service.
        /// </summary>
        public List<Function> Functions { get; set; }

        /// <summary>
        /// Formats the request message of the service for a specific function.
        /// </summary>
        /// <param name="function">The function to include in the request message.</param>
        /// <returns>The formatted request message as a byte array.</returns>
        public byte[] FormatRequestMessage(Function function = null)
        {
            return FormatMessageInternal(Request, function);
        }

        /// <summary>
        /// Formats the response message of the service for a specific function.
        /// </summary>
        /// <param name="function">The function to include in the response message.</param>
        /// <returns>The formatted response message as a byte array.</returns>
        internal byte[] FormatResponseMessage(Function function = null)
        {
            return FormatMessageInternal(Response, function);
        }

        /// <summary>
        /// Formats the message internally by replacing the placeholder with the function's address.
        /// </summary>
        /// <param name="format">The message format string.</param>
        /// <param name="function">The function to include in the message.</param>
        /// <returns>The formatted message as a byte array.</returns>
        private byte[] FormatMessageInternal(string format, Function function)
        {
            return format.Replace("XX", function?.Address.ToString("X2")).Split('-').Select(x => byte.Parse(x, NumberStyles.HexNumber)).ToArray();
        }
    }

    /// <summary>
    /// Represents a class providing test data for services and functions.
    /// </summary>
    public class TestData
    {
        /// <summary>
        /// Provides a collection of test cases represented as object arrays.
        /// </summary>
        public static IEnumerable<object[]> Cases
        {
            get
            {
                foreach (var service in TestData.GetServices())
                    if (service.Functions == null)
                        yield return new object[] { $"{service.Name}", service.FormatRequestMessage(), service.FormatResponseMessage() };
                    else
                        foreach (var func in service.Functions)
                            yield return new object[] { $"{service.Name}    ({func.Name})", service.FormatRequestMessage(func), service.FormatResponseMessage(func) };
            }
        }

        /// <summary>
        /// Gets a list of services with their corresponding request and response formats.
        /// </summary>
        /// <returns>A list of Service objects.</returns>
        public static List<Service> GetServices()
        {
            return new List<Service>
            {
                new Service { Name = "Read BTT-BTS",          Request = "07-2F-61-01-XX-00-00-00", Response = "06-6F-61-01-XX", Functions = TestData.GetFunctions("BTT-BTS") },
                new Service { Name = "Read SWCH",             Request = "07-2F-61-02-XX-00-00-00", Response = "06-6F-61-02-XX", Functions = TestData.GetFunctions("SWCH") },
                new Service { Name = "Read EO",               Request = "07-2F-61-03-XX-00-00-00", Response = "06-6F-61-03-XX", Functions = TestData.GetFunctions("EO") },
                new Service { Name = "Read ADC",              Request = "07-2F-61-06-XX-00-00-00", Response = "06-6F-61-06-XX", Functions = TestData.GetFunctions("ADC") },
                new Service { Name = "Read XS4200",           Request = "07-2F-61-08-XX-00-00-00", Response = "06-6F-61-08-XX", Functions = TestData.GetFunctions("XS4200") },
                new Service { Name = "Read DIO",              Request = "07-2F-61-09-XX-00-00-00", Response = "06-6F-61-09-XX", Functions = TestData.GetFunctions("DIO") },
                new Service { Name = "Read VND5T035LAK",      Request = "07-2F-61-0C-XX-00-00-00", Response = "06-6F-61-0C-XX", Functions = TestData.GetFunctions("VND5T035LAK") },
                new Service { Name = "Read HSD",              Request = "07-2F-61-11-XX-00-00-00", Response = "06-6F-61-11-XX", Functions = TestData.GetFunctions("HSD") },
                new Service { Name = "Read MPQ6528",          Request = "07-2F-61-13-XX-00-00-00", Response = "06-6F-61-13-XX", Functions = TestData.GetFunctions("MPQ6528") },
                new Service { Name = "Read PWHALL",           Request = "07-2F-61-14-XX-00-00-00", Response = "06-6F-61-14-XX", Functions = TestData.GetFunctions("PWHALL") },

                new Service { Name = "Open EO",               Request = "07-2F-61-04-XX-00-00-00", Response = "06-6F-61-04-XX", Functions = TestData.GetFunctions("EO") },
                new Service { Name = "Open DIO",              Request = "07-2F-61-0A-XX-00-00-00", Response = "06-6F-61-0A-XX", Functions = TestData.GetFunctions("DIO") },

                new Service { Name = "Close EO",              Request = "07-2F-61-05-XX-00-00-00", Response = "06-6F-61-05-XX", Functions = TestData.GetFunctions("EO") },
                new Service { Name = "Close DIO",             Request = "07-2F-61-0B-XX-00-00-00", Response = "06-6F-61-0B-XX", Functions = TestData.GetFunctions("DIO") },

                new Service { Name = "Set PWM",               Request = "07-2F-61-12-XX-00-00-00", Response = "06-6F-61-12-XX", Functions = TestData.GetFunctions("PWM") },
                new Service { Name = "Set PWM XS4200",        Request = "07-2F-61-07-XX-00-00-00", Response = "06-6F-61-07-XX", Functions = TestData.GetFunctions("XS4200") },

                new Service { Name = "LED",                   Request = "03-2F-61-XX-00-00-00-00", Response = "00-00-00-00-6F", Functions = TestData.GetFunctions("LED") },

                new Service { Name = "LIN",                   Request = "07-2F-61-15-XX-00-00-00", Response = "06-6F-61-15-XX", Functions = TestData.GetFunctions("LIN") },

                new Service { Name = "DoorControl Enable",    Request = "07-2F-61-44-XX-00-00-00", Response = "06-6F-61-44-XX", Functions = TestData.GetFunctions("DoorControl") },
                new Service { Name = "DoorControl Disable",   Request = "07-2F-61-45-XX-00-00-00", Response = "06-6F-61-45-XX", Functions = TestData.GetFunctions("DoorControl") },
                new Service { Name = "DoorControl ReadDiag",  Request = "07-2F-61-10-XX-00-00-00", Response = "06-6F-61-10-XX", Functions = TestData.GetFunctions("DoorControl") },
                new Service { Name = "DoorControl ReadADC",   Request = "07-2F-61-06-XX-00-00-00", Response = "06-6F-61-06-XX", Functions = TestData.GetFunctions("DoorControl_ADC") },

                new Service { Name = "Power Window Enable",   Request = "07-2F-61-46-XX-00-00-00", Response = "06-6F-61-46-XX", Functions = TestData.GetFunctions("PowerWindow") },
                new Service { Name = "Power Window Disable",  Request = "07-2F-61-47-XX-00-00-00", Response = "06-6F-61-47-XX", Functions = TestData.GetFunctions("PowerWindow") },
                new Service { Name = "Power Window ReadDiag", Request = "07-2F-61-13-XX-00-00-00", Response = "06-6F-61-13-XX", Functions = TestData.GetFunctions("PowerWindow") },
                new Service { Name = "Power Window ReadADC",  Request = "07-2F-61-06-XX-00-00-00", Response = "06-6F-61-06-XX", Functions = TestData.GetFunctions("PowerWindow_ADC") },

                new Service { Name = "DiagSession",           Request = "02-10-61-00-00-00-00-00", Response = "02-50-61-00-00" },
                new Service { Name = "RequestSeed",           Request = "04-27-01-00-00-00-00-00", Response = "04-67-01-00-00" },
                new Service { Name = "SendKey",               Request = "04-27-02-00-00-00-00-00", Response = "04-67-02-00-00" },
                new Service { Name = "ECUReset",              Request = "02-11-03-00-00-00-00-00", Response = "02-51-03-00-00" },
                new Service { Name = "WriteDataByIdentifier", Request = "05-2E-12-34-00-00-00-00", Response = "03-6E-12-34-00" },
                new Service { Name = "ReadDataByIdentifier",  Request = "03-22-12-34-00-00-00-00", Response = "05-62-12-34-00" },
                new Service { Name = "TesterPresent",         Request = "01-3E-00-00-00-00-00-00", Response = "01-7E-00-00-00" },
                new Service { Name = "UDS_TEST_PARAM",        Request = "03-2F-12-34-00-00-00-00", Response = "00-00-00-00-6F" },

                new Service { Name = "Read Diag HallSensorR", Request = "07-2F-61-0D-00-00-00-00", Response = "06-6F-61-0D-00" },
                new Service { Name = "Read Diag HallSensorL", Request = "07-2F-61-0E-00-00-00-00", Response = "06-6F-61-0E-00" },
            };
        }

        /// <summary>
        /// Gets a list of functions filtered by a specified group.
        /// </summary>
        /// <param name="group">The group used to filter functions.</param>
        /// <returns>A list of Function objects related to the specified group.</returns>
        public static List<Function> GetFunctions(string group)
        {
            return new List<Function>
                {
                    new Function{ Address = 0x00, Group = "BTT-BTS", Name = "O_SteeringColumnSwitchIlluminationSupply_1.5A" },
                    new Function{ Address = 0x01, Group = "BTT-BTS", Name = "O_StartStopFunction12VLED_1A" },
                    new Function{ Address = 0x02, Group = "BTT-BTS", Name = "O_SunroofMotorCloseSupply_3A" },
                    new Function{ Address = 0x03, Group = "BTT-BTS", Name = "O_SunroofMotorOpenSupply_3A" },
                    new Function{ Address = 0x04, Group = "BTT-BTS", Name = "O_HeatedWindshieldSupply_3A" },
                    new Function{ Address = 0x05, Group = "BTT-BTS", Name = "O_HeatedMirrorSupply_3A" },
                    new Function{ Address = 0x06, Group = "BTT-BTS", Name = "O_MainRightMirrorDownLeftCtrl_0.5A" },
                    new Function{ Address = 0x07, Group = "BTT-BTS", Name = "O_MainRightMirrorRightCtrl_0.5A" },
                    new Function{ Address = 0x08, Group = "BTT-BTS", Name = "O_MainLeftMirrorUpCtrl_0.5A" },
                    new Function{ Address = 0x09, Group = "BTT-BTS", Name = "O_MainLeftMirrorDownLeftCtrl_0.5A" },
                    new Function{ Address = 0x0A, Group = "BTT-BTS", Name = "O_MainLeftMirrorRightCtrl_0.5A" },
                    new Function{ Address = 0x0B, Group = "BTT-BTS", Name = "O_MainRightMirrorUpCtrl_0.5A" },

                    new Function { Address = 0x00, Group = "SWCH", Name = "MapReadingLampDriverSideSwitch" },
                    new Function { Address = 0x01, Group = "SWCH", Name = "Spare1ConfigSwitch" },
                    new Function { Address = 0x02, Group = "SWCH", Name = "IgnitionAccessorySwitch" },
                    new Function { Address = 0x03, Group = "SWCH", Name = "IgnitionONSwitch" },
                    new Function { Address = 0x04, Group = "SWCH", Name = "AjarOnOffControl" },
                    new Function { Address = 0x05, Group = "SWCH", Name = "CabinLockSwitch_wakeup" },
                    new Function { Address = 0x06, Group = "SWCH", Name = "BedAreaLightSwitch" },
                    new Function { Address = 0x07, Group = "SWCH", Name = "Startstopswitch1" },
                    new Function { Address = 0x08, Group = "SWCH", Name = "Doorlockrightackswitch" },
                    new Function { Address = 0x09, Group = "SWCH", Name = "ManeuveringSwitch" },
                    new Function { Address = 0x0A, Group = "SWCH", Name = "CruiseControlSetPlusSwitch" },
                    new Function { Address = 0x0B, Group = "SWCH", Name = "CruiseControlSetMinusSwitch" },
                    new Function { Address = 0x0C, Group = "SWCH", Name = "CruiseControlResumeSwitch" },
                    new Function { Address = 0x0D, Group = "SWCH", Name = "CruiseControlOFFSwitch" },
                    new Function { Address = 0x0E, Group = "SWCH", Name = "DPFManuelRegenSwitch" },
                    new Function { Address = 0x0F, Group = "SWCH", Name = "WiperParkPositionSwitch" },
                    new Function { Address = 0x10, Group = "SWCH", Name = "SpaceSwitch2" },
                    new Function { Address = 0x11, Group = "SWCH", Name = "PowerWindowPASSDownSwitch" },
                    new Function { Address = 0x12, Group = "SWCH", Name = "PowerWindowPASSUpSwitch" },
                    new Function { Address = 0x13, Group = "SWCH", Name = "Doorlockleftackswitch" },
                    new Function { Address = 0x14, Group = "SWCH", Name = "Volumeupswitch" },
                    new Function { Address = 0x15, Group = "SWCH", Name = "VolumeDownSwitch" },
                    new Function { Address = 0x16, Group = "SWCH", Name = "SunroofOpenSwitch" },
                    new Function { Address = 0x17, Group = "SWCH", Name = "SunroofCloseSwitch" },
                    new Function { Address = 0x18, Group = "SWCH", Name = "HazardSwitch" },
                    new Function { Address = 0x19, Group = "SWCH", Name = "DoorAjarLeftSwitch" },
                    new Function { Address = 0x1A, Group = "SWCH", Name = "DoorAjarRightSwitch" },
                    new Function { Address = 0x1B, Group = "SWCH", Name = "MapReadingLampPassSideSwitch" },
                    new Function { Address = 0x1C, Group = "SWCH", Name = "DomeLampSwitch" },
                    new Function { Address = 0x1D, Group = "SWCH", Name = "Ambientlightswitch" },
                    new Function { Address = 0x1E, Group = "SWCH", Name = "DiffLock1FeedbackSwitch" },
                    new Function { Address = 0x1F, Group = "SWCH", Name = "SpaceSwitch3" },
                    new Function { Address = 0x20, Group = "SWCH", Name = "PowerOFFroadSwitch" },
                    new Function { Address = 0x21, Group = "SWCH", Name = "Lowlinerswitch" },
                    new Function { Address = 0x22, Group = "SWCH", Name = "Difflock1driverrequestswitch" },
                    new Function { Address = 0x23, Group = "SWCH", Name = "PTOActivation1Switch" },
                    new Function { Address = 0x24, Group = "SWCH", Name = "BlendingSwitch" },
                    new Function { Address = 0x25, Group = "SWCH", Name = "ActiveRideHeight" },
                    new Function { Address = 0x26, Group = "SWCH", Name = "DPFInhibitSwitch" },
                    new Function { Address = 0x27, Group = "SWCH", Name = "CabinTiltSwitch" },
                    new Function { Address = 0x28, Group = "SWCH", Name = "AC_CompressorSwitch" },
                    new Function { Address = 0x29, Group = "SWCH", Name = "Frontsetswitch" },
                    new Function { Address = 0x2A, Group = "SWCH", Name = "TurnIndicatorLeftSwitch" },
                    new Function { Address = 0x2B, Group = "SWCH", Name = "Interiorlampoffswitch" },
                    new Function { Address = 0x2C, Group = "SWCH", Name = "FlashToPassSwitch" },
                    new Function { Address = 0x2D, Group = "SWCH", Name = "ParkingBrakeSwitch" },
                    new Function { Address = 0x2E, Group = "SWCH", Name = "SpaceSwitch1" },
                    new Function { Address = 0x2F, Group = "SWCH", Name = "HornActivateSwitch" },
                    new Function { Address = 0x30, Group = "SWCH", Name = "RollingBackInput" },
                    new Function { Address = 0x31, Group = "SWCH", Name = "StowageBoxInput" },
                    new Function { Address = 0x32, Group = "SWCH", Name = "CabinUnlockSwitch_wakeup" },
                    new Function { Address = 0x33, Group = "SWCH", Name = "SpaceSwitch5" },
                    new Function { Address = 0x34, Group = "SWCH", Name = "ForwardDrivingSwitch" },
                    new Function { Address = 0x35, Group = "SWCH", Name = "HornTypeSelectionSwitch" },
                    new Function { Address = 0x36, Group = "SWCH", Name = "Startstopswitch2" },
                    new Function { Address = 0x37, Group = "SWCH", Name = "Startstopswitch3" },
                    new Function { Address = 0x38, Group = "SWCH", Name = "Frontresetswitch" },
                    new Function { Address = 0x39, Group = "SWCH", Name = "Blowerctrlswitch" },
                    new Function { Address = 0x3A, Group = "SWCH", Name = "AlternatorChargingStatus" },
                    new Function { Address = 0x3B, Group = "SWCH", Name = "TrailerTagAxleLiftingSwitch" },
                    new Function { Address = 0x3C, Group = "SWCH", Name = "HeatedWindshieldSwitch" },
                    new Function { Address = 0x3D, Group = "SWCH", Name = "EHPASInput" },
                    new Function { Address = 0x3E, Group = "SWCH", Name = "PTOAcknowledgementSwitch" },
                    new Function { Address = 0x3F, Group = "SWCH", Name = "AxleLifting1Switch" },
                    new Function { Address = 0x40, Group = "SWCH", Name = "DamperConnectionSwitch" },
                    new Function { Address = 0x41, Group = "SWCH", Name = "RockingSwitch" },
                    new Function { Address = 0x42, Group = "SWCH", Name = "TrailerDetectionSwitch" },
                    new Function { Address = 0x43, Group = "SWCH", Name = "SpaceSwitch4" },
                    new Function { Address = 0x44, Group = "SWCH", Name = "VehSpeedLimitInput" },

                    new Function { Address = 0x00, Group = "EO", Name = "EO_SunroofMotorCloseSupply_HS" },
                    new Function { Address = 0x01, Group = "EO", Name = "EO_StartStopILED_IntLightDim12VLED" },
                    new Function { Address = 0x02, Group = "EO", Name = "EO_SunroofMotorCloseSupply_LS" },
                    new Function { Address = 0x03, Group = "EO", Name = "EO_SunroofMotorOpenSupply_LS" },
                    new Function { Address = 0x04, Group = "EO", Name = "EO_WiperSpeed" },
                    new Function { Address = 0x05, Group = "EO", Name = "EO_ACCutOffSupply" },
                    new Function { Address = 0x06, Group = "EO", Name = "EO_SunroofMotorOpenSupply_HS" },
                    new Function { Address = 0x07, Group = "EO", Name = "EO_5V_DCDC" },
                    new Function { Address = 0x08, Group = "EO", Name = "EO_WaterPumpSupply" },
                    new Function { Address = 0x09, Group = "EO", Name = "EO_TurnFront_SideRightSupply" },
                    new Function { Address = 0x0A, Group = "EO", Name = "EO_EngineBrakeSupply" },
                    new Function { Address = 0x0B, Group = "EO", Name = "EO_DoorCap_Pullup_CTRL" },
                    new Function { Address = 0x0C, Group = "EO", Name = "TP1355" },
                    new Function { Address = 0x0D, Group = "EO", Name = "EO_FRSTBY" },
                    new Function { Address = 0x0E, Group = "EO", Name = "TP1356" },
                    new Function { Address = 0x0F, Group = "EO", Name = "EO_CabinTiltValveSupply" },
                    new Function { Address = 0x10, Group = "EO", Name = "EO_DoorCap1_CTRL" },
                    new Function { Address = 0x11, Group = "EO", Name = "EO_Multisense_SEL1" },
                    new Function { Address = 0x12, Group = "EO", Name = "EO_LowLinerFrontSideLiftedValveSupply" },
                    new Function { Address = 0x13, Group = "EO", Name = "MO_VCAN_STBN" },
                    new Function { Address = 0x14, Group = "EO", Name = "EO_AirHornActivateSupply" },
                    new Function { Address = 0x15, Group = "EO", Name = "EO_HallSensorLeftSupply" },
                    new Function { Address = 0x16, Group = "EO", Name = "EO_HallSensorRightSupply" },
                    new Function { Address = 0x17, Group = "EO", Name = "EO_LF_RSTN" },
                    new Function { Address = 0x18, Group = "EO", Name = "EO_DifferentialLockValve1Supply" },
                    new Function { Address = 0x19, Group = "EO", Name = "EO_DDRLSupplyLeft" },
                    new Function { Address = 0x1A, Group = "EO", Name = "EO_InterLockValveSupply" },
                    new Function { Address = 0x1B, Group = "EO", Name = "EO_PTOValveSupply" },
                    new Function { Address = 0x1C, Group = "EO", Name = "EO_ReverseGearSupply" },
                    new Function { Address = 0x1D, Group = "EO", Name = "EO_GLOBALENABLE" },
                    new Function { Address = 0x1E, Group = "EO", Name = "EO_DDRLSupplyRight" },
                    new Function { Address = 0x1F, Group = "EO", Name = "EO_RSTB_HSD_SPI1" },
                    new Function { Address = 0x20, Group = "EO", Name = "EO_ManeuveringSwitchIllimunationSupply" },
                    new Function { Address = 0x21, Group = "EO", Name = "EO_TrailerLeftSupply" },
                    new Function { Address = 0x22, Group = "EO", Name = "EO_TrailerRightSupply" },
                    new Function { Address = 0x23, Group = "EO", Name = "EO_Lelt_PowerWindow_Nsleep" },
                    new Function { Address = 0x24, Group = "EO", Name = "EO_Right_PowerWindow_Nsleep" },
                    new Function { Address = 0x25, Group = "EO", Name = "MO_VCAN_EN" },
                    new Function { Address = 0x26, Group = "EO", Name = "EO_BlowerControlRelaySupply" },
                    new Function { Address = 0x27, Group = "EO", Name = "EO_TrailerTagAxleLiftingSupply" },
                    new Function { Address = 0x28, Group = "EO", Name = "EO_StepLightsSupply" },
                    new Function { Address = 0x29, Group = "EO", Name = "EO_DoorLockIndicatorSupply" },
                    new Function { Address = 0x2A, Group = "EO", Name = "EO_HeatedMirrorSupply" },
                    new Function { Address = 0x2B, Group = "EO", Name = "EO_HeatedWindshieldSupply" },
                    new Function { Address = 0x2C, Group = "EO", Name = "EO_LDWSFuncIlluminationLED" },
                    new Function { Address = 0x2D, Group = "EO", Name = "EO_TSRFuncIlluminationLED" },
                    new Function { Address = 0x2E, Group = "EO", Name = "EO_BlendingFuncIlluminationLED" },
                    new Function { Address = 0x2F, Group = "EO", Name = "EO_ESPOffOutputLED" },
                    new Function { Address = 0x30, Group = "EO", Name = "EO_MainLeftMirrorLeftCtrl" },
                    new Function { Address = 0x31, Group = "EO", Name = "EO_MainLeftMirrorDownLeftCtrl" },
                    new Function { Address = 0x32, Group = "EO", Name = "EO_MainRightMirrorUpRightCtrl" },
                    new Function { Address = 0x33, Group = "EO", Name = "EO_MainLeftMirrorUpRightCtrl" },
                    new Function { Address = 0x34, Group = "EO", Name = "EO_MainRightMirrorDownCtrl" },
                    new Function { Address = 0x35, Group = "EO", Name = "EO_MainRightMirrorUpCtrl" },
                    new Function { Address = 0x36, Group = "EO", Name = "EO_MainLeftMirrorRightCtrl" },
                    new Function { Address = 0x37, Group = "EO", Name = "EO_MainRightMirrorRightCtrl" },
                    new Function { Address = 0x38, Group = "EO", Name = "MO_EN_PDCtrl" },
                    new Function { Address = 0x39, Group = "EO", Name = "EO_MainLeftMirrorDownCtrl" },
                    new Function { Address = 0x3A, Group = "EO", Name = "EO_MainRightMirrorLeftCtrl" },
                    new Function { Address = 0x3B, Group = "EO", Name = "EO_MainLeftMirrorUpCtrl" },
                    new Function { Address = 0x3C, Group = "EO", Name = "EO_BatterySaverSetCtrl" },
                    new Function { Address = 0x3D, Group = "EO", Name = "EO_BatterySaverResetCtrl" },
                    new Function { Address = 0x3E, Group = "EO", Name = "EO_MainRightMirrorDownLeftCtrl" },
                    new Function { Address = 0x3F, Group = "EO", Name = "EO_RF_P17_LNA" },
                    new Function { Address = 0x40, Group = "EO", Name = "EO_Left_PowerWindow_ENA" },
                    new Function { Address = 0x41, Group = "EO", Name = "EO_Left_PowerWindow_ENB" },
                    new Function { Address = 0x42, Group = "EO", Name = "EO_SpareDigitalOutput1" },
                    new Function { Address = 0x43, Group = "EO", Name = "EO_SpareDigitalOutput2" },
                    new Function { Address = 0x44, Group = "EO", Name = "EO_Right_PowerWindow_ENA" },
                    new Function { Address = 0x45, Group = "EO", Name = "EO_Right_PowerWindow_ENB" },
                    new Function { Address = 0x46, Group = "EO", Name = "EO_TurnFront_SideLeftSupply" },
                    new Function { Address = 0x47, Group = "EO", Name = "U108_Q8" },
                    new Function { Address = 0x48, Group = "EO", Name = "EO_DoorLockLeft_INA" },
                    new Function { Address = 0x49, Group = "EO", Name = "EO_DoorLockLeft_INB" },
                    new Function { Address = 0x4A, Group = "EO", Name = "EO_DoorLock_SEL0" },
                    new Function { Address = 0x4B, Group = "EO", Name = "EO_DoorLock_SEL1" },
                    new Function { Address = 0x4C, Group = "EO", Name = "EO_DoorLockRight_INA" },
                    new Function { Address = 0x4D, Group = "EO", Name = "EO_DoorLockRight_INB" },
                    new Function { Address = 0x4E, Group = "EO", Name = "EO_DoorLockPWM_CTRL" },
                    new Function { Address = 0x4F, Group = "EO", Name = "EO_DoorCap2_CTRL" },
                    new Function { Address = 0x50, Group = "EO", Name = "PD_ManeuveringSwitch" },
                    new Function { Address = 0x51, Group = "EO", Name = "PD_DoorLockRightAckSwitch" },
                    new Function { Address = 0x52, Group = "EO", Name = "PD_CruiseControlSetPlusSwitch" },
                    new Function { Address = 0x53, Group = "EO", Name = "PD_CruiseControlSetMinusSwitch" },
                    new Function { Address = 0x54, Group = "EO", Name = "PD_CruiseControlResumeSwitch" },
                    new Function { Address = 0x55, Group = "EO", Name = "PD_CruiseControlOFFSwitch" },
                    new Function { Address = 0x56, Group = "EO", Name = "PD_WiperParkPositionSwitch" },
                    new Function { Address = 0x57, Group = "EO", Name = "PD_DPFManuelRegenSwitch" },
                    new Function { Address = 0x58, Group = "EO", Name = "PD_PowerWindowPASSDownSwitch" },
                    new Function { Address = 0x59, Group = "EO", Name = "PD_SpaceSwitch2" },
                    new Function { Address = 0x5A, Group = "EO", Name = "PD_DoorLockLeftAckSwitch" },
                    new Function { Address = 0x5B, Group = "EO", Name = "PD_PowerWindowPASSUpSwitch" },
                    new Function { Address = 0x5C, Group = "EO", Name = "PD_VolumeDownSwitch" },
                    new Function { Address = 0x5D, Group = "EO", Name = "PD_VolumeUpSwitch" },
                    new Function { Address = 0x5E, Group = "EO", Name = "PD_DiffLock1FeedbackSwitch" },
                    new Function { Address = 0x5F, Group = "EO", Name = "PD_SpaceSwitch3" },
                    new Function { Address = 0x60, Group = "EO", Name = "PD_LowLinerSwitch" },
                    new Function { Address = 0x61, Group = "EO", Name = "PD_PowerOFFroadSwitch" },
                    new Function { Address = 0x62, Group = "EO", Name = "PD_PTOActivation1Switch" },
                    new Function { Address = 0x63, Group = "EO", Name = "PD_DiffLock1DriverRequestSwitch" },
                    new Function { Address = 0x64, Group = "EO", Name = "PD_ActiveRideHeight" },
                    new Function { Address = 0x65, Group = "EO", Name = "PD_BlendingSwitch" },
                    new Function { Address = 0x66, Group = "EO", Name = "PD_CabinTiltSwitch" },
                    new Function { Address = 0x67, Group = "EO", Name = "PD_DPFInhibitSwitch" },
                    new Function { Address = 0x68, Group = "EO", Name = "PD_FrontSetSwitch" },
                    new Function { Address = 0x69, Group = "EO", Name = "PD_AC_CompressorSwitch" },
                    new Function { Address = 0x6A, Group = "EO", Name = "PD_InteriorLampOffSwitch" },
                    new Function { Address = 0x6B, Group = "EO", Name = "PD_TurnIndicatorLeftSwitch" },
                    new Function { Address = 0x6C, Group = "EO", Name = "PD_BlowerCtrlSwitch" },
                    new Function { Address = 0x6D, Group = "EO", Name = "PD_FrontResetSwitch" },
                    new Function { Address = 0x6E, Group = "EO", Name = "PD_TrailerTagAxleLiftingSwitch" },
                    new Function { Address = 0x6F, Group = "EO", Name = "PD_AlternatorChargingStatus" },
                    new Function { Address = 0x70, Group = "EO", Name = "PD_EHPASInput" },
                    new Function { Address = 0x71, Group = "EO", Name = "PD_HeatedWindshieldSwitch" },
                    new Function { Address = 0x72, Group = "EO", Name = "PD_AxleLifting1Switch" },
                    new Function { Address = 0x73, Group = "EO", Name = "PD_PTOAcknowledgementSwitch" },
                    new Function { Address = 0x74, Group = "EO", Name = "PD_RockingSwitch" },
                    new Function { Address = 0x75, Group = "EO", Name = "PD_DamperConnectionSwitch" },
                    new Function { Address = 0x76, Group = "EO", Name = "PD_VehSpeedLimitInput" },
                    new Function { Address = 0x77, Group = "EO", Name = "PD_TrailerDetectionSwitch" },

                    new Function { Address = 0x00, Group = "ADC", Name = "ADC_KL15Relay2_KL50Relay" },
                    new Function { Address = 0x01, Group = "ADC", Name = "ADC_HallR_HallL" },
                    new Function { Address = 0x02, Group = "ADC", Name = "ADC_ClockSpring2RHSensor" },
                    new Function { Address = 0x03, Group = "ADC", Name = "ADC_AmbientLightSensor" },
                    new Function { Address = 0x04, Group = "ADC", Name = "ADC_MainLeftMirrorUpCtrl_DownLeftCtrl_DIAG" },
                    new Function { Address = 0x05, Group = "ADC", Name = "ADC_Rear_TURNL_FOG" },
                    new Function { Address = 0x06, Group = "ADC", Name = "ADC_DoorLockLeft" },
                    new Function { Address = 0x07, Group = "ADC", Name = "ADC_DoorLockRight" },
                    new Function { Address = 0x08, Group = "ADC", Name = "ADC_Heater_Mirror_Windshield" },
                    new Function { Address = 0x09, Group = "ADC", Name = "ADC_RearParkingLight_Horn" },
                    new Function { Address = 0x0A, Group = "ADC", Name = "ADC_FuelLevel" },
                    new Function { Address = 0x0B, Group = "ADC", Name = "ADC_StartStopILED_IntLightDim12VLED" },
                    new Function { Address = 0x0C, Group = "ADC", Name = "ADC_KL15Relay1_KL75Relay" },
                    new Function { Address = 0x0D, Group = "ADC", Name = "ADC_24V_SUPPLY2" },
                    new Function { Address = 0x0E, Group = "ADC", Name = "ADC_24V_SUPPLY4" },
                    new Function { Address = 0x0F, Group = "ADC", Name = "ADC_TrailerLeftSupply" },
                    new Function { Address = 0x10, Group = "ADC", Name = "ADC_DDRLSupplyRight" },
                    new Function { Address = 0x11, Group = "ADC", Name = "ADC_RearParkingLeft_Right" },
                    new Function { Address = 0x12, Group = "ADC", Name = "ADC_BlendingFuncIlluminationLED" },
                    new Function { Address = 0x13, Group = "ADC", Name = "ADC_PTOValveSupply" },
                    new Function { Address = 0x14, Group = "ADC", Name = "ADC_CabinTiltValveSupply" },
                    new Function { Address = 0x15, Group = "ADC", Name = "ADC_AmbientLightSupply" },
                    new Function { Address = 0x16, Group = "ADC", Name = "ADC_ReverseGearSupply" },
                    new Function { Address = 0x17, Group = "ADC", Name = "ADC_TrailerRightSupply" },
                    new Function { Address = 0x18, Group = "ADC", Name = "ADC_DDRLSupplyLeft" },
                    new Function { Address = 0x19, Group = "ADC", Name = "ADC_HighBeamL_FrontParkingLight" },
                    new Function { Address = 0x1A, Group = "ADC", Name = "ADC_TSRFuncIlluminationLED" },
                    new Function { Address = 0x1B, Group = "ADC", Name = "ADC_LowLinerFrontSideLiftedValveSupply" },
                    new Function { Address = 0x1C, Group = "ADC", Name = "ADC_InterLockValveSupply" },
                    new Function { Address = 0x1D, Group = "ADC", Name = "ADC_BedAreaLightingSupply" },
                    new Function { Address = 0x1E, Group = "ADC", Name = "ADC_DoorLockIndicatorSupply" },
                    new Function { Address = 0x1F, Group = "ADC", Name = "ADC_EngineBrakeSupply" },
                    new Function { Address = 0x20, Group = "ADC", Name = "ADC_TurnFront_SideLeftSupply" },
                    new Function { Address = 0x21, Group = "ADC", Name = "ADC_ACCutOffSupply" },
                    new Function { Address = 0x22, Group = "ADC", Name = "ADC_LDWSFuncIlluminationLED" },
                    new Function { Address = 0x23, Group = "ADC", Name = "ADC_DomeLightSupply" },
                    new Function { Address = 0x24, Group = "ADC", Name = "ADC_DifferentialLockValve1Supply" },
                    new Function { Address = 0x25, Group = "ADC", Name = "ADC_SteeringColumnSwitchIlluminationSupply" },
                    new Function { Address = 0x26, Group = "ADC", Name = "ADC_HornActivateSupply" },
                    new Function { Address = 0x27, Group = "ADC", Name = "ADC_AirHornActivateSupply" },
                    new Function { Address = 0x28, Group = "ADC", Name = "ADC_TurnFront_SideRightSupply" },
                    new Function { Address = 0x29, Group = "ADC", Name = "ADC_ESPOffOutputLED" },
                    new Function { Address = 0x2A, Group = "ADC", Name = "ADC_TrailerTagAxleLiftingSupply" },
                    new Function { Address = 0x2B, Group = "ADC", Name = "ADC_ManeuveringSwitchIllimunationSupply" },
                    new Function { Address = 0x2C, Group = "ADC", Name = "ADC_StepLightsSupply" },
                    new Function { Address = 0x2D, Group = "ADC", Name = "ADC_StartStopFunction12VLED" },
                    new Function { Address = 0x2E, Group = "ADC", Name = "ADC_RearTurnR_HBeamR" },
                    new Function { Address = 0x2F, Group = "ADC", Name = "ADC_Wiper" },
                    new Function { Address = 0x30, Group = "ADC", Name = "ADC_MainLeftMirrorRightCtrl_MainRightMirrorUpCtrl_DIAG" },
                    new Function { Address = 0x31, Group = "ADC", Name = "ADC_Right_PowerWindow_Current" },
                    new Function { Address = 0x32, Group = "ADC", Name = "ADC_Left_PowerWindow_Current" },
                    new Function { Address = 0x33, Group = "ADC", Name = "ADC_MAPLAMPPWM" },
                    new Function { Address = 0x34, Group = "ADC", Name = "ADC_24V_SUPPLY3" },
                    new Function { Address = 0x35, Group = "ADC", Name = "ADC_24V_SUPPLY1" },
                    new Function { Address = 0x36, Group = "ADC", Name = "ADC_ClockSpring1LHSensor" },
                    new Function { Address = 0x37, Group = "ADC", Name = "ADC_ClockSpring1RHSensor" },
                    new Function { Address = 0x38, Group = "ADC", Name = "ADC_ClockSpring2LHSensor" },
                    new Function { Address = 0x39, Group = "ADC", Name = "ADC_WasherFW_FogLampFrontR" },
                    new Function { Address = 0x3A, Group = "ADC", Name = "ADC_FogLampFL_LowBeamL" },
                    new Function { Address = 0x3B, Group = "ADC", Name = "ADC_IntLightDim_LowBeamR" },
                    new Function { Address = 0x3C, Group = "ADC", Name = "ADC_WaterPumpSupply" },
                    new Function { Address = 0x3D, Group = "ADC", Name = "ADC_12V_RKESUPPLY" },
                    new Function { Address = 0x3E, Group = "ADC", Name = "ADC_SunroofMotor" },
                    new Function { Address = 0x3F, Group = "ADC", Name = "ADC_ClockSpring3LHSensor" },
                    new Function { Address = 0x40, Group = "ADC", Name = "ADC_ClockSpring3RHSensor" },
                    new Function { Address = 0x41, Group = "ADC", Name = "ADC_MainRightMirrorDownLeftCtrl_RightCtrl_DIAG" },
                    new Function { Address = 0x42, Group = "ADC", Name = "ADC_12V_SUPPLY" },
                    new Function { Address = 0x43, Group = "ADC", Name = "ADC_ExternalTemperature" },
                    new Function { Address = 0x44, Group = "ADC", Name = "ADC_SparePWM4" },
                    new Function { Address = 0x45, Group = "ADC", Name = "ADC_SparePWM3" },

                    new Function { Address = 0x00, Group = "XS4200", Name = "O_RearParkingLightLeftSupply_3A" },
                    new Function { Address = 0x01, Group = "XS4200", Name = "O_RearParkingLightRightSupply_3A" },
                    new Function { Address = 0x02, Group = "XS4200", Name = "WIPER_30A" },
                    new Function { Address = 0x03, Group = "XS4200", Name = "SPACE_CHANNEL" },
                    new Function { Address = 0x04, Group = "XS4200", Name = "O_FogLampFrontRightSupply_5A" },
                    new Function { Address = 0x05, Group = "XS4200", Name = "O_WasherForwardSupply_5A" },
                    new Function { Address = 0x06, Group = "XS4200", Name = "O_LowBeamLeftSupply_4A" },
                    new Function { Address = 0x07, Group = "XS4200", Name = "O_FogLampFrontLeftSupply_5A" },
                    new Function { Address = 0x08, Group = "XS4200", Name = "O_RearParkingLightTrailerSupply_7A" },
                    new Function { Address = 0x09, Group = "XS4200", Name = "O_HornActivateSupply_6A" },
                    new Function { Address = 0x0A, Group = "XS4200", Name = "O_HighBeamSupplyLeft_6A" },
                    new Function { Address = 0x0B, Group = "XS4200", Name = "O_FrontParkingLightSupply_3A" },
                    new Function { Address = 0x0C, Group = "XS4200", Name = "O_MapLampPassengerPWMSupply_1A" },
                    new Function { Address = 0x0D, Group = "XS4200", Name = "O_MapLampDriverPWMSupply_1A" },
                    new Function { Address = 0x0E, Group = "XS4200", Name = "O_InteriorLightDimmingSupply_3.5A" },
                    new Function { Address = 0x0F, Group = "XS4200", Name = "O_LowBeamRightSupply_3A" },
                    new Function { Address = 0x10, Group = "XS4200", Name = "O_HighBeamSupplyRight_7A" },
                    new Function { Address = 0x11, Group = "XS4200", Name = "O_TurnRearRightSupply_3A" },
                    new Function { Address = 0x12, Group = "XS4200", Name = "O_TurnRearLeftSupply_7A" },
                    new Function { Address = 0x13, Group = "XS4200", Name = "O_RearFogLampsSupply_7A" },
                    new Function { Address = 0x14, Group = "XS4200", Name = "O_KL75Relay1_1A" },
                    new Function { Address = 0x15, Group = "XS4200", Name = "O_KL15Relay1_1A" },
                    new Function { Address = 0x16, Group = "XS4200", Name = "DUMMYIC0_CHNL0" },
                    new Function { Address = 0x17, Group = "XS4200", Name = "DUMMYIC0_CHNL1" },
                    new Function { Address = 0x18, Group = "XS4200", Name = "O_KL50Output_1A" },
                    new Function { Address = 0x19, Group = "XS4200", Name = "O_KL15Relay2_1A" },
                    new Function { Address = 0x1A, Group = "XS4200", Name = "DUMMYIC1_CHNL0" },
                    new Function { Address = 0x1B, Group = "XS4200", Name = "DUMMYIC1_CHNL1" },

                    new Function { Address = 0x00, Group = "DIO", Name = "MO_EN_Expander" },
                    new Function { Address = 0x01, Group = "DIO", Name = "MO_MO_RF_RST_N" },
                    new Function { Address = 0x02, Group = "DIO", Name = "MO_WIPERFRONTBRAKE" },
                    new Function { Address = 0x03, Group = "DIO", Name = "MO_DiagSel" },
                    new Function { Address = 0x04, Group = "DIO", Name = "MO_ACCutOffSupply" },
                    new Function { Address = 0x05, Group = "DIO", Name = "MO_PEPS_DCDC_Supply_EN" },
                    new Function { Address = 0x06, Group = "DIO", Name = "MI_BatterySaverResetCtrl" },
                    new Function { Address = 0x07, Group = "DIO", Name = "MI_FogLampFL_LowBeamL_SYNC" },
                    new Function { Address = 0x08, Group = "DIO", Name = "MI_ForwardDrivingSwitch" },
                    new Function { Address = 0x09, Group = "DIO", Name = "MI_Right_PowerWindow_Nfault" },
                    new Function { Address = 0x0A, Group = "DIO", Name = "MI_SYNC_MAPLAMPPWM_SYNC" },
                    new Function { Address = 0x0B, Group = "DIO", Name = "MI_LF_BUSY" },
                    new Function { Address = 0x0C, Group = "DIO", Name = "MI_DoorCap1_DIAG" },
                    new Function { Address = 0x0D, Group = "DIO", Name = "MI_BlowerControlRelaySupply" },
                    new Function { Address = 0x0E, Group = "DIO", Name = "MI_IntLightDim_LowBeamR_SYNC" },
                    new Function { Address = 0x0F, Group = "DIO", Name = "MI_RF_P15_RDY_N" },
                    new Function { Address = 0x10, Group = "DIO", Name = "MI_BatterySaverSetCtrl" },
                    new Function { Address = 0x11, Group = "DIO", Name = "MI_Left_PowerWindow_Nfault" },
                    new Function { Address = 0x12, Group = "DIO", Name = "MI_DoorCap2_DIAG" },
                    new Function { Address = 0x13, Group = "DIO", Name = "MI_FofLampFR_WasherFW_SYNC" },

                    new Function { Address = 0x00, Group = "VND5T035LAK", Name = "O_DomeLightSupply_3A" },
                    new Function { Address = 0x01, Group = "VND5T035LAK", Name = "O_DDRLSupplyRight_3A" },
                    new Function { Address = 0x02, Group = "VND5T035LAK", Name = "O_DDRLSupplyLeft_3A" },
                    new Function { Address = 0x03, Group = "VND5T035LAK", Name = "O_EngineBrakeSupply_3A" },
                    new Function { Address = 0x04, Group = "VND5T035LAK", Name = "O_WaterPumpSupply_3A" },
                    new Function { Address = 0x05, Group = "VND5T035LAK", Name = "O_TurnFront_SideRightSupply_3A" },
                    new Function { Address = 0x06, Group = "VND5T035LAK", Name = "O_ACCutOffSupply_3A" },
                    new Function { Address = 0x07, Group = "VND5T035LAK", Name = "O_TurnFront_SideLeftSupply_3A" },
                    new Function { Address = 0x08, Group = "VND5T035LAK", Name = "O_StepLightsSupplyH62X_3A" },
                    new Function { Address = 0x09, Group = "VND5T035LAK", Name = "O_ManeuveringSwitchIllimunationSupply_3A" },
                    new Function { Address = 0x0A, Group = "VND5T035LAK", Name = "O_SpareDigitalOutput1_3A" },
                    new Function { Address = 0x0B, Group = "VND5T035LAK", Name = "O_SpareDigitalOutput2_3A" },
                    new Function { Address = 0x0C, Group = "VND5T035LAK", Name = "O_SparePWM3_3A" },
                    new Function { Address = 0x0D, Group = "VND5T035LAK", Name = "O_SparePWM4_3A" },
                    new Function { Address = 0x0E, Group = "VND5T035LAK", Name = "O_TrailerLeftSupply_5A" },
                    new Function { Address = 0x0F, Group = "VND5T035LAK", Name = "O_TrailerRightSupply_5A" },
                    new Function { Address = 0x10, Group = "VND5T035LAK", Name = "O_ReverseGearSupply_3A" },
                    new Function { Address = 0x11, Group = "VND5T035LAK", Name = "O_AirHornActivateSupply_3A" },

                    new Function { Address = 0x00, Group = "HSD", Name = "O_StartStopIllimunation12VLED_3A" },
                    new Function { Address = 0x01, Group = "HSD", Name = "O_Interior12VLightDimmingSupply_3A" },
                    new Function { Address = 0x02, Group = "HSD", Name = "O_DoorLockIndicatorSupply_2.5A" },
                    new Function { Address = 0x03, Group = "HSD", Name = "O_TSRFuncIlluminationLED_1A" },
                    new Function { Address = 0x04, Group = "HSD", Name = "O_LDWSFuncIlluminationLED_1A" },
                    new Function { Address = 0x05, Group = "HSD", Name = "O_ESPOffOutputLED_1A" },
                    new Function { Address = 0x06, Group = "HSD", Name = "O_BlendingFuncIlluminationLED_1A" },
                    new Function { Address = 0x07, Group = "HSD", Name = "O_BedAreaLightingSupply_1A" },
                    new Function { Address = 0x08, Group = "HSD", Name = "O_CabinTiltValveSupply_1.5A" },
                    new Function { Address = 0x09, Group = "HSD", Name = "O_AmbientLightSupply_1A" },
                    new Function { Address = 0x0A, Group = "HSD", Name = "O_TrailerTagAxleLiftingSupply_1.5A" },
                    new Function { Address = 0x0B, Group = "HSD", Name = "O_PTOValveSupply_1.5A" },
                    new Function { Address = 0x0C, Group = "HSD", Name = "O_LowLinerFrontSideLiftedValveSupply_1.5A" },
                    new Function { Address = 0x0D, Group = "HSD", Name = "O_DifferentialLockValve1Supply_1.5A" },
                    new Function { Address = 0x0E, Group = "HSD", Name = "O_InterLockValveSupply_1.5A" },

                    new Function { Address = 0x00, Group = "PWM", Name = "MO_Left_PowerWindow_PWMA" },
                    new Function { Address = 0x01, Group = "PWM", Name = "MO_PWM_CLK" },
                    new Function { Address = 0x02, Group = "PWM", Name = "MO_BedAreaLightingSupply_PWM" },
                    new Function { Address = 0x03, Group = "PWM", Name = "MO_Right_PowerWindow_PWMB" },
                    new Function { Address = 0x04, Group = "PWM", Name = "MO_Interior12VLightDimmingSupply_PWM" },
                    new Function { Address = 0x05, Group = "PWM", Name = "MO_DomeLightSupply_PWM" },
                    new Function { Address = 0x06, Group = "PWM", Name = "MO_SteeringColumnSwitchIlluminationSupply_PWM" },
                    new Function { Address = 0x07, Group = "PWM", Name = "MO_Right_PowerWindow_PWMA" },
                    new Function { Address = 0x08, Group = "PWM", Name = "MO_StartStopFunction12VLED_PWM" },
                    new Function { Address = 0x09, Group = "PWM", Name = "MO_AmbientLightSupply_PWM" },
                    new Function { Address = 0x0A, Group = "PWM", Name = "MO_DoorLockIndicatorSupply_PWM" },
                    new Function { Address = 0x0B, Group = "PWM", Name = "MO_StartStopIllimunation12VLED_PWM" },
                    new Function { Address = 0x0C, Group = "PWM", Name = "MO_Left_PowerWindow_PWMB" },

                    new Function { Address = 0x00, Group = "LIN", Name = "LIN1" },
                    new Function { Address = 0x01, Group = "LIN", Name = "LIN_IPSwitch" },
                    new Function { Address = 0x02, Group = "LIN", Name = "SpareLIN1" },
                    new Function { Address = 0x03, Group = "LIN", Name = "SpareLIN2" },

                    new Function { Address = 0x00, Group = "DoorControl", Name = "DoorRight_Unlock" },
                    new Function { Address = 0x01, Group = "DoorControl", Name = "DoorRight_Lock" },
                    new Function { Address = 0x02, Group = "DoorControl", Name = "DoorLeft_Lock" },
                    new Function { Address = 0x03, Group = "DoorControl", Name = "DoorLeft_Unlock" },

                    new Function { Address = 0x06, Group = "DoorControl_ADC", Name = "DoorLeft" },
                    new Function { Address = 0x07, Group = "DoorControl_ADC", Name = "DoorRight" },

                    new Function { Address = 0x00, Group = "PowerWindow", Name = "Power_Window_Left_Side" },
                    new Function { Address = 0x01, Group = "PowerWindow", Name = "Power_Window_Left_Side" },
                    new Function { Address = 0x02, Group = "PowerWindow", Name = "Power_Window_Right_Side" },
                    new Function { Address = 0x03, Group = "PowerWindow", Name = "Power_Window_Right_Side" },

                    new Function { Address = 0x33, Group = "PowerWindow_ADC", Name = "Power_Window_Left_Side" },
                    new Function { Address = 0x32, Group = "PowerWindow_ADC", Name = "Power_Window_Right_Side" },

                    new Function { Address = 0x00, Group = "MPQ6528", Name = "O_PowerWindowLeftDownRelay_12A" },
                    new Function { Address = 0x01, Group = "MPQ6528", Name = "O_PowerWindowLeftUpRelay_12A" },
                    new Function { Address = 0x02, Group = "MPQ6528", Name = "O_PowerWindowRightUpRelay_12A" },
                    new Function { Address = 0x03, Group = "MPQ6528", Name = "O_PowerWindowRightDownRelay_12A" },

                    new Function { Address = 0x00, Group = "PWHALL", Name = "ADC_PowerWindowDirectionLeftHallSensor" },
                    new Function { Address = 0x01, Group = "PWHALL", Name = "ADC_PowerWindowDirectionRightHallSensor" },

                    new Function { Address = 0x01, Group = "LED", Name = "LED0_OFF" },
                    new Function { Address = 0x02, Group = "LED", Name = "LED0_RED" },
                    new Function { Address = 0x03, Group = "LED", Name = "LED0_GREEN" },
                    new Function { Address = 0x04, Group = "LED", Name = "LED0_BLUE" },
                    new Function { Address = 0x05, Group = "LED", Name = "LED0_YELLOW" },
                    new Function { Address = 0x06, Group = "LED", Name = "LED0_WHITE" },
                    new Function { Address = 0x07, Group = "LED", Name = "LED0_PURPLE" },
                    new Function { Address = 0x08, Group = "LED", Name = "LED0_CYAN" },
                    new Function { Address = 0x11, Group = "LED", Name = "LED1_OFF" },
                    new Function { Address = 0x12, Group = "LED", Name = "LED1_RED" },
                    new Function { Address = 0x13, Group = "LED", Name = "LED1_GREEN" },
                    new Function { Address = 0x14, Group = "LED", Name = "LED1_BLUE" },
                    new Function { Address = 0x15, Group = "LED", Name = "LED1_YELLOW" },
                    new Function { Address = 0x16, Group = "LED", Name = "LED1_WHITE" },
                    new Function { Address = 0x17, Group = "LED", Name = "LED1_PURPLE" },
                    new Function { Address = 0x18, Group = "LED", Name = "LED1_CYAN" },
                }
                .Where(f => f.Group == group).ToList();
        }
    }
}
