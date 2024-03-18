using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace AutosarBCM
{
    /// <summary>
    /// About window.
    /// </summary>
    partial class FormAbout : Form
    {
        #region Constructor

        /// <summary>
        /// Default constructor.
        /// </summary>
        public FormAbout()
        {
            InitializeComponent();
            this.Text = String.Format("About {0}", AssemblyTitle);
            this.labelProductName.Text = AssemblyProduct;
            this.labelVersion.Text = String.Format("Version {0}", AssemblyVersion);
            this.labelCopyright.Text = AssemblyCopyright;
            //this.labelCompanyName.Text = AssemblyCompany;
            this.textBoxDesc.Text = AssemblyDescription;

            listBoxProduct.Items.Add("Apache Log4net");
            listBoxProduct.Items.Add("DockPanelSuite");
        }

        #endregion

        #region Assembly Attribute Accessors

        /// <summary>
        /// Gets the title of the assembly, usually the name of the application.
        /// </summary>
        public string AssemblyTitle
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "")
                    {
                        return titleAttribute.Title;
                    }
                }
                return Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        /// <summary>
        /// Gets the version of the assembly along with the build date in a specific format.
        /// </summary>
        public string AssemblyVersion
        {
            get
            {
                var buildDate = new FileInfo(Assembly.GetExecutingAssembly().Location).LastWriteTime;
                var version = Assembly.GetExecutingAssembly().GetName().Version;
                return string.Format("{0}.{1}.{2} - build {3}", version.Major, version.Minor, version.Build, buildDate.ToShortDateString());
            }
        }

        /// <summary>
        /// Gets the description of the assembly, if available.
        /// </summary>
        public string AssemblyDescription
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        /// <summary>
        /// Gets the product name associated with the assembly.
        /// </summary>
        public string AssemblyProduct
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        /// <summary>
        /// Gets the copyright information associated with the assembly.
        /// </summary>
        public string AssemblyCopyright
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        /// <summary>
        /// Gets the name of the company or organization that created the assembly.
        /// </summary>
        public string AssemblyCompany
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Display related license text.
        /// </summary>
        private void listBoxProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            string text = "";
            switch (listBoxProduct.SelectedIndex)
            {
                case 0: // log4net
                    text = Properties.Resources.license_apache_2_0;
                    break;
                case 1:
                    text = Properties.Resources.license_dockpanelsuite;
                    break;
            }
            textBoxLicense.Text = text;
        }

        #endregion
    }
}
