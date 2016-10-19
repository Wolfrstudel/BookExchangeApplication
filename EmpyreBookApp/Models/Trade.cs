using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace EmpyreBookApp.Models
{
    public class Trade
    {

        public int TradeID { get; set; }
        
        public int SellerID { get; set; }
        public int UserID { get; set; }
        //buyer id

        


        public string Review { get; set; }

        [Range(0, 5)]
        public int Credit { get; set; }

        public virtual User User { get; set; }
        //public virtual Book Book { get; set; }

    }
}