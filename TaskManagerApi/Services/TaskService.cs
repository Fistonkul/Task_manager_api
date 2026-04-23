using TaskManagerApi.Dtos;
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

        public async Task<TaskDto?> GetTaskByIdAsync(int id)
        {
            var task =  await _repository.GetByIdAsync(id);

            if (task == null)
                return null;

            return new TaskDto
            {
                Id = task.Id,
                Title = task.Title,
                Status = task.Status,
            };
        }

        public async Task<TaskDto> CreateTaskAsync(TaskDto taskDto)
        {
            //validation
            if (string.IsNullOrWhiteSpace(taskDto.Title))
                throw new ArgumentNullException("Title is required");

            // Map DTO → Entity
            var task = new TaskItem
            {
                Title = taskDto.Title,
                Description = taskDto.Description,
                DueDate = taskDto.DueDate,
                Priority = taskDto.Priority,
                Status = "Pending"
            };

            // Data access
            await _repository.Add(task);
            await _repository.SaveChangesAsync();

            // Map Entity → DTO (return)
            return new TaskDto
            {
                Id = task.Id,
                Title = task.Title,
                Status = task.Status
            };
        }

        public async Task<TaskDto?> UpdateTaskAsync(int id, TaskDto updatedTask)
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

            // Map to DTO before returning
            return new TaskDto
            {
                Id = existing.Id,
                Title = existing.Title,
                Description = existing.Description,
                DueDate = existing.DueDate,
                Priority = existing.Priority,
                Status = existing.Status
            };
        }

        public async Task<IEnumerable<TaskDto>> FilterTasksAsync(string? status, string? priority, string? search)
        {
            var tasks = await _repository.FilterTasksAsync(status, priority, search);

            return tasks.Select(t => new TaskDto
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                DueDate = t.DueDate,
                Priority = t.Priority,
                Status = t.Status
            });
        }        

        public async Task<IEnumerable<TaskDto>> SortTasksAsync(string orderBy, string direction)
        {
            var tasks = await _repository.SortTasksAsync(orderBy, direction);

            return tasks.Select(t => new TaskDto
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                DueDate = t.DueDate,
                Priority = t.Priority,
                Status = t.Status
            });
        }        
        public async Task<PagedResult<TaskDto>> GetPagedTasksAsync(int pageNumber, int pageSize)
        {
            var result = await _repository.GetPagedTasksAsync(pageNumber, pageSize);

            return new PagedResult<TaskDto>
            {
                Items = result.Items.Select(t => new TaskDto
                {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    DueDate = t.DueDate,
                    Priority = t.Priority,
                    Status = t.Status
                }),
                TotalCount = result.TotalCount,
                PageNumber = result.PageNumber,
                PageSize = result.PageSize
            };
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
