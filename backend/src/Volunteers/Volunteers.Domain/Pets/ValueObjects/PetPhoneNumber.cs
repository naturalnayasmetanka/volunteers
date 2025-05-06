using CSharpFunctionalExtensions;
using Shared.Kernel.CustomErrors;

namespace Volunteers.Domain.Pets.ValueObjects;

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