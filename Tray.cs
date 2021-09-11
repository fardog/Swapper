using Swapper.Properties;
using System;
using System.Windows.Forms;

namespace Swapper
{
    public class Tray: ApplicationContext
    {
        private readonly NotifyIcon trayIcon;
        private readonly MouseButtonManager buttonManager;

        public Tray()
        {
            // Initialize Tray Icon
            trayIcon = new NotifyIcon()
            {
                ContextMenu = new ContextMenu(new MenuItem[] {
                    new MenuItem("Exit", MenuItem_Exit)
                }),
                Visible = true,
            };
            trayIcon.MouseClick += MouseClick;

            buttonManager = new MouseButtonManager();
            buttonManager.MouseButtonChanged += ButtonManager_MouseButtonChanged;
            UpdateUI(buttonManager.PrimaryButton);
        }

        private void ButtonManager_MouseButtonChanged(object sender, MouseButtonChangedEventArgs e)
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

         private void MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            buttonManager.Swap();
        }

        void MenuItem_Exit(object sender, EventArgs e)
        {
            trayIcon.Visible = false;
            Application.Exit();
        }
    }
}
