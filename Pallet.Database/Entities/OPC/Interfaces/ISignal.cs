namespace Pallet.Models.Interfaces;

/// <summary>
/// Interface signal definition.
/// </summary>
public interface ISignal
{
    /// <summary>
    /// Signal name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Signal address.
    /// </summary>
    public string Address { get; set; }
}