using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace EmpyreBookApp.Models
{
    public class User
    {

        public int UserID { get; set; }
        public int ComunityID { get; set; }

        [Required]
        [RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Remote("validUsername", "User", ErrorMessage = "Username already exists.")]
        [StringLength(60, MinimumLength = 3)]
        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }



        [Required]
        public string Contact { get; set; }


        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [StringLength(150)]
        [Display(Name = "Email adress: ")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 6)]
        [Display(Name = "Password: ")]
        public string Password { get; set; }


        [Display(Name = "Full Name")]
        public string FullName
        {
            get
            {
                return LastName + " " + FirstName;
            }
        }
        public virtual Community community { get; set; }

        public virtual ICollection<Request> Requests { get; set; }

        public virtual ICollection<Trade> Trades { get; set; }


        public virtual ICollection<Book> Books { get; set; }

        //public virtual ICollection<Community> Communities { get; set; }
    }
}