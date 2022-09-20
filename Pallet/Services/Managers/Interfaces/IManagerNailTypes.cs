using Pallet.Database.Entities.ProfileData.Types;

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
    IQueryable<Nailer> NailTypes { get; }
}