using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Swapper;

public class HotKeyEventArgs : EventArgs
{
    public HotKey HotKey { get; }

    public HotKeyEventArgs(HotKey hotKey) => HotKey = hotKey;

    public HotKeyEventArgs(Modifiers modifiers, Keys key)
    {
        HotKey = new HotKey(modifiers, key);
    }
}

public interface IHotKeyManager
{
    int RegisterHotKey(HotKey hotKey);
    int RegisterHotKey(Modifiers modifiers, Keys key);

    void UnregisterHotKey(int handle);

    event EventHandler<HotKeyEventArgs> HotKeyPressed;
}

public class HotKeyManager : IDisposable, IHotKeyManager
{
    [DllImport("user32.dll")]
    private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

    [DllImport("user32.dll")]
    private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

    private readonly HiddenWindow _window = new();
    private int _currentId;

    public event EventHandler<HotKeyEventArgs> HotKeyPressed = delegate { };

    private sealed class HiddenWindow : NativeWindow, IDisposable
    {
        private const int WM_HOTKEY = 0x0312;

        public event EventHandler<HotKeyEventArgs> HotKeyPressed = delegate { };

        public HiddenWindow()
        {
            CreateHandle(new CreateParams());
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (m.Msg != WM_HOTKEY) return;

            Keys key = (Keys)(((int)m.LParam >> 16) & 0xFFFF);
            Modifiers modifiers = (Modifiers)((int)m.LParam & 0xFFFF);

            HotKeyPressed.Invoke(this, new HotKeyEventArgs(modifiers, key));
        }

        public void Dispose()
        {
            DestroyHandle();
            GC.SuppressFinalize(this);
        }
    }

    public HotKeyManager() => _window.HotKeyPressed += Window_OnKeyGesture;

    private void Window_OnKeyGesture(object? sender, HotKeyEventArgs e)
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
