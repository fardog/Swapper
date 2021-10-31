using Swapper.Properties;
using System;
using System.Windows.Forms;
using System.Windows.Input;

namespace Swapper
{
    public class Tray: ApplicationContext
    {
        private readonly NotifyIcon trayIcon;
        private readonly MouseButtonManager buttonManager;
        private AboutBox aboutBox;
        private HotKeyManager keyManager;

        public Tray()
        {
            // Initialize Tray Icon
            trayIcon = new NotifyIcon()
            {
                ContextMenu = new ContextMenu(new MenuItem[] {
                    new MenuItem("About", MenuItem_About),
                    new MenuItem("Exit", MenuItem_Exit)
                }),
                Visible = true,
            };
            trayIcon.MouseClick += MouseClick;

            keyManager = new HotKeyManager();
            keyManager.RegisterHotKey(ModifierKeys.Control | ModifierKeys.Shift, Key.Left);
            keyManager.RegisterHotKey(ModifierKeys.Control | ModifierKeys.Shift, Key.Right);
            keyManager.OnKeyPressed += KeyManager_OnKeyPressed;

            buttonManager = new MouseButtonManager();
            buttonManager.MouseButtonChanged += ButtonManager_MouseButtonChanged;
            UpdateUI(buttonManager.PrimaryButton);
        }

        private void KeyManager_OnKeyPressed(object sender, HotKeyManager.KeyPressedEventArgs e)
        {
            if (e.Key == Key.Left)
                buttonManager.PrimaryButton = ButtonState.Left;
            else if (e.Key == Key.Right)
                buttonManager.PrimaryButton = ButtonState.Right;
        }

        private void MenuItem_About(object sender, EventArgs e)
        {
            if (aboutBox == null)
            {
                aboutBox = new AboutBox();
                aboutBox.Show();
                aboutBox.FormClosed += (s, ee) => { aboutBox = null; };
            }
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

         private void MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
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
