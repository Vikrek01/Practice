using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace UserIDPAssword.Models
{
    [NotMapped]
    public class Password
    {
        [Required]
        public string password { get; set; }
        [Required]
        [Compare("password",ErrorMessage ="Password and Confirm Password Should match")]
        public string ConfirmPassword { get; set; }
    }
}