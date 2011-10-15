using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;
using Canary.Web.Models;

namespace Canary.Web.Controllers
{
    public class SquawkController : Controller
    {
        //
        // POST: /squawk/

        [HttpPost]
        public ActionResult Index()
        {
            SquawkEventModel ev;

            // todo: log JSON body to mongo
            using (var reader = new StreamReader(Request.InputStream))
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                string json = reader.ReadToEnd();
                ev = serializer.Deserialize<SquawkEventModel>(json);
            }

            //var store = new MongoCanaryStore();
            //store.InsertEvent(ev);

            return new HttpStatusCodeResult((int)HttpStatusCode.OK);
        }

    }
}
