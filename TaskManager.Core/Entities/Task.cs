using TaskStatus = TaskManager.Core.Enums.TaskStatus;
namespace TaskManager.Core.Entities
{
    public class Task
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public TaskStatus Status { get; set; }
    }
}
