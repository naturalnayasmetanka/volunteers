using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using Volunteers.Application.Volunteer;
using Volunteers.Domain.Shared.CustomErrors;

namespace Volunteers.Application.Volunteers.AddPet;

public class AddPetVolunteerHandler
{
    private readonly ILogger<AddPetVolunteerHandler> _logger;
    private readonly IVolunteerRepository _volunteerRepository;
    public AddPetVolunteerHandler(
        ILogger<AddPetVolunteerHandler> logger,
        IVolunteerRepository volunteerRepository)
    {
        _logger = logger;
        _volunteerRepository = volunteerRepository;
    }

    public async Task<Result<Guid, List<Error>>> Handle(
        CancellationToken cancellationToken = default) 
    {
        _logger.LogInformation("Volunteer was restored with id: {0}", "");

        return Guid.Empty;
    }
}
