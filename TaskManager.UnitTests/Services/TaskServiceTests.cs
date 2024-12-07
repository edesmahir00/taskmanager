using Moq;
using TaskManager.Application.Interfaces;
using TaskManager.Application.Services;
using TaskManager.Application.DTOs;
using TaskManager.Core.Entities;
using Xunit;
using TaskManager.Core.Interfaces;
using TaskManager.Application.Mappings;

namespace TaskManager.UnitTests.Services
{
    public class TaskServiceTests
    {
        private readonly Mock<ITaskRepository> _mockTaskRepository;
        private readonly ITaskService _taskService;

        public TaskServiceTests()
        {
            _mockTaskRepository = new Mock<ITaskRepository>();
            _taskService = new TaskService(_mockTaskRepository.Object, new AutoMapper.MapperConfiguration(cfg => cfg.AddProfile(new TaskMappingProfile())).CreateMapper());
        }

        [Fact]
        public async System.Threading.Tasks.Task AddTaskAsync_ShouldCallRepositoryAddOnce()
        {
            var taskDTO = new TaskDTO
            {
                Title = "Test Task",
                Description = "Test Description",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(1),
                Status = TaskStatus.Created
            };

            await _taskService.AddTaskAsync(taskDTO);

            _mockTaskRepository.Verify(x => x.AddAsync(It.IsAny<Core.Entities.Task>()), Times.Once);
        }
    }
}
