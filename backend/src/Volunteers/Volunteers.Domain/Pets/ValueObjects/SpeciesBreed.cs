using CSharpFunctionalExtensions;

namespace Volunteers.Domain.PetManagment.Pet.ValueObjects;

public record SpeciesBreed
{
    private SpeciesBreed(Guid speciesId, Guid breedId)
    {
        SpeciesId = speciesId;
        BreedId = breedId;
    }

    public Guid SpeciesId { get; }
    public Guid BreedId { get; }

    public static Result<SpeciesBreed> Create(
        Guid speciesId,
        Guid breedId)
    {
        var newSpeciesBreed = new SpeciesBreed(
            speciesId,
            breedId);

        return Result.Success(newSpeciesBreed);
    }
}