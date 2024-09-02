using AutosarBCM.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutosarBCM{
    /// <summary>
    /// Transmit Form class
    /// </summary>
    public partial class FormTransmit : Form
    {
        #region Variables

        /// <summary>
        /// Binding list for storing and managing CanMessage objects.
        /// </summary>
        private BindingList<CanMessage> bindingList = new BindingList<CanMessage>();

        /// <summary>
        /// The current filter text used to filter the data in the DataGridView. 
        /// </summary>
        private string currentFilter = "";

        #endregion

        #region Constructor

        public FormTransmit()
        {
            InitializeComponent();
            dgvMessages.AutoGenerateColumns = false;
            dgvMessages.DataSource = bindingList;
            dgvMessages.DefaultCellStyle.SelectionBackColor = Color.Orange;
        }

        #endregion

        #region Public Methods

        #endregion

        #region Private Methods


        /// <summary>
        /// Loads the Transmit form
        /// </summary>
        /// <param name="sender">Transmit form</param>
        /// <param name="e">Event args</param>
        private void FormTransmit_Load(object sender, EventArgs e)
        {
            FormMain formMain = new FormMain();
            this.Height = formMain.Height / 2;
        }

        /// <summary>
        /// Transmit Message from the selected row of the DataGridView 
        /// </summary>
        private async void TransmitMessage()
        {
            if (!ConnectionUtil.CheckConnection())
                return;

            if (dgvMessages.CurrentRow == null)
            {
                Helper.ShowWarningMessageBox("Please add or select a message!");
                return;
            }

            var baseMessage = ((BaseMessage)dgvMessages.CurrentRow.DataBoundItem);

            if (baseMessage.CheckForTransmit())
            {
                tsbTransmit.Enabled = false;
                await Task.Run(() => baseMessage.Transmit());
                tsbTransmit.Enabled = true;
                bindingList.ResetBindings();
                Helper.ApplyFilterAndRestoreSelection(dgvMessages,currentFilter);
            }
            else
            {
                Helper.ShowWarningMessageBox("Unexpected transmit message.\nPlease check transmit message.");
            }
        }

        /// <summary>
        /// Handles key down events on the DataGridView. Triggers message transmission on space key press.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Provides data for the KeyEventArgs.</param>
        private void dgvMessages_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == (Keys.Space))
            {
                TransmitMessage();
            }
        }

        /// <summary>
        /// Edit action on double click of grid row
        /// </summary>
        /// <param name="sender">Grid control</param>
        /// <param name="e">Event args</param>
        private void dgvMessages_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            tsmiEdit_Click(sender, null);
        }

        /// <summary>
        /// Transmits the selected message
        /// </summary>
        /// <param name="sender">Transmit button</param>
        /// <param name="e">Click event args</param>
        private void tsbTransmit_Click(object sender, EventArgs e)
        {
            TransmitMessage();
        }

        /// <summary>
        /// Opens New message window
        /// </summary>
        /// <param name="sender">New message button</param>
        /// <param name="e">Event args</param>
        private void tsbNewMsg_Click(object sender, EventArgs e)
        {
            var message = new CanMessage();
            message.Length = 8;
            var f = new FormMessageAddition(message);
            if (f.ShowDialog() == DialogResult.Yes)
            {
                bindingList.Add(message);
                Helper.ApplyFilterAndRestoreSelection(this.dgvMessages, currentFilter);
            }
        }


        /// <summary>
        /// Click event of toolStripButton1 control
        /// </summary>
        /// <param name="sender">Strip button control</param>
        /// <param name="e">Event args</param>
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow dgvRow in dgvMessages.Rows)
            {
                dgvRow.Cells["Count"].Value = "0";
            }
        }

        /// <summary>
        /// Text change event of txtFilter control
        /// </summary>
        /// <param name="sender">Filter text box</param>
        /// <param name="e">Event args</param>
        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            currentFilter = txtFilter.Text.ToLower();
            Helper.ApplyFilter(dgvMessages, currentFilter);
        }

        /// <summary>
        /// Opens multi transmit window
        /// </summary>
        /// <param name="sender">Multi transmit button</param>
        /// <param name="e">Event args</param>
        private void tsbMultiTransmit_Click(object sender, EventArgs e)
        {
            var message = new CanMessage(true);
            message.Length = 8;
            if (new FormTransmitMulti(TransmitProtocol.Can, message).ShowDialog() == DialogResult.Yes)
                bindingList.Add(message);
        }

        /// <summary>
        /// Event handler for the context menu opening. Cancels opening if no row is currently selected.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Provides data for the CancelEventArgs.</param>
        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            e.Cancel = dgvMessages.CurrentRow == null;
        }


        private void dgvMessages_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex > -1)
                dgvMessages.CurrentCell = dgvMessages.Rows[e.RowIndex].Cells[0];
        }

        /// <summary>
        /// Copies the selected message
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event args</param>
        private void tsmiCopy_Click(object sender, EventArgs e)
        {
            if (dgvMessages.CurrentRow == null)
                return;

            bindingList.Add((CanMessage)((CanMessage)dgvMessages.CurrentRow.DataBoundItem).Clone());
        }

        /// <summary>
        /// Deletes the selected message
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event args</param>
        private void tsmiDelete_Click(object sender, EventArgs e)
        {
            if (dgvMessages.CurrentRow == null)
                return;

            dgvMessages.Rows.Remove(dgvMessages.CurrentRow);
        }

        /// <summary>
        /// Edits the selected row
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event args</param>
        private void tsmiEdit_Click(object sender, EventArgs e)
        {
            if (dgvMessages.CurrentRow == null)
                return;

            var message = dgvMessages.CurrentRow.DataBoundItem as CanMessage;

            if (message.Multi)
            {
                FormTransmitMulti form = new FormTransmitMulti(TransmitProtocol.Can, message);
                form.txtComment.Text = message.Comment;
                form.ShowDialog();
            }
            else
            {
                if (new FormMessageAddition(message).ShowDialog() != DialogResult.Yes)
                    return;
            }
            bindingList.ResetBindings();
            Helper.ApplyFilterAndRestoreSelection(dgvMessages, currentFilter);
        }

        /// <summary>
        /// Handles click event of the import data tool strip button. Imports data from a CSV file.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Provides data for the EventArgs.</param>
        private void tsbImportData_Click(object sender, EventArgs e)
        {
            var csvList = (List<CanMessage>)CsvHelper.ImportCsvFile(TransmitProtocol.Can);

            if (csvList != null)
                csvList.ForEach(x => bindingList.Add(x));
        }

        /// <summary>
        /// Handles click event of the CSV template tool strip button. Downloads a CSV template.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Provides data for the EventArgs.</param>
        private void tsbCsvTemplate_Click(object sender, EventArgs e)
        {
            CsvHelper.DownloadCsvFile(TransmitProtocol.Can, bindingList);
        }

        /// <summary>
        /// An event handler to the btnUp's Click event.
        /// </summary>
        /// <param name="sender">A reference to the btnUp instance.</param>
        /// <param name="e">A reference to the Click event's arguments.</param>
        private void btnUp_Click(object sender, EventArgs e)
        {
            MoveUpDown(-1);
        }

        /// <summary>
        /// An event handler to the btnDown's Click event.
        /// </summary>
        /// <param name="sender">A reference to the btnDown instance.</param>
        /// <param name="e">A reference to the Click event's arguments.</param>
        private void btnDown_Click(object sender, EventArgs e)
        {
            MoveUpDown(1);
        }

        /// <summary>
        /// Moves a row up or down in the DataGridView.
        /// </summary>
        /// <param name="direction">Up: -1, Down: 1</param>
        private void MoveUpDown(int direction)
        {
            if (dgvMessages.CurrentRow == null) return;
            int currentIndex = dgvMessages.CurrentRow.Index;
            int newIndex = currentIndex + direction;

            if (newIndex < 0 || newIndex >= dgvMessages.Rows.Count) return;

            var item = bindingList[currentIndex];
            bindingList.RemoveAt(currentIndex);
            bindingList.Insert(newIndex, item);

            dgvMessages.DataSource = null;
            dgvMessages.DataSource = bindingList;

            dgvMessages.ClearSelection();
            dgvMessages.CurrentCell = dgvMessages.Rows[newIndex].Cells[0];

            Helper.ApplyFilterAndRestoreSelection(dgvMessages, currentFilter);
        }
        #endregion
    }
}
