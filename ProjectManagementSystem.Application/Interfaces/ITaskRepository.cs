using ProjectManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Application.Interfaces
{
    public interface ITaskRepository
    {
        Task<ProjectTask?> GetByIdAsync(Guid id);
        Task<IEnumerable<ProjectTask>> GetTasksByProjectIdAsync(Guid projectId); // ميثود خاصة لجلب مهام مشروع معين
        Task AddAsync(ProjectTask task);
        Task<IEnumerable<ProjectTask>> GetAllAsync();
        void Update(ProjectTask task);
        void Delete(ProjectTask task);
    }
}
