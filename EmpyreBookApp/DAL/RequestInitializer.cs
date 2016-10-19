using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using EmpyreBookApp.Models;


namespace EmpyreBookApp.DAL
{
    
        public class RequestInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<RequestContext>
        {
            protected override void Seed(RequestContext context)
            {
                /*
                var bis = new List<BI> { new BI { ISBN="123-456-7890" } };

                bis.ForEach(s => context.BIs.Add(s));
                context.SaveChanges();
                */

                /*var users = new List<User> { new User { LastName = "Carson", FirstName = "Alexander", UserName="supperman",Email="superman@hotmail.com",School="Western Oregon University",Contact="5038386660" } };
                users.ForEach(s => context.Users.Add(s));
                context.SaveChanges();

                /*
                var requests = new List<Request> { new Request { BIID = 1, UserID = 1, Preferance = "test test", ISBN = "123-456-7890", RequestDate = DateTime.Parse("2005-09-01") } };

                requests.ForEach(s => context.Requests.Add(s));
                context.SaveChanges();
                */
            }


    }
}