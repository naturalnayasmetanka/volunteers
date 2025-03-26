using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using Volunteers.Application.Abstractions;
using Volunteers.Application.Handlers.Volunteers.Commands.Delete;
using Volunteers.Application.Handlers.Volunteers.Commands.DeletePet.Commands;
using Volunteers.Domain.Shared.CustomErrors;

namespace Volunteers.Application.Handlers.Volunteers.Commands.DeletePet;

public class SoftDeletePetHandler : ICommandHandler<Guid, SoftDeletePetCommand>
{
    private List<Error> _errors = [];
    private readonly IVolunteerRepository _repository;
    private readonly ILogger<SoftDeleteVolunteerHandler> _logger;

    public SoftDeletePetHandler(
        IVolunteerRepository repository, 
        ILogger<SoftDeleteVolunteerHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public Task<Result<Guid, Error>> Handle(
        SoftDeletePetCommand command, 
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
