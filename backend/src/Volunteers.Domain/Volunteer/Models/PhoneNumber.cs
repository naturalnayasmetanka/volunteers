using CSharpFunctionalExtensions;
using Volunteers.Domain.Shared.CustomErrors;

namespace Volunteers.Domain.Volunteer.Models;

public record PhoneNumber
{
    private PhoneNumber(int value)
    {
        Value = value;
    }

    public int Value { get; }

    public static Result<PhoneNumber, Error> Create(int value)
    {
        if (value <= 0)
            return Errors.General.ValueIsInvalid("Volunteer Phone Number"); ;

        var phoneNumber = new PhoneNumber(value: value);

        return phoneNumber;
    }
}