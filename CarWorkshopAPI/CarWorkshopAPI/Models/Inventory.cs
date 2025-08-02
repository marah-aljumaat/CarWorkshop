using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CarWorkshopAPI.Models;

[Table("Inventory", Schema = "workshop")]
public partial class Inventory
{
    [Key]
    [Column("InventoryID")]
    public int InventoryId { get; set; }

    [StringLength(50)]
    public string? ItemName { get; set; }

    [StringLength(50)]
    public string? ItemType { get; set; }

    [Column(TypeName = "decimal(20, 2)")]
    public decimal? ItemPrice { get; set; }

    [StringLength(50)]
    public string? ItemStatus { get; set; }

    [InverseProperty("Inventory")]
    public virtual ICollection<InvoiceLine> InvoiceLines { get; set; } = new List<InvoiceLine>();

    [InverseProperty("Inventory")]
    public virtual ICollection<TaskLine> TaskLines { get; set; } = new List<TaskLine>();

    [ForeignKey("InventoryId")]
    [InverseProperty("Inventories")]
    public virtual ICollection<InventoryGroup> Groups { get; set; } = new List<InventoryGroup>();
}
