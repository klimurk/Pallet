using Microsoft.Extensions.DependencyInjection;
using Pallet.Database.Entities.Change.Products;
using Pallet.Database.Entities.Change.Profiles;
using Pallet.Database.Entities.Change.Tables;
using Pallet.Database.Entities.Change.Tools;
using Pallet.Database.Entities.Change.Types;
using Pallet.Database.Entities.OPC;
using Pallet.Database.Entities.Users;
using Pallet.Database.Repositories;
using Pallet.Database.Repositories.Interfaces;

namespace Pallet.Database
{
    public static class RepositoryRegistrator
    {
        public static IServiceCollection AddRepositoriesInDB(this IServiceCollection services) =>
            services
            .AddTransient<IDbRepository<AlarmLog>, DbRepository<AlarmLog>>()
            .AddTransient<IDbRepository<Alarm>, DbRepository<Alarm>>()
            .AddTransient<IDbRepository<Signal>, DbRepository<Signal>>()
            .AddTransient<IDbRepository<User>, DbRepository<User>>()

            .AddTransient<IDbRepository<Profile>, ProfilesRepository>()

            .AddTransient<IDbRepository<Table>, TablesRepository>()

            .AddTransient<IDbRepository<Tool>, DbRepository<Tool>>()

            .AddTransient<IDbRepository<Nail>, NailsRepository>()
            .AddTransient<IDbRepository<Product>, ProductsRepository>()
            .AddTransient<IDbRepository<ElementPosition>, ElementPositionsRepository>()

            .AddTransient<IDbRepository<Element>, ElementsRepository>()
            .AddTransient<IDbRepository<Nailer>, NailersRepository>()
            ;
    }
}