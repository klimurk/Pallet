namespace Pallet.BaseDatabase.CustomDbContext;

public class ExceptionHandlerDbContext<T> where T : class
{
    private DbSet<T> dbSet;

    public ExceptionHandlerDbContext(DbSet<T> realDbSet) => dbSet = realDbSet;

    public List<T> ToList()
    {
        try
        {
            return dbSet.ToList();
        }
        catch (Exception e)
        {
            // Do some logging..
            throw;
        }
    }

    public T? SingleOrDefault()
    {
        try
        {
            return dbSet.SingleOrDefault();
        }
        catch (Exception e)
        {
            // Do some logging..
            throw;
        }
    }
}