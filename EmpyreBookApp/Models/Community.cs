using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmpyreBookApp.Models
{
    public class Community
    {
        public int CommunityID { get; set; }
        public string CName { get; set; }
        public string CShortName { get; set; }

        public string CUrl { get; set; }

        public string BackgroundColor { get; set; }

        public string SecondaryBackgroundColor { get; set; }

        public byte[] Picture { get; set; }

    }
}
