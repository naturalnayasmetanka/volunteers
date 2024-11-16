namespace Volunteers.Domain.Shared.Models.CustomErrors;

public static class Errors
{
    public static class General
    {
        public static Error ValueIsInvalid(string? name = null)
        {
            var label = name ?? "- value -";

            return Error.Validation("value.is.invalid", $"{label} is invalid");
        }

        public static Error NotFound(Guid? id = null)
        {
            var labelId = id == null ? string.Empty : $"- {id} -";

            return Error.Validation("record.not.found", $"item not found by id {labelId}");
        }

        public static Error ValueIsRequired(string? value = null)
        {
            var label = value == null ? string.Empty : $"- {value} -";

            return Error.Validation("length.is.invalid", $"invalid {label} length");
        }
    }
}