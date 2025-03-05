using Volunteers.Application.Abstractions;

namespace Volunteers.Application.Pets.Queries.GetPet.Queries;

public record GetPetQuery(Guid id): IQuery;
