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
    public class GetAllTasksQuery : IRequest<Result<IEnumerable<ProjectTask>>>
    {
    }

    public class GetAllTasksQueryHandler : IRequestHandler<GetAllTasksQuery, Result<IEnumerable<ProjectTask>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllTasksQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<IEnumerable<ProjectTask>>> Handle(GetAllTasksQuery request, CancellationToken cancellationToken)
        {
            var tasks = await _unitOfWork.Tasks.GetAllAsync();

            return Result<IEnumerable<ProjectTask>>.Success(tasks);
        }
    }
}
