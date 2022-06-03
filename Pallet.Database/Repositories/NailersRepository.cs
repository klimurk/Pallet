using Microsoft.EntityFrameworkCore;
using Pallet.Database.Context;
using Pallet.Database.Entities.Change.Types;
using System.Linq;

namespace Pallet.Database.Repositories
{
    internal class NailersRepository : DbRepository<Nailer>
    {
        public override IQueryable<Nailer> Items =>
            base.Items
            .Include(item => item.Nails);

        public NailersRepository(DatabaseDB db) : base(db)
        {
        }
    }
}