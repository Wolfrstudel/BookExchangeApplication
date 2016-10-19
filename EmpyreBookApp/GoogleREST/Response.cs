using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmpyreBookApp.GoogleREST
{
    public class Response
    {
        public string kind { get; set; }
        public int totalItems { get; set; }
        public Book[] items { get; set; }
    }
}