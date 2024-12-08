using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.Interfaces;
using TaskManager.Application.DTOs;
using Swashbuckle.AspNetCore.Annotations; // Swagger açıklamaları için

namespace TaskManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Create a new task", Description = "Creates a new task in the system.")]
        public async Task<IActionResult> CreateTask([FromBody] TaskDTO taskDTO)
        {
            await _taskService.AddTaskAsync(taskDTO);
            return Ok();
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get task by ID", Description = "Retrieves a task's details using its ID.")]
        public async Task<IActionResult> GetTaskById(int id)
        {
            var task = await _taskService.GetTaskByIdAsync(id);
            if (task == null) return NotFound();
            return Ok(task);
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Get all tasks", Description = "Retrieves a paginated list of all tasks.")]
        public async Task<IActionResult> GetAllTasks([FromQuery] int page, [FromQuery] int pageSize)
        {
            var tasks = await _taskService.GetAllTasksAsync(page, pageSize);
            return Ok(tasks);
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Update an existing task", Description = "Updates the details of an existing task by ID.")]
        public async Task<IActionResult> UpdateTask(int id, [FromBody] TaskDTO taskDTO)
        {
            await _taskService.UpdateTaskAsync(id, taskDTO);
            return Ok();
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete a task", Description = "Removes a task from the system using its ID.")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            await _taskService.DeleteTaskAsync(id);
            return Ok();
        }
    }
}
