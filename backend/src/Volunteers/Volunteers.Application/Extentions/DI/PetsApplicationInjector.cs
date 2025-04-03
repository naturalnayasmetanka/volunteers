using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Volunteers.Application.Extentions.DI;

public static class PetsApplicationInjector
{
    public static IServiceCollection AddPetsApplication(this IServiceCollection services)
    {
        services.AddCommands();
        services.AddQueries();

        services.AddValidatorsFromAssembly(typeof(PetsApplicationInjector).Assembly);

        return services;
    }

    public static IServiceCollection AddCommands(this IServiceCollection services)
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

    public static IServiceCollection AddQueries(this IServiceCollection services)
    {
        #region Pet
        services.AddScoped<IQueryHandler<PetDTO?, GetPetQuery>, GetPetHandler>();
        services.AddScoped<IQueryHandler<PagedList<PetDTO>, GetFilteredWithPaginationPetsQuery>, GetPaginatePetsHandler>();
        #endregion

        return services;
    }
}
