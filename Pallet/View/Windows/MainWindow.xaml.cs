using Pallet.ViewModels.Windows;

namespace Pallet
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = App.Services.GetService(typeof(MainWindowViewModel)) as MainWindowViewModel;
        }
    }
}