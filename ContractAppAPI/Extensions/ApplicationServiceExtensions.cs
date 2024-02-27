using ContractAppAPI.Data;
using ContractAppAPI.Interfaces;
using ContractAppAPI.Services;
using Microsoft.EntityFrameworkCore;

namespace ContractAppAPI.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            }
            );
            services.AddCors();
            services.AddScoped<ITokenService, TokenService>();

            return services;
        }
    }
}