using MediatR;
using ProjectManagementSystem.Application.Common;
using ProjectManagementSystem.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Application.Features.Projects.Commands
{
    public class DeleteProjectCommand : IRequest<Result<string>>
    {
        public Guid Id { get; set; }
        public DeleteProjectCommand(Guid id) => Id = id;
    }

    public class DeleteProjectCommandHandler : IRequestHandler<DeleteProjectCommand, Result<string>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteProjectCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<string>> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _unitOfWork.Projects.GetByIdAsync(request.Id);

            if (project == null)
            {
                return Result<string>.Failure("Project not found.");
            }

           
            _unitOfWork.Projects.Delete(project);
            await _unitOfWork.SaveChangesAsync();

            return Result<string>.Success("Project deleted successfully.");
        }
    }
}
