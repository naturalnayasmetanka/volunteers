using CSharpFunctionalExtensions;
using Shared.Kernel.CustomErrors;

namespace Species.Domain.Species.ValueObjects;

public class Description
{
    private Description(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<Description, Error> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Errors.General.ValueIsInvalid("Species description");

        var description = new Description(value: value);

        return description;
    }
}