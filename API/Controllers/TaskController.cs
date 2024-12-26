using DBModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITask _repository;

        public TaskController(ITask repository)
        {
            _repository = repository;
        }

        // GET: api/Task`
        [HttpGet]
        public IActionResult GetTasks()
        {
            var tasks = _repository.GetAllTasks();
            if (tasks == null || !tasks.Any())
                return NoContent(); // 204 No Content
            return Ok(tasks); // 200 OK
        }

        // GET: api/Task/{id}
        [HttpGet("{id}")]
        public IActionResult GetTask(int id)
        {
            var task = _repository.GetTaskById(id);
            if (task == null)
                return NotFound(); // 404 Not Found
            return Ok(task); // 200 OK
        }

        // POST: api/Task
        [HttpPost]
        public IActionResult CreateTask([FromBody] TaskDto taskDto)
        {
            if (taskDto == null)
                return BadRequest("Task data cannot be null."); // 400 Bad Request

            _repository.AddTask(taskDto);
            return CreatedAtAction(nameof(GetTask), new { id = taskDto.Id }, taskDto); // 201 Created
        }

        // PUT: api/Task/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateTask(int id, [FromBody] TaskDto taskDto)
        {
            if (taskDto == null || taskDto.Id != id)
                return BadRequest("Task ID mismatch."); // 400 Bad Request

            if (_repository.GetTaskById(id) == null)
                return NotFound("Task not found."); // 404 Not Found

            _repository.UpdateTask(taskDto);
            return NoContent(); // 204 No Content
        }

        // DELETE: api/Task/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteTask(int id)
        {
            if (_repository.GetTaskById(id) == null)
                return NotFound("Task not found."); // 404 Not Found

            _repository.DeleteTask(id);
            return NoContent(); // 204 No Content
        }
    }
}
