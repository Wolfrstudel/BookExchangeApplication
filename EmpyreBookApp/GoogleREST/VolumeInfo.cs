using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmpyreBookApp.GoogleREST
{
    public class VolumeInfo
    {
        public string title { get; set; }
        public string[] authors { get; set; }
        public string publisher { get; set; }
        public string publishedDate { get; set; }
        public string discription { get; set; }
        public IndustryID[] industryIdentifiers { get; set; }
        public int pageCount { get; set; }
        public string printType { get; set; }
        public string[] categories { get; set; }
        public double averageRating { get; set; }
        public int ratingCount { get; set; }
        public string contentVersion { get; set; }
        public ImageLink imageLinks { get; set; }
        public string language { get; set; }
        public string previewLink { get; set; }
        public string infoLink { get; set; }
        public string canonicalVolumeLink { get; set; }
    }
}