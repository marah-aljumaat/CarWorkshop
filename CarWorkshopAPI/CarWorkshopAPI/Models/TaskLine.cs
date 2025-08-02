using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CarWorkshopAPI.Models;

[Table("TaskLines", Schema = "workshop")]
public partial class TaskLine
{
    [Key]
    [Column("TaskLineID")]
    public int TaskLineId { get; set; }

    [Column("TaskID")]
    public int? TaskId { get; set; }

    [Column("InventoryID")]
    public int? InventoryId { get; set; }

    [Column("EmployeeID")]
    public int? EmployeeId { get; set; }

    [Column(TypeName = "decimal(20, 2)")]
    public decimal? Quantity { get; set; }

    [Column(TypeName = "decimal(20, 2)")]
    public decimal? UnitPrice { get; set; }

    [Column(TypeName = "decimal(20, 2)")]
    public decimal? LineTotal { get; set; }

    public string? TaskLineDescription { get; set; }

    [ForeignKey("EmployeeId")]
    [InverseProperty("TaskLines")]
    public virtual Employee? Employee { get; set; }

    [ForeignKey("InventoryId")]
    [InverseProperty("TaskLines")]
    public virtual Inventory? Inventory { get; set; }

    [InverseProperty("TaskLine")]
    public virtual ICollection<InvoiceLine> InvoiceLines { get; set; } = new List<InvoiceLine>();

    [ForeignKey("TaskId")]
    [InverseProperty("TaskLines")]
    public virtual Task? Task { get; set; }
}
