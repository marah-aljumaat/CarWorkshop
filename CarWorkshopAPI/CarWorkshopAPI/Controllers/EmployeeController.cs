using Microsoft.AspNetCore.Mvc;
using CarWorkshopAPI.Data;
using CarWorkshopAPI.Models;
using Microsoft.EntityFrameworkCore;
using CarWorkshopAPI.DTO;

namespace CarWorkshopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly CarServiceSystemContext _context;

        public EmployeeController(CarServiceSystemContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<EmployeeDto>>> GetAllEmployees()
        {
            var employees = await _context.Employees
                .Select(e => new EmployeeDto
                {
                    EmployeeId = e.EmployeeId,
                    EmployeeName = e.EmployeeName,
                    JobTitle = e.JobTitle,
                    Specialty = e.Specialty,
                    CommissionRate = e.CommissionRate,
                    UserName = e.UserName,
                    AttendanceStatus = e.AttendanceStatus,
                    AttendanceDate = e.AttendanceDate
                }).ToListAsync();

            return Ok(employees);
        }
        [HttpPost("login")]
        public async Task <ActionResult<Employee>> checkLogin(LoginRequestDto loginRequest)
        {
            var employee = await _context.Employees
            .FirstOrDefaultAsync(e => e.UserName == loginRequest.UserName && e.EmpPassword == loginRequest.EmpPassword);

            if (employee == null)
                return NotFound("Invalid username or password.");

            return Ok(employee);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDto>> GetOneEmployee(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
                return NotFound();

            var dto = new EmployeeDto
            {
                EmployeeId = employee.EmployeeId,
                EmployeeName = employee.EmployeeName,
                JobTitle = employee.JobTitle,
                Specialty = employee.Specialty,
                CommissionRate = employee.CommissionRate,
                UserName = employee.UserName,
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
            if (employee.AttendanceDate == today && (employee.AttendanceStatus == "Present" || employee.AttendanceStatus == "present") )
            {
                return BadRequest("It has already been marked as present for today.");
            }
            employee.AttendanceStatus = attendanceDto.AttendanceStatus;
            employee.AttendanceDate = attendanceDto.AttendanceDate;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<EmployeeDto>> CreateNewEmployee(EmployeeDto newEmployeeDto)
        {
            if (newEmployeeDto == null)
                return BadRequest();

            var newEmployee = new Employee
            {
                EmployeeName = newEmployeeDto.EmployeeName,
                JobTitle = newEmployeeDto.JobTitle,
                Specialty = newEmployeeDto.Specialty,
                CommissionRate = newEmployeeDto.CommissionRate,
                UserName = newEmployeeDto.UserName
            };
            _context.Employees.Add(newEmployee);
            await _context.SaveChangesAsync();

            var returnedEmployeeDto = new EmployeeDto
            {
                EmployeeId = newEmployee.EmployeeId,
                EmployeeName = newEmployee.EmployeeName,
                JobTitle = newEmployee.JobTitle,
                Specialty = newEmployee.Specialty,
                CommissionRate = newEmployee.CommissionRate,
                UserName = newEmployee.UserName
            };
            return CreatedAtAction(nameof(GetOneEmployee), new { id = returnedEmployeeDto.EmployeeId }, returnedEmployeeDto);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, EmployeeDto updatedEmployeeDto)
        {
            if (id != updatedEmployeeDto.EmployeeId)
                return BadRequest("ID in URL does not match ID in the body.");

            var existingEmployee = await _context.Employees.FindAsync(id);
            if (existingEmployee == null)
                return NotFound();

            existingEmployee.EmployeeName = updatedEmployeeDto.EmployeeName;
            existingEmployee.JobTitle = updatedEmployeeDto.JobTitle;
            existingEmployee.Specialty = updatedEmployeeDto.Specialty;
            existingEmployee.CommissionRate = updatedEmployeeDto.CommissionRate;
            existingEmployee.UserName = updatedEmployeeDto.UserName;
            existingEmployee.AttendanceStatus = updatedEmployeeDto.AttendanceStatus;
            existingEmployee.AttendanceDate = updatedEmployeeDto.AttendanceDate;

            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
                return NotFound();
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return NoContent();

        }
    }
}
