using TaskManagement.Domain.Entities;

namespace TaskManagement.Application.Interfaces.Repositories
{
    public interface ITaskRepository
    {
        Task<List<TaskItem>> GetByUserIdAsync(Guid userId);
        Task<TaskItem?> GetByIdAsync(Guid id);
        Task AddAsync(TaskItem item);
        Task UpdateAsync(TaskItem item);
        Task DeleteAsync(TaskItem item);
        Task<(List<TaskItem> items, int totalCount)> GetPagedbyUserIdAsync(
            Guid userId,
            int page,
            int pageSize);
    }
}
