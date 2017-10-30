using CourseEnrollmentApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace CourseEnrollmentApp.EntityConfiguration
{
    public class CourseConfiguration : EntityTypeConfiguration<Course>
    {
        public CourseConfiguration()
        {
            Property(c => c.StartingDate)
                .HasColumnType("date");

            Property(c => c.Name)
                .IsRequired();

            Property(c => c.Description)
                .IsRequired();

            // Course -> Teacher
            HasRequired(c => c.Teacher)
            .WithMany(t => t.Courses)
            .HasForeignKey(c => c.TeacherId)
            .WillCascadeOnDelete(false);

            // Course -> Enrollment
            HasMany(c => c.Enrollments)
            .WithOptional(e => e.Course)
            .HasForeignKey(e => e.CourseId)
            .WillCascadeOnDelete(false);


        }
    }
}