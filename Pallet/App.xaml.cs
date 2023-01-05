using CodingSeb.Localization;
using CodingSeb.Localization.Loaders;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pallet.Extensions;
using Pallet.ExternalDatabase;
using Pallet.InternalDatabase;
using Pallet.Services;
using Pallet.Services.Draw;
using Pallet.Services.Draw.Interface;
using Pallet.Services.Language;
using Pallet.Services.UserDialog;
using Pallet.Services.UserDialog.Interfaces;
using Pallet.ViewModels;
using System.Configuration;
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

        public static Window? ActivedWindow => Current.Windows.Cast<Window>().FirstOrDefault(w => w.IsActive);

        public static Window? FocusedWindow => Current.Windows.Cast<Window>().FirstOrDefault(w => w.IsFocused);

        public static Window? CurrentWindow => FocusedWindow ?? ActivedWindow;

        public static bool IsDesignMode { get; private set; } = true;

        public static string? CurrentDirectory => IsDesignMode
            ? Path.GetDirectoryName(GetSourceCodePath())
            : Environment.CurrentDirectory;

        #endregion Fields

        #region Host

        private static IHost? __Host;

        public static IHost Host
        {
            get
            {
                if (__Host is not null) return __Host;
                else
                {
                    IHostBuilder hb = Program.CreateHostBuilder(Environment.GetCommandLineArgs());
                    try
                    {
                        __Host = hb.Build();
                    }
                    catch (Exception e) { e.ExceptionToString(); }
                    return __Host;
                }
            }
        }

        public static IServiceProvider Services => Host.Services;


        #endregion Host
        protected override async void OnStartup(StartupEventArgs e)
        {
            IsDesignMode = false;
            var host = Host;
            LocalizationLoader.Instance.FileLanguageLoaders.Add(new JsonFileLoader());
            LocalizationLoader.Instance.AddDirectory(@"Localizations");

            using (var scope = Services.CreateScope())
            {
                try
                {
                    await scope.ServiceProvider.GetRequiredService<ExternalDbInitializer>().InitializeAsync();
                }
                catch
                {
                    MessageBox.Show(Loc.Tr("Database.Errors.ExternalDdNotConnected"), "External database", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                try
                {
                    await scope.ServiceProvider.GetRequiredService<InternalDbInitializer>().InitializeAsync();
                }
                catch
                {
                    MessageBox.Show(Loc.Tr("Database.Errors.InternalDdNotConnected"), "Internal database", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            try
            {
                base.OnStartup(e);
                await host.StartAsync().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                ex.ExceptionToString();
            }
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            var host = Host;
            await host.StopAsync().ConfigureAwait(false);
            host.Dispose();
            __Host = null;
        }

        private static string GetSourceCodePath([CallerFilePath] string Path = "") => Path; // подстановка пути в дизайнмоде

        public static void ConfigureServices(HostBuilderContext host, IServiceCollection services)
        {
            services
                .AddSingleton<IConfiguration>(provider => host.Configuration)
                .RegisterInternalDatabase(host.Configuration.GetSection("Database"))
                .RegisterExternalDatabase(host.Configuration.GetSection("Database"))
                .RegisterServices()
                .AddTransient<IDrawer, Drawer>()
                .AddSingleton<IManagerLanguage, ManagerLanguage>()
                .AddSingleton<IUserDialogService, UserDialogService>()
                .RegisterViewModels();
        }
    }
}