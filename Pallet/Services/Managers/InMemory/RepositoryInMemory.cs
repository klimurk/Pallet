using Pallet.Database.Entities.Base.Interfaces;
using Pallet.Services.Managers.InMemory.Interfaces;

namespace Pallet.Services.Managers.InMemory;

internal abstract class RepositoryInMemory<T> : IRepository<T> where T : IEntity
{
    private readonly List<T> _Items = new();
    private int _LastId;

    protected RepositoryInMemory()
    { }

    protected RepositoryInMemory(IEnumerable<T> items)
    {
        foreach (var item in items)
            Add(item);
    }

    public void Add(T item)
    {
        if (item is null) throw new ArgumentNullException(nameof(item));

        if (_Items.Contains(item)) return;

        item.ID = ++_LastId;
        _Items.Add(item);
    }

    public IEnumerable<T> GetAll() => _Items;

    public bool Remove(T item) => _Items.Remove(item);

    public void Update(int id, T item)
    {
        if (item is null) throw new ArgumentNullException(nameof(item));
        if (id <= 0) throw new ArgumentOutOfRangeException(nameof(id), id, "Index cannot be less than 1");

        if (_Items.Contains(item)) return;

        var db_item = ((IRepository<T>)this).Get(id);
        if (db_item is null)
            throw new InvalidOperationException("Edited element wasn't finded in repository");

        Update(item, db_item);
    }

    protected abstract void Update(T Source, T Destination);
}