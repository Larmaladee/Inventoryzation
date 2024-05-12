using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace InventorizationApi.Models
{
    public partial class InventoryDBContext : DbContext
    {
        public InventoryDBContext()
        {
        }

        public InventoryDBContext(DbContextOptions<InventoryDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Application> Applications { get; set; } = null!;
        public virtual DbSet<ApplicationStatus> ApplicationStatuses { get; set; } = null!;
        public virtual DbSet<Class> Classes { get; set; } = null!;
        public virtual DbSet<Equipment> Equipment { get; set; } = null!;
        public virtual DbSet<EquipmentCategory> EquipmentCategories { get; set; } = null!;
        public virtual DbSet<EquipmentMovement> EquipmentMovements { get; set; } = null!;
        public virtual DbSet<EquipmentStatus> EquipmentStatuses { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Application>(entity =>
            {
                entity.ToTable("Application");

                entity.Property(e => e.ApplicationId).HasColumnName("Application_id");

                entity.Property(e => e.Description)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.IdApplicationStatus).HasColumnName("id_ApplicationStatus");

                entity.Property(e => e.IdEquipment).HasColumnName("id_Equipment");

                entity.Property(e => e.IdUser).HasColumnName("id_User");

            });

            modelBuilder.Entity<ApplicationStatus>(entity =>
            {
                entity.ToTable("ApplicationStatus");

                entity.HasIndex(e => e.ApplicationStatus1, "UQ__Applicat__B633E4EB74BEBEBF")
                    .IsUnique();

                entity.Property(e => e.ApplicationStatusId).HasColumnName("ApplicationStatus_id");

                entity.Property(e => e.ApplicationStatus1)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ApplicationStatus");
            });

            modelBuilder.Entity<Class>(entity =>
            {
                entity.ToTable("Class");

                entity.HasIndex(e => e.Class1, "UQ__Class__E81871BCEDB3C03A")
                    .IsUnique();

                entity.Property(e => e.ClassId).HasColumnName("Class_id");

                entity.Property(e => e.Class1)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Class");
            });

            modelBuilder.Entity<Equipment>(entity =>
            {
                entity.HasIndex(e => e.SerialNumber, "UQ__Equipmen__048A0008C0720AFA")
                    .IsUnique();

                entity.Property(e => e.EquipmentId).HasColumnName("Equipment_id");

                entity.Property(e => e.IdClass).HasColumnName("id_Class");

                entity.Property(e => e.IdEquipmentCategory).HasColumnName("id_EquipmentCategory");

                entity.Property(e => e.IdEquipmentStatus).HasColumnName("id_EquipmentStatus");

                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.SerialNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<EquipmentCategory>(entity =>
            {
                entity.ToTable("EquipmentCategory");

                entity.HasIndex(e => e.Category, "UQ__Equipmen__4BB73C32E039B79D")
                    .IsUnique();

                entity.Property(e => e.EquipmentCategoryId).HasColumnName("EquipmentCategory_id");

                entity.Property(e => e.Category)
                    .HasMaxLength(80)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<EquipmentMovement>(entity =>
            {
                entity.HasKey(e => e.MovementId)
                    .HasName("PK__Equipmen__BE67F5B65F891340");

                entity.ToTable("EquipmentMovement");

                entity.Property(e => e.MovementId).HasColumnName("Movement_id");

                entity.Property(e => e.EquipmentId).HasColumnName("Equipment_id");

                entity.Property(e => e.MovementDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Movement_Date");

                entity.Property(e => e.NewClassId).HasColumnName("New_Class_id");

                entity.Property(e => e.NewEquipmentStatusId).HasColumnName("New_EquipmentStatus_id");

                entity.Property(e => e.PreviousClassId).HasColumnName("Previous_Class_id");

                entity.Property(e => e.PreviousEquipmentStatusId).HasColumnName("Previous_EquipmentStatus_id");

            });

            modelBuilder.Entity<EquipmentStatus>(entity =>
            {
                entity.ToTable("EquipmentStatus");

                entity.HasIndex(e => e.EquipmentStatus1, "UQ__Equipmen__BD050F14B1A516AF")
                    .IsUnique();

                entity.Property(e => e.EquipmentStatusId).HasColumnName("EquipmentStatus_id");

                entity.Property(e => e.EquipmentStatus1)
                    .HasMaxLength(80)
                    .IsUnicode(false)
                    .HasColumnName("EquipmentStatus")
                    .HasDefaultValueSql("('В рабочем состоянии')");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.Property(e => e.RoleId).HasColumnName("Role_id");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.UserId).HasColumnName("User_id");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.IdRole).HasColumnName("id_Role");

                entity.Property(e => e.LastName)
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.Login)
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.Surname)
                    .HasMaxLength(80)
                    .IsUnicode(false);

            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
