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
    }
}
