using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using TemplateFull.Models;
using TemplateFull.Models.ViewModels;
using TemplateFull.Models.Interfaces;
using Ninject;
using Ninject.Components;


namespace TemplateFull.Controllers
{
    public class SummaryTablesController : Controller
    {
        private QclcdEntities db = new QclcdEntities();

        public ActionResult Index(int? beginMonth, int? beginDay, int? beginYear, int? endMonth, int? endDay, int? endYear, string stationState, string stationName)
        {
            // preserve form entries
            ViewBag.BeginMonth = beginMonth;
            ViewBag.BeginDay = beginDay;
            ViewBag.BeginYear = beginYear;
            ViewBag.EndMonth = endMonth;
            ViewBag.EndDay = endDay;
            ViewBag.EndYear = endYear;
            ViewBag.StationState = stationState;
            ViewBag.StationName = stationName;
            
            // validation logic - combination of begin month and day must be greater than or equal to the end month and day
            // validation logic - end year must be greater than or equal to the begin year
            // cannot us data datatype validation as this won't work with the day-range logic of the datasets
            // end validation errors to the view
            if (endYear < beginYear) {
                ModelState.AddModelError("Years", "ERROR - The end year must be greater than or equal to begin year");
            } else if (endMonth < beginMonth) {
                ModelState.AddModelError("Months", "ERROR - The end month must be greater than or equal to the begin month.");
            } else if ((endMonth == beginMonth) && (endDay < beginDay)) {
                ModelState.AddModelError("Days", "ERROR - The end day must be greater than or equal to the begin day.");
            }
     
            // choose view based on model validation
            if (ModelState.IsValid)
            {
                // build request objects and table view
                ISummaryRequest summaryRequest = new SummaryRequest(stationName, beginMonth, beginDay, beginYear, endMonth, endDay, endYear);
                ITablesView tableView = new TablesView(summaryRequest);
                return View("SummaryTables", tableView);
            }
            else
            {
                // build request objects and table view
                // null all values sent to SummaryRequest to avoid data errors
                ISummaryRequest summaryRequest = new SummaryRequest(null, null, null, null, null, null, null);
                ITablesView tableView = new TablesView(summaryRequest);
                return View("SummaryTables", tableView);
            }
        }


        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

    }
}
