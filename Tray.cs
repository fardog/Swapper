using Swapper.Properties;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Swapper
{
    public class Tray: ApplicationContext
    {
        private readonly ISwapperConfiguration _configurationManager;
        private readonly HotKeyManager _hotkeyManager = new();
        private readonly HashSet<int> _hotkeyHandles = new();
        private readonly NotifyIcon _trayIcon;
        private readonly MouseButtonManager _buttonManager;
        private AboutBox? _aboutBox;

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

            _buttonManager = new MouseButtonManager();
            _buttonManager.MouseButtonChanged += ButtonManager_MouseButtonChanged;
            UpdateUI(_buttonManager.PrimaryButton);

            try
            {
                _configurationManager = new ConfigurationManager();
            }
            catch (InvalidConfigurationValueException e)
            {
                MessageBox.Show($"Unable to load configuration, resetting to defaults. Error was:\n\n{e.Message}");
                ConfigurationManager.Reset();
                _configurationManager = new ConfigurationManager();
            }

            _configurationManager.ConfigurationChange += _configurationManager_ConfigurationChange;

            _hotkeyManager.HotKeyPressed += _hotkeyManager_HotKeyPressed;
            RegisterHotKeys();
        }

        private void _configurationManager_ConfigurationChange(object? sender, EventArgs e)
        {
            RegisterHotKeys();
        }

        private void UnregisterHotKeys()
        {
            foreach(var handle in _hotkeyHandles)
            {
                _hotkeyManager.UnregisterHotKey(handle);
            }
            _hotkeyHandles.Clear();
        }

        private void RegisterHotKeys()
        {
            UnregisterHotKeys();

            if (_configurationManager.HotKeyLeftPrimary != null)
                AddHotKey(_configurationManager.HotKeyLeftPrimary);
            if (_configurationManager.HotKeyRightPrimary != null)
                AddHotKey(_configurationManager.HotKeyRightPrimary);
            if (_configurationManager.HotKeySwapPrimary != null)
                AddHotKey(_configurationManager.HotKeySwapPrimary);
        }

        private void AddHotKey(HotKey hotKey)
        {
            _hotkeyHandles.Add(_hotkeyManager.RegisterHotKey(hotKey.Modifiers, hotKey.Key));
        }

        private void _hotkeyManager_HotKeyPressed(object? sender, HotKeyEventArgs e)
        {
            if (e.HotKey == _configurationManager.HotKeyLeftPrimary)
                _buttonManager.PrimaryButton = ButtonState.Left;
            if (e.HotKey == _configurationManager.HotKeyRightPrimary)
                _buttonManager.PrimaryButton = ButtonState.Right;
            if (e.HotKey == _configurationManager.HotKeySwapPrimary)
                _buttonManager.Swap();
        }

        private void ButtonManager_MouseButtonChanged(object? sender, MouseButtonChangedEventArgs e)
        {
            UpdateUI(e.PrimaryButton);
        }

        private void UpdateUI(ButtonState primaryButton)
        {
            if (primaryButton == ButtonState.Left)
            {
                _trayIcon.Text = "Primary Button: Left";
                _trayIcon.Icon = Resources.TrayIconDarkLeft;
            }
            else if (primaryButton == ButtonState.Right)
            {
                
                _trayIcon.Text = "Primary Button: Right";
                _trayIcon.Icon = Resources.TrayIconDarkRight;
            }
        }

        private void TrayItem_MouseClick(object? sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            _buttonManager.Swap();
        }

        private void MenuItem_About(object? sender, EventArgs e)
        {
            if (_aboutBox == null)
            {
                _aboutBox = new AboutBox();
                _aboutBox.Show();
                _aboutBox.FormClosed += (s, ee) => { _aboutBox = null; };
            }
        }

        private void MenuItem_Exit(object? sender, EventArgs e)
        {
            _trayIcon.Visible = false;
            Application.Exit();
        }
    }
}
