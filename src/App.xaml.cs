using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using TABFRET.Services;
using TABFRET.ViewModels;

namespace TABFRET
{
    public partial class App : Application
    {
        public static ServiceProvider? ServiceProvider { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            var services = new ServiceCollection();

            // Register services and viewmodels
            services.AddSingleton<IMidiParser, MidiParserDryWetMidi>();
            services.AddSingleton<ISettingsService, SettingsService>();
            services.AddTransient<MainViewModel>();

            ServiceProvider = services.BuildServiceProvider();

            base.OnStartup(e);
        }
    }
}
