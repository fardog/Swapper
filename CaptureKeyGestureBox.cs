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
            KeyUp += CaptureKeyGestureBox_KeyUp;
        }

        private void CaptureKeyGestureBox_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            ModifierKeys modifier = (ModifierKeys)((int)e.KeyData & 0xFFFF);
            Console.WriteLine(e.KeyCode);
            Console.WriteLine(modifier);
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
