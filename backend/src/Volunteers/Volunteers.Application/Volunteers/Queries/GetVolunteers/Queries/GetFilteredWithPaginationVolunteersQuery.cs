﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Volunteers.Application.Volunteers.Queries.GetVolunteers.Queries;

public record GetFilteredWithPaginationVolunteersQuery(
    string? Name,
    string? Email,
    double? ExperienceInYears,
    int Page,
    int PageSize,
    string? SortBy,
    string? SortDirection) : IQuery;