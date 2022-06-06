using Pallet.Database.Entities.Base;
using Pallet.Database.Entities.Change.Types;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pallet.Database.Entities.Change.Products;

/// <summary>
/// The element position database .
/// </summary>
[Table("WPROD_ELE_POS")]
public class ElementPosition : Entity
{
    /// <summary>
    /// Link to the element.
    /// </summary>
    [InverseProperty("Positions")]
    [ForeignKey("ELE_ID")]
    public Element Element { get; set; }

    /// <summary>
    /// Link to the product.
    /// </summary>
    [ForeignKey("PROD_ID")]
    [InverseProperty("Elements")]
    public Product Product { get; set; }

    /// <summary>
    /// Sequence number of element.
    /// </summary>
    [Column("POS_ID")]
    public int PosID { get; set; }

    /// <summary>
    /// Position x.
    /// </summary>
    [Column("POSX", TypeName = "int")]
    public double PosX { get; set; }

    /// <summary>
    /// Position y.
    /// </summary>
    [Column("POSY", TypeName = "int")]
    public double PosY { get; set; }

    /// <summary>
    /// Position z.
    /// </summary>
    [Column("POSZ", TypeName = "int")]
    public double PosZ { get; set; }

    /// <summary>
    /// Layer number.
    /// </summary>
    [Column("LAYER", TypeName = "smallint")]
    public short Layer { get; set; }

    /// <summary>
    /// Is outlined.
    /// </summary>
    [Column("OUTLN")]
    public bool OutLN { get; set; }

    /// <summary>
    /// Side of table.
    /// </summary>
    [Column("SIDE")]
    public int Side { get; set; }
}