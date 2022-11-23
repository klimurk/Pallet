using Pallet.ExternalDatabase.Context;
using Pallet.ExternalDatabase.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Pallet.ExternalDatabase;

/// <summary>
/// The db registrator.
/// </summary>
public static class DbRegistrator
{
    /// <summary>
    /// Registers the database.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <param name="Configuration">The configuration.</param>
    /// <returns>An IServiceCollection.</returns>
    public static IServiceCollection RegisterExternalDatabase(this IServiceCollection services, IConfiguration Configuration)
    {
        services.AddDbContext<ExternalDbContext>(opt =>
        {
            var type = Configuration["ExternalType"];
            switch (type)
            {
                case null: throw new InvalidOperationException("Not defined database type");
                case "MSSQL": opt.UseSqlServer(Configuration.GetConnectionString(type)); break;
                case "MSSQLDev": opt.UseSqlServer(Configuration.GetConnectionString(type)); break;
                case "SQLite": opt.UseSqlite(Configuration.GetConnectionString(type)); break;
                case "MySQL": opt.UseMySQL(Configuration.GetConnectionString(type)); break;
                default: throw new InvalidOperationException($"Connection type {type} not supported");
            }
        }, ServiceLifetime.Transient);

        services.AddRepositoriesInDB();
        return services;
        ;
    }
}