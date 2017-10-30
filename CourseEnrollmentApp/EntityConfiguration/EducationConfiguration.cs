using CourseEnrollmentApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace CourseEnrollmentApp.EntityConfiguration
{
    public class EducationConfiguration : EntityTypeConfiguration<Education>
    {
        public EducationConfiguration()
        {
            Property(e => e.GraduationDate)
                .HasColumnType("date");
        }
    }
}