using DataLayer.Services;
using Microsoft.Extensions.DependencyInjection;

namespace WorldCupManager
{
    public partial class MainForm : Form
    {
        /* I DON'T THINK THESE ARE NEEDED IN THE FORM
       
        // Path to Config folder
        private static readonly string configFolderPath
            = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config");

        // Path to user selected settings
        private static readonly string userSettingsPath
            = Path.Combine(configFolderPath, "userSettings.txt");

        // Path to config settings
        private static readonly string configSettingsPath
            = Path.Combine(configFolderPath, "config.txt");
        */

        private IDataService _dataService;

        // Inject Service Provider and IDataService through constructor
        public MainForm(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _dataService = serviceProvider.GetRequiredService<IDataService>();
        }

    }
}
