using Volunteers.Application.Abstractions;

namespace Volunteers.Application.Handlers.Pets.Queries.GetPet.Queries;

public record GetPetQuery(Guid Id) : IQuery;
