using Pallet.Database.Entities.Base;
using Pallet.Models.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pallet.Database.Entities.OPC;

[Table("SIGNALS_DEF")]
public class Signal : NodeOPC, ISignal
{
    [Column("SIG_NAME", TypeName = "varchar(50)")]
    public new string Name { get; set; }

    [Column("SIG_ADDRESS", TypeName = "varchar(100)")]
    public string Address { get; set; }

    [Column("DEVICE", TypeName = "varchar(50)")]
    public string Device { get; set; }

    [Column("DESC1", TypeName = "nvarchar(100)")]
    public string DescriptionEn { get; set; }

    [Column("DESC2", TypeName = "nvarchar(100)")]
    public string DescriptionDe { get; set; }

    [Column("DESC3", TypeName = "nvarchar(100)")]
    public string DescriptionLocal { get; set; }

    public Signal()
    {
        Value = false;
    }
}