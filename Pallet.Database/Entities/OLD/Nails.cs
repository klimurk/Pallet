using Pallet.Database.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pallet.Database.Entities.OLD;

[Table("WPROD_NAIL_POS")]
public class Nails : NamedEntity
{
    [Column("WPROD_NAME", TypeName = "varchar(20)")]
    public new string Name { get; set; }

    [Column("NAIL_ID")]
    public int NailID { get; set; }

    [Column("NAILER_ID")]
    public Nailer Nailer { get; set; }

    [Column("POSX")]
    public int PosX { get; set; }

    [Column("POSY")]
    public int PosY { get; set; }

    [Column("POSZ")]
    public int PosZ { get; set; }

    [Column("ANGLE1")]
    public int Angle1 { get; set; }

    [Column("ANGLE2")]
    public int Angle2 { get; set; }

    [Column("NAIL_FIX")]
    public bool NailFix { get; set; }

    [Column("MOVE_TO_NEXT")]
    public int NailType { get; set; }

    [Column("ALT")]
    public int Alt { get; set; }

    [Column("MODE")]
    public int Mode { get; set; }
}