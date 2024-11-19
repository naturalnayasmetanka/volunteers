using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Volunteers.Application.Volunteer.CreateVolunteer;

namespace Volunteers.Application.Extentions.DI;

public static class ApplicationInjector
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<CreateVolunteerHandler>();
        services.AddValidatorsFromAssembly(typeof(ApplicationInjector).Assembly);

        return services;
    }
}