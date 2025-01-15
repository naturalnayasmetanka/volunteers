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
        var result = await _minIoProvider.GetPresignedAsync(command.FileData, cancellationToken);

        _logger.LogInformation("Presigned Link was getted: {0}", result.Value);

        return result;
    }
}
