using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Volunteers.Application.Volunteer.CreateVolunteer;
using Volunteers.Application.Volunteers.UpdateMainInfo;

namespace Volunteers.Application.Extentions.DI;

public static class ApplicationInjector
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<CreateVolunteerHandler>();
        services.AddScoped<UpdateMainInfoHandler>();

        services.AddValidatorsFromAssembly(typeof(ApplicationInjector).Assembly);

        return services;
    }
}