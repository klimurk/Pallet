using Pallet.ViewModels.Windows;

namespace Pallet.View
{
    /// <summary>
    /// Interaction logic for ProfilePreview.xaml
    /// </summary>
    public partial class ProfilePreviewWindow : Window
    {
        public ProfilePreviewWindow()
        {
            InitializeComponent();
            DataContext = App.Services.GetService(typeof(ProfilePreviewViewModel)) as ProfilePreviewViewModel;
        }

        // TODO Manual control commands
        // TODO Pallet view model
        // TODO conditions for auto and stop mode
        // TODO user level controls
    }
}