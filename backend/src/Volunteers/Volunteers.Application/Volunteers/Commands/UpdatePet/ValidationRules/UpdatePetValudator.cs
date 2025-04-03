using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Volunteers.Application.Volunteers.Commands.UpdatePet.ValidationRules;

public class UpdatePetValudator : AbstractValidator<UpdatePetCommand>
{
    public UpdatePetValudator()
    {
        RuleFor(x => x.Nickname).NotEmpty().MaximumLength(20);
        RuleFor(x => x.CommonDescription).NotEmpty().MaximumLength(1000);
        RuleFor(x => x.HelthDescription).NotEmpty().MaximumLength(1000);
        RuleFor(x => x.PetPhoneNumber).NotEmpty();
        RuleFor(x => x.PetStatus).NotEmpty();
        RuleFor(x => x.CreationDate).NotEmpty();
    }
}