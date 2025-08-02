using Microsoft.AspNetCore.Mvc;
using CarWorkshopAPI.Data;
using CarWorkshopAPI.Models;
using Microsoft.EntityFrameworkCore;
using CarWorkshopAPI.DTO;

namespace CarWorkshopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskLineController : ControllerBase
    {
        private readonly CarServiceSystemContext _context;
        public TaskLineController(CarServiceSystemContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<TaskLineDto>>> GetAllTaskLines()
        {
            var taskLines = await _context.TaskLines
                .Select(tl => new TaskLineDto
                {
                    TaskLineId = tl.TaskLineId,
                    TaskId = tl.TaskId,
                    InventoryId = tl.InventoryId,
                    EmployeeId = tl.EmployeeId,
                    TaskLineDescription = tl.TaskLineDescription,
                    UnitPrice = tl.UnitPrice,
                    Quantity = tl.Quantity,
                    LineTotal = tl.LineTotal,
                }).ToListAsync();
            return Ok(taskLines);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskLineDto>> GetOneTaskLine(int id)
        {
            var taskLine = await _context.TaskLines.FindAsync(id);
            if (taskLine == null)
                return NotFound();
            var dto = new TaskLineDto
            {
                TaskLineId = taskLine.TaskLineId,
                TaskId = taskLine.TaskId,
                InventoryId = taskLine.InventoryId,
                EmployeeId = taskLine.EmployeeId,
                TaskLineDescription = taskLine.TaskLineDescription,
                UnitPrice = taskLine.UnitPrice,
                Quantity = taskLine.Quantity,
                LineTotal = taskLine.LineTotal,
            };
            return Ok(dto);
        }
        [HttpPost]
        public async Task<ActionResult<TaskLineDto>> CreateNewTaskLine(TaskLineDto newTaskLineDto)
        {
            if (newTaskLineDto == null)
                return BadRequest();
            var taskLine = new TaskLine
            {
                TaskId = newTaskLineDto.TaskId,
                InventoryId = newTaskLineDto.InventoryId,
                EmployeeId = newTaskLineDto.EmployeeId,
                TaskLineDescription = newTaskLineDto.TaskLineDescription,
                UnitPrice = newTaskLineDto.UnitPrice,
                Quantity = newTaskLineDto.Quantity,
                LineTotal = newTaskLineDto.LineTotal
            };
            _context.TaskLines.Add(taskLine);
            await _context.SaveChangesAsync();
            var returnedTaskLineDto = new TaskLineDto
            {
                TaskLineId = taskLine.TaskLineId,
                TaskId = taskLine.TaskId,
                InventoryId = taskLine.InventoryId,
                EmployeeId = taskLine.EmployeeId,
                TaskLineDescription = taskLine.TaskLineDescription,
                UnitPrice = taskLine.UnitPrice,
                Quantity = taskLine.Quantity,
                LineTotal = taskLine.LineTotal
            };
            return CreatedAtAction(nameof(GetOneTaskLine), new { id = returnedTaskLineDto.TaskLineId }, returnedTaskLineDto);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTaskLine(int id, TaskLineDto updatedTaskLineDto)
        {
            if (id != updatedTaskLineDto.TaskLineId)
                return BadRequest("ID in URL does not match ID in the body.");

            var taskLine = await _context.TaskLines.FindAsync(id);
            if (taskLine == null)
                return NotFound();
            taskLine.TaskId = updatedTaskLineDto.TaskId;
            taskLine.InventoryId = updatedTaskLineDto.InventoryId;
            taskLine.EmployeeId = updatedTaskLineDto.EmployeeId;
            taskLine.TaskLineDescription = updatedTaskLineDto.TaskLineDescription;
            taskLine.UnitPrice = updatedTaskLineDto.UnitPrice;
            taskLine.Quantity = updatedTaskLineDto.Quantity;
            taskLine.LineTotal = updatedTaskLineDto.LineTotal;

            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaskLine(int id)
        {
            var taskLine = await _context.TaskLines.FindAsync(id);
            if (taskLine == null)
                return NotFound();
            _context.TaskLines.Remove(taskLine);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
