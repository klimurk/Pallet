using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Pallet.Database.Entities.Log;
using Pallet.Database.Entities.OPC;
using Pallet.Database.Entities.ProfileData.Products;
using Pallet.Database.Entities.ProfileData.Profiles;
using Pallet.Database.Entities.ProfileData.Tables;
using Pallet.Database.Entities.ProfileData.Tools;
using Pallet.Database.Entities.ProfileData.Types;
using Pallet.Database.Entities.Users;

namespace Pallet.Database.Context;

/// <summary>
/// The database context for entity framework core.
/// </summary>
public class DatabaseDB : DbContext
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

    #region Profile context

    /// <summary>
    /// Context for profiles.
    /// </summary>
    public DbSet<Profile> Profiles { get; set; }

    /// <summary>
    /// Context for products.
    /// </summary>
    public DbSet<ProfileProducts> ProfileProducts { get; set; }

    public DbSet<ProfileTools> ProfileTools { get; set; }
    public DbSet<Product> Products { get; set; }

    /// <summary>
    /// Context for positions of elements.
    /// </summary>
    ///
    public DbSet<ElementPosition> ElementPositions { get; set; }

    /// <summary>
    /// Context for elements.
    /// </summary>
    public DbSet<Element> Elements { get; set; }

    /// <summary>
    /// Context for nailers.
    /// </summary>
    public DbSet<Nailer> Nailers { get; set; }

    /// <summary>
    /// Context for list of nails.
    /// </summary>
    public DbSet<Nail> Nails { get; set; }

    /// <summary>
    /// Context for tables.
    /// </summary>
    public DbSet<Table> Tables { get; set; }

    /// <summary>
    /// Context for tools.
    /// </summary>
    public DbSet<Tool> Tools { get; set; }

    #endregion Profile context

    #region Ctor

    /// <summary>
    /// Initializes a new instance of the <see cref="DatabaseDB"/> class.
    /// Set lazy loading disabled (internal objects must be loaded)
    /// </summary>
    /// <param name="options">The options.</param>
    public DatabaseDB(DbContextOptions<DatabaseDB> options) : base(options)
    {
        ChangeTracker.LazyLoadingEnabled = false;
    }

    #endregion Ctor

    #region Creating model (convertors, keys...)

    /// <summary>
    /// On the model creating.
    /// Convert db values to internal and back
    /// Correct lazy loading
    /// </summary>
    /// <param name="modelBuilder">The model builder.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //Keys
        modelBuilder.Entity<Profile>().HasIndex(p => p.Name).IsUnique();
        modelBuilder.Entity<Product>().HasIndex(p => p.Name).IsUnique();
        modelBuilder.Entity<Table>().HasIndex(p => p.Name).IsUnique();
        //modelBuilder.Entity<Nailer>().HasIndex(p => p.Name).IsUnique();
        //modelBuilder.Entity<Element>().HasIndex(p => p.Name).IsUnique();

        // With converters
        var Pos10To1Converter = new ValueConverter<double, int>(
        from => (int)(from * 10),
        to => to / 10d);

        modelBuilder
            .Entity<ElementPosition>()
            .Property(m => m.PosX)
            .HasConversion(Pos10To1Converter);
        modelBuilder
            .Entity<ElementPosition>()
            .Property(m => m.PosY)
            .HasConversion(Pos10To1Converter);
        modelBuilder
            .Entity<ElementPosition>()
            .Property(m => m.PosZ)
            .HasConversion(Pos10To1Converter);
        //=====
        modelBuilder
            .Entity<Nail>()
            .Property(m => m.PosX)
            .HasConversion(Pos10To1Converter);
        modelBuilder
            .Entity<Nail>()
            .Property(m => m.PosY)
            .HasConversion(Pos10To1Converter);
        modelBuilder
            .Entity<Nail>()
            .Property(m => m.PosZ)
            .HasConversion(Pos10To1Converter);

        //=====

        modelBuilder
            .Entity<Product>()
            .Property(m => m.Size1X)
            .HasConversion(Pos10To1Converter);
        modelBuilder
            .Entity<Product>()
            .Property(m => m.Size1Y)
            .HasConversion(Pos10To1Converter);
        modelBuilder
            .Entity<Product>()
            .Property(m => m.Size1Z)
            .HasConversion(Pos10To1Converter);
        modelBuilder
            .Entity<Product>()
            .Property(m => m.Size2X)
            .HasConversion(Pos10To1Converter);
        modelBuilder
            .Entity<Product>()
            .Property(m => m.Size2Y)
            .HasConversion(Pos10To1Converter);
        modelBuilder
            .Entity<Product>()
            .Property(m => m.Size2Z)
            .HasConversion(Pos10To1Converter);

        //=====
        modelBuilder
            .Entity<Table>()
            .Property(m => m.PlaceA1OffsetX)
            .HasConversion(Pos10To1Converter);
        modelBuilder
            .Entity<Table>()
            .Property(m => m.PlaceA1OffsetY)
            .HasConversion(Pos10To1Converter);
        modelBuilder
            .Entity<Table>()
            .Property(m => m.PlaceA2OffsetX)
            .HasConversion(Pos10To1Converter);
        modelBuilder
            .Entity<Table>()
            .Property(m => m.PlaceA2OffsetY)
            .HasConversion(Pos10To1Converter);
        modelBuilder
            .Entity<Table>()
            .Property(m => m.PlaceB1OffsetX)
            .HasConversion(Pos10To1Converter);
        modelBuilder
            .Entity<Table>()
            .Property(m => m.PlaceB1OffsetY)
            .HasConversion(Pos10To1Converter);
        modelBuilder
            .Entity<Table>()
            .Property(m => m.PlaceB2OffsetX)
            .HasConversion(Pos10To1Converter);
        modelBuilder
            .Entity<Table>()
            .Property(m => m.PlaceB2OffsetY)
            .HasConversion(Pos10To1Converter);
        modelBuilder
            .Entity<Table>()
            .Property(m => m.SideASizeX)
            .HasConversion(Pos10To1Converter);
        modelBuilder
            .Entity<Table>()
            .Property(m => m.SideASizeY)
            .HasConversion(Pos10To1Converter);
        modelBuilder
            .Entity<Table>()
            .Property(m => m.SideBSizeX)
            .HasConversion(Pos10To1Converter);
        modelBuilder
            .Entity<Table>()
            .Property(m => m.SideBSizeY)
            .HasConversion(Pos10To1Converter);
        modelBuilder
            .Entity<Table>()
            .Property(m => m.WorkAreaAOffsetX)
            .HasConversion(Pos10To1Converter);
        modelBuilder
            .Entity<Table>()
            .Property(m => m.WorkAreaAOffsetY)
            .HasConversion(Pos10To1Converter);
        modelBuilder
            .Entity<Table>()
            .Property(m => m.WorkAreaBOffsetX)
            .HasConversion(Pos10To1Converter);
        modelBuilder
            .Entity<Table>()
            .Property(m => m.WorkAreaBOffsetY)
            .HasConversion(Pos10To1Converter);
        modelBuilder
            .Entity<Table>()
            .Property(m => m.WorkAreaASizeX)
            .HasConversion(Pos10To1Converter);
        modelBuilder
            .Entity<Table>()
            .Property(m => m.WorkAreaASizeY)
            .HasConversion(Pos10To1Converter);
        modelBuilder
            .Entity<Table>()
            .Property(m => m.WorkAreaBSizeX)
            .HasConversion(Pos10To1Converter);
        modelBuilder
            .Entity<Table>()
            .Property(m => m.WorkAreaBSizeY)
            .HasConversion(Pos10To1Converter);

        //=====
        modelBuilder
            .Entity<Element>()
            .Property(m => m.SizeX)
            .HasConversion(Pos10To1Converter);
        modelBuilder
            .Entity<Element>()
            .Property(m => m.SizeY)
            .HasConversion(Pos10To1Converter);
        modelBuilder
            .Entity<Element>()
            .Property(m => m.SizeZ)
            .HasConversion(Pos10To1Converter);

        //=====

        modelBuilder
            .Entity<Nailer>()
            .Property(m => m.Lenght)
            .HasConversion(Pos10To1Converter);

        modelBuilder
            .Entity<Nailer>()
            .Property(m => m.Width)
            .HasConversion(Pos10To1Converter);

        modelBuilder
            .Entity<Nailer>()
            .Property(m => m.Size)
            .HasConversion(Pos10To1Converter);

        //===================================
        //Write Fluent API configurations here

        // Add all relatives datas
        modelBuilder.Entity<ProfileProducts>()
            .HasKey(bc => new { bc.ProductId, bc.ProfileId, bc.Position });
        modelBuilder.Entity<ProfileProducts>()
            .HasOne(bc => bc.Profile)
            .WithMany(b => b.ProfileProducts)
            .HasForeignKey(bc => bc.ProfileId);
        modelBuilder.Entity<ProfileProducts>()
            .HasOne(bc => bc.Product)
            .WithMany(c => c.ProfileProducts)
            .HasForeignKey(bc => bc.ProductId);

        modelBuilder.Entity<ProfileTools>()
            .HasKey(bc => new { bc.ToolId, bc.ProfileId });
        modelBuilder.Entity<ProfileTools>()
            .HasOne(bc => bc.Profile)
            .WithMany(b => b.ProfileTools)
            .HasForeignKey(bc => bc.ProfileId);
        modelBuilder.Entity<ProfileTools>()
            .HasOne(bc => bc.Tool)
            .WithMany(c => c.ProfileTools)
            .HasForeignKey(bc => bc.ToolId);

        modelBuilder.Entity<Profile>()
            .Property(p => p.DateCreate)
            .ValueGeneratedOnAdd();

        //modelBuilder.Entity<Nailer>()
        //    .HasKey(bc => new { bc.Name, bc.Dock });
    }

    #endregion Creating model (convertors, keys...)

    #region Extensions

    public void RefreshAll()
    {
        foreach (var entity in ChangeTracker.Entries()) entity.Reload();
    }

    #endregion Extensions
}