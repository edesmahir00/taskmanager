using AutoMapper;
using TaskManager.Application.DTOs;
using TaskManager.Application.Interfaces;
using TaskManager.Core.Entities;
using TaskManager.Core.Interfaces;
using SystemTask = System.Threading.Tasks.Task;
using Task = TaskManager.Core.Entities.Task;

namespace TaskManager.Application.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IMapper _mapper;

        public TaskService(ITaskRepository taskRepository, IMapper mapper)
        {
            _taskRepository = taskRepository;
            _mapper = mapper;
        }

        public async SystemTask AddTaskAsync(TaskDTO taskDTO)
        {
            var task = _mapper.Map<Task>(taskDTO);
            await _taskRepository.AddAsync(task);
        }

        public async Task<TaskDTO> GetTaskByIdAsync(int id)
        {
            var task = await _taskRepository.GetByIdAsync(id);
            return _mapper.Map<TaskDTO>(task);
        }

        public async Task<List<TaskDTO>> GetAllTasksAsync(int page, int pageSize)
        {
            var tasks = await _taskRepository.GetAllAsync(page, pageSize);
            return _mapper.Map<List<TaskDTO>>(tasks);
        }

        public async SystemTask UpdateTaskAsync(int id, TaskDTO taskDTO)
        {
            var task = await _taskRepository.GetByIdAsync(id);
            if (task == null) throw new Exception("Task not found");
            _mapper.Map(taskDTO, task);
            await _taskRepository.UpdateAsync(task);
        }

        public async SystemTask DeleteTaskAsync(int id)
        {
            await _taskRepository.DeleteAsync(id);
        }
    }
}
