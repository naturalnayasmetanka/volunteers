using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Volunteers.Application.Abstractions;
using Volunteers.Application.DTO;
using Volunteers.Application.Handlers.Breeds.Queries.GetBreed;
using Volunteers.Application.Handlers.Breeds.Queries.GetBreed.Queries;
using Volunteers.Application.Handlers.Species.Queries.CheckExists;
using Volunteers.Application.Handlers.Species.Queries.CheckExists.Queries;
using Volunteers.Application.Handlers.Species.Queries.GetSpecies;
using Volunteers.Application.Handlers.Species.Queries.GetSpecies.Queries;
using Volunteers.Application.Handlers.Volunteers.Commands.AddPet;
using Volunteers.Application.Handlers.Volunteers.Commands.AddPet.Commands;
using Volunteers.Application.Handlers.Volunteers.Commands.AddPetPhoto;
using Volunteers.Application.Handlers.Volunteers.Commands.AddPetPhoto.Commands;
using Volunteers.Application.Handlers.Volunteers.Commands.Create;
using Volunteers.Application.Handlers.Volunteers.Commands.Create.Commands;
using Volunteers.Application.Handlers.Volunteers.Commands.Delete;
using Volunteers.Application.Handlers.Volunteers.Commands.Delete.Commands;
using Volunteers.Application.Handlers.Volunteers.Commands.DeletePet;
using Volunteers.Application.Handlers.Volunteers.Commands.DeletePet.Commands;
using Volunteers.Application.Handlers.Volunteers.Commands.DeletePetPhoto;
using Volunteers.Application.Handlers.Volunteers.Commands.DeletePetPhoto.Commands;
using Volunteers.Application.Handlers.Volunteers.Commands.GetPresignedLinkPhoto;
using Volunteers.Application.Handlers.Volunteers.Commands.GetPresignedLinkPhoto.Commands;
using Volunteers.Application.Handlers.Volunteers.Commands.MovePet;
using Volunteers.Application.Handlers.Volunteers.Commands.MovePet.Commands;
using Volunteers.Application.Handlers.Volunteers.Commands.Restore;
using Volunteers.Application.Handlers.Volunteers.Commands.Restore.Commands;
using Volunteers.Application.Handlers.Volunteers.Commands.UpdateMainInfo;
using Volunteers.Application.Handlers.Volunteers.Commands.UpdateMainInfo.Commands;
using Volunteers.Application.Handlers.Volunteers.Commands.UpdatePet;
using Volunteers.Application.Handlers.Volunteers.Commands.UpdatePet.Commands;
using Volunteers.Application.Handlers.Volunteers.Commands.UpdatePetStatus;
using Volunteers.Application.Handlers.Volunteers.Commands.UpdatePetStatus.Commands;
using Volunteers.Application.Handlers.Volunteers.Commands.UpdateRequisites;
using Volunteers.Application.Handlers.Volunteers.Commands.UpdateRequisites.Commands;
using Volunteers.Application.Handlers.Volunteers.Commands.UpdateSotialNetworks;
using Volunteers.Application.Handlers.Volunteers.Commands.UpdateSotialNetworks.Commands;
using Volunteers.Application.Handlers.Volunteers.Queries.GetVolunteer;
using Volunteers.Application.Handlers.Volunteers.Queries.GetVolunteer.Queries;
using Volunteers.Application.Handlers.Volunteers.Queries.GetVolunteers;
using Volunteers.Application.Handlers.Volunteers.Queries.GetVolunteers.Queries;
using Volunteers.Application.Models;

namespace Volunteers.Application.Extentions.DI;

public static class ApplicationInjector
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddCommands();
        services.AddQueries();

        services.AddValidatorsFromAssembly(typeof(ApplicationInjector).Assembly);

        return services;
    }

    public static IServiceCollection AddCommands(this IServiceCollection services)
    {
        services.AddScoped<ICommandHandler<Guid, CreateVolunteerCommand>, CreateVolunteerHandler>();
        services.AddScoped<ICommandHandler<Guid, SoftDeleteVolunteerCommand>, SoftDeleteVolunteerHandler>();
        services.AddScoped<ICommandHandler<Guid, HardDeleteVolunteerCommand>, HardDeleteVolunteerHandler>();
        services.AddScoped<ICommandHandler<Guid, RestoreCommand>, RestoreVolunteerHandler>();

        services.AddScoped<ICommandHandler<Guid, UpdateMainInfoCommand>, UpdateMainInfoHandler>();
        services.AddScoped<ICommandHandler<Guid, UpdateSocialNetworksCommand>, UpdateSotialNetworksHandler>();
        services.AddScoped<ICommandHandler<Guid, UpdateRequisiteCommand>, UpdateRequisitesHandler>();

        services.AddScoped<ICommandHandler<Guid, AddPetCommand>, AddPetVolunteerHandler>();
        services.AddScoped<ICommandHandler<Guid, AddPetPhotoCommand>, AddPetPhotoHandler>();
        services.AddScoped<ICommandHandler<string, GetPresignedLinkPhotoCommand>, GetPresignedLinkPhotoHandler>();
        services.AddScoped<ICommandHandler<string, DeletePetPhotoCommand>, DeletePetPhotoHandler>();
        services.AddScoped<ICommandHandler<Guid, MovePetCommand>, MovePetHandler>();

        services.AddScoped<ICommandHandler<Guid, UpdatePetCommand>, UpdatePetHandler>();
        services.AddScoped<ICommandHandler<Guid, UpdatePetStatusCommand>, UpdatePetStatusHandler>();

        services.AddScoped<ICommandHandler<Guid, SoftDeletePetCommand>, SoftDeletePetHandler>();
        services.AddScoped<ICommandHandler<Guid, HardDeletePetCommand>, HardDeletePetHandler>();

        return services;
    }

    public static IServiceCollection AddQueries(this IServiceCollection services)
    {
        services.AddScoped<IQueryHandler<PagedList<VolunteerDTO>, GetFilteredWithPaginationVolunteersQuery>, GetPaginateVolunteersHandler>();
        services.AddScoped<IQueryHandler<VolunteerDTO?, GetVolunteerQuery>, GetVolunteerHandler>();

        services.AddScoped<IQueryHandler<PagedList<SpeciesDTO>, GetSpeciesWithPaginationQuery>, GetSpeciesHandler>();
        services.AddScoped<IQueryHandler<PagedList<BreedDTO>, GetBreedQuery>, GetBreedHandler>();

        services.AddScoped<IQueryHandler<bool, CheckExistsQuery>, CheckExistsHandler>();

        return services;
    }
}