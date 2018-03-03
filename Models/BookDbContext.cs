﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BookCatalog.Models
{
    public class BookDbContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx

        public BookDbContext() : base("name=BookDbContext")
        {
            Database.SetInitializer<BookDbContext>(new BookDbContextInitializer());
        }

        public DbSet<BookCatalog.Models.Review> Reviews { get; set; }
        public DbSet<BookCatalog.Models.Book> Books { get; set; }
        public DbSet<BookCatalog.Models.User> Users { get; set; }
    }
}
