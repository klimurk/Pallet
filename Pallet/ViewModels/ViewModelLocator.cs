using Microsoft.Extensions.DependencyInjection;
using Pallet.ViewModels.Windows;

namespace Pallet.ViewModels
{
    internal class ViewModelLocator
    {
        public MainWindowViewModel MainWindowViewModel => App.Services.GetRequiredService<MainWindowViewModel>();
        public ProfilePreviewViewModel ProfilePreviewViewModel => App.Services.GetRequiredService<ProfilePreviewViewModel>();
        public LoginViewModel LoginViewModel => App.Services.GetRequiredService<LoginViewModel>();
    }
}