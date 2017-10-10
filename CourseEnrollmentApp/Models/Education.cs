using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourseEnrollmentApp.Models
{
    public class Education
    {
        public int EducationId { get; set; }

        public string University { get; set; }

        public string Department { get; set; }

        public string AcademicDegree { get; set; }

        public DateTime GraduationDate { get; set; }

        public Student Student { get; set; }
    }
}