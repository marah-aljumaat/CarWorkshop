using Microsoft.AspNetCore.Mvc;
using CarWorkshopAPI.Data;
using CarWorkshopAPI.Models;
using Microsoft.EntityFrameworkCore;
using CarWorkshopAPI.DTO;

namespace CarWorkshopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly CarServiceSystemContext _context;
        public TaskController(CarServiceSystemContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<TaskDto>>> GetAllTasks()
        {
            var tasks = await _context.Tasks
                .Select(t => new TaskDto
                {
                    TaskId = t.TaskId,
                    CustomerId = t.CustomerId,
                    ProjectId = t.ProjectId,
                    CarId = t.CarId,
                    TaskName = t.TaskName,
                    TaskDescription = t.TaskDescription,
                    TaskStartDate = t.TaskStartDate,
                    TaskEndDate = t.TaskEndDate,
                    TaskStatus = t.TaskStatus,
                    CarDeliveredAt = t.CarDeliveredAt,
                    CarReceivedAt = t.CarReceivedAt
                }).ToListAsync();
            return Ok(tasks);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskDto>> GetOneTask(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
                return NotFound();
            var dto = new TaskDto
            {
                TaskId = task.TaskId,
                CustomerId = task.CustomerId,
                ProjectId = task.ProjectId,
                CarId = task.CarId,
                TaskName = task.TaskName,
                TaskDescription = task.TaskDescription,
                TaskStartDate = task.TaskStartDate,
                TaskEndDate = task.TaskEndDate,
                TaskStatus = task.TaskStatus,
                CarDeliveredAt = task.CarDeliveredAt,
                CarReceivedAt = task.CarReceivedAt
            };
            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<TaskDto>> CreateNewTask(TaskDto newTaskDto)
        {
            if (newTaskDto == null)
                return BadRequest();
            var task = new Models.Task
            {
                CustomerId = newTaskDto.CustomerId,
                ProjectId = newTaskDto.ProjectId,
                CarId = newTaskDto.CarId,
                TaskName = newTaskDto.TaskName,
                TaskDescription = newTaskDto.TaskDescription,
                TaskStartDate = newTaskDto.TaskStartDate,
                TaskEndDate = newTaskDto.TaskEndDate,
                TaskStatus = newTaskDto.TaskStatus,
                CarDeliveredAt = newTaskDto.CarDeliveredAt,
                CarReceivedAt = newTaskDto.CarReceivedAt
            };
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();

            var returnedTask = new TaskDto
            {
                CustomerId = task.CustomerId,
                ProjectId = task.ProjectId,
                CarId = task.CarId,
                TaskName = task.TaskName,
                TaskDescription = task.TaskDescription,
                TaskStartDate = task.TaskStartDate,
                TaskEndDate = task.TaskEndDate,
                TaskStatus = task.TaskStatus,
                CarDeliveredAt = task.CarDeliveredAt,
                CarReceivedAt = task.CarReceivedAt
            };
            return CreatedAtAction(nameof(GetOneTask), new { id = returnedTask.TaskId }, returnedTask);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, TaskDto updatedTaskDto)
        {
            if (id != updatedTaskDto.TaskId)
                return BadRequest("ID in URL does not match ID in the body.");

            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
                return NotFound();
            task.CustomerId = updatedTaskDto.CustomerId;
            task.ProjectId = updatedTaskDto.ProjectId;
            task.CarId = updatedTaskDto.CarId;
            task.TaskName = updatedTaskDto.TaskName;
            task.TaskDescription = updatedTaskDto.TaskDescription;
            task.TaskStartDate = updatedTaskDto.TaskStartDate;
            task.TaskEndDate = updatedTaskDto.TaskEndDate;
            task.TaskStatus = updatedTaskDto.TaskStatus;
            task.CarDeliveredAt = updatedTaskDto.CarDeliveredAt;
            task.CarReceivedAt = updatedTaskDto.CarReceivedAt;

            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
                return NotFound();
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
