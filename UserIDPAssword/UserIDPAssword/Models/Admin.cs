using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserIDPAssword.Models
{
    public class Admin
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public string GmailConfirm { get; set; }

        public string Leave { get; set; }
    }
}