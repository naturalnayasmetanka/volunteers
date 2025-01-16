using FluentValidation;
using Volunteers.Application.Shared.Validations;
using Volunteers.Application.Volunteer.CreateVolunteer.DTO;
using Volunteers.Domain.PetManagment.Volunteer.ValueObjects;

namespace Volunteers.Application.Volunteer.CreateVolunteer.ValidatorRules;

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