using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CourseEnrollmentApp.Models;
using System.Data.Entity;
using CourseEnrollmentApp.ViewModels;
using PagedList;

namespace CourseEnrollmentApp.Controllers
{
    public class StudentEnrollmentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [HttpGet]
        public ActionResult CoursesList(string option, string search)
        {
            IEnumerable<Course> result;

            if (option == "All")
            {
                result = GetCourses().Where(c => c.Name.StartsWith(search)).ToList();
            }
            else if (option == "Available")
            {
                result = GetCourses().Where(c => c.Available == true && c.Name.StartsWith(search)).ToList();
            }
            else
            {
                result = GetCourses().Where(c => c.Name.StartsWith(search) || search == null).ToList();
            }

            return View(result);
        }

        [HttpGet] 
        public ActionResult Enroll(int id)
        {
            var course = db.Courses.Find(id);

            if (course.Available)
            {
                var viewModel = new EnrollViewModel()
                {
                    CourseId = id,
                    Student = new Student()
                };

                return View(viewModel);
            }
            else
            {
                return RedirectToAction("CoursesList");
            }
            
        }

        [HttpPost]
        public ActionResult Enroll(EnrollViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("Enroll", viewModel);
            }
            else
            {
                var student = viewModel.Student;
                student.Address.AddressType = AddressType.StudentAddress;

                var courseId = viewModel.CourseId;

                var enrollment = new Enrollment()
                {
                    CourseId = courseId,
                    StudentId = student.StudentId,
                    DateOfEnrollment = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd H:mm")),
                    Status = EnrollmentStatus.Undefined,
                    ClientIP = Request.UserHostAddress,
                    ClientDNS = Request.UserHostName
                };

                student.Enrollments.Add(enrollment);
                db.Students.Add(student);
                db.SaveChanges();
            }

            return RedirectToAction("CoursesList");
        }

        private IQueryable<Course> GetCourses()
        {
            var courses = db.Courses
                    .Include(c => c.Teacher)
                    .Include(c => c.Address)                    
                    .OrderBy(c => c.StartingDate);

            return courses;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}