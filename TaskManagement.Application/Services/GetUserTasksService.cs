using System.Linq;
using TaskManagement.Application.DTOs;
using TaskManagement.Application.Interfaces.Repositories;

namespace TaskManagement.Application.Services
{
    public class GetUserTasksService
    {
        private readonly ITaskRepository _taskRepository;

        public GetUserTasksService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<List<TaskDto>> GetAsync(Guid userId)
        {
            var tasks = await _taskRepository.GetByUserIdAsync(userId);

            return tasks.Select(t => new TaskDto
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                DueDate = t.DueDate,
                IsCompleted = t.IsCompleted,
                Priority = (int)t.Priority
            }
            ).ToList();
        }

        public async Task<PagedResult<TaskDto>> GetPagedAsync(
            Guid userId,
            int page,
            int pageSize)
        {
            var (items, totalCount) = await _taskRepository.GetPagedbyUserIdAsync(userId, page, pageSize);

            return new PagedResult<TaskDto>
            {
                Items = items.Select(t => new TaskDto
                {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    DueDate = t.DueDate,
                    IsCompleted = t.IsCompleted,
                    Priority = (int)t.Priority
                }) .ToList(),
                
                TotalCount = totalCount,
                Page = page,
                PageSize = pageSize
            };
        }
    }
}
