using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Pallet
{
    public static class Program
    {
        [STAThread]
        public static void Main()
        {
            var app = new App();
            app.InitializeComponent();

            app.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] Args)
        {
            "create  host builder ".CheckStage();

            IHostBuilder hostBuilder = Host.CreateDefaultBuilder(Args);
            "CreateDefaultBuilder".CheckStage();

            hostBuilder.UseContentRoot(App.CurrentDirectory);
            "UseContentRoot".CheckStage();

            try
            {
                hostBuilder.ConfigureAppConfiguration((host, cfg) =>
                {
                    cfg.SetBasePath(App.CurrentDirectory);
                    "SetBasePath".CheckStage();
                    cfg.AddJsonFile("appsettings.json", true, true);
                    "AddJsonFile".CheckStage();
                });
                "ConfigureAppConfiguration".CheckStage();
            }
            catch (Exception e) { e.ExceptionToString(); }

            hostBuilder.ConfigureServices(App.ConfigureServices);
            "ConfigureServices".CheckStage();

            return hostBuilder;
        }
    }
}