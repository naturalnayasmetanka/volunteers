using CSharpFunctionalExtensions;
using Volunteers.Domain.Shared.CustomErrors;

namespace Volunteers.Domain.PetManagment.Pet.ValueObjects;

public record Position
{
    private Position(int value)
    {
        Value = value;
    }

    public int Value { get; }

    public static Position First => new(1);

    public static Result<Position, Error> Create(int value)
    {
        if (value <= 0)
            return Errors.General.ValueIsInvalid("SerialNumber");

        var nickname = new Position(value: value);

        return nickname;
    }

    public Result<Position, Error> Forward()
        => Create(Value + 1);

    public Result<Position, Error> Back()
        => Create(Value - 1);
}