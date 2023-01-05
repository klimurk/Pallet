using Pallet.InternalDatabase.Context;
using Pallet.InternalDatabase.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Pallet.InternalDatabase;

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
    

    public static IServiceCollection RegisterInternalDatabase(this IServiceCollection services, IConfiguration Configuration)
    {
        services.AddDbContext<InternalDbContext>(opt =>
        {
            var type = Configuration["InternalType"];
            switch (type)
            {
                case null: throw new InvalidOperationException("Not defined database type");
                case "MSSQL":
                case "MSSQLDev":
                    opt.UseSqlServer(Configuration.GetConnectionString(type), sqlServerOptionsAction: sqlOptions => sqlOptions.EnableRetryOnFailure());
                    break;

                case "SQLite": opt.UseSqlite(Configuration.GetConnectionString(type)); break;
                default: throw new InvalidOperationException($"Connection type {type} not supported");
            }
        }, ServiceLifetime.Transient);

        services.AddTransient<InternalDbInitializer>();
        services.AddRepositoriesInDB();
        return services;
        ;
    }
}