using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using Volunteers.Application.Abstractions;
using Volunteers.Application.Database;
using Volunteers.Application.DTO;
using Volunteers.Application.Handlers.Volunteers.Commands.AddPet;
using Volunteers.Application.Handlers.Volunteers.Commands.DeletePetPhoto.Commands;
using Volunteers.Application.MessageQueues;
using Volunteers.Application.Providers;
using Volunteers.Domain.Shared.CustomErrors;
using Volunteers.Domain.Shared.Ids;

namespace Volunteers.Application.Handlers.Volunteers.Commands.DeletePetPhoto;

public class DeletePetPhotoHandler : ICommandHandler<string, DeletePetPhotoCommand>
{
    private readonly IMinIoProvider _minIoProvider;
    private readonly ILogger<AddPetVolunteerHandler> _logger;
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMessageQueue<List<FileDTO>> _messageQueue;

    public DeletePetPhotoHandler(
        IMinIoProvider minIoProvider,
        ILogger<AddPetVolunteerHandler> logger,
        IVolunteerRepository volunteerRepository,
        IUnitOfWork unitOfWork,
        IMessageQueue<List<FileDTO>> messageQueue)
    {
        _minIoProvider = minIoProvider;
        _logger = logger;
        _volunteerRepository = volunteerRepository;
        _unitOfWork = unitOfWork;
        _messageQueue = messageQueue;
    }

    public async Task<Result<string, Error>> Handle(
        DeletePetPhotoCommand command,
        CancellationToken cancellationToken = default)
    {
        var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);

        try
        {
            var volunteer = await _volunteerRepository.GetByIdAsync(VolunteerId.Create(command.VolunteerId));

            if (volunteer is null)
            {
                _logger.LogError("Volunteer {0} was not found into {1}",
                    command.VolunteerId,
                    nameof(DeletePetPhotoHandler));

                return Errors.General.NotFound(command.VolunteerId);
            }

            var pet = volunteer.Pets.FirstOrDefault(x => x.Id == PetId.Create(command.PetId));

            if (pet is null)
            {
                _logger.LogError("Pet {0} was not found in the volunteer {1} into {2}",
                    command.PetId,
                    command.VolunteerId,
                    nameof(DeletePetPhotoHandler));

                return Errors.General.NotFound(command.PetId);
            }

            var actualPhoto = pet.PhotoDetails?.PetPhoto.Where(x => x.Path != command.FileData.FileName).ToList();

            if (actualPhoto is not null)
            {
                _logger.LogInformation("Photo {0} was not found in the pet {1} of the volunteer {2} into {3}",
                    command.FileData.FileName,
                    command.PetId,
                    command.VolunteerId,
                    nameof(DeletePetPhotoHandler));

                pet.UpdatePhoto(actualPhoto);
            }

            var deleteResult = await _minIoProvider.DeleteAsync(command.FileData, cancellationToken);

            if (deleteResult.IsFailure)
            {
                await _messageQueue.WriteAsync(new List<FileDTO>() { command.FileData }, cancellationToken);

                throw new Exception(deleteResult.Error.ToString());
            }

            _volunteerRepository.Attach(volunteer);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Photo {0} was deleted into {1}",
                command.FileData.FileName,
                nameof(DeletePetPhotoHandler));

            transaction.Commit();

            return command.FileData.FileName;
        }
        catch (Exception ex)
        {
            transaction.Rollback();

            _logger.LogError(ex.Message);

            return Error.Failure(ex.Message, "delete.photo");
        }
    }
}
