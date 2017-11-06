using CourseEnrollmentApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourseEnrollmentApp.ViewModels
{
    public class EnrollViewModel
    {
        public List<int> CourseIds { get; set; }
        public Student Student { get; set; }
    }
}