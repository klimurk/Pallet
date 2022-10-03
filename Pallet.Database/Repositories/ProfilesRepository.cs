using Microsoft.EntityFrameworkCore;
using Pallet.Database.Context;
using Pallet.Database.Entities.ProfileData.Profiles;
using System;
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

        public override Profile Add(Profile item)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));
            _db.Attach(item).State = EntityState.Added;
            if (AutoSaveChanges)
                _db.SaveChanges();
            return item;
        }
    }
}