using TaskManagerApi.Models;

namespace TaskManagerApi.Repositories
{
    public interface ITaskRepository
    {
        Task<IEnumerable<TaskItem>> GetAllAsync();
        Task<TaskItem?> GetByIdAsync(int id);
        Task Add(TaskItem task);
        void Update(TaskItem task);
        void Delete(TaskItem task);
        Task<IEnumerable<TaskItem>> FilterTasksAsync(string? status, string? priority, string? search);
        Task<IEnumerable<TaskItem>> SortTasksAsync(string orderBy, string direction);
        Task<PagedResult<TaskItem>> GetPagedTasksAsync(int pageNumber, int pageSize);

        Task SaveChangesAsync();
    }
}
