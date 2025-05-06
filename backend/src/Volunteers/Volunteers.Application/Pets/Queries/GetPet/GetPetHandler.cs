using System.Text;
using System.Text.Json;
using CSharpFunctionalExtensions;
using Dapper;
using Shared.Core.Abstractions.Database;
using Shared.Core.Abstractions.Handlers;
using Shared.Core.DTO;
using Shared.Kernel.CustomErrors;
using Volunteers.Application.Pets.Queries.GetPet.Queries;

namespace Volunteers.Application.Pets.Queries.GetPet;

public class GetPetHandler : IQueryHandler<PetDTO?, GetPetQuery>
{
    private readonly ISqlConnConnectionFactory _connection;
    public GetPetHandler(ISqlConnConnectionFactory connection)
    {
        _connection = connection;
    }

    public async Task<Result<PetDTO?, Error>> Handle(
        GetPetQuery query,
        CancellationToken cancellationToken = default)
    {
        var connection = _connection.Create();

        var parameters = new DynamicParameters();
        parameters.Add("@Id", query.Id);

        var sqlQuery = new StringBuilder("""
                        SELECT 
                            id, nickname, common_description, helth_description, phone_number, help_status, birth_date, creation_date,
                            "position", volunteer_id, "LocationDetails", "PhotoDetails", "PhysicalParametersDetails", "RequisitesDetails", "SpeciesBreed"
                        FROM pets
                        WHERE Id = @Id
                        """);

        var pet = await connection.QueryAsync<PetDTO, string?, string?, string?, string?, string?, PetDTO>(
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


        return pet.FirstOrDefault();
    }
}
