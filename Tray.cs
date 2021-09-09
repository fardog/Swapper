using Swapper.Properties;
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.Win32;

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

            SystemEvents.UserPreferenceChanged += new UserPreferenceChangedEventHandler(System_UserPreferenceChanged);

            UpdateCurrentPrimaryButton();
        }

        private void UpdateCurrentPrimaryButton()
        {
            PrimaryButton = SystemInformation.MouseButtonsSwapped ? ButtonState.Right : ButtonState.Left;
        }

        private void System_UserPreferenceChanged(object sender, UserPreferenceChangedEventArgs e)
        {
            if (e.Category == UserPreferenceCategory.Mouse)
            {
                UpdateCurrentPrimaryButton();
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
