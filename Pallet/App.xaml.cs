﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pallet.Data;
using Pallet.Services;
using Pallet.ViewModels;
using System.IO;
using System.Runtime.CompilerServices;

namespace Pallet
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        #region Fields

        public static Window ActivedWindow => Current.Windows.Cast<Window>().FirstOrDefault(w => w.IsActive);

        public static Window FocusedWindow => Current.Windows.Cast<Window>().FirstOrDefault(w => w.IsFocused);

        public static Window CurrentWindow => FocusedWindow ?? ActivedWindow;

        public static bool IsDesignMode { get; private set; } = true;

        public static string? CurrentDirectory => IsDesignMode
            ? Path.GetDirectoryName(GetSourceCodePath())
            : Environment.CurrentDirectory;

        #endregion Fields

        #region Host

        private static IHost? __Host;

        public static IHost Host => __Host ??= Program
            .CreateHostBuilder(Environment.GetCommandLineArgs())
            .Build();

        public static IServiceProvider Services => Host.Services;

        protected override async void OnStartup(StartupEventArgs e)
        {
            IsDesignMode = false;
            var host = Host;

            using (var scope = Services.CreateScope())
                await scope.ServiceProvider.GetRequiredService<DbInitializer>().InitializeAsync();

            base.OnStartup(e);
            await host.StartAsync().ConfigureAwait(false);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            var host = Host;
            await host.StopAsync().ConfigureAwait(false);
            host.Dispose();
            __Host = null;
        }

        #endregion Host

        private static string GetSourceCodePath([CallerFilePath] string Path = "") => Path; // подстановка пути в дизайнмоде

        public static void ConfigureServices(HostBuilderContext host, IServiceCollection services)
            => services
                    .RegisterDatabase(host.Configuration.GetSection("Database"))
                    .RegisterServices()
                    .RegisterViewModels();

        public static void ChangeCulture(CultureInfo newCulture)
        {
            Thread.CurrentThread.CurrentCulture = newCulture;
            Thread.CurrentThread.CurrentUICulture = newCulture;
            var oldWindow = App.Current.MainWindow;
            App.Current.MainWindow = new MainWindow();
            App.Current.MainWindow.Show();

            oldWindow.Close();
        }
    }
}