using System;

namespace EmpyreBookApp.Models
{
    public class Request
    {


        public int RequestID { get; set; }
        public int BIID { get; set; }
        public int UserID { get; set; }
        public string Preferance { get; set; }
        public string ISBN { get; set; }

        public DateTime RequestDate { get; set; }

        public virtual User User { get; set; }
        public virtual BI BI { get; set; }







    }
}