using TaskManagement.Application.DTOs;
using TaskManagement.Application.Interfaces.Repositories;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Enums;


namespace TaskManagement.Application.Services
{
    public class CreateTaskService
    {
        private readonly ITaskRepository _taskRepository;
        
        public CreateTaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }
        
        public async Task<TaskDto> CreateAsync(CreateTaskDto dto, Guid userId)
        {
            var task = new TaskItem
            {
                Id = Guid.NewGuid(),
                Title = dto.Title,
                Description = dto.Description,
                DueDate = dto.DueDate,
                Priority = (Priority)dto.Priority,
                IsCompleted = false,
                UserId = userId
            };

            await _taskRepository.AddAsync(task);

            return new TaskDto
            {
                Id = task.Id,

                Title = task.Title,
                Description = task.Description,
                DueDate = task.DueDate,
                Priority = (int)task.Priority,
                IsCompleted = task.IsCompleted
            };
        }
    }
}
