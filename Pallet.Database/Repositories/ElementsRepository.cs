using Microsoft.EntityFrameworkCore;
using Pallet.Database.Context;
using Pallet.Database.Entities.Change.Types;
using System.Linq;

namespace Pallet.Database.Repositories
{
    /// <summary>
    /// Realization repository for elements.
    /// </summary>
    internal class ElementsRepository : DbRepository<Element>
    {
        /// <summary>
        /// All items include internal link to positions.
        /// </summary>
        public override IQueryable<Element> Items =>
            base.Items
            .Include(item => item.Positions);

        /// <summary>
        /// Initializes a new instance of the <see cref="ElementsRepository"/> class.
        /// </summary>
        /// <param name="db">The db.</param>
        public ElementsRepository(DatabaseDB db) : base(db)
        {
        }
    }
}