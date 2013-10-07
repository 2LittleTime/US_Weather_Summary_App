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

namespace TemplateFull.Models.Interfaces
{
    public interface IStationView
    {
        // define public properties
        PagedList.IPagedList<TemplateFull.Models.Station> StationPageList { get; set; }
        IEnumerable<SelectListItem> StationStates { get; }
    }
}