using Pallet.Database.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pallet.Database.Entities.OLD;

[Table("WTABLE_DEF")]
public class TableTemplate : NamedEntity
{
    [Column("WTABLE_ID")]
    public int InternalID { get; set; }

    [Column("WTABLE_NAME", TypeName = "varchar(20)")]
    public new string Name { get; set; }

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

    [Column("A1_OFFS_X")]
    public int PlaceA1OffsetX { get; set; }

    [Column("A1_OFFS_Y")]
    public int PlaceA1OffsetY { get; set; }

    [Column("A2_OFFS_X")]
    public int PlaceA2OffsetX { get; set; }

    [Column("A2_OFFS_Y")]
    public int PlaceA2OffsetY { get; set; }

    [Column("B1_OFFS_X")]
    public int PlaceB1OffsetX { get; set; }

    [Column("B1_OFFS_Y")]
    public int PlaceB1OffsetY { get; set; }

    [Column("B2_OFFS_X")]
    public int PlaceB2OffsetX { get; set; }

    [Column("B2_OFFS_Y")]
    public int PlaceB2OffsetY { get; set; }

    [Column("A_SIZE_X")]
    public int PlaceASizeX { get; set; }

    [Column("A_SIZE_Y")]
    public int PlaceASizeY { get; set; }

    [Column("B_SIZE_X")]
    public int PlaceBSizeX { get; set; }

    [Column("B_SIZE_Y")]
    public int PlaceBSizeY { get; set; }

    [Column("A_WA_SIZE_X")]
    public int WorkAreaASizeX { get; set; }

    [Column("A_WA_SIZE_Y")]
    public int WorkAreaASizeY { get; set; }

    [Column("B_WA_SIZE_X")]
    public int WorkAreaBSizeX { get; set; }

    [Column("B_WA_SIZE_Y")]
    public int WorkAreaBSizeY { get; set; }

    [Column("A_WA_OFFS_X")]
    public int WorkAreaAOffsetX { get; set; }

    [Column("A_WA_OFFS_Y")]
    public int WorkAreaAOffsetY { get; set; }

    [Column("B_WA_OFFS_X")]
    public int WorkAreaBOffsetX { get; set; }

    [Column("B_WA_OFFS_Y")]
    public int WorkAreaBOffsetY { get; set; }
}