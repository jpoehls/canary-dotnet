using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Canary.Web.Controllers
{
    public class DataController : Controller
    {
        //
        // GET: /Data/

        public ActionResult Index(string appToken, string eventHash = null)
        {
            var store = new MongoCanaryStore();

            ViewBag.Token = appToken;
            ViewBag.Events = store.GetAllEvents(appToken);

            if (eventHash == null)
            {
                return View("Dashboard");
            }
            else
            {
                return View("EventDetails");
            }
        }

    }
}
