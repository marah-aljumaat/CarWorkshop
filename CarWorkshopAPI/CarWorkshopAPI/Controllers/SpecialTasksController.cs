using Microsoft.AspNetCore.Mvc;
using CarWorkshopAPI.Data;
using CarWorkshopAPI.Models;
using Microsoft.EntityFrameworkCore;
using CarWorkshopAPI.DTO;

namespace CarWorkshopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecialTasksController : ControllerBase
    {
        private readonly CarServiceSystemContext _context;
        public SpecialTasksController(CarServiceSystemContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public async Task<ActionResult<Employee>> checkLogin(LoginRequestDto loginRequest)
        {
            var employee = await _context.Employees
            .FirstOrDefaultAsync(e => e.UserName == loginRequest.UserName && e.EmpPassword == loginRequest.EmpPassword);

            if (employee == null)
                return NotFound("Invalid username or password.");

            return Ok(employee);
        }

        [HttpGet("attendance")]
        public async Task<ActionResult<List<AttendanceDto>>> GetAllAttendances()
        {
            var attendances = await _context.Employees
                .Select(e => new AttendanceDto
                {
                    EmployeeId = e.EmployeeId,
                    EmployeeName = e.EmployeeName,
                    AttendanceStatus = e.AttendanceStatus,
                    AttendanceDate = e.AttendanceDate
                }).ToListAsync();
            return Ok(attendances);
        }

        [HttpGet("attendance/{id}")]
        public async Task<ActionResult<AttendanceDto>> GetAttendance(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
                return NotFound();

            var dto = new AttendanceDto
            {
                EmployeeId = employee.EmployeeId,
                EmployeeName = employee.EmployeeName,
                AttendanceStatus = employee.AttendanceStatus,
                AttendanceDate = employee.AttendanceDate
            };

            return Ok(dto);
        }

        //تسجيل الحضور
        [HttpPost("{id}/attendance")]
        public async Task<IActionResult> PostAttendance(int id, AttendanceDto attendanceDto)
        {
            if (id != attendanceDto.EmployeeId)
                return BadRequest("Invalid attendance data.");

            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
                return NotFound();

            var today = DateOnly.FromDateTime(DateTime.UtcNow);
            if (employee.AttendanceDate == today && (employee.AttendanceStatus == "Present" || employee.AttendanceStatus == "present"))
            {
                return BadRequest("It has already been marked as present for today.");
            }

            employee.AttendanceStatus = attendanceDto.AttendanceStatus;
            employee.AttendanceDate = attendanceDto.AttendanceDate;
            employee.EmployeeId = attendanceDto.EmployeeId;
            employee.EmployeeName = attendanceDto.EmployeeName;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("{id}/attendance")]
        public async Task<IActionResult> UpdateAttendance(int id, AttendanceDto attendanceDto)
        {
            if (id != attendanceDto.EmployeeId)
                return BadRequest("Invalid attendance data.");
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
                return NotFound();
            employee.AttendanceStatus = attendanceDto.AttendanceStatus;
            employee.AttendanceDate = attendanceDto.AttendanceDate;
            employee.EmployeeId = attendanceDto.EmployeeId;
            employee.EmployeeName = attendanceDto.EmployeeName;
            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}
