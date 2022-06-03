using Pallet.ViewModels.Windows;

namespace Pallet.View
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();

            DataContext = App.Services.GetService(typeof(LoginViewModel)) as LoginViewModel;
        }
    }
}