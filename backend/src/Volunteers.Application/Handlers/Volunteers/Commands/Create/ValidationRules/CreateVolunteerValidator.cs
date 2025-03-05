using FluentValidation;
using Volunteers.Application.Handlers.Volunteers.Commands.Create.DTO;
using Volunteers.Application.Shared.Validations;
using Volunteers.Domain.PetManagment.Volunteer.ValueObjects;

namespace Volunteers.Application.Handlers.Volunteers.Commands.Create.ValidationRules;

public class CreateVolunteerValidator : AbstractValidator<CreateVolunteerDto>
{
    public CreateVolunteerValidator()
    {
        RuleFor(c => c.Name)
            .MustBeValueObject(Name.Create);

        RuleFor(c => c.Email)
            .MustBeValueObject(Email.Create);

        RuleFor(c => c.PhoneNumber)
            .MustBeValueObject(PhoneNumber.Create);

        RuleFor(c => c.ExperienceInYears)
            .MustBeValueObject(ExperienceInYears.Create);

        RuleForEach(c => c.VolunteerRequisites)
            .MustBeValueObject(c => VolunteerRequisite.Create(c.Title, c.Description));

        RuleForEach(c => c.SocialNetworks)
            .MustBeValueObject(c => SocialNetwork.Create(c.Title, c.Link));
    }
}