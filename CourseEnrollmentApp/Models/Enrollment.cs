using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace CourseEnrollmentApp.Models
{
    public class Enrollment
    {
        public int EnrollmentId { get; set; }

        public int StudentId { get; set; }

        public int? CourseId { get; set; }

        [Display(Name = "Date of enrollment")]
        public DateTime DateOfEnrollment { get; set; }

        public string ClientIP { get; set; }

        public string ClientDNS { get; set; }

        public EnrollmentStatus Status { get; set; }


        public Student Student { get; set; }

        public Course Course { get; set; }


        public static Enrollment GetSingleEnrollmentById(int id)
        {
            using (var db = new ApplicationDbContext())
            {
                var enrollment = db.Enrollments.Include(e => e.Course)
                    .Include(e => e.Course.Address)
                    .Single(e => e.EnrollmentId == id);

                return enrollment;
            }
        }
    }
}