using Swapper.Properties;
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Swapper
{
    public enum ButtonState
    {
        Left,
        Right
    }

    public class Tray: ApplicationContext
    {
        private NotifyIcon trayIcon;
        private ButtonState primaryButton;

        [DllImport("user32.dll")]
        public static extern bool SwapMouseButton(bool bSwap);

        public Tray()
        {
            // Initialize Tray Icon
            trayIcon = new NotifyIcon()
            {
                ContextMenu = new ContextMenu(new MenuItem[] {
                    new MenuItem("Exit", Exit)
                }),
                Visible = true,
            };
            trayIcon.MouseClick += MouseClick;

            // set current primary state; we need to toggle things quickly to get the current value,
            // at least until i figure out something better.
            if (SwapMouseButton(true))
            {
                PrimaryButton = ButtonState.Right;
            }
            else
            {
                SwapMouseButton(false);
                PrimaryButton = ButtonState.Left;
            }
        }

        private void UpdateUI()
        {
            if (PrimaryButton == ButtonState.Left)
            {
                trayIcon.Text = "Primary Button: Left";
                trayIcon.Icon = Resources.TrayIconDarkLeft;
            }
            else if (PrimaryButton == ButtonState.Right)
            {
                
                trayIcon.Text = "Primary Button: Right";
                trayIcon.Icon = Resources.TrayIconDarkRight;
            }
        }

        public ButtonState PrimaryButton
        {
            get
            {
                return primaryButton;
            }
            set
            {
                if (value == ButtonState.Left)
                {
                    SwapMouseButton(false);
                }
                else if (value == ButtonState.Right)
                {
                    SwapMouseButton(true);
                }

                primaryButton = value;
                UpdateUI();
            }
        }

        private void Swap()
        {
            if (PrimaryButton == ButtonState.Left) PrimaryButton = ButtonState.Right;
            else if (PrimaryButton == ButtonState.Right) PrimaryButton = ButtonState.Left;
        }

        private void MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            Swap();
        }

        void Exit(object sender, EventArgs e)
        {
            trayIcon.Visible = false;
            Application.Exit();
        }
    }
}
