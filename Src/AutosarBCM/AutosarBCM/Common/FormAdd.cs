using System;
using System.Windows.Forms;

namespace AutosarBCM.Common
{
    /// <summary>
    /// Message add window
    /// </summary>
    public partial class FormAdd : Form
    {
        #region Variables

        /// <summary>
        /// Return Value for text box
        /// </summary>
        public string ReturnValue { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public FormAdd()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Constructor with Dialog Header 
        /// </summary>
        /// <param name="header"></param>
        public FormAdd(string header)
        {
            InitializeComponent();
            Text = header;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Initializes the form.
        /// </summary>
        /// <param name="sender">form</param>
        /// <param name="e">arguments</param>
        private void FormAdd_Load(object sender, EventArgs e)
        {
            txtName.Focus();
        }

        /// <summary>
        /// Handles Add item.
        /// </summary>
        /// <param name="sender">button</param>
        /// <param name="e">argument</param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            ReturnValue = txtName.Text;
            DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// Handles Cancel item.
        /// </summary>
        /// <param name="sender">button</param>
        /// <param name="e">argument</param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            ReturnValue = string.Empty;
            DialogResult = DialogResult.Cancel;
        }

        #endregion
    }
}
