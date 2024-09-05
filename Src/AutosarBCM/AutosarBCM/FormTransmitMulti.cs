using AutosarBCM.Core;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace AutosarBCM
{
    /// <summary>
    /// Implements the FormTransmitMulti form.
    /// </summary>
    public partial class FormTransmitMulti : Form
    {
        #region Variables

        /// <summary>
        /// The type of the current protocol.
        /// </summary>
        private TransmitProtocol TransmitProtocol;

        /// <summary>
        /// A reference to the current message to be added/edited.
        /// </summary>
        private BaseMessage CurrentMessage { get; set; }

        /// <summary>
        /// A reference to the object that contains the data for the DataGridView to display.
        /// </summary>
        private BindingList<BaseMessage> BindingList { get; set; }

        /// <summary>
        /// Indicates whether any data in the form has been modified by the user.
        /// </summary>
        private bool isDataModified = false;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the FormTransmitMulti class.
        /// </summary>
        /// <param name="protocol">The protocol to be used.</param>
        /// <param name="message">A reference to the current message instance.</param>
        public FormTransmitMulti(TransmitProtocol protocol, BaseMessage message)
        {
            InitializeComponent();

            TransmitProtocol = protocol;
            CurrentMessage = message;
            CurrentMessage.Id = "-";
            BindingList = new BindingList<BaseMessage>();

            CurrentMessage.SubMessages.ForEach(x => BindingList.Add((x.Clone(false))));

            dgvMessages.AutoGenerateColumns = false;
            dgvMessages.DataSource = BindingList;
            BindingList.ResetBindings();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// An event handler to the btnAdd's Click event.
        /// </summary>
        /// <param name="sender">A reference to the btnAdd instance.</param>
        /// <param name="e">A reference to the Click event's arguments.</param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (TransmitProtocol == TransmitProtocol.Can)
            {
                CanMessage message = new CanMessage();
                message.Length = 8;
                if (new FormMessageAddition(message).ShowDialog() == DialogResult.Yes)
                {
                    BindingList.Add(message);
                    isDataModified = true;
                }
                    
            }
        }

        /// <summary>
        /// An event handler to the btnSave's Click event.
        /// </summary>
        /// <param name="sender">A reference to the btnSave instance.</param>
        /// <param name="e">A reference to the Click event's arguments.</param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (BindingList.Count == 0)
            {
                Helper.ShowWarningMessageBox("Please add a message!");
                return;
            }   

            CurrentMessage.Comment = txtComment.Text;
            CurrentMessage.SubMessages.Clear();
            CurrentMessage.SubMessages.AddRange(BindingList.OfType<BaseMessage>());
            CurrentMessage.Length = CurrentMessage.SubMessages.Count;
            this.DialogResult = DialogResult.Yes;
            this.Close();
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

            var item = BindingList[currentIndex];
            BindingList.RemoveAt(currentIndex);
            BindingList.Insert(newIndex, item);

            dgvMessages.DataSource = null;
            dgvMessages.DataSource = BindingList;

            dgvMessages.ClearSelection();
            dgvMessages.CurrentCell = dgvMessages.Rows[newIndex].Cells[0];

            isDataModified = true;
        }

        /// <summary>
        /// An event handler to the tsmiDelete's Click event.
        /// </summary>
        /// <param name="sender">A reference to the tsmiDelete instance.</param>
        /// <param name="e">A reference to the Click event's arguments.</param>
        private void tsmiDelete_Click(object sender, EventArgs e)
        {
            if (dgvMessages.CurrentRow != null)
            {
                dgvMessages.Rows.Remove(dgvMessages.CurrentRow);
                isDataModified = true;
            }
        }

        /// <summary>
        /// An event handler to the tsmiEdit's Click event.
        /// </summary>
        /// <param name="sender">A reference to the tsmiEdit instance.</param>
        /// <param name="e">A reference to the Click event's arguments.</param>
        private void tsmiEdit_Click(object sender, EventArgs e)
        {
            if (dgvMessages.CurrentRow == null)
                return;

            if (TransmitProtocol == TransmitProtocol.Can)
            {
                if (new FormMessageAddition(dgvMessages.CurrentRow.DataBoundItem as CanMessage).ShowDialog() == DialogResult.Yes)
                {
                    BindingList.ResetBindings();
                    isDataModified = true;
                }
                    
            }
        }

        /// <summary>
        /// An event handler to the contextMenuStrip1's Opening event.
        /// </summary>
        /// <param name="sender">A reference to the contextMenuStrip1 instance.</param>
        /// <param name="e">A reference to the Opening event's arguments.</param>
        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            e.Cancel = dgvMessages.Rows.Count == 0;
        }
        
        /// <summary>
        /// An event handler to the dgvMessages's CellDoubleClick event.
        /// </summary>
        /// <param name="sender">A reference to the dgvMessages instance.</param>
        /// <param name="e">A reference to the CellDoubleClick event's arguments.</param>
        private void dgvMessages_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            tsmiEdit_Click(sender, EventArgs.Empty);
        }

        /// <summary>
        /// An event handler to the tsmiCopy's Click event.
        /// </summary>
        /// <param name="sender">A reference to the tsmiCopy instance.</param>
        /// <param name="e">A reference to the Click event's arguments.</param>
        private void tsmiCopy_Click(object sender, EventArgs e)
        {
            if (dgvMessages.CurrentRow == null)
                return;

            BindingList.Add(((BaseMessage)dgvMessages.CurrentRow.DataBoundItem).Clone());
            isDataModified = true;
        }

        /// <summary>
        /// An event handler to the dgvMessages's CellMouseDown event.
        /// </summary>
        /// <param name="sender">A reference to the dgvMessages instance.</param>
        /// <param name="e">A reference to the CellMouseDown event's arguments.</param>
        private void dgvMessages_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex > -1)
                dgvMessages.CurrentCell = dgvMessages.Rows[e.RowIndex].Cells[0];
        }

        /// <summary>
        /// An event handler to the FormTransmitMulti's KeyDown event.
        /// </summary>
        /// <param name="sender">A reference to the FormTransmitMulti instance.</param>
        /// <param name="e">A reference to the KeyDown event's arguments.</param>
        private void FormTransmitMulti_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) this.Close();
        }

        /// <summary>
        /// An event handler for the closing event of FormTransmitMulti.
        /// </summary>
        /// <param name="sender">A reference to the FormTransmitMulti instance.</param>
        /// <param name="e">A reference to the arguments of the closing event.</param>
        private void FormTransmitMulti_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (BindingList.Count != 0 && this.DialogResult == DialogResult.Cancel && isDataModified)
            {
                if (!Helper.ShowConfirmationMessageBox("Unsaved changes will be lost. Are you sure you want to continue?"))
                    e.Cancel = true;
            }
        }

        #endregion
    }
}
