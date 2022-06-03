using Microsoft.EntityFrameworkCore;
using Pallet.Database.Context;
using Pallet.Database.Entities.Change.Products;
using System.Linq;

namespace Pallet.Database.Repositories
{
    internal class NailsRepository : DbRepository<Nail>
    {
        public override IQueryable<Nail> Items =>
            base.Items
            .Include(item => item.Nailer);

        public NailsRepository(DatabaseDB db) : base(db)
        {
        }
    }
}