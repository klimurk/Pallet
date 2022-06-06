using Pallet.Database.Entities.Base;
using Pallet.Models.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pallet.Database.Entities.OPC;
/// <summary>
/// The OPC Alarm definition .
/// </summary>

[Table("ALARM_DEF")]
public class Alarm : NamedEntity, IAlarm
{
    /// <summary>
    /// Internal error number in PLC.
    /// </summary>
    [Column("ALM_NR")]
    public int NR { get; set; }

    [Column("ALM_NAME", TypeName = "varchar(50)")]
    public new string Name { get; set; }

    [Column("DEVICE", TypeName = "varchar(50)")]
    public string Device { get; set; }

    [Column("TEXT1", TypeName = "nvarchar(200)")]
    public string Alarmtext1 { get; set; }

    [Column("TEXT2", TypeName = "nvarchar(200)")]
    public string Alarmtext2 { get; set; }

    [Column("TEXT3", TypeName = "nvarchar(200)")]
    public string Alarmtext3 { get; set; }

    [Column("PRIO", TypeName = "char(1)")]
    public string Priority { get; set; }

    [Column("STYPE", TypeName = "char(1)")]
    public string StopType { get; set; }

    [Column("ALM_ADDRESS", TypeName = "varchar(100)")]
    public string Address { get; set; }

    [Column("INVERTED")]
    public bool Inverted { get; set; }
}