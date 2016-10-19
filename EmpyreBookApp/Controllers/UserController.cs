using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using EmpyreBookApp.Models;
using EmpyreBookApp.DAL;
using EmpyreBookApp.GoogleREST;
using PagedList;
using System.Data.Entity.Infrastructure;
using System.Web.Security;

namespace EmpyreBookApp.Controllers
{
    public class UserController : Controller
    {
        private RequestContext db = new RequestContext();
        private BookSearcher bs = new BookSearcher();
        private static string backgroundColor = "";
        private static string secondColor = "#e8eafc";
        private static string userid = "";
        private static string username = "";
        public JsonResult validUsername(string userName)
        {
            var exists = (from u in db.Users where u.UserName.Contains(userName) select u).Count() > 0;
            if (exists)
            {
                return Json("Username already exists.", JsonRequestBehavior.AllowGet);
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        // GET: /User/
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page, string communityType)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.SchoolSortParm = sortOrder == "school" ? "school_desc" : "school";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var users = from s in db.Users
                            select s;


            if (!String.IsNullOrEmpty(searchString))
            {
                if (String.IsNullOrEmpty(communityType))
                {

                    users = users.Where(s => s.UserName.ToUpper().Contains(searchString.ToUpper())
                                           || s.community.CName.ToUpper().Contains(searchString.ToUpper())
                                           || s.Contact.ToUpper().Contains(searchString.ToUpper())
                                           || s.Email.ToUpper().Contains(searchString.ToUpper()));
                }
                else
                {
                    users = users.Where(s => s.UserName.ToUpper().Contains(searchString.ToUpper())
                                           || s.Contact.ToUpper().Contains(searchString.ToUpper())
                                           || s.Email.ToUpper().Contains(searchString.ToUpper())
                                           && s.community.CName.ToUpper().Equals(communityType.ToUpper()));

                }
            }

            else
            {
                if (!String.IsNullOrEmpty(communityType))
                {
                    users = users.Where(s => s.community.CName.ToUpper().Equals(communityType.ToUpper()));
                }

            }

            switch (sortOrder)
            {
                case "name_desc":
                    users = users.OrderByDescending(s => s.UserName);
                    break;
                case "school":
                    users = users.OrderBy(s => s.community.CName);
                    break;
                case "school_desc":
                    users = users.OrderByDescending(s => s.community.CName);
                    break;
                default:
                    users = users.OrderBy(s => s.UserName);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(users.ToPagedList(pageNumber, pageSize));


        }

        public ActionResult Index2(string sortOrder, string currentFilter, string searchString, int? page, string communityType)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.SchoolSortParm = sortOrder == "school" ? "school_desc" : "school";
            
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            ViewBag.CurrentCommunity = communityType;

            var users = from s in db.Users
                        where s.community.CName.ToUpper().Equals(communityType.ToUpper())
                        select s;

            string url=(from c in db.Communities
                        where c.CName.ToUpper().Equals(communityType.ToUpper())
                        select c.CUrl).First();

            ViewBag.picUrl = url.ToString();


            string backColor = (from c in db.Communities
                          where c.CName.ToUpper().Equals(communityType.ToUpper())
                          select c.BackgroundColor).First();

            ViewBag.backColor = backColor.ToString();


            if (!String.IsNullOrEmpty(searchString))
            {
                
                

                    users = users.Where(s => s.UserName.ToUpper().Contains(searchString.ToUpper()) 
                                           || s.Contact.ToUpper().Contains(searchString.ToUpper())
                                           || s.Email.ToUpper().Contains(searchString.ToUpper()));
               
            }

            

            switch (sortOrder)
            {
                case "name_desc":
                    users = users.OrderByDescending(s => s.UserName);
                    break;
                case "school":
                    users = users.OrderBy(s => s.community.CName);
                    break;
                case "school_desc":
                    users = users.OrderByDescending(s => s.community.CName);
                    break;
                default:
                    users = users.OrderBy(s => s.UserName);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(users.ToPagedList(pageNumber, pageSize));


        }

        
       

        // GET: /User/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            string communityType = (from s in db.Users
                                    where s.UserID == id
                                    select s.community.CName).FirstOrDefault();
            ViewBag.CurrentCommunity = communityType;





            string url = (from c in db.Communities
                          where c.CName.ToUpper().Equals(communityType.ToUpper())
                          select c.CUrl).FirstOrDefault();

            if (url == null)
            {
                ViewBag.picUrl = "~/Images/Empyre logo.png";
            }
            else
            {
                ViewBag.picUrl = url.ToString();
            }
            


            string backColor = (from c in db.Communities
                               where c.CName.ToUpper().Equals(communityType.ToUpper())
                               select c.BackgroundColor).FirstOrDefault();

            if (backColor == null)
            {
                ViewBag.backColor = "";
                backgroundColor = "";
            }
            else
            {
                ViewBag.backColor = backColor.ToString();
                backgroundColor = backColor;
            }

            string secColor = (from c in db.Communities
                                where c.CName.ToUpper().Equals(communityType.ToUpper())
                                select c.SecondaryBackgroundColor).FirstOrDefault();

            if (secColor == null)
            {
                ViewBag.secColor = "";
                secondColor = "#e8eafc";
            }
            else
            {
                ViewBag.secColor = secColor.ToString();
                secondColor = secColor;
            }



            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }



        public ActionResult GetBackground()
        {
            return Content(backgroundColor); // Of whatever you need to return.
        }

        public ActionResult GetSecond()
        {
            return Content(secondColor); // Of whatever you need to return.
        }
        public ActionResult GetHello()
        {
            return Content(username); // Of whatever you need to return.
        }

        public ActionResult GetUserid()
        {
            return Content(userid); // Of whatever you need to return.
        }




        // GET: /User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /User/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LastName,FirstName,UserName,Email,School,Contact")] User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Users.Add(user);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (RetryLimitExceededException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }

            return View(user);
        }

        // GET: /User/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: /User/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserID,LastName,FirstName,UserName,Email,School,Contact,Password")] User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(user).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (RetryLimitExceededException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(user);
        }

        // GET: /User/Delete/5
        public ActionResult Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists see your system administrator.";
            }

            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: /User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                User user = db.Users.Find(id);
                db.Users.Remove(user);
                db.SaveChanges();

            }
            catch (RetryLimitExceededException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return RedirectToAction("Index");
        }

        // GET: /User/Create
        public ActionResult Add()
        {
            ViewBag.GenreID = new SelectList(db.Communities, "GenreID", "Name");
            return View();
        }

        // POST: /User/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(int? id, [Bind(Include = "Description,BookID,ISBN,Condition,Photo,Genre,Price,PostDate")] Models.Book book)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        book.BIID = bs.DbSearch(book.ISBN);
                    }
                    catch (Exception)
                    {
                        ViewBag.ISBNexcept = true;
                        return RedirectToAction("Add");
                    }
                    book.UserID = db.Users.Find(id).UserID;

                    //////convert to pst time

                    DateTime timeUtc = DateTime.UtcNow;
                    try
                    {
                        TimeZoneInfo pstZone = TimeZoneInfo.FindSystemTimeZoneById("Pacific Standard Time");
                        DateTime pstTime = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, pstZone);
                        book.PostDate = pstTime;
                    }
                    catch (TimeZoneNotFoundException)
                    {
                        ModelState.AddModelError("","The registry does not define the Pacific Standard Time zone.");
                    }
                    catch (InvalidTimeZoneException)
                    {
                        ModelState.AddModelError("","Registry data on the Pacific Standard Time zone has been corrupted.");
                    }


                    //////

                    
                    db.Books.Add(book);
                    db.SaveChanges();
                   return RedirectToAction("Details", "User", new {id=userid });
                }
            }
            catch (RetryLimitExceededException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }

            //THIS WAS YOUR ERROR. YOU HAD return View(Request) when it wanted the book.
            return View(book);
        }





        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        public ActionResult IndexNew()
        {
            return View();
        }

        [HttpGet]
        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(Models.User user)
        {
            
            

            if (IsValid(user.Email.ToLower(), user.Password))
            {
                FormsAuthentication.SetAuthCookie(user.Email, false);
                //ModelState.AddModelError("", userid);
                return RedirectToAction("Details", "User", new {id=userid });
            }
            else
            {
                ModelState.AddModelError("", "Login Data is incorrect.");
            }
            //}
            return View(user);
        }
        private bool IsValid(string email, string password)
        {
            //var crypto = new SimpleCrypto.PBKDF2();

            bool isValid = false;

            var user = (from u in db.Users
                        where u.Email.Trim().Equals(email.Trim())
                        select u).First();
            
            if (user != null)
            {
                if (user.Password.Equals(password)) //crypto.Compute(password))

                {

                    userid = user.UserID.ToString();
                    username = user.UserName;

                    isValid = true;
                }
            }

            return isValid;

        }

        [HttpGet]
        public ActionResult Registration()
        {
            ViewBag.CommunityID = new SelectList(db.Communities, "CommunityID", "CName");
            return View();

        }

        [HttpPost]
        public ActionResult Registration(Models.User user)
        {
            if (ModelState.IsValid)
            {
                //var crypto = new SimpleCrypto.PBKDF2();

                //var encrpPass = crypto.Compute(user.Password);

                var User = db.Users.Create();

                User.Email = user.Email.ToLower();
                User.Password = user.Password;
                User.Contact = user.Contact;
                User.FirstName = user.FirstName;
                User.LastName = user.LastName;
                User.ComunityID = user.ComunityID;
                User.UserName = user.UserName;
                //User.UserID = Guid.NewGuid();

                db.Users.Add(user);
                db.SaveChanges();

                return RedirectToAction("Index", "Home");
            }
            return View();

        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            userid = "";
            username = "";
            return RedirectToAction("Index", "Home");
        }


    }
}
