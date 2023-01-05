using Pallet.ExternalDatabase.Context;
using Pallet.ExternalDatabase.Models;
using Pallet.ExternalDatabase.Models.NotMapped;
using Pallet.Services.Managers.Interfaces;
using Pallet.Services.Models;

namespace Pallet.Services.Managers;

/// <summary>
/// Manager profiles.
/// </summary>
public class ManagerProfiles : IManagerProfiles
{
    private readonly IExternalDbContext _dbExt;

    public IEnumerable<PackageItem> TaskOrder { get; private set; }
    //{
    //    //return _dbExt.TRoboPackageItems.Where(s =>  _dbExt.TRoboTaskItems.Where(t => t.DoneCount <= t.Count).OrderBy(t => t.PlannedDateTime).ThenByDescending(t => t.Count).Any(t=> t.NId == s.NItemId && t.CVerpId == s.CPackageId)).ToListAsync();

    //    //from task in _dbExt.TRoboTaskItems
    //    //from item in _dbExt.TRoboPackageItems
    //    //where task.NId == item.NItemId && task.CVerpId == item.CPackageId && task.DoneCount < task.Count
    //    //orderby task.PlannedDateTime, task.Count
    //    //select item;
    //}

    public RobotTaskItem RobotTaskItem { get; private set; }

    public RobotTaskItem NextRobotTaskItem { get; private set; }

    public PackageItem? CurrentTask { get; private set; }
    //{
    //        //_CurrentTaskOld ??= TaskOrder.FirstOrDefault();
    //        var newTask = TaskOrder
    //            //.Skip(1)
    //            .Skip(2)
    //            .FirstOrDefault();

    //        if (newTask != (_CurrentTaskOld ??= TaskOrder.FirstOrDefault())) OnCurrentTaskChanged();

    //        _CurrentTaskOld = newTask;

    //        return newTask;

    //}

    private PackageItem? _CurrentTaskOld;
    public PackageItem? NextTask { get; private set; }

    public Verpackung CrateCharacteristics { get; private set; }
    public Auftrag Contract { get; private set; }
    public Firma Firm { get; private set; }

    public Verpackung NextCrateCharacteristics { get; private set; }
    public Auftrag NextContract { get; private set; }
    public Firma NextFirm { get; private set; }


    public Profile CurrentProfile { get; private set; }
    public Profile NextProfile { get; private set; }

    public ManagerProfiles(IExternalDbContext dbExt)
    {
        _dbExt = dbExt;
        ReadNewTask().ConfigureAwait(false);
    }

    public async Task ReadNewTask()
    {
        List<PackageItem> result = new();
        foreach (var tsk in await _dbExt.TRoboTaskItems.Where(t => t.DoneCount <= t.Count).OrderBy(t => t.PlannedDateTime).ThenByDescending(t => t.Count).ToListAsync())
        {
            var item = await _dbExt.TRoboPackageItems.FirstOrDefaultAsync(s => tsk.NId == s.NItemId && tsk.CVerpId == s.CPackageId);

            if (item != default) result.Add(item);
        }

        TaskOrder = result;
        var newTask = TaskOrder
                //.Skip(1)
                .Skip(2)
                .FirstOrDefault();

        if (newTask != (_CurrentTaskOld ??= TaskOrder.FirstOrDefault())) OnCurrentTaskChanged();

        _CurrentTaskOld = newTask;
        CurrentTask = newTask;
        RobotTaskItem = await _dbExt.TRoboTaskItems.FirstOrDefaultAsync(s => s.CVerpId == CurrentTask.CPackageId && s.NId == CurrentTask.NItemId);
        CrateCharacteristics = await _dbExt.TVerpackungs.FirstAsync(s => CurrentTask.CPackageId == s.CId);
        Contract = await _dbExt.TAuftrags.FirstAsync(s => s.NId == CrateCharacteristics.VerpAuftragId);
        Firm = await _dbExt.TFirmas.FirstAsync(s => s.Firmakey == Contract.NKundeId);

        CurrentProfile = new()
        {
            Task = CurrentTask,
            Contract = Contract,
            Firm = Firm,
            CrateCharacteristics = CrateCharacteristics,
            Nails = GetTaskNails(newTask),
            Parts = GetTaskParts(RobotTaskItem),
            RobotTaskItem = RobotTaskItem
        };

        

        NextTask = TaskOrder.ElementAt(TaskOrder.ToList().IndexOf(CurrentTask) + 1);
        NextRobotTaskItem = await _dbExt.TRoboTaskItems.FirstOrDefaultAsync(s => s.CVerpId == NextTask.CPackageId && s.NId == NextTask.NItemId);
        NextCrateCharacteristics = await _dbExt.TVerpackungs.FirstAsync(s => NextTask.CPackageId == s.CId);
        NextContract = await _dbExt.TAuftrags.FirstAsync(s => s.NId == NextCrateCharacteristics.VerpAuftragId);
        NextFirm = await _dbExt.TFirmas.FirstAsync(s => s.Firmakey == NextContract.NKundeId);

        NextProfile = new()
        {
            Task = NextTask,
            Contract = NextContract,
            Firm = NextFirm,
            CrateCharacteristics = NextCrateCharacteristics,
            Nails = GetTaskNails(NextTask),
            Parts = GetTaskParts(NextRobotTaskItem),
            RobotTaskItem = NextRobotTaskItem
        };

        OnCurrentTaskChanged();
    }

    public IQueryable<NailingData> GetTaskNails(PackageItem task) => _dbExt.TRoboPackageItemNailingData.Where(s => s.CProcessId == task.CProcessId);

    public event EventHandler? CurrentTaskChanged;

    private void OnCurrentTaskChanged() => CurrentTaskChanged?.Invoke(this, new());

    public IQueryable<WoodenPart> GetTaskParts(RobotTaskItem robotTaskItem) =>
            from d in _dbExt.T3dVerpackungDetails
            join v in _dbExt.T3dVerpackungs on d.CId equals v.CId
            where v.CVerpId == robotTaskItem.CVerpId && d.CLayerId == robotTaskItem.CLayerId
            orderby d.NId descending
            select new WoodenPart(d, v)
        ;

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
                public static string Active { get; } = "active";
                public static string Done { get; } = "done";

                public static string FixNail { get; } = "Fix_nail";
            }
        }
    }
}