using Pallet.ExternalDatabase.Models;
using Pallet.ExternalDatabase.Models.NotMapped;
using Pallet.Services.Models;

namespace Pallet.Services.Managers.Interfaces;

public interface IManagerProfiles
{
    public Task ReadNewTask();

    PackageItem? CurrentTask { get; }

    IEnumerable<PackageItem> TaskOrder { get; }


    IQueryable<NailingData> GetTaskNails(PackageItem task);

    IQueryable<WoodenPart> GetTaskParts(RobotTaskItem robotTaskItem);

    public RobotTaskItem RobotTaskItem { get; }
    public Verpackung CrateCharacteristics { get; }
    public Auftrag Contract { get; }
    public Firma Firm { get; }

    public RobotTaskItem NextRobotTaskItem { get; }
    public Verpackung NextCrateCharacteristics { get; }
    public Auftrag NextContract { get; }
    public Firma NextFirm { get; }


    public Profile CurrentProfile { get;  }
    public Profile NextProfile { get;  }

    event EventHandler? CurrentTaskChanged;
}