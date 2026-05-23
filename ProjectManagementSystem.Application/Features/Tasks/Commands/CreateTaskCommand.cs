using MediatR;
using ProjectManagementSystem.Application.Common;
using ProjectManagementSystem.Application.Features.Tasks.DTOs;
using ProjectManagementSystem.Application.Interfaces;
using ProjectManagementSystem.Domain.Entities;
using ProjectManagementSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Application.Features.Tasks.Commands
{
    public class CreateTaskCommand : IRequest<Result<string>>
    {
        public CreateTaskDTO CreateTaskDTO { get; set; } = null!;
    }

    
    public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, Result<string>>
    {
     
        private readonly IUnitOfWork _unitOfWork;
        private readonly FluentValidation.IValidator<CreateTaskCommand> _validator;

        public CreateTaskCommandHandler(IUnitOfWork unitOfWork, FluentValidation.IValidator<CreateTaskCommand> validator)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task<Result<string>> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
               
                var errors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                return Result<string>.Failure(errors);
            }

           
            var projectExists = await _unitOfWork.Projects.GetByIdAsync(request.CreateTaskDTO.ProjectId);
            if (projectExists == null)
            {
                return Result<string>.Failure("Target project not found.");
            }

            var task = new ProjectTask
            {
                Title = request.CreateTaskDTO.Title,
                Description = request.CreateTaskDTO.Description,
                ProjectId = request.CreateTaskDTO.ProjectId,
                Priority = request.CreateTaskDTO.Priority,
                DueDate = request.CreateTaskDTO.DueDate,
                Status = ProjectTaskStatus.Todo,
                CreatedAt = DateTime.UtcNow
            };

            await _unitOfWork.Tasks.AddAsync(task);
            await _unitOfWork.SaveChangesAsync();

            return Result<string>.Success("Task created successfully.");
        }
    }
}
