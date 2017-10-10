using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourseEnrollmentApp.Models
{
    public class Student
    {
        public int StudentId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Email { get; set; }

        public string TelephoneNumber { get; set; }

        public string MobileNumber { get; set; }


        public int AddressId { get; set; }

        public Address Address { get; set; }

        public ICollection<Course> Courses { get; set; }

        public Education EducationHistory { get; set; }
    }
}