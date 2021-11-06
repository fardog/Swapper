using System;
using System.Globalization;
using System.Windows.Forms;

namespace Swapper
{
    public partial class OptionsBox : Form
    {
        private SwapperConfiguration configurationManager;
        private CaptureKeyGestureBox _captureBox;

        public OptionsBox(SwapperConfiguration configurationManager)
        {
            InitializeComponent();

            this.configurationManager = configurationManager;
            this.configurationManager.OnConfigurationChange += ConfigurationManager_OnConfigurationChange;

            updateFromConfiguration();
        }

        private void updateFromConfiguration()
        {
            leftGestureText.Text = configurationManager.LeftGesture?.GetDisplayStringForCurrentCulture() ?? "None";
            rightGestureText.Text = configurationManager.RightGesture?.GetDisplayStringForCurrentCulture() ?? "None";
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
                _captureBox.OnGestureAccepted += (s, ee) =>
                {
                    _captureBox.Close();
                    configurationManager.LeftGesture = ee.Gesture;
                };
            }
        }

        private void setRightButton_Click(object sender, EventArgs e)
        {
            if (_captureBox == null)
            {
                _captureBox = new CaptureKeyGestureBox();
                _captureBox.Show();
                _captureBox.FormClosed += (s, ee) => { _captureBox = null; };
                _captureBox.OnGestureAccepted += (s, ee) =>
                {
                    _captureBox.Close();
                    configurationManager.RightGesture = ee.Gesture;
                };
            }
        }

        private void clearLeftButton_Click(object sender, EventArgs e)
        {
            configurationManager.LeftGesture = null;
        }

        private void clearRightButton_Click(object sender, EventArgs e)
        {
            configurationManager.RightGesture = null;
        }
    }
}
