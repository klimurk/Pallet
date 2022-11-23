using Microsoft.EntityFrameworkCore.Metadata;
using Pallet.BaseDatabase.Base;
using Pallet.InternalDatabase.Entities.Base.Interfaces;
using System.Collections.ObjectModel;

namespace Pallet.BaseDatabase.Repositories.Interfaces;

public interface IDbRepository<T> where T : class, IEntity, new()
{
    public IEnumerable<IProperty> GetColumnNames();

    public void RefreshAll(T obj); //? TEST

    /// <summary>
    /// Items from database.
    /// </summary>
    IQueryable<T> Items { get; }

    /// <summary>
    /// Get item by ID
    /// </summary>
    /// <param name="id">The id.</param>
    /// <returns>A T.</returns>
    T Get(object id);

    /// <summary>
    /// Get item by ID async.
    /// </summary>
    /// <param name="id">The id.</param>
    /// <param name="Cancel">The cancel.</param>
    /// <returns>A Task.</returns>
    Task<T> GetAsync(object id, CancellationToken Cancel = default);

    /// <summary>
    /// Add item and save.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <returns>A T.</returns>
    T Add(T item);

    /// <summary>
    /// Add item and save async.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <param name="Cancel">The cancel.</param>
    /// <returns>A Task.</returns>
    Task<T> AddAsync(T item, CancellationToken Cancel = default);

    /// <summary>
    /// Update item and save.
    /// </summary>
    /// <param name="item">The item.</param>
    void Update(T item);

    /// <summary>
    /// Update item and save async.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <param name="Cancel">The cancel.</param>
    /// <returns>A Task.</returns>
    Task UpdateAsync(T item, CancellationToken Cancel = default);

    /// <summary>
    /// Remove item and save.
    /// </summary>
    /// <param name="id">The id.</param>
    void Remove(object id);

    /// <summary>
    /// Remove item and save async.
    /// </summary>
    /// <param name="id">The id.</param>
    /// <param name="Cancel">The cancel.</param>
    /// <returns>A Task.</returns>
    Task RemoveAsync(object id, CancellationToken Cancel = default);
}