using Microsoft.Extensions.DependencyInjection;
using Pallet.Services.Draw;
using Pallet.Services.Draw.Interface;
using Pallet.Services.Logging;
using Pallet.Services.Logging.Interfaces;
using Pallet.Services.Managers;
using Pallet.Services.Managers.Interfaces;
using Pallet.Services.OPC;
using Pallet.Services.OPC.Interfaces;
using Pallet.Services.UserDialog;
using Pallet.Services.UserDialog.Interfaces;

namespace Pallet.Services;

internal static class Registator
{
    public static IServiceCollection RegisterServices(this IServiceCollection services) =>
        services
            .AddSingleton<IAlarmLogService, AlarmLogService>()
            .AddSingleton<IManagerLanguage, ManagerLanguage>()
            .AddSingleton<IManagerNailTypes, ManagerNailTypes>()
            .AddSingleton<IManagerProfiles, ManagerProfiles>()
            .AddSingleton<IManagerUser, ManagerUser>()
            .AddSingleton<IOPC, OPCProxy>()
            .AddTransient<IUserDialogService, UserDialogService>()
            .AddSingleton<IDrawer, Drawer>()
            .AddTransient<IManagerLanguage, ManagerLanguage>()
    ;
}