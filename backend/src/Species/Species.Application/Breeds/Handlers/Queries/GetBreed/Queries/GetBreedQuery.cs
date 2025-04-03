using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Species.Application.Breeds.Handlers.Queries.GetBreed.Queries;

public record GetBreedQuery(int Page, int PageSize, Guid SpeciesId) : IQuery;