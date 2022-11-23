using Microsoft.Extensions.DependencyInjection;

namespace Pallet.ExternalDatabase.Repositories
{
    internal static class RepositoryRegistrator
    {
        public static IServiceCollection AddRepositoriesInDB(this IServiceCollection services) =>
            services
            //.AddTransient<IDbRepository<AlarmLog>, DbExternalRepository<AlarmLog>>()
            //.AddTransient<IDbRepository<Alarm>, DbExternalRepository<Alarm>>()
            //.AddTransient<IDbRepository<Signal>, DbExternalRepository<Signal>>()
            //.AddTransient<IDbRepository<User>, DbExternalRepository<User>>()
            //.AddTransient<IDbRepository<Log>, DbExternalRepository<Log>>()
            //.AddTransient<IDbRepository<SystemEvent>, DbExternalRepository<SystemEvent>>()
            ;
    }
}