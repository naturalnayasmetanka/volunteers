using FluentValidation;
using Volunteers.Application.Handlers.Volunteers.Commands.UpdateRequisites.DTO;

namespace Volunteers.Application.Handlers.Volunteers.Commands.UpdateRequisites.ValidationRules;

public class UpdateRequisiteListValidator : AbstractValidator<UpdateRequisiteListDTO>
{
    public UpdateRequisiteListValidator()
    {
        RuleForEach(x => x.RequisiteList).SetValidator(new UpdateRequisiteValidator());
    }
}

public class UpdateRequisiteValidator : AbstractValidator<UpdateRequisiteDTO>
{
    public UpdateRequisiteValidator()
    {
        RuleFor(x => x.Title).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Description).NotEmpty().MaximumLength(1000);
    }
}