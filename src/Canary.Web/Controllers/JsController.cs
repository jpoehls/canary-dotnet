using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Canary.Web.Controllers
{
    public class JsController : Controller
    {
        //
        // GET: /js/canary.js

        [HttpGet]
        public ActionResult Canary()
        {
            string script = System.IO.File.ReadAllText(Server.MapPath("~/scripts/canary.js"));
            return JavaScript(script);
        }

    }
}
