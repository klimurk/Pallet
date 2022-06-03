using Pallet.Database.Entities.Base;
using Pallet.Models.Interfaces;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pallet.Database.Entities.OPC;

[Table("ALARM_LOG")]
public class AlarmLog : NamedEntity, IAlarm
{
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

    [Column("TIMESTAMP1")]
    public DateTime TimestampStart { get; set; }

    [Column("TIMESTAMP2")]
    public DateTime? TimestampEnd { get; set; }

    [Column("GONE")]
    public bool Gone { get; set; }

    [NotMapped]
    public string FullType => Priority + StopType;

    [NotMapped]
    public string Duration
    {
        get
        {
            TimeSpan ts = TimestampEnd is not null ? (TimestampEnd ??= DateTime.MinValue).Subtract(TimestampStart) : DateTime.Now.Subtract(TimestampStart);
            return (ts.Hours + (24 * ts.Days)).ToString() + ":"
                + (ts.Minutes < 10 ? "0" + ts.Minutes.ToString() : ts.Minutes.ToString()) + ":"
                + (ts.Seconds < 10 ? "0" + ts.Seconds.ToString() : ts.Seconds.ToString());
        }
    }

    public AlarmLog()
    { }

    public AlarmLog(Alarm alarm)
    {
        Address = alarm.Address;
        Alarmtext1 = alarm.Alarmtext1;
        Alarmtext2 = alarm.Alarmtext2;
        Alarmtext3 = alarm.Alarmtext3;
        Device = alarm.Device;
        Inverted = alarm.Inverted;
        Name = alarm.Name;
        NR = alarm.NR;
        Priority = alarm.Priority;
        StopType = alarm.StopType;
    }
}