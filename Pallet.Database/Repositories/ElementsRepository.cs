using Microsoft.EntityFrameworkCore;
using Pallet.Database.Context;
using Pallet.Database.Entities.Change.Types;
using System.Linq;

namespace Pallet.Database.Repositories
{
    internal class ElementsRepository : DbRepository<Element>
    {
        public override IQueryable<Element> Items =>
            base.Items
            .Include(item => item.Positions);

        public ElementsRepository(DatabaseDB db) : base(db)
        {
        }
    }
}