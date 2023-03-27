using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using UserIDPAssword.Models;

namespace UserIDPAssword.Controllers
{
    public class GmailController : Controller
    {
        AdminDBContext db = new AdminDBContext();
        // GET: Gmail
        public ActionResult Index(Guid? id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}

            //Admin admin = db.Admins.Find(id);
            //if (admin == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(admin);
            return View();
        }

        [HttpPost]
        public ActionResult Index(Guid id, Password p)
        {
            if (ModelState.IsValid)
            {
                Admin admin = db.Admins.Find(id);
                if (admin == null)
                {
                    return HttpNotFound();
                }





                // Hash the password using SHA256 algorithm
                using (var sha256 = SHA256.Create())
                {
                    var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(p.ConfirmPassword));
                    var hashedPassword = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();

                    // Store the hashed password in the database
                    admin.Password = hashedPassword;
                }
               // admin.Password = p.ConfirmPassword;
                admin.GmailConfirm = "Approved";
                db.Entry(admin).State = EntityState.Modified;
                db.SaveChanges();
                

            }
            return RedirectToAction("PasswordMessage");

        }



        public ActionResult PasswordMessage()
        {
            return View();
        }
    }
}