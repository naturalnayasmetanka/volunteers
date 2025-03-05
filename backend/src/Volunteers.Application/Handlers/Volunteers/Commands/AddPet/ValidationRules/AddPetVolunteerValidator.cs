using FluentValidation;
using Volunteers.Application.Handlers.Volunteers.Commands.AddPet.Commands;

namespace Volunteers.Application.Handlers.Volunteers.Commands.AddPet.ValidationRules;

public class AddPetVolunteerValidator : AbstractValidator<AddPetCommand>
{
    public AddPetVolunteerValidator()
    {
        RuleFor(x => x.Nickname).NotEmpty().MaximumLength(20);
        RuleFor(x => x.CommonDescription).NotEmpty().MaximumLength(1000);
        RuleFor(x => x.HelthDescription).NotEmpty().MaximumLength(1000);
        RuleFor(x => x.PetPhoneNumber).NotEmpty();
        RuleFor(x => x.PetStatus).NotEmpty();
        RuleFor(x => x.CreationDate).NotEmpty();
    }
}
