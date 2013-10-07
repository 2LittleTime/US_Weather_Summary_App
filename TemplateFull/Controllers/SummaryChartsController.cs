using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using TemplateFull.Models;

namespace TemplateFull.Controllers
{
    public class SummaryChartsController : Controller
    {
        // create db context
        private QclcdEntities db = new QclcdEntities();

        public ActionResult Index()
        {
            return View("SummaryCharts");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
