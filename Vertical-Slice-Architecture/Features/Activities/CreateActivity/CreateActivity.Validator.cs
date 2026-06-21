using FluentValidation;

namespace Vertical_Slice_Architecture.Features.Activities.CreateActivity
{
   public class CreateActivityValidator : AbstractValidator<CreateActivityCommand>
    {
        public CreateActivityValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required");
        }
    }
}
