using CSharpFunctionalExtensions;
using Volunteers.Domain.Shared.CustomErrors;

namespace Volunteers.Domain.Pet.Models;

public class PetPhoneNumber
{
    private PetPhoneNumber(int value)
    {
        Value = value;
    }

    public int Value { get; }

    public static Result<PetPhoneNumber, Error> Create(int value)
    {
        if (value <= 0)
            return Errors.General.ValueIsInvalid("Pet Phone Number"); ;

        var phoneNumber = new PetPhoneNumber(value: value);

        return phoneNumber;
    }
}