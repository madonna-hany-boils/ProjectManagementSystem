using FluentValidation;
using ProjectManagementSystem.Application.Features.Projects.Commonds;
using ProjectManagementSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Application.Features.Tasks.Commands
{
    public class CreateTaskCommandValidator : AbstractValidator<CreateTaskCommand>
    {
        public CreateTaskCommandValidator()
        {
            RuleFor(x => x.CreateTaskDTO.Title)
                .NotEmpty().WithMessage("Task title is required.")
                .MaximumLength(150).WithMessage("Task title cannot exceed 150 characters.");

            RuleFor(x => x.CreateTaskDTO.DueDate)
                .GreaterThan(DateTime.UtcNow).WithMessage("Due date must be a future date.");

            // Fixed using IsInEnum() which automatically validates against Low, Medium, High
            RuleFor(x => x.CreateTaskDTO.Priority)
                .IsInEnum().WithMessage("Priority must be either Low, Medium, or High.");
        }
    }
}
