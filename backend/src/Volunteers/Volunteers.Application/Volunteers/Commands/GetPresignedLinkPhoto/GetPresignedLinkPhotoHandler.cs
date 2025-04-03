using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volunteers.Application.Volunteers.Commands.AddPet;

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
