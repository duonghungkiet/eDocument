using eDocument.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace eDocument.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));
            services.Scan(scan => scan
                .FromAssemblies(typeof(DependencyInjection).Assembly)
                .AddClasses(classes => classes.InNamespaces(
                    "eDocument.Infrastructure.Security",
                    "eDocument.Infrastructure.Persistence",
                    "eDocument.Infrastructure.Repositories"
                ))
                .AsImplementedInterfaces()
                .WithScopedLifetime());

            return services;
        }
    }
}
