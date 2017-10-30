using CourseEnrollmentApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace CourseEnrollmentApp.EntityConfiguration
{
    public class AddressConfiguration : EntityTypeConfiguration<Address>
    {
        public AddressConfiguration()
        {
            Property(a => a.AddressType).IsRequired().HasColumnOrder(2);

            Property(a => a.Country).IsRequired();

            Property(a => a.City).IsRequired();

            Property(a => a.PostalCode).IsRequired();

            Property(a => a.Street).IsRequired();

            Property(a => a.HouseNumber).IsRequired();

            // Address -> Course
            HasMany(a => a.Courses)
            .WithRequired(c => c.Address)
            .HasForeignKey(c => c.AddressId)
            .WillCascadeOnDelete(false);

            // Address -> Student
            HasMany(a => a.Students)
            .WithRequired(s => s.Address)
            .HasForeignKey(s => s.AddressId)
            .WillCascadeOnDelete(false);
        }
    }
}