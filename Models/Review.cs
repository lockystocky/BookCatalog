using System;
using System.ComponentModel.DataAnnotations;

namespace BookCatalog.Models
{
    public class Review
    {
        public int Id { get; set; }
        public  DateTime Date { get; set; }
        public User Author { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        public double Rate { get; set; }
    }
}