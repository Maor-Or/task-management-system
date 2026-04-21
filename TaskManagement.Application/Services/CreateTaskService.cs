using Microsoft.Extensions.Logging;
using TaskManagement.Application.DTOs;
using TaskManagement.Application.Interfaces.Repositories;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Enums;


namespace TaskManagement.Application.Services
{
    public class CreateTaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly ILogger<CreateTaskService> _logger;


        public CreateTaskService(ITaskRepository taskRepository, ILogger<CreateTaskService> logger)
        {
            _taskRepository = taskRepository;
            _logger = logger;
        }
        
        public async Task<TaskDto> CreateAsync(CreateTaskDto dto, Guid userId)
        {
            _logger.LogInformation("Creating task for user {userId}", userId);
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

            _logger.LogInformation("Task created successfully with Id {taskId}", task.Id);

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
