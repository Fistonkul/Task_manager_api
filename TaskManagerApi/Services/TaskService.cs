using TaskManagerApi.Models;
using TaskManagerApi.Repositories;

namespace TaskManagerApi.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _repository;

        public TaskService(ITaskRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<TaskItem>> GetAllTasksAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<TaskItem?> GetTaskByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<TaskItem> CreateTaskAsync(TaskItem task)
        {
            await _repository.AddAsync(task);
            await _repository.SaveChangesAsync();
            return task;
        }

        public async Task<TaskItem?> UpdateTaskAsync(int id, TaskItem updatedTask)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return null;

            existing.Title = updatedTask.Title;
            existing.Description = updatedTask.Description;
            existing.DueDate = updatedTask.DueDate;
            existing.Priority = updatedTask.Priority;
            existing.Status = updatedTask.Status;

            _repository.Update(existing);
            await _repository.SaveChangesAsync();

            return existing;
        }

        public async Task<IEnumerable<TaskItem>> FilterTasksAsync(string? status, string? priority, string? search)
        {
            return await _repository.FilterTasksAsync(status, priority, search);
        }

        public async Task<IEnumerable<TaskItem>> SortTasksAsync(string orderBy, string direction)
        {
            return await _repository.SortTasksAsync(orderBy, direction);
        }
        public async Task<PagedResult<TaskItem>> GetPagedTasksAsync(int pageNumber, int pageSize)
        {
            return await _repository.GetPagedTasksAsync(pageNumber, pageSize);
        }


        public async Task<bool> DeleteTaskAsync(int id)
        {
            var task = await _repository.GetByIdAsync(id);
            if (task == null) return false;

            _repository.Delete(task);
            await _repository.SaveChangesAsync();

            return true;
        }
    }
}
