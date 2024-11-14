using CSharpFunctionalExtensions;

namespace Volunteers.Domain.Volunteer.Models;

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

    public static Result<VolunteerRequisite> Create(
        string title,
        string description)
    {
        if (string.IsNullOrWhiteSpace(title))
            return Result.Failure<VolunteerRequisite>("Title can not be empty");

        if (string.IsNullOrWhiteSpace(description))
            return Result.Failure<VolunteerRequisite>("Description can not be empty");

        var newRequisite = new VolunteerRequisite(
            title: title,
            description: description);

        return Result.Success(newRequisite);
    }
}