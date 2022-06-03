using Microsoft.EntityFrameworkCore;
using Pallet.Database.Context;
using Pallet.Database.Entities.Base;
using Pallet.Database.Repositories.Interfaces;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Pallet.Database.Repositories
{
    internal class DbRepository<T> : IDbRepository<T> where T : Entity, new()
    {
        #region Fields

        private readonly DatabaseDB _db;
        private readonly DbSet<T> _Set;

        public bool AutoSaveChanges { get; set; } = true;

        #endregion Fields

        #region Constructor

        public DbRepository(DatabaseDB db)
        {
            _db = db;
            _Set = db.Set<T>();
        }

        #endregion Constructor

        public virtual IQueryable<T> Items => _Set;

        #region Get

        public T Get(int id) => Items.SingleOrDefault(item => item.ID == id);

        public async Task<T> GetAsync(int id, CancellationToken Cancel = default) => await Items
            .SingleOrDefaultAsync(item => item.ID == id, Cancel)
            .ConfigureAwait(false);

        #endregion Get

        #region Add

        public T Add(T item)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));
            _db.Entry(item).State = EntityState.Added;
            if (AutoSaveChanges)
                _db.SaveChanges();
            return item;
        }

        public async Task<T> AddAsync(T item, CancellationToken Cancel = default)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));
            _db.Entry(item).State = EntityState.Added;
            if (AutoSaveChanges)
                await _db.SaveChangesAsync(Cancel).ConfigureAwait(false);

            return item;
        }

        #endregion Add

        #region Update

        public void Update(T item)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));
            _db.Entry(item).State = EntityState.Modified;
            if (AutoSaveChanges)
                _db.SaveChanges();
        }

        public async Task UpdateAsync(T item, CancellationToken Cancel = default)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));
            _db.Entry(item).State = EntityState.Modified;
            if (AutoSaveChanges)
                await _db.SaveChangesAsync(Cancel).ConfigureAwait(false);
        }

        #endregion Update

        #region Remove

        public void Remove(int id)
        {
            var item = _Set.Local.FirstOrDefault(i => i.ID == id) ?? new T { ID = id };

            _db.Remove(item);

            if (AutoSaveChanges)
                _db.SaveChanges();
        }

        public async Task RemoveAsync(int id, CancellationToken Cancel = default)
        {
            _db.Remove(new T { ID = id });
            if (AutoSaveChanges)
                await _db.SaveChangesAsync(Cancel).ConfigureAwait(false);
        }

        #endregion Remove

        //#region Events

        //public event PropertyChangedEventHandler PropertyChanged;

        //#endregion Events
    }
}