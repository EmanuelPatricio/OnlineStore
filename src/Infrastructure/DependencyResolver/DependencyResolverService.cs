using Application.Core.Repositories;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.DependencyResolver;
public static class DependencyResolverService
{
    public static void Register(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<OnlineStoreContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DbConnection"),
            x => x.MigrationsAssembly("DbMigrations")));

        services.AddScoped(typeof(IBaseRepositoryAsync<>), typeof(BaseRepositoryAsync<>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }

    public static void MigrateDatabase(IServiceProvider serviceProvider)
    {
        var dbContextOptions = serviceProvider.GetRequiredService<DbContextOptions<OnlineStoreContext>>();
        using var dbContext = new OnlineStoreContext(dbContextOptions);
        dbContext.Database.Migrate();
    }
}