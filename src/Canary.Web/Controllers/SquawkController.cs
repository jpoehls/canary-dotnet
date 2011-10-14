using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;

namespace Canary.Web.Controllers
{
    public class SquawkController : Controller
    {
        //
        // GET: /Squawk/

        public ActionResult Index()
        {
            // todo: log JSON body to mongo

            return new HttpStatusCodeResult((int)HttpStatusCode.OK);
        }

    }
}
