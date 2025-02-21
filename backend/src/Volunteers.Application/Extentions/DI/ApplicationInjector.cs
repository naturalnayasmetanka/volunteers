using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Volunteers.Application.Volunteers.Commands.AddPet;
using Volunteers.Application.Volunteers.Commands.AddPetPhoto;
using Volunteers.Application.Volunteers.Commands.Create;
using Volunteers.Application.Volunteers.Commands.Delete;
using Volunteers.Application.Volunteers.Commands.DeletePetPhoto;
using Volunteers.Application.Volunteers.Commands.GetPresignedLinkPhoto;
using Volunteers.Application.Volunteers.Commands.MovePet;
using Volunteers.Application.Volunteers.Commands.Restore;
using Volunteers.Application.Volunteers.Commands.UpdateMainInfo;
using Volunteers.Application.Volunteers.Commands.UpdateRequisites;
using Volunteers.Application.Volunteers.Commands.UpdateSotialNetworks;

namespace Volunteers.Application.Extentions.DI;

public static class ApplicationInjector
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<CreateVolunteerHandler>();
        services.AddScoped<SoftDeleteVolunteerHandler>();
        services.AddScoped<HardDeleteVolunteerHandler>();
        services.AddScoped<RestoreVolunteerHandler>();

        services.AddScoped<UpdateMainInfoHandler>();
        services.AddScoped<UpdateSotialNetworksHandler>();
        services.AddScoped<UpdateRequisitesHandler>();

        services.AddScoped<AddPetVolunteerHandler>();
        services.AddScoped<AddPetPhotoHandler>();
        services.AddScoped<GetPresignedLinkPhotoHandler>();
        services.AddScoped<DeletePetPhotoHandler>();
        services.AddScoped<MovePetHandler>();

        services.AddValidatorsFromAssembly(typeof(ApplicationInjector).Assembly);

        return services;
    }
}