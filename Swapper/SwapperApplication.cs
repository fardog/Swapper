using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Swapper
{
    public class SwapperApplication: ApplicationContext
    {
        private readonly ISwapperConfiguration _configurationManager;
        private readonly IMouseButtonManager _buttonManager;
        private readonly IHotKeyManager _hotkeyManager = new HotKeyManager();
        private readonly ITray _tray;
        private readonly HashSet<int> _hotkeyHandles = new();
        private AboutBox? _aboutBox;

        public SwapperApplication()
        {
            _buttonManager = new MouseButtonManager();
            _buttonManager.MouseButtonChanged += ButtonManager_MouseButtonChanged;

            _tray = new Tray(_buttonManager.PrimaryButton);
            _tray.ButtonSwapClicked += Tray_ButtonSwapClicked;
            _tray.AboutClicked += Tray_AboutClicked;
            _tray.ExitClicked += Tray_ExitClicked;

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

            _configurationManager.ConfigurationChange += ConfigurationManager_ConfigurationChange;

            _hotkeyManager.HotKeyPressed += HotKeyManager_HotKeyPressed;
            RegisterHotKeys();
        }

        private void ConfigurationManager_ConfigurationChange(object? sender, EventArgs e)
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

        private void HotKeyManager_HotKeyPressed(object? sender, HotKeyEventArgs e)
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
            _tray.SetPrimaryButton(e.PrimaryButton);
        }

        private void Tray_ButtonSwapClicked(object? sender, EventArgs e)
        {
            _buttonManager.Swap();
        }

        private void Tray_AboutClicked(object? sender, EventArgs e)
        {
            if (_aboutBox == null)
            {
                _aboutBox = new AboutBox();
                _aboutBox.Show();
                _aboutBox.FormClosed += (s, ee) => { _aboutBox = null; };
            }
        }

        private void Tray_ExitClicked(object? sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
