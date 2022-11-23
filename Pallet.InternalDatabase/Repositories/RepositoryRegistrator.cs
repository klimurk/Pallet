using Pallet.BaseDatabase.Repositories.Interfaces;
using Pallet.InternalDatabase.Entities.Log;
using Pallet.InternalDatabase.Entities.OPC;
using Pallet.InternalDatabase.Entities.Users;
using Microsoft.Extensions.DependencyInjection;

namespace Pallet.InternalDatabase.Repositories
{
    internal static class RepositoryRegistrator
    {
        public static IServiceCollection AddRepositoriesInDB(this IServiceCollection services) =>
            services
            .AddScoped<IDbRepository<AlarmLog>, DbInternalRepository<AlarmLog>>()
            .AddScoped<IDbRepository<Alarm>, DbInternalRepository<Alarm>>()
            .AddScoped<IDbRepository<Signal>, DbInternalRepository<Signal>>()
            .AddScoped<IDbRepository<User>, DbInternalRepository<User>>()
            .AddScoped<IDbRepository<Log>, DbInternalRepository<Log>>()
            .AddScoped<IDbRepository<SystemEvent>, DbInternalRepository<SystemEvent>>()
            ;
    }
}