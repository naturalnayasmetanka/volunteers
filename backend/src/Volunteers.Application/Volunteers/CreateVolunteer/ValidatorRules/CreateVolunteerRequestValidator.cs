using FluentValidation;
using Volunteers.Application.Shared.Validations;
using Volunteers.Application.Volunteers.CreateVolunteer.RequestModels;
using Volunteers.Domain.PetManagment.Volunteer.ValueObjects;

namespace Volunteers.Application.Volunteer.CreateVolunteer.ValidatorRules;

public class CreateVolunteerRequestValidator : AbstractValidator<CreateVolunteerRequest>
{
    public CreateVolunteerRequestValidator()
    {
        RuleFor(c => c.VolunteerDto.Name)
            .MustBeValueObject(Name.Create);

        RuleFor(c => c.VolunteerDto.Email)
            .MustBeValueObject(Email.Create);

        RuleFor(c => c.VolunteerDto.PhoneNumber)
            .MustBeValueObject(PhoneNumber.Create);

        RuleFor(c => c.VolunteerDto.ExperienceInYears)
            .MustBeValueObject(ExperienceInYears.Create);

        RuleForEach(c => c.VolunteerDto.VolunteerRequisites)
            .MustBeValueObject(c =>VolunteerRequisite.Create(c.Title, c.Description));

        RuleForEach(c => c.VolunteerDto.SocialNetworks)
            .MustBeValueObject(c => VolunteerRequisite.Create(c.Title, c.Link));
    }
}