using Microsoft.AspNetCore.Mvc;
using CarWorkshopAPI.Data;
using CarWorkshopAPI.Models;
using Microsoft.EntityFrameworkCore;
using CarWorkshopAPI.DTO;

namespace CarWorkshopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly CarServiceSystemContext _context;
        public ProjectController(CarServiceSystemContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<ProjectDto>>> GetAllProjects()
        {
            var projects = await _context.Projects
                .Select(p => new ProjectDto
                {
                    ProjectId = p.ProjectId,
                    CustomerId = p.CustomerId,
                    ProjectName = p.ProjectName,
                    ProjectDescription = p.ProjectDescription,
                    ProjectStartDate = p.ProjectStartDate,
                    ProjectEndDate = p.ProjectEndDate,
                    ProjectStatus = p.ProjectStatus
                }).ToListAsync();
            return Ok(projects);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectDto>> GetOneProject(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null)
                return NotFound();

            var dto = new ProjectDto
            {
                ProjectId = project.ProjectId,
                CustomerId = project.CustomerId,
                ProjectName = project.ProjectName,
                ProjectDescription = project.ProjectDescription,
                ProjectStartDate = project.ProjectStartDate,
                ProjectEndDate = project.ProjectEndDate,
                ProjectStatus = project.ProjectStatus
            };
            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<ProjectDto>> CreateNewProject(ProjectDto newProjectDto)
        {
            if (newProjectDto == null)
                return BadRequest();
            var project = new Project
            {
                CustomerId = newProjectDto.CustomerId,
                ProjectName = newProjectDto.ProjectName,
                ProjectDescription = newProjectDto.ProjectDescription,
                ProjectStartDate = newProjectDto.ProjectStartDate,
                ProjectEndDate = newProjectDto.ProjectEndDate,
                ProjectStatus = newProjectDto.ProjectStatus
            };
            _context.Projects.Add(project);
            await _context.SaveChangesAsync();

            var returnedProject = new ProjectDto
            {
                ProjectId = project.ProjectId,
                CustomerId = project.CustomerId,
                ProjectName = project.ProjectName,
                ProjectDescription = project.ProjectDescription,
                ProjectStartDate = project.ProjectStartDate,
                ProjectEndDate = project.ProjectEndDate,
                ProjectStatus = project.ProjectStatus
            };
            return CreatedAtAction(nameof(GetOneProject), new { id = returnedProject.ProjectId }, returnedProject);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProject(int id, ProjectDto updatedProjectDto)
        {
            if (id != updatedProjectDto.ProjectId)
                return BadRequest("ID in URL does not match ID in the body.");

            var project = await _context.Projects.FindAsync(id);
            if (project == null)
                return NotFound();

            project.CustomerId = updatedProjectDto.CustomerId;
            project.ProjectName = updatedProjectDto.ProjectName;
            project.ProjectDescription = updatedProjectDto.ProjectDescription;
            project.ProjectStartDate = updatedProjectDto.ProjectStartDate;
            project.ProjectEndDate = updatedProjectDto.ProjectEndDate;
            project.ProjectStatus = updatedProjectDto.ProjectStatus;

            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null)
                return NotFound();
            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
