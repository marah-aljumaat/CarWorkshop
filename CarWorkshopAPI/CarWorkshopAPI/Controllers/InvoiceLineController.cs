using Microsoft.AspNetCore.Mvc;
using CarWorkshopAPI.Data;
using CarWorkshopAPI.Models;
using Microsoft.EntityFrameworkCore;
using CarWorkshopAPI.DTO;

namespace CarWorkshopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceLineController : ControllerBase
    {
        private readonly CarServiceSystemContext _context;

        public InvoiceLineController(CarServiceSystemContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<InvoiceLineDto>>> GetAllInvoiceLines()
        {
            var invoiceLines = await _context.InvoiceLines
                .Select(il => new InvoiceLineDto
                {
                    LineId = il.LineId,
                    TaskLineId = il.TaskLineId,
                    InvoiceId = il.InvoiceId,
                    InventoryId = il.InventoryId,
                    LineDescription = il.LineDescription,
                    UnitPrice = il.UnitPrice,
                    Quantity = il.Quantity,
                    LineTotal = il.LineTotal,
                }).ToListAsync();
            return Ok(invoiceLines);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<InvoiceLineDto>> GetOneInvoiceLine(int id)
        {
            var invoiceLine = await _context.InvoiceLines.FindAsync(id);
            if (invoiceLine == null)
                return NotFound();

            var dto = new InvoiceLineDto
            {
                LineId = invoiceLine.LineId,
                TaskLineId = invoiceLine.TaskLineId,
                InvoiceId = invoiceLine.InvoiceId,
                InventoryId = invoiceLine.InventoryId,
                LineDescription = invoiceLine.LineDescription,
                UnitPrice = invoiceLine.UnitPrice,
                Quantity = invoiceLine.Quantity,
                LineTotal = invoiceLine.LineTotal,
            };
            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<InvoiceLineDto>> CreateNewInvoiceLine(InvoiceLineDto newInvoiceLineDto)
        {
            if (newInvoiceLineDto == null)
                return BadRequest();
            var invoiceLine = new InvoiceLine
            {
                TaskLineId = newInvoiceLineDto.TaskLineId,
                InvoiceId = newInvoiceLineDto.InvoiceId,
                InventoryId = newInvoiceLineDto.InventoryId,
                LineDescription = newInvoiceLineDto.LineDescription,
                UnitPrice = newInvoiceLineDto.UnitPrice,
                Quantity = newInvoiceLineDto.Quantity,
                LineTotal = newInvoiceLineDto.LineTotal,
            };
            _context.InvoiceLines.Add(invoiceLine);
            await _context.SaveChangesAsync();

            var returnedInvoiceLineDto = new InvoiceLineDto
            {
                LineId = invoiceLine.LineId,
                TaskLineId = invoiceLine.TaskLineId,
                InvoiceId = invoiceLine.InvoiceId,
                InventoryId = invoiceLine.InventoryId,
                LineDescription = invoiceLine.LineDescription,
                UnitPrice = invoiceLine.UnitPrice,
                Quantity = invoiceLine.Quantity,
                LineTotal = invoiceLine.LineTotal,
            };
            return CreatedAtAction(nameof(GetOneInvoiceLine), new { id = returnedInvoiceLineDto.LineId }, returnedInvoiceLineDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInvoiceLine(int id, InvoiceLineDto updatedInvoiceLineDto)
        {
            if (id != updatedInvoiceLineDto.LineId)
                return BadRequest("ID in URL does not match ID in the body.");

            var invoiceLine = await _context.InvoiceLines.FindAsync(id);
            if (invoiceLine == null)
                return NotFound();
            invoiceLine.TaskLineId = updatedInvoiceLineDto.TaskLineId;
            invoiceLine.InvoiceId = updatedInvoiceLineDto.InvoiceId;
            invoiceLine.InventoryId = updatedInvoiceLineDto.InventoryId;
            invoiceLine.LineDescription = updatedInvoiceLineDto.LineDescription;
            invoiceLine.UnitPrice = updatedInvoiceLineDto.UnitPrice;
            invoiceLine.Quantity = updatedInvoiceLineDto.Quantity;
            invoiceLine.LineTotal = updatedInvoiceLineDto.LineTotal;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvoiceLine(int id)
        {
            var invoiceLine = await _context.InvoiceLines.FindAsync(id);
            if (invoiceLine == null)
                return NotFound();
            _context.InvoiceLines.Remove(invoiceLine);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
