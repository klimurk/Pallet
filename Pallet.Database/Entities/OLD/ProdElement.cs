using Pallet.Database.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pallet.Database.Entities.OLD;

[Table("WPROD_ELEMENTS")]
public class ProdElement : NamedEntity
{
    [Column("NAILER_NAME", TypeName = "varchar(20)")]
    public new string Name { get; set; }

    [Column("ELE_ID")]
    public int ElementID { get; set; }

    [Column("ELE_NAME", TypeName = "varchar(20)")]
    public string ElementName { get; set; }

    [Column("DESC1", TypeName = "nvarchar(100)")]
    public string Description1 { get; set; }

    [Column("DESC2", TypeName = "nvarchar(100)")]
    public string Description2 { get; set; }

    [Column("DESC3", TypeName = "nvarchar(100)")]
    public string Description3 { get; set; }

    [Column("ELE_CNT")]
    public int Dock { get; set; }

    [Column("SIZEX")]
    public int SizeX { get; set; }

    [Column("SIZEY")]
    public int SizeY { get; set; }

    [Column("SIZEZ")]
    public int SizeZ { get; set; }

    [Column("WOOD_DIR", TypeName = "smallint")]
    public short Direction { get; set; }

    [Column("LAYER", TypeName = "smallint")]
    public short Layer { get; set; }

    [Column("OUTLN")]
    public bool OutLN { get; set; }
}