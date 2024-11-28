using FluentValidation;
using Volunteers.Application.Volunteers.UpdateSotialNetworks.DTO;

namespace Volunteers.Application.Volunteers.UpdateSotialNetworks.ValidationRules;

public class UpdateSocialListInfoValidator : AbstractValidator<UpdateSocialListDto>
{
    public UpdateSocialListInfoValidator()
    {
        RuleForEach(x => x.ListSocial).SetValidator(new UpdateSocialInfoValidator());
    }
}

public class UpdateSocialInfoValidator : AbstractValidator<UpdateSocialDto>
{
    public UpdateSocialInfoValidator()
    {
        RuleFor(x => x.Title).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Link).NotEmpty().MaximumLength(1000);
    }
}