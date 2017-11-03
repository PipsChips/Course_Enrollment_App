using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CourseEnrollmentApp.Models;
using System.Data.Entity;

namespace CourseEnrollmentApp.Controllers
{
    public class CartController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [HttpGet]
        public ActionResult AddToCart(int id)
        {
            var course = db.Courses.Include(c => c.Teacher)
                .Include(c => c.Address)
                .Where(c => c.CourseId == id)
                .First();

            if (Session["Cart"] == null)
            {
                List<Course> coursesInCart = new List<Course>();
                coursesInCart.Add(course);

                Session["Cart"] = coursesInCart;
            }
            else
            {
                ((IList<Course>)Session["Cart"]).Add(course);
            }

            return RedirectToAction("CoursesList", "StudentEnrollments");
        }

        [HttpGet]
        public ActionResult CartItems()
        {
            return View((IEnumerable<Course>)Session["Cart"]);
        }

        [HttpGet]
        public ActionResult RemoveItem(int id)
        {
            var itemToRemove = ((IList<Course>)Session["Cart"]).Single(c => c.CourseId == id);

            ((IList<Course>)Session["Cart"]).Remove(itemToRemove);

            return View("CartItems", (IEnumerable<Course>)Session["Cart"]);
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