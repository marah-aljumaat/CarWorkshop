using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CarWorkshopAPI.Models;

[Table("Projects", Schema = "workshop")]
public partial class Project
{
    [Key]
    [Column("ProjectID")]
    public int ProjectId { get; set; }

    [Column("CustomerID")]
    public int? CustomerId { get; set; }

    public string? ProjectName { get; set; }

    public string? ProjectDescription { get; set; }

    public DateOnly? ProjectStartDate { get; set; }

    public DateOnly? ProjectEndDate { get; set; }

    public string? ProjectStatus { get; set; }

    [ForeignKey("CustomerId")]
    [InverseProperty("Projects")]
    public virtual Customer? Customer { get; set; }

    [InverseProperty("Project")]
    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();
}
