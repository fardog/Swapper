using System;
using System.Reflection;
using System.Windows.Forms;
using Swapper.Resources;

namespace Swapper;

partial class AboutBox : Form
{
    private const string UNKNOWN_VALUE_STRING = "UNKNOWN";

    public AboutBox()
    {
        InitializeComponent();
        Text = string.Format(en_US.AboutBox_Text, AssemblyTitle);
        labelProductName.Text = AssemblyProduct;
        labelVersion.Text = string.Format(en_US.AboutBox_Version, AssemblyVersion);
        labelCopyright.Text = AssemblyCopyright;
        labelCompanyName.Text = AssemblyCompany;
        textBoxDescription.Text = AssemblyDescription;
    }

    public sealed override string Text
    {
        get => base.Text;
        set => base.Text = value;
    }

    #region Assembly Attribute Accessors

    private static string AssemblyTitle
    {
        get
        {
            object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
            if (attributes.Length <= 0) return UNKNOWN_VALUE_STRING;

            AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
            return titleAttribute.Title != "" ? titleAttribute.Title : UNKNOWN_VALUE_STRING;
        }
    }

    private static string AssemblyVersion => Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? UNKNOWN_VALUE_STRING;

    private static string AssemblyDescription
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

    private static string AssemblyProduct
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

    private static string AssemblyCopyright
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

    private static string AssemblyCompany
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

    private void OkButton_Click(object sender, EventArgs e)
    {
        Close();
    }
}
