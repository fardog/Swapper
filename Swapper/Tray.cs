using Swapper.Properties;
using System;
using System.Windows.Forms;

namespace Swapper
{
    public interface ITray
    {
        void SetPrimaryButton(ButtonState buttonState);

        event EventHandler ButtonSwapClicked;
        event EventHandler AboutClicked;
        event EventHandler ExitClicked;
    }

    class Tray : ITray, IDisposable
    {
        private readonly NotifyIcon _trayIcon;

        public event EventHandler ButtonSwapClicked = delegate { };
        public event EventHandler AboutClicked = delegate { };
        public event EventHandler ExitClicked = delegate { };

        public Tray()
        {
            // Initialize Tray Icon
            _trayIcon = new NotifyIcon()
            {
                ContextMenuStrip = new ContextMenuStrip(),
                Visible = true,
            };
            _trayIcon.MouseClick += TrayItem_MouseClick;

            ToolStripMenuItem aboutMenuItem = new("About", null, MenuItem_About, "About");
            ToolStripMenuItem exitMenuItem = new("Exit", null, MenuItem_Exit, "Exit");
            _trayIcon.ContextMenuStrip.Items.Add(aboutMenuItem);
            _trayIcon.ContextMenuStrip.Items.Add(exitMenuItem);
        }

        private void TrayItem_MouseClick(object? sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            ButtonSwapClicked.Invoke(this, new EventArgs());
        }

        private void MenuItem_About(object? sender, EventArgs e)
        {
            AboutClicked.Invoke(this, new EventArgs());
        }

        private void MenuItem_Exit(object? sender, EventArgs e)
        {
            ExitClicked.Invoke(this, new EventArgs());
        }

        public void SetPrimaryButton(ButtonState buttonState)
        {
            if (buttonState == ButtonState.Left)
            {
                _trayIcon.Text = "Primary Button: Left";
                _trayIcon.Icon = Resources.TrayIconDarkLeft;
            }
            else if (buttonState == ButtonState.Right)
            {
                
                _trayIcon.Text = "Primary Button: Right";
                _trayIcon.Icon = Resources.TrayIconDarkRight;
            }
        }

        public void Dispose()
        {
            _trayIcon.Visible = false;
        }
    }
}
