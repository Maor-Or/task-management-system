
using Microsoft.Extensions.Logging;
using TaskManagement.Application.Exceptions;
using TaskManagement.Application.Interfaces.Repositories;

namespace TaskManagement.Application.Services
{
    public class DeleteTaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly ILogger<DeleteTaskService> _logger;


        public DeleteTaskService(ITaskRepository taskRepository, ILogger<DeleteTaskService> logger)
        {
            _taskRepository = taskRepository;
            _logger = logger;
        }

        public async Task DeleteAsync(Guid taskId, Guid userId)
        {
            _logger.LogInformation("Deleting task: {taskId}", taskId);

            var task = await _taskRepository.GetByIdAsync(taskId);

            if (task == null)
            {
                _logger.LogWarning("Could not find task: {taskId}", taskId);

                throw new TaskNotFoundException();
            }

            if (task.UserId !=  userId)
            {
                _logger.LogWarning(
                "Access denied. UserId: {UserId} tried to access Task owned by UserId: {TaskUserId}",
                userId,
                task.UserId);
                throw new ForbiddenTaskAccessException();
            }

            await _taskRepository.DeleteAsync(task);
            _logger.LogInformation("Task with taskId: {TaskId} successfully deleted by User {UserId}", task.Id, task.UserId);

        }
    }
}
