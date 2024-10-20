using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Elevate.PL;

public partial class ElevateEntities : DbContext
{
    public ElevateEntities()
    {
    }

    public ElevateEntities(DbContextOptions<ElevateEntities> options)
        : base(options)
    {
    }

    public virtual DbSet<tblCollection> tblCollections { get; set; }

    public virtual DbSet<tblCourse> tblCourses { get; set; }

    public virtual DbSet<tblCustomer> tblCustomers { get; set; }

    public virtual DbSet<tblOrder> tblOrders { get; set; }

    public virtual DbSet<tblOrderItem> tblOrderItems { get; set; }

    public virtual DbSet<tblUser> tblUsers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=Elevate.DB;Integrated Security=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<tblCollection>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tblColle__3214EC07D8A319DB");

            entity.ToTable("tblCollection");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<tblCourse>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tblCours__3214EC078CAAB492");

            entity.ToTable("tblCourse");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<tblCustomer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tblCusto__3214EC079CC07D56");

            entity.ToTable("tblCustomer");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Email)
                .HasMaxLength(35)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<tblOrder>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tblOrder__3214EC077DCE2E1F");

            entity.ToTable("tblOrder");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.OrderDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<tblOrderItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tblOrder__3214EC07E70C4FE2");

            entity.ToTable("tblOrderItem");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<tblUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tblUser__3214EC07A6DD9573");

            entity.ToTable("tblUser");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
