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
}