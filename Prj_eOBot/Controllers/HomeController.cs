using Prj_Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Prj_eOBot.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            RI_Users user = (RI_Users)Session["User"];
            ViewBag.Role = user.Role;
            return View();
        }

       
    }
}