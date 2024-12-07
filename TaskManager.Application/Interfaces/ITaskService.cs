using TaskManager.Application.DTOs;

namespace TaskManager.Application.Interfaces
{
    public interface ITaskService
    {
        Task AddTaskAsync(TaskDTO taskDTO);
        Task<TaskDTO> GetTaskByIdAsync(int id);
        Task<List<TaskDTO>> GetAllTasksAsync(int page, int pageSize);
        Task UpdateTaskAsync(int id, TaskDTO taskDTO);
        Task DeleteTaskAsync(int id);
    }
}
