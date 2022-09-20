using Pallet.Database.Entities.Base;
using Pallet.Database.Entities.Base.Interfaces;
using Pallet.Database.Entities.ProfileData.Products;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pallet.Database.Entities.ProfileData.Types;

[Table("NAILER_DEF")]
public class Nailer : NamedEntity, IDBTranslateble
{
    //[ConcurrencyCheck]
    [Column("NAILER_ID")]
    public int InternalID { get; set; }

    [Column("NAILER_NAME", TypeName = "varchar(20)")]
    public override string Name { get; set; }

    [Column("DESC1", TypeName = "nvarchar(100)")]
    public string DescriptionEn { get; set; }

    [Column("DESC2", TypeName = "nvarchar(100)")]
    public string DescriptionDe { get; set; }

    [Column("DESC3", TypeName = "nvarchar(100)")]
    public string DescriptionLocal { get; set; }

    [Column("DOCK")]
    public int Dock { get; set; }

    [Column("NLENGTH", TypeName = "int")]
    public double Lenght { get; set; }

    [Column("NWIDTH", TypeName = "int")]
    public double Width { get; set; }

    [Column("MSIZE", TypeName = "int")]
    public double Size { get; set; }

    [Column("MCOLOR", TypeName = "bigint")]
    public long Color { get; set; }

    [InverseProperty("Nailer")]
    public ICollection<Nail> Nails { get; set; } = new HashSet<Nail>();

    [Column("NSIZE")]
    public int NailSize { get; set; }

    [NotMapped]
    public int NailLeftCounter { get; set; }
}