using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CarWorkshopAPI.Models;

[Table("CustomerContacts", Schema = "workshop")]
public partial class CustomerContact
{
    [Key]
    [Column("ContactID")]
    public int ContactId { get; set; }

    [Column("CustomerID")]
    public int? CustomerId { get; set; }

    [StringLength(50)]
    public string? ContactName { get; set; }

    [StringLength(50)]
    public string? ContactRole { get; set; }

    [StringLength(20)]
    public string? ContactPhone { get; set; }

    [StringLength(50)]
    public string? ContactEmail { get; set; }

    [ForeignKey("CustomerId")]
    [InverseProperty("CustomerContacts")]
    public virtual Customer? Customer { get; set; }
}
