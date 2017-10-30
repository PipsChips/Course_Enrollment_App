using CourseEnrollmentApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace CourseEnrollmentApp.EntityConfiguration
{
    public class TeacherConfiguration : EntityTypeConfiguration<Teacher>
    {
        public TeacherConfiguration() 
        {
            Property(t => t.MobileNumber).IsOptional();

            Property(t => t.FirstName).IsRequired();

            Property(t => t.LastName).IsRequired();

            Property(t => t.Email).IsRequired();
        }
    }
}