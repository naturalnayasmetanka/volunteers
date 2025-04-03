using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Volunteers.Application.Volunteers.Queries.GetVolunteer.Queries;

public record GetVolunteerQuery(Guid Id) : IQuery;
