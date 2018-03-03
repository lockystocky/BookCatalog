using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace BookCatalog.Models
{
    public class BookDbContextInitializer : DropCreateDatabaseIfModelChanges<BookDbContext>
    {
        protected override void Seed(BookDbContext context)
        {
            context.Books.Add(new Book
            {
                Title = "Lolita",
                Annotation = "Book about woman",
                Genre = Genre.Drama,
                Author = new Person { FirstName = "Vladimir", LastName = "Nabokov" },
                Reviews =
                {
                    new Review
                    {
                        Rate = 4, Date = new DateTime(2018, 1, 12),
                        Author = new User
                        {
                            FirstName = "John",
                            LastName = "Doe",
                            Email = "john1@gmail.com",
                            BirthDate = new DateTime(1989, 4, 5)
                        },
                        Description = "Lolita is a 1955 novel written by Russian American novelist Vladimir Nabokov. " +
                "The novel is notable for its controversial subject: the protagonist and unreliable narrator—a " +
                "middle-aged literature professor called Humbert Humbert—is obsessed with the 12-year-old Dolores" +
                " Haze, with whom he becomes sexually involved after he becomes her stepfather. \"Lolita\" is his private" +
                " nickname for Dolores. The novel was originally written in English and first published in Paris in 1955" +
                " by Olympia Press. Later it was translated into Russian by Nabokov himself and published in New York City " +
                "in 1967 by Phaedra Publishers.\n",
                    }
                }
            });

            context.Books.Add(new Book
            {
                Title = "It",
                Annotation = "Book about murder",
                Genre = Genre.Drama,
                Author = new Person { FirstName = "Stephen", LastName = "King" },
                Reviews =
                {
                    new Review
                    {
                        Rate = 5, Date = new DateTime(2016, 1, 12),
                        Author = new User
                        {
                            FirstName = "John",
                            LastName = "Doe",
                            Email = "john1@gmail.com",
                            BirthDate = new DateTime(1999, 4, 5)
                        },
                        Description = "Lolita is a 1955 novel written by Russian American novelist Vladimir Nabokov. " +
                "The novel is notable for its controversial subject: the protagonist and unreliable narrator—a " +
                "middle-aged literature professor called Humbert Humbert—is obsessed with the 12-year-old Dolores" +
                " Haze, with whom he becomes sexually involved after he becomes her stepfather. \"Lolita\" is his private" +
                " nickname for Dolores. The novel was originally written in English and first published in Paris in 1955" +
                " by Olympia Press. Later it was translated into Russian by Nabokov himself and published in New York City " +
                "in 1967 by Phaedra Publishers.\n",
                    }
                }
            });

            context.SaveChanges();

            base.Seed(context);
        }
    }
}