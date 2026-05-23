using ProjectManagementSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Application.Features.Tasks.DTOs
{
    public class UpdateTaskStatusDTO
    {
        public Guid TaskId { get; set; }
        public ProjectTaskStatus Status { get; set; }  // Todo, InProgress, Done
    }
}
