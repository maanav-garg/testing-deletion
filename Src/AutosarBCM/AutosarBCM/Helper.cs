using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using log4net;
using AutosarBCM.Properties;
using System.Linq;
using AutosarBCM.Config;
using System.ComponentModel;
using AutosarBCM.Core;
using static System.Windows.Forms.LinkLabel;

namespace AutosarBCM
{
    #region Helper

    /// <summary>
    /// Contains general usage helper methods.
    /// </summary>
    static class Helper
    {
        #region Properties

        /// <summary>
        /// GUI logger instance
        /// </summary>
        private static readonly ILog log = LogManager.GetLogger("GUI");

        /// <summary>
        /// Returns GUI logger
        /// </summary>
        public static ILog Logger
        {
            get { return log; }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Updates log4net log level
        /// </summary>
        /// <param name="level">"debug" for DEBUG. INFO otherwise</param>
        public static void UpdateLog4netLevel(string level)
        {
            var hierarchy = (log4net.Repository.Hierarchy.Hierarchy)log4net.LogManager.GetRepository();
            hierarchy.Root.Level = (level == "debug") ? log4net.Core.Level.Debug : log4net.Core.Level.Info;
        }

        /// <summary>
        /// Shows appropriate error msg to user.
        /// </summary>
        /// <param name="ex">exception</param>
        /// <param name="initialMsg">The descriptive text that will be displayed at the beginning of error text</param>
        public static void ShowErrorMessageBox(Exception ex, string initialMsg = null)
        {
            // initial text if specified
            string msg = string.Empty;
            if (initialMsg != null)
            {
                msg += initialMsg;
                if (!initialMsg.TrimEnd().EndsWith("."))
                    msg += ". ";
            }

            // append exception
            if (ex is ApplicationException)
            {
                msg += ex.Message;
                var innerEx = ex.InnerException;
                var level = 0;
                while (innerEx != null)
                {
                    if (!(innerEx is ApplicationException) && !(innerEx is System.IO.IOException))
                        break;

                    msg += "\n";
                    if (level == 0)
                        msg += ("\n" + Resources.Details + ":\n");
                    msg += string.Format("{0}- {1}", new string(' ', level), innerEx.Message);
                    innerEx = innerEx.InnerException;
                    ++level;
                }
            }
            else
                msg += string.Format("Unexpected error occured.\n{0}: {1}", Resources.Details, ex.Message);

            // show message
            MessageBox.Show(msg, Resources.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Shows error message to user.
        /// </summary>
        /// <param name="msg">text to display</param>
        /// <param name="c">control to invoke GUI thread</param>
        public static void ShowErrorMessageBox(string msg, Control c = null)
        {
            if (c != null && c.InvokeRequired)
            {
                c.Invoke(new Action<string, Control>(ShowErrorMessageBox), msg, null);
                return;
            }
            MessageBox.Show(msg, Resources.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Shows warning message to user.
        /// </summary>
        /// <param name="msg">text to display</param>
        /// <param name="c">control to invoke GUI thread</param>
        public static void ShowWarningMessageBox(string msg, Control c = null)
        {
            if (c != null && c.InvokeRequired)
            {
                c.Invoke(new Action<string, Control>(ShowWarningMessageBox), msg, null);
                return;
            }
            MessageBox.Show(msg, Resources.Warning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// Shows confirmation message to user and returns reply of user.
        /// Returns true if user clicks yes.
        /// </summary>
        /// <param name="msg">text to display</param>
        /// <param name="c">control to invoke GUI thread</param>
        /// <returns>true if confirmed by user</returns>
        public static bool ShowConfirmationMessageBox(string msg, Control c = null)
        {
            if (c != null && c.InvokeRequired)
            {
                return (bool)c.Invoke(new Func<string, Control, bool>(ShowConfirmationMessageBox), msg, null);
            }
            var dialogResult = MessageBox.Show(msg, Resources.Confirmation,
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            return (dialogResult == DialogResult.Yes);
        }

        /// <summary>
        /// Shows yes-no-cancel message box to user and returns result
        /// </summary>
        /// <param name="msg">text to display</param>
        /// <param name="defaultButton">default button</param>
        /// <returns>result</returns>
        public static DialogResult ShowYesNoCancelMessageBox(string msg, MessageBoxDefaultButton defaultButton = MessageBoxDefaultButton.Button2)
        {
            return MessageBox.Show(msg, Resources.Confirmation,
                MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, defaultButton);
        }

        /// <summary>
        /// Creates an icon with the given bitmap instance
        /// </summary>
        /// <param name="bmp">bitmap [Not Null]</param>
        /// <returns>icon</returns>
        public static System.Drawing.Icon GetIconFromImage(System.Drawing.Bitmap bmp)
        {
            if (bmp == null)
                throw new ArgumentNullException("bmp");
            return System.Drawing.Icon.FromHandle(bmp.GetHicon());
        }

        /// <summary>
        /// Returns the application version in "Tool X.X.X" format.
        /// </summary>
        public static string AppVersionText
        {
            get
            {
                try
                {
                    string productName = "";
                    object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                    if (attributes.Length != 0)
                        productName = ((AssemblyTitleAttribute)attributes[0]).Title;

                    var version = Assembly.GetExecutingAssembly().GetName().Version;
                    return string.Format("{0} {1}.{2}.{3} {4}", productName, version.Major, version.Minor, version.Build, "Beta-9");
                }
                catch { }
                return "";
            }
        }

        /// <summary>
        /// Returns the application version in "Release X.X.X - build date" format.
        /// </summary>
        public static string AppVersionFullText
        {
            get
            {
                try
                {
                    var buildDate = new FileInfo(Assembly.GetExecutingAssembly().Location).LastWriteTime;
                    var version = Assembly.GetExecutingAssembly().GetName().Version;
                    return string.Format("Release {0}.{1}.{2} - build {3}", version.Major,
                        version.Minor, version.Build, buildDate.ToShortDateString());
                }
                catch
                {
                    return "";
                }
            }
        }

        /// <summary>
        /// Shortens given path, by inserting ... into the middle of path.
        /// </summary>
        /// <param name="longPath">original file path</param>
        /// <param name="maxLength">maximum length</param>
        /// <returns>short file path</returns>
        public static string ToShortPath(string longPath, int maxLength = 60)
        {
            if (longPath == null || longPath.Length <= maxLength)
                return longPath;

            try
            {
                var strBuilder = new StringBuilder();
                for (int i = longPath.Length - 1; i >= 0; i--)
                {
                    var ch = longPath[i];
                    strBuilder.Insert(0, ch);
                    if (ch == '\\' && strBuilder.Length > maxLength - 4)
                        break;
                }

                strBuilder.Insert(0, "...");
                strBuilder.Insert(0, longPath.Substring(0, 3));

                return strBuilder.ToString();
            }
            catch
            {
                return longPath;
            }
        }

        /// <summary>
        /// Checks if a character is in a hexadecimal format
        /// </summary>
        /// <param name="ch">The char to be checked</param>
        /// <returns>true if the char is a hexadecimal char; otherwise, false.</returns>
        public static bool IsHexadecimal(char ch)
        {
            return (ch >= '0' && ch <= '9') || (ch >= 'A' && ch <= 'F') || (ch >= 'a' && ch <= 'f') || (ch == (char)Keys.Back);
        }

        /// <summary>
        /// Converts hexadecimal to ByteArray
        /// </summary>
        /// <param name="hex">hex string</param>
        /// <returns>byte array</returns>
        public static byte[] StringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }

        /// <summary>
        /// Converts ByteArray to hexadecimal string
        /// </summary>
        /// <param name="hex">hex string</param>
        /// <returns>byte array</returns>
        public static string ByteArrayToString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }

        /// <summary>
        /// Converts byte array to value( short, int )
        /// </summary>
        /// <param name="rawMessage">byte array</param>
        /// <param name="index">int</param>
        /// <param name="size">int</param>
        /// <returns>object data</returns>
        public static object GetValueOfPrimitive(byte[] rawMessage, int index, int size)
        {
            try
            {
                if (rawMessage.Length < index + size)
                {
                    return 0;
                }

                byte[] tmpArray = new byte[size];
                Array.Copy(rawMessage, index, tmpArray, 0, size);
                Array.Reverse(tmpArray);
                switch (size)
                {
                    case 1:
                        return tmpArray[0];
                    case 2:
                        return (short)BitConverter.ToInt16(tmpArray, 0);
                    case 3:
                        return (int)BitConverter.ToInt32(tmpArray, 0);
                    case 4:
                        return CombineHexData(tmpArray);
                    default:
                        return 0;
                }
            }
            catch (Exception e)
            {
                ShowErrorMessageBox($"{e.Message}");
                return 0;
            }
        }

        /// <summary>
        /// Converts hex byte array to decimal value
        /// </summary>
        /// <param name="hexData">byte array</param>
        /// <returns>ulong</returns>
        private static ulong CombineHexData(byte[] hexData)
        {
            int length = hexData.Length;
            ulong result = 0;
            int multiplier = 16;
            int degree = 0;

            for (int i = 0; i < length; i++)
            {
                result += hexData[i] * (ulong)Math.Pow(multiplier, degree);

                degree += 2;
            }

            return result;
        }

        /// <summary>
        /// Applies a filter and updates the visibility in the DataGridView.
        /// </summary>
        /// <param name="dgvMessages">The DataGridView object to which the filter is to be applied.</param>
        /// <param name="currentFilter">The text of the filter to be applied.</param>
        internal static void ApplyFilter(DataGridView dgvMessages, string currentFilter)
        {
            CurrencyManager currencyManager = (CurrencyManager)dgvMessages.BindingContext[dgvMessages.DataSource];
            currencyManager.SuspendBinding();

            foreach (DataGridViewRow row in dgvMessages.Rows)
            {
                if (!row.IsNewRow)
                {
                    bool isVisible = row.Cells.Cast<DataGridViewCell>()
                        .Any(cell => cell.Value != null && cell.Value.ToString().ToLower().Contains(currentFilter));

                    row.Visible = isVisible;
                }
            }

            currencyManager.ResumeBinding();
        }

        /// <summary>
        /// Applies a filter, saves the selection, and updates the visibility in the DataGridView. 
        /// After the operation, it reapplies the saved selection in the DataGridView.
        /// </summary>
        /// <param name="dgvMessages">The DataGridView object to which the filter is to be applied and where the selection will be reapplied.</param>
        /// <param name="currentFilter">The text of the filter to be applied.</param>
        internal static void ApplyFilterAndRestoreSelection(DataGridView dgvMessages, string currentFilter)
        {
            try
            {
                if (currentFilter == String.Empty)
                    return;

                int? selectedRowIndex = dgvMessages.CurrentRow?.Index;

                Helper.ApplyFilter(dgvMessages, currentFilter);

                if (selectedRowIndex.HasValue && selectedRowIndex.Value < dgvMessages.Rows.Count)
                {
                    dgvMessages.CurrentCell = dgvMessages.Rows[selectedRowIndex.Value].Cells[0];
                }
            }
            catch
            {
                dgvMessages.ClearSelection();
            }
        }
        /// <summary>
        /// Create a txt file to the unopened DIDS during an environmental test.
        /// </summary>
        /// <param name="count">The name of the item.</param>
        /// <param name="payloadName">The type of the item.</param>
        public static void WriteUnopenedPayloadsToLogFile(string payloadName, string controlName, int count, int rangeCount)
        {
            string logFilePath = $"{DateTime.Now.ToString("dd-MM-yyyy")}_Unopened_Payloads_log.txt";
            string groupName = $"{count}. Group";
            string rangeHeader = $"{groupName} (Range: {rangeCount}) Started -- ({DateTime.Now.ToString("dd/MM_HH:mm:ss")}) {System.Environment.NewLine}";
            string logMessage = $"Control: {controlName} - Payload: {payloadName}";

            List<string> lines = new List<string>();

            if (File.Exists(logFilePath))
            {
                lines = File.ReadAllLines(logFilePath).ToList();
            }

            bool rangeHeaderExists = lines.Any(line => line.Contains(groupName));
            if (!rangeHeaderExists)
            {
                if(count != 1)
                    lines.Add($"{count-1}. Group Finished -- ({DateTime.Now.ToString("dd/MM_HH:mm:ss")}) {System.Environment.NewLine}");
                lines.Add(rangeHeader);
            }
            int rangeHeaderIndex = lines.FindIndex(line => line.StartsWith(rangeHeader.Trim()));
            if (rangeHeaderIndex != -1)
            {
                bool logMessageExists = lines.Skip(rangeHeaderIndex + 1).TakeWhile(line => !line.Contains("Range")).Any(line => line.Contains(logMessage.Trim()));
                if (!logMessageExists)
                    lines.Add(logMessage);
                File.WriteAllLines(logFilePath, lines);
            }

        }



        /// <summary>
        /// Writes a cycle message to the log file during an environmental test.
        /// </summary>
        /// <param name="itemName">The name of the item.</param>
        /// <param name="itemType">The type of the item.</param>
        /// <param name="operation">The operation performed.</param>
        /// <param name="comment">Optional comment.</param>
        /// <param name="escapeChars">Optional escape characters for formatting.</param>
        /// <param name="data">Additional data related to the message.</param>
        public static void WriteCycleMessageToLogFile(string itemName, string itemType, string operation, string comment = "", string escapeChars = "", string data = "")
        {
            if (FormMain.IsTestRunning)
            {
                if (String.IsNullOrEmpty(comment))
                    ((FormMain)Application.OpenForms[Constants.Form_Main]).LogCycleMessageQueue.Enqueue($"{escapeChars}{DateTime.Now.ToString("HH:mm:ss.fff\t")};{itemName};{itemType};{operation};{data};{escapeChars}");
                else
                    ((FormMain)Application.OpenForms[Constants.Form_Main]).LogCycleMessageQueue.Enqueue($"{escapeChars}#{DateTime.Now.ToString("HH:mm:ss.fff\t")}#{comment}#{escapeChars}");
            }
        }

        /// <summary>
        /// Writes an error message to the log file during an environmental test.
        /// </summary>
        /// <param name="itemName">The name of the item.</param>
        /// <param name="itemType">The type of the item.</param>
        /// <param name="operation">The operation that caused the error.</param>
        /// <param name="comment">Optional comment.</param>
        /// <param name="escapeChars">Optional escape characters for formatting.</param>
        /// <param name="data">Additional data related to the error.</param>
        public static void WriteErrorMessageToLogFile(string itemName, string itemType, string operation, string comment = "", string escapeChars = "", string data = "", string cycleId = "", string loopId = "")
        {
            if (FormMain.IsTestRunning && FormMain.MonitorTestTypeClone == MonitorTestType.Environmental)
            {
                if (String.IsNullOrEmpty(comment))
                    ((FormMain)Application.OpenForms[Constants.Form_Main]).LogErrorMessageQueue.Enqueue($"{escapeChars}{DateTime.Now.ToString("HH:mm:ss.fff\t")} [{cycleId}-{loopId}];{itemName};{itemType};{operation};{data};{escapeChars}");
                else
                    ((FormMain)Application.OpenForms[Constants.Form_Main]).LogErrorMessageQueue.Enqueue($"{escapeChars}#{comment}#{escapeChars}");
            }
        }

        /// <summary>
        /// Returns a enum value with using name
        /// </summary>
        /// <param name="name">Enum name</param>
        public static T GetEnumValue<T>(string name)
        {
            T retVal;

            try
            {
                retVal = (T)Enum.Parse(typeof(T), name);
            }
            catch
            {
                retVal = default(T);
            }
            return retVal;
        }

        /// <summary>
        /// Returns a List of grouped Lists
        /// </summary>
        /// <param name="sourceList">List to be group</param>
        /// <param name="groupSize">size of group</param>
        public static List<List<T>> GroupList<T>(List<T> sourceList, int groupSize)
        {
            return sourceList
                .Select((value, index) => new { Index = index, Value = value })
                .GroupBy(x => x.Index % groupSize)
                .Select(g => g.Select(x => x.Value).ToList())
                .ToList();
        }

        /// <summary>
        /// Exports the data in the specified DataGridView to a CSV file.
        /// </summary>
        /// <param name="grid">The DataGridView to be exported.</param>
        internal static void ExportToCSV(DataGridView grid)
        {
            if (grid.Rows.Count == 0) return;

            StringBuilder sb = new StringBuilder();
            var delimiter = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator;

            ExportToCSV(grid.Columns, grid.Rows.OfType<DataGridViewRow>().ToList());
        }

        internal static void ExportToCSV(DataGridViewColumnCollection columns, List<DataGridViewRow> rows)
        {
            StringBuilder sb = new StringBuilder();
            var delimiter = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator;

            foreach (DataGridViewColumn column in columns)
                sb.Append($"{column.HeaderText}{delimiter}");

            sb.AppendLine();
            foreach (DataGridViewRow row in rows)
                sb.AppendLine(row.Cells.OfType<DataGridViewCell>().Aggregate(new StringBuilder(), (x, y) => x.Append($"{y.Value}{delimiter}")).ToString());

            using (var dialog = new SaveFileDialog())
            {
                dialog.Filter = "CSV Files (.csv)|*.csv|TXT Files (.txt)|*.txt";
                if (dialog.ShowDialog() == DialogResult.OK)
                    System.IO.File.WriteAllText(dialog.FileName, sb.ToString());
                Helper.ShowWarningMessageBox("The Operation Completed Successfully");
            }
        }

        public static void SendExtendedDiagSession()
        {
            if (!ConnectionUtil.CheckConnection())
                return;
            if (ASContext.Configuration != null)
            {
                var sessionInfo = (SessionInfo)ASContext.Configuration.Sessions.FirstOrDefault(x => x.Name == "Extended Diagnostic Session");
                ASContext.CurrentSession = sessionInfo;
                new DiagnosticSessionControl().Transmit(sessionInfo);
            }
            else
            {
                return;
            }
        }

        #endregion
    }

    #endregion

    #region Control Helper

    /// <summary>
    /// Handles custom controls for specific output controls
    /// </summary>
    public class ControlHelper
    {
        /// <summary>
        /// Gets custom controls for all output controls
        /// </summary>
        /// <param name="config">A reference to the monitor configuration instance</param>
        /// <returns>A list of custom controls for all output controls</returns>
        internal static List<ControlInfoCC> GetControlsExtended(AutosarBcmConfiguration config)
        {
            var mapping = GetInOutMapping(config);
            var controls = new List<ControlInfoCC>();

            foreach (var output in config.GenericMonitorConfiguration.OutputSection.Groups.SelectMany(x => x.OutputItemList))
                controls.AddRange(GetControls(output, config));

            foreach (var output in controls)
                if (mapping.TryGetValue(output.Name, out InputMonitorItem input))
                    output.CorrespondingInput = input;

            return controls;
        }

        /// <summary>
        /// Gets a dictionary that maps output controls to their corresponding input controls
        /// </summary>
        /// <param name="config">A reference to the monitor configuration instance</param>
        /// <returns>A dictionary of output controls along with their corresponding Input controls</returns>
        private static Dictionary<string, InputMonitorItem> GetInOutMapping(AutosarBcmConfiguration config)
        {
            return config.EnvironmentalMonitorConfiguration.MappingSection.ConnectionMappings.Select(x =>
                            new KeyValuePair<string, InputMonitorItem>(x.OutputName, config.GenericMonitorConfiguration.InputSection.Groups.SelectMany(y =>
                                y.InputItemList).Where(z => z.Name == x.InputName).FirstOrDefault()))
                            .ToDictionary(x => x.Key, y => y.Value);
        }

        /// <summary>
        /// Gets custom controls for a specific output item
        /// </summary>
        /// <param name="output">A reference to the output item as an OutputMonitorItem</param>
        /// <param name="config">A reference to the monitor configuration instance</param>
        /// <returns>A list of ControlInfo instances representing the custom controls for the specified output item</returns>
        private static List<ControlInfoCC> GetControls(OutputMonitorItem output, AutosarBcmConfiguration config)
        {
            var result = new List<ControlInfoCC>();

            if (output.ItemType == "Digital" && output.PwmTag != "XS4200")
                result.Add(new ControlInfoCC(output, output.ReadDiagData, output.ReadADCData, output.ReadCurrentData)
                {
                    Open = output.OpenData,
                    Close = output.CloseData,
                });
            else if (output.ItemType == "PWM" || (output.ItemType == "Digital" && output.PwmTag == "XS4200"))
                result.Add(new ControlInfoCC(output, output.ReadDiagData, output.ReadADCData, output.ReadCurrentData)
                {
                    Open = output.GetPWMMessage(config.EnvironmentalMonitorConfiguration.OutputSection.CommonConfig.PWMDutyOpenValue, config.EnvironmentalMonitorConfiguration.OutputSection.CommonConfig.PWMFreqOpenValue),
                    Close = output.GetPWMMessage(config.EnvironmentalMonitorConfiguration.OutputSection.CommonConfig.PWMDutyCloseValue, config.EnvironmentalMonitorConfiguration.OutputSection.CommonConfig.PWMFreqCloseValue),
                });
            else if (output.ItemType == "DoorControl")
                result.AddRange(new[] {
                    new ControlInfoCC(output, output.DoorControl.ReadDoorLockDiag, output.ReadADCData, output.ReadCurrentData)
                    {
                        Open = output.DoorControl.DoorLockEnable,
                        Close = output.DoorControl.DoorLockDisable,

                        Name = output.Name.Replace("_", "_LOCK_")
                    },
                    new ControlInfoCC(output, output.DoorControl.ReadDoorUnlockDiag, output.ReadADCData, output.ReadCurrentData)
                    {
                        Open = output.DoorControl.DoorUnlockEnable,
                        Close = output.DoorControl.DoorUnlockDisable,

                        Name = output.Name.Replace("_", "_UNLOCK_")
                    }});
            else if (output.ItemType == "Wiper")
                result.AddRange(new[] {
                    new ControlInfoCC(output, output.ReadDiagData, output.ReadADCData, output.ReadCurrentData)
                    {
                        Open = output.WiperCase.StopLow,
                        Close = output.WiperCase.LowStop,

                        Name = "Set_Wiper_Case_Stop_2_Low"
                    },
                    new ControlInfoCC(output, output.ReadDiagData, output.ReadADCData, output.ReadCurrentData)
                    {
                        Open = output.WiperCase.StopLow,
                        Close = output.WiperCase.LowStop,

                        Name = "Set_Wiper_Case_Low_2_Stop"
                    },
                    new ControlInfoCC(output, output.ReadDiagData, output.ReadADCData, output.ReadCurrentData)
                    {
                        Open = output.WiperCase.StopHigh,
                        Close = output.WiperCase.HighStop,

                        Name = "Set_Wiper_Case_Stop_2_High"
                    },
                    new ControlInfoCC(output, output.ReadDiagData, output.ReadADCData, output.ReadCurrentData)
                    {
                        Open = output.WiperCase.StopHigh,
                        Close = output.WiperCase.HighStop,

                        Name = "Set_Wiper_Case_High_2_Stop"
                    }});
            else if (output.ItemType == "Power Mirror")
                result.AddRange(new[] {
                    new ControlInfoCC(output, output.PowerMirror.ReadUp, null, null)
                    {
                        Open = output.PowerMirror.SetOpenUp,
                        Close = output.PowerMirror.SetCloseUp,

                        Name = $"{output.Name}_UP"
                    },
                    new ControlInfoCC(output, output.PowerMirror.ReadLeftDown, null, null)
                    {
                        Open = output.PowerMirror.SetOpenDown,
                        Close = output.PowerMirror.SetCloseDown,

                        Name = $"{output.Name}_DOWN"
                    },
                    new ControlInfoCC(output, output.PowerMirror.ReadLeftDown, null, null)
                    {
                        Open = output.PowerMirror.SetOpenLeft,
                        Close = output.PowerMirror.SetCloseLeft,

                        Name = $"{output.Name}_LEFT"
                    },
                    new ControlInfoCC(output, output.PowerMirror.ReadRight, null, null)
                    {
                        Open = output.PowerMirror.SetOpenRight,
                        Close = output.PowerMirror.SetCloseRight,

                        Name = $"{output.Name}_RIGHT"
                    }});
            else if (output.ItemType == "Power Window")
                result.AddRange(new[] {
                    new ControlInfoCC(output, output.PowerWindow.ReadUpDiagData, output.ReadADCData, output.ReadCurrentData)
                    {
                        Open = output.PowerWindow.EnableUpData,
                        Close = output.PowerWindow.DisableUpData,

                        Name = $"{output.Name}_UP"
                    },
                    new ControlInfoCC(output, output.PowerWindow.ReadDownDiagData, output.ReadADCData, output.ReadCurrentData)
                    {
                        Open = output.PowerWindow.EnableDownData,
                        Close = output.PowerWindow.DisableDownData,

                        Name = $"{output.Name}_DOWN"
                    }});
            else if (output.ItemType == "Sunroof")
                result.AddRange(new[] {
                    new ControlInfoCC(output, output.Sunroof.ReadOpenDiagData, output.ReadADCData, output.ReadCurrentData)
                    {
                        Open = output.Sunroof.EnableOpenData,
                        Close = output.Sunroof.DisableOpenData,

                        Name = $"{output.Name}_OPEN"
                    },
                    new ControlInfoCC(output, output.Sunroof.ReadCloseDiagData, output.ReadADCData, output.ReadCurrentData)
                    {
                        Open = output.Sunroof.EnableCloseData,
                        Close = output.Sunroof.DisableCloseData,

                        Name = $"{output.Name}_CLOSE"
                    }});

            return result;
        }
    }

    /// <summary>
    /// Represents a custom control for a specific output item.
    /// </summary>
    public class ControlInfoCC
    {
        /// <summary>
        /// Gets or sets the output item as an OutputMonitorItem.
        /// </summary>
        public OutputMonitorItem Output { get; set; }
        /// <summary>
        /// Gets or sets the corresponding input item as an InputMonitorItem.
        /// </summary>
        public InputMonitorItem CorrespondingInput { get; set; }
        /// <summary>
        /// Gets or sets the name of the control.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the open data as a byte array.
        /// </summary>
        public byte[] Open { get; set; }
        /// <summary>
        /// Gets or sets the close data as a byte array.
        /// </summary>
        public byte[] Close { get; set; }
        /// <summary>
        /// Gets or sets the diagnostic data as a byte array.
        /// </summary>
        public byte[] Diag { get; set; }
        /// <summary>
        /// Gets or sets the ADC data as a byte array.
        /// </summary>
        public byte[] ADC { get; set; }
        /// <summary>
        /// Gets or sets the current data as a byte array.
        /// </summary>
        public byte[] Current { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether the control is closing.
        /// </summary>
        public bool Closing { get; set; }

        /// <summary>
        /// Initializes a new instance of the ControlInfo class.
        /// </summary>
        /// <param name="output">A reference to the output item as an OutputMonitorItem.</param>
        /// <param name="diag">The diagnostic data as a byte array.</param>
        /// <param name="adc">The ADC data as a byte array.</param>
        /// <param name="current">The current data as a byte array.</param>
        public ControlInfoCC(OutputMonitorItem output, byte[] diag, byte[] adc, byte[] current)
        {
            Output = output;
            Diag = diag;
            ADC = adc;
            Current = current;

            Name = output.Name;
        }

        /// <summary>
        /// Determines whether the specified response is a diagnostic response
        /// </summary>
        /// <param name="response">A reference to the response to be checked</param>
        /// <returns>True if the response is a diagnostic response; otherwise, false</returns>
        public bool IsDiagResponse(Response response)
        {
            return Diag.Skip(2).Take(3).SequenceEqual(response.RawData.Skip(4).Take(3));
        }

        /// <summary>
        /// Determines whether the specified response is an ADC response
        /// </summary>
        /// <param name="response">A reference to the response to be checked</param>
        /// <returns>True if the response is an ADC response; otherwise, false</returns>
        public bool IsADCResponse(Response response)
        {
            return ADC.Skip(2).Take(3).SequenceEqual(response.RawData.Skip(4).Take(3));
        }

        /// <summary>
        /// Determines whether the specified response is a current response
        /// </summary>
        /// <param name="response">A reference to the response to be checked</param>
        /// <returns>True if the response is a current response; otherwise, false</returns>
        internal bool IsCurrentResponse(GenericResponse response)
        {
            return Current.Skip(2).Take(3).SequenceEqual(response.RawData.Skip(4).Take(3));
        }

        /// <summary>
        /// Determines whether the specified response is an open response
        /// </summary>
        /// <param name="response">A reference to the response to be checked</param>
        /// <returns>True if the response is an open response; otherwise, false</returns>
        public bool IsOpenResponse(Response response)
        {
            return Open.Skip(2).Take(3).SequenceEqual(response.RawData.Skip(4).Take(3));
        }

        /// <summary>
        /// Determines whether the specified response is a close response
        /// </summary>
        /// <param name="response">A reference to the response to be checked</param>
        /// <returns>True if the response is a close response; otherwise, false</returns>
        public bool IsCloseResponse(Response response)
        {
            return Close.Skip(2).Take(3).SequenceEqual(response.RawData.Skip(4).Take(3));
        }

        /// <summary>
        /// Determines whether the specified response is an input response
        /// </summary>
        /// <param name="response">A reference to the response to be checked</param>
        /// <returns>True if the response is an input response; otherwise, false</returns>
        public bool IsInputResponse(Response response)
        {
            return CorrespondingInput?.Data.Skip(2).Take(3).SequenceEqual(response.RawData.Skip(4).Take(3)) ?? false;
        }
    }

    #endregion

    #region RecentFileHelper

    /// <summary>
    /// Represents helper class for managing recent files.
    /// </summary>
    class RecentFileHelper
    {
        #region Properties

        /// <summary>
        /// Maximum number of recent files.
        /// </summary>
        private const int recentFileCount = 10;

        /// <summary>
        /// Notifies that user clicked a recent file. Argument is file path.
        /// </summary>
        public event EventHandler<string> MenuClick;

        /// <summary>
        /// Notifies that recent file list is updated (Initiated, added or removed).
        /// </summary>
        public event EventHandler<string> ListUpdated;

        /// <summary>
        /// List of recent files. They are stored internally.
        /// </summary>
        private List<string> recentFiles = new List<string>();

        #endregion

        #region Public Methods

        /// <summary>
        /// Initiates the recent files.
        /// </summary>
        /// <param name="recentFilesText">List of recent files, separated by ; character.</param>
        public void Init(string recentFilesText)
        {
            var files = new List<string>();

            try
            {
                var splited = recentFilesText.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                files.AddRange(splited);
                while (files.Count > recentFileCount)
                    files.RemoveAt(files.Count - 1);
            }
            catch
            {
            }

            recentFiles = files;
            OnListUpdated(); // notify
        }

        /// <summary>
        /// Updates recent files menu with the current recent files.
        /// </summary>
        /// <param name="menuItem">menu item to fill</param>
        public void UpdateRecentFilesMenu(ToolStripDropDownItem menuItem)
        {
            #region shortcut click
            EventHandler shortcutClick = (sender, args) =>
            {
                var subMenuItem = (ToolStripMenuItem)sender;
                var fileName = (string)subMenuItem.Tag;
                if (string.IsNullOrEmpty(fileName))
                    return;
                // check that file exists
                if (!File.Exists(fileName))
                {
                    // TODO: localize
                    if (Helper.ShowConfirmationMessageBox("The item '" + Path.GetFileName(fileName) +
                                                   "' that this shortcut refers to has been changed or moved, so this shortcut will no longer work properly." +
                                                   System.Environment.NewLine + System.Environment.NewLine +
                                                   "Do you want to delete this shortcut?"))
                    {
                        // remove shortcut
                        RemoveFromRecentFiles(fileName);
                    }
                }
                else
                {
                    var handler = MenuClick;
                    if (handler != null)
                        handler(this, fileName);

                    OpenRecentFile(fileName);
                }
            };
            #endregion

            var files = recentFiles;

            #region prepare menu items
            for (int i = 0; i < recentFileCount; i++)
            {
                var key = "recent" + i;
                if (files.Count <= i || string.IsNullOrEmpty(files[i]))
                {
                    if (menuItem.DropDownItems.ContainsKey(key))
                        menuItem.DropDownItems.RemoveByKey(key);
                }
                else
                {
                    var fileName = files[i];
                    // add menu item if needed
                    ToolStripItem subMenuItem;
                    if (!menuItem.DropDownItems.ContainsKey(key))
                    {
                        subMenuItem = menuItem.DropDownItems.Add(fileName);
                        subMenuItem.Name = key;
                        subMenuItem.Click += shortcutClick;
                    }
                    else
                        subMenuItem = menuItem.DropDownItems[key];
                    if (subMenuItem.Tag as string != fileName)
                    {
                        subMenuItem.Tag = fileName;
                        subMenuItem.Text = Helper.ToShortPath(fileName);
                    }
                }
            }
            #endregion

            if (menuItem.DropDownItems.Count > files.Count)
            {
                // hide the separator above if no recent file
                var lastDropDownItem = menuItem.DropDownItems[menuItem.DropDownItems.Count - 1];
                if (lastDropDownItem is ToolStripSeparator)
                    lastDropDownItem.Visible = !(files.Count == 0 || files.TrueForAll(string.IsNullOrEmpty));
            }
            else
            {
                // disable the menu item if no recent file
                if (!(menuItem is ToolStripSplitButton))
                    menuItem.Enabled = (files.Count != 0);
            }
        }

        /// <summary>
        /// Returns the most recent file name. Null if not existed.
        /// </summary>
        /// <returns>most recent file name</returns>
        public string GetMostRecentFile()
        {
            if (recentFiles != null && recentFiles.Count > 0)
                return recentFiles[0];
            return null;
        }

        /// <summary>
        /// Appends given file name to the top of recent files.
        /// </summary>
        /// <param name="fileName">file name to be added</param>
        public void AddToRecentFiles(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                return;

            // add the file to the first position
            if (recentFiles.Contains(fileName))
                recentFiles.Remove(fileName);
            else if (recentFiles.Count > recentFileCount)
                recentFiles.RemoveAt(recentFiles.Count - 1);
            recentFiles.Insert(0, fileName);

            OnListUpdated();    // notify
        }

        /// <summary>
        /// Removes given file from recent file list.
        /// </summary>
        /// <param name="fileName">file name to remove</param>
        public void RemoveFromRecentFiles(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                return;

            var index = recentFiles.IndexOf(fileName);
            if (index != -1)
            {
                recentFiles.RemoveAt(index);
                OnListUpdated();    // notify
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// SubMenu Click for Opening Recent File
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        private void OpenRecentFile(string fileName)
        {
            FormMain formMain = (FormMain)Application.OpenForms[Constants.Form_Main];
            formMain.ParseMessages(fileName);
            // add to recent file list
            AddToRecentFiles(fileName);
        }

        /// <summary>
        /// Notifies parent control that the recent file list is updated.
        /// </summary>
        private void OnListUpdated()
        {
            var strBuilder = new StringBuilder();
            foreach (var item in recentFiles)
            {
                strBuilder.Append(item);
                strBuilder.Append(';');
            }

            var handler = ListUpdated;
            if (handler != null)
                handler(this, strBuilder.ToString());
        }

        #endregion
    }

    #endregion

    #region CSV Import Export Helper

    /// <summary>
    /// Static class providing methods for CSV file operations.
    /// </summary>
    static class CsvHelper
    {
        #region Properties

        /// <summary>
        /// OpenFileDialog instance for selecting CSV files to import.
        /// </summary>
        private static OpenFileDialog openFileDialog = new OpenFileDialog();

        /// <summary>
        /// SaveFileDialog instance for saving CSV templates.
        /// </summary>
        private static SaveFileDialog saveFileDialog = new SaveFileDialog();

        #endregion

        #region Public Methods

        /// <summary>
        /// Initializes the CsvHelper class, setting up file dialog properties.
        /// </summary>
        public static void PrepareCsvHelper()
        {
            saveFileDialog.InitialDirectory = openFileDialog.InitialDirectory = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop);
            saveFileDialog.Filter = openFileDialog.Filter = "CSV File| *.csv";
            openFileDialog.Multiselect = false;
            openFileDialog.Title = "Please select a CSV file.";

            saveFileDialog.DefaultExt = "csv";
            saveFileDialog.Title = "Save a Csv template.";
        }

        public static object ImportCsvFile(TransmitProtocol protocol)
        {
            try
            {
                using (openFileDialog)
                {
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        switch (protocol)
                        {
                            case TransmitProtocol.Can:
                                return CanMessageCsvParser(File.ReadAllLines(openFileDialog.FileName)
                                               .Skip(1)
                                               .ToList());
                            case TransmitProtocol.Uds:
                                return UdsMessageCsvParser(File.ReadAllLines(openFileDialog.FileName)
                                               .Skip(1)
                                               .ToList());
                            default: return null;
                        }
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                Helper.ShowWarningMessageBox("Unexpected CSV File.\nPlease check CSV File.");
                return null;
            }
        }
        public static void DownloadCsvFile(TransmitProtocol protocol, BindingList<CanMessage> bindingList)
        {
            using (saveFileDialog)
            {
                saveFileDialog.Filter = "CSV files (*.csv)|*.csv";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    switch (protocol)
                    {
                        case TransmitProtocol.Can:
                            var csv = new StringBuilder();
                            bool isPrevSingle = false;
                            bool isPrevMulti = false;
                            foreach (CanMessage message in bindingList)
                            {
                                var newLine = "";
                                var subMesages = message.SubMessages;
                                var first = message.Id;
                                var dataBytes = message.Data;
                                var second = "";
                                foreach (byte data in dataBytes)
                                {
                                    second += data + " ";
                                }
                                second = second.TrimEnd();
                                var third = message.CycleTime;
                                var fourth = message.CycleCount;
                                var fifth = message.DelayTime;
                                var sixth = message.Count;
                                var seventh = message.Trigger;
                                var eighth = message.Comment;
                                var ninth = message.Multi;
                                if (!isPrevSingle && !ninth)
                                {
                                    csv.AppendLine("MessageID; Data; Cycle Time; Cycle Count; Delay Time; Count; Trigger; Comment; Multi Message");
                                    isPrevSingle = true;
                                    newLine = string.Format("{0};{1};{2};{3};{4};{5};{6};{7};{8}", first, second, third, fourth, fifth, sixth, seventh, eighth, ninth.ToString().ToUpper());
                                    csv.AppendLine(newLine);
                                }
                                else if (ninth)
                                {
                                    isPrevSingle = false;
                                }
                                if (!isPrevMulti && ninth)
                                {
                                    csv.AppendLine("-;Multi Messages;x;x;x;x;x;Text;");
                                    foreach (BaseMessage subMessage in subMesages)
                                    {
                                        first = subMessage.Id;
                                        dataBytes = subMessage.Data;
                                        second = "";
                                        foreach (byte data in dataBytes)
                                        {
                                            second += data + " ";
                                        }
                                        second = second.TrimEnd();
                                        third = subMessage.CycleTime;
                                        fourth = subMessage.CycleCount;
                                        fifth = subMessage.DelayTime;
                                        sixth = subMessage.Count;
                                        seventh = subMessage.Trigger;
                                        eighth = subMessage.Comment;
                                        ninth = subMessage.Multi;
                                        ninth = true;
                                        newLine = string.Format("{0};{1};{2};{3};{4};{5};{6};{7};{8}", first, second, third, fourth, fifth, sixth, seventh, eighth, ninth.ToString().ToUpper());
                                        csv.AppendLine(newLine);
                                    }
                                    isPrevMulti = true;
                                }
                                else if (!ninth)
                                {
                                    isPrevMulti = false;
                                }
                            }
                            File.WriteAllText(saveFileDialog.FileName, csv.ToString());
                            break;
                        case TransmitProtocol.Uds:
                            File.WriteAllText(saveFileDialog.FileName, Resources.Uds_CSV_Template);
                            break;
                    }
                }
            }
        }
        #endregion

        #region Private Methods


        private static List<CanMessage> CanMessageCsvParser(List<string> lines)
        {
            var result = new List<CanMessage>();
            for (int i = 0; i < lines.Count; i++)
            {
                var line = lines[i].Replace("\",\"", ";").Split(';');
                if (line[0] == "-")
                {
                    var subMesages = new List<BaseMessage>();
                    for (int j = i + 1; j < lines.Count; j++, i = j - 1)
                    {
                        var subLine = lines[j].Replace("\",\"", ";").Split(';');
                        if (string.IsNullOrWhiteSpace(subLine[8]) ? false : bool.Parse(subLine[8]))
                        {
                            subLine[8] = "FALSE";
                            subMesages.Add(CanMessage.SetCanMessage(subLine));
                        }
                        else
                            break;
                    }
                    result.Add(new CanMessage() { Id = line[0], Multi = true, Comment = line[7], SubMessages = subMesages });
                }
                else
                {
                    result.Add(CanMessage.SetCanMessage(line));
                }
            }
            return result;
        }
        private static List<UdsMessage> UdsMessageCsvParser(List<string> lines)
        {
            var result = new List<UdsMessage>();
            for (int i = 0; i < lines.Count; i++)
            {
                var line = lines[i].Replace("\",\"", ";").Split(';');
                if (line[0] == "-")
                {
                    var subMesages = new List<BaseMessage>();
                    for (int j = i + 1; j < lines.Count; j++, i = j - 1)
                    {
                        var subLine = lines[j].Replace("\",\"", ";").Split(';');
                        if (string.IsNullOrWhiteSpace(subLine[8]) ? false : bool.Parse(subLine[8]))
                        {
                            subLine[8] = "FALSE";
                            subMesages.Add(UdsMessage.SetUdsMessage(subLine));
                        }
                        else
                            break;
                    }
                    result.Add(new UdsMessage() { Id = line[0], Multi = true, Comment = line[7], SubMessages = subMesages });
                }
                else
                {
                    result.Add(UdsMessage.SetUdsMessage(line));
                }
            }
            return result;
        }

        #endregion
    }

    #endregion

    #region MultiKey dictionary
    /// <summary> 
    /// Multi-Key Dictionary Class 
    /// </summary> 
    /// <typeparam name="TKey1">Primary Key Type</typeparam>
    /// <typeparam name="TKey2">Sub Key Type</typeparam>
    /// <typeparam name="TValue">Value Type</typeparam>
    class MultiKeyDictionary<TKey1, TKey2, TValue>
    {
        private readonly Dictionary<(TKey1, TKey2), TValue> dictionary = new Dictionary<(TKey1, TKey2), TValue>();
        private readonly Dictionary<TKey1, (TKey1, TKey2)> key1MatchDict = new Dictionary<TKey1, (TKey1, TKey2)>();
        private readonly Dictionary<TKey2, (TKey1, TKey2)> key2MatchDict = new Dictionary<TKey2, (TKey1, TKey2)>();

        private object locker = new object();
        /// <summary> 
        /// Adds the specified key and value to the dictionary.
        /// </summary> 
        /// <param name="key1">Primary Key</param>
        /// <param name="key2">Sub Key</param>
        /// <param name="value">Value</param>
        public void Add(TKey1 key1, TKey2 key2, TValue value)
        {
            lock (locker)
            {
                var key = (key1, key2);
                dictionary.Add(key, value);
                if (!key1MatchDict.ContainsKey(key1))
                    key1MatchDict.Add(key1, key);
                if (!key2MatchDict.ContainsKey(key2))
                    key2MatchDict.Add(key2, key);
            }
        }

        /// <summary> 
        /// Gets the value associated with the specified key.
        /// </summary> 
        /// <param name="key1">Primary Key</param>
        /// <param name="key2">Sub Key</param>
        /// <param name="value">Value</param>
        public bool TryGetValue(TKey1 key1, TKey2 key2, out TValue value)
        {
            var key = (key1, key2);

            return dictionary.TryGetValue(key, out value);
        }

        /// <summary> 
        /// Gets the value associated with the specified key.
        /// </summary> 
        /// <param name="key">Primary Key or Sub Key</param>
        /// <param name="value">Value</param>
        public bool TryGetValue(object key, out TValue value)
        {
            var match = GetMatch(key);
            return dictionary.TryGetValue(match, out value);
        }

        /// <summary> 
        /// Gets the key match associated with the specified key.
        /// </summary> 
        /// <param name="key">Primary Key or Sub Key</param>
        public (TKey1, TKey2) GetMatch(object key)
        {
            if (key1MatchDict.TryGetValue((TKey1)key, out (TKey1, TKey2) matching1))
                return matching1;
            else if (key2MatchDict.TryGetValue((TKey2)key, out (TKey1, TKey2) matching2))
                return matching2;


            return default;
        }

        /// <summary> 
        /// Determines whether the MultiKeyDictionary contains the specified
        /// </summary> 
        /// <param name="key">Primary Key or Sub Key</param>
        public bool ContainsKey(object key)
        {
            return key1MatchDict.ContainsKey((TKey1)key) || key2MatchDict.ContainsKey((TKey2)key);
        }

        /// <summary> 
        /// Updates the value with the specified key
        /// </summary> 
        /// <param name="key">Primary Key or Sub Key</param>
        /// <param name="newValue">New Value</param>
        public void UpdateValue(object key, TValue newValue)
        {
            if (ContainsKey(key))
            {
                lock (locker)
                    dictionary[GetMatch(key)] = newValue;
            }
        }

        /// <summary> 
        /// Removes the value with the specified key
        /// </summary> 
        /// <param name="key">Primary Key or Sub Key</param>
        public void Remove(object key)
        {
            if (!ContainsKey(key))
                return;

            var match = GetMatch(key);
            lock (locker)
            {
                dictionary.Remove(match);
                key1MatchDict.Remove(match.Item1);
                key2MatchDict.Remove(match.Item2);
            }
        }
    }

    public class ErrorLogDetectObject
    {
        public MappingResponse OutputResponse { get; set; } = MappingResponse.NOC;
        public MappingResponse InputResponse { get; set; } = MappingResponse.NOC;
        public MappingState OutputState { get; set; } = MappingState.NOC;
        public MappingState InputState { get; set; } = MappingState.NOC;
        public MappingOperation Operation { get; set; }

        /// <summary> 
        /// Updates the current output status of the mapping operation
        /// </summary> 
        /// <param name="operation">CurrentOperation</param>
        /// <param name="state">Message State</param>
        /// <param name="outputResponse">Message Response</param>
        public ErrorLogDetectObject UpdateOutputResponse(MappingOperation operation, MappingState state, MappingResponse outputResponse)
        {
            Operation = operation;
            OutputResponse = outputResponse;
            OutputState = state;
            return this;
        }

        /// <summary> 
        /// Updates the current input status of the mapping operation
        /// </summary> 
        /// <param name="state">Message State</param>
        /// <param name="inputResponse">Message Response</param>
        public ErrorLogDetectObject UpdateInputResponse(MappingState state, MappingResponse inputResponse)
        {
            InputResponse = inputResponse;
            InputState = state;
            return this;
        }

        /// <summary> 
        /// Returns the status of the Error statement
        /// </summary> 
        public bool CheckIsError()
        {
            if (OutputState == MappingState.OutputSent || InputState == MappingState.InputSent)
                return true;

            if ((OutputResponse == MappingResponse.NOC || InputResponse == MappingResponse.NOC)
                || (OutputResponse == MappingResponse.OutputError || InputResponse == MappingResponse.OutputError))
                return true;

            if (OutputResponse == MappingResponse.OutputOpen && InputResponse == MappingResponse.InputOn)
                return false;
            else if (OutputResponse == MappingResponse.OutputClose && InputResponse == MappingResponse.InputOff)
                return false;

            return true;
        }
    }

    #endregion
}
