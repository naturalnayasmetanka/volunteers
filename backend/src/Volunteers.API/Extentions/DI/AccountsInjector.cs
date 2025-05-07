using Accounts.Infrastructure.Extentions.DI;

namespace Volunteers.API.Extentions.DI
{
    public static class AccountsInjector
    {
        public static IServiceCollection AddAccountsDI(this IServiceCollection services)
        {
            services.AddAccountsInfrastructure();

            return services;
        }
    }
}
