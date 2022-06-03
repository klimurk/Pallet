using Pallet.Database.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pallet.Database.Entities.OLD;

[Table("WPROD_DEF")]
public class Product : NamedEntity

{
    [Column("WPROD_ID")]
    public int ProdID { get; set; }

    [Column("WPROD_NAME", TypeName = "varchar(20)")]
    public new string Name { get; set; }

    [Column("DESC1", TypeName = "nvarchar(200)")]
    public string Description1 { get; set; }

    [Column("DESC2", TypeName = "nvarchar(200)")]
    public string Description2 { get; set; }

    [Column("DESC3", TypeName = "nvarchar(200)")]
    public string Description3 { get; set; }

    [Column("STEP1")]
    public bool Step1 { get; set; }

    [Column("STEP2")]
    public bool Step2 { get; set; }

    [Column("SIZE1_X")]
    public int Size1X { get; set; }

    [Column("SIZE1_Y")]
    public int Size1Y { get; set; }

    [Column("SIZE1_Z")]
    public int Size1Z { get; set; }

    [Column("SIZE2_X")]
    public int Size2X { get; set; }

    [Column("SIZE2_Y")]
    public int Size2Y { get; set; }

    [Column("SIZE2_Z")]
    public int Size2Z { get; set; }

    [Column("PRESET")]
    public bool Preset { get; set; }

    [Column("TYPE")]
    public int Type { get; set; }

    [Column("PROD")]
    public int Prod { get; set; }
}