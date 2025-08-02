using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CarWorkshopAPI.Models;

[Table("InventoryGroups", Schema = "workshop")]
public partial class InventoryGroup
{
    [Key]
    [Column("GroupID")]
    public int GroupId { get; set; }

    [StringLength(50)]
    public string? GroupName { get; set; }

    public string? GroupDescription { get; set; }

    [ForeignKey("GroupId")]
    [InverseProperty("Groups")]
    public virtual ICollection<Inventory> Inventories { get; set; } = new List<Inventory>();
}
