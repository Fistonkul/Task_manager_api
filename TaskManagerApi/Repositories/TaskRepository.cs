using Microsoft.EntityFrameworkCore;
using TaskManagerApi.Data;
using TaskManagerApi.Models;

namespace TaskManagerApi.Repositories
{
    public class TaskRepository: ITaskRepository
    {
        private readonly ApplicationDbContext _context;

        public TaskRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TaskItem>> GetAllAsync()
        {
            return await _context.Tasks.ToListAsync();
        }

        public async Task<TaskItem?> GetByIdAsync(int id)
        {
            return await _context.Tasks.FindAsync(id);
        }

        public void Add(TaskItem task)
        { 
            _context.Tasks.Update(task);
        }

        public void Update(TaskItem task)
        {
            _context.Tasks.Update(task);
        }

        public void Delete(TaskItem task)
        {
            _context.Tasks.Remove(task);
        }
        public async Task<IEnumerable<TaskItem>> FilterTasksAsync(string? status, string? priority, string? search)
        {
            var query = _context.Tasks.AsQueryable();

            if (!string.IsNullOrWhiteSpace(status))
                query = query.Where(t => t.Status == status);

            if (!string.IsNullOrWhiteSpace(priority))
                query = query.Where(t => t.Priority == priority);

            if (!string.IsNullOrWhiteSpace(search))
                query = query.Where(t => 
                    t.Title.Contains(search) || 
                    (t.Description != null && t.Description.Contains(search)));

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<TaskItem>> SortTasksAsync(string orderBy, string direction)
        {
            var query = _context.Tasks.AsQueryable();

            bool descending = direction.Equals("desc", StringComparison.CurrentCultureIgnoreCase);

            switch (orderBy.ToLower())
            {
                case "title":
                    query = descending ? query.OrderByDescending(t => t.Title) : query.OrderBy(t => t.Title);
                    break;

                case "duedate":
                    query = descending ? query.OrderByDescending(t => t.DueDate) : query.OrderBy(t => t.DueDate);
                    break;

                case "priority":
                    query = descending ? query.OrderByDescending(t => t.Priority) : query.OrderBy(t => t.Priority);
                    break;

                case "status":
                    query = descending ? query.OrderByDescending(t => t.Status) : query.OrderBy(t => t.Status);
                    break;

                default:
                    // fallback: sort by ID
                    query = descending ? query.OrderByDescending(t => t.Id) : query.OrderBy(t => t.Id);
                    break;
            }

            return await query.ToListAsync();
        }
        public async Task<PagedResult<TaskItem>> GetPagedTasksAsync(int pageNumber, int pageSize)
        {
            var query = _context.Tasks.AsQueryable();

            int totalCount = await query.CountAsync();

            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<TaskItem>
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }


        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
