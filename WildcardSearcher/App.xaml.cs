﻿using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using WildcardSearcher.ViewModels;
using WildcardSearcher.Views;

namespace WildcardSearcher
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IServiceProvider Services { get; }

        public App()
        {
            Services = ConfigureServices();
        }

        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            services.AddTransient<MainWindow>();
            services.AddTransient<MainWindowViewModel>();

            return services.BuildServiceProvider();
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            InitializeComponent();

            var mainWindow = Services.GetService<MainWindow>();
            mainWindow.Show();
        }
    }
}