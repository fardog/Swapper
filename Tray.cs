using Swapper.Properties;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Swapper
{
    public class Tray: ApplicationContext
    {
        private readonly NotifyIcon trayIcon;
        private readonly MouseButtonManager buttonManager;
        private AboutBox aboutBox;
        private OptionsBox optionsBox;
        private HotKeyManager keyManager;
        private HashSet<int> hotkeyHandles = new HashSet<int>();
        private SwapperConfiguration configurationManager = new DynamicConfigurationManager();

        public Tray()
        {
            // Initialize Tray Icon
            trayIcon = new NotifyIcon()
            {
                ContextMenu = new ContextMenu(new MenuItem[] {
                    new MenuItem("About", MenuItem_About),
                    new MenuItem("Options", MenuItem_Options),
                    new MenuItem("Exit", MenuItem_Exit)
                }),
                Visible = true,
            };
            trayIcon.MouseClick += MouseClick;

            configurationManager.OnConfigurationChange += delegate { registerHotKeys(); };

            keyManager = new HotKeyManager();
            registerHotKeys();
            keyManager.OnKeyGesture += KeyManager_OnKeyGesture;

            buttonManager = new MouseButtonManager();
            buttonManager.MouseButtonChanged += ButtonManager_MouseButtonChanged;
            UpdateUI(buttonManager.PrimaryButton);
        }

        private void registerHotKeys()
        {
            foreach(var handle in hotkeyHandles)
            {
                keyManager.UnregisterHotKey(handle);
            }
            hotkeyHandles.Clear();

            if (configurationManager.LeftGesture != null)
                hotkeyHandles.Add(
                    keyManager.RegisterHotKey(configurationManager.LeftGesture.Modifiers, configurationManager.LeftGesture.Key));
            if (configurationManager.RightGesture != null)
                hotkeyHandles.Add(
                    keyManager.RegisterHotKey(configurationManager.RightGesture.Modifiers, configurationManager.RightGesture.Key));
        }

        private void KeyManager_OnKeyGesture(object sender, KeyGestureEventArgs e)
        {
            if (e.Gesture.Equivalent(configurationManager.LeftGesture))
                buttonManager.PrimaryButton = ButtonState.Left;
            if (e.Gesture.Equivalent(configurationManager.RightGesture))
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

        private void MenuItem_Options(object sender, EventArgs e)
        {
            if (optionsBox == null)
            {
                optionsBox = new OptionsBox(configurationManager);
                optionsBox.Show();
                optionsBox.FormClosed += (s, ee) => { optionsBox = null; };
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
