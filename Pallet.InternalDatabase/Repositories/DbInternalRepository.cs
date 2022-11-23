using Pallet.BaseDatabase.Base;
using Pallet.BaseDatabase.Repositories;
using Pallet.InternalDatabase.Context;

namespace Pallet.InternalDatabase.Repositories
{
    /// <summary>
    /// The base database repository realization.
    /// </summary>
    internal class DbInternalRepository<T> : DbRepository<T> where T : Entity, new()
    {
        public DbInternalRepository(InternalDbContext db) : base(db) { }

    }
}