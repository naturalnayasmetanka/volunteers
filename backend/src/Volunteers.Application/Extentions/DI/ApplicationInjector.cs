﻿using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Volunteers.Application.Volunteer.CreateVolunteer;
using Volunteers.Application.Volunteers.AddPet;
using Volunteers.Application.Volunteers.AddPetPhoto;
using Volunteers.Application.Volunteers.Delete;
using Volunteers.Application.Volunteers.DeletePetPhoto;
using Volunteers.Application.Volunteers.GetPresignedLinkPhoto;
using Volunteers.Application.Volunteers.MovePet;
using Volunteers.Application.Volunteers.Restore;
using Volunteers.Application.Volunteers.UpdateMainInfo;
using Volunteers.Application.Volunteers.UpdateRequisites;
using Volunteers.Application.Volunteers.UpdateSotialNetworks;

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