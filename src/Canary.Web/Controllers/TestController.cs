using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Canary.Web.Controllers
{
    public class TestController : Controller
    {
        //
        // GET: /test/

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

    }
}
