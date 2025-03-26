using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using Volunteers.Application.Abstractions;
using Volunteers.Application.Handlers.Volunteers.Commands.Delete;
using Volunteers.Application.Handlers.Volunteers.Commands.DeletePet.Commands;
using Volunteers.Domain.Shared.CustomErrors;

namespace Volunteers.Application.Handlers.Volunteers.Commands.DeletePet;

public class HardDeletePetHandler : ICommandHandler<Guid, HardDeletePetCommand>
{
    private List<Error> _errors = [];
    private readonly IVolunteerRepository _repository;
    private readonly ILogger<HardDeleteVolunteerHandler> _logger;

    public HardDeletePetHandler(
        IVolunteerRepository repository, 
        ILogger<HardDeleteVolunteerHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public Task<Result<Guid, Error>> Handle(
        HardDeletePetCommand command, 
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
