namespace Pallet.Models.Interfaces
{
    public interface IProfileInfoData : INotifyPropertyChanged
    {
        string Name { get; set; }
        DateTime? DateLastUse { get; set; }
    }
}