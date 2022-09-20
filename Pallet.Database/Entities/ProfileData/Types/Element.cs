using Pallet.Database.Entities.Base;
using Pallet.Database.Entities.Base.Interfaces;
using Pallet.Database.Entities.ProfileData.Products;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pallet.Database.Entities.ProfileData.Types;

/// <summary>
/// Element user created.
/// </summary>
[Table("WPROD_ELEMENTS")]
public class Element : NamedEntity, IDBTranslateble
{
    /// <summary>
    /// Name of element.
    /// </summary>
    [ConcurrencyCheck]
    //[Key]
    [Column("ELE_NAME", TypeName = "varchar(20)")]
    public override string Name { get; set; }

    /// <summary>
    /// Description on english.
    /// </summary>
    [Column("DESC1", TypeName = "nvarchar(100)")]
    public string DescriptionEn { get; set; }

    /// <summary>
    /// Description on German.
    /// </summary>
    [Column("DESC2", TypeName = "nvarchar(100)")]
    public string DescriptionDe { get; set; }

    /// <summary>
    /// Description on Czech.
    /// </summary>
    [Column("DESC3", TypeName = "nvarchar(100)")]
    public string DescriptionLocal { get; set; }

    /// <summary>
    /// Dock position.
    /// </summary>
    [Column("ELE_CNT")]
    public int Count { get; set; }

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