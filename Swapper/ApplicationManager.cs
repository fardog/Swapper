using System;
using System.Collections.Generic;

namespace Swapper
{
    class ApplicationManager
    {
        private readonly ISwapperConfiguration _configurationManager;
        private readonly IMouseButtonManager _buttonManager;
        private readonly IHotKeyManager _hotkeyManager;
        private readonly ITray _tray;
        private readonly HashSet<int> _hotkeyHandles = new();
        private AboutBox? _aboutBox;

        public event EventHandler ApplicationExit = delegate { };

        public ApplicationManager(
            ISwapperConfiguration configurationManager,
            IMouseButtonManager buttonManager,
            IHotKeyManager hotKeyManager,
            ITray tray)
        {
            _buttonManager = buttonManager;
            _buttonManager.MouseButtonChanged += ButtonManager_MouseButtonChanged;

            _tray = tray;
            _tray.SetPrimaryButton(_buttonManager.PrimaryButton);
            _tray.ButtonSwapClicked += Tray_ButtonSwapClicked;
            _tray.AboutClicked += Tray_AboutClicked;
            _tray.ExitClicked += Tray_ExitClicked;

            _configurationManager = configurationManager;
            _configurationManager.ConfigurationChange += ConfigurationManager_ConfigurationChange;

            _hotkeyManager = hotKeyManager;
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
                _aboutBox.FormClosed += (_, _) => { _aboutBox = null; };
            }
        }

        private void Tray_ExitClicked(object? sender, EventArgs e)
        {
            ApplicationExit.Invoke(this, new EventArgs());
        }
    }
}
