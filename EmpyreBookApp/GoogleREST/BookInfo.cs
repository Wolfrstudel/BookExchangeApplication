using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmpyreBookApp.GoogleREST
{
    public class BookInfo
    {
        public int BIID { get; set; }

        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Authors { get; set; }
        public string CoverLink { get; set; }
	    public string Version {get; set; }

        public string toString()
        {
            return Title + " (" + Version + ")" + "; " + Authors + ", " + ISBN + " (" + CoverLink + ").";
        }
        //public virtual ICollection<Request> Requests { get; set; }
    }
}