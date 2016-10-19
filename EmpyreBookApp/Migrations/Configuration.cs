namespace EmpyreBookApp.Migrations
{
    using EmpyreBookApp.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;


    internal sealed class Configuration : DbMigrationsConfiguration<EmpyreBookApp.DAL.RequestContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(EmpyreBookApp.DAL.RequestContext context)
        {
            //Add some default stuff to our database
            //4 users.
            var comms = new List<Community>
            {
                new Community { CommunityID = 0, CName = "Western Oregon University", CShortName = "WOU", CUrl = "http://go.ctlcorp.com/wou/wp-content/uploads/2011/05/Western_Oregon_Wolves.png", BackgroundColor = "#FF0000", SecondaryBackgroundColor = "#FF1100"}
            };
            comms.ForEach(s => context.Communities.AddOrUpdate(s));
            context.SaveChanges();

            var genres = new List<Genre>
            {
                new Genre {GenreID = 0, CommunityID = 0, Name = "Math"},
                new Genre {GenreID = 1, CommunityID = 0, Name = "Computer Science"},
                new Genre {GenreID = 2, CommunityID = 0, Name = "Psychology"},
                new Genre {GenreID = 3, CommunityID = 0, Name = "Pre Med"},
                new Genre {GenreID = 4, CommunityID = 0, Name = "Socialogy"},
                new Genre {GenreID = 5, CommunityID = 0, Name = "Comunications"},
                new Genre {GenreID = 6, CommunityID = 0, Name = "Business"},
                new Genre {GenreID = 7, CommunityID = 0, Name = "Geology"},
            };
            genres.ForEach(s => context.Genres.AddOrUpdate(s));
            context.SaveChanges();

            var users = new List<User>
            {
                new User { UserID = 1, LastName = "Clouse", FirstName = "Zachary", UserName = "zclouse", ComunityID = 0, Contact = "5035550001", Email = "zclouse@empyre.net", Password = "asdf1234"},
                new User { UserID = 2, LastName = "Chuprov", FirstName = "Maria", UserName = "mchuprov", ComunityID = 0, Contact = "5035550002", Email = "mchuprov@empyre.net", Password = "asdf1234"},
                new User { UserID = 3, LastName = "McDonald", FirstName = "Riley", UserName = "rmcdonald", ComunityID = 0, Contact = "5035550003", Email = "rmcdonald@empyre.net", Password = "asdf1234"},
                new User { UserID = 4, LastName = "Wu", FirstName = "Wenjin", UserName = "wwu", ComunityID = 0, Contact = "5035550004", Email = "wwu@empyre.net", Password = "asdf1234"}
            };
            users.ForEach(s => context.Users.AddOrUpdate(s));
            context.SaveChanges();
        }
    }
}
