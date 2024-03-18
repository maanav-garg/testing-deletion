using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutosarBCM.Forms
{
    public partial class FormTestLogView : Form
    {
        #region Variables

        /// <summary>
        /// To store the destination file
        /// </summary>
        private string fileName;
        /// <summary>
        /// List of items which parsed from file
        /// </summary>
        private BindingList<LogObject> logObjects;
        /// <summary>
        /// To store total length of readed file
        /// </summary>
        private static int LineNum = 0;
        /// <summary>
        /// To handle cross thread operation for treeView
        /// </summary>
        private TreeView tempTreeView = new TreeView();
        /// <summary>
        /// Total number of received messages
        /// </summary>
        private int rxCt = 0;
        /// <summary>
        /// Total number of transmitted messages
        /// </summary>
        private int txCt = 0;
        /// <summary>
        /// Loop counter for iteration
        /// </summary>
        private int loopCounter = 0;
        /// <summary>
        /// Total time spent in loops
        /// </summary>
        private long totalLoopTime = 0;
        /// <summary>
        /// Minimum time taken by a loop
        /// </summary>
        private long minLoopTime = long.MaxValue;
        /// <summary>
        /// Maximum time taken by a loop
        /// </summary>
        private int maxLoopTime = 0;
        /// <summary>
        /// Name of the longest-running loop
        /// </summary>
        private string longestLoopName = "";
        /// <summary>
        /// Name of the shortest-running loop
        /// </summary>
        private string shortestLoopName = "";

        #endregion

        #region Public

        public FormTestLogView()
        {
            InitializeComponent();           
        }

        #endregion

        #region Private
        /// <summary>
        /// Read file and parse message to TreeView
        /// </summary>
        private void LoadCycle_treeView()
        {
            if (!string.IsNullOrEmpty(fileName))
            {
                var stringList = File.ReadAllLines(fileName);
                var currentNode = tempTreeView.Nodes.Add("Environmental Test");
                var childNode = new TreeNode();
                LineNum = stringList.Length;
                var currentCycle = string.Empty;

                foreach (var line in stringList)
                {
                    LineNum--;
                    if (string.IsNullOrEmpty(line))
                        continue;

                    var cells = line.Trim('#').Replace("\",\"", ";").Split(';');

                    switch (cells.Length)
                    {
                        case 1:
                            if (cells[0] == Constants.StartProcessStarted)
                            {
                                currentNode = currentNode.Nodes.Add(cells[0]);
                            }
                            else if (cells[0] == Constants.StartProcessCompleted)
                            {
                                currentNode = currentNode.Nodes.Add(cells[0]).Parent.Parent;
                            }
                            else if (cells[0] == Constants.ClosingOutputsStarted)
                            {
                                childNode = currentNode.Nodes.Add(cells[0]);
                            }
                            else if (cells[0].StartsWith("Loop") && cells[0].Contains("Started"))
                            {
                                var cycle = cells[0].Substring(cells[0].IndexOf("Cycle"));
                                if (cycle != currentCycle)
                                {
                                    if (!string.IsNullOrEmpty(currentCycle))
                                    {
                                        currentNode = currentNode.Parent;
                                        currentCycle = cycle;
                                    }

                                    currentCycle = cycle;
                                    currentNode = currentNode.Nodes.Add(cycle);
                                }
                                childNode = currentNode.Nodes.Add(cells[0]);   
                            }
                            else if (cells[0].StartsWith("Loop") && cells[0].Contains("finished"))
                            {
                                int loopNumber = int.Parse(cells[0].Split(' ')[1]);
                                if (childNode.Nodes.Count > 0 && loopNumber > 16)
                                {
                                    DateTime firstMessageTime = DateTime.ParseExact((childNode.Nodes[0].Tag as LogObject).DateTime.TrimEnd(), "HH:mm:ss.fff", CultureInfo.InvariantCulture);
                                    DateTime lastMessageTime = DateTime.ParseExact((childNode.Nodes[childNode.Nodes.Count - 1].Tag as LogObject).DateTime.TrimEnd(), "HH:mm:ss.fff", CultureInfo.InvariantCulture);

                                    TimeSpan timeDifference = lastMessageTime - firstMessageTime;

                                    if (timeDifference.TotalMilliseconds < 0)
                                        timeDifference = timeDifference.Add(TimeSpan.FromDays(1));

                                    var timeDifferenceMilliseconds = (int)timeDifference.TotalMilliseconds;
                                    totalLoopTime += timeDifferenceMilliseconds;
                                    loopCounter++;

                                    if (minLoopTime > timeDifferenceMilliseconds)
                                    {
                                        minLoopTime = timeDifferenceMilliseconds;
                                        shortestLoopName = childNode.Text;
                                    }
                                        
                                    if (maxLoopTime < timeDifferenceMilliseconds)
                                    {
                                        maxLoopTime = timeDifferenceMilliseconds;
                                        longestLoopName = childNode.Text;
                                    }
                                }
                            }
                            break;
                        case 6:
                            if (string.IsNullOrEmpty(childNode.Text))
                                childNode = currentNode.Nodes.Add("Unnamed");

                            var node = childNode.Nodes.Add(cells[1]);
                            node.Tag = new LogObject
                            {
                                DateTime = cells[0],
                                Name = cells[1],
                                ItemType = cells[2],
                                Operation = cells[3],
                                Response = cells[4]
                            };
                            break;
                        default:
                            break;
                    }
                    backgroundWorker.ReportProgress(LineNum * 100 / stringList.Length);
                }
            }
        }

        /// <summary>
        /// Read file and parse message to TreeView
        /// </summary>
        private void LoadError_treeView()
        {
            if (!string.IsNullOrEmpty(fileName))
            {
                var stringList = File.ReadAllLines(fileName);
                var currentNode = tempTreeView.Nodes.Add("Error Logs");
                LineNum = stringList.Length;


                foreach (var line in stringList)
                {
                    LineNum--;
                    if (string.IsNullOrEmpty(line))
                        continue;

                    var cells = line.Trim('#').Replace("\",\"", ";").Split(';');

                    switch (cells.Length)
                    {
                        case 1:
                            currentNode = currentNode.Nodes.Add(cells[0]);
                            break;
                        case 6:
                            var node = currentNode.Nodes.Add(cells[1]);
                            node.Tag = new LogObject
                            {
                                DateTime = cells[0],
                                Name = cells[1],
                                ItemType = cells[2],
                                Operation = cells[3],
                                Response = cells[4]
                            };
                            break;
                        default:
                            break;
                    }
                    backgroundWorker.ReportProgress(LineNum * 100 / stringList.Length);

                }
            }
        }

        /// <summary>
        /// To load data grid when double clicking to the tree view item
        /// </summary>
        ///  <param name="sender"></param>
        ///  <param name="e">MouseEventArgs</param>
        private void treeView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var root = treeView.SelectedNode;

            logObjects.Clear();

            AddNodesToListRecursive(root);

            rxCt = txCt = 0;
            lblRxCount.Text = lblTxCount.Text = "0";
            dataGridView.DataSource = new BindingList<LogObject>(logObjects);
        }

        /// <summary>
        /// Adding tree nodes item to binding list recursively
        /// </summary>
        ///  <param name="parentNode">Root or tree</param>
        private void AddNodesToListRecursive(TreeNode parentNode)
        {
            if (parentNode == null)
                return;

            if (parentNode.Tag == null)
                logObjects.Add(new LogObject { Response = parentNode.Text });
            else
                logObjects.Add(parentNode.Tag as LogObject);            

            foreach (TreeNode oSubNode in parentNode.Nodes)
            {
                AddNodesToListRecursive(oSubNode);
            }
        }

        /// <summary>
        /// When text is written to the filter, the log object divided and searched
        /// </summary>
        ///  <param name="sender"></param>
        ///  <param name="e">EventArgs</param>
        private async void txtFilter_TextChanged(object sender, EventArgs e)
        {
            var filteredBindingList = new List<LogObject>();
            var tasks = new List<Task>();
            var text = txtFilter.Text;

            for (int i = 0; i <= logObjects.Count / 50000; i += 50000)
            {
                var divided = logObjects.Skip(i).Take(50000);
                tasks.Add(Task.Run(() =>
                {
                    if (string.IsNullOrEmpty(text))
                        filteredBindingList.AddRange(divided);
                    else
                        filteredBindingList.AddRange(divided.Where(x => x._RowData.Contains(text.ToLower())).ToList());
                }));
            }
            await Task.WhenAll(tasks);

            rxCt = txCt = 0;
            lblRxCount.Text = lblTxCount.Text = "0";
            dataGridView.DataSource = new BindingList<LogObject>(filteredBindingList);
        }

        /// <summary>
        /// Background worker for loading data
        /// </summary>
        ///  <param name="sender"></param>
        ///  <param name="e">DoWorkEventArgs</param>
        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (fileName.Contains("CycleLog"))
                LoadCycle_treeView();
            else
                LoadError_treeView();
                  
            logObjects = new BindingList<LogObject>();
        }

        /// <summary>
        /// Triggering the UI while background worker running
        /// </summary>
        ///  <param name="sender"></param>
        ///  <param name="e">ProgressChangedEventArgs</param>
        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar.Invoke(new Action(() => { progressBar.Value = progressBar.Maximum - e.ProgressPercentage; }));
        }

        /// <summary>
        /// It triggers the completion of the process when the BackgroundWorker finishes
        /// </summary>
        ///  <param name="sender"></param>
        ///  <param name="e">RunWorkerCompletedEventArgs</param>
        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            CopyTreeNodes(tempTreeView, treeView);
            progressBar.Value = progressBar.Maximum;
            treeView.Nodes[0].Expand();
            if (fileName.Contains("CycleLog"))
            {
                lblAvgLoopTime.Text = (totalLoopTime / loopCounter).ToString() + "ms";
                lblMinLoopTime.Text = minLoopTime.ToString() + "ms";
                lblMaxLoopTime.Text = maxLoopTime.ToString() + "ms";
                lblLongestLoopName.Text = longestLoopName;
                lblShortestLoopName.Text = shortestLoopName;
            }
        }

        /// <summary>
        /// Copies the source treeView object to the destination treeView object.
        /// </summary>
        ///  <param name="source">source treeview</param>
        ///  <param name="dest">destination treeview</param>
        private void CopyTreeNodes(TreeView source, TreeView dest)
        {
            TreeNode newTn;
            foreach (TreeNode tn in source.Nodes)
            {
                newTn = new TreeNode(tn.Text, tn.ImageIndex, tn.SelectedImageIndex);
                newTn.Tag = tn.Tag;
                CopyChildren(newTn, tn);
                dest.Nodes.Add(newTn);
            }
        }

        /// <summary>
        /// Copies the source tree node object to the destination tree node object.
        /// </summary>
        ///  <param name="parent">destination tree node</param>
        ///  <param name="original">source tree node</param>
        private void CopyChildren(TreeNode parent, TreeNode original)
        {
            TreeNode newTn;
            foreach (TreeNode tn in original.Nodes)
            {
                newTn = new TreeNode(tn.Text, tn.ImageIndex, tn.SelectedImageIndex);
                newTn.Tag = tn.Tag;
                parent.Nodes.Add(newTn);
                CopyChildren(newTn, tn);
            }
        }

        /// <summary>
        /// Clear the UI and temp values.
        /// </summary>
        ///  <param name="sender"></param>
        ///  <param name="e">FormClosingEventArgs</param>
        private void FormTestLog_FormClosing(object sender, FormClosingEventArgs e)
        {
            treeView.Nodes.Clear();
            progressBar.Value = 75;
            tempTreeView.Nodes.Clear();
            progressBar.Value = 50;
            dataGridView.Rows.Clear();
            progressBar.Value = 0;
        }

        /// <summary>
        /// The action when user change the table. It creates counters and fill with the calors
        /// </summary>
        ///  <param name="sender"></param>
        ///  <param name="e">EventArgs</param>
        private void dataGridView_DataSourceChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                if (!string.IsNullOrEmpty(row.Cells[4].Value.ToString()))
                {
                    if (row.Cells[0].Value == null )
                        continue;
                    row.DefaultCellStyle.BackColor = Color.Green;
                    lblRxCount.Text = (++rxCt).ToString();
                }

                else
                {
                    row.DefaultCellStyle.BackColor = Color.Gray;
                    lblTxCount.Text = (++txCt).ToString();
                }                
            }
            lblTotalCount.Text = (rxCt + txCt).ToString();
        }

        [Obfuscation(Exclude = true)]
        private class LogObject
        {
            public string DateTime { get; set; }
            public string Name { get; set; }
            public string ItemType { get; set; }
            public string Operation { get; set; }
            public string Response { get; set; }
            public string _RowData { get { return (DateTime + Name + ItemType + Operation + Response).ToLower(); } }
        }

        /// <summary>
        /// Selecting a file and fill the UI with button click.
        /// </summary>
        ///  <param name="sender"></param>
        ///  <param name="e">EventArgs</param>
        private void btnReadFile_Click(object sender, EventArgs e)
        {
            using (var dialog = new OpenFileDialog())
            {
                dialog.Filter = "txt files (*.txt)|*.txt";
                dialog.FileName = "*Log*";
                dialog.Multiselect = false;
                dialog.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    if (fileName != dialog.FileName)
                        fileName = dialog.FileName;
                    else
                    {
                        MessageBox.Show("Same log file has been selected!");
                        return;
                    }
                }
                else
                    return;
            }

            if (treeView.Nodes.Count > 0)
            {
                treeView.Nodes.Clear();
                tempTreeView = new TreeView();
                progressBar.Value = 50;
                dataGridView.Rows.Clear();
                progressBar.Value = 0;
            }

            pnlStats.Visible = fileName.Contains("CycleLog");

            backgroundWorker.WorkerReportsProgress = true;

            Text = "Log View - " + fileName;

            backgroundWorker.RunWorkerAsync();
        }

        #endregion
    }
}
