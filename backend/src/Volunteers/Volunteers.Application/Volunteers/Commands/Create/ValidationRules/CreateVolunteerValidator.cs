﻿using FluentValidation;
using Shared.Core.Validations;
using Volunteers.Application.Volunteers.Commands.Create.DTO;
using Volunteers.Domain.Volunteers.ValueObjects;

namespace Volunteers.Application.Volunteers.Commands.Create.ValidationRules;

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
