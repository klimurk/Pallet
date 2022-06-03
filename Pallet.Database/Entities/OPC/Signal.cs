using Pallet.Database.Entities.Base;
using Pallet.Models.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pallet.Database.Entities.OPC;

[Table("SIGNALS_DEF")]
public class Signal : NamedEntity, ISignal
{
    [Column("SIG_NAME", TypeName = "varchar(50)")]
    public new string Name { get; set; }

    [Column("SIG_ADDRESS", TypeName = "varchar(100)")]
    public string Address { get; set; }
}