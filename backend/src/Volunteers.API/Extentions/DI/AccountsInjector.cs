using Accounts.Application.Extentions.DI;
using Accounts.Infrastructure.Extentions.DI;

namespace Volunteers.API.Extentions.DI
{
    public static class AccountsInjector
    {
        public static IServiceCollection AddAccountsDI(this IServiceCollection services, WebApplicationBuilder builder)
        {
            services.AddAccountsInfrastructure(builder);
            services.AddAccountsApplication();

            return services;
        }
    }
}
