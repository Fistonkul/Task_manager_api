using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagerApi.Data;
using TaskManagerApi.Models;
using TaskManagerApi.Services;

namespace TaskManagerApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _service;

        public TasksController(ITaskService service)
        {
            _service = service;

        }

        // GET: api/tasks
        [HttpGet]
        public async Task<IActionResult> GetTasks()
        {
            var tasks = await _service.GetAllTasksAsync();
            return Ok(tasks);
        }

        // GET: api/tasks/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTask(int id)
        {
            var task = await _service.GetTaskByIdAsync(id);
            if (task == null) return NotFound();
            return Ok(task);
        }

        // POST: api/tasks
        [HttpPost]
        public async Task<IActionResult> CreateTask(TaskItem task)
        {
            var created = await _service.CreateTaskAsync(task);
            return CreatedAtAction(nameof(GetTask), new { id = task.Id }, task);
        }

        // PUT: api/tasks/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, TaskItem updatedTask)
        {
            var result = await _service.UpdateTaskAsync(id, updatedTask);
            return result == null ? NotFound() : Ok(result);
        }

        // GET: api/tasks/filter
        [HttpGet("filter")]
        public async Task<IActionResult> FilterTasks([FromQuery] string? status, [FromQuery] string? priority, [FromQuery] string? search)
        {
            var results = await _service.FilterTasksAsync(status, priority, search);
            return Ok(results);
        }

        // GET: api/tasks/sort
        [HttpGet("sort")]
        public async Task<IActionResult> SortTasks([FromQuery] string orderBy = "Id", [FromQuery] string direction = "asc")
        {
            var results = await _service.SortTasksAsync(orderBy, direction);
            return Ok(results);
        }


        // GET: api/tasks/paged
        [HttpGet("paged")]
        public async Task<IActionResult> GetPagedTasks([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _service.GetPagedTasksAsync(pageNumber, pageSize);
            return Ok(result);
        }



        // DELETE: api/tasks/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var deleted = await _service.DeleteTaskAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}