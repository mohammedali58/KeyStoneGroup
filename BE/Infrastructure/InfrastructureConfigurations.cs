using Domain.Common.Interfaces;
using Infrastructure.Interfaces;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class InfrastructureConfigurations
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configurations)
        {

            services.AddDbContext<ApplicationDbContext>(options => 
                options.UseSqlite(configurations.GetConnectionString("DefaultConnection"), 
                builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)
                ));

            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<IContactRepository, ContactRepository>();

            services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

            services.AddScoped<ApplicationDbContextInitializer>();

            return services;
            
        }
    }
}
