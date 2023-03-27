using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.EnterpriseServices;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Xml.Linq;
using UserIDPAssword.Models;
using PagedList;
using PagedList.Mvc;

namespace UserIDPAssword.Controllers
{
    public class AdminsController : Controller
    {
        private AdminDBContext db = new AdminDBContext();

        // GET: Admins
        public ActionResult Index(string search, int? page)
        {
            return View(db.Admins.Where(x => x.Name.StartsWith(search) || search == null).ToList().ToPagedList(page ?? 1, 3));
        }

        // GET: Admins/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin admin = db.Admins.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View(admin);
        }

        // GET: Admins/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Email,Password,GmailConfirm, Leave")] Admin admin)
        {
            if (ModelState.IsValid)
            {
                admin.Id = Guid.NewGuid();
                admin.GmailConfirm = "Not approved";
                admin.Leave = "No leave";
                db.Admins.Add(admin);

                string confirmationUrl = Url.Action(
                                             "Index",
                                             "Gmail",
                                             new { id = admin.Id.ToString() },
                                             Request.Url.Scheme
                                                    );
                string body = string.Format("<a href='{0}'>Click here to set password</a>", confirmationUrl);

                /*                string body = string.Format("<a href='https://localhost:44345/Gmail/Index/{0}'>Click here to set password</a>", admin.Id.ToString());
                */
                SendEmail(body, admin.Email);

                db.SaveChanges();


                return RedirectToAction("Index");
            }

            return View(admin);
        }
        //Gmail  Code//
        public void SendEmail(string body, string emailAddress)
        {
            try
            {

                MailMessage message = new MailMessage();
                message.From = new MailAddress("vikrantr.aspirefox@gmail.com");
                message.To.Add(emailAddress);
                message.IsBodyHtml = true;
                message.Subject = "Generate Password"; 
                message.Body = body;
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                client.Credentials = new System.Net.NetworkCredential("vikrantr.aspirefox@gmail.com", "yzramxxbbxlkeqav");
                client.EnableSsl = true;
                client.Send(message);

                Console.WriteLine($"Email sent to {emailAddress}"); Console.WriteLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine($"Error sending email to {emailAddress}: {ex.Message}"); Console.WriteLine();
            }
        }



        // GET: Admins/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin admin = db.Admins.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View(admin);
        }

        // POST: Admins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Email,Password,GmailConfirm")] Admin admin)
        {
            if (ModelState.IsValid)
            {
                db.Entry(admin).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(admin);
        }

        // GET: Admins/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin admin = db.Admins.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View(admin);
        }

        // POST: Admins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Admin admin = db.Admins.Find(id);
            db.Admins.Remove(admin);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
