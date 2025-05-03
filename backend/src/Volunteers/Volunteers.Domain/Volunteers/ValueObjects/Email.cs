using CSharpFunctionalExtensions;
using Shared.Kernel.CustomErrors;

namespace Volunteers.Domain.Volunteers.ValueObjects;

public record Email
{
    private Email(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<Email, Error> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Errors.General.ValueIsInvalid("Volunteer Email");

        var email = new Email(value: value);

        return email;
    }
}