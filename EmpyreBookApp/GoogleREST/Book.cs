﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmpyreBookApp.GoogleREST
{
    public class Book
    {
        public string kind { get; set; }
        public string id { get; set; }
        public string etag { get; set; }
        public string selfLink { get; set; }
        public VolumeInfo volumeInfo { get; set; }
        public SaleInfo saleInfo { get; set; }
        public AccessInfo accessInfo { get; set; }
        public SearchInfo searchInfo { get; set; }
    }
}