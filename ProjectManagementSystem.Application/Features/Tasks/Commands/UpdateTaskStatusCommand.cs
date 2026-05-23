using MediatR;
using ProjectManagementSystem.Application.Common;
using ProjectManagementSystem.Application.Features.Tasks.DTOs;
using ProjectManagementSystem.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Application.Features.Tasks.Commands
{
    public class UpdateTaskStatusCommand : IRequest<Result<string>>
    {
        public UpdateTaskStatusDTO UpdateTaskStatusDTO { get; set; } = null!;
    }

    public class UpdateTaskStatusCommandHandler : IRequestHandler<UpdateTaskStatusCommand, Result<string>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateTaskStatusCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<string>> Handle(UpdateTaskStatusCommand request, CancellationToken cancellationToken)
        {
            var task = await _unitOfWork.Tasks.GetByIdAsync(request.UpdateTaskStatusDTO.TaskId);
            if (task == null)
            {
                return Result<string>.Failure("Task not found.");
            }

            task.Status = request.UpdateTaskStatusDTO.Status;

            _unitOfWork.Tasks.Update(task);
            await _unitOfWork.SaveChangesAsync();

            return Result<string>.Success("Task status updated successfully.");
        }
    }
}
