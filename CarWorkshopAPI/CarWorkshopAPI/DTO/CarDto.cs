using CarWorkshopAPI.Models;

namespace CarWorkshopAPI.DTO
{
    public class CarDto
    {
        public int CarId { get; set; }
        public int? CustomerId { get; set; }
        public string? PlateNumber { get; set; }
        public string? Color { get; set; }
        public string? Model { get; set; }
        public int? ManufactureYear { get; set; }
        public string? ChassisNumber { get; set; }
        public string? EngineNumber { get; set; }
        public DateOnly? WarrantyStartDate { get; set; }
        public DateOnly? WarrantyEndDate { get; set; }
        public int? WarrantyCoveredDistance { get; set; }
        public int? WarrantyDuration { get; set; }
        public int? OdometerReading { get; set; }
        public string? CarStatus { get; set; }
        public PlateType PlateType { get; set; }
        public EngineType EngineType { get; set; }
        public TransmissionType TransmissionType { get; set; }
    }
}
