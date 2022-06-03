using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Pallet.Database.Entities.Change.Products;
using Pallet.Database.Entities.Change.Profiles;
using Pallet.Database.Entities.Change.Tables;
using Pallet.Database.Entities.Change.Tools;
using Pallet.Database.Entities.Change.Types;
using Pallet.Database.Entities.OPC;
using Pallet.Database.Entities.Users;

namespace Pallet.Database.Context;

public class DatabaseDB : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<AlarmLog> AlarmLogs { get; set; }
    public DbSet<Alarm> Alarms { get; set; }
    public DbSet<Signal> Signals { get; set; }

    public DbSet<Profile> Profiles { get; set; }

    public DbSet<Product> Products { get; set; }
    public DbSet<ElementPosition> ElementPositions { get; set; }
    public DbSet<Element> Elements { get; set; }
    public DbSet<Nailer> Nailers { get; set; }
    public DbSet<Nail> Nails { get; set; }

    public DbSet<Table> Tables { get; set; }

    public DbSet<Tool> Tools { get; set; }

    public DatabaseDB(DbContextOptions<DatabaseDB> options) : base(options)
    {
        ChangeTracker.LazyLoadingEnabled = false;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
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
            .HasKey(bc => new { bc.ProductId, bc.ProfileId });
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
    }
}