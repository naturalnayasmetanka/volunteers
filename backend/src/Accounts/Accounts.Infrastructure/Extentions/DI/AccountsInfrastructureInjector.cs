using System.Text;
using Accounts.Infrastructure.Abstractions;
using Accounts.Infrastructure.Contexts;
using Accounts.Infrastructure.Models;
using Accounts.Infrastructure.Options;
using Accounts.Infrastructure.Providers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Accounts.Infrastructure.Extentions.DI;

public static class AccountsInfrastructureInjector
{
    public static IServiceCollection AddAccountsInfrastructure(this IServiceCollection services, WebApplicationBuilder builder)
    {
        services.AddJwtOptions(builder);

        services
            .AddIdentity<User, Role>(options =>
            {
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<AccountDbContext>()
            .AddDefaultTokenProviders();

        services.AddScoped<AccountDbContext>();

        services.AddTransient<ITokenProvider, JwtTokenProvider>();

        return services;
    }

    public static IServiceCollection AddAccountsAuthentication(this IServiceCollection services, WebApplicationBuilder builder)
    {
        var minioOptions = builder.Configuration.GetSection(JwtOptions.JWT_SECTION_NAME).Get<JwtOptions>()
             ?? throw new ApplicationException("JwtOptions not found in appsettings");

        builder.Services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = minioOptions.Issuer,
                    ValidAudience = minioOptions.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(minioOptions.Key)),

                    ValidateIssuer = minioOptions.ValidateIssuer,
                    ValidateAudience = minioOptions.ValidateAudience,
                    ValidateIssuerSigningKey = minioOptions.ValidateIssuerSigningKey,
                    ValidateLifetime = minioOptions.ValidateLifetime,
                };
            });

        return services;
    }

    private static IServiceCollection AddJwtOptions(this IServiceCollection services, WebApplicationBuilder builder)
    {
        services.Configure<JwtOptions>(builder.Configuration.GetSection(JwtOptions.JWT_SECTION_NAME));

        return services;
    }
}
