using CSharpFunctionalExtensions;
using CustomEntity = Volunteers.Domain.Shared;
using SpeciesModel = Volunteers.Domain.Species.Models.Species;

namespace Volunteers.Domain.Breed.Models;

public class Breed : CustomEntity.Models.Entity<BreedId>
{
    private Breed(BreedId id)
        : base(id)
    {
    }

    private Breed(
        BreedId id,
        Title title,
        Description description
        ) : base(id)
    {
        Title = title;
        Description = description;
    }

    public Title Title { get; private set; } = default!;
    public Description Description { get; private set; } = default!;

    public SpeciesModel Species { get; private set; } = default!;

    public static Result<Breed> Create(
        BreedId id,
        Title title,
        Description description)
    {
        var newBreed = new Breed(
            id,
            title,
            description);

        return Result.Success(newBreed);
    }
}