using Microsoft.EntityFrameworkCore;
using Pallet.Database.Context;
using Pallet.Database.Entities.Change.Products;
using System.Linq;

namespace Pallet.Database.Repositories
{
    internal class ElementPositionsRepository : DbRepository<ElementPosition>
    {
        public override IQueryable<ElementPosition> Items =>
            base.Items
            .Include(item => item.Element);

        public ElementPositionsRepository(DatabaseDB db) : base(db)
        {
        }
    }
}