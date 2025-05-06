using System.Text;
using System.Text.Json;
using CSharpFunctionalExtensions;
using Dapper;
using Shared.Core.Abstractions.Database;
using Shared.Core.Abstractions.Handlers;
using Shared.Core.DTO;
using Shared.Core.Extentions;
using Shared.Core.Models;
using Shared.Kernel.CustomErrors;
using Volunteers.Application.Pets.Queries.GetPets.Queries;

namespace Volunteers.Application.Pets.Queries.GetPets;

public class GetPaginatePetsHandler : IQueryHandler<PagedList<PetDTO>, GetFilteredWithPaginationPetsQuery>
{
    private readonly ISqlConnConnectionFactory _connection;
    public GetPaginatePetsHandler(ISqlConnConnectionFactory connection)
    {
        _connection = connection;
    }

    public async Task<Result<PagedList<PetDTO>, Error>> Handle(
        GetFilteredWithPaginationPetsQuery query,
        CancellationToken cancellationToken = default)
    {
        var connection = _connection.Create();

        var parameters = new DynamicParameters();

        var sqlTotalCountQuery = new StringBuilder("""
                            SELECT COUNT(*) FROM pets
                        """
                      );

        sqlTotalCountQuery.ApplyFilterByColumn(columnName: "nickname", queryFieldValue: query.Name);
        sqlTotalCountQuery.ApplyFilterByColumn(columnName: "volunteer_id", queryFieldValue: query.VolunteerId.ToString());
        //sqlTotalCountQuery.ApplyFilterByColumn(columnName: "experience_in_years", queryFieldValue: query.Species);
        //sqlTotalCountQuery.ApplyFilterByColumn(columnName: "experience_in_years", queryFieldValue: query.Breed);

        if (query.PetStatus is not null)
        {
            sqlTotalCountQuery.ApplyFilterByColumn(columnName: "help_status", queryFieldValue: (int)query.PetStatus);
        }

        var totalCount = await connection.ExecuteScalarAsync<long>(sqlTotalCountQuery.ToString());

        var sqlQuery = new StringBuilder("""
                        SELECT 
                            id, nickname, common_description, helth_description, phone_number, help_status, birth_date, creation_date,
                            "position", volunteer_id, "LocationDetails", "PhotoDetails", "PhysicalParametersDetails", "RequisitesDetails", "SpeciesBreed"
                        FROM pets
                        """);

        sqlQuery.ApplyFilterByColumn(columnName: "nickname", queryFieldValue: query.Name);
        sqlQuery.ApplyFilterByColumn(columnName: "volunteer_id", queryFieldValue: query.VolunteerId.ToString());
        //sqlQuery.ApplyFilterByColumn(columnName: "experience_in_years", queryFieldValue: query.Species);
        //sqlQuery.ApplyFilterByColumn(columnName: "experience_in_years", queryFieldValue: query.Breed);

        if (query.PetStatus is not null)
        {
            sqlTotalCountQuery.ApplyFilterByColumn(columnName: "help_status", queryFieldValue: (int)query.PetStatus);
        }

        sqlQuery.ApplySorting(sortBy: query.SortBy, sortDirection: query.SortDirection, dynamicParameters: parameters);

        sqlQuery.ApplyPagination(page: query.Page, pageSize: query.PageSize, dynamicParameters: parameters);

        var pets = await connection.QueryAsync<PetDTO, string?, string?, string?, string?, string?, PetDTO>(
            sqlQuery.ToString(),
            (pet, jsonLocation, jsonRequisites, jsonPhoto, jsonPhysicalParameters, jsonSpeciesBreed) =>
            {
                var locations = jsonLocation is null ? null : JsonSerializer.Deserialize<LocationDetailsDTO>(jsonLocation);
                var requisites = jsonRequisites is null ? null : JsonSerializer.Deserialize<RequisitesDetailsDTO>(jsonRequisites);
                var photo = jsonPhoto is null ? null : JsonSerializer.Deserialize<PhotoDetailsDTO>(jsonPhoto);
                var physicalParams = jsonPhysicalParameters is null ? null : JsonSerializer.Deserialize<PhysicalParametersDetailsDTO>(jsonPhysicalParameters);
                var speciesBreed = jsonSpeciesBreed is null ? null : JsonSerializer.Deserialize<SpeciesBreedDTO>(jsonSpeciesBreed);

                pet.LocationDetails = locations;
                pet.RequisitesDetails = requisites;
                pet.PhotoDetails = photo;
                pet.PhysicalParametersDetails = physicalParams;
                pet.SpeciesBreed = speciesBreed;

                return pet;
            },
            splitOn: "LocationDetails, PhotoDetails, PhysicalParametersDetails, RequisitesDetails, SpeciesBreed",
            param: parameters);

        var pagedList = new PagedList<PetDTO>()
        {
            Items = pets.ToList(),
            Page = query.Page,
            PageSize = query.PageSize,
            TotalCount = totalCount,
        };

        return pagedList;
    }
}
