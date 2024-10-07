using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Hotelier_web.Models;

public partial class HotelDbContext : DbContext
{
    public HotelDbContext()
    {
    }

    public HotelDbContext(DbContextOptions<HotelDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Guest> Guests { get; set; }

    public virtual DbSet<Permission> Permissions { get; set; }

    public virtual DbSet<Reservation> Reservations { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Database=hotel_db;Username=hotel_admin;Password=hotel");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Guest>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("guests_pkey");

            entity.ToTable("guests");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Citizenship).HasColumnName("citizenship");
            entity.Property(e => e.DateAdded)
                .HasDefaultValueSql("'2024-10-03 10:37:50.101029'::timestamp without time zone")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("date_added");
            entity.Property(e => e.DateOfBirth).HasColumnName("date_of_birth");
            entity.Property(e => e.DateUpdated)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("date_updated");
            entity.Property(e => e.EMail).HasColumnName("e_mail");
            entity.Property(e => e.FirstName).HasColumnName("first_name");
            entity.Property(e => e.IsFirstVisit).HasColumnName("is_first_visit");
            entity.Property(e => e.LastName).HasColumnName("last_name");
            entity.Property(e => e.Phone).HasColumnName("phone");
        });

        modelBuilder.Entity<Permission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("permissions_pkey");

            entity.ToTable("permissions");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreateReservations).HasColumnName("create_reservations");
            entity.Property(e => e.DateAdded)
                .HasDefaultValueSql("'2024-10-03 10:37:50.01225'::timestamp without time zone")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("date_added");
            entity.Property(e => e.DateUpdated)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("date_updated");
            entity.Property(e => e.DeleteReservations).HasColumnName("delete_reservations");
            entity.Property(e => e.EditReservations).HasColumnName("edit_reservations");
            entity.Property(e => e.ManageReservations).HasColumnName("manage_reservations");
        });

        modelBuilder.Entity<Reservation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("reservations_pkey");

            entity.ToTable("reservations");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Commentary).HasColumnName("commentary");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.DateAdded)
                .HasDefaultValueSql("'2024-10-03 10:37:50.179321'::timestamp without time zone")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("date_added");
            entity.Property(e => e.DateArrival).HasColumnName("date_arrival");
            entity.Property(e => e.DateDeparture).HasColumnName("date_departure");
            entity.Property(e => e.GuestCount).HasColumnName("guest_count");
            entity.Property(e => e.IsCancelled).HasColumnName("is_cancelled");
            entity.Property(e => e.LastUpdateBy).HasColumnName("last_update_by");
            entity.Property(e => e.PaymentMethod).HasColumnName("payment_method");
            entity.Property(e => e.TotalCost).HasColumnName("total_cost");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.ReservationCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("reservations_created_by_fkey");

            entity.HasOne(d => d.LastUpdateByNavigation).WithMany(p => p.ReservationLastUpdateByNavigations)
                .HasForeignKey(d => d.LastUpdateBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("reservations_last_update_by_fkey");

            entity.HasMany(d => d.Guests).WithMany(p => p.Reservations)
                .UsingEntity<Dictionary<string, object>>(
                    "ReservationsGuest",
                    r => r.HasOne<Guest>().WithMany()
                        .HasForeignKey("GuestId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("reservations_guests_guest_id_fkey"),
                    l => l.HasOne<Reservation>().WithMany()
                        .HasForeignKey("ReservationId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("reservations_guests_reservation_id_fkey"),
                    j =>
                    {
                        j.HasKey("ReservationId", "GuestId").HasName("reservations_guests_pkey");
                        j.ToTable("reservations_guests");
                        j.IndexerProperty<int>("ReservationId").HasColumnName("reservation_id");
                        j.IndexerProperty<int>("GuestId").HasColumnName("guest_id");
                    });
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("roles_pkey");

            entity.ToTable("roles");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DateAdded)
                .HasDefaultValueSql("'2024-10-03 10:37:49.9091'::timestamp without time zone")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("date_added");
            entity.Property(e => e.DateUpdated)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("date_updated");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.Title).HasColumnName("title");

            entity.HasMany(d => d.Permissions).WithMany(p => p.Roles)
                .UsingEntity<Dictionary<string, object>>(
                    "RolesPermission",
                    r => r.HasOne<Permission>().WithMany()
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("roles_permissions_permission_id_fkey"),
                    l => l.HasOne<Role>().WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("roles_permissions_role_id_fkey"),
                    j =>
                    {
                        j.HasKey("RoleId", "PermissionId").HasName("roles_permissions_pkey");
                        j.ToTable("roles_permissions");
                        j.IndexerProperty<int>("RoleId").HasColumnName("role_id");
                        j.IndexerProperty<int>("PermissionId").HasColumnName("permission_id");
                    });
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_pkey");

            entity.ToTable("users");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DateAdded)
                .HasDefaultValueSql("'2024-10-03 10:37:49.399168'::timestamp without time zone")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("date_added");
            entity.Property(e => e.EMail).HasColumnName("e_mail");
            entity.Property(e => e.FirstName).HasColumnName("first_name");
            entity.Property(e => e.LastLogin)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("last_login");
            entity.Property(e => e.LastName).HasColumnName("last_name");
            entity.Property(e => e.Login).HasColumnName("login");
            entity.Property(e => e.Password).HasColumnName("password");
            entity.Property(e => e.Phone).HasColumnName("phone");
            entity.Property(e => e.RoleId).HasColumnName("role_id");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("users_role_id_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
