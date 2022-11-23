using Pallet.BaseDatabase.Base;
using Pallet.BaseDatabase.Repositories;
using Pallet.ExternalDatabase.Context;

namespace Pallet.ExternalDatabase.Repositories
{
    /// <summary>
    /// The base database repository realization.
    /// </summary>
    internal class DbExternalRepository<T> : DbRepository<T> where T : Entity, new()
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="DbRepository"/> class.
        /// </summary>
        /// <param name="db">The db.</param>
        public DbExternalRepository(ExternalDbContext db) : base(db) { }

        #endregion Constructor

        #region Remove

        /// <summary>
        /// Remove item in repository and db.
        /// </summary>
        /// <param name="id">The id.</param>
        public new void Remove(object id) => throw new NotSupportedException("DON'T REMOVE ITEMS FROM EXTERNAL DATABASE");

        /// <summary>
        /// Remove item in repository and db async.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="Cancel">The cancel.</param>
        /// <returns>A Task.</returns>
        public new async Task RemoveAsync(object id, CancellationToken Cancel = default) => throw new NotSupportedException("DON'T REMOVE ITEMS FROM EXTERNAL DATABASE");

        #endregion Remove
    }
}