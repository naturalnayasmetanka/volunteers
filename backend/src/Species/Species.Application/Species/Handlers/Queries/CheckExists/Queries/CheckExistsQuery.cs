using Shared.Core.Abstractions.Handlers;

namespace Species.Application.Species.Handlers.Queries.CheckExists.Queries;

public record CheckExistsQuery(
    Guid SpeciesId,
    Guid BreedId) : IQuery;
