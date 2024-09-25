using System.Windows;
using Calabonga.Wpf.MvvmMdi.Core;
using Calabonga.Wpf.MvvmMdi.ViewModels;
using Calabonga.Wpf.MvvmMdi.Views;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Calabonga.Wpf.MvvmMdi
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.File("logs/local-.log", rollingInterval: RollingInterval.Day, rollOnFileSizeLimit: true, shared: true)
                .CreateLogger();

            Services = DependencyContainer.ConfigureServices();
        }

        /// <summary>
        /// Gets the current <see cref="App"/> instance in use
        /// </summary>
        public new static App Current => (App)Application.Current;

        /// <summary>
        /// Gets the <see cref="IServiceProvider"/> instance to resolve application services.
        /// </summary>
        internal IServiceProvider Services { get; set; }

        /// <summary>Raises the <see cref="E:System.Windows.Application.Startup" /> event.</summary>
        /// <param name="e">A <see cref="T:System.Windows.StartupEventArgs" /> that contains the event data.</param>
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var shell = Services.GetService<MainWindow>();
            var shellViewModel = Services.GetService<MainWindowViewModel>();

            if (shellViewModel is null || shell is null)
            {
                return;
            }

            shell.DataContext = shellViewModel;

            shell.Show();
        }

        /// <summary>
        /// Requires to load Application Resources
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Application_Startup(object sender, StartupEventArgs e) => InitializeComponent();

        protected override void OnExit(ExitEventArgs e)
        {
            Log.Information("Application shutdown successful");
            Log.CloseAndFlush();
            base.OnExit(e);
        }
    }
}