using DataLayer.Services;
using Microsoft.Extensions.DependencyInjection;
using Utilities;
using System;
using System.Drawing.Text;
using System.Windows.Forms;

namespace WorldCupManager
{
    internal static class Program
    {
        // Path to Config folder
        private static readonly string configFolderPath
            = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config");

        // Path to userSettings file
        private static readonly string userSettingsPath
            = Path.Combine(configFolderPath, "userSettings.txt");

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            // Create Config folder if non existent
            if (!Directory.Exists(configFolderPath))
                Directory.CreateDirectory(configFolderPath);

            // Open entry form if no existing settings are present
            if (!File.Exists(userSettingsPath))
            {
                using EntryForm entryForm = new EntryForm();

                DialogResult result = entryForm.ShowDialog();

                if (result == DialogResult.Cancel)
                {
                    Application.Exit();
                    return;
                }
            }

            // Instantiate data service based on config.txt
            IDataService _service
                = Utility.UseApiService()
                ? new ApiService(new HttpClient())
                : new LocalDataService();


            // Create service collection and configure services
            IServiceProvider serviceProvider = new ServiceCollection()
                .AddSingleton<IDataService>(_service)
                .AddSingleton<MainForm>()
                .BuildServiceProvider();

            // Retrieve MainForm with correct IDataService already injected
            MainForm mainForm = serviceProvider.GetRequiredService<MainForm>();

            // Pass pre-configured mainForm instead of "new MainForm()"
            Application.Run(mainForm);
        }
    }
}