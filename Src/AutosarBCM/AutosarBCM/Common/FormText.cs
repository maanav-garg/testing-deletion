using System;
using System.Windows.Forms;

namespace AutosarBCM.Common
{
    /// <summary>
    /// Form to display a long text message inside.
    /// </summary>
    partial class FormText : Form
    {
        /// <summary>
        /// Message to display
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public FormText()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Inits form
        /// </summary>
        /// <param name="sender">form</param>
        /// <param name="e">argument</param>
        private void FormText_Load(object sender, EventArgs e)
        {
            textBox1.Text = Message;
            textBox1.Select(0, 0);
            buttonOk.Select();
        }
    }
}
