using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cannon.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public JsonResult Fire()
        {
            if (CannonController.Fire())
            {
                return this.Json("Fired!");
            }
            else
            {
                return this.Json("Couldn't Fire?...");
            }
        }
    }
}
