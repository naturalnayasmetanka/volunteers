using CSharpFunctionalExtensions;
using Volunteers.Domain.Shared.CustomErrors;

namespace Volunteers.Domain.SpeciesManagment.Breed.ValueObjects;

public record Title
{
    private Title(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<Title, Error> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Errors.General.ValueIsInvalid("Breed title");

        var title = new Title(value: value);

        return title;
    }
}