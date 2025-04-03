using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Species.Application.Species.Handlers.Queries.CheckExists.Queries;

public record CheckExistsQuery(
    Guid SpeciesId,
    Guid BreedId) : IQuery;