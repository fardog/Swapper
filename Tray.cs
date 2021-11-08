﻿using Swapper.Properties;
using System;
using System.Windows.Forms;

namespace Swapper
{
    public class Tray: ApplicationContext
    {
        private readonly NotifyIcon _trayIcon;
        private readonly MouseButtonManager _buttonManager;
        private AboutBox? _aboutBox;

        public Tray()
        {
            // Initialize Tray Icon
            _trayIcon = new NotifyIcon()
            {
                ContextMenuStrip = new ContextMenuStrip(),
                Visible = true,
            };
            _trayIcon.MouseClick += TrayItem_MouseClick;

            ToolStripMenuItem aboutMenuItem = new("About", null, MenuItem_About, "About");
            ToolStripMenuItem exitMenuItem = new("Exit", null, MenuItem_Exit, "Exit");
            _trayIcon.ContextMenuStrip.Items.Add(aboutMenuItem);
            _trayIcon.ContextMenuStrip.Items.Add(exitMenuItem);

            _buttonManager = new MouseButtonManager();
            _buttonManager.MouseButtonChanged += ButtonManager_MouseButtonChanged;
            UpdateUI(_buttonManager.PrimaryButton);
        }

        private void ButtonManager_MouseButtonChanged(object? sender, MouseButtonChangedEventArgs e)
        {
            UpdateUI(e.PrimaryButton);
        }

        private void UpdateUI(ButtonState primaryButton)
        {
            if (primaryButton == ButtonState.Left)
            {
                _trayIcon.Text = "Primary Button: Left";
                _trayIcon.Icon = Resources.TrayIconDarkLeft;
            }
            else if (primaryButton == ButtonState.Right)
            {
                
                _trayIcon.Text = "Primary Button: Right";
                _trayIcon.Icon = Resources.TrayIconDarkRight;
            }
        }

        private void TrayItem_MouseClick(object? sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            _buttonManager.Swap();
        }

        private void MenuItem_About(object? sender, EventArgs e)
        {
            if (_aboutBox == null)
            {
                _aboutBox = new AboutBox();
                _aboutBox.Show();
                _aboutBox.FormClosed += (s, ee) => { _aboutBox = null; };
            }
        }

        private void MenuItem_Exit(object? sender, EventArgs e)
        {
            _trayIcon.Visible = false;
            Application.Exit();
        }
    }
}
