using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Application.Interfaces;
using ProjectManagementSystem.Domain.Entities;
using ProjectManagementSystem.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Infrastructure.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly ApplicationDbContext _context;

        public TaskRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ProjectTask?> GetByIdAsync(Guid id)
        {
            return await _context.Set<ProjectTask>().FindAsync(id);
        }

        public async Task<IEnumerable<ProjectTask>> GetTasksByProjectIdAsync(Guid projectId)
        {
            return await _context.Set<ProjectTask>()
                .Where(t => t.ProjectId == projectId)
                .AsNoTracking()
                .ToListAsync();
        }
        public async Task<IEnumerable<ProjectTask>> GetAllAsync()
        {
            return await _context.Set<ProjectTask>()
                                 .AsNoTracking()
                                 .ToListAsync();
        }
        public async Task AddAsync(ProjectTask task)
        {
            await _context.Set<ProjectTask>().AddAsync(task);
        }

        public void Update(ProjectTask task)
        {
            _context.Set<ProjectTask>().Update(task);
        }

        public void Delete(ProjectTask task)
        {
            _context.Set<ProjectTask>().Remove(task);
        }
    }
}
