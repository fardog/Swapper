using System;
using System.Windows.Input;

namespace Swapper
{
    public interface ConfigurationManager
    {
        KeyGesture LeftGesture { get; set; }

        KeyGesture RightGesture { get; set; }

        event EventHandler OnConfigurationChange;
    }

    public class StaticConfigurationManager : ConfigurationManager
    {
        private static KeyGesture _leftGesture = new KeyGesture(Key.Left, ModifierKeys.Control | ModifierKeys.Shift);
        private static KeyGesture _rightGesture = new KeyGesture(Key.Right, ModifierKeys.Control | ModifierKeys.Shift);

        KeyGesture ConfigurationManager.LeftGesture {
            get => _leftGesture;
            set
            {
                _leftGesture = value;
                OnConfigurationChange.Invoke(this, new EventArgs());
            }
        }
        KeyGesture ConfigurationManager.RightGesture {
            get => _rightGesture;
            set
            {
                _rightGesture = value;
                OnConfigurationChange.Invoke(this, new EventArgs());
            }
        }

        public event EventHandler OnConfigurationChange = delegate { };
    }
}
