using System;
using System.Windows.Input;
using System.Windows.Forms;
using System.Globalization;

namespace Swapper
{
    public static class KeyUtils
    {
        public static (ModifierKeys, Key) ToKeys(string keySpec)
        {
            var converter = new KeyGestureConverter();
            if (converter.ConvertFrom(keySpec) is KeyGesture result)
            {
                return (result.Modifiers, result.Key);
            }
            throw new ArgumentException($"invalid keySpec: {keySpec}");
        }

        public static string FromKeys(ModifierKeys modifiers, Key key)
        {
            KeyGesture gesture = new KeyGesture(key, modifiers);
            return gesture.ToString();
        }

        public static KeyGesture ToKeyGesture(System.Windows.Forms.KeyEventArgs args)
        {
            ModifierKeys modifiers = ModifierKeys.None;
            if (args.Alt)
                modifiers |= ModifierKeys.Alt;
            if (args.Control)
                modifiers |= ModifierKeys.Control;
            if (args.Shift)
                modifiers |= ModifierKeys.Shift;

            return new KeyGesture(KeyInterop.KeyFromVirtualKey(args.KeyValue), modifiers);
        }

        public static KeyGesture ToKeyGesture(ModifierKeys modifiers, Keys key)
        {
            return new KeyGesture(KeyInterop.KeyFromVirtualKey((int)key), modifiers);
        }
    }

    public static class KeyGestureExtensions
    {
        public static bool Equivalent(this KeyGesture self, KeyGesture other)
        {
            return self.Key == other.Key && self.Modifiers == other.Modifiers;
        }

        public static string GetDisplayStringForCurrentCulture(this KeyGesture self)
        {
            return self.GetDisplayStringForCulture(CultureInfo.CurrentUICulture);
        }

        public static bool Complete(this KeyGesture self)
        {
            return (int)self.Key >= (int)Key.Space
                && (int)self.Key <= (int)Key.F24
                && (int)self.Modifiers > 0;
        }
    }

    public class IncompleteKeyGestureException : Exception { }

    public class KeyGestureEventArgs : EventArgs
    {
        public KeyGesture Gesture { get; private set; }

        public KeyGestureEventArgs(KeyGesture gesture)
        {
            Gesture = gesture;
        }
    }
}
