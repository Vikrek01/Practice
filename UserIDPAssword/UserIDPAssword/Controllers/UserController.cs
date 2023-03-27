using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using UserIDPAssword.Models;

namespace UserIDPAssword.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User model)
        {
            using (AdminDBContext context = new AdminDBContext())
            {

                using (var sha256 = SHA256.Create())
                {
                    var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(model.Password));
                    var hashedPassword = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();

                    // Store the hashed password in the database
                    



                    bool IsValidUser = context.Admins.Any(user => user.Email.ToLower() ==
                         model.UserEmail.ToLower() && user.Password == hashedPassword);
                    if (IsValidUser)
                    {
                        Session["username"] = model.UserEmail;
                        FormsAuthentication.SetAuthCookie(model.UserEmail, false);
                        return RedirectToAction("Index", "User");
                    }
                    ModelState.AddModelError("", "invalid Username or Password");
                    return View("Login");

                }
            }

        }


        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session["username"] = null;
            return RedirectToAction("Index", "User");
        }
    }
}