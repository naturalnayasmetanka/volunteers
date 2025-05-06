using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using Shared.Core.Abstractions.Handlers;
using Shared.Core.Abstractions.Providers;
using Shared.Kernel.CustomErrors;
using Volunteers.Application.Volunteers.Commands.AddPet;
using Volunteers.Application.Volunteers.Commands.GetPresignedLinkPhoto.Commands;

namespace Volunteers.Application.Volunteers.Commands.GetPresignedLinkPhoto;

public class GetPresignedLinkPhotoHandler : ICommandHandler<string, GetPresignedLinkPhotoCommand>
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
