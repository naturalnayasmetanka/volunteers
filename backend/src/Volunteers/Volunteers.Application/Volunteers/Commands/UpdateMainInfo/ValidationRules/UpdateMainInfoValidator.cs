using FluentValidation;
using Shared.Core.Validations;
using Volunteers.Application.Volunteers.Commands.UpdateMainInfo.DTO;
using Volunteers.Domain.Volunteers.ValueObjects;

namespace Volunteers.Application.Volunteers.Commands.UpdateMainInfo.ValidationRules;

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
