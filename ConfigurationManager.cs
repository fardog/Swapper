using System;

namespace Swapper
{
    public interface ISwapperConfiguration
    {
        HotKey? HotKeyLeftPrimary { get; set; }

        HotKey? HotKeyRightPrimary { get; set; }

        event EventHandler ConfigurationChange;
    }

    class ConfigurationManager : ISwapperConfiguration
    {
        private HotKey? _hotKeyLeftPrimary;
        private HotKey? _hotKeyRightPrimary;

        public HotKey? HotKeyLeftPrimary { get => _hotKeyLeftPrimary; set => SetHotKey(ref _hotKeyLeftPrimary, (s) => Properties.Settings.Default.HotKey_LeftPrimary = s, value); }
        public HotKey? HotKeyRightPrimary { get => _hotKeyRightPrimary; set => SetHotKey(ref _hotKeyRightPrimary, (s) => Properties.Settings.Default.HotKey_RightPrimary = s, value); }

        public event EventHandler ConfigurationChange = delegate { };

        private void SetHotKey(ref HotKey? store, Action<string> writeConfiguration, HotKey? value)
        {
            store = value;
            writeConfiguration(value?.ToString() ?? "");
            Properties.Settings.Default.Save();
            ConfigurationChange(this, new EventArgs());
        }

        public ConfigurationManager()
        {
            if (Properties.Settings.Default.HotKey_LeftPrimary != "")
                _hotKeyLeftPrimary = new HotKey(Properties.Settings.Default.HotKey_LeftPrimary);
            if (Properties.Settings.Default.HotKey_RightPrimary != "")
                _hotKeyRightPrimary = new HotKey(Properties.Settings.Default.HotKey_RightPrimary);
        }
    }
}
