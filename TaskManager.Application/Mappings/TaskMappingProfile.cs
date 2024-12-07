using AutoMapper;
using TaskManager.Core.Entities;
using TaskManager.Application.DTOs;

namespace TaskManager.Application.Mappings
{
    public class TaskMappingProfile : Profile
    {
        public TaskMappingProfile()
        {
            CreateMap<Core.Entities.Task, TaskDTO>().ReverseMap();
        }
    }
}
