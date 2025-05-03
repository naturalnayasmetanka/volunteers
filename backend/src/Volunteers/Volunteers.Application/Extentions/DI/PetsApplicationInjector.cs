using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Shared.Core.Abstractions.Handlers;
using Shared.Core.DTO;
using Shared.Core.Models;
using Volunteers.Application.Pets.Queries.GetPet;
using Volunteers.Application.Pets.Queries.GetPet.Queries;
using Volunteers.Application.Pets.Queries.GetPets;
using Volunteers.Application.Pets.Queries.GetPets.Queries;
using Volunteers.Application.Volunteers.Commands.AddPet;
using Volunteers.Application.Volunteers.Commands.AddPet.Commands;
using Volunteers.Application.Volunteers.Commands.AddPetPhoto;
using Volunteers.Application.Volunteers.Commands.AddPetPhoto.Commands;
using Volunteers.Application.Volunteers.Commands.DeletePet;
using Volunteers.Application.Volunteers.Commands.DeletePet.Commands;
using Volunteers.Application.Volunteers.Commands.DeletePetPhoto;
using Volunteers.Application.Volunteers.Commands.DeletePetPhoto.Commands;
using Volunteers.Application.Volunteers.Commands.GetPresignedLinkPhoto;
using Volunteers.Application.Volunteers.Commands.GetPresignedLinkPhoto.Commands;
using Volunteers.Application.Volunteers.Commands.MovePet;
using Volunteers.Application.Volunteers.Commands.MovePet.Commands;
using Volunteers.Application.Volunteers.Commands.SetMainPetPhoto;
using Volunteers.Application.Volunteers.Commands.SetMainPetPhoto.Commands;
using Volunteers.Application.Volunteers.Commands.UpdatePet;
using Volunteers.Application.Volunteers.Commands.UpdatePet.Commands;
using Volunteers.Application.Volunteers.Commands.UpdatePetStatus;
using Volunteers.Application.Volunteers.Commands.UpdatePetStatus.Commands;

namespace Volunteers.Application.Extentions.DI;

public static class PetsApplicationInjector
{
    public static IServiceCollection AddPetsApplication(this IServiceCollection services)
    {
        services.AddPetsCommands();
        services.AddPetsQueries();

        services.AddValidatorsFromAssembly(typeof(PetsApplicationInjector).Assembly);

        return services;
    }

    private static IServiceCollection AddPetsCommands(this IServiceCollection services)
    {
        #region Pet
        services.AddScoped<ICommandHandler<Guid, AddPetCommand>, AddPetVolunteerHandler>();
        services.AddScoped<ICommandHandler<Guid, AddPetPhotoCommand>, AddPetPhotoHandler>();
        services.AddScoped<ICommandHandler<string, GetPresignedLinkPhotoCommand>, GetPresignedLinkPhotoHandler>();
        services.AddScoped<ICommandHandler<string, DeletePetPhotoCommand>, DeletePetPhotoHandler>();
        services.AddScoped<ICommandHandler<Guid, MovePetCommand>, MovePetHandler>();

        services.AddScoped<ICommandHandler<Guid, UpdatePetCommand>, UpdatePetHandler>();
        services.AddScoped<ICommandHandler<Guid, UpdatePetStatusCommand>, UpdatePetStatusHandler>();

        services.AddScoped<ICommandHandler<Guid, SoftDeletePetCommand>, SoftDeletePetHandler>();
        services.AddScoped<ICommandHandler<Guid, HardDeletePetCommand>, HardDeletePetHandler>();

        services.AddScoped<ICommandHandler<Guid, SetMainPetPhotoCommand>, SetMainPetPhotoHandler>();
        #endregion

        return services;
    }

    private static IServiceCollection AddPetsQueries(this IServiceCollection services)
    {
        #region Pet
        services.AddScoped<IQueryHandler<PetDTO?, GetPetQuery>, GetPetHandler>();
        services.AddScoped<IQueryHandler<PagedList<PetDTO>, GetFilteredWithPaginationPetsQuery>, GetPaginatePetsHandler>();
        #endregion

        return services;
    }
}
