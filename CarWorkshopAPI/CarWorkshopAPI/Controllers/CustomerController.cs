using Microsoft.AspNetCore.Mvc;
using CarWorkshopAPI.Data;
using CarWorkshopAPI.Models;
using Microsoft.EntityFrameworkCore;
using CarWorkshopAPI.DTO;

namespace CarWorkshopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {

        private readonly CarServiceSystemContext _context;

        public CustomerController(CarServiceSystemContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<CustomerDto>>> GetAllCustomers()
        {
            var customers = await _context.Customers
                .Select(c => new CustomerDto
                {
                    CustomerId = c.CustomerId,
                    CustomerName = c.CustomerName,
                    CustomerType = c.CustomerType,
                    CustomerPhone = c.CustomerPhone,
                    CustomerEmail = c.CustomerEmail
                }).ToListAsync();

            return Ok(customers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDto>> GetOneCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
                return NotFound();

            var dto = new CustomerDto
            {
                CustomerId = customer.CustomerId,
                CustomerName = customer.CustomerName,
                CustomerType = customer.CustomerType,
                CustomerPhone = customer.CustomerPhone,
                CustomerEmail = customer.CustomerEmail
            };
            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<CustomerDto>> CreateNewCustomer(CustomerDto newCustomerDto)
        {
            if (newCustomerDto == null)
                return BadRequest();

            var newCustomer = new Customer
            {
                CustomerName = newCustomerDto.CustomerName,
                CustomerType = newCustomerDto.CustomerType,
                CustomerPhone = newCustomerDto.CustomerPhone,
                CustomerEmail = newCustomerDto.CustomerEmail
            };

            _context.Customers.Add(newCustomer);
            await _context.SaveChangesAsync();

            var resultDto = new CustomerDto
            {
                CustomerId = newCustomer.CustomerId,
                CustomerName = newCustomer.CustomerName,
                CustomerType = newCustomer.CustomerType,
                CustomerPhone = newCustomer.CustomerPhone,
                CustomerEmail = newCustomer.CustomerEmail
            };

            return CreatedAtAction(nameof(GetOneCustomer), new { id = resultDto.CustomerId }, resultDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, CustomerDto updatedDto)
        {
            if (id != updatedDto.CustomerId)
                return BadRequest("ID in URL does not match ID in the body.");

            var existingCustomer = await _context.Customers.FindAsync(id);
            if (existingCustomer == null)
                return NotFound();

            existingCustomer.CustomerName = updatedDto.CustomerName;
            existingCustomer.CustomerType = updatedDto.CustomerType;
            existingCustomer.CustomerPhone = updatedDto.CustomerPhone;
            existingCustomer.CustomerEmail = updatedDto.CustomerEmail;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
                return NotFound();

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}