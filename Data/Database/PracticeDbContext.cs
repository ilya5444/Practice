using Microsoft.EntityFrameworkCore;
using WebAPI.Domain.Models;

namespace WebAPI.Data.Database;

public partial class PracticeDbContext : DbContext
{
    public PracticeDbContext()
    {
    }

    public PracticeDbContext(DbContextOptions<PracticeDbContext> options)
        : base(options)
    {
    }

    internal virtual DbSet<Group> Groups { get; set; }

    internal virtual DbSet<Institute> Institutes { get; set; }

    internal virtual DbSet<Role> Roles { get; set; }

    internal virtual DbSet<Specialization> Specializations { get; set; }

    internal virtual DbSet<Student> Students { get; set; }

    internal virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Name=ConnectionStrings:PostgreSQLConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Group>(entity =>
        {
            entity.HasKey(e => e.GroupNumber).HasName("Groups_pkey");

            entity.Property(e => e.GroupNumber)
                .HasMaxLength(7)
                .IsFixedLength()
                .HasColumnName("group_number");
            entity.Property(e => e.Institute).HasColumnName("institute");

            entity.HasOne(d => d.InstituteNavigation).WithMany(p => p.Groups)
                .HasForeignKey(d => d.Institute)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Groups_institute_fkey");
        });

        modelBuilder.Entity<Institute>(entity =>
        {
            entity.HasKey(e => e.InstituteId).HasName("Institutes_pkey");

            entity.Property(e => e.InstituteId)
                .UseIdentityAlwaysColumn()
                .HasIdentityOptions(null, null, null, null, true, null)
                .HasColumnName("institute_id");
            entity.Property(e => e.DeanDegree)
                .HasMaxLength(100)
                .HasColumnName("dean_degree");
            entity.Property(e => e.DeanFirstname)
                .HasMaxLength(25)
                .HasColumnName("dean_firstname");
            entity.Property(e => e.DeanLastname)
                .HasMaxLength(25)
                .HasColumnName("dean_lastname");
            entity.Property(e => e.DeanPatronymic)
                .HasMaxLength(25)
                .HasColumnName("dean_patronymic");
            entity.Property(e => e.InstituteName)
                .HasMaxLength(100)
                .HasColumnName("institute_name");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("Roles_pkey");

            entity.Property(e => e.RoleId)
                .ValueGeneratedNever()
                .HasColumnName("role_id");
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Specialization>(entity =>
        {
            entity.HasKey(e => e.SpecializationId).HasName("Specialization_pkey");

            entity.Property(e => e.SpecializationId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("specialization_id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.StudentId).HasName("Students_pkey");

            entity.Property(e => e.StudentId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("student_id");
            entity.Property(e => e.AdmissionYear).HasColumnName("admission_year");
            entity.Property(e => e.Birthdate).HasColumnName("birthdate");
            entity.Property(e => e.Firstname)
                .HasMaxLength(25)
                .HasColumnName("firstname");
            entity.Property(e => e.Group)
                .HasMaxLength(7)
                .IsFixedLength()
                .HasColumnName("group");
            entity.Property(e => e.Lastname)
                .HasMaxLength(25)
                .HasColumnName("lastname");
            entity.Property(e => e.Specialization).HasColumnName("specialization");
            entity.Property(e => e.Year).HasColumnName("year");

            entity.HasOne(d => d.GroupNavigation).WithMany(p => p.Students)
                .HasForeignKey(d => d.Group)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Students_group_fkey");

            entity.HasOne(d => d.SpecializationNavigation).WithMany(p => p.Students)
                .HasForeignKey(d => d.Specialization)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Students_specialization_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("Users_pkey");

            entity.HasIndex(e => e.Email, "Users_email_key").IsUnique();

            entity.Property(e => e.UserId)
                .UseIdentityAlwaysColumn()
                .HasIdentityOptions(null, null, null, null, true, null)
                .HasColumnName("user_id");
            entity.Property(e => e.Email)
                .HasMaxLength(320)
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(25)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .HasColumnName("password");
            entity.Property(e => e.Role).HasColumnName("role");

            entity.HasOne(d => d.RoleNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.Role)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Users_role_fkey");
        });
    }
}
