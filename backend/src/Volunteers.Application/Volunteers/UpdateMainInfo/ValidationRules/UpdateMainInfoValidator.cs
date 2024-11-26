using FluentValidation;
using Volunteers.Application.Shared.Validations;
using Volunteers.Application.Volunteers.UpdateMainInfo.DTO;
using Volunteers.Domain.PetManagment.Volunteer.ValueObjects;

namespace Volunteers.Application.Volunteers.UpdateMainInfo.ValidationRules;

public class UpdateMainInfoValidator : AbstractValidator<UpdateMainInfoDto>
{
    public UpdateMainInfoValidator()
    {
        RuleFor(x => x.Name).MustBeValueObject(Name.Create);
        RuleFor(x => x.Email).MustBeValueObject(Email.Create);
        RuleFor(x => x.ExperienceInYears).MustBeValueObject(ExperienceInYears.Create);
        RuleFor(x => x.PhoneNumber).MustBeValueObject(PhoneNumber.Create);
    }
}