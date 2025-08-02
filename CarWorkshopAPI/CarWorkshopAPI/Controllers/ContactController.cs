using Microsoft.AspNetCore.Mvc;
using CarWorkshopAPI.Data;
using CarWorkshopAPI.Models;
using Microsoft.EntityFrameworkCore;
using CarWorkshopAPI.DTO;

namespace CarWorkshopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly CarServiceSystemContext _context;
        public ContactController(CarServiceSystemContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<ContactDto>>> GetAllContacts()
        {
            var customerContacts = await _context.CustomerContacts
                .Select(c=> new ContactDto
                {
                    ContactId = c.ContactId,
                    CustomerId = c.CustomerId,
                    ContactName = c.ContactName,
                    ContactRole = c.ContactRole,
                    ContactPhone = c.ContactPhone,
                    ContactEmail = c.ContactEmail
                }).ToListAsync();
            return Ok(customerContacts);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<ContactDto>> GetOneContact(int id)
        {
            var customerContact = await _context.CustomerContacts.FindAsync(id);
            if (customerContact == null)
                return NotFound();

            var dto = new ContactDto
            {
                CustomerId = customerContact.CustomerId,
                ContactId = customerContact.ContactId,
                ContactName = customerContact.ContactName,
                ContactRole = customerContact.ContactRole,
                ContactPhone = customerContact.ContactPhone,
                ContactEmail = customerContact.ContactEmail
            };
            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<ContactDto>> CreateNewContact(ContactDto newContactDto)
        {
            if (newContactDto == null)
                return BadRequest();

            var newContact = new CustomerContact
            {
                CustomerId = newContactDto.CustomerId,
                ContactName = newContactDto.ContactName,
                ContactRole = newContactDto.ContactRole,
                ContactPhone = newContactDto.ContactPhone,
                ContactEmail = newContactDto.ContactEmail
            };
            _context.CustomerContacts.Add(newContact);
            await _context.SaveChangesAsync();

            var returnDto = new ContactDto
            {
                ContactId = newContact.ContactId,
                CustomerId = newContact.CustomerId,
                ContactName = newContact.ContactName,
                ContactRole = newContact.ContactRole,
                ContactPhone = newContact.ContactPhone,
                ContactEmail = newContact.ContactEmail
            };
            return CreatedAtAction(nameof(GetOneContact), new { id = returnDto.ContactId }, returnDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateContact(int id, ContactDto updatedContactDto)
        {
            if (id != updatedContactDto.ContactId)
                return BadRequest("ID in URL does not match ID in the body.");

            var existingContact = await _context.CustomerContacts.FindAsync(id);
            if (existingContact == null)
                return NotFound();
            
            existingContact.CustomerId = updatedContactDto.CustomerId;
            existingContact.ContactName = updatedContactDto.ContactName;
            existingContact.ContactPhone = updatedContactDto.ContactPhone;
            existingContact.ContactEmail = updatedContactDto.ContactEmail;
            existingContact.ContactRole = updatedContactDto.ContactRole;

            await _context.SaveChangesAsync();
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContact(int id)
        {
            var contactToDelete = await _context.CustomerContacts.FindAsync(id);
            if (contactToDelete == null)
                return NotFound();

            _context.CustomerContacts.Remove(contactToDelete);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
