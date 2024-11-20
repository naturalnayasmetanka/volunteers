using CSharpFunctionalExtensions;
using Volunteers.Domain.Shared.CustomErrors;

namespace Volunteers.Domain.PetManagment.Pet.ValueObjects;

public class HelthDescription
{
    private HelthDescription(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<HelthDescription, Error> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Errors.General.ValueIsInvalid("Pet helth description");

        var helthDescription = new HelthDescription(value: value);

        return helthDescription;
    }
}