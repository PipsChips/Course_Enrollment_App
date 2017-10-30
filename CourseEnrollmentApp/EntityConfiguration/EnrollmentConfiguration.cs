using CourseEnrollmentApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace CourseEnrollmentApp.EntityConfiguration
{
    public class EnrollmentConfiguration : EntityTypeConfiguration<Enrollment>
    {
        public EnrollmentConfiguration()
        {
            // Enrollment StudentId Property
            Property(e => e.StudentId)
            .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Enrollment CourseId Property
            Property(e => e.CourseId)
            .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(e => e.Status)
                .IsRequired();

            Property(e => e.CourseId)
                .IsOptional();
        }
    }
}