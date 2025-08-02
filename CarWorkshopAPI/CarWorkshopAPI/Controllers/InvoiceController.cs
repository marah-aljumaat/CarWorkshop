using Microsoft.AspNetCore.Mvc;
using CarWorkshopAPI.Data;
using CarWorkshopAPI.Models;
using Microsoft.EntityFrameworkCore;
using CarWorkshopAPI.DTO;

namespace CarWorkshopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly CarServiceSystemContext _context;

        public InvoiceController(CarServiceSystemContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<InvoiceDto>>> GetAllInvoices()
        {
            var invoices = await _context.Invoices
                .Select(i => new InvoiceDto
                {
                    InvoiceId = i.InvoiceId,
                    CustomerId = i.CustomerId,
                    DateIssued = i.DateIssued,
                    DueDate = i.DueDate,
                    TotalAmount = i.TotalAmount,
                    InvoiceNotes = i.InvoiceNotes,
                    InvoiceStatus = i.InvoiceStatus,
                }).ToListAsync();
            return Ok(invoices);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<InvoiceDto>> GetOneInvoice(int id)
        {
            var invoice = await _context.Invoices.FindAsync(id);
            if (invoice == null)
                return NotFound();

            var dto = new InvoiceDto
            {
                InvoiceId = invoice.InvoiceId,
                CustomerId = invoice.CustomerId,
                DateIssued = invoice.DateIssued,
                DueDate = invoice.DueDate,
                TotalAmount = invoice.TotalAmount,
                InvoiceNotes = invoice.InvoiceNotes,
                InvoiceStatus = invoice.InvoiceStatus,
            };
            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<InvoiceDto>> CreateNewInvoice(InvoiceDto newInvoiceDto)
        {
            if (newInvoiceDto == null)
                return BadRequest();

            var invoice = new Invoice
            {
                CustomerId = newInvoiceDto.CustomerId,
                DateIssued = newInvoiceDto.DateIssued,
                DueDate = newInvoiceDto.DueDate,
                TotalAmount = newInvoiceDto.TotalAmount,
                InvoiceNotes = newInvoiceDto.InvoiceNotes,
                InvoiceStatus = newInvoiceDto.InvoiceStatus,
            };
            _context.Invoices.Add(invoice);
            await _context.SaveChangesAsync();

            var returnedInvoice = new InvoiceDto
            {
                CustomerId = invoice.CustomerId,
                DateIssued = invoice.DateIssued,
                DueDate = invoice.DueDate,
                TotalAmount = invoice.TotalAmount,
                InvoiceNotes = invoice.InvoiceNotes,
                InvoiceStatus = invoice.InvoiceStatus,
            };
            return CreatedAtAction(nameof(GetOneInvoice), new { id = returnedInvoice.InvoiceId }, returnedInvoice);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInvoice(int id, InvoiceDto updatedInvoiceDto)
        {
            if (id != updatedInvoiceDto.InvoiceId)
                return BadRequest("ID in URL does not match ID in the body.");

            var invoice = await _context.Invoices.FindAsync(id);
            if (invoice == null)
                return NotFound();

            invoice.CustomerId = updatedInvoiceDto.CustomerId;
            invoice.DateIssued = updatedInvoiceDto.DateIssued;
            invoice.DueDate = updatedInvoiceDto.DueDate;
            invoice.TotalAmount = updatedInvoiceDto.TotalAmount;
            invoice.InvoiceNotes = updatedInvoiceDto.InvoiceNotes;
            invoice.InvoiceStatus = updatedInvoiceDto.InvoiceStatus;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvoice(int id)
        {
            var invoice = await _context.Invoices.FindAsync(id);
            if (invoice == null)
                return NotFound();
            _context.Invoices.Remove(invoice);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
