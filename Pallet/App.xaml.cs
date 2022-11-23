using CodingSeb.Localization.Loaders;
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

        protected override async void OnStartup(StartupEventArgs e)
        {
            IsDesignMode = false;
            var host = Host;

            using (var scope = Services.CreateScope())
            {
                scope.ServiceProvider.GetRequiredService<DbInitializer>().InitializeAsync().ConfigureAwait(false);
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

            LocalizationLoader.Instance.FileLanguageLoaders.Add(new JsonFileLoader());
            LocalizationLoader.Instance.AddDirectory(@"Localizations");
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            string str = "Exit";
            str.CheckStage();
            base.OnExit(e);
            var host = Host;
            await host.StopAsync().ConfigureAwait(false);
            host.Dispose();
            __Host = null;
        }

        #endregion Host

        private static string GetSourceCodePath([CallerFilePath] string Path = "") => Path; // подстановка пути в дизайнмоде

        public static void ConfigureServices(HostBuilderContext host, IServiceCollection services)
        {
            services.RegisterInternalDatabase(host.Configuration.GetSection("Database"));
            services.RegisterExternalDatabase(host.Configuration.GetSection("Database"));
            services.RegisterServices();
            services.AddTransient<IDrawer, Drawer>();
            services.AddSingleton<IManagerLanguage, ManagerLanguage>();
            services.AddSingleton<IUserDialogService, UserDialogService>();
            services.RegisterViewModels();
        }
    }
}