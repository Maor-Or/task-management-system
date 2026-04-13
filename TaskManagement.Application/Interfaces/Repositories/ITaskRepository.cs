using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Application.Interfaces.Repositories
{
    public interface ITaskRepository
    {
        Task<List<TaskItem>> GetByUserIdAsync(Guid userId);
        Task<TaskItem> GetByIdAsync(Guid id);
        Task AddAsync(TaskItem item);
        Task UpdateAsync(TaskItem item);
        Task DeleteAsync(TaskItem item);
    }
}
