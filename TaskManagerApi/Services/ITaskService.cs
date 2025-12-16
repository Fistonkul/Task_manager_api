using TaskManagerApi.Models;

namespace TaskManagerApi.Services
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskItem>> GetAllTasksAsync();
        Task<TaskItem?> GetTaskByIdAsync(int id);
        Task<TaskItem> CreateTaskAsync(TaskItem task);
        Task<TaskItem?> UpdateTaskAsync(int id, TaskItem updatedTask);
        Task<IEnumerable<TaskItem>> FilterTasksAsync(string? status, string? priority, string? search);
        Task<IEnumerable<TaskItem>> SortTasksAsync(string orderBy, string direction);
        Task<PagedResult<TaskItem>> GetPagedTasksAsync(int pageNumber, int pageSize);

        Task<bool> DeleteTaskAsync(int id);
    }
}
