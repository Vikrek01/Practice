using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UserIDPAssword.Models
{
    [NotMapped]
    public class User
    {
        [Required]
        public string UserEmail { get; set; }

        [Required]
        public string Password { get; set; }
    }
}