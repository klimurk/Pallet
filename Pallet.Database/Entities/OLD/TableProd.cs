using Pallet.Database.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pallet.Database.Entities.OLD;

[Table("WTABLE_WPROD")]
public class TableProd : NamedEntity
{
    [Column("WTABLE_ID")]
    public int TableID { get; set; }

    [Column("WPROD_ID1")]
    public Product Prod1 { get; set; }

    [Column("WPROD_ID2")]
    public Product Prod2 { get; set; }

    [Column("WPROD_ID3")]
    public Product Prod3 { get; set; }

    [Column("WPROD_ID4")]
    public Product Prod4 { get; set; }
}