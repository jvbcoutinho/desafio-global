using Desafio.Domain.UserAggregate;
using Desafio.Repository.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Desafio.Repository
{
    public static class ConfigurationModule
    {
        public static void RegisterRepository(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<UserContext>(opt =>
            {
                opt.UseSqlServer(connectionString);
            });

            services.AddTransient<IUserRepository, UserRepository>();
        }
    }
}