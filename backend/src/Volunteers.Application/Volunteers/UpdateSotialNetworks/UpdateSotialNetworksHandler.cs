using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using Volunteers.Application.Volunteer;
using Volunteers.Application.Volunteers.UpdateSotialNetworks.Commands;
using Volunteers.Domain.PetManagment.Volunteer.ValueObjects;
using Volunteers.Domain.Shared.CustomErrors;
using Volunteers.Domain.Shared.Ids;

namespace Volunteers.Application.Volunteers.UpdateSotialNetworks;

public class UpdateSotialNetworksHandler
{
    private List<Error> _errors = [];
    private readonly IVolunteerRepository _repository;
    private readonly ILogger<UpdateSotialNetworksHandler> _logger;

    public UpdateSotialNetworksHandler(
        IVolunteerRepository repository,
        ILogger<UpdateSotialNetworksHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<Guid, List<Error>>> Handle(
        UpdateSocialNetworksCommand command,
        CancellationToken cancellationToken = default)
    {
        var id = VolunteerId.Create(command.Id);
        var volunteer = await _repository.GetByIdAsync(id, cancellationToken);

        if (volunteer is null)
        {
            _errors.Add(Errors.General.NotFound(command.Id));

            return _errors;
        }

        List<SocialNetwork> newSocials = new List<SocialNetwork>();
        command.SocialListDto.ListSocial.ForEach(x => newSocials.Add(SocialNetwork.Create(x.Title, x.Link).Value));

        volunteer.UpdateSocial(newSocials);

        await _repository.UpdateAsync(volunteer, cancellationToken);

        _logger.LogInformation("id: {0} Volunteer social networks info was updated", command.Id);

        return (Guid)command.Id;
    }
}