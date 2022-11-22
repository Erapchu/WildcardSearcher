using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using WildcardSearcher.Lucene.Extensions;
using WildcardSearcher.ViewModels;
using WildcardSearcher.Views;

namespace WildcardSearcher
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly ServiceProvider _serviceProvider;
        public IServiceProvider Services => _serviceProvider;

        public App()
        {
            _serviceProvider = ConfigureServices();
        }

        private static ServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            services.AddTransient<MainWindow>();
            services.AddTransient<MainWindowViewModel>();
            services.AddTransient<AddWordWindow>();
            services.AddTransient<AddWordViewModel>();
            services.RegisterWildcardSearcher();

            return services.BuildServiceProvider();
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            InitializeComponent();

            var mainWindow = Services.GetService<MainWindow>();
            mainWindow.Show();
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            _serviceProvider.Dispose();
        }
    }
}
