using DataLayer.Services;
using Microsoft.Extensions.DependencyInjection;

namespace WorldCupManager
{
    public partial class MainForm : Form
    {
        // Path to Config folder
        private static readonly string configFolderPath
            = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config");

        // Path to user selected settings
        private static readonly string userSettingsPath
            = Path.Combine(configFolderPath, "userSettings.txt");

        // Path to config settings
        private static readonly string configSettingsPath
            = Path.Combine(configFolderPath, "config.txt");

        private readonly IServiceProvider _serviceProvider;
        private IDataService _dataService;

        // Inject Service Provider and IDataService through constructor
        public MainForm(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            InitializeComponent();

            _dataService = _serviceProvider.GetRequiredService<IDataService>();
        }

    }
}
