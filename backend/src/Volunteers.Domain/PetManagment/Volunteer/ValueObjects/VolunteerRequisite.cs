using CSharpFunctionalExtensions;
using Volunteers.Domain.Shared.CustomErrors;

namespace Volunteers.Domain.PetManagment.Volunteer.ValueObjects;

public record VolunteerRequisite
{
    private VolunteerRequisite(
        string title,
        string description)
    {
        Title = title;
        Description = description;
    }

    public string Title { get; }
    public string Description { get; }

    public static Result<VolunteerRequisite, Error> Create(
        string title,
        string description)
    {
        if (string.IsNullOrWhiteSpace(title))
            return Errors.General.ValueIsInvalid("Requisite Title");

        if (string.IsNullOrWhiteSpace(description))
            return Errors.General.ValueIsInvalid("Requisite Description");

        var newRequisite = new VolunteerRequisite(
            title: title,
            description: description);

        return newRequisite;
    }
}