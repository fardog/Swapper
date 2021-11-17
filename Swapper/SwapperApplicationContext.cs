using System.Windows.Forms;

namespace Swapper
{
    public class SwapperApplicationContext: ApplicationContext
    {
        private readonly ApplicationManager _application;

        public SwapperApplicationContext()
        {
            ConfigurationManager configurationManager;
            try
            {
                configurationManager = new ConfigurationManager();
            }
            catch (InvalidConfigurationValueException e)
            {
                MessageBox.Show(string.Format(Resources.en_US.ErrorMessage_LoadingConfiguration, e.Message));
                ConfigurationManager.Reset();
                configurationManager = new ConfigurationManager();
            }

            MouseButtonManager buttonManager = new();
            HotKeyManager hotKeyManager = new();
            Tray tray = new();

            _application = new ApplicationManager(configurationManager, buttonManager, hotKeyManager, tray);
            _application.ApplicationExit += (s, ee) => Application.Exit();
        }
    }
}
