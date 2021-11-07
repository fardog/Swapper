using Swapper.Properties;
using System;
using System.Windows.Forms;

namespace Swapper
{
    public class Tray: ApplicationContext
    {
        private readonly NotifyIcon trayIcon;
        private readonly MouseButtonManager buttonManager;
        private AboutBox? aboutBox;

        public Tray()
        {
            // Initialize Tray Icon
            trayIcon = new NotifyIcon()
            {
                ContextMenuStrip = new ContextMenuStrip(),
                Visible = true,
            };
            trayIcon.MouseClick += TrayItem_MouseClick;

            ToolStripMenuItem aboutMenuItem = new("About", null, MenuItem_About, "About");
            ToolStripMenuItem exitMenuItem = new("Exit", null, MenuItem_Exit, "Exit");
            trayIcon.ContextMenuStrip.Items.Add(aboutMenuItem);
            trayIcon.ContextMenuStrip.Items.Add(exitMenuItem);

            buttonManager = new MouseButtonManager();
            buttonManager.MouseButtonChanged += ButtonManager_MouseButtonChanged;
            UpdateUI(buttonManager.PrimaryButton);
        }

        private void ButtonManager_MouseButtonChanged(object? sender, MouseButtonChangedEventArgs e)
        {
            UpdateUI(e.PrimaryButton);
        }

       private void UpdateUI(ButtonState primaryButton)
        {
            if (primaryButton == ButtonState.Left)
            {
                trayIcon.Text = "Primary Button: Left";
                trayIcon.Icon = Resources.TrayIconDarkLeft;
            }
            else if (primaryButton == ButtonState.Right)
            {
                
                trayIcon.Text = "Primary Button: Right";
                trayIcon.Icon = Resources.TrayIconDarkRight;
            }
        }

        private void TrayItem_MouseClick(object? sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            buttonManager.Swap();
        }

        private void MenuItem_About(object? sender, EventArgs e)
        {
            if (aboutBox == null)
            {
                aboutBox = new AboutBox();
                aboutBox.Show();
                aboutBox.FormClosed += (s, ee) => { aboutBox = null; };
            }
        }

        void MenuItem_Exit(object? sender, EventArgs e)
        {
            trayIcon.Visible = false;
            Application.Exit();
        }
    }
}
