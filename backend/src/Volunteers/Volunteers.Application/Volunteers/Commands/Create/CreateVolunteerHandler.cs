using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Shared.Core.Abstractions.Handlers;
using Shared.Kernel.CustomErrors;
using Shared.Kernel.Ids;
using Volunteers.Application.Volunteers.Commands.Create.Commands;
using Volunteers.Application.Volunteers.Commands.Create.DTO;
using Volunteers.Domain.Volunteers.ValueObjects;
using VolunteerModel = Volunteers.Domain.Volunteers.AggregateRoot.Volunteer;

namespace Volunteers.Application.Volunteers.Commands.Create;

public class CreateVolunteerHandler : ICommandHandler<Guid, CreateVolunteerCommand>
{
    private List<Error> _errors = [];
    private readonly IVolunteerRepository _repository;
    private readonly ILogger<CreateVolunteerHandler> _logger;
    private readonly IValidator<CreateVolunteerDto> _validator;

    public CreateVolunteerHandler(
        IVolunteerRepository repository,
        ILogger<CreateVolunteerHandler> logger,
        IValidator<CreateVolunteerDto> validator)
    {
        _repository = repository;
        _logger = logger;
        _validator = validator;
    }

    public async Task<Result<Guid, Error>> Handle(
        CreateVolunteerCommand command,
        CancellationToken cancellationToken = default)
    {
        var volunteerId = VolunteerId.NewVolunteerId();

        var volunteerResult = VolunteerModel.Create(
            volunteerId,
            Name.Create(command.VolunteerDto.Name).Value,
            Email.Create(command.VolunteerDto.Email).Value,
            ExperienceInYears.Create(command.VolunteerDto.ExperienceInYears).Value,
            PhoneNumber.Create(command.VolunteerDto.PhoneNumber).Value).Value;

        var socialNetworks = command.VolunteerDto.SocialNetworks;

        if (socialNetworks is not null)
            socialNetworks
                .ForEach(x =>
                    volunteerResult.AddSocialNetwork(
                        SocialNetwork.Create(
                            x.Title,
                            x.Link
                        ).Value));

        var requisites = command.VolunteerDto.VolunteerRequisites;

        if (requisites is not null)
            requisites
                .ForEach(x =>
                    volunteerResult.AddVolunteerRequisite(
                        VolunteerRequisite.Create(
                            x.Title,
                            x.Description
                        ).Value));

        var createResult = await _repository
            .CreateAsync(volunteerResult, cancellationToken);

        _logger.LogInformation("Volunteer {0} was created into {1}", volunteerId.Value, nameof(CreateVolunteerHandler));

        return (Guid)createResult.Id;
    }
}
