using Pallet.Database.Entities.Change.Types;

namespace Pallet.Services.Managers.Interfaces;
/// <summary>
/// The manager nail types.
/// </summary>
public interface IManagerNailTypes
{
    /// <summary>
    /// Active nailer type.
    /// </summary>
    Nailer ActiveNailType { get; set; }
    /// <summary>
    /// All nailers.
    /// </summary>
    List<Nailer> NailTypes { get; }
}