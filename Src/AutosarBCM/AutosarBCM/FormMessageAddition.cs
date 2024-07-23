using AutosarBCM.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace AutosarBCM
{
    /// <summary>
    /// Implements the FormMessageAddition form.
    /// </summary>
    public partial class FormMessageAddition : Form
    {
        #region Variables

        internal int msgByteLength = 8;
        private CanMessage CurrentMessage { get; set; }
        private List<TextBox> textBoxes = new List<TextBox>();
        private List<Label> labels = new List<Label>();
        internal int byteLen = int.MinValue;
        internal int horizontalSize = 472;
        internal int verticalSize= 279;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the FormMessageAddition class.
        /// </summary>
        /// <param name="message">A reference to the current message instance.</param>
        public FormMessageAddition(CanMessage message)
        {
            InitializeComponent();
            for (int i = 4; i<68; i++)
            {
                Label label = new Label();
                label.Name = "label" + (i-4);
                label.Text = (i - 4).ToString();
                labels.Add(label);
            }
            for (int i = 0; i < 64; i++)
            {
                TextBox textBox = new TextBox();
                textBox.Name = "txtDataByte" + i;
                textBox.Text = "00";
                textBoxes.Add(textBox);
            }
            LoadCombo();
            LoadLabel();
            pnlHexBytes.ResumeLayout(false);
            pnlHexBytes.PerformLayout();
            msgPanel.ResumeLayout(false);
            msgPanel.PerformLayout();
            ClientSize = new System.Drawing.Size(horizontalSize, verticalSize);
            ResumeLayout(false);

            for (int x = 0; x < 64; x++)
            {
                textBoxes.ElementAt(x).MaxLength = 2;
                textBoxes.ElementAt(x).Click += TextBoxOnClick;        
                textBoxes.ElementAt(x).KeyPress += TextBoxKeyPress;
                textBoxes.ElementAt(x).TextChanged += TextBoxTextChanged;

            }

            CurrentMessage = message;
            GetData(message);
            txtArbID.KeyPress += TextBoxKeyPress;
            KeyDown += new KeyEventHandler(PopupForm_KeyDown);
            KeyPreview = true;
        }

        private void LoadLabel()
        {
            int k = 0;
            while (k < msgByteLength)
            {
                Label label = labels.ElementAt(k);
                pnlHexBytes.Controls.Add(label); // Add the label to the form's controls
                k++;
            }

            k = 0;
            while (k < msgByteLength)
            {
                Label label = labels.ElementAt(k);
                label.AutoSize = true;
                label.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
                label.Location = new System.Drawing.Point(18 + (k * 34), 31);
                label.Margin = new Padding(4, 0, 4, 0);
                label.Size = new System.Drawing.Size(14, 15);
                label.TabIndex = 96;
                k++;
                if (k == 16)
                    break;
            }
            if (msgByteLength > 16)
            {
                while (k < msgByteLength)
                {
                    Label label = labels.ElementAt(k);
                    label.AutoSize = true;
                    label.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
                    label.Location = new System.Drawing.Point(18 + ((k - 16) * 34), 78);
                    label.Margin = new Padding(4, 0, 4, 0);
                    label.Size = new System.Drawing.Size(14, 15);
                    label.TabIndex = 96;
                    k++;
                    if (k == 32)
                        break;
                }
                if (msgByteLength > 32){
                    while (k < msgByteLength)
                    {
                        Label label = labels.ElementAt(k);
                        label.AutoSize = true;
                        label.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
                        label.Location = new System.Drawing.Point(18 + ((k - 32) * 34), 125);
                        label.Margin = new Padding(4, 0, 4, 0);
                        label.Size = new System.Drawing.Size(14, 15);
                        label.TabIndex = 96;
                        k++;
                        if (k == 48)
                            break;

                    }
                    if(msgByteLength > 48)
                    {
                        while (k < msgByteLength)
                        {
                            Label label = labels.ElementAt(k);
                            label.AutoSize = true;
                            label.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
                            label.Location = new System.Drawing.Point(18 + ((k - 48) * 34), 172);
                            label.Margin = new Padding(4, 0, 4, 0);
                            label.Size = new System.Drawing.Size(14, 15);
                            label.TabIndex = 96;
                            k++;
                        }
                    }
                }
            }
/*            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
*/          AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(horizontalSize, verticalSize);
            ControlBox = false;
            Controls.Add(msgPanel);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Margin = new Padding(4);
            Name = "FormMessageAddition";
            StartPosition = FormStartPosition.CenterParent;
            Text = "New Message";
            Load += new EventHandler(FormMessageAddition_Load);
            msgPanel.ResumeLayout(false);
            msgPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(numDelayTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(numCycleCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(numCycleTime)).EndInit();
            this.ResumeLayout(false);
        }

        private void LoadCombo()
        {
            horizontalSize = 712;
            verticalSize = 279;
            pnlHexBytes.Size = new System.Drawing.Size(555, 70);
            msgPanel.Size = new System.Drawing.Size(648, 249);
            int k = 0;
            while (k < msgByteLength)
            {
                TextBox textBox = textBoxes.ElementAt(k);
                pnlHexBytes.Controls.Add(textBox); // Add the label to the form's controls
                k++;
            }
            k = 0;
            while (k < msgByteLength)
            {
                TextBox textBox = textBoxes.ElementAt(k);
                textBox.CharacterCasing = CharacterCasing.Upper;
                textBox.Location = new System.Drawing.Point(13 + ((k) * 34), 4);
                textBox.Margin = new Padding(3, 2, 3, 2);
                textBox.Size = new System.Drawing.Size(28, 22);
                textBox.TabIndex = 78;
                textBox.TextAlign = HorizontalAlignment.Center;
                k++;
                if (k == 16)
                    break;
            }
            btnCancel.Location = new System.Drawing.Point(352, 218);
            btnMsgAdd.Location = new System.Drawing.Point(268, 218);

            txtComment.Location = new System.Drawing.Point(23, 187);
            lblComment.Location = new System.Drawing.Point(19, 168);

            lblCycleTime.Location = new System.Drawing.Point(117, 120);
            numCycleTime.Location = new System.Drawing.Point(118, 138);

            lblDelayTime.Location = new System.Drawing.Point(337, 120);
            numDelayTime.Location = new System.Drawing.Point(340, 140);

            triggerLabel.Location = new System.Drawing.Point(19, 118);
            cbTrigger.Location = new System.Drawing.Point(23, 138);

            lblCycleCount.Location = new System.Drawing.Point(227, 120);
            numCycleCount.Location = new System.Drawing.Point(228, 140);
            if (msgByteLength > 16)
            {
                horizontalSize = 748;
                verticalSize = 305;
                pnlHexBytes.Size = new System.Drawing.Size(555, 140);
                msgPanel.Size = new System.Drawing.Size(748, 305);

                while (k < msgByteLength)
                {
                    TextBox textBox = textBoxes.ElementAt(k);
                    textBox.CharacterCasing = CharacterCasing.Upper;
                    textBox.Location = new System.Drawing.Point(13 + ((k-16) * 34), 51);
                    textBox.Margin = new Padding(3, 2, 3, 2);
                    textBox.Size = new System.Drawing.Size(28, 22);
                    textBox.TabIndex = 78;
                    textBox.TextAlign = HorizontalAlignment.Center;
                    k++;
                    if (k == 32)
                        break;
                }
                btnCancel.Location = new System.Drawing.Point(552, 246);
                btnMsgAdd.Location = new System.Drawing.Point(468, 246);

                txtComment.Location = new System.Drawing.Point(23, 257);
                lblComment.Location = new System.Drawing.Point(19, 237);

                lblCycleTime.Location = new System.Drawing.Point(117, 174);
                numCycleTime.Location = new System.Drawing.Point(118, 194);

                lblDelayTime.Location = new System.Drawing.Point(337, 174);
                numDelayTime.Location = new System.Drawing.Point(340, 194);

                triggerLabel.Location = new System.Drawing.Point(19, 92);
                cbTrigger.Location = new System.Drawing.Point(23, 112);

                lblCycleCount.Location = new System.Drawing.Point(227, 174);
                numCycleCount.Location = new System.Drawing.Point(228, 194);
                if (msgByteLength>32)
                {
                    horizontalSize = 748;
                    verticalSize = 450;
                    pnlHexBytes.Size = new System.Drawing.Size(555, 140);
                    msgPanel.Size = new System.Drawing.Size(748, 305);
                    while (k < msgByteLength)
                    {
                        TextBox textBox = textBoxes.ElementAt(k);
                        textBox.CharacterCasing = CharacterCasing.Upper;
                        textBox.Location = new System.Drawing.Point(13 + ((k-32) * 34), 98);
                        textBox.Margin = new Padding(3, 2, 3, 2);
                        textBox.Size = new System.Drawing.Size(28, 22);
                        textBox.TabIndex = 78;
                        textBox.TextAlign = HorizontalAlignment.Center;
                        k++;
                        if (k == 48)
                            break;
                    }
                    if (msgByteLength > 48)
                    {
                        pnlHexBytes.Size = new System.Drawing.Size(555, 225);
                        msgPanel.Size = new System.Drawing.Size(748, 450);
                        while (k < msgByteLength)
                        {
                            TextBox textBox = textBoxes.ElementAt(k);
                            textBox.CharacterCasing = CharacterCasing.Upper;
                            textBox.Location = new System.Drawing.Point(13 + ((k - 48) * 34), 145);
                            textBox.Margin = new Padding(3, 2, 3, 2);
                            textBox.Size = new System.Drawing.Size(28, 22);
                            textBox.TabIndex = 78;
                            textBox.TextAlign = HorizontalAlignment.Center;
                            k++;
                            if (k == 64)
                                break;
                        }
                    }
                    btnCancel.Location = new System.Drawing.Point(552, 366);
                    btnMsgAdd.Location = new System.Drawing.Point(468, 366);

                    txtComment.Location = new System.Drawing.Point(23, 377);
                    lblComment.Location = new System.Drawing.Point(19, 357);

                    lblCycleTime.Location = new System.Drawing.Point(117, 274);
                    numCycleTime.Location = new System.Drawing.Point(118, 294);

                    lblDelayTime.Location = new System.Drawing.Point(337, 274);
                    numDelayTime.Location = new System.Drawing.Point(340, 294);

                    triggerLabel.Location = new System.Drawing.Point(19, 192);
                    cbTrigger.Location = new System.Drawing.Point(23, 212);

                    lblCycleCount.Location = new System.Drawing.Point(227, 274);
                    numCycleCount.Location = new System.Drawing.Point(228, 294);
                }
            }
        }





        #endregion

        #region Private Methods

        /// <summary>
        /// An event handler to the PopupForm's KeyDown event.
        /// </summary>
        /// <param name="sender">A reference to the PopupForm instance.</param>
        /// <param name="e">A reference to the KeyDown event's arguments.</param>
        private void PopupForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                btnCancel_Click(sender, e);
            }
            else if(e.KeyCode == Keys.Enter)
            {
                btnMsgAdd_Click(sender, e);
            }
        }
        /// <summary>
        /// </summary>
        /// <param name="sender">A reference to the txtDataByte instance.</param>
        /// <param name="e">A reference to the Click event's arguments.</param>

        private void numMsgLen_ValChanged(object sender, EventArgs e)
        {
            msgByteLength = (int)numMessageLength.Value;
            this.horizontalSize = 460;
            this.verticalSize = 209;
            this.msgPanel.SuspendLayout();
            this.pnlHexBytes.SuspendLayout();
            this.pnlHexBytes.Controls.Clear();
            LoadCombo();
            LoadLabel();
            this.msgPanel.ResumeLayout(false);
            this.pnlHexBytes.ResumeLayout(false);
        }

        /// <summary>
        /// An event handler to the txtDataByte's Click event.
        /// </summary>
        /// <param name="sender">A reference to the txtDataByte instance.</param>
        /// <param name="eventArgs">A reference to the Click event's arguments.</param>
        private void TextBoxOnClick(object sender, EventArgs eventArgs)
        {
            var textBox = (TextBox)sender;
            textBox.SelectAll();
            textBox.Focus();
        }

        /// <summary>
        /// An event handler to the txtDataByte's KeyPress event.
        /// </summary>
        /// <param name="sender">A reference to the txtDataByte instance.</param>
        /// <param name="keyEventArgs">A reference to the KeyPress event's arguments.</param>
        private void TextBoxKeyPress(object sender, KeyPressEventArgs keyEventArgs)
        {
            keyEventArgs.Handled = !Helper.IsHexadecimal(keyEventArgs.KeyChar);
        }

        /// <summary>
        /// Event handler for the text changed event of text boxes. 
        /// Moves the focus to the next control in the tab order within the same container when text changes.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="eventArgs">The event arguments.</param>
        private void TextBoxTextChanged(object sender, EventArgs eventArgs)
        {
            //TODO!: change flowunu sagdan sola aliyor nextcontrol nedeniyle bunu soldan saga cekmek gerek
            var textBox = (TextBox)sender;
            if(textBox.Text.Length == 2)
            {
                Control parentItem = textBox.Parent;
                var nextItem = parentItem.GetNextControl(textBox, true);
                nextItem.Focus();
            }
        }

        /// <summary>
        /// An event handler to the btnCancel's Click event.
        /// </summary>
        /// <param name="sender">A reference to the btnCancel instance.</param>
        /// <param name="e">A reference to the Click event's arguments.</param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        /// <summary>
        /// Displays the data of the CanMessage instance in the corresponding UI controls.
        /// </summary>
        /// <param name="message">The CanMessage instance to be displayed.</param>
        private void GetData(CanMessage message)
        {
            txtArbID.Text = message.Id;
            numMessageLength.Text = message.Length.ToString();
            numCycleTime.Value = message.CycleTime;
            numCycleCount.Value = message.CycleCount;
            numDelayTime.Value = message.DelayTime;
            cbTrigger.Text = message.Trigger;
            txtComment.Text = message.Comment;
            ArrayToTextBoxes(message.Data);

            Text = message.Id != null ? "Message Update" : Text;
        }

        /// <summary>
        /// An event handler to the btnMsgAdd's Click event.
        /// </summary>
        /// <param name="sender">A reference to the btnMsgAdd instance.</param>
        /// <param name="e">A reference to the Click event's arguments.</param>
        private void btnMsgAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtArbID.Text))
            {
                Helper.ShowWarningMessageBox("Arb ID can not be empty!");
                return;
            }

            CurrentMessage.Id = txtArbID.Text;
            CurrentMessage.Data = TextBoxesToArray();
            CurrentMessage.CycleTime = (int)numCycleTime.Value;
            CurrentMessage.CycleCount = (int)numCycleCount.Value;
            CurrentMessage.DelayTime = (int)numDelayTime.Value;
            CurrentMessage.Trigger = cbTrigger.Text;
            CurrentMessage.Comment = txtComment.Text;
            CurrentMessage.Length = (int)numMessageLength.Value;

            this.DialogResult = DialogResult.Yes;
            this.Close();
        }

        /// <summary>
        /// Parses the values of the message data from each TextBox and returns the result as an array of bytes.
        /// </summary>
        /// <returns>The data of the message as a byte array.</returns>
        /// TDDO:fix this problem that I have to adjust the iteration for every single byte length 
        private byte[] TextBoxesToArray()
        {
            var data = new byte[msgByteLength];
            for(int x = 0; x < msgByteLength; x++)
            {
                data[x] = byte.Parse(textBoxes.ElementAt(x).Text, System.Globalization.NumberStyles.AllowHexSpecifier);
            }
            return data;
        }
        /// <summary>
        /// Displays each byte of the message data in a hexadecimal format in its corresponding TextBox.
        /// </summary>
        /// <param name="data">The message data to be displayed.</param>
        private void ArrayToTextBoxes(byte[] data)
        {
            for(int i = 0;i < data.Length;i++)
            {
                this.textBoxes.ElementAt(i).Text = data[i].ToString("X2");
            }
        }

        /// <summary>
        /// An event handler to the FormMessageAddition's Load event.
        /// </summary>
        /// <param name="sender">A reference to the FormMessageAddition instance.</param>
        /// <param name="e">A reference to the Load event's arguments.</param>
        private void FormMessageAddition_Load(object sender, EventArgs e)
        {
            this.Height = btnMsgAdd.Height + btnMsgAdd.Location.Y + 70;
            cbTrigger_SelectedIndexChanged(sender, e);
        }

        /// <summary>
        /// An event handler to the cbTrigger's SelectedIndexChanged event.
        /// </summary>
        /// <param name="sender">A reference to the cbTrigger instance.</param>
        /// <param name="e">A reference to the SelectedIndexChanged event's arguments.</param>
        private void cbTrigger_SelectedIndexChanged(object sender, EventArgs e)
        {
            numCycleTime.Enabled = numCycleCount.Enabled = cbTrigger.SelectedIndex == 1;
        }

        #endregion
    }
}
