using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmpyreBookApp.Models
{
    public class Genre
    {
        public int GenreID { get; set; }
        [Required]
        public int CommunityID { get; set; }
        [Required]
        public string Name { get; set; }
    }
}