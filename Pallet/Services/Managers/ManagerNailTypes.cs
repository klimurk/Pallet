using Pallet.Database.Entities.ProfileData.Types;
using Pallet.Database.Repositories.Interfaces;
using Pallet.Services.Managers.Interfaces;

namespace Pallet.Services.Managers
{
    public class ManagerNailTypes : IManagerNailTypes
    {
        public Nailer ActiveNailType { get; set; } = new();

        private readonly IDbRepository<Nailer> _RepositoryNailTypes;

        public ManagerNailTypes(IDbRepository<Nailer> RepositoryNailTypes) => _RepositoryNailTypes = RepositoryNailTypes;

        public IQueryable<Nailer> NailTypes => _RepositoryNailTypes.Items;
    }
}