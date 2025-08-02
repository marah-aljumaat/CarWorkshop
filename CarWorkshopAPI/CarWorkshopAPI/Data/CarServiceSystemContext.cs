using System;
using System.Collections.Generic;
using CarWorkshopAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CarWorkshopAPI.Data;

public partial class CarServiceSystemContext : DbContext
{
    public CarServiceSystemContext()
    {
    }

    public CarServiceSystemContext(DbContextOptions<CarServiceSystemContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<CustomerCar> CustomerCars { get; set; }

    public virtual DbSet<CustomerContact> CustomerContacts { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Inventory> Inventories { get; set; }

    public virtual DbSet<InventoryGroup> InventoryGroups { get; set; }

    public virtual DbSet<Invoice> Invoices { get; set; }

    public virtual DbSet<InvoiceLine> InvoiceLines { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<Models.Task> Tasks { get; set; }

    public virtual DbSet<TaskLine> TaskLines { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=CarServiceSystem;Trusted_Connection=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__Customer__A4AE64B8351E4813");
        });

        modelBuilder.Entity<CustomerCar>(entity =>
        {
            entity.HasKey(e => e.CarId).HasName("PK__Customer__68A0340EA731298F");

            entity.HasOne(d => d.Customer).WithMany(p => p.CustomerCars).HasConstraintName("FK__CustomerC__Custo__29572725");
            
            entity.Property(e => e.PlateType).HasConversion<string>();

            entity.Property(e => e.EngineType).HasConversion<string>();

            entity.Property(e => e.TransmissionType).HasConversion<string>();
        });

        modelBuilder.Entity<CustomerContact>(entity =>
        {
            entity.HasKey(e => e.ContactId).HasName("PK__Customer__5C6625BBE21068EC");

            entity.HasOne(d => d.Customer).WithMany(p => p.CustomerContacts).HasConstraintName("FK__CustomerC__Custo__267ABA7A");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK__Employee__7AD04FF1B5C0AED8");
        });

        modelBuilder.Entity<Inventory>(entity =>
        {
            entity.HasKey(e => e.InventoryId).HasName("PK__Inventor__F5FDE6D398DEE5D8");

            entity.HasMany(d => d.Groups).WithMany(p => p.Inventories)
                .UsingEntity<Dictionary<string, object>>(
                    "InventoryGroupItem",
                    r => r.HasOne<InventoryGroup>().WithMany()
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Inventory__Group__3E52440B"),
                    l => l.HasOne<Inventory>().WithMany()
                        .HasForeignKey("InventoryId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Inventory__Inven__3D5E1FD2"),
                    j =>
                    {
                        j.HasKey("InventoryId", "GroupId").HasName("PK__Inventor__44B449E3345B8891");
                        j.ToTable("InventoryGroupItems", "workshop");
                        j.IndexerProperty<int>("InventoryId").HasColumnName("InventoryID");
                        j.IndexerProperty<int>("GroupId").HasColumnName("GroupID");
                    });
        });

        modelBuilder.Entity<InventoryGroup>(entity =>
        {
            entity.HasKey(e => e.GroupId).HasName("PK__Inventor__149AF30A740C4AF3");
        });

        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.HasKey(e => e.InvoiceId).HasName("PK__Invoices__D796AAD5005A4568");

            entity.HasOne(d => d.Customer).WithMany(p => p.Invoices).HasConstraintName("FK__Invoices__Custom__31EC6D26");
        });

        modelBuilder.Entity<InvoiceLine>(entity =>
        {
            entity.HasKey(e => e.LineId).HasName("PK__InvoiceL__2EAE64C93E676699");

            entity.HasOne(d => d.Inventory).WithMany(p => p.InvoiceLines).HasConstraintName("FK__InvoiceLi__Inven__49C3F6B7");

            entity.HasOne(d => d.Invoice).WithMany(p => p.InvoiceLines).HasConstraintName("FK__InvoiceLi__Invoi__48CFD27E");

            entity.HasOne(d => d.TaskLine).WithMany(p => p.InvoiceLines).HasConstraintName("FK__InvoiceLi__TaskL__47DBAE45");
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.ProjectId).HasName("PK__Projects__761ABED0A63D2B41");

            entity.HasOne(d => d.Customer).WithMany(p => p.Projects).HasConstraintName("FK__Projects__Custom__2F10007B");
        });

        modelBuilder.Entity<Models.Task>(entity =>
        {
            entity.HasKey(e => e.TaskId).HasName("PK__Tasks__7C6949D1FC36E41C");

            entity.HasOne(d => d.Car).WithMany(p => p.Tasks).HasConstraintName("FK__Tasks__CarID__36B12243");

            entity.HasOne(d => d.Customer).WithMany(p => p.Tasks).HasConstraintName("FK__Tasks__CustomerI__34C8D9D1");

            entity.HasOne(d => d.Project).WithMany(p => p.Tasks).HasConstraintName("FK__Tasks__ProjectID__35BCFE0A");
        });

        modelBuilder.Entity<TaskLine>(entity =>
        {
            entity.HasKey(e => e.TaskLineId).HasName("PK__TaskLine__41E8C463300AE93F");

            entity.HasOne(d => d.Employee).WithMany(p => p.TaskLines).HasConstraintName("FK__TaskLines__Emplo__44FF419A");

            entity.HasOne(d => d.Inventory).WithMany(p => p.TaskLines).HasConstraintName("FK__TaskLines__Inven__440B1D61");

            entity.HasOne(d => d.Task).WithMany(p => p.TaskLines).HasConstraintName("FK__TaskLines__TaskI__4316F928");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
