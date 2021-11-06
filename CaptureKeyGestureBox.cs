using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace Swapper
{
    public partial class CaptureKeyGestureBox : Form
    {
        public event EventHandler<KeyGestureEventArgs> OnGestureAccepted = delegate { };

        private KeyGesture _gesture;

        public CaptureKeyGestureBox()
        {
            InitializeComponent();
            okButton.Enabled = false;
            KeyDown += CaptureKeyGestureBox_KeyUp;
        }

        private void CaptureKeyGestureBox_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            KeyGesture gesture = null;
            try
            {
                gesture = KeyUtils.ToKeyGesture(e);
            }
            catch (NotSupportedException) { }

            if (gesture?.Complete() ?? false)
            {
                okButton.Enabled = true;
                gestureLabel.Text = gesture.GetDisplayStringForCurrentCulture();
                _gesture = gesture;
            }
            else
            {
                okButton.Enabled = false;
                _gesture = null;
            }
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (_gesture != null)
            {
                OnGestureAccepted.Invoke(this, new KeyGestureEventArgs(_gesture));
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
