using CSharpFunctionalExtensions;
using Volunteers.Domain.Shared.CustomErrors;

namespace Volunteers.Domain.PetManagment.Pet.ValueObjects;

public record SerialNumber
{
    private SerialNumber(int value)
    {
        Value = value;
    }

    public int Value { get; }

    public static SerialNumber First => new(1);

    public static Result<SerialNumber, Error> Create(int value)
    {
        if (value <= 0)
            return Errors.General.ValueIsInvalid("SerialNumber");

        var nickname = new SerialNumber(value: value);

        return nickname;
    }
}