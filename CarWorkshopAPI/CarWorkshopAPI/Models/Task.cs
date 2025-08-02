using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CarWorkshopAPI.Models;

[Table("Tasks", Schema = "workshop")]
public partial class Task
{
    [Key]
    [Column("TaskID")]
    public int TaskId { get; set; }

    [Column("CustomerID")]
    public int? CustomerId { get; set; }

    [Column("ProjectID")]
    public int? ProjectId { get; set; }

    [Column("CarID")]
    public int? CarId { get; set; }

    public string? TaskName { get; set; }

    public string? TaskDescription { get; set; }

    public string? TaskStatus { get; set; }

    public DateOnly? TaskStartDate { get; set; }

    public DateOnly? TaskEndDate { get; set; }

    public DateOnly? CarReceivedAt { get; set; }

    public DateOnly? CarDeliveredAt { get; set; }

    [ForeignKey("CarId")]
    [InverseProperty("Tasks")]
    public virtual CustomerCar? Car { get; set; }

    [ForeignKey("CustomerId")]
    [InverseProperty("Tasks")]
    public virtual Customer? Customer { get; set; }

    [ForeignKey("ProjectId")]
    [InverseProperty("Tasks")]
    public virtual Project? Project { get; set; }

    [InverseProperty("Task")]
    public virtual ICollection<TaskLine> TaskLines { get; set; } = new List<TaskLine>();
}
