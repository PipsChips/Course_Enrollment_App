using CourseEnrollmentApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace CourseEnrollmentApp.Controllers
{
    [Authorize]
    public class AdminEnrollmentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [HttpGet]
        public ActionResult Index(string option, string search)
        {
            IEnumerable<Enrollment> enrollments;

            if (option == "Undefined")
            {
                enrollments = GetListOfEnrollmentsByStatusAndSearchValue(EnrollmentStatus.Undefined, search);
            }
            else if (option == "Accepted")
            {
                enrollments = GetListOfEnrollmentsByStatusAndSearchValue(EnrollmentStatus.Accepted, search);
            }
            else if (option == "Declined")
            {
                enrollments = GetListOfEnrollmentsByStatusAndSearchValue(EnrollmentStatus.Declined, search);
            }
            else if (option == "Archived")
            {
                enrollments = GetListOfEnrollmentsByStatusAndSearchValue(EnrollmentStatus.Archived, search);
            }
            else
            {
                enrollments = GetListOfEnrollmentsByStatusAndSearchValue(null, search);
            }

            return View(enrollments);
        }

        [HttpPost]
        public ActionResult Accept(int id)
        {
            var enrollment = Enrollment.GetSingleEnrollmentById(id);

            AcceptEnrollment(enrollment);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Decline(int id)
        {
            var enrollment = Enrollment.GetSingleEnrollmentById(id);

            DeclineEnrollment(enrollment);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Archive(int id)
        {
            var enrollment = Enrollment.GetSingleEnrollmentById(id);

            if (enrollment.Status == EnrollmentStatus.Accepted)
            {
                enrollment.Course.CurrentNumOfStudents -= 1;
                db.Entry(enrollment.Course).State = EntityState.Modified;
            }
            
            enrollment.Status = EnrollmentStatus.Archived;
            db.Entry(enrollment).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var enrollment = Enrollment.GetSingleEnrollmentById(id);

            DeleteEnrollment(enrollment);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult ProcessEnrollments(IEnumerable<int> enrollmentIds, string action)
        {
            var enrollments = db.Enrollments
                    .Include(e => e.Course)
                    .Where(e => enrollmentIds.Contains(e.EnrollmentId))
                    .ToList();

            if (action == "accept")
            {
                foreach (var enrollment in enrollments)
                {
                    AcceptEnrollment(enrollment);
                }
            }
            else if (action == "decline")
            {
                foreach (var enrollment in enrollments)
                {
                    DeclineEnrollment(enrollment);
                }
            }
            else if (action == "archive")
            {
                foreach (var enrollment in enrollments)
                {
                    ArchiveEnrollment(enrollment);
                }
            }
            else 
            {
                foreach (var enrollment in enrollments)
                {
                    DeleteEnrollment(enrollment);
                }
            }

            return RedirectToAction("Index");
        }

        #region Utility_Methods

        private IEnumerable<Enrollment> GetListOfEnrollmentsByStatus(EnrollmentStatus? status)
        {
            var listOfEnrollments = db.Enrollments
                .Where(e => e.Status == status || status == null)
                .Include(e => e.Student)
                .Include(e => e.Course)
                .Include(e => e.Course.Teacher)
                .Include(e => e.Course.Address)
                .OrderByDescending(e => e.DateOfEnrollment)
                .AsEnumerable();

            return listOfEnrollments;
        }

        private IEnumerable<Enrollment> GetListOfEnrollmentsByStatusAndSearchValue(EnrollmentStatus? status, string search)
        {
            IEnumerable<Enrollment> listOfEnrollments;

            if (search == null)
            {
                listOfEnrollments = GetListOfEnrollmentsByStatus(status);
            }
            else
            {
                listOfEnrollments = GetListOfEnrollmentsByStatus(status)
                    .Where(e => e.Student.FirstName.ToLower().StartsWith(search.ToLower()) || search == "");
            }

            return listOfEnrollments;
        }

        private void AcceptEnrollment(Enrollment enrollment)
        {
            enrollment.Status = EnrollmentStatus.Accepted;
            enrollment.Course.CurrentNumOfStudents += 1;

            db.Entry(enrollment).State = EntityState.Modified;
            db.Entry(enrollment.Course).State = EntityState.Modified;

            db.SaveChanges();
        }

        private void DeclineEnrollment(Enrollment enrollment)
        {
            if (enrollment.Status == EnrollmentStatus.Accepted)
            {
                enrollment.Course.CurrentNumOfStudents -= 1;
                db.Entry(enrollment.Course).State = EntityState.Modified;

                if (enrollment.Course.CurrentNumOfStudents < enrollment.Course.MaxNumberOfStudents)
                {
                    enrollment.Course.Available = true;
                }
            }

            enrollment.Status = EnrollmentStatus.Declined;
            db.Entry(enrollment).State = EntityState.Modified;

            db.SaveChanges();
        }

        private void ArchiveEnrollment(Enrollment enrollment)
        {
            if (enrollment.Status == EnrollmentStatus.Accepted)
            {
                enrollment.Course.CurrentNumOfStudents -= 1;
                db.Entry(enrollment.Course).State = EntityState.Modified;

                if (enrollment.Course.CurrentNumOfStudents < enrollment.Course.MaxNumberOfStudents)
                {
                    enrollment.Course.Available = true;
                }
            }

            enrollment.Status = EnrollmentStatus.Archived;
            db.Entry(enrollment).State = EntityState.Modified;

            db.SaveChanges();
        }

        private void DeleteEnrollment(Enrollment enrollment)
        {
            db.Entry(enrollment).State = EntityState.Deleted;
            db.SaveChanges();
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