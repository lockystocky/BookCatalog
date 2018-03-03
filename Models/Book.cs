using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookCatalog.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public Person Author { get; set; }

        [DataType(DataType.MultilineText)]
        public string Annotation { get; set; }
        public Genre Genre { get; set; }
        public List<Review> Reviews { get; set; }
        public double AvgRate { get; set; }

    }

    public enum Genre
    {
        Fiction,
        ScienceFiction,
        Drama,
        Travel,
        Horror,
        Poetry
    }

    public class Person
    {
        public int Id { get; set; }

        [Display(Name ="Author first name")]
        public string FirstName { get; set; }

        [Display(Name = "Author last name")]
        public string LastName { get; set; }
    }
}