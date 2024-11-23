using CSharpFunctionalExtensions;
using Volunteers.Domain.Shared.CustomErrors;

namespace Volunteers.Domain.SpeciesManagment.Species.ValueObjects;

public class Title
{
    private Title(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<Title, Error> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Errors.General.ValueIsInvalid("Species title");

        var title = new Title(value: value);

        return title;
    }
}