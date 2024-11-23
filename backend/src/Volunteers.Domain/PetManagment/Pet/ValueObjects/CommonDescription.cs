using CSharpFunctionalExtensions;
using Volunteers.Domain.Shared.CustomErrors;

namespace Volunteers.Domain.PetManagment.Pet.ValueObjects;

public record CommonDescription
{
    private CommonDescription(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<CommonDescription, Error> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Errors.General.ValueIsInvalid("Pet common description");

        var commonDescription = new CommonDescription(value: value);

        return commonDescription;
    }
}