using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TeamHubSessionsServices.Entities;

public partial class TeamHubContext : DbContext
{
    public TeamHubContext()
    {
    }

    public TeamHubContext(DbContextOptions<TeamHubContext> options)
        : base(options)
    {
    }

    public virtual DbSet<document> document { get; set; }

    public virtual DbSet<extension> extension { get; set; }

    public virtual DbSet<project> project { get; set; }

    public virtual DbSet<projectstudent> projectstudent { get; set; }

    public virtual DbSet<student> student { get; set; }

    public virtual DbSet<studentsession> studentsession { get; set; }

    public virtual DbSet<tasks> tasks { get; set; }

    public virtual DbSet<taskstudent> taskstudent { get; set; }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
       // => optionsBuilder.UseMySQL("server=localhost;port=3309;database=teamhub_db;user=root;password=1234");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<document>(entity =>
        {
            entity.HasKey(e => e.IdDocument).HasName("PRIMARY");

            entity.HasIndex(e => e.Extension, "document_extension_idx");

            entity.HasIndex(e => e.IdProject, "document_project_idx");

            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Path).HasMaxLength(250);

            entity.HasOne(d => d.ExtensionNavigation).WithMany(p => p.document)
                .HasForeignKey(d => d.Extension)
                .HasConstraintName("document_extension");

            entity.HasOne(d => d.IdProjectNavigation).WithMany(p => p.document)
                .HasForeignKey(d => d.IdProject)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("document_project");
        });

        modelBuilder.Entity<extension>(entity =>
        {
            entity.HasKey(e => e.IdExtension).HasName("PRIMARY");

            entity.Property(e => e.Extension1)
                .HasMaxLength(45)
                .HasColumnName("Extension");
        });

        modelBuilder.Entity<project>(entity =>
        {
            entity.HasKey(e => e.IdProject).HasName("PRIMARY");

            entity.Property(e => e.EndDate).HasColumnType("date");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.StartDate).HasColumnType("date");
        });

        modelBuilder.Entity<projectstudent>(entity =>
        {
            entity.HasKey(e => e.IdProjectStudent).HasName("PRIMARY");

            entity.HasIndex(e => e.IdProject, "IdProject");

            entity.HasIndex(e => e.IdStudent, "IdStudent");

            entity.HasOne(d => d.IdProjectNavigation).WithMany(p => p.projectstudent)
                .HasForeignKey(d => d.IdProject)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("projectstudent_ibfk_1");

            entity.HasOne(d => d.IdStudentNavigation).WithMany(p => p.projectstudent)
                .HasForeignKey(d => d.IdStudent)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("projectstudent_ibfk_2");
        });

        modelBuilder.Entity<student>(entity =>
        {
            entity.HasKey(e => e.IdStudent).HasName("PRIMARY");

            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(15);
            entity.Property(e => e.MiddleName).HasMaxLength(15);
            entity.Property(e => e.Name).HasMaxLength(15);
            entity.Property(e => e.Password).HasMaxLength(200);
            entity.Property(e => e.ProDocumentImage).HasMaxLength(250);
            entity.Property(e => e.SurName).HasMaxLength(15);
        });

        modelBuilder.Entity<studentsession>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.HasIndex(e => e.IdStudent, "session_student_idx");

            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.IPAdress).HasMaxLength(30);
            entity.Property(e => e.StartDate).HasColumnType("datetime");

            entity.HasOne(d => d.IdStudentNavigation).WithMany(p => p.studentsession)
                .HasForeignKey(d => d.IdStudent)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("session_student");
        });

        modelBuilder.Entity<tasks>(entity =>
        {
            entity.HasKey(e => e.IdTask).HasName("PRIMARY");

            entity.HasIndex(e => e.IdProject, "task_project_idx");

            entity.Property(e => e.Description).HasMaxLength(250);
            entity.Property(e => e.EndDate).HasColumnType("date");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.StartDate).HasColumnType("date");

            entity.HasOne(d => d.IdProjectNavigation).WithMany(p => p.tasks)
                .HasForeignKey(d => d.IdProject)
                .HasConstraintName("task_project");
        });

        modelBuilder.Entity<taskstudent>(entity =>
        {
            entity.HasKey(e => e.IdTaskStudent).HasName("PRIMARY");

            entity.HasIndex(e => e.IdStudent, "IdStudent");

            entity.HasIndex(e => e.IdTask, "IdTask");

            entity.HasOne(d => d.IdStudentNavigation).WithMany(p => p.taskstudent)
                .HasForeignKey(d => d.IdStudent)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("taskstudent_ibfk_2");

            entity.HasOne(d => d.IdTaskNavigation).WithMany(p => p.taskstudent)
                .HasForeignKey(d => d.IdTask)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("taskstudent_ibfk_1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
