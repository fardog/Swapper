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

    public class MouseButtonManager
    {
        private ButtonState primaryButton;

        public event EventHandler<MouseButtonChangedEventArgs> MouseButtonChanged = delegate { };

        [DllImport("user32.dll")]
        public static extern bool SwapMouseButton(bool bSwap);

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
                return primaryButton;
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

                primaryButton = value;
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
