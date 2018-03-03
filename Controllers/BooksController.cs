using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using BookCatalog.Models;
using PagedList;
using Microsoft.AspNet.Identity;
using System.Net;
using System.IO;

namespace BookCatalog.Controllers
{
    public class BooksController : Controller
    {
        private BookDbContext db = new BookDbContext();

        // GET: Books
        public ActionResult Index(int? page)
        {
          //  int pageSize = 2;
            int pageNumber = (page ?? 1);
            var books = db.Books
                .Include(b => b.Reviews)
                .Include(b => b.Author)
                .OrderBy(b => b.Title)
                .ToList();
                //.ToPagedList(pageNumber, pageSize);

            return View(books);
        }

       

        /* using (StreamWriter sw = new StreamWriter("D:\\test.txt"))
                  {
                      sw.WriteLine(DateTime.Now.ToString());
                      sw.WriteLine(book == null);
                      sw.WriteLine(review == null);
                      sw.WriteLine(book.Reviews == null);
                  }*/

        // GET: Books/CreateBook
        public ActionResult CreateBook()
        {
            return View();
        }

        // POST: Books/CreateBook
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult CreateBook(Book book, Person author)
        {
            if (ModelState.IsValid)
            {
                if(author != null) book.Author = author;
                db.Books.Add(book);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(book);
        }

        // GET: Books/SearchBooks/searchword
        public ActionResult SearchBooks(string searchword)
        {
            if (searchword == "")
            {
                var allBooks = db.Books
                .Include(b => b.Author)
                .OrderBy(b => b.Title)
                .ToList();

                return Json(allBooks, JsonRequestBehavior.AllowGet);
            }

            var books = db.Books
               .Include(b => b.Author)
               .Where(b => b.Title.ToLower().Contains(searchword.ToLower()))
               .ToList();

            return Json(books, JsonRequestBehavior.AllowGet);
        }

        // GET: Books/SortedBooks/sortParam
        public ActionResult SortedBooks(string sortParam)
        {          
            var books = db.Books
                .Include(b => b.Author);

            switch (sortParam)
            {
                case "title_desc":
                    books = books.OrderByDescending(b => b.Title);
                    break;

                case "title_asc":
                    books = books.OrderBy(b => b.Title);
                    break;

                case "rate_desc":
                    books = books.OrderByDescending(b => b.AvgRate);
                    break;

                case "rate_asc":
                    books = books.OrderByDescending(b => b.AvgRate);
                    break;
            }


            //return books.ToPagedList(1, 2);
            return Json(books.ToList(), JsonRequestBehavior.AllowGet);
        }

        

    }
}