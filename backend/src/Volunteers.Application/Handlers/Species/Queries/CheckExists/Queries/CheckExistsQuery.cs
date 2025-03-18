using Volunteers.Application.Abstractions;

namespace Volunteers.Application.Handlers.Species.Queries.CheckExists.Queries;

public record CheckExistsQuery(
    Guid SpeciesId,
    Guid BreedId) : IQuery;
