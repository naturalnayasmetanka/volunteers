using FluentValidation;
using Volunteers.Application.Volunteers.UpdateMainInfo.RequestModels;

namespace Volunteers.Application.Volunteers.UpdateMainInfo.ValidationRules;

public class UpdateMainInfoValidator : AbstractValidator<UpdateMainInfoRequest>
{
    public UpdateMainInfoValidator()
    {

    }
}