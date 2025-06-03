using BackEnd.Core.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.EF
{
    public class StudentDbContext : IdentityDbContext<AppUser>
    {
        public StudentDbContext(DbContextOptions<StudentDbContext> options) : base(options)
        {
        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Professor> Professors { get; set; }
        public DbSet<Course>   Courses { get; set; }    
        public DbSet<StudentCourse> StudentCourses { get; set; }
        public DbSet<CourseMaterial> CourseMaterial { get; set; }
        public DbSet<LectureSchedule> LectureSchedules { get; set; }
        public DbSet<Lecture> Lectures { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Student>()
                .HasOne(s => s.AppUser)
                .WithOne(u=> u.student) 
                .HasForeignKey<Student>(s => s.AppUserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Professor>()
               .HasOne(p => p.AppUser)
               .WithOne(u => u.professor)
               .HasForeignKey<Professor>(p => p.AppUserId)
                .OnDelete(DeleteBehavior.Cascade)
               .IsRequired();

            modelBuilder.Entity<StudentCourse>()
                .HasKey(sc => new { sc.StudentId, sc.CourseId });

            modelBuilder.Entity<StudentCourse>()
                .HasOne(sc => sc.Student)
                .WithMany(s => s.StudentCourses)
                .HasForeignKey(sc => sc.StudentId)
                .OnDelete(DeleteBehavior.Restrict);
               

            modelBuilder.Entity<StudentCourse>()
                .HasOne(sc => sc.Course)
                .WithMany(c => c.StudentCourses)
                .HasForeignKey(sc => sc.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Course>()
                .HasOne(c => c.Professor)
                .WithMany(p => p.Courses)
                .HasForeignKey(c => c.ProfessorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CourseMaterial>()
                 .HasOne(m => m.Course)
                 .WithMany(c => c.CourseMaterials)
                 .HasForeignKey(m => m.CourseId)
                 .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Lecture>()
                  .HasOne(l => l.ScheduleLecture)
                  .WithMany(s => s.Lectures)
                  .HasForeignKey(l => l.ScheduleLectureId)
                  .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Attendance>()
                        .HasIndex(a => new { a.StudentId, a.LectureId })
                        .IsUnique();
        }

    }
}
