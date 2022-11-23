using Pallet.BaseDatabase.Base.Interfaces;

namespace Pallet.Models.Interfaces;

/// <summary>
/// Interface signal definition.
/// </summary>
public interface ISignal : IDBTranslateble
{
    /// <summary>
    /// Signal name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Signal address.
    /// </summary>
    public string Address { get; set; }

    /// <summary>
    /// Signal from device .
    /// </summary>
    public string Device { get; set; }
}