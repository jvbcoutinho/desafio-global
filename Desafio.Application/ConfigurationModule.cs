using Desafio.Application.Authentication;
using Desafio.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Desafio.Application
{
    public static class ConfigurationModule
    {
        public static void RegisterAuthApplication(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();

            services.RegisterRepository();
        }
    }
}