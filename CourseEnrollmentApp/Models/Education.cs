using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CourseEnrollmentApp.Models
{
    public class Education
    {
        public int EducationId { get; set; }

        public string University { get; set; }

        public string Department { get; set; }

        [Display(Name = "Academic Degree (Title)")]
        public string AcademicDegree { get; set; }

        [Display(Name = "Graduation Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? GraduationDate { get; set; }

        public Student Student { get; set; }
    }
}