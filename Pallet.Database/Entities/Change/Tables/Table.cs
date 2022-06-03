using Pallet.Database.Entities.Base;
using Pallet.Database.Entities.Change.Profiles;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pallet.Database.Entities.Change.Tables;

[Table("WTABLE_DEF")]
public class Table : NamedEntity
{
    //[ConcurrencyCheck]
    [Column("WTABLE_NAME", TypeName = "varchar(20)")]
    public override string Name { get; set; }

    [Column("A1_CONF", TypeName = "char(5)")]
    public string PlaceA1Cofiguration { get; set; }

    [Column("A2_CONF", TypeName = "char(5)")]
    public string PlaceA2Cofiguration { get; set; }

    [Column("B1_CONF", TypeName = "char(5)")]
    public string PlaceB1Cofiguration { get; set; }

    [Column("B2_CONF", TypeName = "char(5)")]
    public string PlaceB2Cofiguration { get; set; }

    [Column("B_ENA")]
    public bool Enabled { get; set; }

    [Column("A1_OFFS_X", TypeName = "int")]
    public double PlaceA1OffsetX { get; set; }

    [Column("A1_OFFS_Y", TypeName = "int")]
    public double PlaceA1OffsetY { get; set; }

    [Column("A2_OFFS_X", TypeName = "int")]
    public double PlaceA2OffsetX { get; set; }

    [Column("A2_OFFS_Y", TypeName = "int")]
    public double PlaceA2OffsetY { get; set; }

    [Column("B1_OFFS_X", TypeName = "int")]
    public double PlaceB1OffsetX { get; set; }

    [Column("B1_OFFS_Y", TypeName = "int")]
    public double PlaceB1OffsetY { get; set; }

    [Column("B2_OFFS_X", TypeName = "int")]
    public double PlaceB2OffsetX { get; set; }

    [Column("B2_OFFS_Y", TypeName = "int")]
    public double PlaceB2OffsetY { get; set; }

    [Column("A_SIZE_X", TypeName = "int")]
    public double SideASizeX { get; set; }

    [Column("A_SIZE_Y", TypeName = "int")]
    public double SideASizeY { get; set; }

    [Column("B_SIZE_X", TypeName = "int")]
    public double SideBSizeX { get; set; }

    [Column("B_SIZE_Y", TypeName = "int")]
    public double SideBSizeY { get; set; }

    [Column("A_WA_SIZE_X", TypeName = "int")]
    public double WorkAreaASizeX { get; set; }

    [Column("A_WA_SIZE_Y", TypeName = "int")]
    public double WorkAreaASizeY { get; set; }

    [Column("B_WA_SIZE_X", TypeName = "int")]
    public double WorkAreaBSizeX { get; set; }

    [Column("B_WA_SIZE_Y", TypeName = "int")]
    public double WorkAreaBSizeY { get; set; }

    [Column("A_WA_OFFS_X", TypeName = "int")]
    public double WorkAreaAOffsetX { get; set; }

    [Column("A_WA_OFFS_Y", TypeName = "int")]
    public double WorkAreaAOffsetY { get; set; }

    [Column("B_WA_OFFS_X", TypeName = "int")]
    public double WorkAreaBOffsetX { get; set; }

    [Column("B_WA_OFFS_Y", TypeName = "int")]
    public double WorkAreaBOffsetY { get; set; }

    [InverseProperty("Table")]
    public ICollection<Profile> Profiles { get; set; } = new HashSet<Profile>();
}