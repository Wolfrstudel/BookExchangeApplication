using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmpyreBookApp.Models
{
    public class Book
    {

        public int BookID { get; set; }
        public int UserID { get; set; }
        //seller id
        public int BIID { get; set; }
        public string Description { get; set; }
        public string SoldTo { get; set; }
        public string ISBN { get; set; }
        public string Condition { get; set; }
        public string Photo { get; set; }
        public int GenreID { get; set; }
        public DateTime PostDate { get; set; }
        [Required]
        public string Price { get; set; }
        //public virtual Trade Trade { get; set; }
        public virtual User User { get; set; }

        public virtual BI BI { get; set; }
        public virtual Genre Genre { get; set; }

    }
}