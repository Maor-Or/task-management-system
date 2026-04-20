using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using TaskManagement.Application.Interfaces.Repositories;
using TaskManagement.Domain.Entities;
using TaskManagement.Infrastructure.Persistence;

namespace TaskManagement.Infrastructure.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly AppDbContext _context;

        public TaskRepository(AppDbContext context)
        {
            _context = context;
        }
        
        public async Task AddAsync(TaskItem item)
        {
            await _context.Tasks.AddAsync(item);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(TaskItem item)
        {
            _context.Tasks.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task<TaskItem?> GetByIdAsync(Guid id)
        {
            return await _context.Tasks.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<TaskItem>> GetByUserIdAsync(Guid userId)
        {
            return await _context.Tasks.Where(x => x.UserId == userId).ToListAsync();
        }

        public async Task UpdateAsync(TaskItem item)
        {
            _context.Tasks.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task<(List<TaskItem> items, int totalCount)> GetPagedbyUserIdAsync(Guid userId, int page, int pageSize, bool? isCompleted, int? priority, string? sortBy)
        {
            var query = _context.Tasks.Where(t => t.UserId == userId);

            //filtering
            if (isCompleted.HasValue)
            {
                query = query.Where(t => t.IsCompleted == isCompleted.Value);
            }
            
            if (priority.HasValue)
            {
                query = query.Where(t => (int)t.Priority == priority.Value);
            }

            //sorting

            if (sortBy != null)
            {
                query = sortBy switch
                {
                    "dueDate" =>
                        query.OrderBy(t => t.DueDate),
                    "priority" =>
                        query.OrderBy(t => t.Priority),
                    _ =>
                        query.OrderBy(t => t.DueDate)
                };
            }

            var totalCount = await query.CountAsync();

            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, totalCount);
        }
    }
}
