using CSharpFunctionalExtensions;
using Volunteers.Domain.Shared.CustomErrors;

namespace Volunteers.Domain.Pet.Models;

public class Nickname
{
    private Nickname(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<Nickname, Error> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Errors.General.ValueIsInvalid("Pet nickname");

        var nickname = new Nickname(value: value);

        return nickname;
    }
}