using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Windows.Input;

namespace Swapper
{
    public class HotKeyManager : IDisposable
    {
        [DllImport("user32.dll")]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

        [DllImport("user32.dll")]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        private HiddenWindow _window = new HiddenWindow();
        private int _currentId;

        public event EventHandler<KeyPressedEventArgs> OnKeyPressed = delegate { };

        public class HiddenWindow : NativeWindow, IDisposable
        {
            private static int WM_HOTKEY = 0x0312;

            public event EventHandler<KeyPressedEventArgs> OnKeyPressed = delegate { };

            public HiddenWindow()
            {
                CreateHandle(new CreateParams());
            }

            protected override void WndProc(ref Message m)
            {
                base.WndProc(ref m);

                if (m.Msg == WM_HOTKEY)
                {
                    Keys key = (Keys)(((int)m.LParam >> 16) & 0xFFFF);
                    ModifierKeys modifier = (ModifierKeys)((int)m.LParam & 0xFFFF);

                    OnKeyPressed.Invoke(this, new KeyPressedEventArgs(modifier, key));
                }
            }

            public void Dispose()
            {
                DestroyHandle();
            }
        }

        public class KeyPressedEventArgs : EventArgs
        {
            public ModifierKeys Modifier { get; private set; }
            public Keys Key { get; private set; }

            public KeyPressedEventArgs(ModifierKeys modifier, Keys key)
            {
                Modifier = modifier;
                Key = key;
            }
        }

        public HotKeyManager()
        {
            _window.OnKeyPressed += _window_OnKeyPressed;
        }

        private void _window_OnKeyPressed(object sender, KeyPressedEventArgs e)
        {
            OnKeyPressed.Invoke(this, e);
        }

        public int RegisterHotKey(ModifierKeys modifier, Keys key)
        {
            _currentId += 1;
            if (!RegisterHotKey(_window.Handle, _currentId, (uint)modifier, (uint)key))
                throw new InvalidOperationException("Couldn't register hot key.");
            return _currentId;
        }

        public void UnregisterHotKey(int handle)
        {
            UnregisterHotKey(_window.Handle, handle);
        }

        public void Dispose()
        {
            for (int i = _currentId; i > 0; i--)
            {
                UnregisterHotKey(_window.Handle, i);
            }

            _window.Dispose();
        }
    }
}
