using Pallet.Database.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pallet.Database.Entities.OLD;

[Table("NAILER_DEF")]
public class Nailer : NamedEntity
{
    [Column("WPROD_NAME")]
    public int NailerID { get; set; }

    [Column("NAILER_NAME", TypeName = "varchar(20)")]
    public new string Name { get; set; }

    [Column("DESC1", TypeName = "nvarchar(100)")]
    public string Description1 { get; set; }

    [Column("DESC2", TypeName = "nvarchar(100)")]
    public string Description2 { get; set; }

    [Column("DESC3", TypeName = "nvarchar(100)")]
    public string Description3 { get; set; }

    [Column("DOCK")]
    public int Dock { get; set; }

    [Column("NLENGTH")]
    public int Lenght { get; set; }

    [Column("NWIDTH")]
    public int Width { get; set; }

    [Column("MSIZE")]
    public int Size { get; set; }

    [Column("MCOLOR", TypeName = "bigint")]
    public long Color { get; set; }
}