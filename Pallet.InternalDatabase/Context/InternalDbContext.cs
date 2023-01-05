using Pallet.InternalDatabase.Entities.Log;
using Pallet.InternalDatabase.Entities.OPC;
using Pallet.InternalDatabase.Entities.Users;

namespace Pallet.InternalDatabase.Context;

/// <summary>
/// The database context for entity framework core.
/// </summary>
public class InternalDbContext : DbContext, IInternalDbContext
{
    #region System context

    /// <summary>
    /// Context for users.
    /// </summary>
    public DbSet<User> Users { get; set; }

    /// <summary>
    /// Context for EventLogs.
    /// </summary>
    public DbSet<Log> Logs { get; set; }

    /// <summary>
    /// Context for PalletLogs.
    /// </summary>
    //public DbSet<PalletLog> PalletLogs { get; set; }

    /// <summary>
    /// Context for users.
    /// </summary>
    public DbSet<SystemEvent> SystemEvents { get; set; }

    #endregion System context

    #region OPC context

    /// <summary>
    /// Context for Alarm logs.
    /// </summary>
    public DbSet<AlarmLog> AlarmLogs { get; set; }

    /// <summary>
    /// Context for alarms definitions.
    /// </summary>
    public DbSet<Alarm> Alarms { get; set; }

    /// <summary>
    /// Context for signals definitions.
    /// </summary>
    public DbSet<Signal> Signals { get; set; }

    #endregion OPC context

    #region Ctor

    /// <summary>
    /// Initializes a new instance of the <see cref="InternalDbContext"/> class.
    /// Set lazy loading disabled (internal objects must be loaded)
    /// </summary>
    /// <param name="options">The options.</param>
    public InternalDbContext(DbContextOptions<InternalDbContext> options) : base(options)
    {
    }

    #endregion Ctor

    #region Creating model (convertors, keys...)

    /// <summary>
    /// On the model creating.
    /// Convert db values to internal and back
    /// Correct lazy loading
    /// </summary>
    /// <param name="modelBuilder">The model builder.</param>

    #endregion Creating model (convertors, keys...)

    #region Extensions

    public void RefreshAll()
    {
        foreach (var entity in ChangeTracker.Entries()) entity.Reload();
    }

    #endregion Extensions
}