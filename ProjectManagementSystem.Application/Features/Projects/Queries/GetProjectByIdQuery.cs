using MediatR;
using ProjectManagementSystem.Application.Common;
using ProjectManagementSystem.Application.Interfaces;
using ProjectManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Application.Features.Projects.Queries
{
    public class GetProjectByIdQuery : IRequest<Result<Project>>
    {
        public Guid Id { get; set; }
        public GetProjectByIdQuery(Guid id) => Id = id;
    }

    
    public class GetProjectByIdQueryHandler : IRequestHandler<GetProjectByIdQuery, Result<Project>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetProjectByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Project>> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
        {
            var project = await _unitOfWork.Projects.GetByIdAsync(request.Id);

            if (project == null)
            {
                return Result<Project>.Failure("Project not found.");
            }

            return Result<Project>.Success(project);
        }
    }
}
