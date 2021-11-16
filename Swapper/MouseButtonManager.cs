#pragma warning disable CA1416 // Validate platform compatibility
using Microsoft.Win32;
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Swapper
{
    public enum ButtonState
    {
        Left,
        Right
    }

    public class MouseButtonChangedEventArgs: EventArgs
    {
        public ButtonState PrimaryButton { get; set; }

        public MouseButtonChangedEventArgs(ButtonState primaryButton)
        {
            this.PrimaryButton = primaryButton;
        }
    }

    public interface IMouseButtonManager
    {
        ButtonState PrimaryButton { get; set; }
        void Swap();

        event EventHandler<MouseButtonChangedEventArgs> MouseButtonChanged;
    }

    public class MouseButtonManager : IMouseButtonManager
    {
        private ButtonState _primaryButton;

        public event EventHandler<MouseButtonChangedEventArgs> MouseButtonChanged = delegate { };

        [DllImport("user32.dll")]
        private static extern bool SwapMouseButton(bool bSwap);

        public MouseButtonManager()
        {
            SystemEvents.UserPreferenceChanged += new UserPreferenceChangedEventHandler(System_UserPreferenceChanged);

            UpdateCurrentPrimaryButton();
        }

        public void Swap()
        {
            if (PrimaryButton == ButtonState.Left) PrimaryButton = ButtonState.Right;
            else if (PrimaryButton == ButtonState.Right) PrimaryButton = ButtonState.Left;
        }

        public ButtonState PrimaryButton
        {
            get
            {
                return _primaryButton;
            }
            set
            {
                if (value == ButtonState.Left)
                {
                    SwapMouseButton(false);
                }
                else if (value == ButtonState.Right)
                {
                    SwapMouseButton(true);
                }

                _primaryButton = value;
                MouseButtonChanged?.Invoke(this, new MouseButtonChangedEventArgs(value));
            }
        }


        private void System_UserPreferenceChanged(object? sender, UserPreferenceChangedEventArgs e)
        {
            if (e.Category == UserPreferenceCategory.Mouse)
            {
                UpdateCurrentPrimaryButton();
            }
        }

        private void UpdateCurrentPrimaryButton()
        {
            PrimaryButton = SystemInformation.MouseButtonsSwapped ? ButtonState.Right : ButtonState.Left;
        }
    }
}
#pragma warning restore CA1416 // Validate platform compatibility
