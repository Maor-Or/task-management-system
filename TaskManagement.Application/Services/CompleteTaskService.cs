using TaskManagement.Application.Interfaces.Repositories;
using TaskManagement.Application.Exceptions;

namespace TaskManagement.Application.Services
{
    public class CompleteTaskService
    {
        private readonly ITaskRepository _taskRepository;
    
        public CompleteTaskService(ITaskRepository repository)
        {
            _taskRepository = repository;
        }

        public async Task CompleteAsync(Guid taskId, Guid userId)
        {
            var task = await _taskRepository.GetByIdAsync(taskId);
            if (task == null)
            {
                throw new TaskNotFoundException();

            }

            if (task.UserId != userId)
            {
                throw new ForbiddenTaskAccessException();
            }

            task.IsCompleted = true;
             await _taskRepository.UpdateAsync(task);

            return;
        }
    }
}
