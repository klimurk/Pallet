using Microsoft.EntityFrameworkCore;
using Pallet.Database.Context;
using Pallet.Database.Entities.Change.Tables;
using System.Linq;

namespace Pallet.Database.Repositories
{
    internal class TablesRepository : DbRepository<Table>
    {
        public override IQueryable<Table> Items =>
            base.Items
            .Include(item => item.Profiles)
            ;

        public TablesRepository(DatabaseDB db) : base(db)
        {
        }
    }
}