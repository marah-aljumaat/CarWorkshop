using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CarWorkshopAPI.Models;

[Table("Employees", Schema = "workshop")]
public partial class Employee
{
    [Key]
    [Column("EmployeeID")]
    public int EmployeeId { get; set; }

    [StringLength(50)]
    public string? EmployeeName { get; set; }

    [StringLength(50)]
    public string? JobTitle { get; set; }

    [StringLength(50)]
    public string? Specialty { get; set; }

    [Column(TypeName = "decimal(20, 2)")]
    public decimal? CommissionRate { get; set; }

    [StringLength(50)]
    public string? UserName { get; set; }

    [StringLength(8)]
    public string? EmpPassword { get; set; }

    [StringLength(20)]
    public string? AttendanceStatus { get; set; }

    public DateOnly? AttendanceDate { get; set; }

    [InverseProperty("Employee")]
    public virtual ICollection<TaskLine> TaskLines { get; set; } = new List<TaskLine>();
}
