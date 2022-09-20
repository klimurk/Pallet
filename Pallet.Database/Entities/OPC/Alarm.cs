using Pallet.Database.Entities.Base;
using Pallet.Database.Entities.Base.Interfaces;
using Pallet.Models.Interfaces;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pallet.Database.Entities.OPC;

[Table("ALARM_DEF")]
public class Alarm : NodeOPC, IAlarm
{
    [Column("ALM_NR")]
    public int NR { get; set; }

    [Column("ALM_NAME", TypeName = "varchar(50)")]
    public new string Name { get; set; }

    [Column("DEVICE", TypeName = "varchar(50)")]
    public string Device { get; set; }

    [Column("TEXT1", TypeName = "nvarchar(200)")]
    public string DescriptionEn { get; set; }

    [Column("TEXT2", TypeName = "nvarchar(200)")]
    public string DescriptionDe { get; set; }

    [Column("TEXT3", TypeName = "nvarchar(200)")]
    public string DescriptionLocal { get; set; }

    [Column("PRIO", TypeName = "char(1)")]
    public string Priority { get; set; }

    [Column("STYPE", TypeName = "char(1)")]
    public string StopType { get; set; }

    [Column("ALM_ADDRESS", TypeName = "varchar(100)")]
    public string Address { get; set; }

    [Column("INVERTED")]
    public bool Inverted { get; set; }

    [NotMapped]
    public DateTime TimeStamp { get; set; }

    public Alarm()
    {
        Value = false;
    }
}