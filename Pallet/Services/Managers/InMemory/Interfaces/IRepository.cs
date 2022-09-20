using Pallet.Database.Entities.Base.Interfaces;

namespace Pallet.Services.Managers.InMemory.Interfaces;

/// <summary>
/// Base inMemory repository.
/// </summary>
public interface IRepository<T> where T : IEntity
{
    /// <summary>
    /// Add item.
    /// </summary>
    /// <param name="item">The item.</param>
    void Add(T item);

    /// <summary>
    /// Get all items.
    /// </summary>
    /// <returns>A list of TS.</returns>
    IEnumerable<T> GetAll();

    /// <summary>
    /// Get item.
    /// </summary>
    /// <param name="id">The id.</param>
    /// <returns>A T.</returns>
    T Get(int id) => GetAll().FirstOrDefault(item => item.ID == id);

    /// <summary>
    /// Remove item.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <returns>A bool.</returns>
    bool Remove(T item);

    /// <summary>
    /// Update item.
    /// </summary>
    /// <param name="id">The id.</param>
    /// <param name="item">The item.</param>
    void Update(int id, T item);
}