﻿namespace Pallet.Extensions
{
    public static class EntityFrameworkQueryableExtensions
    {
        public static IQueryable<T> IncludeAll<T>(this IQueryable<T> queryable) where T : class
        {
            var type = typeof(T);
            var properties = type.GetProperties();
            foreach (var property in properties)
            {
                if (property.GetGetMethod().IsVirtual && Array.Find(properties, c => c.Name == property.Name + "Id") != null)
                {
                    queryable = queryable.Include(property.Name);
                }
            }
            return queryable;
        }

        public static bool In<T, Y>(this T source, params T[] list) where T : IEnumerable<Y> where Y : class
        {
            return list.Contains(source);
        }
    }
}