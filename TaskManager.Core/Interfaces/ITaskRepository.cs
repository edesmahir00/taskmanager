using Task = TaskManager.Core.Entities.Task;

namespace TaskManager.Core.Interfaces
{
    public interface ITaskRepository
    {
        System.Threading.Tasks.Task AddAsync(Task task);
        Task<Task> GetByIdAsync(int id);
        Task<List<Task>> GetAllAsync(int page, int pageSize);
        System.Threading.Tasks.Task UpdateAsync(Task task);
        System.Threading.Tasks.Task DeleteAsync(int id);
    }
}
