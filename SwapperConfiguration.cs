using System;
using System.Windows.Input;

namespace Swapper
{
    public interface SwapperConfiguration
    {
        KeyGesture LeftGesture { get; set; }

        KeyGesture RightGesture { get; set; }

        event EventHandler OnConfigurationChange;
    }

    public class StaticConfigurationManager : SwapperConfiguration
    {
        private KeyGesture _leftGesture = new KeyGesture(Key.Left, ModifierKeys.Control | ModifierKeys.Shift);
        private KeyGesture _rightGesture = new KeyGesture(Key.Right, ModifierKeys.Control | ModifierKeys.Shift);

        KeyGesture SwapperConfiguration.LeftGesture {
            get => _leftGesture;
            set
            {
                _leftGesture = value;
                OnConfigurationChange.Invoke(this, new EventArgs());
            }
        }
        KeyGesture SwapperConfiguration.RightGesture {
            get => _rightGesture;
            set
            {
                _rightGesture = value;
                OnConfigurationChange.Invoke(this, new EventArgs());
            }
        }

        public event EventHandler OnConfigurationChange = delegate { };
    }

    public class InvalidConfigurationException : Exception { }

    public class DynamicConfigurationManager : SwapperConfiguration
    {
        private KeyGesture _leftGesture;
        private KeyGesture _rightGesture;

        public event EventHandler OnConfigurationChange = delegate { };

        public DynamicConfigurationManager()
        {
            var converter = new KeyGestureConverter();
            if (Properties.Settings.Default.LeftGesture != "")
                _leftGesture = (KeyGesture)converter.ConvertFromString(Properties.Settings.Default.LeftGesture);
            if (Properties.Settings.Default.RightGesture != "")
                _rightGesture = (KeyGesture)converter.ConvertFromString(Properties.Settings.Default.RightGesture);
        }

        private void _set(ref KeyGesture gesture, Action<string> set,  KeyGesture value)
        {
            gesture = value;
            set(value == null ? "" : value.GetDisplayStringForCurrentCulture());
            OnConfigurationChange.Invoke(this, new EventArgs());

            Properties.Settings.Default.Save();
        }

        public KeyGesture LeftGesture
        {
            get => _leftGesture;
            set => _set(ref _leftGesture, (v) => Properties.Settings.Default.LeftGesture = v, value);
        }
        public KeyGesture RightGesture
        {
            get => _rightGesture;
            set => _set(ref _rightGesture, (v) => Properties.Settings.Default.RightGesture = v, value);
        }
    }
}
