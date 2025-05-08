using Accounts.Infrastructure.Contexts;
using Accounts.Infrastructure.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Accounts.Infrastructure.Extentions.DI;

public static class AccountsInfrastructureInjector
{
    public static IServiceCollection AddAccountsInfrastructure(this IServiceCollection services)
    {
        services
            .AddIdentity<User, Role>(options =>
            {
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<AccountDbContext>();

        services.AddScoped<AccountDbContext>();

        return services;
    }

    public static IServiceCollection AddAccountsAuthentication(this IServiceCollection services, WebApplicationBuilder builder)
    {
        builder.Services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = "test",
                    ValidAudience = "test",
                    IssuerSigningKey = new SymmetricSecurityKey("qwertyuiopasdfdghjklklzxcvbnmpoiuytrewkjhgf"u8.ToArray()),

                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = false,
                };
            });

        return services;
    }
}
