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
                        || (emailContact.WebMessage.ToUpper().Contains("VERDIENEN"))            // utf-8
                        || (emailContact.ContactName.ToUpper().Contains("VERDIENEN"))           // utf-8
                        || (emailContact.ContactEmail.ToUpper().Contains("VERDIENEN"))          // utf-8
                        || (emailContact.WebMessage.Contains("Vеrdienen Sie Gеld"))             // utf-8
                        || (emailContact.WebMessage.Contains("vеrdienen sie gеld"))             // utf-8
                        || (emailContact.WebMessage.ToUpper().Contains("PASSIVES EINKOMMEN"))   // utf-8
                        || (emailContact.ContactName.ToUpper().Contains("PASSIVES EINKOMMEN"))  // utf-8
                        || (emailContact.ContactEmail.ToUpper().Contains("PASSIVESEINKOMMEN"))  // utf-8
                        || (emailContact.WebMessage.ToUpper().Contains("EINKOMMEN"))            // utf-8
                        || (emailContact.WebMessage.Contains("Passives Einkommеn"))             // utf-8
                        || (emailContact.WebMessage.Contains("Einkommеn"))                      // utf-8
                        || (emailContact.WebMessage.Contains("passives einkommеn"))             // utf-8
                        || (emailContact.WebMessage.Contains("einkommеn"))                      // utf-8
                        || (emailContact.WebMessage.ToUpper().Contains("SEX"))                  // utf-8
                        || (emailContact.WebMessage.ToUpper().Contains("SPAM"))                 // utf-8
                        || (emailContact.WebMessage.ToUpper().Contains("DATING"))               // utf-8
                        || (emailContact.WebMessage.ToUpper().Contains("BITCOIN"))              // utf-8
                        || (emailContact.ContactName.ToUpper().Contains("SEX"))                 // utf-8
                        || (emailContact.ContactName.ToUpper().Contains("ADULT"))               // utf-8
                        || (emailContact.ContactName.ToUpper().Contains("DATING"))              // utf-8
                        || (emailContact.ContactName.ToUpper().Contains("SEX"))                // utf-8
                        || (emailContact.ContactName.ToUpper().Contains("ADULT"))              // utf-8
                        || (emailContact.ContactName.ToUpper().Contains("DATING"))             // utf-8
                        || (emailContact.ContactName.ToUpper().Contains("PAYMENT"))            // utf-8
                        || (emailContact.ContactName.ToUpper().Contains("EINNAHMEN"))          // utf-8
                        || (emailContact.ContactName.ToUpper().Contains("ZAHLUNG"))            // utf-8
                        || (emailContact.ContactName.ToUpper().Contains("PASSIVE INCOME"))     // utf-8
                        || (emailContact.ContactName.ToUpper().Contains("INCOME"))             // utf-8
                        || (emailContact.ContactName.ToUpper().Contains("HENRYESSED"))         // utf-8
                        || (emailContact.ContactName.ToUpper().Contains("HENRY ESSED"))        // utf-8
                        || (emailContact.ContactName.ToUpper().Contains("SOCIAL MEDIA"))       // utf-8
                        || (emailContact.ContactName.ToUpper().Contains("WEB DOMAIN"))         // utf-8
                        || (emailContact.WebMessage.ToUpper().Contains("WEB DOMAIN"))         // utf-8
                        || (emailContact.WebMessage.ToUpper().Contains("SOCIAL MEDIA"))        // utf-8
                        || (emailContact.WebMessage.ToUpper().Contains("WEB DOMAIN"))          // utf-8
                        || (emailContact.WebMessage.ToUpper().Contains("FEEDBACK FORM"))       // utf-8
                        || (emailContact.ContactName.ToUpper().Contains("FEEDBACK FORM"))      // utf-8
                        || (emailContact.WebMessage.ToUpper().Contains("PAID COMMISSION"))     // utf-8
                        || (emailContact.ContactName.ToUpper().Contains("PAID COMMISSION"))    // utf-8
                        || (emailContact.ContactEmail.ToUpper().Contains(".RU"))               // utf-8
                        || (emailContact.WebMessage.ToUpper().Contains("PHARMACY"))            // utf-8
                        || (emailContact.ContactName.ToUpper().Contains("PHARMACY"))           // utf-8
                        || (emailContact.WebMessage.ToUpper().Contains("N95"))                 // utf-8
                        || (emailContact.ContactName.ToUpper().Contains("N95"))                 // utf-8
                        || (emailContact.WebMessage.ToUpper().Contains("BACKLINK"))             // utf-8
                        || (emailContact.ContactName.ToUpper().Contains("BACKLINK"))            // utf-8
                        || (emailContact.WebMessage.ToUpper().Contains("DOMAIN"))               // utf-8
                        || (emailContact.ContactName.ToUpper().Contains("DOMAIN"))              // utf-8
                        || (emailContact.WebMessage.ToUpper().Contains("DOMAIN SERVICES"))      // utf-8
                        || (emailContact.ContactName.ToUpper().Contains("DOMAIN SERVICES"))     // utf-8
                        || (emailContact.WebMessage.ToUpper().Contains("MICHAELGWORKMAN.COM"))  // utf-8
                        || (emailContact.ContactEmail.ToUpper().Contains("MICHAELGWORKMAN.COM"))// utf-8
                        || (emailContact.WebMessage.ToUpper().Contains("TRAFFIC"))              // utf-8
                        || (emailContact.WebMessage.ToUpper().Contains("MICHAELGWORKMAN.COM"))  // ansi
                        || (emailContact.WebMessage.ToUpper().Contains("TRAFFIC"))              // ansi
                        || (emailContact.ContactEmail.ToUpper().Contains("MICHAELGWORKMAN.COM"))// ansi
                        || (emailContact.WebMessage.ToUpper().Contains("PHARMACY"))             // ansi
                        || (emailContact.ContactName.ToUpper().Contains("PHARMACY"))            // ansi
                        || (emailContact.WebMessage.ToUpper().Contains("N95"))                  // ansi
                        || (emailContact.ContactName.ToUpper().Contains("N95"))                 // ansi
                        || (emailContact.WebMessage.ToUpper().Contains("BACKLINK"))             // ansi
                        || (emailContact.ContactName.ToUpper().Contains("BACKLINK"))            // ansi
                        || (emailContact.WebMessage.ToUpper().Contains("DOMAIN"))               // ansi
                        || (emailContact.ContactName.ToUpper().Contains("DOMAIN"))              // ansi
                        || (emailContact.WebMessage.ToUpper().Contains("DOMAIN SERVICES"))      // ansi
                        || (emailContact.ContactName.ToUpper().Contains("DOMAIN SERVICES"))     // ansi
                        || (emailContact.WebMessage.ToUpper().Contains("HENRYESSED"))           // ansi
                        || (emailContact.WebMessage.ToUpper().Contains("HENRY ESSED"))          // ansi
                        || (emailContact.WebMessage.ToUpper().Contains("PAYMENT"))              // ansi
                        || (emailContact.WebMessage.ToUpper().Contains("ZAHLUNG"))              // ansi
                        || (emailContact.WebMessage.ToUpper().Contains("EINNAHMEN"))            // ansi
                        || (emailContact.WebMessage.ToUpper().Contains("PASSIVE INCOME"))       // ansi
                        || (emailContact.WebMessage.ToUpper().Contains("INCOME"))               // ansi
                        || (emailContact.WebMessage.ToUpper().Contains("VERDIENEN SIE GELD"))   // ansi
                        || (emailContact.WebMessage.ToUpper().Contains("VERDIENEN"))            // ansi
                        || (emailContact.ContactName.ToUpper().Contains("VERDIENEN"))           // ansi
                        || (emailContact.ContactEmail.ToUpper().Contains("VERDIENEN"))          // ansi
                        || (emailContact.WebMessage.Contains("Verdienen Sie Geld"))             // ansi
                        || (emailContact.WebMessage.Contains("verdienen sie geld"))             // ansi
                        || (emailContact.WebMessage.ToUpper().Contains("PASSIVES EINKOMMEN"))   // ansi
                        || (emailContact.ContactName.ToUpper().Contains("PASSIVES EINKOMMEN"))  // ansi
                        || (emailContact.ContactEmail.ToUpper().Contains("PASSIVESEINKOMMEN"))  // ansi
                        || (emailContact.ContactEmail.ToUpper().Contains(".RU"))                // ansi
                        || (emailContact.WebMessage.ToUpper().Contains("EINKOMMEN"))            // ansi
                        || (emailContact.WebMessage.Contains("Passives Einkommen"))             // ansi
                        || (emailContact.WebMessage.Contains("Einkommen"))                      // ansi
                        || (emailContact.WebMessage.Contains("passives einkommen"))             // ansi
                        || (emailContact.WebMessage.Contains("einkommen"))                      // ansi
                        || (emailContact.WebMessage.ToUpper().Contains("SPAM"))                 // ansi
                        || (emailContact.WebMessage.ToUpper().Contains("BITCOIN"))              // ansi
                        || (emailContact.ContactName.ToUpper().Contains("SEX"))                 // ansi
                        || (emailContact.ContactName.ToUpper().Contains("ADULT"))               // ansi
                        || (emailContact.ContactName.ToUpper().Contains("DATING"))              // ansi
                        || (emailContact.ContactName.ToUpper().Contains("PASSIVE INCOME"))      // ansi
                        || (emailContact.ContactName.ToUpper().Contains("INCOME"))              // ansi
                        || (emailContact.ContactPhone.ToUpper().Contains("SEX"))                // ansi
                        || (emailContact.ContactPhone.ToUpper().Contains("ADULT"))              // ansi
                        || (emailContact.ContactPhone.ToUpper().Contains("DATING"))             // ansi
                        || (emailContact.ContactName.ToUpper().Contains("SOCIAL MEDIA"))        // ansi
                        || (emailContact.WebMessage.ToUpper().Contains("SOCIAL MEDIA"))         // ansi
                        || (emailContact.ContactName.ToUpper().Contains("WEB DOMAIN"))          // ansi
                        || (emailContact.WebMessage.ToUpper().Contains("WEB DOMAIN"))           // ansi
                        || (emailContact.ContactName.ToUpper().Contains("FEEDBACK FORM"))       // ansi
                        || (emailContact.WebMessage.ToUpper().Contains("FEEDBACK FORM"))        // ansi
                        || (emailContact.ContactName.ToUpper().Contains("PAID COMMISSION"))     // ansi
                        || (emailContact.WebMessage.ToUpper().Contains("PAID COMMISSION"))      // ansi
                        )
                    {
                        // make error message
                        ViewBag.ErrorMessage = "Invalid Message Content, Message Not Sent";

                        // show error message
                        return View("~/Views/Shared/Error.cshtml");
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


                        // create email object for text message and initialize data
                        OutlookMailer textSender = new OutlookMailer();
                        textSender.ToEmail = "3214329295@txt.att.net";
                        textSender.FromEmail = emailContact.ContactEmail;
                        textSender.FromName = emailContact.ContactName;

                        // get the text category and set the text body
                        string textCategory = db.ContactCategories.Where(x => x.ID == emailContact.ContactCategories_ID).SingleOrDefault().category;
                        textSender.Subject = "Michael G. Workman Career Inquiry";
                        textSender.Body = "From Name: " + emailContact.ContactName +
                                      " From Email: " + emailContact.ContactEmail +
                                      " Phone: " + emailContact.ContactPhone +
                                      " Category: " + textCategory +
                                      "<br>" + emailContact.WebMessage;

                        // send text message
                        textSender.Send();

                        // create email object for second text message, a copy of first text message, and initialize data
                        OutlookMailer textSender2 = new OutlookMailer();
                        textSender2.ToEmail = "3213946240@txt.att.net";
                        textSender2.FromEmail = textSender.FromEmail;
                        textSender2.FromName = textSender.FromName;

                        // get the second text category and set the second text body
                        textSender2.Subject = textSender.Subject;
                        textSender2.Body = textSender.Body;

                        // send second text message
                        textSender2.Send();

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
