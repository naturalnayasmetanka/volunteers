using CSharpFunctionalExtensions;
using CustomEntity = Volunteers.Domain.Shared;

namespace Volunteers.Domain.Breed.Models;

public class Breed : CustomEntity.Entity<BreedId>
{
    private Breed(BreedId id)
        : base(id)
    {
    }

    private Breed(
        BreedId id,
        string title,
        string description
        ) : base(id)
    {
        Title = title;
        Description = description;
    }

    public string Title { get; private set; } = default!;
    public string Description { get; private set; } = default!;

    public static Result<Breed> Create(
        BreedId id,
        string title,
        string description)
    {
        if (string.IsNullOrWhiteSpace(title))
            return Result.Failure<Breed>("Title can not be empty");

        if (string.IsNullOrWhiteSpace(description))
            return Result.Failure<Breed>("Description can not be empty");

        var newBreed = new Breed(
            id,
            title,
            description);

        return Result.Success(newBreed);
    }
}