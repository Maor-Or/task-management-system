
using TaskManagement.Application.Exceptions;
using TaskManagement.Application.Interfaces.Repositories;

namespace TaskManagement.Application.Services
{
    public class DeleteTaskService
    {
        private readonly ITaskRepository _taskRepository;

    public DeleteTaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task DeleteAsync(Guid taskId, Guid userId)
        {
            var task = await _taskRepository.GetByIdAsync(taskId);

            if (task == null)
            {
                throw new TaskNotFoundException();
            }

            if (task.UserId !=  userId)
            {
                throw new ForbiddenTaskAccessException();
            }

            await _taskRepository.DeleteAsync(task);
        }
    }
}
