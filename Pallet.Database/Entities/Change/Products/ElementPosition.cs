using Pallet.Database.Entities.Base;
using Pallet.Database.Entities.Change.Types;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pallet.Database.Entities.Change.Products;

[Table("WPROD_ELE_POS")]
public class ElementPosition : Entity
{
    [InverseProperty("Positions")]
    [ForeignKey("ELE_ID")]
    public Element Element { get; set; }

    [ForeignKey("PROD_ID")]
    [InverseProperty("Elements")]
    public Product Product { get; set; }

    [Column("POS_ID")]
    public int PosID { get; set; }

    [Column("POSX", TypeName = "int")]
    public double PosX { get; set; }

    [Column("POSY", TypeName = "int")]
    public double PosY { get; set; }

    [Column("POSZ", TypeName = "int")]
    public double PosZ { get; set; }

    [Column("LAYER", TypeName = "smallint")]
    public short Layer { get; set; }

    [Column("OUTLN")]
    public bool OutLN { get; set; }


    [Column("SIDE")]
    public int Side { get; set; }

    //[ForeignKey("Product")]
    //public int? PROD_ID { get; set; }

    //public Product Product { get; set; }
}