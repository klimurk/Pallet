using Pallet.ExternalDatabase.Models;
using Pallet.ExternalDatabase.Models.NotMapped;

namespace Pallet.Services.Managers.Interfaces;

public interface IManagerProfiles
{
    PackageItem CurrentTask { get; }
    IEnumerable<PackageItem> TaskOrder { get; }

    event EventHandler? CurrentTaskChanged;

    List<NailingData> GetTaskNails();
    List<WoodenPart> GetTaskParts();

    public RobotTaskItem RobotTaskItem { get; }
    public RobotTaskItem NextRobotTaskItem { get; }
    public Verpackung CrateCharacteristics { get; }
    public Auftrag Contract { get; }
    public Firma Firm { get; }

    public Verpackung NextCrateCharacteristics { get; }
    public Auftrag NextContract { get; }
    public Firma NextFirm { get; }
}