using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Swapper
{
    [Flags]
    public enum Modifiers
    {
        Alt = 0x0001,
        Control = 0x0002,
        Shift = 0x0004,
        Windows = 0x0008,
    }

    public static class ModifiersConversion
    {
        private static readonly Dictionary<string, Modifiers> _modifiers = new()
        {
            { "alt", Modifiers.Alt },
            { "control", Modifiers.Control },
            { "ctrl", Modifiers.Control },
            { "shift", Modifiers.Shift },
            { "windows", Modifiers.Windows },
            { "win", Modifiers.Windows },
        };

        public static Modifiers FromString(string value)
        {
            if (_modifiers.TryGetValue(value.ToLower(), out var m))
                return m;
            
            throw new ArgumentException($"invalid modifier: {value}");
        }
    }


    public class HotKey
    {
        public Modifiers Modifiers;
        public Keys Key;

        public HotKey(Modifiers modifiers, Keys key)
        {
            Modifiers = modifiers;
            Key = key;
        }

        public HotKey(string spec)
        {
            Modifiers? modifiers = null;
            Keys? key = null;

            var parts = spec.Split("+").Select(s => s.Trim());
            foreach (var part in parts)
            {
                try
                {
                    modifiers |= ModifiersConversion.FromString(part);
                    continue;
                }
                catch (ArgumentException) { }

                foreach (Keys k in Enum.GetValues(typeof(Keys)))
                    if (k.ToString() == part.ToLower())
                        Key = k;
            }

            if (modifiers == null)
                throw new ArgumentException("no modifiers present in spec");
            Modifiers = (Modifiers)modifiers;

            if (key == null)
                throw new ArgumentException("no key present in spec");
            Key = (Keys)key;
        }

        public override string ToString()
        {
            List<string> parts = new();
            if (Modifiers.HasFlag(Modifiers.Windows))
                parts.Add("Win");
            if (Modifiers.HasFlag(Modifiers.Control))
                parts.Add("Ctrl");
            if (Modifiers.HasFlag(Modifiers.Alt))
                parts.Add("Alt");
            if (Modifiers.HasFlag(Modifiers.Shift))
                parts.Add("Shift");

            parts.Add(Key.ToString());

            return string.Join("+", parts);
        }

        public override bool Equals(object? obj)
        {
            if (obj is HotKey other)
            {
                return other.Key == Key && other.Modifiers == Modifiers;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return Modifiers.GetHashCode() + Key.GetHashCode();
        }

        public static bool operator ==(HotKey? a, HotKey? b)
        {
            return a?.Equals(b) ?? false;
        }

        public static bool operator !=(HotKey? a, HotKey? b)
        {
            return !a?.Equals(b) ?? false;
        }
    }

    public class HotKeyEventArgs : EventArgs
    {
        public HotKey HotKey { get; private set; }

        public HotKeyEventArgs(HotKey hotKey) => HotKey = hotKey;

        public HotKeyEventArgs(Modifiers modifiers, Keys key)
        {
            HotKey = new HotKey(modifiers, key);
        }
    }

    public class HotKeyManager : IDisposable
    {
        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        private readonly HiddenWindow _window = new();
        private int _currentId;

        public event EventHandler<HotKeyEventArgs> HotKeyPressed = delegate { };

        public class HiddenWindow : NativeWindow, IDisposable
        {
            private static readonly int WM_HOTKEY = 0x0312;

            public event EventHandler<HotKeyEventArgs> HotKeyPressed = delegate { };

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
                    Modifiers modifiers = (Modifiers)((int)m.LParam & 0xFFFF);

                    HotKeyPressed.Invoke(this, new HotKeyEventArgs(modifiers, key));
                }
            }

            public void Dispose()
            {
                DestroyHandle();
                GC.SuppressFinalize(this);
            }
        }

        public HotKeyManager() => _window.HotKeyPressed += _window_OnKeyGesture;

        private void _window_OnKeyGesture(object? sender, HotKeyEventArgs e)
        {
            HotKeyPressed.Invoke(this, e);
        }

        public int RegisterHotKey(HotKey hotKey)
        {
            return RegisterHotKey(hotKey.Modifiers, hotKey.Key);
        }

        public int RegisterHotKey(Modifiers modifiers, Keys key)
        {
            _currentId += 1;
            if (!RegisterHotKey(_window.Handle, _currentId, (uint)modifiers, (uint)key))
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
            GC.SuppressFinalize(this);
        }
    }
}
