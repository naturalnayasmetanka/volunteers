using CSharpFunctionalExtensions;
using Volunteers.Domain.Shared.Ids;
using Volunteers.Domain.SpeciesManagment.Breed.ValueObjects;
using CustomEntity = Volunteers.Domain.Shared;
using SpeciesModel = Volunteers.Domain.SpeciesManagment.Species.AggregateRoot.Species;

namespace Volunteers.Domain.SpeciesManagment.Breed.Entities;

public class Breed : CustomEntity.Entity<BreedId>
{
    private Breed(BreedId id) : base(id) { }

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