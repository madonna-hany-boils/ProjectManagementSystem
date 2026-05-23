using MediatR;
using ProjectManagementSystem.Application.Common;
using ProjectManagementSystem.Application.Features.Projects.DTOs;
using ProjectManagementSystem.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Application.Features.Projects.Commands
{
    public class UpdateProjectCommand : IRequest<Result<string>>
    {
        public UpdateProjectDTO UpdateProjectDTO { get; set; } = null!;
    }

    public class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand, Result<string>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateProjectCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<string>> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
        {
         
            var project = await _unitOfWork.Projects.GetByIdAsync(request.UpdateProjectDTO.Id);

            if (project == null)
            {
                return Result<string>.Failure("Project not found.");
            }

            project.Name = request.UpdateProjectDTO.Name;
            project.Description = request.UpdateProjectDTO.Description;

        
            _unitOfWork.Projects.Update(project);
            await _unitOfWork.SaveChangesAsync();

            return Result<string>.Success("Project updated successfully.");
        }
    }
}
