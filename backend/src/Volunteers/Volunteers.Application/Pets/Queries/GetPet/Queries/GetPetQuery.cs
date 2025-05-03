using Shared.Core.Abstractions.Handlers;

namespace Volunteers.Application.Pets.Queries.GetPet.Queries;

public record GetPetQuery(Guid Id) : IQuery;

