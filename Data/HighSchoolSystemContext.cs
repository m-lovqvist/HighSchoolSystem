using System;
using System.Collections.Generic;
using EduBase1.Models;
using Microsoft.EntityFrameworkCore;

namespace EduBase1.Data;

public partial class HighSchoolSystemContext : DbContext
{
    public HighSchoolSystemContext()
    {
    }

    public HighSchoolSystemContext(DbContextOptions<HighSchoolSystemContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Enrollment> Enrollments { get; set; }

    public virtual DbSet<Profession> Professions { get; set; }

    public virtual DbSet<SchoolClass> SchoolClasses { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-64QT8T3;Initial Catalog=HighSchoolSystem;Integrated Security=True;Trusted_Connection=True;Encrypt=false");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Course>(entity =>
        {
            entity.Property(e => e.CourseId)
                .ValueGeneratedNever()
                .HasColumnName("CourseID");
            entity.Property(e => e.EndDate).HasMaxLength(20);
            entity.Property(e => e.FkclassId).HasColumnName("FKClassID");
            entity.Property(e => e.StartDate).HasMaxLength(20);
            entity.Property(e => e.Title).HasMaxLength(30);

            entity.HasOne(d => d.StartDateNavigation).WithMany(p => p.Courses)
                .HasForeignKey(d => d.FkclassId)
                .HasConstraintName("FK_Courses_SchoolClasses");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.DepartmentId).HasName("PK_ClassInfo");

            entity.Property(e => e.DepartmentId)
                .ValueGeneratedNever()
                .HasColumnName("DepartmentID");
            entity.Property(e => e.Name).HasMaxLength(40);
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.Property(e => e.EmployeeId)
                .ValueGeneratedNever()
                .HasColumnName("EmployeeID");
            entity.Property(e => e.Address).HasMaxLength(50);
            entity.Property(e => e.FirstName).HasMaxLength(15);
            entity.Property(e => e.FkdepartmentId).HasColumnName("FKDepartmentID");
            entity.Property(e => e.FkprofessionId).HasColumnName("FKProfessionID");
            entity.Property(e => e.HireDate).HasMaxLength(15);
            entity.Property(e => e.LastName).HasMaxLength(20);
            entity.Property(e => e.Salary).HasColumnType("money");
            entity.Property(e => e.SocialSecurityNumber).HasMaxLength(20);
            entity.Property(e => e.Zip).HasMaxLength(10);

            entity.HasOne(d => d.Fkdepartment).WithMany(p => p.Employees)
                .HasForeignKey(d => d.FkdepartmentId)
                .HasConstraintName("FK_Employees_Departments");

            entity.HasOne(d => d.Fkprofession).WithMany(p => p.Employees)
                .HasForeignKey(d => d.FkprofessionId)
                .HasConstraintName("FK_Employees_Professions");
        });

        modelBuilder.Entity<Enrollment>(entity =>
        {
            entity.HasKey(e => e.EnrollmentId).HasName("PK_CourseLists");

            entity.Property(e => e.EnrollmentId)
                .ValueGeneratedNever()
                .HasColumnName("EnrollmentID");
            entity.Property(e => e.FkcourseId).HasColumnName("FKCourseID");
            entity.Property(e => e.Grade).HasMaxLength(5);
            entity.Property(e => e.SetDate).HasColumnType("datetime");

            entity.HasOne(d => d.Fkcourse).WithMany(p => p.Enrollments)
                .HasForeignKey(d => d.FkcourseId)
                .HasConstraintName("FK_Enrollments_Courses");
        });

        modelBuilder.Entity<Profession>(entity =>
        {
            entity.Property(e => e.ProfessionId)
                .ValueGeneratedNever()
                .HasColumnName("ProfessionID");
            entity.Property(e => e.Name).HasMaxLength(20);
        });

        modelBuilder.Entity<SchoolClass>(entity =>
        {
            entity.HasKey(e => e.ClassId).HasName("PK_Classes");

            entity.Property(e => e.ClassId)
                .HasMaxLength(20)
                .HasColumnName("ClassID");
            entity.Property(e => e.FkemployeeId).HasColumnName("FKEmployeeID");
            entity.Property(e => e.Name).HasMaxLength(40);

            entity.HasOne(d => d.Fkemployee).WithMany(p => p.SchoolClasses)
                .HasForeignKey(d => d.FkemployeeId)
                .HasConstraintName("FK_SchoolClasses_Employees");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.Property(e => e.StudentId)
                .ValueGeneratedNever()
                .HasColumnName("StudentID");
            entity.Property(e => e.Address).HasMaxLength(30);
            entity.Property(e => e.FirstName).HasMaxLength(15);
            entity.Property(e => e.FkclassId)
                .HasMaxLength(20)
                .HasColumnName("FKClassID");
            entity.Property(e => e.LastName).HasMaxLength(20);
            entity.Property(e => e.SocialSecurityNumber).HasMaxLength(20);
            entity.Property(e => e.Zip).HasMaxLength(10);

            entity.HasOne(d => d.Fkclass).WithMany(p => p.Students)
                .HasForeignKey(d => d.FkclassId)
                .HasConstraintName("FK_Students_SchoolClasses");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
