using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PL;

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

    public virtual DbSet<Courses> tblCourses { get; set; }

    public virtual DbSet<Collections> tblCollections { get; set; }

    public virtual DbSet<Users> tblUsers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

        //remote
        //optionsBuilder.UseSqlServer("Server=tcp:peerenboom.database.windows.net,1433; Initial Catalog = peerenboom; Persist Security Info = False; User ID = peerenboom; Password =Test123!; MultipleActiveResultSets = False; Encrypt = True; TrustServerCertificate = False; Connection Timeout = 30;");
        //local
        optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=Elevate.DB;Integrated Security=True");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Courses>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tblCourses__3214EC07BA3D27D6");

            entity.ToTable("Courses");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Collections>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tblCourses__3214EC0797875A24");

            entity.ToTable("Collections");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Users>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tblUser__3214EC07B7DF0B2F");

            entity.ToTable("Users");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(28)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}