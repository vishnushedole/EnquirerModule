using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace EnquiryModule.Models;

[ExcludeFromCodeCoverage]
public partial class EnquiryModuleContext : DbContext
{
    public EnquiryModuleContext()
    {
    }

    public EnquiryModuleContext(DbContextOptions<EnquiryModuleContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DocType> DocTypes { get; set; }

    public virtual DbSet<Document> Documents { get; set; }

    public virtual DbSet<Enquirer> Enquirers { get; set; }

    public virtual DbSet<Manager> Managers { get; set; }

    public virtual DbSet<MgrAssignedEnquire> MgrAssignedEnquires { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(local);Database=EnquiryModule;Integrated Security=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DocType>(entity =>
        {
            entity.HasKey(e => e.DocType1).HasName("PK__DocTypes__21333C970228EABB");

            entity.Property(e => e.DocType1)
                .ValueGeneratedNever()
                .HasColumnName("docType");
            entity.Property(e => e.DocName)
                .HasMaxLength(15)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("docName");
            entity.Property(e => e.Isactive).HasColumnName("isactive");
        });

        modelBuilder.Entity<Document>(entity =>
        {
            entity.HasKey(e => e.DocId).HasName("PK__Document__3EF188AD36C7298D");

            entity.HasOne(d => d.DocType).WithMany(p => p.Documents)
                .HasForeignKey(d => d.DocTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("foreignKey_FK");

            entity.HasOne(d => d.Enq).WithMany(p => p.Documents)
                .HasForeignKey(d => d.EnqId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("foreignKeyEnq_FK");
        });

        modelBuilder.Entity<Enquirer>(entity =>
        {
            entity.HasKey(e => e.EnquiryId).HasName("PK__Enquirer__0A019B7D4B2E5337");

            entity.ToTable("Enquirer");

            entity.Property(e => e.Addr).HasMaxLength(255);
            entity.Property(e => e.City).HasMaxLength(255);
            entity.Property(e => e.Country).HasMaxLength(255);
            entity.Property(e => e.Dob).HasColumnName("DOB");
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.FirstName).HasMaxLength(255);
            entity.Property(e => e.Gender).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(255);
            entity.Property(e => e.MaritalStatus).HasMaxLength(255);
            entity.Property(e => e.PhoneNo).HasMaxLength(15);
            entity.Property(e => e.PinCode).HasMaxLength(6);
            entity.Property(e => e.Stat).HasMaxLength(255);

            entity.HasOne(d => d.Employee).WithMany(p => p.Enquirers)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("foreignKeyEmpId_FK");
        });

        modelBuilder.Entity<Manager>(entity =>
        {
            entity.HasKey(e => e.EmpId).HasName("PK__Managers__263E2DD3225D8FA6");

            entity.Property(e => e.EmpId)
                .ValueGeneratedNever()
                .HasColumnName("Emp_id");
            entity.Property(e => e.AddressLine)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.City)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Country)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength();
        });

        modelBuilder.Entity<MgrAssignedEnquire>(entity =>
        {
            entity.HasKey(e => e.MgrAssignedEnquiresId).HasName("PK__MgrAssig__5E498F88641DDA68");

            entity.Property(e => e.Isprocessed).HasColumnName("isprocessed");

            entity.HasOne(d => d.Emp).WithMany(p => p.MgrAssignedEnquires)
                .HasForeignKey(d => d.EmpId)
                .HasConstraintName("FK_EmpId");

            entity.HasOne(d => d.Enquiry).WithMany(p => p.MgrAssignedEnquires)
                .HasForeignKey(d => d.EnquiryId)
                .HasConstraintName("FK_EnqId");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
