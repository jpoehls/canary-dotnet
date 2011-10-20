using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;

namespace Canary.Web.Controllers
{
    public class SquawkController : Controller
    {
        //
        // POST: /squawk/

        [HttpPost]
        public ActionResult Index()
        {
            using (var reader = new StreamReader(Request.InputStream))
            {
                string json = reader.ReadToEnd();
                SquawkEventLogger.LogEvent(json);
            }

            return new HttpStatusCodeResult((int)HttpStatusCode.OK);
        }

    }
}
