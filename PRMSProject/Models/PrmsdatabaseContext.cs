using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PRMSProject.Models;

public partial class PrmsdatabaseContext : DbContext
{
    public PrmsdatabaseContext()
    {
    }

    public PrmsdatabaseContext(DbContextOptions<PrmsdatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Apartment> Apartments { get; set; }

    public virtual DbSet<Appointment> Appointments { get; set; }

    public virtual DbSet<Building> Buildings { get; set; }

    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<Listing> Listings { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Request> Requests { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Apartment>(entity =>
        {
            entity.HasKey(e => e.ApartmentId).HasName("PK__Apartmen__CBDF57641B0E8B20");

            entity.Property(e => e.ApartmentId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ApartmentInterior)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.ApartmentNumber)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Area)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.BuildingId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PicturesPath)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.RoomAmount).HasColumnType("decimal(2, 1)");

            entity.HasOne(d => d.Building).WithMany(p => p.Apartments)
                .HasForeignKey(d => d.BuildingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Apartment__Build__47A6A41B");

            entity.HasOne(d => d.Owner).WithMany(p => p.ApartmentOwners)
                .HasForeignKey(d => d.OwnerId)
                .HasConstraintName("FK__Apartment__Owner__489AC854");

            entity.HasOne(d => d.Tenant).WithMany(p => p.ApartmentTenants)
                .HasForeignKey(d => d.TenantId)
                .HasConstraintName("FK__Apartment__Tenan__498EEC8D");

            //manually added
            
            entity.HasOne(d => d.Manager)
                    .WithMany(p => p.AparmentManagers)
                    .HasForeignKey(d => d.ManagerId)
                    .HasConstraintName("FK_Apartments_Users_ManagerId");
            
        });

        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.HasKey(e => e.AppointmentId).HasName("PK__Appointm__8ECDFCC239B74581");

            entity.Property(e => e.AppointmentId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ApartmentId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.AppointmentDateTime).HasColumnType("datetime");
            entity.Property(e => e.VisitorName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.VisitorPhone)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Apartment).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.ApartmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Appointme__Apart__503BEA1C");

            entity.HasOne(d => d.Manager).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.ManagerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Appointme__Manag__51300E55");
        });

        modelBuilder.Entity<Building>(entity =>
        {
            entity.HasKey(e => e.BuildingId).HasName("PK__Building__5463CDC433032ECB");

            entity.Property(e => e.BuildingId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.BuildingAddress)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.BuildingName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.BuildingPhoneNumber)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Event>(entity =>
        {
            entity.HasKey(e => e.EventId).HasName("PK__Events__7944C810313BBD46");

            entity.Property(e => e.EventId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ApartmentId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.EventType)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Apartment).WithMany(p => p.Events)
                .HasForeignKey(d => d.ApartmentId)
                .HasConstraintName("FK__Events__Apartmen__607251E5");

            entity.HasOne(d => d.RelatedToNavigation).WithMany(p => p.EventRelatedToNavigations)
                .HasForeignKey(d => d.RelatedTo)
                .HasConstraintName("FK__Events__RelatedT__5F7E2DAC");

            entity.HasOne(d => d.ReportedByNavigation).WithMany(p => p.EventReportedByNavigations)
                .HasForeignKey(d => d.ReportedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Events__Reported__5E8A0973");
        });

        modelBuilder.Entity<Listing>(entity =>
        {
            entity.HasKey(e => e.ListingId).HasName("PK__Listings__BF3EBED03FC23927");

            entity.Property(e => e.ListingId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ApartmentId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.ListingStatus)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Apartment).WithMany(p => p.Listings)
                .HasForeignKey(d => d.ApartmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Listings__Apartm__4C6B5938");

            //entity.HasOne(d => d.Manager).WithMany(p => p.Listings)
            //    .HasForeignKey(d => d.ManagerId)
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("FK__Listings__Manage__4D5F7D71");
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasKey(e => e.MessageId).HasName("PK__Messages__C87C0C9CC7FC08CF");

            entity.Property(e => e.MessageId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.MessageSubject)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.MessageText).HasColumnType("text");
            entity.Property(e => e.SentAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Receiver).WithMany(p => p.MessageReceivers)
                .HasForeignKey(d => d.ReceiverId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Messages__Receiv__55F4C372");

            entity.HasOne(d => d.Sender).WithMany(p => p.MessageSenders)
                .HasForeignKey(d => d.SenderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Messages__Sender__55009F39");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__Payments__9B556A38405EC684");

            entity.Property(e => e.PaymentId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ApartmentId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PaymentMethod)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PaymentStatus)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Apartment).WithMany(p => p.Payments)
                .HasForeignKey(d => d.ApartmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Payments__Apartm__6442E2C9");

            entity.HasOne(d => d.Tenant).WithMany(p => p.Payments)
                .HasForeignKey(d => d.TenantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Payments__Tenant__634EBE90");
        });

        modelBuilder.Entity<Request>(entity =>
        {
            entity.HasKey(e => e.RequestId).HasName("PK__Requests__33A8517AAF3611BF");

            entity.Property(e => e.RequestId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ApartmentId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.RequestStatus)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.RequestText).HasColumnType("text");

            entity.HasOne(d => d.Apartment).WithMany(p => p.Requests)
                .HasForeignKey(d => d.ApartmentId)
                .HasConstraintName("FK__Requests__Apartm__5AB9788F");

            entity.HasOne(d => d.User).WithMany(p => p.Requests)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Requests__UserId__59C55456");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Roles__8AFACE1A2DEF830B");

            entity.HasIndex(e => e.RoleName, "UQ__Roles__8A2B616094F2FFAE").IsUnique();

            entity.Property(e => e.RoleId).ValueGeneratedNever();
            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4CA5C9F674");

            entity.HasIndex(e => e.Email, "UQ__Users__A9D10534E84D5D0E").IsUnique();

            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.UserFullName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UserPassword)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "UserRole",
                    r => r.HasOne<Role>().WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__UserRoles__RoleI__42E1EEFE"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__UserRoles__UserI__41EDCAC5"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId").HasName("PK__UserRole__AF2760ADEDD1968C");
                        j.ToTable("UserRoles");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
