using System.Web.Mvc;
using EmpyreBookApp.DAL;
using EmpyreBookApp.ViewModels;
using System.Linq;
using EmpyreBookApp.GoogleREST;
using System;
using System.Collections.Generic;

namespace EmpyreBookApp.Controllers
{
    public class HomeController : Controller
    {
        private RequestContext db = new RequestContext();
        public ActionResult Index()

        {
            ViewBag.communityType = new SelectList(db.Communities, "CName", "CName");
            return View();
        }

        public ActionResult About()
        {
            IQueryable<UserSchoolGroup> data = from user in db.Users
                                                   group user by user.community.CName into fromschool
                                               select new UserSchoolGroup()
                                                   {
                                                        FromSchool = fromschool.Key,
                                                       StudentCount = fromschool.Count()
                                                   };
            return View(data.ToList());
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult ISBNSearch(string searchString)
        {
            if (searchString != null && (searchString.Length == 10 || searchString.Length == 13))
            {
                try
                {
                    BookSearcher bs = new BookSearcher();
                    EmpyreBookApp.GoogleREST.Book[] books = bs.searchISBN(searchString);
                    if (books == null)
                    {
                        ViewBag.Message = "No Results";
                    }
                    else
                    {
                        EmpyreBookApp.GoogleREST.Book b1 = books.First<EmpyreBookApp.GoogleREST.Book>();
                        BookInfo model = bs.parse2BI(b1, searchString);
                        ViewBag.Message = model.toString();

                    }
                }
                catch (Exception)
                {
                    ViewBag.Message = "Errors have occered. Please view Contact page to contact the app devs.";
                }
            }
            else
            {
                ViewBag.Message = "ISBN must be 10 or 13 digits";

            }
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        /*public ActionResult SelectCategory()
        {

            List<SelectListItem> items = new List<SelectListItem>();

            items.Add(new SelectListItem { Text = "---Please Choose a Community---", Value = "0" });

            items.Add(new SelectListItem { Text = "Western Oregon University", Value = "Western Oregon University" });

            items.Add(new SelectListItem { Text = "Oregon State University", Value = "Oregon State University" });

            

            ViewBag.CommunityType = items;

            return View();

        }*/

    }
}