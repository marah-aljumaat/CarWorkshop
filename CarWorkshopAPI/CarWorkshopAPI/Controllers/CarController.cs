using Microsoft.AspNetCore.Mvc;
using CarWorkshopAPI.Data;
using CarWorkshopAPI.Models;
using Microsoft.EntityFrameworkCore;
using CarWorkshopAPI.DTO;

namespace CarWorkshopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly CarServiceSystemContext _context;

        public CarController(CarServiceSystemContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<CarDto>>> GetAllCars()
        {
            var customerCars = await _context.CustomerCars
                .Select(c => new CarDto
                {
                    CarId = c.CarId,
                    CustomerId = c.CustomerId,
                    PlateNumber = c.PlateNumber,
                    Color = c.Color,
                    Model = c.Model,
                    ManufactureYear = c.ManufactureYear,
                    ChassisNumber = c.ChassisNumber,
                    EngineNumber = c.EngineNumber,
                    WarrantyStartDate = c.WarrantyStartDate,
                    WarrantyEndDate = c.WarrantyEndDate,
                    WarrantyCoveredDistance = c.WarrantyCoveredDistance,
                    WarrantyDuration = c.WarrantyDuration,
                    OdometerReading = c.OdometerReading,
                    CarStatus = c.CarStatus,
                    PlateType = c.PlateType,
                    EngineType = c.EngineType,
                    TransmissionType = c.TransmissionType
                }).ToListAsync();
            return Ok(customerCars);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CarDto>> GetOneCar(int id)
        {
            var customerCar = await _context.CustomerCars.FindAsync(id);
            if (customerCar == null)
                return NotFound();

            var dto = new CarDto
                {
                CarId = customerCar.CarId,
                CustomerId = customerCar.CustomerId,
                PlateNumber = customerCar.PlateNumber,
                Color = customerCar.Color,
                Model = customerCar.Model,
                ManufactureYear = customerCar.ManufactureYear,
                ChassisNumber = customerCar.ChassisNumber,
                EngineNumber = customerCar.EngineNumber,
                WarrantyStartDate = customerCar.WarrantyStartDate,
                WarrantyEndDate = customerCar.WarrantyEndDate,
                WarrantyCoveredDistance = customerCar.WarrantyCoveredDistance,
                WarrantyDuration = customerCar.WarrantyDuration,
                OdometerReading = customerCar.OdometerReading,
                CarStatus = customerCar.CarStatus,
                PlateType = customerCar.PlateType,
                EngineType = customerCar.EngineType,
                TransmissionType = customerCar.TransmissionType
            };
            return Ok(customerCar);
        }

        [HttpPost]
        public async Task<ActionResult<CarDto>> CreateNewCar(CarDto newCarDto)
        {
            if (newCarDto == null)
                return BadRequest();

            var newCar = new CustomerCar
            {
                CustomerId = newCarDto.CustomerId,
                PlateNumber = newCarDto.PlateNumber,
                Color = newCarDto.Color,
                Model = newCarDto.Model,
                ManufactureYear = newCarDto.ManufactureYear,
                ChassisNumber = newCarDto.ChassisNumber,
                EngineNumber = newCarDto.EngineNumber,
                WarrantyStartDate = newCarDto.WarrantyStartDate,
                WarrantyEndDate = newCarDto.WarrantyEndDate,
                WarrantyCoveredDistance = newCarDto.WarrantyCoveredDistance,
                WarrantyDuration = newCarDto.WarrantyDuration,
                OdometerReading = newCarDto.OdometerReading,
                CarStatus = newCarDto.CarStatus,
                PlateType = newCarDto.PlateType,
                EngineType = newCarDto.EngineType,
                TransmissionType = newCarDto.TransmissionType
            };
            _context.CustomerCars.Add(newCar);
            await _context.SaveChangesAsync();

            var returnedCarDto = new CarDto
            {
                CarId = newCar.CarId,
                CustomerId = newCar.CustomerId,
                PlateNumber = newCar.PlateNumber,
                Color = newCar.Color,
                Model = newCar.Model,
                ManufactureYear = newCar.ManufactureYear,
                ChassisNumber = newCar.ChassisNumber,
                EngineNumber = newCar.EngineNumber,
                WarrantyStartDate = newCar.WarrantyStartDate,
                WarrantyEndDate = newCar.WarrantyEndDate,
                WarrantyCoveredDistance = newCar.WarrantyCoveredDistance,
                WarrantyDuration = newCar.WarrantyDuration,
                OdometerReading = newCar.OdometerReading,
                CarStatus = newCar.CarStatus,
                PlateType = newCar.PlateType,
                EngineType = newCar.EngineType,
                TransmissionType = newCar.TransmissionType
            };
            return CreatedAtAction(nameof(GetOneCar), new { id = returnedCarDto.CarId }, returnedCarDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCar(int id, CarDto UpdatedCarDto)
        {
            if (id != UpdatedCarDto.CarId)
                return BadRequest("ID in URL does not match ID in the body.");

            var existingCar = await _context.CustomerCars.FindAsync(id);
            if (existingCar == null)
                return NotFound();

            existingCar.CustomerId = UpdatedCarDto.CustomerId;
            existingCar.PlateNumber = UpdatedCarDto.PlateNumber;
            existingCar.Color = UpdatedCarDto.Color;
            existingCar.Model = UpdatedCarDto.Model;
            existingCar.ManufactureYear = UpdatedCarDto.ManufactureYear;
            existingCar.ChassisNumber = UpdatedCarDto.ChassisNumber;
            existingCar.EngineNumber = UpdatedCarDto.EngineNumber;
            existingCar.WarrantyStartDate = UpdatedCarDto.WarrantyStartDate;
            existingCar.WarrantyEndDate = UpdatedCarDto.WarrantyEndDate;
            existingCar.WarrantyCoveredDistance = UpdatedCarDto.WarrantyCoveredDistance;
            existingCar.WarrantyDuration = UpdatedCarDto.WarrantyDuration;
            existingCar.OdometerReading = UpdatedCarDto.OdometerReading;
            existingCar.CarStatus = UpdatedCarDto.CarStatus;
            existingCar.PlateType = UpdatedCarDto.PlateType;
            existingCar.EngineType = UpdatedCarDto.EngineType;
            existingCar.TransmissionType = UpdatedCarDto.TransmissionType;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCar(int id)
        {
            var customerCar = await _context.CustomerCars.FindAsync(id);
            if (customerCar == null)
                return NotFound();
            _context.CustomerCars.Remove(customerCar);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
