using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BookCatalog.Models;
using Microsoft.AspNet.Identity;

namespace BookCatalog.Controllers
{
    public class ReviewsController : Controller
    {
       /* // GET: Reviews
        public ActionResult Index()
        {
            return View();
        }*/

        private BookDbContext db = new BookDbContext();

        //GET: Reviews/bookId
        public ActionResult Reviews(int? bookId)
        {
            if (bookId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var reviewsWithAuthor = db.Reviews
                .Include(r => r.Author).ToList();

            List<Review> reviews = new List<Review>();

            Book book = db.Books
                .Where(b => b.Id == bookId)
                .Include(b => b.Reviews)
                .Include(b => b.Author)
                .FirstOrDefault();

            foreach (Review review in reviewsWithAuthor)
            {
                if (book.Reviews.Contains(review)) reviews.Add(review);
            }

            book.Reviews = reviews.OrderBy(r => r.Date).ToList();

            return View(book);
        }

        //GET: CreateReview/bookId
        [Authorize]
        public ActionResult CreateReview(int? bookId)
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult CreateReview(int? bookId, Review newReview)
        {
            if (ModelState.IsValid)
            {
                Book book = db.Books
                    .Where(b => b.Id == bookId)
                    .Include(b => b.Reviews)
                    .FirstOrDefault();

                var context = new ApplicationDbContext();
                var currentUserId = User.Identity.GetUserId();
                var currentUser = context.Users.FirstOrDefault(u => u.Id == currentUserId);

                var review = new Review
                {
                    Description = newReview.Description,
                    Rate = newReview.Rate,
                    Date = DateTime.Now,
                    Author = new User
                    {
                        FirstName = currentUser.FirstName,
                        LastName = currentUser.LastName,
                        BirthDate = currentUser.BirthDate,
                        Email = currentUser.Email
                    }
                };

                if (book.Reviews == null) book.Reviews = new List<Review>();
                book.Reviews.Add(review);
                double avgRate = book.Reviews.Average(r => r.Rate);
                book.AvgRate = avgRate;
                db.SaveChanges();
                return RedirectToAction("Reviews", new { bookID = bookId });
            }

            return View(newReview);
        }
    }
}