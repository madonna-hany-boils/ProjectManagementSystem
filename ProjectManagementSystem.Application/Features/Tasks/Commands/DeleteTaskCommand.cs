using MediatR;
using ProjectManagementSystem.Application.Common;
using ProjectManagementSystem.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Application.Features.Tasks.Commands
{
    public class DeleteTaskCommand : IRequest<Result<string>>
    {
        public Guid Id { get; set; }
        public DeleteTaskCommand(Guid id) => Id = id;
    }

    public class DeleteTaskCommandHandler : IRequestHandler<DeleteTaskCommand, Result<string>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteTaskCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<string>> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
        {
            var task = await _unitOfWork.Tasks.GetByIdAsync(request.Id);
            if (task == null)
            {
                return Result<string>.Failure("Task not found.");
            }

            _unitOfWork.Tasks.Delete(task);
            await _unitOfWork.SaveChangesAsync();

            return Result<string>.Success("Task deleted successfully.");
        }
    }
}
