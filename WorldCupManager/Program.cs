using DataLayer.Services;
using Microsoft.Extensions.DependencyInjection;
using Utilities;

namespace WorldCupManager
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            // Create Config folder if non existent
            if (!Directory.Exists(Utility.configFolderPath))
                Directory.CreateDirectory(Utility.configFolderPath);

            // Open entry form if no existing settings are present
            if (!File.Exists(Utility.userSettingsPath))
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
                .AddSingleton<FavouriteTeamForm>()
                .BuildServiceProvider();

            // Run favouriteTeamForm to select favourite team
            if (!File.Exists(Utility.favouriteTeamPath))
            {
                using FavouriteTeamForm form 
                    = serviceProvider.GetRequiredService<FavouriteTeamForm>();

                DialogResult result = form.ShowDialog();

                // Ensure proper closure of application if form cancelled
                if (result == DialogResult.Cancel)
                {
                    Application.Exit();
                    return;
                }
            }

            // Retrieve MainForm with correct IDataService already injected
            MainForm mainForm = serviceProvider.GetRequiredService<MainForm>();

            // Pass pre-configured mainForm instead of "new MainForm()"
            Application.Run(mainForm);
        }
    }
}