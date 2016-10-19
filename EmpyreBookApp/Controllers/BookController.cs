using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

using EmpyreBookApp.Models;
using EmpyreBookApp.DAL;
using System.Net.Mail;
using EmpyreBookApp.GoogleREST;
using System.Data.Entity.Infrastructure;

namespace EmpyreBookApp.Controllers
{
    public class BookController : Controller
    {
        private RequestContext db = new RequestContext();
        private BookSearcher bs = new BookSearcher();

        // GET: /Book/
        public ActionResult Index(string sortOrder,  string searchString)
        {
            ViewBag.PriceSortParm = String.IsNullOrEmpty(sortOrder) ? "price_desc" : "";
            ViewBag.ISBNSortParm = sortOrder == "ISBN" ? "isbn_desc" : "ISBN";
            ViewBag.GenreOrderParm = sortOrder == "Genre" ? "genre_desc" : "Genre";


            var date = DateTime.Now.AddMinutes(-1);
            var books = from s in db.Books join i in db.BIs on s.ISBN equals i.ISBN
                        where s.PostDate >= date
                          select s;

           

            if (!String.IsNullOrEmpty(searchString))
            {
                books = books.Where(s => s.User.UserName.ToUpper().Contains(searchString.ToUpper())
                                       || s.ISBN.ToUpper().Contains(searchString.ToUpper())
                                       || s.Price.ToString().ToUpper().Contains(searchString.ToString().ToUpper())
                                       || s.Condition.ToUpper().Contains(searchString.ToUpper()));
            }

            switch (sortOrder)
            {
                case "price_desc":
                    books = books.OrderByDescending(s => s.Price);
                    break;
                case "ISBN":
                    books = books.OrderBy(s => s.ISBN);
                    break;
                case "isbn_desc":
                    books = books.OrderByDescending(s => s.ISBN);
                    break;
                case "genre_desc":
                    books = books.OrderBy(s => s.Genre);
                    break;
                default:
                    books = books.OrderBy(s => s.Price);
                    break;
            }
            return View(books.ToList());
        }
        public ActionResult Index2(string sortOrder, string searchString, string communityType)
        {
            ViewBag.PriceSortParm = String.IsNullOrEmpty(sortOrder) ? "price_desc" : "";
            ViewBag.ISBNSortParm = sortOrder == "ISBN" ? "isbn_desc" : "ISBN";
            ViewBag.CurrentCommunity = communityType;

            var date = DateTime.Now.AddMinutes(-1);
            var books = from s in db.Books join u in db.Users on s.UserID equals u.UserID join i in db.BIs on s.ISBN equals i.ISBN
                        where u.community.CName.ToUpper().Equals(communityType.ToUpper()) &&
                        s.PostDate>=date
                        
                        select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                books = books.Where(s => s.User.UserName.ToUpper().Contains(searchString.ToUpper())
                                       || s.ISBN.ToUpper().Contains(searchString.ToUpper())
                                       || s.Price.ToString().ToUpper().Contains(searchString.ToString().ToUpper())
                                       || s.Condition.ToUpper().Contains(searchString.ToUpper()));
            }

            switch (sortOrder)
            {
                case "price_desc":
                    books = books.OrderByDescending(s => s.Price);
                    break;
                case "ISBN":
                    books = books.OrderBy(s => s.ISBN);
                    break;
                case "isbn_desc":
                    books = books.OrderByDescending(s => s.ISBN);
                    break;
                default:
                    books = books.OrderBy(s => s.Price);
                    break;
            }
            return View(books.ToList());
        }

        // GET: /Book/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Models.Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // GET: /Book/Create
        public ActionResult Create()
        {
            ViewBag.UserID = new SelectList(db.Users, "UserID", "LastName");
            return View();
        }

        // POST: /Book/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="BookID,UserID,Title,ISBN,Condition,genre,Price,PostDate")] Models.Book book)
        {
            if (ModelState.IsValid)
            {
                bs.DbSearch(book.ISBN);
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
                    ModelState.AddModelError("", "The registry does not define the Pacific Standard Time zone.");
                }
                catch (InvalidTimeZoneException)
                {
                    ModelState.AddModelError("", "Registry data on the Pacific Standard Time zone has been corrupted.");
                }


                //////
               
                db.Books.Add(book);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserID = new SelectList(db.Users, "UserID", "LastName", book.UserID);
            return View(book);
        }

        // GET: /Book/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Models.Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserID = new SelectList(db.Users, "UserID", "LastName", book.UserID);
            return View(book);
        }

        // POST: /Book/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="BookID,BIID,UserID,SoldTo,ISBN,Condition,Photo,Price,PostDate")] Models.Book book)
        {
            if (ModelState.IsValid)
            {
                string userid1 = book.UserID.ToString();
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "User", new { id = userid1 });
            }
            ViewBag.UserID = new SelectList(db.Users, "UserID", "LastName", book.UserID);
            return View(book);
        }

        // GET: /Book/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Models.Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: /Book/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            
            Models.Book book = db.Books.Find(id);
            db.Books.Remove(book);
            db.SaveChanges();
            return RedirectToAction("Details", "User", new { id = book.UserID });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult Trade()
        {
            //Now send out the email messages
            MailMessage message = new MailMessage();

            //Use a generic made up from address
            message.From = new MailAddress("Empyrecorp@gmail.com");
            message.To.Add(new MailAddress("deathevyn@gmail.com"));


            message.Subject = "A Student is Requesting Your Book.";

            message.Body = "A student is interested in your book at the Empyre Exchange.";
            message.IsBodyHtml = true;
            SmtpClient client = new SmtpClient();
            client.Host = "smtp.gmail.com";
            client.Port = 587;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential("empyrecorp", "seniorproject");
            client.EnableSsl = true;

            client.Send(message);

            return RedirectToAction("Index");

        }
        public ActionResult Display(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Models.Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        public ActionResult Browse(string sortOrder, string searchString, string genre)
        {

            //Gets th
            var things = from s in db.Books select s;
            List<Models.Book> items = things.ToList();
            LinkedList<String> genres = new LinkedList<string>();
            foreach(Models.Book b in items)
            {
                genres.AddLast(b.Genre.Name);
            }
            ViewBag.Genres = new HashSet<string>(genres);

            

            ViewBag.PriceSortParm = String.IsNullOrEmpty(sortOrder) ? "price_desc" : "";
            ViewBag.ISBNSortParm = sortOrder == "ISBN" ? "isbn_desc" : "ISBN";
            ViewBag.GenreOrderParm = sortOrder == "Genre" ? "genre_desc" : "Genre";

            var date = DateTime.Now.AddMinutes(-1);
            var books = (from s in db.Books
                        join i in db.BIs on s.ISBN equals i.ISBN
                        where s.PostDate<=date 
                        select s).GroupBy(t => new {t.BI.Title})
                        .Select(g => g.FirstOrDefault());

            if (!String.IsNullOrEmpty(searchString))
            {
                books = books.Where(s => s.BI.Title.ToUpper().Contains(searchString.ToUpper()));
            }

            if(!String.IsNullOrEmpty(genre))
            {
                books = books.Where(s => s.Genre.Name.ToUpper().Contains(genre.ToUpper()));
            }

            switch (sortOrder)
            {
                case "price_desc":
                    books = books.OrderByDescending(s => s.Price);
                    break;
                case "ISBN":
                    books = books.OrderBy(s => s.ISBN);
                    break;
                case "isbn_desc":
                    books = books.OrderByDescending(s => s.ISBN);
                    break;
                case "genre_desc":
                    books = books.OrderBy(s => s.Genre);
                    break;
                default:
                    books = books.OrderBy(s => s.Price);
                    break;
            }
            return View(books.ToList());
        }

        public ActionResult SpecifiedBook(int? id)
        {
            var title = db.Books.Find(id).BI.Title;
            var distinctBooks = from s in db.Books
                                where s.BI.Title.Equals(title)
                                select s;
            ViewBag.SpecifiedBook = title;
            return View(distinctBooks.ToList());
        }
        public ActionResult Add()
        {
            return View();
        }

        // POST: 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(int? id, [Bind(Include = "Description,BookID,ISBN,Condition,Photo,Genre,Price")] Models.Book book)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        //book.BIID = bs.DbSearch(book.ISBN);
                    }
                    catch (Exception)
                    {
                        ViewBag.ISBNexcept = true;
                        return RedirectToAction("Add");
                    }
                    book.UserID = db.Users.Find(id).UserID;
                    db.Books.Add(book);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (RetryLimitExceededException /* dex */)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(book);
        }
    }
}
