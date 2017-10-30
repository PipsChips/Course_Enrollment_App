using CourseEnrollmentApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace CourseEnrollmentApp.EntityConfiguration
{
    public class StudentConfiguration : EntityTypeConfiguration<Student>
    {
        public StudentConfiguration()
        {
            // Student -> Education
            HasOptional(s => s.EducationHistory)
            .WithRequired(e => e.Student);

            // Student -> Enrollment
            HasMany(s => s.Enrollments)
            .WithRequired(e => e.Student)
            .HasForeignKey(e => e.StudentId)
            .WillCascadeOnDelete(true);

            Property(s => s.DateOfBirth)
                .HasColumnType("date");
        }
    }
}