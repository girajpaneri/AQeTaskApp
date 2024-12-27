using DBModels;
using FluentValidation;



namespace API.DtosValidator
{
    public class TaskDtoValidator : AbstractValidator<TaskDto>
    {
        public TaskDtoValidator()
        {
            RuleFor(t => t.Title)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");

            RuleFor(t => t.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");

            RuleFor(t => t.DueDate)
                .GreaterThanOrEqualTo(DateTime.Now).WithMessage("Due date must be in the future.");

            RuleFor(t => t.IsCompleted)
                .NotNull().WithMessage("Completion status is required.");
        }
    }
}
