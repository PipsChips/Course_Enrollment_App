using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CourseEnrollmentApp.Models
{
    public class Student
    {
        public int StudentId { get; set; }

        [Display(Name = "First name")]
        [Required]
        public string FirstName { get; set; }

        [Display(Name = "Last name")]
        [Required]
        public string LastName { get; set; }

        [Display(Name = "Date of birth")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        //[DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required]
        public string Email { get; set; }

        [Display(Name = "Telephone number")]
        [DataType(DataType.PhoneNumber)]
        public int? TelephoneNumber { get; set; }

        [Display(Name = "Mobile number")]
        [DataType(DataType.PhoneNumber)]
        public int MobileNumber { get; set; }

        public int AddressId { get; set; }

        [Required]
        public Address Address { get; set; }


        public Education EducationHistory { get; set; }


        public ICollection<Enrollment> Enrollments { get; set; }

        public Student()
        {
            Enrollments = new HashSet<Enrollment>();
        }
    }
}