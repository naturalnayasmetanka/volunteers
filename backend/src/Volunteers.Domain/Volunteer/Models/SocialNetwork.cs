using CSharpFunctionalExtensions;

namespace Volunteers.Domain.Volunteer.Models;

public record SocialNetwork
{
    private SocialNetwork(
        string title,
        string link)
    {
        Title = title;
        Link = link;
    }

    public string Title { get; }
    public string Link { get; }

    public static Result<SocialNetwork> Create(
        string title,
        string link)
    {
        if (string.IsNullOrWhiteSpace(title))
            return Result.Failure<SocialNetwork>("Title can not be empty");

        if (string.IsNullOrWhiteSpace(link))
            return Result.Failure<SocialNetwork>("Link can not be empty");

        var newSocial = new SocialNetwork(
            title: title,
            link: link);

        return Result.Success(newSocial);
    }
}