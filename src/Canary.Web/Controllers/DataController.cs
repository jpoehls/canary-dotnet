using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Canary.Data;

namespace Canary.Web.Controllers
{
    public class DataController : Controller
    {
        //
        // GET: /Data/

        public ActionResult Index(string appToken, string eventHash = null)
        {
            ViewBag.Token = appToken;

            if (eventHash == null)
            {
                var query = new GetAllEventsQuery();
                ViewBag.Events = query.Execute(appToken);
                return View("Dashboard");
            }
            else
            {
                var query = new GetSingleEventQuery();
                ViewBag.Event = query.Execute(appToken, eventHash);
                return View("EventDetails");
            }
        }

    }
}
