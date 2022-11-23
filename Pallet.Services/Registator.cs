using BlazorServerPallet.Services.Logging;
using Microsoft.Extensions.DependencyInjection;
using Pallet.Services.Logging.Interfaces;
using Pallet.Services.Managers;
using Pallet.Services.Managers.Interfaces;
using Pallet.Services.OPC;
using Pallet.Services.OPC.Interfaces;

namespace Pallet.Services;

public static class Registator
{
    public static IServiceCollection RegisterServices(this IServiceCollection services) =>
        services
            .AddSingleton<ILogService, LogService>()
            .AddSingleton<IManagerProfiles, ManagerProfiles>()
            .AddSingleton<IManagerUser, ManagerUser>()
            .AddSingleton<IOPC, OPCService>()
            //.AddTransient<IUserDialogService, UserDialogService>()
        ;
}