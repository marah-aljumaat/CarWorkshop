using Microsoft.AspNetCore.Mvc;
using CarWorkshopAPI.Data;
using CarWorkshopAPI.Models;
using Microsoft.EntityFrameworkCore;
using CarWorkshopAPI.DTO;

namespace CarWorkshopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly CarServiceSystemContext _context;

        public InventoryController(CarServiceSystemContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<InventoryDto>>> GetAllInventoryItems()
        {
            var inventoryItems = await _context.Inventories
                .Select(i => new InventoryDto
                {
                    InventoryId = i.InventoryId,
                    ItemName = i.ItemName,
                    ItemType = i.ItemType,
                    ItemPrice = i.ItemPrice,
                    ItemStatus = i.ItemStatus
                }).ToListAsync();

            return Ok(inventoryItems);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<InventoryDto>> GetOneInventoryItem(int id)
        {
            var inventoryItem = await _context.Inventories.FindAsync(id);
            if (inventoryItem == null)
                return NotFound();

            var dto = new InventoryDto
            {
                InventoryId = inventoryItem.InventoryId,
                ItemName = inventoryItem.ItemName,
                ItemType = inventoryItem.ItemType,
                ItemPrice = inventoryItem.ItemPrice,
                ItemStatus = inventoryItem.ItemStatus
            };
            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<InventoryDto>> CreateNewInventoryItem(InventoryDto newInventoryItemDto)
        {
            if (newInventoryItemDto == null)
                return BadRequest();

            var inventoryItem = new Inventory
            {
                ItemName = newInventoryItemDto.ItemName,
                ItemType = newInventoryItemDto.ItemType,
                ItemPrice = newInventoryItemDto.ItemPrice,
                ItemStatus = newInventoryItemDto.ItemStatus
            };

            _context.Inventories.Add(inventoryItem);
            await _context.SaveChangesAsync();

            var returnedDto = new InventoryDto
            {
                InventoryId = inventoryItem.InventoryId,
                ItemName = inventoryItem.ItemName,
                ItemType = inventoryItem.ItemType,
                ItemPrice = inventoryItem.ItemPrice,
                ItemStatus = inventoryItem.ItemStatus


            };
            return CreatedAtAction(nameof(GetOneInventoryItem), new { id = returnedDto.InventoryId }, returnedDto);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInventoryItem(int id, InventoryDto updatedInventoryItemDto)
        {
            if (id != updatedInventoryItemDto.InventoryId)
                return BadRequest("ID in URL does not match ID in the body.");

            var inventoryItem = await _context.Inventories.FindAsync(id);
            if (inventoryItem == null)
                return NotFound();

            inventoryItem.ItemName = updatedInventoryItemDto.ItemName;
            inventoryItem.ItemType = updatedInventoryItemDto.ItemType;
            inventoryItem.ItemPrice = updatedInventoryItemDto.ItemPrice;
            inventoryItem.ItemStatus = updatedInventoryItemDto.ItemStatus;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInventoryItem(int id)
        {
            var inventoryItem = await _context.Inventories.FindAsync(id);
            if (inventoryItem == null)
                return NotFound();
            _context.Inventories.Remove(inventoryItem);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
