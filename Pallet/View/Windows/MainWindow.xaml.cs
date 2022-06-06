using Pallet.ViewModels.Windows;

namespace Pallet
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            DataContext = App.Services.GetService(typeof(MainWindowViewModel)) as MainWindowViewModel;
        }
    }
}