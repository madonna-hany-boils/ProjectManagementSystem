using MediatR;
using ProjectManagementSystem.Application.Common;
using ProjectManagementSystem.Application.Interfaces;
using ProjectManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Application.Features.Tasks.Queries
{
    public class GetTasksByProjectIdQuery : IRequest<Result<IEnumerable<ProjectTask>>>
    {
        public Guid ProjectId { get; set; }
        public GetTasksByProjectIdQuery(Guid projectId) => ProjectId = projectId;
    }

    public class GetTasksByProjectIdQueryHandler : IRequestHandler<GetTasksByProjectIdQuery, Result<IEnumerable<ProjectTask>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetTasksByProjectIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<IEnumerable<ProjectTask>>> Handle(GetTasksByProjectIdQuery request, CancellationToken cancellationToken)
        {
            var projectExists = await _unitOfWork.Projects.GetByIdAsync(request.ProjectId);
            if (projectExists == null)
            {
                return Result<IEnumerable<ProjectTask>>.Failure("Project not found.");
            }

            var tasks = await _unitOfWork.Tasks.GetTasksByProjectIdAsync(request.ProjectId);
            return Result<IEnumerable<ProjectTask>>.Success(tasks);
        }
    }
}
