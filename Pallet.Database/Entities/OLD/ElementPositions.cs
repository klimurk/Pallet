using Pallet.Database.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pallet.Database.Entities.OLD;

[Table("WPROD_ELE_POS")]
public class ElementPositions : NamedEntity
{
    [Column("WPROD_NAME", TypeName = "varchar(20)")]
    public new string Name { get; set; }

    [Column("ELE_ID")]
    public int ElementID { get; set; }

    [Column("POS_ID")]
    public int PosID { get; set; }

    [Column("POSX")]
    public int PosX { get; set; }

    [Column("POSY")]
    public int PosY { get; set; }

    [Column("POSZ")]
    public int PosZ { get; set; }
}