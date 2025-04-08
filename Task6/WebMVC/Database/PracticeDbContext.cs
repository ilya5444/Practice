using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WebMVC.Models;

namespace WebMVC.Database;

// Этот класс используется для взаимодействия с базой данных
public partial class PracticeDbContext : DbContext
{
    public PracticeDbContext()
    {
    }

    public PracticeDbContext(DbContextOptions<PracticeDbContext> options)
        : base(options)
    {
    }

    // 4 коллекции, которые позволяют работать с таблицами с помощью LINQ
    public virtual DbSet<Group> Groups { get; set; }

    public virtual DbSet<Institute> Institutes { get; set; }

    public virtual DbSet<Specialization> Specializations { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    // Указываются настройки сессии подключения
    // В данном случае указывается, что подключение происходит к PostgreSQL через поставщика Npgsql по указанной строке подключения
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Name=ConnectionStrings:PostgreSQLConnection");

    // В данном методе конфигуриются 4 сущности в соответствии с таблицами в БД
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

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
