using CSharpFunctionalExtensions;
using Volunteers.Domain.Shared.CustomErrors;

namespace Volunteers.Domain.PetManagment.Volunteer.ValueObjects;

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

    public static Result<SocialNetwork, Error> Create(
        string title,
        string link)
    {
        if (string.IsNullOrWhiteSpace(title))
            return Errors.General.ValueIsInvalid("Social network title");

        if (string.IsNullOrWhiteSpace(link))
            return Errors.General.ValueIsInvalid("Social network link");

        var newSocial = new SocialNetwork(
            title: title,
            link: link);

        return newSocial;
    }
}