using Pallet.Database.Entities.Base;
using Pallet.Database.Entities.ProfileData.Types;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pallet.Database.Entities.ProfileData.Products
{
    [Table("WPROD_NAIL_POS")]
    public class Nail : Entity
    {
        //[ConcurrencyCheck]
        [Column("NAIL_ID")]
        public int NailID { get; set; }

        [Column("POSX", TypeName = "int")]
        public double PosX { get; set; }

        [Column("POSY", TypeName = "int")]
        public double PosY { get; set; }

        [Column("POSZ", TypeName = "int")]
        public double PosZ { get; set; }

        [Column("ANGLE1")]
        public int Angle1 { get; set; }

        [Column("ANGLE2")]
        public int Angle2 { get; set; }

        [Column("NAIL_FIX")]
        public bool NailFix { get; set; }

        [Column("MOVE_TO_NEXT")]
        public int MoveNextType { get; set; }

        [Column("ALT")]
        public int Alt { get; set; }

        [Column("MODE")]
        public int Mode { get; set; }

        [ForeignKey("NAILER_ID")]
        [InverseProperty("Nails")]
        public Nailer Nailer { get; set; }

        [ForeignKey("PROD_ID")]
        [InverseProperty("Nails")]
        public Product Product { get; set; }
    }
}