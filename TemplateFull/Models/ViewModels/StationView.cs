using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList.Mvc;
using TemplateFull.Models;
using System.Data;
using System.Data.Entity;
using PagedList;
using TemplateFull.Models.Interfaces;

namespace TemplateFull.Models.ViewModels
{
    public class StationView : IStationView, IDisposable
    {
        // create db context
        private QclcdEntities db = new QclcdEntities();

        // define public properties
        public PagedList.IPagedList<TemplateFull.Models.Station> StationPageList {get; set;}
        public IEnumerable<SelectListItem> StationStates { get; private set; }

        // constructor
        public StationView(IQueryable<Station> stations, int pageNumber, int pageSize)
        {
            StationStates = getStationStates();
            StationPageList = stations.ToPagedList(pageNumber, pageSize);
        }

        // create states select list
        private IEnumerable<SelectListItem> getStationStates()
        {
            var sl = db.usp_StatesWithData().Select(x => new SelectListItem { Text = x, Value = x });
            return sl.ToList();
        }

        // dispose db context
        public void Dispose()
        {
            db.Dispose();
        }
    }
}