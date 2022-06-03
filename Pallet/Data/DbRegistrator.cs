using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pallet.Database;
using Pallet.Database.Context;

namespace Pallet.Data;

/// <summary>
/// The db registrator.
/// </summary>
internal static class DbRegistrator
{
    /// <summary>
    /// Registers the database.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <param name="Configuration">The configuration.</param>
    /// <returns>An IServiceCollection.</returns>
    public static IServiceCollection RegisterDatabase(this IServiceCollection services, IConfiguration Configuration) => services
        .AddDbContext<DatabaseDB>(opt =>
        {
            var type = Configuration["Type"];
            switch (type)
            {
                case null: throw new InvalidOperationException("Not defined database type");
                case "MSSQL": opt.UseSqlServer(Configuration.GetConnectionString(type)); break;
                case "SQLite": opt.UseSqlite(Configuration.GetConnectionString(type)); break;
                case "InMemory": opt.UseInMemoryDatabase("Database.db"); break;
                default: throw new InvalidOperationException($"Connection type {type} not supported");
            }
        }, ServiceLifetime.Transient)
        .AddTransient<DbInitializer>()
        .AddRepositoriesInDB()
    ;
}