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
        public ActionResult Enroll(params int[] CourseId)
        {
            var viewModel = new EnrollViewModel();
            viewModel.Student = new Student();
            viewModel.CourseIds = new List<int>();

            foreach (var id in CourseId)
            {
                viewModel.CourseIds.Add(id);
            }
            
            return View(viewModel);
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
                if (Session["AlreadyAppliedCourses"] == null)
                {
                    Session["AlreadyAppliedCourses"] = new List<int>();
                }

                var student = viewModel.Student;
                student.Address.AddressType = AddressType.StudentAddress;

                var courseIds = viewModel.CourseIds;

                for (int i = 0; i < courseIds.Count(); i++)
                {
                    var enrollment = new Enrollment()
                    {
                        CourseId = courseIds[i],
                        StudentId = student.StudentId,
                        DateOfEnrollment = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd H:mm")),
                        Status = EnrollmentStatus.Undefined,
                        ClientIP = Request.UserHostAddress,
                        ClientDNS = Request.UserHostName
                    };

                    student.Enrollments.Add(enrollment);
                    db.Students.Add(student);

                    ((IList<int>)Session["AlreadyAppliedCourses"]).Add(courseIds[i]);

                    if (Session["Cart"] != null)
                    {
                        Session["Cart"] = ((IList<Course>)Session["Cart"]).Where(c => c.CourseId != courseIds[i]).ToList();
                    }
                    else
                    {
                        Session["Cart"] = new List<Course>();
                    }
                }

                db.SaveChanges();
            }

            ViewBag.Message = "You've successfully applied to this course(s)!";

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