using FluentValidation;
using ProjectManagementSystem.Application.Features.Projects.Commonds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Application.Features.Projects.Commands
{
    public class CreateProjectCommandValidator : AbstractValidator<CreateProjectCommand>
    {
        public CreateProjectCommandValidator()
        {
            RuleFor(x => x.CreateProjectDTO.Name)
                .NotEmpty().WithMessage("Project name is required.")
                .MaximumLength(100).WithMessage("Project name cannot exceed 100 characters.");

            RuleFor(x => x.CreateProjectDTO.Description)
                .NotEmpty().WithMessage("Project description is required.")
                .MinimumLength(10).WithMessage("Description must be at least 10 characters long.");
        }
    }
}
