using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CarWorkshopAPI.Models;

public enum PlateType
{
    Private,
    Commercial,
    Government,
    Taxi,
    Diplomatic,
    Temporary,
    Custom
}
public enum EngineType
{
    Gasoline,
    Diesel,
    Hybrid,
    Electric
}
public enum TransmissionType
{
    Manual,
    Automatic
}

[Table("CustomerCars", Schema = "workshop")]
public partial class CustomerCar
{
    [Key]
    [Column("CarID")]
    public int CarId { get; set; }

    [Column("CustomerID")]
    public int? CustomerId { get; set; }

    public string? PlateNumber { get; set; }

    [StringLength(20)]
    public string? Color { get; set; }

    [StringLength(50)]
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

    [ForeignKey("CustomerId")]
    [InverseProperty("CustomerCars")]
    public virtual Customer? Customer { get; set; }

    [InverseProperty("Car")]
    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();
}
