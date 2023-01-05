using Pallet.InternalDatabase.Entities.Log;
using Pallet.InternalDatabase.Entities.OPC;
using Pallet.InternalDatabase.Entities.Users;

namespace Pallet.InternalDatabase.Context
{
    public interface IInternalDbContext
    {
        DbSet<AlarmLog> AlarmLogs { get; set; }
        DbSet<Alarm> Alarms { get; set; }
        DbSet<Log> Logs { get; set; }
        DbSet<Signal> Signals { get; set; }
        DbSet<SystemEvent> SystemEvents { get; set; }
        DbSet<User> Users { get; set; }

        void RefreshAll();
    }
}