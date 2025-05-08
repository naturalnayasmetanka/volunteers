using Accounts.Application.Accounts.Commands.Login;
using Accounts.Application.Accounts.Commands.Login.Commands;
using Accounts.Application.Accounts.Commands.Register;
using Accounts.Application.Accounts.Commands.Register.Commands;
using Microsoft.Extensions.DependencyInjection;
using Shared.Core.Abstractions.Handlers;

namespace Accounts.Application.Extentions.DI;

public static class AccountsApplicationInjector
{
    public static IServiceCollection AddAccountsApplication(this IServiceCollection services)
    {
        services.AddAccountsCommands();

        return services;
    }

    private static IServiceCollection AddAccountsCommands(this IServiceCollection services)
    {
        services.AddScoped<ICommandHandler<string, RegisterUserCommand>, RegisterUserHandler>();
        services.AddScoped<ICommandHandler<string, LoginUserCommand>, LoginUserHandler>();

        return services;
    }
}
