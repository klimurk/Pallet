using Microsoft.Extensions.DependencyInjection;
using Pallet.ViewModels.SubView;
using Pallet.ViewModels.Windows;

namespace Pallet.ViewModels
{
    internal static class ViewModelRegistrator
    {
        public static IServiceCollection RegisterViewModels(this IServiceCollection services)
            => services
            .AddSingleton<MainWindowViewModel>()
            .AddSingleton<MainControlViewModel>()
            .AddTransient<CreateUserViewModel>()
            .AddTransient<LoginViewModel>()

            .AddTransient<AlarmViewModel>()
            .AddSingleton<LogViewModel>()
            .AddTransient<ManualViewModel>()
            .AddTransient<PalletViewModel>()
            .AddTransient<UsersViewModel>()
        ;
    }
}