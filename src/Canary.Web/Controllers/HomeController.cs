using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Canary.Web.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /

        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.Token = Guid.NewGuid().ToString().Replace("-", "");

            return View();
        }

    }
}
