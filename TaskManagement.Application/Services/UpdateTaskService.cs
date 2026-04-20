using TaskManagement.Application.DTOs;
using TaskManagement.Application.Exceptions;
using TaskManagement.Application.Interfaces.Repositories;
using TaskManagement.Domain.Enums;

namespace TaskManagement.Application.Services
{
    public class UpdateTaskService
    {
        private readonly ITaskRepository _taskRepository;

        public UpdateTaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task UpdateAsync(Guid taskId, Guid userId, UpdateTaskDto dto)
        {
            var task = await _taskRepository.GetByIdAsync(taskId);

            if (task == null)
            {
                throw new Exceptions.TaskNotFoundException();
            }

            if(task.UserId != userId)
            {
                throw new Exceptions.ForbiddenTaskAccessException();
            }

            task.Title = dto.Title;
            task.Description = dto.Description;
            task.DueDate = dto.DueDate;
            task.Priority = (Priority)dto.Priority;

            await _taskRepository.UpdateAsync(task);
        }
    }
}
