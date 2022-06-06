using Pallet.Database.Entities.Base;
using Pallet.Database.Entities.Change.Products;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pallet.Database.Entities.Change.Types;

/// <summary>
/// Element user created.
/// </summary>
[Table("WPROD_ELEMENTS")]
public class Element : NamedEntity
{
    /// <summary>
    /// Name of element.
    /// </summary>
    [ConcurrencyCheck]
    [Column("ELE_NAME", TypeName = "varchar(20)")]
    public override string Name { get; set; }

    /// <summary>
    /// Description on english.
    /// </summary>
    [Column("DESC1", TypeName = "nvarchar(100)")]

    public string Description1 { get; set; }
    /// <summary>
    /// Description on German.
    /// </summary>
    [Column("DESC2", TypeName = "nvarchar(100)")]

    public string Description2 { get; set; }
    /// <summary>
    /// Description on Czech.
    /// </summary>
    [Column("DESC3", TypeName = "nvarchar(100)")]

    public string Description3 { get; set; }

    /// <summary>
    /// Dock position.
    /// </summary>
    [Column("ELE_CNT")]

    public int Dock { get; set; }
    /// <summary>
    /// Element size x.
    /// </summary>
    [Column("SIZEX", TypeName = "int")]

    public double SizeX { get; set; }
    /// <summary>
    /// Element size y .
    /// </summary>
    [Column("SIZEY", TypeName = "int")]

    public double SizeY { get; set; }
    /// <summary>
    /// Element size z.
    /// </summary>
    [Column("SIZEZ", TypeName = "int")]
    public double SizeZ { get; set; }

    /// <summary>
    /// Base direction of element.
    /// </summary>
    [Column("WOOD_DIR", TypeName = "smallint")]
    public short Direction { get; set; }

    /// <summary>
    /// Link to collection of positions.
    /// </summary>
    [InverseProperty("Element")]
    public ICollection<ElementPosition> Positions { get; set; } = new HashSet<ElementPosition>();
}