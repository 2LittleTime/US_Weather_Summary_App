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
using PagedList;


namespace TemplateFull.Controllers
{
    public class StationViewController : Controller
    {
        // create db context
        private QclcdEntities db = new QclcdEntities();

        public ViewResult Index(string sortOrder, string currentFilter, int? page, string stationState)
        {
            // set and preserve selected sort orders
            ViewBag.CurrentSort = sortOrder;
            ViewBag.WbanSortParam = String.IsNullOrEmpty(sortOrder) ? "Wban_Desc" : "";
            ViewBag.StateSortParam = sortOrder == "State" ? "State_Desc" : "State";
            ViewBag.StationNameSortParam = sortOrder == "StationName" ? "StationName_Desc" : "StationName";

            // set and preserve state filter
            if (stationState != null)
            {
                page = 1;
            }
            else
            {
                stationState = currentFilter;
            }

            ViewBag.CurrentFilter = stationState;
            ViewBag.StationState = stationState;

            // get all stations
            var stations = from s in db.Stations select s;

            // set current station state filter
            if (!String.IsNullOrEmpty(stationState))
            {
                stations = stations.Where(s => s.StationState.ToUpper().Contains(stationState.ToUpper()));
            }
            
            // set selected sort order
            switch (sortOrder)
            {
                case "Wban_Desc":
                    stations = stations.OrderByDescending(s => s.WbanId);
                    break;
                case "State":
                    stations = stations.OrderBy(s => s.StationState);
                    break;
                case "State_Desc":
                    stations = stations.OrderByDescending(s => s.StationState);
                    break;
                case "StationName":
                    stations = stations.OrderBy(s => s.StationName);
                    break;
                case "StationName_Desc":
                    stations = stations.OrderByDescending(s => s.StationName);
                    break;
                default: //WBAN ascending
                    stations = stations.OrderBy(s => s.WbanId);
                    break;
            }

            // set page size and page number
            int pageSize = 20;
            int pageNumber = (page ?? 1);

            // create station view object
            IStationView stationView = new StationView(stations, pageNumber, pageSize);

            // pass station view object to station view
            return View("StationView", stationView);

        }

        public ActionResult Details(string id = null)
        {
            Station station = db.Stations.Find(id);
            if (station == null)
            {
                return HttpNotFound();
            }
            return View(station);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

    }
}
