using Microsoft.AspNetCore.Mvc;
using CarWorkshopAPI.Data;
using CarWorkshopAPI.Models;
using Microsoft.EntityFrameworkCore;
using CarWorkshopAPI.DTO;

namespace CarWorkshopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryGroupController : ControllerBase
    {
        private readonly CarServiceSystemContext _context;
        public InventoryGroupController(CarServiceSystemContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<InventoryGroupDto>>> GetAllInventoryGroups()
        {
            var inventoryGroups = await _context.InventoryGroups
                .Select(g => new InventoryGroupDto
                {
                    GroupId = g.GroupId,
                    GroupName = g.GroupName,
                    GroupDescription = g.GroupDescription
                }).ToListAsync();
            return Ok(inventoryGroups);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<InventoryGroupDto>> GetOneInventoryGroup(int id)
        {
            var inventoryGroup = await _context.InventoryGroups.FindAsync(id);
            if (inventoryGroup == null)
                return NotFound();

            var dto = new InventoryGroupDto
            {
                GroupId = inventoryGroup.GroupId,
                GroupName = inventoryGroup.GroupName,
                GroupDescription = inventoryGroup.GroupDescription
            };
            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<InventoryGroupDto>> CreateNewInventoryGroup(InventoryGroupDto newInventoryGroupDto)
        {
            if (newInventoryGroupDto == null)
                return BadRequest();

            var inventoryGroup = new InventoryGroup
            {
                GroupName = newInventoryGroupDto.GroupName,
                GroupDescription = newInventoryGroupDto.GroupDescription
            };

            _context.InventoryGroups.Add(inventoryGroup);
            await _context.SaveChangesAsync();

            var returnedInventoryGroupDto = new InventoryGroupDto
            {
                GroupName = inventoryGroup.GroupName,
                GroupDescription = inventoryGroup.GroupDescription
            };

            return CreatedAtAction(nameof(GetOneInventoryGroup), new { id = returnedInventoryGroupDto.GroupId }, returnedInventoryGroupDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInventoryGroup(int id, InventoryGroupDto updatedInventoryGroupDto)
        {
            if (id != updatedInventoryGroupDto.GroupId)
                return BadRequest("ID in URL does not match ID in the body.");

            var inventoryGroup = await _context.InventoryGroups.FindAsync(id);
            if (inventoryGroup == null)
                return NotFound();

            inventoryGroup.GroupName = updatedInventoryGroupDto.GroupName;
            inventoryGroup.GroupDescription = updatedInventoryGroupDto.GroupDescription;

            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInventoryGroup(int id)
        {
            var inventoryGroup = await _context.InventoryGroups.FindAsync(id);
            if (inventoryGroup == null)
                return NotFound();

            _context.InventoryGroups.Remove(inventoryGroup);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
