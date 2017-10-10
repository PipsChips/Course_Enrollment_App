using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CourseEnrollmentApp.Models;

namespace CourseEnrollmentApp.Controllers
{
    public class CoursesController : Controller
    {
        // GET: Courses
        public ActionResult Enrollment()
        {
            var course = new Course() { Name = "ProgramerInternet Aplikacija" };

            return View();
        }
    }
}