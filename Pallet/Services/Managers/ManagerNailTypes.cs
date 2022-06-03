using Pallet.Database.Entities.Change.Types;
using Pallet.Database.Repositories.Interfaces;
using Pallet.Services.Managers.Interfaces;

namespace Pallet.Services.Managers
{
    public class ManagerNailTypes : IManagerNailTypes
    {
        public Nailer ActiveNailType { get; set; }

        private readonly IDbRepository<Nailer> _RepositoryNailTypes;

        public ManagerNailTypes(
            IDbRepository<Nailer> RepositoryNailTypes
            )
        {
            ActiveNailType = new();
            _RepositoryNailTypes = RepositoryNailTypes;
        }

        public List<Nailer> NailTypes => _RepositoryNailTypes.Items.ToList();
    }
}