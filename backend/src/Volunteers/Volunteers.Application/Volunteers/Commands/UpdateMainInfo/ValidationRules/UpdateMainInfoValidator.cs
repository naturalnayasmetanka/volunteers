using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
