﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CourseEnrollmentApp.Models
{
    public class Teacher
    {
        public int TeacherId { get; set; }

        [Display(Name = "First name")]
        [Required]
        public string FirstName { get; set; }

        [Display(Name = "Last name")]
        [Required]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Mobile number")]
        public string MobileNumber { get; set; }


        public ICollection<Course> Courses { get; set; }

        public Teacher()
        {
            Courses = new HashSet<Course>();
        }
    }
}