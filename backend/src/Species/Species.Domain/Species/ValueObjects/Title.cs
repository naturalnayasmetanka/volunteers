using CSharpFunctionalExtensions;
using Shared.Kernel.CustomErrors;

namespace Species.Domain.Species.ValueObjects;

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