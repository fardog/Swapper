using System;
using System.Windows.Input;

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
    }

    public class KeyGestureEventArgs : EventArgs
    {
        public KeyGesture Gesture { get; private set; }

        public KeyGestureEventArgs(KeyGesture gesture)
        {
            Gesture = gesture;
        }
    }
}
