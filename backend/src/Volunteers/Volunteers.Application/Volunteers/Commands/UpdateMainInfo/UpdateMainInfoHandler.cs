﻿using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Shared.Core.Abstractions.Handlers;
using Shared.Kernel.CustomErrors;
using Shared.Kernel.Ids;
using Volunteers.Application.Volunteers.Commands.UpdateMainInfo.Commands;
using Volunteers.Application.Volunteers.Commands.UpdateMainInfo.DTO;
using Volunteers.Domain.Volunteers.ValueObjects;

namespace Volunteers.Application.Volunteers.Commands.UpdateMainInfo;

public class UpdateMainInfoHandler : ICommandHandler<Guid, UpdateMainInfoCommand>
{
    private List<Error> _errors = [];
    private readonly IVolunteerRepository _repository;
    private readonly ILogger<UpdateMainInfoHandler> _logger;
    private readonly IValidator<UpdateMainInfoDto> _validator;

    public UpdateMainInfoHandler(
        IVolunteerRepository repository,
        ILogger<UpdateMainInfoHandler> logger,
        IValidator<UpdateMainInfoDto> validator)
    {
        _repository = repository;
        _logger = logger;
        _validator = validator;
    }

    public async Task<Result<Guid, Error>> Handle(
        UpdateMainInfoCommand command,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command.MainInfoDto, cancellationToken);

        if (!validationResult.IsValid)
        {
            validationResult.Errors.ForEach(error => _errors.Add(Error.Validation(error.ErrorMessage, error.ErrorCode)));
            _logger.LogError("Validation is failed into {0}", nameof(UpdateMainInfoHandler));

            return _errors[0];
        }

        var id = VolunteerId.Create(command.VolunteerId);
        var volunteer = await _repository.GetByIdAsync(id, cancellationToken);

        if (volunteer is null)
        {
            _logger.LogError("Volunteer {0} was not found into {1}", id.Value, nameof(UpdateMainInfoHandler));

            return Errors.General.NotFound(command.VolunteerId);
        }

        volunteer.UpdateMainInfo(
            name: Name.Create(command.MainInfoDto.Name).Value,
            email: Email.Create(command.MainInfoDto.Email).Value,
            experienceInYears: ExperienceInYears.Create(command.MainInfoDto.ExperienceInYears).Value,
            phoneNumber: PhoneNumber.Create(command.MainInfoDto.PhoneNumber).Value);

        await _repository.UpdateAsync(volunteer, cancellationToken);

        _logger.LogInformation("Volunteer`s {0} main info was updated into {1}", id, nameof(UpdateMainInfoHandler));

        return command.VolunteerId;
    }
}
