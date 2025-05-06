using CSharpFunctionalExtensions;
using Shared.Kernel.Ids;
using Species.Domain.Species.ValueObjects;
using CustomEntity = Shared.Kernel;
using BreedModel = Species.Domain.Breeds.Entities.Breed;

namespace Species.Domain.Species.AggregateRoot;

public class Species : CustomEntity.Entity<SpeciesId>
{
    private List<BreedModel> _breeds = [];

    private Species(SpeciesId id) : base(id) { }

    public Species(
        SpeciesId id,
        Title title,
        Description description
        ) : base(id)
    {
        Title = title;
        Description = description;
    }

    public Title Title { get; private set; } = default!;
    public Description Description { get; private set; } = default!;

    public IReadOnlyList<BreedModel> Breeds => _breeds;

    public static Result<Species> Create(
        SpeciesId id,
        Title title,
        Description description)
    {
        var newSpecies = new Species(
            id: id,
            title: title,
            description: description);

        return Result.Success(newSpecies);
    }

    public void AddBreed(BreedModel newBreed)
    {
        _breeds.Add(newBreed);
    }
}