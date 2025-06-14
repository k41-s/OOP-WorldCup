using DataLayer.Services;
using System.Globalization;
using System.Net.Http;
using System.Windows;
using Utilities;
using WorldCupStatsViewer.Views;

namespace WorldCupStatsViewer;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private IDataService _dataService =
        Utility.UseApiService()
        ? new ApiService(new HttpClient())
        : new LocalDataService();

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        // Prevent app from shutting down when settings window closes
        Application.Current.ShutdownMode = ShutdownMode.OnExplicitShutdown;

        bool settingsValid = InitUserSettings();

        // Only show overview if settings saved properly
        if (settingsValid)
            ShowNationalTeamOverview();
        else
            Shutdown();
    }

    private bool InitUserSettings()
    {
        IDictionary<string, string> settings = Utility.LoadUserSettings();

        // Show initial settings window if missing any
        if (!settings.ContainsKey("Category") || !settings.ContainsKey("Language") || !settings.ContainsKey("DisplayMode"))
        {
            var initialSettingsView = new InitialSettingsView();
            bool? result = initialSettingsView.ShowDialog();

            // not true
            if (result != true)
            {
                return false; // User closed the window without saving
            }

            // Reload settings after saving
            settings = Utility.LoadUserSettings();
        }

        // Apply language
        if (settings.TryGetValue("Language", out string? language))
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo(language == "Croatian" ? "hr" : "en");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(language == "Croatian" ? "hr" : "en");
        }

        return true;
    }

    private void ShowNationalTeamOverview()
    {
        Window window = new NationalTeamView(_dataService);
        Application.Current.MainWindow = window;

        Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
        window.Show();
    }
}

