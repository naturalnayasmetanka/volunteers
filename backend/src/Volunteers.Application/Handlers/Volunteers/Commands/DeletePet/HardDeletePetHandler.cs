using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using Volunteers.Application.Abstractions;
using Volunteers.Application.Database;
using Volunteers.Application.DTO;
using Volunteers.Application.Handlers.Volunteers.Commands.Delete;
using Volunteers.Application.Handlers.Volunteers.Commands.DeletePet.Commands;
using Volunteers.Application.Providers;
using Volunteers.Domain.Shared.CustomErrors;
using Volunteers.Domain.Shared.Ids;

namespace Volunteers.Application.Handlers.Volunteers.Commands.DeletePet;

public class HardDeletePetHandler : ICommandHandler<Guid, HardDeletePetCommand>
{
    private List<Error> _errors = [];
    private readonly IMinIoProvider _minIoProvider;
    private readonly IVolunteerRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<HardDeleteVolunteerHandler> _logger;

    public HardDeletePetHandler(
        IVolunteerRepository repository,
        IMinIoProvider minIoProvider,
        IUnitOfWork unitOfWork,
        ILogger<HardDeleteVolunteerHandler> logger)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _minIoProvider = minIoProvider;
        _logger = logger;
    }

    public async Task<Result<Guid, Error>> Handle(
        HardDeletePetCommand command,
        CancellationToken cancellationToken = default)
    {
        using (var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken))
        {
            try
            {
                var volunteer = await _repository.GetByIdAsync(VolunteerId.Create(command.VolunteerId));

                if (volunteer is null)
                {
                    _logger.LogError("Volunteer {0} was not found into {1}", command.VolunteerId, nameof(HardDeleteVolunteerHandler));

                    return Errors.General.NotFound(command.VolunteerId);
                }

                var pet = volunteer.Pets.FirstOrDefault(x => x.Id == command.PetId);

                if (pet is null)
                {
                    _logger.LogError("Pet {0} was not found into {1}", command.PetId, nameof(HardDeleteVolunteerHandler));

                    return Errors.General.NotFound(command.PetId);
                }

                if (pet.PhotoDetails is not null)
                {
                    foreach (var photo in pet.PhotoDetails.PetPhoto)
                    {
                        var deleteResult = await _minIoProvider.DeleteAsync(new FileDTO(Stream: null, FileName: photo.Path, BucketName: command.BucketName, ContentType: null), cancellationToken);

                        if (deleteResult.IsFailure)
                        {
                            throw new Exception(deleteResult.Error.ToString());
                        }
                    }
                }

                var petHardDeleteResult = volunteer.HardDeletePet(pet);

                if (petHardDeleteResult.IsFailure)
                {
                    _logger.LogError("Pet {0} was not deleted(hard) in the volunteer {1} into {2}", command.PetId, command.VolunteerId, nameof(HardDeleteVolunteerHandler));

                    return petHardDeleteResult.Error;
                }

                _repository.Attach(volunteer);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                _logger.LogInformation("Pet {0} was deleted in the volunteer {1} into {2}", command.PetId, command.VolunteerId, nameof(HardDeleteVolunteerHandler));

                return (Guid)volunteer.Id;
            }
            catch
            {
                transaction.Rollback();

                return Error.Failure("Error hard delete", "delete.pet.hard");
            }
        }
    }
}
