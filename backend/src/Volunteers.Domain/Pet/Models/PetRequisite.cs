using CSharpFunctionalExtensions;

namespace Volunteers.Domain.Pet.Models;

public record PetRequisite
{
    private PetRequisite(
        string title,
        string description)
    {
        Title = title;
        Description = description;
    }

    public string Title { get; }
    public string Description { get; }

    public static Result<PetRequisite> Create(
        string title,
        string description)
    {
        if (string.IsNullOrWhiteSpace(title))
            return Result.Failure<PetRequisite>("Title can not be empty");

        if (string.IsNullOrWhiteSpace(description))
            return Result.Failure<PetRequisite>("Description can not be empty");

        var newRequisite = new PetRequisite(
            title: title,
            description: description);

        return Result.Success(newRequisite);
    }
}