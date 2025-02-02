﻿using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Volunteers.Application.Volunteer;
using Volunteers.Application.Volunteers.UpdateSotialNetworks.Commands;
using Volunteers.Application.Volunteers.UpdateSotialNetworks.DTO;
using Volunteers.Domain.PetManagment.Volunteer.ValueObjects;
using Volunteers.Domain.Shared.CustomErrors;
using Volunteers.Domain.Shared.Ids;

namespace Volunteers.Application.Volunteers.UpdateSotialNetworks;

public class UpdateSotialNetworksHandler
{
    private List<Error> _errors = [];
    private readonly IVolunteerRepository _repository;
    private readonly ILogger<UpdateSotialNetworksHandler> _logger;
    private readonly IValidator<UpdateSocialListDto> _validator;

    public UpdateSotialNetworksHandler(
        IVolunteerRepository repository,
        ILogger<UpdateSotialNetworksHandler> logger,
        IValidator<UpdateSocialListDto> validator
        )
    {
        _repository = repository;
        _logger = logger;
        _validator = validator;
    }

    public async Task<Result<Guid, List<Error>>> Handle(
        UpdateSocialNetworksCommand command,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command.SocialListDto, cancellationToken);

        if (!validationResult.IsValid)
        {
            validationResult.Errors.ForEach(error => _errors.Add(Error.Validation(error.ErrorMessage, error.ErrorCode)));
            _logger.LogError("Validation is failed into {0}", nameof(UpdateSotialNetworksHandler));

            return _errors;
        }

        var id = VolunteerId.Create(command.Id);
        var volunteer = await _repository.GetByIdAsync(id, cancellationToken);

        if (volunteer is null)
        {
            _errors.Add(Errors.General.NotFound(command.Id));
            _logger.LogError("Volunteer {0} was not found into {1}", id.Value, nameof(UpdateSotialNetworksHandler));

            return _errors;
        }

        List<SocialNetwork> newSocials = new List<SocialNetwork>();
        command.SocialListDto.ListSocial.ForEach(x => newSocials.Add(SocialNetwork.Create(x.Title, x.Link).Value));

        volunteer.UpdateSocial(newSocials);
        await _repository.UpdateAsync(volunteer, cancellationToken);

        _logger.LogInformation("Volunteer`s {0} social networks info was updated into {1}", command.Id, nameof(UpdateSotialNetworksHandler));

        return (Guid)command.Id;
    }
}