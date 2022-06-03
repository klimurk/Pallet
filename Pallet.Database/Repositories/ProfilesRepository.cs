using Microsoft.EntityFrameworkCore;
using Pallet.Database.Context;
using Pallet.Database.Entities.Change.Profiles;
using System.Linq;

namespace Pallet.Database.Repositories
{
    internal class ProfilesRepository : DbRepository<Profile>
    {
        public override IQueryable<Profile> Items =>
            base.Items
            .Include(item => item.Table)
            .Include(item => item.ProfileProducts).ThenInclude(s => s.Product).ThenInclude(s => s.Elements)
            .Include(item => item.ProfileProducts).ThenInclude(s => s.Product).ThenInclude(s => s.Elements).ThenInclude(s => s.Element)
            .Include(item => item.ProfileProducts).ThenInclude(s => s.Product).ThenInclude(s => s.Nails).ThenInclude(s => s.Nailer)
            ;

        public ProfilesRepository(DatabaseDB db) : base(db)
        {
        }
    }
}