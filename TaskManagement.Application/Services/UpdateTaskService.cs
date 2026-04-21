using Microsoft.Extensions.Logging;
using TaskManagement.Application.DTOs;
using TaskManagement.Application.Exceptions;
using TaskManagement.Application.Interfaces.Repositories;
using TaskManagement.Domain.Enums;

namespace TaskManagement.Application.Services
{
    public class UpdateTaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly ILogger<UpdateTaskService> _logger;

        public UpdateTaskService(ITaskRepository taskRepository, ILogger<UpdateTaskService> logger)
        {
            _taskRepository = taskRepository;
            _logger = logger;
        }

        public async Task UpdateAsync(Guid taskId, Guid userId, UpdateTaskDto dto)
        {
            _logger.LogInformation("Updating task: {taskId}", taskId);

            var task = await _taskRepository.GetByIdAsync(taskId);

            if (task == null)
            {
                _logger.LogWarning("Could not find task: {taskId}", taskId);
                throw new Exceptions.TaskNotFoundException();
            }

            if(task.UserId != userId)
            {
                _logger.LogWarning(
                    "Access denied. UserId: {UserId} tried to access Task owned by UserId: {TaskUserId}",
                    userId,
                    task.UserId);
                throw new Exceptions.ForbiddenTaskAccessException();
            }

            task.Title = dto.Title;
            task.Description = dto.Description;
            task.DueDate = dto.DueDate;
            task.Priority = (Priority)dto.Priority;

            await _taskRepository.UpdateAsync(task);

            _logger.LogInformation("Task with taskId: {TaskId} updated successfully by User {UserId}", task.Id, task.UserId);
        }
    }
}
