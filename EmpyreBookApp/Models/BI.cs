using System.Collections.Generic;

namespace EmpyreBookApp.Models
{
    public class BI
    {
        public int BIID { get; set; }
        
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Authors { get; set; }
        public string CoverLink { get; set; }
        public string Version { get; set; }
        public virtual ICollection<Request> Requests { get; set; }

    }
}