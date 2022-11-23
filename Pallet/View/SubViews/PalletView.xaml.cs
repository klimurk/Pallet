using System.Windows.Controls;

namespace Pallet.View.SubViews;

/// <summary>
/// Логика взаимодействия для PalletView.xaml
/// </summary>
public partial class PalletView : UserControl
{
    public PalletView()
    {
        InitializeComponent();
        //DataContext = App.Services.GetService(typeof(PalletViewModel)) as PalletViewModel;
        //((PalletViewModel)DataContext).OnCanvasReloadCommandExecuted(new());
    }
}