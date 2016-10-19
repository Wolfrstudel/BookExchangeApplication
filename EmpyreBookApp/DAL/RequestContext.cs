using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity;
using EmpyreBookApp.Models;

namespace EmpyreBookApp.DAL
{
    public class RequestContext:DbContext

    {

        public RequestContext()
            //: base("Empyre_")
            : base("RequestContext")
        {
        }

        public DbSet<BI> BIs { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<Trade> Tradets { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Genre> Genres { get; set; }

        public DbSet<Community> Communities { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }







    }
}