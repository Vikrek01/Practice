using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace UserIDPAssword.Models
{
    public class AdminDBContext:DbContext
    {
        public AdminDBContext() 
        {
            Database.SetInitializer<AdminDBContext>(new AdminDbInitializer());
        }
        public DbSet<Admin> Admins {  get; set; }
    }

    //Data seeding
    public class AdminDbInitializer : DropCreateDatabaseAlways<AdminDBContext> 
    { 
        protected override void Seed(AdminDBContext context)
        {
            var admin = new List<Admin>
            {
                new Admin() { Name="Vik", Email= "vikrantr.aspirefox@gmail.com", Password="Hii", GmailConfirm="Yes", Leave="No" },
                new Admin() { Name="Vicky", Email= "vikrant@gmail.com", Password="hello", GmailConfirm="No", Leave="No" }
            };
            admin.ForEach(a => context.Admins.Add(a));
            context.SaveChanges();
        }
    }

}