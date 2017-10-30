using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CourseEnrollmentApp.Models
{
    public class Address
    {
        public int AddressId { get; set; }

        public AddressType AddressType { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public string City { get; set; }


        [Display(Name = "Postal code")]
        [Required]
        [DataType(DataType.PostalCode)]
        public string PostalCode { get; set; }

        [Required]
        public string Street { get; set; }

        [Display(Name = "House number")]
        [Required]
        public string HouseNumber { get; set; }

        public ICollection<Course> Courses { get; set; }

        public ICollection<Student> Students { get; set; }

        public Address()
        {
            Courses = new HashSet<Course>();
            Students = new HashSet<Student>();
        }
    }
}