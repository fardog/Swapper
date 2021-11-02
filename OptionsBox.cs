using System;
using System.Globalization;
using System.Windows.Forms;

namespace Swapper
{
    public partial class OptionsBox : Form
    {
        private ConfigurationManager configurationManager;
        private CaptureKeyGestureBox _captureBox;

        public OptionsBox(ConfigurationManager configurationManager)
        {
            InitializeComponent();

            this.configurationManager = configurationManager;
            this.configurationManager.OnConfigurationChange += ConfigurationManager_OnConfigurationChange;

            updateFromConfiguration();
        }

        private void updateFromConfiguration()
        {
            leftGestureText.Text = configurationManager.LeftGesture.GetDisplayStringForCulture(CultureInfo.CurrentUICulture);
            rightGestureText.Text = configurationManager.RightGesture.GetDisplayStringForCulture(CultureInfo.CurrentUICulture);
        }

        private void ConfigurationManager_OnConfigurationChange(object sender, EventArgs e)
        {
            updateFromConfiguration();
        }

        private void setLeftButton_Click(object sender, EventArgs e)
        {
            if (_captureBox == null)
            {
                _captureBox = new CaptureKeyGestureBox();
                _captureBox.Show();
                _captureBox.FormClosed += (s, ee) => { _captureBox = null; };
            }
        }

        private void setRightButton_Click(object sender, EventArgs e)
        {
            if (_captureBox == null)
            {
                _captureBox = new CaptureKeyGestureBox();
                _captureBox.Show();
                _captureBox.FormClosed += (s, ee) => { _captureBox = null; };
            }
        }
    }
}
