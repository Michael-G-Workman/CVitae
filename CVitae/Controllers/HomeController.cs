using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Net.Mail;
using System.Web.Mvc;
using CVitae.Models;
using CVitae.DAL;

namespace CVitae.Controllers
{
    public class HomeController : Controller
    {
        private CVitaeContext db = new CVitaeContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        // Contact GET
        public ActionResult Contact()
        {
            ViewBag.Message = "Michael G. Workman Contact";

            // build select list of contact categories
            ViewBag.Categories = new SelectList(db.ContactCategories.OrderBy(x => x.sortorder), "ID", "category");

            return View();
        }

        // Contact POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contact([Bind(Include = "ContactName, ContactEmail, ContactPhone, ContactCategories_ID, WebMessage")] EmailContact emailContact)
        {
            ViewBag.Message = "Michael G. Workman Contact";

            try
            {
                // update database with contact information
                if (ModelState.IsValid)
                {
                    if ((emailContact.WebMessage.ToUpper().Contains("ADULT") 
                        && (emailContact.WebMessage.ToUpper().Contains("SEX")))
                        || (emailContact.WebMessage.ToUpper().Contains("ADULT") 
                        && (emailContact.WebMessage.ToUpper().Contains("DATE")))
                        || (emailContact.WebMessage.ToUpper().Contains("ADULT") 
                        && (emailContact.WebMessage.ToUpper().Contains("DATING")))
                        || (emailContact.WebMessage.ToUpper().Contains("ADULT") 
                        && (emailContact.WebMessage.ToUpper().Contains("HOT")))
                        || (emailContact.WebMessage.ToUpper().Contains("GIRL") 
                        && (emailContact.WebMessage.ToUpper().Contains("SEX")))
                        || (emailContact.WebMessage.ToUpper().Contains("GIRL") 
                        && (emailContact.WebMessage.ToUpper().Contains("DATE")))
                        || (emailContact.WebMessage.ToUpper().Contains("GIRL") 
                        && (emailContact.WebMessage.ToUpper().Contains("DATING")))
                        || (emailContact.WebMessage.ToUpper().Contains("GIRL") 
                        && (emailContact.WebMessage.ToUpper().Contains("HOT")))
                        || (emailContact.WebMessage.ToUpper().Contains("WEB")
                        && (emailContact.WebMessage.ToUpper().Contains("AD")))
                        || (emailContact.WebMessage.ToUpper().Contains("WEB")
                        && (emailContact.WebMessage.ToUpper().Contains("ADVERTISEMENT")))
                        || (emailContact.WebMessage.ToUpper().Contains("OAKLEY") 
                        && (emailContact.WebMessage.ToUpper().Contains("RAY BAN")))
                        || (emailContact.WebMessage.ToUpper().Contains("PASSIVE")       // ansi
                        && (emailContact.WebMessage.ToUpper().Contains("INCOME")))      // ansi
                        || (emailContact.WebMessage.ToUpper().Contains("PASSIVE")       // utf-8
                        && (emailContact.WebMessage.ToUpper().Contains("INCOME")))      // utf-8
                        || (emailContact.WebMessage.Contains("Passive")                 // utf-8
                        && (emailContact.WebMessage.Contains("Income")))                // utf-8
                        || (emailContact.WebMessage.Contains("passive")                 // utf-8
                        && (emailContact.WebMessage.Contains("income")))                // utf-8
                        || (emailContact.WebMessage.ToUpper().Contains("SEX"))
                        || (emailContact.WebMessage.ToUpper().Contains("DATING"))
                        || (emailContact.WebMessage.ToUpper().Contains("SEO"))
                        || (emailContact.WebMessage.ToUpper().Contains("S.E.O."))
                        || (emailContact.WebMessage.ToUpper().Contains("GOOGLE"))
                        || (emailContact.WebMessage.ToUpper().Contains("OAKLEY"))
                        || (emailContact.WebMessage.ToUpper().Contains("RAY BAN"))
                        || (emailContact.WebMessage.ToUpper().Contains("RAY-BAN"))
                        || (emailContact.WebMessage.ToUpper().Contains("RAYBAN"))
                        || (emailContact.WebMessage.ToUpper().Contains("SUNGLASS"))
                        || (emailContact.WebMessage.ToUpper().Contains("PENNY STOCK"))
                        || (emailContact.WebMessage.ToUpper().Contains("VERDIENEN SIE GELD"))   // utf-8
                        || (emailContact.WebMessage.Contains("Vеrdienen Sie Gеld"))             // utf-8
                        || (emailContact.WebMessage.Contains("vеrdienen sie gеld"))             // utf-8
                        || (emailContact.WebMessage.ToUpper().Contains("PASSIVES EINKOMMEN"))   // utf-8
                        || (emailContact.WebMessage.ToUpper().Contains("EINKOMMEN"))            // utf-8
                        || (emailContact.WebMessage.Contains("Passives Einkommеn"))             // utf-8
                        || (emailContact.WebMessage.Contains("Einkommеn"))                      // utf-8
                        || (emailContact.WebMessage.Contains("passives einkommеn"))             // utf-8
                        || (emailContact.WebMessage.Contains("einkommеn"))                      // utf-8
                        || (emailContact.WebMessage.ToUpper().Contains("SEX"))                  // utf-8
                        || (emailContact.WebMessage.ToUpper().Contains("SPAM"))                 // utf-8
                        || (emailContact.WebMessage.ToUpper().Contains("DATING"))               // utf-8
                        || (emailContact.WebMessage.ToUpper().Contains("BITCOIN"))              // utf-8
                        || (emailContact.WebMessage.ToUpper().Contains("VERDIENEN SIE GELD"))   // ansi
                        || (emailContact.WebMessage.Contains("Verdienen Sie Geld"))             // ansi
                        || (emailContact.WebMessage.Contains("verdienen sie geld"))             // ansi
                        || (emailContact.WebMessage.ToUpper().Contains("PASSIVES EINKOMMEN"))   // ansi
                        || (emailContact.WebMessage.ToUpper().Contains("EINKOMMEN"))            // ansi
                        || (emailContact.WebMessage.Contains("Passives Einkommen"))             // ansi
                        || (emailContact.WebMessage.Contains("Einkommen"))                      // ansi
                        || (emailContact.WebMessage.Contains("passives einkommen"))             // ansi
                        || (emailContact.WebMessage.Contains("einkommen"))                      // ansi
                        || (emailContact.WebMessage.ToUpper().Contains("SPAM"))                 // ansi
                        || (emailContact.WebMessage.ToUpper().Contains("BITCOIN"))              // ansi
                        )
                    {
                        // do nothing, spam email
                        return View("Index");
                    }
                    else
                    {

                        db.EmailContacts.Add(emailContact);
                        db.SaveChanges();

                        // create Gmailer object and initialize data
                        OutlookMailer mailer = new OutlookMailer();
                        mailer.ToEmail = "michael.g.workman@gmail.com";
                        mailer.FromEmail = emailContact.ContactEmail;
                        mailer.FromName = emailContact.ContactName;

                        // get the email category and set the email Body
                        string emailCategory = db.ContactCategories.Where(x => x.ID == emailContact.ContactCategories_ID).SingleOrDefault().category;
                        mailer.Subject = "Michael G. Workman Career Inquiry - Category: " + emailCategory;
                        mailer.Body = "From Name: " + emailContact.ContactName +
                                      " From Email: " + emailContact.ContactEmail +
                                      " Phone: " + emailContact.ContactPhone +
                                      "<br>" + emailContact.WebMessage;

                        // send email
                        mailer.Send();

                        return View("ContactConfirmation");
                    }
                }
                else
                {
                    ViewBag.ErrorMessage = "Contact View, Model State Not Valid, Email Not Sent.";

                    return View("~/Views/Shared/Error.cshtml");
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Error Encountered: " + ex.Message + " Inner Exception: " + ex.InnerException;
                return View("~/Views/Shared/Error.cshtml");
            }
        }

        // mailer class courtesy of stackoverflow.com
        public class OutlookMailer
        {
            public static string OutlookUsername { get; set; }
            public static string OutlookPassword { get; set; }
            public static string OutlookHost { get; set; }
            public static int OutlookPort { get; set; }
            public static bool OutlookSSL { get; set; }
            public string ToEmail { get; set; }
            public string FromEmail { get; set; }
            public string FromName { get; set; }
            public string Subject { get; set; }
            public string Body { get; set; }

            static OutlookMailer()
            {
                OutlookHost = "smtp-mail.outlook.com";
                OutlookPort = 587;
                OutlookSSL = true;
            }

            public void Send()
            {
                SmtpClient smtp = new SmtpClient();
                smtp.Host = OutlookHost;
                smtp.Port = OutlookPort;
                smtp.EnableSsl = OutlookSSL;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential("michael.g.workman@outlook.com", "REDACTED");

                MailMessage message = new MailMessage();
                message.From = new MailAddress("michael.g.workman@outlook.com");
                message.To.Add(new MailAddress(ToEmail));
                message.Subject = Subject;
                message.Body = Body;
                message.IsBodyHtml = true;
                smtp.Send(message);
            }
        }
    }
}
