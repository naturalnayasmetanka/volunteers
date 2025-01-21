using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using Volunteers.Application.Providers;
using Volunteers.Application.Volunteers.AddPet;
using Volunteers.Application.Volunteers.GetPresignedLinkPhoto.Commands;
using Volunteers.Domain.Shared.CustomErrors;

namespace Volunteers.Application.Volunteers.GetPresignedLinkPhoto;

public class GetPresignedLinkPhotoHandler
{
    private readonly IMinIoProvider _minIoProvider;
    private readonly ILogger<AddPetVolunteerHandler> _logger;

    public GetPresignedLinkPhotoHandler(
        IMinIoProvider minIoProvider,
        ILogger<AddPetVolunteerHandler> logger)
    {
        _minIoProvider = minIoProvider;
        _logger = logger;
    }

    public async Task<Result<string, Error>> Handle(
        GetPresignedLinkPhotoCommand command,
        CancellationToken cancellationToken = default)
    {
        var resultPresignedLink = await _minIoProvider.GetPresignedAsync(command.FileData, cancellationToken);

        if (resultPresignedLink.IsFailure)
        {
            _logger.LogError("Presigned Link {0} was getted with error into {1}", resultPresignedLink.Value, nameof(GetPresignedLinkPhotoHandler));

            return Error.Failure("Presigned Link was getted with error", "minio.presigned");
        }

        _logger.LogInformation("Presigned Link {0} was getted into {1}", resultPresignedLink.Value, nameof(GetPresignedLinkPhotoHandler));

        return resultPresignedLink;
    }
}
