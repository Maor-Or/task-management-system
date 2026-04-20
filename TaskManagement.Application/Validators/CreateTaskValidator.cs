using FluentValidation;
using TaskManagement.Application.DTOs;
namespace TaskManagement.Application.Validators
{
    public class CreateTaskValidator : AbstractValidator<CreateTaskDto>
    {
        public CreateTaskValidator() 
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required")
                .MaximumLength(100);

            RuleFor(x => x.Description)
                .MaximumLength(500);

            RuleFor(x => x.Priority)
                .InclusiveBetween(1, 3)
                .WithMessage("Priority must be between 1 and 3");
        }
    }
}
