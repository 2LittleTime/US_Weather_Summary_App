using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TemplateFull.Models.Interfaces
{
    public interface IWeatherSummaryFilter
    {
        // define public properties
        string StationName { get; set; }
        string StationState { get; set; }
        string WBanId { get; set; }
        int? BeginMonth { get; set; }
        int? BeginDay { get; set; }
        int? BeginYear { get; set; }
        int? EndMonth { get; set; }
        int? EndDay { get; set; }
        int? EndYear { get; set; }
        IEnumerable<SelectListItem> StationStates { get; }
        IEnumerable<SelectListItem> StationNames { get; }
        IEnumerable<SelectListItem> BeginMonthList { get; }
        IEnumerable<SelectListItem> BeginDayList { get; }
        IEnumerable<SelectListItem> BeginYearList { get; }
        IEnumerable<SelectListItem> EndMonthList { get; }
        IEnumerable<SelectListItem> EndDayList { get; }
        IEnumerable<SelectListItem> EndYearList { get; }
    }
}