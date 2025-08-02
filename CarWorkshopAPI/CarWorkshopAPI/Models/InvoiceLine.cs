using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CarWorkshopAPI.Models;

[Table("InvoiceLines", Schema = "workshop")]
public partial class InvoiceLine
{
    [Key]
    [Column("LineID")]
    public int LineId { get; set; }

    [Column("TaskLineID")]
    public int? TaskLineId { get; set; }

    [Column("InvoiceID")]
    public int? InvoiceId { get; set; }

    [Column("InventoryID")]
    public int? InventoryId { get; set; }

    [Column(TypeName = "decimal(20, 2)")]
    public decimal? Quantity { get; set; }

    [Column(TypeName = "decimal(20, 2)")]
    public decimal? UnitPrice { get; set; }

    [Column(TypeName = "decimal(20, 2)")]
    public decimal? LineTotal { get; set; }

    public string? LineDescription { get; set; }

    [ForeignKey("InventoryId")]
    [InverseProperty("InvoiceLines")]
    public virtual Inventory? Inventory { get; set; }

    [ForeignKey("InvoiceId")]
    [InverseProperty("InvoiceLines")]
    public virtual Invoice? Invoice { get; set; }

    [ForeignKey("TaskLineId")]
    [InverseProperty("InvoiceLines")]
    public virtual TaskLine? TaskLine { get; set; }
}
