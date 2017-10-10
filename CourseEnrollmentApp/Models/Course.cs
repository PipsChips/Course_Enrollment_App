using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourseEnrollmentApp.Models
{
    public class Course
    {
        public int CourseId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime StartingDate { get; set; }

        public bool Available { get; set; }


        public int AddressId { get; set; }

        public Address Address { get; set; }


        public int TeacherId { get; set; }

        public Teacher Teacher { get; set; }


        public ICollection<Student> Students { get; set; }
    }
}