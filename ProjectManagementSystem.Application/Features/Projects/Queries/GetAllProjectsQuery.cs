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
    public class GetAllProjectsQuery : IRequest<Result<IEnumerable<Project>>>
    {
    }

    public class GetAllProjectsQueryHandler : IRequestHandler<GetAllProjectsQuery, Result<IEnumerable<Project>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllProjectsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<IEnumerable<Project>>> Handle(GetAllProjectsQuery request, CancellationToken cancellationToken)
        {
         
            var projects = await _unitOfWork.Projects.GetAllAsync();

            if (projects == null)
            {
                return Result<IEnumerable<Project>>.Failure("No projects found or an error occurred.");
            }

            return Result<IEnumerable<Project>>.Success(projects);
        }
    }
}

