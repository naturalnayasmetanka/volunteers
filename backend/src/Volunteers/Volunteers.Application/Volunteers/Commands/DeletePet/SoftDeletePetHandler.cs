using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using Shared.Core.Abstractions.Handlers;
using Shared.Kernel.CustomErrors;
using Shared.Kernel.Ids;
using Volunteers.Application.Volunteers.Commands.Delete;
using Volunteers.Application.Volunteers.Commands.DeletePet.Commands;

namespace Volunteers.Application.Volunteers.Commands.DeletePet;

public class SoftDeletePetHandler : ICommandHandler<Guid, SoftDeletePetCommand>
{
    private List<Error> _errors = [];
    private readonly IVolunteerRepository _repository;
    private readonly ILogger<SoftDeleteVolunteerHandler> _logger;

    public SoftDeletePetHandler(
        IVolunteerRepository repository,
        ILogger<SoftDeleteVolunteerHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<Guid, Error>> Handle(
        SoftDeletePetCommand command,
        CancellationToken cancellationToken = default)
    {
        var volunteer = await _repository.GetByIdAsync(VolunteerId.Create(command.VolunteerId));

        if (volunteer is null)
        {
            _logger.LogError("Volunteer {0} was not found into {1}", command.VolunteerId, nameof(SoftDeletePetHandler));

            return Errors.General.NotFound(command.VolunteerId);
        }

        var petSoftDeleteResult = volunteer.SoftDeletePet(PetId.Create(command.PetId));

        if (petSoftDeleteResult.IsFailure)
        {
            _logger.LogError("Pet {0} was not deleted(soft) in the volunteer {1} into {2}", command.PetId, command.VolunteerId, nameof(SoftDeletePetHandler));

            return petSoftDeleteResult.Error;
        }

        await _repository.SaveAsync(cancellationToken);

        _logger.LogInformation("Pet {0} was deleted(soft) in the volunteer {1} into {2}", command.PetId, command.VolunteerId, nameof(SoftDeletePetHandler));

        return (Guid)volunteer.Id;
    }
}
