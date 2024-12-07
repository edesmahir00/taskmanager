using Microsoft.EntityFrameworkCore;
using TaskManager.Core.Entities;
using TaskManager.Core.Interfaces;
using TaskManager.Infrastructure.Data;
using Task = TaskManager.Core.Entities.Task;
using TaskStatus = TaskManager.Core.Enums.TaskStatus;

namespace TaskManager.Infrastructure.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly TaskManagerDbContext _context;

        public TaskRepository(TaskManagerDbContext context)
        {
            _context = context;
        }

        public async System.Threading.Tasks.Task AddAsync(Task task)
        {
            await _context.Tasks.AddAsync(task);
            await _context.SaveChangesAsync();
        }

        public async Task<Task> GetByIdAsync(int id)
        {
            return await _context.Tasks.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<List<Task>> GetAllAsync(int page, int pageSize)
        {
            return await _context.Tasks
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async System.Threading.Tasks.Task UpdateAsync(Task task)
        {
            _context.Tasks.Update(task);
            await _context.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task DeleteAsync(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task != null)
            {
                task.Status = (System.Threading.Tasks.TaskStatus)TaskStatus.Passive; // Pasif yapýyoruz
                _context.Tasks.Update(task);
                await _context.SaveChangesAsync();
            }
        }
    }
}
