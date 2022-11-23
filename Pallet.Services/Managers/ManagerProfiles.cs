using Pallet.Extensions;
using Pallet.ExternalDatabase.Context;
using Pallet.ExternalDatabase.Models;
using Pallet.ExternalDatabase.Models.NotMapped;
using Pallet.Services.Managers.Interfaces;

namespace Pallet.Services.Managers;

/// <summary>
/// Manager profiles.
/// </summary>
public class ManagerProfiles : IManagerProfiles
{
    private readonly ExternalDbContext _dbExt;

    public IEnumerable<PackageItem> TaskOrder
    {
        get
        {
            List<PackageItem> result = new();
            //result.AddRange(
            //    from task in _dbExt.TRoboTaskItems
            //    from item in _dbExt.TRoboPackageItems
            //    where task.NId == item.NItemId && task.CVerpId == item.CPackageId && task.DoneCount < task.Count
            //    orderby task.PlannedDateTime, task.Count
            //    select item
            //);
            foreach (var tsk in _dbExt.TRoboTaskItems.Where(t => t.DoneCount <= t.Count).OrderBy(t => t.PlannedDateTime).ThenByDescending(t => t.Count).ToList())
            {
                var item = _dbExt.TRoboPackageItems.FirstOrDefault(s => tsk.NId == s.NItemId && tsk.CVerpId == s.CPackageId);

                if (item != default) result.Add(item);
            }

            return result;
        }
    }

    public RobotTaskItem? RobotTaskItem => _dbExt.TRoboTaskItems.FirstOrDefault(s => s.CVerpId == CurrentTask.CPackageId && s.NId == CurrentTask.NItemId);

    public RobotTaskItem? NextRobotTaskItem => _dbExt.TRoboTaskItems.FirstOrDefault(s => s.CVerpId == NextTask.CPackageId && s.NId == NextTask.NItemId);

    public PackageItem? CurrentTask
    {
        get
        {
            _CurrentTaskOld ??= TaskOrder.FirstOrDefault();
            var newTask = TaskOrder
                //.Skip(1)
                .Skip(2)
                .FirstOrDefault();

            if (newTask != _CurrentTaskOld) OnCurrentTaskChanged();

            _CurrentTaskOld = newTask;

            return newTask;
        }
    }

    private PackageItem? _CurrentTaskOld;
    public PackageItem? NextTask => TaskOrder.ElementAt(TaskOrder.IndexOf(CurrentTask) + 1);

    public Verpackung CrateCharacteristics => _dbExt.TVerpackungs.First(s => CurrentTask.CPackageId == s.CId);
    public Auftrag Contract => _dbExt.TAuftrags.First(s => s.NId == CrateCharacteristics.VerpAuftragId);
    public Firma Firm => _dbExt.TFirmas.First(s => s.Firmakey == Contract.NKundeId);

    public Verpackung NextCrateCharacteristics => _dbExt.TVerpackungs.First(s => NextTask.CPackageId == s.CId);
    public Auftrag NextContract => _dbExt.TAuftrags.First(s => s.NId == NextCrateCharacteristics.VerpAuftragId);
    public Firma NextFirm => _dbExt.TFirmas.First(s => s.Firmakey == NextContract.NKundeId);

    public ManagerProfiles(ExternalDbContext dbExt) => _dbExt = dbExt;

    public List<NailingData> GetTaskNails() => _dbExt.TRoboPackageItemNailingData.Where(s => s.CProcessId == CurrentTask.CProcessId).ToList();

    public event EventHandler? CurrentTaskChanged;

    private void OnCurrentTaskChanged() => CurrentTaskChanged?.Invoke(this, new());

    public List<WoodenPart> GetTaskParts() =>
        new(
            from d in _dbExt.T3dVerpackungDetails
            join v in _dbExt.T3dVerpackungs on d.CId equals v.CId
            where v.CVerpId == RobotTaskItem.CVerpId && d.CLayerId == RobotTaskItem.CLayerId
            orderby d.NId descending
            select new WoodenPart(d, v)
        );

    public struct OPCData
    {
        public struct Nails
        {
            public static string DBName { get; } = "Empfang";
            public static string DBVar { get; } = "Data";
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
}