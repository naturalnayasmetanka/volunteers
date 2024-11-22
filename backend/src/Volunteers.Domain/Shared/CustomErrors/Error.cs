namespace Volunteers.Domain.Shared.CustomErrors;

public record Error
{
    private Error(
        string code,
        string message,
        ErrorType type)
    {
        Code = code;
        Message = message;
        Type = type;
    }

    public string Code { get; }
    public string Message { get; }
    public ErrorType Type { get; }

    public static Error Validation(string message, string code)
        => new Error(message, code, ErrorType.Validation);

    public static Error NotFound(string message, string code)
        => new Error(message, code, ErrorType.NotFound);

    public static Error Conflict(string message, string code)
        => new Error(message, code, ErrorType.Conflict);

    public static Error Failure(string message, string code)
        => new Error(message, code, ErrorType.Failure);

    public static Error ServerInternal(string message, string code)
        => new Error(message, code, ErrorType.ServerInternal);
}