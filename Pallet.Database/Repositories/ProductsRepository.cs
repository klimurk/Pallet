using Microsoft.EntityFrameworkCore;
using Pallet.Database.Context;
using Pallet.Database.Entities.ProfileData.Products;
using System.Linq;

namespace Pallet.Database.Repositories
{
    internal class ProductsRepository : DbRepository<Product>
    {
        public override IQueryable<Product> Items =>
            base.Items
            .Include(item => item.Elements)
            .Include(item => item.Nails);

        public ProductsRepository(DatabaseDB db) : base(db)
        {
        }
    }
}