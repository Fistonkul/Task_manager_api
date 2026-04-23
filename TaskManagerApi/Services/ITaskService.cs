using TaskManagerApi.Dtos;
using TaskManagerApi.Models;

namespace TaskManagerApi.Services
{
    public interface ITaskService
    {
        Task<PagedResult<TaskDto>> GetPagedTasksAsync(int page, int size);
        Task<TaskDto?> GetTaskByIdAsync(int id);
        Task<TaskDto> CreateTaskAsync(TaskDto task);
        Task<TaskDto?> UpdateTaskAsync(int id, TaskDto updatedTask);
        Task<IEnumerable<TaskDto>> FilterTasksAsync(string? status, string? priority, string? search);
        Task<IEnumerable<TaskDto>> SortTasksAsync(string orderBy, string direction);

        Task<bool> DeleteTaskAsync(int id);
    }
}
