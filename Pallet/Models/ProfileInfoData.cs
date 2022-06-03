using Pallet.Database.Entities.Change.Profiles;
using Pallet.Models.Interfaces;

namespace Pallet.Models;

public class ProfileInfoData : IProfileInfoData
{
    private DateTime? _DateLastUse;

    public DateTime? DateLastUse
    {
        get => _DateLastUse;
        set
        {
            if (value == null) return;
            _DateLastUse = value;
            OnPropertyChanged(new PropertyChangedEventArgs(nameof(DateLastUse)));
        }
    }

    public string Name { get; set; }

    public ProfileInfoData(Profile profile)
    {
        Name = profile.Name;
        DateLastUse = profile.DateLastUse;
    }

    public ProfileInfoData()
    { }

    private void OnPropertyChanged(PropertyChangedEventArgs propertyChangedEventArgs) => PropertyChanged?.Invoke(this, propertyChangedEventArgs);

    public event PropertyChangedEventHandler? PropertyChanged;

    #region Field OPC

    public struct OPCData
    {
        public struct Nails
        {
            public static string DBName { get; } = "Empfang";
            public static string DBVar { get; } = "Static_1";
            public static int DBNamespace { get; } = 3;

            public struct Fields
            {
                public static string CoorX { get; } = "coor X";
                public static string CoorY { get; } = "coor Y";
                public static string CoorZ { get; } = "coor Z";
                public static string Checksum { get; } = "checksum";
                public static string Active { get; } = "active";
                public static string Done { get; } = "done";
                public static string Angle1 { get; } = "angle1";
                public static string Angle2 { get; } = "angle2";
                public static string NailType { get; } = "NailType";
                public static string NailID { get; } = "NailID";
                public static string NailGRP { get; } = "NailGRP";
            }
        }
    }

    #endregion Field OPC
}