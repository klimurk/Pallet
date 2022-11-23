using System.ComponentModel;

namespace Pallet.InternalDatabase.Entities.Base;

public  interface IValueNotified
{
    public event PropertyChangedEventHandler PropertyChanged;

    public void OnPropertyChanged(PropertyChangedEventArgs propertyChangedEventArgs);
}