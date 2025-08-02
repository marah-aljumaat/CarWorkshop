using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CarWorkshopAPI.Models;

[Table("Invoices", Schema = "workshop")]
public partial class Invoice
{
    [Key]
    [Column("InvoiceID")]
    public int InvoiceId { get; set; }

    [Column("CustomerID")]
    public int? CustomerId { get; set; }

    public DateOnly? DateIssued { get; set; }

    public DateOnly? DueDate { get; set; }

    [Column(TypeName = "decimal(20, 2)")]
    public decimal? TotalAmount { get; set; }

    public string? InvoiceNotes { get; set; }

    public string? InvoiceStatus { get; set; }

    [ForeignKey("CustomerId")]
    [InverseProperty("Invoices")]
    public virtual Customer? Customer { get; set; }

    [InverseProperty("Invoice")]
    public virtual ICollection<InvoiceLine> InvoiceLines { get; set; } = new List<InvoiceLine>();
}
