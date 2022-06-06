namespace Pallet.Models.Interfaces;

/// <summary>
/// Interface for database Alarm.
/// </summary>
public interface IAlarm : ISignal
{
    /// <summary>
    /// Internal error number in PLC.
    /// </summary>
    public int NR { get; set; }
    /// <summary>
    /// Alarm device for.
    /// </summary>
    public string Device { get; set; }
    /// <summary>
    /// Alarm priority.
    /// </summary>
    public string Priority { get; set; }
    /// <summary>
    /// Alarm stop type.
    /// </summary>
    public string StopType { get; set; }
    /// <summary>
    /// Alarm is inverted.
    /// </summary>
    public bool Inverted { get; set; }
    /// <summary>
    /// Alarm text on English.
    /// </summary>
    public string Alarmtext1 { get; set; }
    /// <summary>
    /// Alarm text on German.
    /// </summary>
    public string Alarmtext2 { get; set; }
    /// <summary>
    /// Alarm text on Czech.
    /// </summary>
    public string Alarmtext3 { get; set; }
}