using Pallet.Database.Entities.Base.Interfaces;

namespace Pallet.Services.Managers.InMemory.Interfaces;

public interface IRepository<T> where T : IEntity
{
    void Add(T item);

    IEnumerable<T> GetAll();

    T Get(int id) => GetAll().FirstOrDefault(item => item.ID == id);

    bool Remove(T item);

    void Update(int id, T item);
}