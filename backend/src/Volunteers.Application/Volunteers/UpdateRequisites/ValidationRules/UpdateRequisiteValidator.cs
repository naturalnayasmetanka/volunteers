using FluentValidation;
using Volunteers.Application.Volunteers.UpdateRequisites.DTO;

namespace Volunteers.Application.Volunteers.UpdateRequisites.ValidationRules;

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