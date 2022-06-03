using Pallet.Database.Entities.Base;
using Pallet.Database.Entities.Change.Products;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pallet.Database.Entities.Change.Types;

[Table("WPROD_ELEMENTS")]
public class Element : NamedEntity
{
    [ConcurrencyCheck]
    [Column("ELE_NAME", TypeName = "varchar(20)")]
    public override string Name { get; set; }

    [Column("DESC1", TypeName = "nvarchar(100)")]
    public string Description1 { get; set; }

    [Column("DESC2", TypeName = "nvarchar(100)")]
    public string Description2 { get; set; }

    [Column("DESC3", TypeName = "nvarchar(100)")]
    public string Description3 { get; set; }

    [Column("ELE_CNT")]
    public int Dock { get; set; }

    [Column("SIZEX", TypeName = "int")]
    public double SizeX { get; set; }

    [Column("SIZEY", TypeName = "int")]
    public double SizeY { get; set; }

    [Column("SIZEZ", TypeName = "int")]
    public double SizeZ { get; set; }

    [Column("WOOD_DIR", TypeName = "smallint")]
    public short Direction { get; set; }

    [InverseProperty("Element")]
    public ICollection<ElementPosition> Positions { get; set; } = new HashSet<ElementPosition>();
}