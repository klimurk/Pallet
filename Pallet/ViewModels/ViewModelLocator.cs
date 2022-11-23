using Microsoft.Extensions.DependencyInjection;
using Pallet.ViewModels.Windows;

namespace Pallet.ViewModels
{
    internal class ViewModelLocator
    {
        public MainWindowViewModel MainWindowViewModel => App.Services.GetRequiredService<MainWindowViewModel>();
        //public LoginViewModel LoginViewModel => App.Services.GetRequiredService<LoginViewModel>();
    }
}