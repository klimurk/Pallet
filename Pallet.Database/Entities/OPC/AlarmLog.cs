using Pallet.Database.Entities.Base;
using Pallet.Models.Interfaces;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pallet.Database.Entities.OPC;

/// <summary>
/// The OPC Alarms logging data.
/// </summary>
[Table("ALARM_LOG")]
public class AlarmLog : NamedEntity, IAlarm
{
    /// <summary>
    /// Internal error number in PLC.
    /// </summary>
    [Column("ALM_NR")]
    public int NR { get; set; }

    /// <summary>
    /// Name of signal.
    /// </summary>
    [Column("ALM_NAME", TypeName = "varchar(50)")]
    public override string Name { get; set; }

    /// <summary>
    /// Device name.
    /// </summary>
    [Column("DEVICE", TypeName = "varchar(50)")]
    public string Device { get; set; }

    /// <summary>
    /// AlarmText English.
    /// </summary>
    [Column("TEXT1", TypeName = "nvarchar(200)")]
    public string DescriptionEn { get; set; }

    /// <summary>
    /// AlarmText German.
    /// </summary>
    [Column("TEXT2", TypeName = "nvarchar(200)")]
    public string DescriptionDe { get; set; }

    /// <summary>
    /// AlarmText Czech.
    /// </summary>
    [Column("TEXT3", TypeName = "nvarchar(200)")]
    public string DescriptionLocal { get; set; }

    /// <summary>
    /// Alarm priority.
    /// </summary>
    [Column("PRIO", TypeName = "char(1)")]
    public string Priority { get; set; }

    /// <summary>
    /// Alarm stop type.
    /// </summary>
    [Column("STYPE", TypeName = "char(1)")]
    public string StopType { get; set; }

    /// <summary>
    /// OPC address.
    /// </summary>
    [Column("ALM_ADDRESS", TypeName = "varchar(100)")]
    public string Address { get; set; }

    /// <summary>
    /// Inverted alarm or not.
    /// </summary>
    [Column("INVERTED")]
    public bool Inverted { get; set; }

    /// <summary>
    /// Alarm timestamp beginning.
    /// </summary>
    [Column("TIMESTAMP1")]
    public DateTime TimestampStart { get; set; }

    /// <summary>
    /// Alarm timestamp end.
    /// </summary>
    [Column("TIMESTAMP2")]
    public DateTime? TimestampEnd { get; set; }

    /// <summary>
    /// Alarm is ended.
    /// </summary>
    [Column("GONE")]
    public bool Gone { get; set; }

    /// <summary>
    /// Gets the full type of alarm .
    /// </summary>
    [NotMapped]
    public string FullType => Priority + StopType;

    /// <summary>
    /// Duration of alarm.
    /// </summary>
    [NotMapped]
    public string Duration
    {
        get
        {
            TimeSpan? ts = TimestampEnd is null ? DateTime.Now.Subtract(TimestampStart) : TimestampEnd - TimestampStart;
            return ts is null
                ? ""
                : (ts?.Hours + (24 * ts?.Days)).ToString() + ":"
                    + (ts?.Minutes < 10 ? "0" + ts?.Minutes.ToString() : ts?.Minutes.ToString()) + ":"
                    + (ts?.Seconds < 10 ? "0" + ts?.Seconds.ToString() : ts?.Seconds.ToString());
        }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="AlarmLog"/> class.
    /// </summary>
    public AlarmLog()
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="AlarmLog"/> class.
    /// </summary>
    /// <param name="alarm">Base alarm.</param>
    public AlarmLog(Alarm alarm)
    {
        Address = alarm.Address;
        DescriptionEn = alarm.DescriptionEn;
        DescriptionDe = alarm.DescriptionDe;
        DescriptionLocal = alarm.DescriptionLocal;
        Device = alarm.Device;
        Inverted = alarm.Inverted;
        Name = alarm.Name;
        NR = alarm.NR;
        Priority = alarm.Priority;
        StopType = alarm.StopType;
    }
}