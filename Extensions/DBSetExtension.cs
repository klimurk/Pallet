using Microsoft.EntityFrameworkCore;

namespace Pallet.Extensions;

public static class DBSetExtension
{
    public static void AddOrUpdateRange<TEntity>(this DbSet<TEntity> set, IEnumerable<TEntity> entities)
            where TEntity : class
    {
        foreach (TEntity entity in entities)
        {
            set.AddOrUpdate(entity);
        }
    }

    public static void AddOrUpdate<TEntity>(this DbSet<TEntity> set, TEntity entity) where TEntity : class
    {
        _ = !set.Any(e => e == entity) ? set.Add(entity) : set.Update(entity);
    }
}