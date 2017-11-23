﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CourseEnrollmentApp.Models;
using CourseEnrollmentApp.ViewModels;

namespace CourseEnrollmentApp.Controllers
{
    [Authorize]
    public class CoursesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [HttpGet]
        public ActionResult Index()
        {
            var courses = db.Courses
                .Include(c => c.Address)
                .Include(c => c.Teacher)
                .Include(c => c.Enrollments)
                .ToList();

            return View(courses);
        }

        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var course = db.Courses
                .Include(c => c.Address)
                .Include(c => c.Teacher)
                .Include(c => c.Enrollments)
                .FirstOrDefault(c => c.CourseId == id);

            var enrollments = db.Enrollments
                .Include(e => e.Student)
                .Where(e => e.CourseId == id)
                .OrderBy(e => e.Student.LastName)
                .ToList();

            course.Enrollments = enrollments;

            if (course == null)
            {
                return HttpNotFound();
            }

            return View(course);
        }

        [HttpGet]
        public ActionResult Create(CourseViewModel model)
        {
            if (model.Course != null)
            {
                return View(model);
            }
            else
            {
                var viewModel = new CourseViewModel()
                {
                    Course = new Course(),
                    Teachers = GetTeachersAsSelectListItems(),
                    Addresses = GetAddressesAsSelectListItems()
                };

                return View(viewModel);
            }
        }

        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCourse(CourseViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var course = viewModel.Course;

                db.Courses.Add(course);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(viewModel);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var course = db.Courses
                .Include(c => c.Address)
                .Include(c => c.Teacher)
                .FirstOrDefault(c => c.CourseId == id);

            var viewModel = new CourseViewModel()
            {
                Course = course,
                Teachers = GetTeachersAsSelectListItems(),
                Addresses = GetAddressesAsSelectListItems()
            };

            if (viewModel.Course == null)
            {
                return HttpNotFound();
            }

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Course course)
        {
            if (course.MaxNumberOfStudents < course.CurrentNumOfStudents)
            {
                ModelState.AddModelError("MaxNumberOfStudents", "Current number of students on course is " + course.CurrentNumOfStudents);
            }

            if (course.Available == true && course.MaxNumberOfStudents == course.CurrentNumOfStudents)
            {
                ModelState.AddModelError("Available", "Course is not available, since it's already full.");
            }

            if (ModelState.IsValid)
            {
                db.Entry(course).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            else
            {
                var viewModel = new CourseViewModel()
                {
                    Course = course,
                    Teachers = GetTeachersAsSelectListItems(),
                    Addresses = GetAddressesAsSelectListItems()

                };

                return View(viewModel);
            }
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var course = db.Courses
                .Include(c => c.Address)
                .Include(c => c.Teacher)
                .Include(c => c.Enrollments)
                .FirstOrDefault(c => c.CourseId == id);

            var enrollments = db.Enrollments
                .Include(e => e.Student)
                .Where(e => e.CourseId == id)
                .ToList();

            course.Enrollments = enrollments;

            if (course == null)
            {
                return HttpNotFound();
            }

            return View(course);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var enrollments = db.Enrollments
                .Where(e => e.CourseId == id)
                .AsEnumerable();

            foreach (var enrollment in enrollments)
            {
                db.Enrollments.Remove(enrollment);
                //enrollment.Status = EnrollmentStatus.Archived;
            }

            Course course = db.Courses.Find(id);
            db.Courses.Remove(course);

            if (Session["Cart"] != null)
            {
                var item = ((IList<Course>)Session["Cart"]).FirstOrDefault(c => c.CourseId == course.CourseId);
                ((IList<Course>)Session["Cart"]).Remove(item);
            }

            db.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult AddNewTeacher(CourseViewModel model)
        {
            TempData["ViewModel"] = model;
            return View();
        }

        [HttpPost]
        public ActionResult AddNewTeacher(Teacher teacher)
        {
            db.Teachers.Add(teacher);
            db.SaveChanges();

            var model = (CourseViewModel)TempData["ViewModel"];
            model.Addresses = new SelectList(GetAddressesAsSelectListItems(), "Value", "Text", model.Course.AddressId);
            model.Teachers = new SelectList(GetTeachersAsSelectListItems(), "Value", "Text", teacher.TeacherId);

            return View("Create", (CourseViewModel)TempData["ViewModel"]);
        }

        [HttpGet]
        public ActionResult AddNewAddress(CourseViewModel model)
        {
            TempData["ViewModel"] = model;
            return View();
        }

        [HttpPost]
        public ActionResult AddNewAddress(Address address)
        {
            address.AddressType = AddressType.CourseLocation;
            db.Addresses.Add(address);
            db.SaveChanges();

            var model = (CourseViewModel)TempData["ViewModel"];
            model.Addresses = new SelectList(GetAddressesAsSelectListItems(), "Value", "Text", address.AddressId.ToString());
            model.Teachers = new SelectList(GetTeachersAsSelectListItems(), "Value", "Text", model.Course.TeacherId);

            return View("Create", (CourseViewModel)TempData["ViewModel"]);
        }

        #region UtilityMethods

        private IEnumerable<SelectListItem> GetTeachersAsSelectListItems()
        {
            var teachers = db.Teachers
                        .Select(t => new SelectListItem
                        {
                            Text = t.FirstName + " " + t.LastName,
                            Value = t.TeacherId.ToString(),
                        }).ToList();

            return teachers;
        }

        private IEnumerable<SelectListItem> GetAddressesAsSelectListItems()
        {
            var addresses = db.Addresses
                            .Where(a => a.AddressType == AddressType.CourseLocation)
                            .Select(a => new SelectListItem
                            {
                                Text = a.Street + " " + a.HouseNumber + ", " + a.City + " " + a.Country,
                                Value = a.AddressId.ToString()
                            }).ToList();

            return addresses;
        }

        #endregion

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
