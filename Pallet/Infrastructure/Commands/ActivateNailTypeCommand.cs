using Pallet.Database.Entities.Change.Types;
using Pallet.Infrastructure.Commands.Base;
using Pallet.Services.Managers.Interfaces;
using Pallet.Services.UserDialog.Interfaces;
using Pallet.ViewModels.SubView;

namespace Pallet.Infrastructure.Commands;

internal class ActivateNailTypeCommand : Command
{
    protected override bool CanExecute(object parameter) => true;

    protected override void Execute(object parameter)
    {
        ManualViewModel _ManualViewModel = (ManualViewModel)App.Host.Services.GetService(typeof(ManualViewModel));
        IUserDialogService UserDialogService = (IUserDialogService)App.Host.Services.GetService(typeof(IUserDialogService));

        if (!UserDialogService.ConfirmWarning("Activate Nail Type" + ((Nailer)parameter).Name + "?", "Activate?")) return;
        _ManualViewModel.NailTypeActive = (Nailer)parameter;
    }
}