using TaskManagement.Application.Interfaces.Repositories;
using TaskManagement.Application.Exceptions;
using Microsoft.Extensions.Logging;

namespace TaskManagement.Application.Services
{
    public class CompleteTaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly ILogger<CompleteTaskService> _logger;
    
        public CompleteTaskService(ITaskRepository repository, ILogger<CompleteTaskService> logger)
        {
            _taskRepository = repository;
            _logger = logger;
        }

        public async Task CompleteAsync(Guid taskId, Guid userId)
        {
            _logger.LogInformation("Attempting to complete task with Id: {taskId}, with userId: {userId}", taskId, userId);
            var task = await _taskRepository.GetByIdAsync(taskId);
            if (task == null)
            {
                _logger.LogWarning("Could not find task: {taskId}", taskId);
                throw new TaskNotFoundException();
            }

            if (task.UserId != userId)
            {
                _logger.LogWarning(
                "Access denied. UserId: {UserId} tried to access Task owned by UserId: {TaskUserId}",
                userId,
                task.UserId);
                throw new ForbiddenTaskAccessException();
            }

            task.IsCompleted = true;
             await _taskRepository.UpdateAsync(task);

            return;
        }
    }
}
