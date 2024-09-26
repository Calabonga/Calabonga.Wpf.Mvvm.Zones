using Calabonga.Wpf.Mvvm.Zones;
using Calabonga.Wpf.MvvmMdi.Screens;
using Calabonga.Wpf.MvvmMdi.ViewModels;
using Calabonga.Wpf.MvvmMdi.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Calabonga.Wpf.MvvmMdi.Core
{
    internal static class DependencyContainer
    {
        internal static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            services.AddLogging(options =>
            {
                options.AddSerilog(dispose: true);
                options.AddDebug();
            });

            services.AddScoped<MainWindow>();
            services.AddSingleton<MainWindowViewModel>();
            services.AddScoped<IVersionService, VersionService>();

            // factory
            services.AddScoped<IMvvmObjectFactory, MvvmObjectFactory>();

            // Zones
            services.AddScoped<IMainZoneView, MainZoneView>();
            services.AddScoped<IMainZoneViewModel, MainZoneViewModel>();

            services.AddScoped<IDetailsZoneView, DetailZoneView>();
            services.AddScoped<IDetailsZoneViewModel, DetailsZoneViewModel>();

            services.AddZones();

            return services.BuildServiceProvider();
        }
    }

}