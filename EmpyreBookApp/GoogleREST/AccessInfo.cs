using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmpyreBookApp.GoogleREST
{
    public class AccessInfo
    {
        public string country { get; set; }
        public string viewability { get; set; }
        public Boolean embeddedable { get; set; }
        public Boolean publicDomain { get; set; }
        public string testToSpeechPermission { get; set; }
        public EPub epub { get; set; }
        public PDF pdf { get; set; }
        public string webReaderLink { get; set; }
        public string accessViewStatus { get; set; }
        public Boolean quoteSharingAllowed { get; set; }
    }
}