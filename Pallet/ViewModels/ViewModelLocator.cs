using Microsoft.Extensions.DependencyInjection;
using Pallet.ViewModels.Windows;

namespace Pallet.ViewModels
{
    internal class ViewModelLocator
    {
        public MainControlViewModel MainWindowViewModel => App.Services.GetRequiredService<MainControlViewModel>();
        public MainWindowViewModel PreloadWindowViewModel => App.Services.GetRequiredService<MainWindowViewModel>();
        //public LoginViewModel LoginViewModel => App.Services.GetRequiredService<LoginViewModel>();
    }
}