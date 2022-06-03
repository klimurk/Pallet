using Pallet.Database.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pallet.Database.Entities.OLD;

/// <summary>
/// The nail type.
/// </summary>
///
[Table("NAILER_DEF")]
public class NailType : NamedEntity
{
    [Column("NAILER_ID")]
    public int InternalID { get; set; }

    [Column("NAILER_NAME", TypeName = "varchar(20)")]
    public string NailerName { get; set; }

    [Column("DESC1")]
    public string Description1 { get; set; }

    [Column("DESC2")]
    public string Description2 { get; set; }

    [Column("DESC3")]
    public string Description3 { get; set; }

    [Column("DOCK")]
    public int GreipherPosition { get; set; }

    [Column("NLENGHT")]
    public int Length { get; set; }

    [Column("NWIDTH")]
    public int Width { get; set; }

    [Column("MSIZE")]
    public int Size { get; set; }

    [Column("MCOLOR")]
    public long Color { get; set; }
}