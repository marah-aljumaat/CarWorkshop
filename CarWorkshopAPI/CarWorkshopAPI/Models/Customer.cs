using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CarWorkshopAPI.Models;

[Table("Customers", Schema = "workshop")]
public partial class Customer
{
    [Key]
    [Column("CustomerID")]
    public int CustomerId { get; set; }

    [StringLength(50)]
    public string? CustomerName { get; set; }

    [StringLength(20)]
    public string? CustomerType { get; set; }

    [StringLength(20)]
    public string? CustomerPhone { get; set; }

    [StringLength(50)]
    public string? CustomerEmail { get; set; }

    [InverseProperty("Customer")]
    public virtual ICollection<CustomerCar> CustomerCars { get; set; } = new List<CustomerCar>();

    [InverseProperty("Customer")]
    public virtual ICollection<CustomerContact> CustomerContacts { get; set; } = new List<CustomerContact>();

    [InverseProperty("Customer")]
    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    [InverseProperty("Customer")]
    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();

    [InverseProperty("Customer")]
    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();
}
