using LeaveManagement.Application.Contracts.Persistence;
using LeaveManagement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LeaveManagement.Persistence;

    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<LeaveDatabaseContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("LeaveDatabaseConnectionString"));
            });


            return services;
        }
    }

