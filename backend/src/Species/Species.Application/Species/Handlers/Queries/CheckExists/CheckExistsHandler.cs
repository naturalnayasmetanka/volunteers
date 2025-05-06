using System.Text;
using CSharpFunctionalExtensions;
using Dapper;
using Microsoft.Extensions.Logging;
using Shared.Core.Abstractions.Database;
using Shared.Core.Abstractions.Handlers;
using Shared.Kernel.CustomErrors;
using Species.Application.Species.Handlers.Queries.CheckExists.Queries;

namespace Species.Application.Species.Handlers.Queries.CheckExists;

public class CheckExistsHandler : IQueryHandler<bool, CheckExistsQuery>
{
    private readonly ILogger<CheckExistsHandler> _logger;
    private readonly ISqlConnConnectionFactory _connection;

    public CheckExistsHandler(ISqlConnConnectionFactory connection, ILogger<CheckExistsHandler> logger)
    {
        _connection = connection;
        _logger = logger;
    }

    public async Task<Result<bool, Error>> Handle(CheckExistsQuery query, CancellationToken cancellationToken = default)
    {
        var connection = _connection.Create();

        var parameters = new DynamicParameters();
        parameters.Add("@SpeciesId", query.SpeciesId);
        parameters.Add("@BreedId", query.BreedId);

        var sqlIsExistSpeciesQuery = new StringBuilder("""
                            SELECT EXISTS (SELECT * FROM species WHERE id = @SpeciesId)
                        """
                     );

        var isExistSpecies = await connection.ExecuteScalarAsync<bool>(sqlIsExistSpeciesQuery.ToString(), parameters);

        if (!isExistSpecies)
        {
            _logger.LogError("Species {0} was not found", query.SpeciesId);

            return Errors.General.NotFound(query.SpeciesId);
        }

        var sqlIsExistBreedQuery = new StringBuilder("""
                            SELECT EXISTS (SELECT * FROM breeds WHERE id = @BreedId AND species_id = @SpeciesId)
                        """
                     );

        var isExistBreed = await connection.ExecuteScalarAsync<bool>(sqlIsExistBreedQuery.ToString(), parameters);

        if (!isExistBreed)
        {
            _logger.LogError("Breed {0} was not found", query.BreedId);

            return Errors.General.NotFound(query.BreedId);
        }

        return true;
    }
}

