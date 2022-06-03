using Pallet.Database.Entities.Change.Types;

namespace Pallet.Services.Managers.Interfaces;

public interface IManagerNailTypes
{
    Nailer ActiveNailType { get; set; }
    List<Nailer> NailTypes { get; }
}