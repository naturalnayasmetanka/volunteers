using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Volunteers.Application.Abstractions;
using Volunteers.Application.DTO;
using Volunteers.Application.Models;
using Volunteers.Application.Volunteers.Commands.AddPet;
using Volunteers.Application.Volunteers.Commands.AddPet.Commands;
using Volunteers.Application.Volunteers.Commands.AddPetPhoto;
using Volunteers.Application.Volunteers.Commands.AddPetPhoto.Commands;
using Volunteers.Application.Volunteers.Commands.Create;
using Volunteers.Application.Volunteers.Commands.Create.Commands;
using Volunteers.Application.Volunteers.Commands.Delete;
using Volunteers.Application.Volunteers.Commands.Delete.Commands;
using Volunteers.Application.Volunteers.Commands.DeletePetPhoto;
using Volunteers.Application.Volunteers.Commands.DeletePetPhoto.Commands;
using Volunteers.Application.Volunteers.Commands.GetPresignedLinkPhoto;
using Volunteers.Application.Volunteers.Commands.GetPresignedLinkPhoto.Commands;
using Volunteers.Application.Volunteers.Commands.MovePet;
using Volunteers.Application.Volunteers.Commands.MovePet.Commands;
using Volunteers.Application.Volunteers.Commands.Restore;
using Volunteers.Application.Volunteers.Commands.Restore.Commands;
using Volunteers.Application.Volunteers.Commands.UpdateMainInfo;
using Volunteers.Application.Volunteers.Commands.UpdateMainInfo.Commands;
using Volunteers.Application.Volunteers.Commands.UpdateRequisites;
using Volunteers.Application.Volunteers.Commands.UpdateRequisites.Commands;
using Volunteers.Application.Volunteers.Commands.UpdateSotialNetworks;
using Volunteers.Application.Volunteers.Commands.UpdateSotialNetworks.Commands;
using Volunteers.Application.Volunteers.Queries.GetVolunteer;
using Volunteers.Application.Volunteers.Queries.GetVolunteer.Queries;
using Volunteers.Application.Volunteers.Queries.GetVolunteers;
using Volunteers.Application.Volunteers.Queries.GetVolunteers.Queries;

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
        services.AddScoped<ICommandHandler<Guid, DeleteCommand>, SoftDeleteVolunteerHandler>();
        services.AddScoped<ICommandHandler<Guid, DeleteCommand>, HardDeleteVolunteerHandler>();
        services.AddScoped<ICommandHandler<Guid, RestoreCommand>, RestoreVolunteerHandler>();

        services.AddScoped<ICommandHandler<Guid, UpdateMainInfoCommand>, UpdateMainInfoHandler>();
        services.AddScoped<ICommandHandler<Guid, UpdateSocialNetworksCommand>, UpdateSotialNetworksHandler>();
        services.AddScoped<ICommandHandler<Guid, UpdateRequisiteCommand>, UpdateRequisitesHandler>();

        services.AddScoped<ICommandHandler<Guid, AddPetCommand>, AddPetVolunteerHandler>();
        services.AddScoped<ICommandHandler<Guid, AddPetPhotoCommand>, AddPetPhotoHandler>();
        services.AddScoped<ICommandHandler<string, GetPresignedLinkPhotoCommand>, GetPresignedLinkPhotoHandler>();
        services.AddScoped<ICommandHandler<string, DeletePetPhotoCommand>, DeletePetPhotoHandler>();
        services.AddScoped<ICommandHandler<Guid, MovePetCommand>, MovePetHandler>();

        return services;
    }

    public static IServiceCollection AddQueries(this IServiceCollection services)
    {
        services.AddScoped<IQueryHandler<PagedList<VolunteerDTO>, GetFilteredWithPaginationVolunteersQuery>, GetPaginateVolunteersHandler>();
        services.AddScoped<IQueryHandler<VolunteerDTO?, GetVolunteerQuery>, GetVolunteerHandler>();

        return services;
    }
}