using CSharpFunctionalExtensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Shared.Core.Abstractions.Database;
using Shared.Core.Abstractions.Handlers;
using Shared.Core.Abstractions.MessageQueues;
using Shared.Core.Abstractions.Providers;
using Shared.Core.DTO;
using Shared.Core.Enums;
using Shared.Kernel.CustomErrors;
using Shared.Kernel.Ids;
using Volunteers.Application.Volunteers.Commands.AddPet;
using Volunteers.Application.Volunteers.Commands.AddPetPhoto.Commands;
using Volunteers.Domain.Pets.ValueObjects;

namespace Volunteers.Application.Volunteers.Commands.AddPetPhoto;

public class AddPetPhotoHandler : ICommandHandler<Guid, AddPetPhotoCommand>
{
    private readonly IMinIoProvider _minIoProvider;
    private readonly ILogger<AddPetVolunteerHandler> _logger;
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMessageQueue<List<FileDTO>> _messageQueue;

    public AddPetPhotoHandler(
        ILogger<AddPetVolunteerHandler> logger,
        IVolunteerRepository volunteerRepository,
        IMinIoProvider minIoProvider,
        [FromKeyedServices(UoWServiceDI.VolunteerService)]IUnitOfWork unitOfWork,
        IMessageQueue<List<FileDTO>> messageQueue)
    {
        _logger = logger;
        _volunteerRepository = volunteerRepository;
        _minIoProvider = minIoProvider;
        _unitOfWork = unitOfWork;
        _messageQueue = messageQueue;
    }

    public async Task<Result<Guid, Error>> Handle(
    AddPetPhotoCommand command,
    CancellationToken cancellationToken = default)
    {
        var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);

        try
        {
            var volunteerId = VolunteerId.Create(command.VolunteerId);
            var petId = PetId.Create(command.PetId);

            var volunteer = await _volunteerRepository.GetByIdAsync(volunteerId);

            if (volunteer is null)
            {
                _logger.LogError("Volunteer {0} was not found into {1}", command.VolunteerId, nameof(AddPetPhotoHandler));

                return Errors.General.NotFound(command.VolunteerId);
            }

            var pet = volunteer.Pets.Where(x => x.Id.Value == petId.Value).FirstOrDefault();

            if (pet is null)
            {
                _logger.LogError("Pet {0} was not found in the volunteer {1} into {2}",
                    command.PetId,
                    command.VolunteerId,
                    nameof(AddPetPhotoHandler));

                return Errors.General.NotFound(command.PetId);
            }

            command.Photo.ForEach(photo =>
            {
                pet.AddPhoto(PetPhoto.Create(photo.FileName).Value);

                _logger.LogInformation("Pet {0} was added to volunteer {1} into {2}", petId, command.VolunteerId, nameof(AddPetPhotoHandler));
            });

            _volunteerRepository.Attach(volunteer);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var urlResult = await _minIoProvider.UploadAsync(command.Photo, cancellationToken);

            if (urlResult.IsFailure)
            {
                await _messageQueue.WriteAsync(urlResult.Value, cancellationToken);

                throw new Exception(urlResult.Error.ToString());
            }

            transaction.Commit();

            return (Guid)volunteer.Id;
        }
        catch (Exception ex)
        {
            transaction.Rollback();

            _logger.LogError(ex.Message);

            return Error.Failure(ex.Message, "add.photo");
        }
    }
}
