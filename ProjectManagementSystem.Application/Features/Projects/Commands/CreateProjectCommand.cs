using MediatR;
using ProjectManagementSystem.Application.Common;
using ProjectManagementSystem.Application.Features.Projects.DTOs;
using ProjectManagementSystem.Application.Interfaces;
using ProjectManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Application.Features.Projects.Commonds
{
    public class CreateProjectCommand : IRequest<Result<string>>
    {
        public CreateProjectDTO CreateProjectDTO { get; set; } = null!;
    }

   
    public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, Result<string>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateProjectCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<string>> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
          
            if (request.CreateProjectDTO == null || string.IsNullOrWhiteSpace(request.CreateProjectDTO.Name))
            {
                return Result<string>.Failure("Project name is required.");
            }
              var project = new Project
            {
                Name = request.CreateProjectDTO.Name,
                Description = request.CreateProjectDTO.Description,
                CreatedAt = DateTime.UtcNow,
                  UserId = request.CreateProjectDTO.UserId

              };

           
            await _unitOfWork.Projects.AddAsync(project);

          
            await _unitOfWork.SaveChangesAsync();

            return Result<string>.Success("Project created successfully.");
        }
    }
}
