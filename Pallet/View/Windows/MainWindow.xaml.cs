using MahApps.Metro.Controls;
using Pallet.ViewModels.Windows;

namespace Pallet.View.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            DataContext = App.Services.GetService(typeof(MainWindowViewModel)) as MainWindowViewModel;
        }

        //private void UIElement_OnPreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        //{
        //    //until we had a StaysOpen flag to Drawer, this will help with scroll bars
        //    var dependencyObject = Mouse.Captured as DependencyObject;

        //    while (dependencyObject != null)
        //    {
        //        if (dependencyObject is ScrollBar) return;
        //        dependencyObject = VisualTreeHelper.GetParent(dependencyObject);
        //    }

        //    MenuToggleButton.IsChecked = false;
        //}

        //public static string DialogName = "RootDialog";
    }
}