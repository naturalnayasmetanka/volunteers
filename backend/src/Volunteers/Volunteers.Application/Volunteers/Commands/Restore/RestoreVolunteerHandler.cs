﻿using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using Shared.Core.Abstractions.Handlers;
using Shared.Kernel.CustomErrors;
using Shared.Kernel.Ids;
using Volunteers.Application.Volunteers.Commands.Delete;
using Volunteers.Application.Volunteers.Commands.Restore.Commands;

namespace Volunteers.Application.Volunteers.Commands.Restore;

public class RestoreVolunteerHandler : ICommandHandler<Guid, RestoreCommand>
{
    private List<Error> _errors = [];
    private readonly IVolunteerRepository _repository;
    private readonly ILogger<HardDeleteVolunteerHandler> _logger;

    public RestoreVolunteerHandler(
        IVolunteerRepository repository,
        ILogger<HardDeleteVolunteerHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<Guid, Error>> Handle(
        RestoreCommand command,
        CancellationToken cancellationToken = default)
    {
        var id = VolunteerId.Create(command.Id);
        var volunteer = await _repository.GetByIdAsync(id, cancellationToken);

        if (volunteer is null)
        {
            _logger.LogError("Volunteer {0} was not found into {1}", id.Value, nameof(RestoreVolunteerHandler));

            return Errors.General.NotFound(command.Id);
        }

        volunteer.Restore();
        await _repository.SaveAsync();

        _logger.LogInformation("Volunteer {0} was restored into {1}", command.Id, nameof(RestoreVolunteerHandler));

        return command.Id;
    }
}
