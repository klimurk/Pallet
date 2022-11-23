using Pallet.BaseDatabase.Base;
using Pallet.BaseDatabase.Repositories.Interfaces;
using Pallet.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Pallet.BaseDatabase.Repositories
{
    /// <summary>
    /// The base database repository realization.
    /// </summary>
    public class DbRepository<T> : IDbRepository<T> where T : Entity, new()
    {
        #region Fields

        protected readonly DbContext _db;
        protected readonly DbSet<T> _Set;

        /// <summary>
        /// Autosave for changes.
        /// </summary>
        public bool AutoSaveChanges { get; set; } = true;

        #endregion Fields

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="DbRepository"/> class.
        /// </summary>
        /// <param name="db">The db.</param>
        public DbRepository(DbContext db)
        {
            _db = db;
            _Set = db.Set<T>();
        }

        public void RefreshAll(T obj) => _db.Entry(obj).Reload();

        public IEnumerable<IProperty> GetColumnNames() => _db.Model.FindEntityType(typeof(T)).GetProperties();

        #endregion Constructor

        /// <summary>
        /// All items of repository.
        /// </summary>
        public virtual IQueryable<T> Items => _Set;

        #region Get

        /// <summary>
        /// Get value.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>A T.</returns>
        public T Get(object id) => Items.SingleOrDefault(item => item.ID.ToString() == id.ToString());

        /// <summary>
        /// Get value async.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="Cancel">The cancel.</param>
        /// <returns>A Task.</returns>
        public async Task<T> GetAsync(object id, CancellationToken Cancel = default) => await Items
            .SingleOrDefaultAsync(item => item.ID.ToString() == id.ToString(), Cancel);

        #endregion Get

        #region Add

        /// <summary>
        /// Add item in repository and db.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>A T.</returns>
        public virtual T Add(T item)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));
            _db.Entry(item).State = EntityState.Added;
            if (AutoSaveChanges)
                _db.SaveChanges();
            return item;
        }

        /// <summary>
        /// Add item in repository and db async
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="Cancel">The cancel.</param>
        /// <returns>A Task.</returns>
        public async Task<T> AddAsync(T item, CancellationToken Cancel = default)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));
            _db.Entry(item).State = EntityState.Added;
            if (AutoSaveChanges)
                await _db.SaveChangesAsync(Cancel);

            return item;
        }

        #endregion Add

        #region Update

        /// <summary>
        /// Update item in repository and db.
        /// </summary>
        /// <param name="item">The item.</param>
        public void Update(T item)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));
            _db.Entry(item).State = EntityState.Modified;
            if (AutoSaveChanges) _db.SaveChanges();
        }

        /// <summary>
        /// Update item in repository and db async.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="Cancel">The cancel.</param>
        /// <returns>A Task.</returns>
        public async Task UpdateAsync(T item, CancellationToken Cancel = default)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));
            _db.Entry(item).State = EntityState.Modified;
            if (AutoSaveChanges) await _db.SaveChangesAsync(Cancel);
        }

        #endregion Update

        #region Remove

        /// <summary>
        /// Remove item in repository and db.
        /// </summary>
        /// <param name="id">The id.</param>
        public void Remove(object id)
        {
            var item = _Set.Local.FirstOrDefault(i => i.ID.ToString() == id.ToString()) ?? new T { ID = id };

            _db.Remove(item);

            if (AutoSaveChanges) _db.SaveChanges();
        }

        /// <summary>
        /// Remove item in repository and db async.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="Cancel">The cancel.</param>
        /// <returns>A Task.</returns>
        public async Task RemoveAsync(object id, CancellationToken Cancel = default)
        {
            _db.Remove(new T { ID = id });
            if (AutoSaveChanges) await _db.SaveChangesAsync(Cancel);
        }

        #endregion Remove

        private bool disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed && disposing)
            {
                _db.Dispose();
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    
    }
}