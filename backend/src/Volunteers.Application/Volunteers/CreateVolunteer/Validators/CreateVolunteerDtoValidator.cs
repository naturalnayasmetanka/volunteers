using FluentValidation;
using Volunteers.Application.Volunteer.CreateVolunteer.DTO;

namespace Volunteers.Application.Volunteer.CreateVolunteer.Validators;

public class CreateVolunteerDtoValidator : AbstractValidator<CreateVolunteerDto>
{
    public CreateVolunteerDtoValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty()
            .MaximumLength(50)
            .WithMessage("Invalid name value");

        RuleFor(c => c.Email)
            .NotEmpty()
            .MaximumLength(50)
            .EmailAddress()
            .WithMessage("Invalid email value");

        RuleFor(c => c.PhoneNumber)
            .NotEmpty()
            .Must(c => c > 0)
            .WithMessage("Invalid phone number value");

        RuleFor(c => c.ExperienceInYears)
            .NotEmpty()
            .Must(c => c > 0)
            .WithMessage("Invalid experience value");
    }
}