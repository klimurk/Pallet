﻿using Pallet.Database.Entities.Base.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Pallet.Database.Repositories.Interfaces;

public interface IDbRepository<T> where T : class, IEntity, new()
{
    IQueryable<T> Items { get; }

    T Get(int id);

    Task<T> GetAsync(int id, CancellationToken Cancel = default);

    T Add(T item);

    Task<T> AddAsync(T item, CancellationToken Cancel = default);

    void Update(T item);

    Task UpdateAsync(T item, CancellationToken Cancel = default);

    void Remove(int id);

    Task RemoveAsync(int id, CancellationToken Cancel = default);
}